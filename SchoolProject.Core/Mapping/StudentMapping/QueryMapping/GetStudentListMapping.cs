using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping
{
    public partial class StudentProfile
    {
        public void GetStudentListMapping()
        {
            CreateMap<Student, GetStudentListResponse>()
                .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department.Localize(src.Department.DNameAr, src.Department.DNameEn)))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));

        }
    }
}
