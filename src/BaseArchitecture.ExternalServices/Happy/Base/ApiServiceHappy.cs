using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace BaseArchitecture.ExternalServices.Happy.Base
{
    public class ApiServiceHappy : IApiServiceHappy
    {

        private readonly HttpClient _httpClient;
        private readonly ServiceHappyOptions _serviceHappyOptions;

        /// <summary>
        /// Inicializa una sola vez el HttpClient.
        /// </summary>
        /// <param name="httpClient"></param>
        public ApiServiceHappy(HttpClient httpClient, ServiceHappyOptions serviceHappyOptions)
        {
            this._httpClient = httpClient;
            this._serviceHappyOptions = serviceHappyOptions;
        }


        public async Task<ApiGenericResponse<TypeObject>> GetAsync<TypeObject>(string code, string header, string url, object entity = null) where TypeObject : class
        {
            ApiGenericResponse<TypeObject> apiResponse = new ApiGenericResponse<TypeObject>();

            using HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), url);
            request.Headers.Add("Code", code);
            request.Headers.Add("Header", header);

            if(entity != null )
            {
                var json = JsonConvert.SerializeObject(entity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = content;
            }           

            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Result = JsonConvert.DeserializeObject<TypeObject>(jsonResponse);
            }
            else
                apiResponse.Message = response.ToString();

            return apiResponse;

        }

        public async Task<ApiGenericResponse<TypeObject>> PostAsync<TypeObject>(string code, string header, string url, object entity) where TypeObject : class
        {
            ApiGenericResponse<TypeObject> apiResponse = new ApiGenericResponse<TypeObject>();

            var json = JsonConvert.SerializeObject(entity);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), url) { Content = content };
            request.Headers.Add("Code", code);
            request.Headers.Add("Header", header);

            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Result = JsonConvert.DeserializeObject<TypeObject>(jsonResponse);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                apiResponse.Message = "No autorizado";
            }
            else
                apiResponse.Message = response.ToString();

            return apiResponse;

        }


        public async Task<ApiGenericResponse<TypeObject>> PostAnonymousAsync<TypeObject>(string url, object entity)
        {
            ApiGenericResponse<TypeObject> apiResponse = new ApiGenericResponse<TypeObject>();

            var json = JsonConvert.SerializeObject(entity);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), url) { Content = content };

            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Result = JsonConvert.DeserializeObject<TypeObject>(jsonResponse);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                apiResponse.Message = "No autorizado";
            }
            else
                apiResponse.Message = response.ToString();

            return apiResponse;
        }

        public async Task<ApiGenericResponse<TypeObject>> GetAnonymousAsync<TypeObject>(string url, string entity) where TypeObject : class
        {
            ApiGenericResponse<TypeObject> apiResponse = new ApiGenericResponse<TypeObject>();

            using HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), url);
            request.Headers.Add("Authorization", entity);

            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Result = JsonConvert.DeserializeObject<TypeObject>(jsonResponse);
            }
            else
                apiResponse.Message = response.ToString();

            return apiResponse;

        }

        public async Task<ApiGenericResponse<TypeObject>> PostChangeTokenAsync<TypeObject>() where TypeObject : class
        {
            ApiGenericResponse<TypeObject> apiResponse = new ApiGenericResponse<TypeObject>();

            var body = new Dictionary<string, string>()
            {
                {"Username", _serviceHappyOptions.AwsUsername},
                {"Password", _serviceHappyOptions.AwsPassword},
                {"PoolId", _serviceHappyOptions.AwsPoolId},
                {"IdentityPool", _serviceHappyOptions.AwsIdentityPool},
                {"ClientId", _serviceHappyOptions.AwsClientId},
            };

            var json = JsonConvert.SerializeObject(body);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), _serviceHappyOptions.Token_Create)
            { Content = content };

            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Result = JsonConvert.DeserializeObject<TypeObject>(jsonResponse);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                apiResponse.Message = "No autorizado";
            }
            else
                apiResponse.Message = response.ToString();

            return apiResponse;

        }

    }
}