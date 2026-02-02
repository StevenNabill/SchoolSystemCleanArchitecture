using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Interfaces;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler : ResponseHandler,
                                       IRequestHandler<GetStudentsListQuery, Response<List<GetStudentListResponse>>>,
                                       IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdResponse>>,
                                       IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public StudentQueryHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> localizer)
            : base(localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentsListQuery request, CancellationToken cancellationToken)
        {
            var studentList = await _studentService.GetStudentsListAsync();
            var response = _mapper.Map<List<GetStudentListResponse>>(studentList);
            return Success(response, new { Count = response.Count });
        }

        public async Task<Response<GetStudentByIdResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdWithIncludeAsync(request.Id);
            if (student is null)
                return NotFound<GetStudentByIdResponse>(_localizer[SharedResourcesKeys.NotFound]);
            var response = _mapper.Map<GetStudentByIdResponse>(student);
            return Success(response);
        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListResponse>> expression = s => new GetStudentPaginatedListResponse(s.StudID, s.Localize(s.NameAr, s.NameEn), s.Address, s.Department.Localize(s.Department.DNameAr, s.Department.DNameEn));
            var FilteredQuery = _studentService.FilterStudentPaginatedQueryable(request.OrderBy, request.Search);
            var response = await FilteredQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            response.Meta = new { Count = response.TotalCount };
            return response;
        }
        #endregion
    }
}
