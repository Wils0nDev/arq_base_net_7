namespace BaseArchitecture.ExternalServices.Happy.Models
{
    public class ChangeTokenResponse
    {
        public string Message { get; set; }
        public CognitoToken Value { get; set; }
        public string Status { get; set; }
    }
    public class CognitoToken
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string SesionToken { get; set; }
        public string TokenType { get; set; }
        public string IdToken { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
    }

}
