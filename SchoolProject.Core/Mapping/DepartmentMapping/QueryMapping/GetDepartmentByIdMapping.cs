using SchoolProject.Core.Features.Departments.Queries.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIdResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.DepartmentManager.Localize(src.DepartmentManager.NameAr, src.DepartmentManager.NameEn)))
                .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.DepartmentSubjects))
                .ForMember(dest => dest.Instructors, opt => opt.MapFrom(src => src.Instructors))
                .ForMember(dest => dest.Students, opt => opt.Ignore());


            CreateMap<DepartmentSubject, SubjectResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Subject.SubID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.Localize(src.Subject.SubjectNameAr, src.Subject.SubjectNameEn)));

            CreateMap<Instructor, InstructorResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));

        }
    }
}
