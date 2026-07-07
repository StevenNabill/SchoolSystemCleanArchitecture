namespace SchoolProject.Data.Helpers
{
    public class RefreshToken
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime ExpireOn { get; set; }
    }
}
