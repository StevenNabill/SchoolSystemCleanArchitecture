namespace SchoolProject.Data.Helpers
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public int AccessTokenExpirationInDays { get; set; }
        public int RefreshTokenExpirationInDays { get; set; }
    }
}
