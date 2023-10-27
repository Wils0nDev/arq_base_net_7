using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.ComponentModel;
using System.Data;

namespace BaseArchitecture.Common.Excel
{
    public class ExcelClosedXml : IExcelClosedXml
    {
        private readonly string formatDate = "yyyy-MM-dd hh:mm:ss AM/PM";

        public XLWorkbook AsXLWorkbook(DataTable dataTable, string sheetName = null)
        {
            if (dataTable == null)
                throw new ArgumentNullException(nameof(dataTable), $"El parametro {nameof(dataTable)} no debe ser nulo.");
            else if (dataTable.Rows.Count == 0)
                throw new ArgumentException($"El parametro {nameof(dataTable)} debe tener datos.", nameof(dataTable));


            if (!string.IsNullOrWhiteSpace(sheetName))
                dataTable.TableName = sheetName;
            if (string.IsNullOrWhiteSpace(dataTable.TableName))
                dataTable.TableName = "Hoja1";

            var wb = new XLWorkbook();

            // Add a DataTable as a worksheet
            var workSheet = wb.Worksheets.Add(dataTable);

            int colNumber = 1;
            foreach (DataColumn col in dataTable.Columns)
            {
                if (col.DataType == typeof(DateTime) || col.DataType == typeof(DateTime?))
                    workSheet.Column(colNumber).Style.NumberFormat.Format = formatDate;
                colNumber++;
            }

            return wb;
        }
        public XLWorkbook AsXLWorkbook(DataSet dataSet)
        {
            if (dataSet == null)
                throw new ArgumentNullException(nameof(dataSet), $"El parametro {nameof(dataSet)} no debe ser nulo.");
            else if (dataSet != null && dataSet.Tables.Count == 0)
                throw new ArgumentException($"El parametro {nameof(dataSet)} debe tener tablas asociadas.", nameof(dataSet));

            var wb = new XLWorkbook();
            var count = 1;
            foreach (DataTable table in dataSet.Tables)
            {
                if (string.IsNullOrWhiteSpace(table.TableName))
                    table.TableName = $"Hoja{count}";

                // Add a DataTable as a worksheet
                var workSheet = wb.Worksheets.Add(table);
                //var workSheet = wb.Worksheets.Worksheet(dataTable.TableName);

                int colNumber = 1;
                foreach (DataColumn col in table.Columns)
                {
                    if (col.DataType == typeof(DateTime) || col.DataType == typeof(DateTime?))
                        workSheet.Column(colNumber).Style.NumberFormat.Format = formatDate;
                    colNumber++;
                }
                count++;
            }

            return wb;
        }
        public XLWorkbook AsXLWorkbook<TObject>(IEnumerable<TObject> list, string sheetName = null)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list), $"El parametro {nameof(list)} no debe ser nulo.");
            else if (!list.Any())
                throw new ArgumentException($"El parametro {nameof(list)} debe tener datos.", nameof(list));

            if (string.IsNullOrWhiteSpace(sheetName))
                sheetName = "Hoja1";

            var wb = new XLWorkbook();
            var workSheet = wb.AddWorksheet(sheetName);
            workSheet.Cell("A1").InsertTable(list, true);

            int colNumber = 1;
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(TObject));
            foreach (PropertyDescriptor prop in properties)
            {
                if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                    workSheet.Column(colNumber).Style.NumberFormat.Format = formatDate;
                colNumber++;
            }

            return wb;
        }


        public byte[] SaveToBytes(DataSet dataSet)
        {
            var workbook = this.AsXLWorkbook(dataSet);
            return workbook.SaveToBytes();
        }
        public byte[] SaveToBytes(DataTable dataTable, string sheetName = null)
        {
            var workbook = this.AsXLWorkbook(dataTable, sheetName);
            return workbook.SaveToBytes();
        }
        public byte[] SaveToBytes<TObject>(IEnumerable<TObject> list, string sheetName = null)
        {
            var workbook = this.AsXLWorkbook(list, sheetName);
            return workbook.SaveToBytes();
        }

        public MemoryStream SaveToStream(DataSet dataSet)
        {
            var workbook = this.AsXLWorkbook(dataSet);
            return workbook.SaveToStream();
        }
        public MemoryStream SaveToStream(DataTable dataTable, string sheetName = null)
        {
            var workbook = this.AsXLWorkbook(dataTable, sheetName);
            return workbook.SaveToStream();
        }
        public MemoryStream SaveToStream<TObject>(IEnumerable<TObject> list, string sheetName = null)
        {
            var workbook = this.AsXLWorkbook(list, sheetName);
            return workbook.SaveToStream();
        }


    }
}
