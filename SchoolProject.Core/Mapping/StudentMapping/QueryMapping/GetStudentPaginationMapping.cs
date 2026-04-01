using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping
{
    public partial class StudentProfile
    {
        public void GetStudentPaginationMapping()
        {
            CreateMap<Student, GetStudentPaginatedListResponse>()
                .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department.Localize(src.Department.DNameAr, src.Department.DNameEn)))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.StudID))
                .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address));

        }
    }
}
