using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Departments.Queries.Responses;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Interfaces;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Departments.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler,
                                          IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        #endregion


        #region Ctor
        public DepartmentQueryHandler(IStringLocalizer<SharedResources> localizer,
                                      IDepartmentService departmentService,
                                      IMapper mapper,
                                      IStudentService studentService)
            : base(localizer)
        {
            _localizer = localizer;
            _departmentService = departmentService;
            _mapper = mapper;
            _studentService = studentService;
        }
        #endregion

        #region Functions
        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetDepartmentByIdWithIncludeAsync(request.Id);

            if (department is null)
                return NotFound<GetDepartmentByIdResponse>();

            var response = _mapper.Map<GetDepartmentByIdResponse>(department);

            Expression<Func<Student, StudentResponse>> e = s => new StudentResponse(s.StudID, s.Localize(s.NameAr, s.NameEn));
            var PaginatedStudents = await _studentService.GetStudentsByDepartmentId(request.Id)
                                        .Select(e)
                                        .ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            response.Students = PaginatedStudents;
            return Success(response);
        }
        #endregion
    }
}
