using Newtonsoft.Json;

namespace BaseArchitecture.Api.Model
{
    public class UserModel
    {
        [JsonProperty("Token")]
        public string Token { get; set; }

        [JsonProperty("User")]
        public string User { get; set; }

        [JsonProperty("UserEdit")]
        public long UserEdit { get; set; }

        [JsonProperty("AwsSessionToken")]
        public string AwsSessionToken { get; set; }

        [JsonProperty("EmployeeId")]
        public string EmployeeId { get; set; }

        [JsonProperty("AwsAccessKey")]
        public string AwsAccessKey { get; set; }

        [JsonProperty("AwsSecretKey")]
        public string AwsSecretKey { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("TimeExpiration")]
        public long TimeExpiration { get; set; }

        [JsonProperty("ProfileId")]
        public string ProfileId { get; set; }

        [JsonProperty("VicepresidencyId")]
        public string VicepresidencyId { get; set; }

        [JsonProperty("ManagementId")]
        public string ManagementId { get; set; }

        [JsonProperty("TypeRisk")]
        public string TypeRisk { get; set; }

        [JsonProperty("AccessDevice")]
        public string AccessDevice { get; set; }

        [JsonProperty("Duration")]
        public int? Duration { get; set; }

    }

}
