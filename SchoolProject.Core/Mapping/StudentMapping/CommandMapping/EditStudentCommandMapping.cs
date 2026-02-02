using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping
{
    public partial class StudentProfile
    {
        public void EditStudentCommandMapping()
        {
            CreateMap<EditStudentCommand, Student>()
                .ForMember(dest => dest.DID, options => options.MapFrom(s => s.DepartmentId))
                .ForMember(dest => dest.StudID, options => options.MapFrom(s => s.Id));
        }
    }
}
