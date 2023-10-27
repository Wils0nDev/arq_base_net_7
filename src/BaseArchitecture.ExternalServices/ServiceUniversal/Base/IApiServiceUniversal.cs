using BaseArchitecture.ExternalServices.ServiceUniversal.Models;

namespace BaseArchitecture.ExternalServices.ServiceUniversal.Base
{
    public interface IApiServiceUniversal
    {
        Task<ApiGenericResponse<IEnumerable<UniversalEmployeeResponse>>> ListEmployee(UniversalEmployeeRequest parameter);
        Task<ApiGenericResponse<IEnumerable<UniversalCompanyResponse>>> ListCompany(UniversalCompanyRequest parameter);
        Task<ApiGenericResponse<IEnumerable<UniversalPositionResponse>>> ListPosition(UniversalPositionRequest parameter);
        Task<ApiGenericResponse<IEnumerable<UniversalManagementResponse>>> ListManagement(UniversalManagementRequest parameter);
    }
}
