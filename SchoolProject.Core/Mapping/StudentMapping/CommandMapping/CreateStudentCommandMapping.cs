using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping
{
    public partial class StudentProfile
    {
        public void CreateStudentCommandMapping()
        {
            CreateMap<CreateStudentCommand, Student>()
                .ForMember(dest => dest.DID, options => options.MapFrom(src => src.DepartmentId));
        }
    }
}
