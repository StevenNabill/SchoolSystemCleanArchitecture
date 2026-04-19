using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void AddUpdateUserCommandMapping()
        {
            CreateMap<EditUserCommand, User>();
        }
    }
}
