using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPasswordCommand, Response<string>>
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
            var isExistUserName = await _userManager.Users.AnyAsync(u => u.UserName == newUser.UserName && u.Id != newUser.Id);
            if (isExistUserName)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameAlreadyExist]);

            var result = await _userManager.UpdateAsync(newUser);

            if (!result.Succeeded)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);


        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user is null)
                return NotFound<string>();

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DeleteFailed]);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user is null)
                return NotFound<string>();

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ChangePasswordFailed]);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }
    }
}
