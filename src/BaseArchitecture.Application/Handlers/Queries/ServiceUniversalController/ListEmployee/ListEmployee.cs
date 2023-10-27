using BaseArchitecture.ExternalServices;
using BaseArchitecture.ExternalServices.Happy.EndPoint;
using BaseArchitecture.ExternalServices.Happy.Models;
using BaseArchitecture.ExternalServices.ServiceUniversal.EndPoint;
using BaseArchitecture.ExternalServices.ServiceUniversal.Models;
using MediatR;

namespace BaseArchitecture.Application.Handlers.Queries.ServiceUniversal.ListEmployee
{
    public class ListEmployee : IRequest<ApiGenericResponse<IEnumerable<UniversalEmployeeResponse>>>
    {
        public string Code { get; }
        public string Header { get; }

        public ListEmployee(string code, string header)
        {
            Code = code;
            Header = header;
        }

        public class ListEmployeeHandler : IRequestHandler<ListEmployee, ApiGenericResponse<IEnumerable<UniversalEmployeeResponse>>>
        {
            private readonly IServiceUniversal _serviceUniversal;
            private readonly IAuthentication _authentication;
            public ListEmployeeHandler(IServiceUniversal serviceUniversal,
                IAuthentication authentication)
            {
                _serviceUniversal = serviceUniversal;
                _authentication = authentication;
            }

            public async Task<ApiGenericResponse<IEnumerable<UniversalEmployeeResponse>>> Handle(ListEmployee request, CancellationToken cancellationToken)
            {
                var _header_response = await _authentication.GetDeserializeObject(
                            new EncryptRequest { Code = request.Code, TextTransform = request.Header });
                var _base = _authentication.GetObjectTransform(_header_response.Result.Value);

                var _request = new UniversalEmployeeRequest()
                {
                    UserId = _base.EmployeeId,
                    Permission = "ALL;EDITTIMEEVENT;CHILD;ME;",//_base.ProcessAllow
                    Location = string.Empty,
                    Filter = null
                };

                var r = await _serviceUniversal.ListEmployee(_request);
                return r;
            }
        }
    }
}
