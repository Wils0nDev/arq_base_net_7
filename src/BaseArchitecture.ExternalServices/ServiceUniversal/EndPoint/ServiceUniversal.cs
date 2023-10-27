using BaseArchitecture.ExternalServices.ServiceUniversal.Base;
using BaseArchitecture.ExternalServices.ServiceUniversal.Models;

namespace BaseArchitecture.ExternalServices.ServiceUniversal.EndPoint
{
    public class ServiceUniversal : IServiceUniversal
    {
        private readonly IApiServiceUniversal _apiServiceUniversal;
        public ServiceUniversal(IApiServiceUniversal apiServiceUniversal)
        {
            this._apiServiceUniversal = apiServiceUniversal;
        }
        public Task<ApiGenericResponse<IEnumerable<UniversalEmployeeResponse>>> ListEmployee(UniversalEmployeeRequest parameter)
        {
            return _apiServiceUniversal.ListEmployee(parameter);
        }
        public Task<ApiGenericResponse<IEnumerable<UniversalCompanyResponse>>> ListCompany(UniversalCompanyRequest parameter)
        {
            return _apiServiceUniversal.ListCompany(parameter);
        }
        public Task<ApiGenericResponse<IEnumerable<UniversalPositionResponse>>> ListPosition(UniversalPositionRequest parameter)
        {
            return _apiServiceUniversal.ListPosition(parameter);
        }
        public Task<ApiGenericResponse<IEnumerable<UniversalManagementResponse>>> ListManagement(UniversalManagementRequest parameter)
        {
            return _apiServiceUniversal.ListManagement(parameter);
        }
    }
}
