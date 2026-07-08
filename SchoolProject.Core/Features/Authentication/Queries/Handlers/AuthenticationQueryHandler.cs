using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Interfaces;

namespace SchoolProject.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler :
        ResponseHandler,
        IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;
        #endregion


        #region Ctor

        public AuthenticationQueryHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IAuthenticationService authenticationService)
            : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
        }
        #endregion

        #region handlers
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.ValidateToken(request.AccessToken);

            if (response == "NotExpired")
                return Success(response);

            return Unauthorized<string>(_stringLocalizer[SharedResourcesKeys.TokenIsExpired]);
        }
        #endregion
    }
}
