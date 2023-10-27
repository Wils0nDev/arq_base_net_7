using Reec.Helpers;

namespace BaseArchitecture.Application.Models.Common
{
    public class AttachedFileBase64
    {
        /// <summary>
        /// Nombre del archivo adjunto, debe incluir la extensión. Ejemplo: prueba.txt
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Contenido el archivo expresado en formato Base64String.
        /// </summary>
        public string FileBase64String { get; set; }

        public string ContentType
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(FileName))
                    return HelperContentType.GetContentType(this.FileName);
                return null;
            }
        }

    }
}