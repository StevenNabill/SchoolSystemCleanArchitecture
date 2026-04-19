using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void AddUserPaginationMapping()
        {
            CreateMap<User, GetUserPaginationResponse>();
        }
    }
}
