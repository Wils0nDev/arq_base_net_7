using BaseArchitecture.ExternalServices;
using BaseArchitecture.ExternalServices.ServiceUniversal.EndPoint;
using BaseArchitecture.ExternalServices.ServiceUniversal.Models;
using MediatR;

namespace BaseArchitecture.Application.Handlers.Queries.ServiceUniversal.ListCompany
{
    public class ListCompany : IRequest<ApiGenericResponse<IEnumerable<UniversalCompanyResponse>>>
    {
        public UniversalCompanyRequest Request { get; }

        public ListCompany(UniversalCompanyRequest request)
        {
            Request = request;
        }

        public class ListCompanyHandler : IRequestHandler<ListCompany, ApiGenericResponse<IEnumerable<UniversalCompanyResponse>>>
        {
            private readonly IServiceUniversal _serviceUniversal;
            public ListCompanyHandler(IServiceUniversal serviceUniversal)
            {
                _serviceUniversal = serviceUniversal;
            }

            public async Task<ApiGenericResponse<IEnumerable<UniversalCompanyResponse>>> Handle(ListCompany request, CancellationToken cancellationToken)
            {
                var r = await _serviceUniversal.ListCompany(request.Request);
                return r;
            }
        }
    }
}
