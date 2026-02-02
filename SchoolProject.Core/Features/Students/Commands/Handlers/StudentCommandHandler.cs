using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Interfaces;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler,
                                         IRequestHandler<CreateStudentCommand, Response<string>>,
                                         IRequestHandler<EditStudentCommand, Response<string>>,
                                         IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> localizer)
            : base(localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion
        public async Task<Response<string>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);
            var result = await _studentService.AddAsync(student);
            if (result is "Success")
                return Created<string>("");
            else
                return BadRequest<string>();

        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetByIdAsync(request.Id);
            if (student is null)
                return NotFound<string>();

            var studentMap = _mapper.Map(request, student);
            var response = await _studentService.EditAsync(studentMap);
            if (response is "Success")
                return Success<string>(_localizer[SharedResourcesKeys.Updated]);
            else
                return BadRequest<string>();

        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetByIdAsync(request.Id);
            if (student is null)
                return NotFound<string>();

            var response = await _studentService.DeleteAsync(student);
            if (response is "Success")
                return Deleted<string>();
            else
                return BadRequest<string>();
        }
    }
}
