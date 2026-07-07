namespace SchoolProject.Data.Helpers
{
    public class JwtAuthResponse
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
