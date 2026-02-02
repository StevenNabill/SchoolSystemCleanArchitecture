using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Core.Features.Students.Queries.Models
{
    public class GetStudentByIdQuery : IRequest<Response<GetStudentByIdResponse>>
    {
        public int Id { get; set; }

        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }

    }
}
