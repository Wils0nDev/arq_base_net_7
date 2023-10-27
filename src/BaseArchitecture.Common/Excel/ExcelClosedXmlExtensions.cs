using ClosedXML.Excel;

namespace BaseArchitecture.Common.Excel
{
    public static class ExcelClosedXmlExtensions
    {
        /// <summary>
        /// Retorna un Byte[] que representa un archivo Excel.
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="isDisposable">Libera todos los recursos del excel.</param>
        /// <returns></returns>
        public static byte[] SaveToBytes(this XLWorkbook workbook, bool isDisposable = true)
        {
            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            memoryStream.Position = 0;
            var bytes = memoryStream.ToArray();
            if (isDisposable)
                workbook.Dispose();
            return bytes;
        }

        /// <summary>
        /// Retorna un Stream que representa un archivo Excel.
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="isDisposable">Libera todos los recursos del excel.</param>
        /// <returns></returns>
        public static MemoryStream SaveToStream(this XLWorkbook workbook, bool isDisposable = true)
        {
            var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            memoryStream.Position = 0;
            if (isDisposable)
                workbook.Dispose();
            return memoryStream;
        }
         
        /// <summary>
        /// Retorna un string en Base64 que representa un archivo Excel.
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="isDisposable"></param>
        /// <returns></returns>
        public static string SaveToBase64String(this XLWorkbook workbook, bool isDisposable = true)
        {
            var bytes = workbook.SaveToBytes(isDisposable);
            return Convert.ToBase64String(bytes);
        }


    }

}
