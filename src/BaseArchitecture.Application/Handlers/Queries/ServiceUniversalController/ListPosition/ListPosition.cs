using BaseArchitecture.ExternalServices;
using BaseArchitecture.ExternalServices.ServiceUniversal.EndPoint;
using BaseArchitecture.ExternalServices.ServiceUniversal.Models;
using MediatR;

namespace BaseArchitecture.Application.Handlers.Queries.ServiceUniversal.ListPosition
{
    public class ListPosition : IRequest<ApiGenericResponse<IEnumerable<UniversalPositionResponse>>>
    {
        public UniversalPositionRequest Request { get; }

        public ListPosition(UniversalPositionRequest request)
        {
            Request = request;
        }

        public class ListPositionHandler : IRequestHandler<ListPosition, ApiGenericResponse<IEnumerable<UniversalPositionResponse>>>
        {
            private readonly IServiceUniversal _serviceUniversal;
            public ListPositionHandler(IServiceUniversal serviceUniversal)
            {
                _serviceUniversal = serviceUniversal;
            }

            public async Task<ApiGenericResponse<IEnumerable<UniversalPositionResponse>>> Handle(ListPosition request, CancellationToken cancellationToken)
            {
                var r = await _serviceUniversal.ListPosition(request.Request);
                return r;
            }
        }
    }
}
