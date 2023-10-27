using BaseArchitecture.ExternalServices.AwsS3.Models;
using BaseArchitecture.ExternalServices.Happy.Base;
using BaseArchitecture.ExternalServices.Happy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.ExternalServices.Happy.EndPoint
{
    public class Aws : IAws
    {
        private readonly ServiceHappyOptions _serviceHappyOptions;
        private readonly IApiServiceHappy _apiServiceHappy;

        public Aws(ServiceHappyOptions serviceHappyOptions, IApiServiceHappy apiServiceHappy)
        {
            this._serviceHappyOptions = serviceHappyOptions;
            this._apiServiceHappy = apiServiceHappy;
        }

        public async Task<ApiGenericResponse<Response<List<S3PresignedResponse>>>> S3Presigned(string code, string header, S3PresignedRequest entity)
        {
            var url = this._serviceHappyOptions.Aws_S3_Presigned;
            return await _apiServiceHappy.GetAsync<Response<List<S3PresignedResponse>>>(code, header, url, entity);
        }
    }
}
