
using BaseArchitecture.ExternalServices.AwsS3.Models;

namespace BaseArchitecture.ExternalServices.AwsS3.Base
{
    public interface IApiAwsS3
    {
        FileResponse UploadFileS3(FileModel fileModel, string awsAccessKey, string awsSecretKey, string awsSessionToken);
        Task<bool> DeleteFileS3(string pathFile, string name, string awsAccessKey, string awsSecretKey, string awsSessionToken);
        string DownloadFileS3(string pathFile, string awsAccessKey, string awsSecretKey, string awsSessionToken);
        void DownloadFilePath(FileResponse fileResponse, BaseRequest baseRequest);
    }
}
