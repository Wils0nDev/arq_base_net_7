using System.Collections.Generic;

namespace BaseArchitecture.ExternalServices.AwsS3.Models
{
    public class FileResponse
    {
        public FileResponse(string namePhysicalFile, string pathFile)
        {
            Status = true;
            NamePhysicalFile = namePhysicalFile;
            PathFile = pathFile;
        }

        public FileResponse(string pathFile)
        {
            Status = true;
            PathFile = pathFile;
        }

        public FileResponse()
        {
            Status = false;
        }

        public Dictionary<string, string> CognitionAccess { get; set; }

        public string NamePhysicalFile { get; set; }

        public bool Status { get; set; }

        public string PathFile { get; set; }
    }
}