using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResponse> GetJwtToken(User user);
    }
}
