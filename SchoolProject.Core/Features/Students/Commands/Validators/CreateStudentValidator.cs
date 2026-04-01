using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Interfaces;

namespace SchoolProject.Core.Features.Students.Commands.Validators
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IDepartmentService _departmentService;
        #endregion

        #region Constructors
        public CreateStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> localizer, IDepartmentService departmentService)
        {
            _studentService = studentService;
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _departmentService = departmentService;
        }
        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.NameEn)
                .MustAsync(async (key, CancellationToken) => !await _studentService.IsNameExist(key)).WithMessage(_localizer[SharedResourcesKeys.IsAlreadyExist]);

            RuleFor(x => x.NameAr)
                 .MustAsync(async (key, CancellationToken) => !await _studentService.IsNameExist(key)).WithMessage(_localizer[SharedResourcesKeys.IsAlreadyExist]);

            RuleFor(x => x.DepartmentId)
                .MustAsync(async (key, cancellationToken) => await _departmentService.IsDepartmentExist(key))
                .WithMessage(_localizer[SharedResourcesKeys.IsNotExist]);
        }
        #endregion
    }
}
