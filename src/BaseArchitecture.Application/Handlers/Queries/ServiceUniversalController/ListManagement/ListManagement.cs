using BaseArchitecture.ExternalServices;
using BaseArchitecture.ExternalServices.ServiceUniversal.EndPoint;
using BaseArchitecture.ExternalServices.ServiceUniversal.Models;
using MediatR;

namespace BaseArchitecture.Application.Handlers.Queries.ServiceUniversal.ListManagement
{
    public class ListManagement : IRequest<ApiGenericResponse<IEnumerable<UniversalManagementResponse>>>
    {
        public UniversalManagementRequest Request { get; }

        public ListManagement(UniversalManagementRequest request)
        {
            Request = request;
        }

        public class ListManagementHandler : IRequestHandler<ListManagement, ApiGenericResponse<IEnumerable<UniversalManagementResponse>>>
        {
            private readonly IServiceUniversal _serviceUniversal;
            public ListManagementHandler(IServiceUniversal serviceUniversal)
            {
                _serviceUniversal = serviceUniversal;
            }

            public async Task<ApiGenericResponse<IEnumerable<UniversalManagementResponse>>> Handle(ListManagement request, CancellationToken cancellationToken)
            {
                var r = await _serviceUniversal.ListManagement(request.Request);
                return r;
            }
        }
    }
}
