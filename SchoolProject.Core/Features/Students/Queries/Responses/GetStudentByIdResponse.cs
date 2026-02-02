using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Core.Features.Students.Queries.Responses
{
    public class GetStudentByIdResponse
    {
        public int StudID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? DepartmentName { get; set; }
    }
}
