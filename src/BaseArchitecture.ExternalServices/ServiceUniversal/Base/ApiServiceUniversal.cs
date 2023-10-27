using BaseArchitecture.ExternalServices.ServiceUniversal.Models;
using Newtonsoft.Json;

namespace BaseArchitecture.ExternalServices.ServiceUniversal.Base
{
    public class ApiServiceUniversal : IApiServiceUniversal
    {
        private readonly ServiceUniversalOptions _serviceUniversalOptions;
        private readonly HttpClient _httpClient;
        public ApiServiceUniversal(ServiceUniversalOptions serviceUniversalOptions, HttpClient httpClient)
        {
            this._serviceUniversalOptions = serviceUniversalOptions;
            this._httpClient = httpClient;
        }
        public async Task<ApiGenericResponse<IEnumerable<UniversalEmployeeResponse>>> ListEmployee(UniversalEmployeeRequest parameter)
        {
            ApiGenericResponse<IEnumerable<UniversalEmployeeResponse>> apiResponse = new();

            using HttpRequestMessage request = new(new HttpMethod("GET"), $"{_serviceUniversalOptions.UriListEmployee}userId:{parameter.UserId},permission:{parameter.Permission},filter:{parameter.Filter ?? ""},location:{parameter.Location}");
            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Result = JsonConvert.DeserializeObject<IEnumerable<UniversalEmployeeResponse>>(jsonResponse);
            }
            else
                apiResponse.Message = response.ToString();

            return apiResponse;
        }
        public async Task<ApiGenericResponse<IEnumerable<UniversalCompanyResponse>>> ListCompany(UniversalCompanyRequest parameter)
        {
            ApiGenericResponse<IEnumerable<UniversalCompanyResponse>> apiResponse = new();

            using HttpRequestMessage request = new(new HttpMethod("GET"), $"{_serviceUniversalOptions.UriListCompany}state:{parameter.State}");
            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Result = JsonConvert.DeserializeObject<IEnumerable<UniversalCompanyResponse>>(jsonResponse);
            }
            else
                apiResponse.Message = response.ToString();

            return apiResponse;
        }
        public async Task<ApiGenericResponse<IEnumerable<UniversalPositionResponse>>> ListPosition(UniversalPositionRequest parameter)
        {
            ApiGenericResponse<IEnumerable<UniversalPositionResponse>> apiResponse = new();

            using HttpRequestMessage request = new(new HttpMethod("GET"), $"{_serviceUniversalOptions.UriListPosition}");
            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Result = JsonConvert.DeserializeObject<IEnumerable<UniversalPositionResponse>>(jsonResponse);
            }
            else
                apiResponse.Message = response.ToString();

            return apiResponse;
        }
        public async Task<ApiGenericResponse<IEnumerable<UniversalManagementResponse>>> ListManagement(UniversalManagementRequest parameter)
        {
            ApiGenericResponse<IEnumerable<UniversalManagementResponse>> apiResponse = new();

            using HttpRequestMessage request = new(new HttpMethod("GET"), $"{_serviceUniversalOptions.UriListManagement}");
            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Result = JsonConvert.DeserializeObject<IEnumerable<UniversalManagementResponse>>(jsonResponse);
            }
            else
                apiResponse.Message = response.ToString();

            return apiResponse;
        }
    }
}
