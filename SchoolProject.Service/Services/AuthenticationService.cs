using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure.Interfaces;
using SchoolProject.Service.Interfaces;
using System.Collections.Concurrent;
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
        private readonly ConcurrentDictionary<string, RefreshToken> refreshTokenDictionary;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        #endregion

        #region Ctor
        public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            refreshTokenDictionary = new ConcurrentDictionary<string, RefreshToken>();
        }

        #endregion


        #region Methods
        public async Task<JwtAuthResponse> GetJwtToken(User user)
        {
            var claims = GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SecretKey)), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = GetRefreshToken(user.UserName);

            var userRefreshToken = new UserRefreshToken
            {
                RefreshToken = refreshToken.Token,
                CreatedAt = DateTime.UtcNow,
                ExpireOn = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpirationInDays),
                IsRevoked = false,
                IsUsed = false,
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
        private RefreshToken GetRefreshToken(string userName)
        {
            var refreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                ExpireOn = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays),
                UserName = userName
            };
            refreshTokenDictionary.AddOrUpdate(refreshToken.Token, refreshToken, (s, r) => refreshToken);

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
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),

             };
            return claims;
        }
        #endregion
    }
}
