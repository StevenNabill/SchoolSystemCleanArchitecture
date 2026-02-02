namespace SchoolProject.Data.AppMetaData
{
    public static class Router
    {
        public const string SingleRoute = "{id}";
        public const string Root = "Api";
        public const string Version = "V1";
        public const string Rule = Root + "/" + Version + "/";

        public static class StudentRouting
        {
            public const string Prefix = Rule + "students/";
            public const string GetStudentsList = Prefix + "List";
            public const string GetStudentById = Prefix + SingleRoute;
            public const string CreateStudentCommand = Prefix + "Create";
            public const string EditStudentCommand = Prefix + "Edit";
            public const string DeleteStudentCommand = Prefix + "Delete" + SingleRoute;
            public const string PaginatedList = Prefix + "Paginated";
        }
        public static class DepartmentRouting
        {
            public const string Prefix = Rule + "departments/";
            public const string GetDepartmentsList = Prefix + "List";
            public const string GetDepartmentById = Prefix + "Id";
            public const string CreateDepartmentCommand = Prefix + "Create";
            public const string EditDepartmentCommand = Prefix + "Edit";
            public const string DeleteDepartmentCommand = Prefix + "Delete" + SingleRoute;
        }
    }
}
