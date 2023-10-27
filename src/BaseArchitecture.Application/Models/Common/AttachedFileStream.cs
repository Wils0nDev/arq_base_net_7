using Reec.Helpers;

namespace BaseArchitecture.Application.Models.Common
{
    public class AttachedFileStream
    {
        /// <summary>
        /// Nombre del archivo adjunto, debe incluir la extensión. Ejemplo: prueba.txt
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Contenido el archivo expresado en Stream.
        /// </summary>
        public MemoryStream FileStream { get; set; }

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
