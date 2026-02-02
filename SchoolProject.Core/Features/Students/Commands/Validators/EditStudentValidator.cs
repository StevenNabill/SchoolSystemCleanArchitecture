using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Interfaces;

namespace SchoolProject.Core.Features.Students.Commands.Validators
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public EditStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> localizer)
        {
            _studentService = studentService;
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
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
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.NameEn)
                .MustAsync(async (model, key, CancellationToken) => !await _studentService.IsNameExistExcludeSelf(key, model.Id)).WithMessage(_localizer[SharedResourcesKeys.IsAlreadyExist]);
        }
        #endregion
    }
}
