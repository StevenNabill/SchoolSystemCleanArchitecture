using MediatR;
using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Models
{
    public class GetUserPaginationQuery : IRequest<PaginatedResult<GetUserPaginationResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
