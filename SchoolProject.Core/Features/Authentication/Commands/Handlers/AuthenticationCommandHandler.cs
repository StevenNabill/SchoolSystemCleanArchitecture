using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Interfaces;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler :
        ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResponse>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthResponse>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        #endregion


        #region Ctor

        public AuthenticationCommandHandler(
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAuthenticationService authenticationService)
            : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }
        #endregion


        #region Methods
        public async Task<Response<JwtAuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return NotFound<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.UserNameIsNotExist]);

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signInResult.Succeeded)
                return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.SignInFailed]);

            var response = await _authenticationService.GetJwtToken(user);

            return Success(response);
        }

        public async Task<Response<JwtAuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = _authenticationService.ReadJwtToken(request.AccessToken);
            var UserIdAndExpDate = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
            switch (UserIdAndExpDate)
            {
                case ("InvalidAlgorithm", null):
                    return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.InvalidAlgorithm]);
                case ("TokenIsNotExpired", null):
                    return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.TokenIsNotExpired]);
                case ("InvalidRefreshToken", null):
                    return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.InvalidRefreshToken]);
                case ("RefreshTokenIsExpired", null):
                    return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsExpired]);
                default:
                    break;
            }
            var (userId, expDate) = UserIdAndExpDate;
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return NotFound<JwtAuthResponse>();

            var response = await _authenticationService.RefreshToken(user, request.RefreshToken, (DateTime)expDate!);
            return Success(response);
        }
        #endregion
    }
}
