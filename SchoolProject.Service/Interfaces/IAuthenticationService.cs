using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolProject.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResponse> GetJwtToken(User user);
        public Task<JwtAuthResponse> RefreshToken(User user, string refreshToken, DateTime expDate);
        public Task<string> ValidateToken(string accessToken);
        public JwtSecurityToken ReadJwtToken(string accessToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken);

    }
}
