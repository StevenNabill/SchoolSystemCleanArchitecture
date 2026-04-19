using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        #endregion
        public UserCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            UserManager<User> userManager)
            : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var isExistEmail = await _userManager.FindByEmailAsync(request.Email) is not null;

            if (isExistEmail)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailAlreadyExists]);

            var isExistUserName = await _userManager.FindByNameAsync(request.UserName) is not null;
            if (isExistUserName)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameAlreadyExist]);

            var newUser = _mapper.Map<User>(request);

            var createUserResult = await _userManager.CreateAsync(newUser, request.Password);

            if (!createUserResult.Succeeded)
                return BadRequest<string>(createUserResult.Errors.FirstOrDefault().Description);

            return Created<string>(_stringLocalizer[SharedResourcesKeys.Created]);
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (oldUser is null)
                return NotFound<string>();

            var newUser = _mapper.Map(request, oldUser);

            var result = await _userManager.UpdateAsync(newUser);

            if (!result.Succeeded)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);


        }
    }
}
