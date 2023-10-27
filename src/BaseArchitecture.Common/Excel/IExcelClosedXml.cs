using ClosedXML.Excel;
using System.Data;

namespace BaseArchitecture.Common.Excel
{
    public interface IExcelClosedXml
    {

        /// <summary>
        /// Creación de archivo Excel enviando un DataTable.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="sheetName">Nombre de la hoja de excel.</param>
        /// <returns></returns>
        XLWorkbook AsXLWorkbook(DataTable dataTable, string sheetName = null);

        /// <summary>
        /// Creación de archivo Excel enviando un DataSet.
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        XLWorkbook AsXLWorkbook(DataSet dataSet);

        /// <summary>
        /// Creación de archivo Excel enviando una lista generica.
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="list"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        XLWorkbook AsXLWorkbook<TObject>(IEnumerable<TObject> list, string sheetName = null);

        /// <summary>
        /// Retorna un Byte[] que representa un archivo Excel enviando un DataSet como parametro.
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        byte[] SaveToBytes(DataSet dataSet);

        /// <summary>
        /// Retorna un Byte[] que representa un archivo Excel enviando un DataTable como parametro.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="sheetName">Nombre de la hoja de excel.</param>
        /// <returns></returns>
        byte[] SaveToBytes(DataTable dataTable, string sheetName = null);

        /// <summary>
        /// Retorna un Byte[] que representa un archivo Excel enviando una lista genérica como parametro.
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="list">Lista de datos</param>
        /// <param name="sheetName">Nombre de la hoja de excel.</param>
        /// <returns></returns>
        byte[] SaveToBytes<TObject>(IEnumerable<TObject> list, string sheetName = null);

        /// <summary>
        /// Retorna un Stream que representa un archivo Excel enviando un DataSet como parametro.
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        MemoryStream SaveToStream(DataSet dataSet);

        /// <summary>
        /// Retorna un Stream que representa un archivo Excel enviando un DataTable como parametro.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="sheetName">Nombre de la hoja de excel.</param>
        /// <returns></returns>
        MemoryStream SaveToStream(DataTable dataTable, string sheetName = null);

        /// <summary>
        /// Retorna un Stream que representa un archivo Excel enviando una lista genérica como parametro.
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="list"></param>
        /// <param name="sheetName">Nombre de la hoja de excel.</param>
        /// <returns></returns>
        MemoryStream SaveToStream<TObject>(IEnumerable<TObject> list, string sheetName = null);


    }

}
