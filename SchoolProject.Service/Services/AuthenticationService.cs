using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure.Interfaces;
using SchoolProject.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Ctor
        public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
        }

        #endregion


        #region Methods
        public async Task<JwtAuthResponse> GetJwtToken(User user)
        {
            var (jwtToken, accessToken) = GenerateJwtToken(user);

            var refreshToken = GetRefreshToken(user.UserName);

            var userRefreshToken = new UserRefreshToken
            {
                RefreshToken = refreshToken.Token,
                CreatedAt = DateTime.UtcNow,
                ExpireOn = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpirationInDays),
                IsRevoked = false,
                IsUsed = true,
                JwtId = jwtToken.Id,
                Token = accessToken,
                UserId = user.Id
            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);
            return new JwtAuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        private (JwtSecurityToken, string) GenerateJwtToken(User user)
        {
            var claims = GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SecretKey)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return (jwtToken, accessToken);
        }
        private RefreshToken GetRefreshToken(string userName)
        {
            var refreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                ExpireOn = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays),
                UserName = userName
            };
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        public List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
{
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),

             };
            return claims;
        }

        public async Task<JwtAuthResponse> RefreshToken(User user, string refreshToken, DateTime expDate)
        {


            var (newJwtToken, newAccessToken) = GenerateJwtToken(user);

            var response = new JwtAuthResponse();
            response.AccessToken = newAccessToken;

            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = user.UserName;
            refreshTokenResult.Token = refreshToken;
            refreshTokenResult.ExpireOn = expDate;

            response.RefreshToken = refreshTokenResult;

            return response;
        }
        public JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException(nameof(accessToken));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);

            return jwtToken;
        }

        public async Task<string> ValidateToken(string accessToken)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);

            try
            {
                var principal = tokenHandler.ValidateToken(accessToken,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = _jwtSettings.ValidateIssuer,
                        ValidIssuers = new[] { _jwtSettings.Issuer },
                        ValidateAudience = _jwtSettings.ValidateAudience,
                        ValidAudience = _jwtSettings.Audience,
                        ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SecretKey)),
                        ValidateLifetime = _jwtSettings.ValidateLifetime
                    },
                out SecurityToken validatedToken);
                if (principal is null)
                    return "InvalidToken";

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {

            if (jwtToken is null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("InvalidAlgorithm", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }

            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == nameof(UserClaimModel.Id)).Value;
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                .FirstOrDefaultAsync(rf => rf.RefreshToken == refreshToken &&
                rf.Token == accessToken &&
                rf.UserId == int.Parse(userId));

            if (userRefreshToken is null)
            {
                return ("InvalidRefreshToken", null);

            }
            if (userRefreshToken.ExpireOn < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenIsExpired", null);
            }
            var expDate = userRefreshToken.ExpireOn;
            return (userId, expDate);
        }
        #endregion
    }
}
