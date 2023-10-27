using Amazon.CognitoIdentity;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using BaseArchitecture.ExternalServices.AwsS3.Models;
using BaseArchitecture.ExternalServices.Happy.EndPoint;
using Reec.Inspection;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BaseArchitecture.ExternalServices.AwsS3.Base
{
    public class ApiAwsS3 : IApiAwsS3
    {
        private readonly AwsS3AntaminaOptions _awsS3Options;
        private readonly IAws _aws;

        public ApiAwsS3(AwsS3AntaminaOptions awsS3Options, IAws aws) 
        {
            this._awsS3Options = awsS3Options;
            this._aws = aws;
        }

        public async Task<bool> DeleteFileS3(string pathFile, string name, string awsAccessKey, string awsSecretKey, string awsSessionToken)
        {

            var put = new PutObjectRequest();
            var amazonS3Client = new AmazonS3Client();


            new AmazonCognitoIdentityConfig() { 
            
            };


            var amazonS3Config = new AmazonS3Config { ServiceURL = _awsS3Options.AwsRegionEndPointS3 };
            using (var client = new AmazonS3Client(
                awsAccessKey,
                awsSecretKey,
                awsSessionToken,
                amazonS3Config))
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = _awsS3Options.AwsBucketName,
                    Key = $"{pathFile}/{name}"
                };
                await client.DeleteObjectAsync(request);
                return true;
            }
        }

        public string DownloadFileS3(string pathFile, string awsAccessKey, string awsSecretKey, string awsSessionToken)
        {
            var key = $"{pathFile}";
            string urlIng;
            var amazonS3Config = new AmazonS3Config { ServiceURL = _awsS3Options.AwsRegionEndPointS3 };
            using (IAmazonS3 client = new AmazonS3Client(
                awsAccessKey,
                awsSecretKey,
                awsSessionToken,
                amazonS3Config))
            {
                urlIng = client.GeneratePreSignedURL(_awsS3Options.AwsBucketName, key,
                    DateTime.Now.AddDays(_awsS3Options.AwsDayExpire), null);
            }

            return urlIng;
        }

        public FileResponse UploadFileS3(FileModel fileModel, string awsAccessKey, string awsSecretKey, string awsSessionToken)
        {
            if (string.IsNullOrEmpty(fileModel.NameFile)) return new FileResponse();
            var namePhysicalFile = $"{fileModel.NameFile}";
            var amazonS3Config = new AmazonS3Config { ServiceURL = _awsS3Options.AwsRegionEndPointS3 };
            var stream = new MemoryStream(fileModel.Buffer);
            using (var client = new AmazonS3Client(
                awsAccessKey,
                awsSecretKey,
                awsSessionToken,
                amazonS3Config))
            {
                var utility = new TransferUtility(client);
                var request = new TransferUtilityUploadRequest
                {
                    BucketName = $"{_awsS3Options.AwsBucketName}",
                    Key = namePhysicalFile,
                    InputStream = stream
                };
                utility.Upload(request);
                return new FileResponse(namePhysicalFile, request.Key);
            }
        }

        public void DownloadFilePath(FileResponse fileResponse, BaseRequest baseRequest)
        {
            var key = $"{fileResponse.PathFile}";
            var amazonS3Config = new AmazonS3Config { ServiceURL = _awsS3Options.AwsRegionEndPointS3 };
            using (IAmazonS3 client = new AmazonS3Client(GetCredentials(), amazonS3Config))
            {
                var utility = new TransferUtility(client);
                utility.Download(fileResponse.NamePhysicalFile, _awsS3Options.AwsBucketName, key);
            }
        }

        private BasicAWSCredentials GetCredentials()
            => new BasicAWSCredentials(
                _awsS3Options.AwsIamAccessKey,
                _awsS3Options.AwsIamSecretKey);

    }
}
