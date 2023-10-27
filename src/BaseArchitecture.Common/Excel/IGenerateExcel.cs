using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Data;
using System.Drawing;

namespace BaseArchitecture.Common.Excel
{
    public interface IGenerateExcel
    {
        /// <summary>
        /// Creación de archivo Excel enviando un DataSet.
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="PrintHeaders"></param>
        /// <param name="tableStyles"></param>
        /// <returns></returns>
        ExcelPackage AsExcelPackage(DataSet dataSet, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null, string[] titleColumn = null);

        /// <summary>
        /// Creación de archivo Excel enviando un DataTable.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="PrintHeaders"></param>
        /// <param name="tableStyles"></param>
        /// <returns></returns>
        ExcelPackage AsExcelPackage(DataTable dataTable, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null);

        ExcelPackage AsExcelPackage(DataTable[] dataTable, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null);

        /// <summary>
        /// Creación de archivo Excel enviando una lista de Objetos.
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="listData"></param>
        /// <param name="PrintHeaders"></param>
        /// <param name="tableStyles"></param>
        /// <returns></returns>
        ExcelPackage AsExcelPackage<TObject>(IEnumerable<TObject> listData, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null, string[] titleColumn = null);

        /// <summary>
        /// Retorna un Byte[] que representa un archivo Excel enviando un DataSet como parametro.
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="PrintHeaders"></param>
        /// <param name="tableStyles"></param>
        /// <returns></returns>
        byte[] SaveToBytes(DataSet dataSet, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null, string[] titleColumn = null);

        /// <summary>
        /// Retorna un Byte[] que representa un archivo Excel enviando un DataTable como parametro.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="PrintHeaders"></param>
        /// <param name="tableStyles"></param>
        /// <returns></returns>
        byte[] SaveToBytes(DataTable dataTable, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null);

        byte[] SaveToBytes(DataTable[] dataTable, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null);

        /// <summary>
        /// Retorna un Byte[] que representa un archivo Excel enviando una lista de objetos como parametro.
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="listData"></param>
        /// <param name="PrintHeaders"></param>
        /// <param name="tableStyles"></param>
        /// <returns></returns>
        byte[] SaveToBytes<TObject>(IEnumerable<TObject> listData, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null, string[] titleColumn = null);

        /// <summary>
        /// Retorna un Stream que representa un archivo Excel enviando un DataSet como parametro.
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="PrintHeaders"></param>
        /// <param name="tableStyles"></param>
        /// <returns></returns>
        MemoryStream SaveToStream(DataSet dataSet, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None);

        /// <summary>
        /// Retorna un Stream que representa un archivo Excel enviando un DataTable como parametro.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="PrintHeaders"></param>
        /// <param name="tableStyles"></param>
        /// <returns></returns>
        MemoryStream SaveToStream(DataTable dataTable, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None);

        /// <summary>
        /// Retorna un Stream que representa un archivo Excel enviando una lista de objetos como parametro.
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="listData"></param>
        /// <param name="PrintHeaders"></param>
        /// <param name="tableStyles"></param>
        /// <returns></returns>
        MemoryStream SaveToStream<TObject>(IEnumerable<TObject> listData, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None);

        DataTable ConvertToDataTable<T>(IList<T> data);
    }
}