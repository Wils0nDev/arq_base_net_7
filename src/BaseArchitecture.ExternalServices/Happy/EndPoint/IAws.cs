using BaseArchitecture.ExternalServices.AwsS3.Models;
using BaseArchitecture.ExternalServices.Happy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.ExternalServices.Happy.EndPoint
{
    public interface IAws
    {
        Task<ApiGenericResponse<Response<List<S3PresignedResponse>>>> S3Presigned(string code, string header, S3PresignedRequest entity);
    }
}
