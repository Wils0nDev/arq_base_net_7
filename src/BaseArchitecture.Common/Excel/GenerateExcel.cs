using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BaseArchitecture.Common.Excel
{
    public class GenerateExcel : IGenerateExcel
    {
        private readonly string formatDate = "yyyy-MM-dd hh:mm:ss AM/PM";
        public ExcelPackage AsExcelPackage(DataSet dataSet, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null, string[] titleColumn = null)
        {
            if (dataSet == null)
                throw new ArgumentNullException(nameof(dataSet), $"El parametro {nameof(dataSet)} no debe ser nulo.");
            else if (dataSet != null && dataSet.Tables.Count == 0)
                throw new ArgumentException($"El parametro {nameof(dataSet)} debe tener tablas asociadas.", nameof(dataSet));

            var excel = new ExcelPackage();
            ExcelWorksheet ws;
            var count = 1;
            foreach (DataTable table in dataSet.Tables)
            {
                if (!string.IsNullOrWhiteSpace(table.TableName))
                    ws = excel.Workbook.Worksheets.Add(table.TableName);
                else
                    ws = excel.Workbook.Worksheets.Add($"Hoja{count}");

                var initRow = 1;
                var totalColumn = table.Columns.Count;
                if (imageLogo != null || titleReport != null)
                {
                    initRow = 7;
                    setImageTitle(ws, totalColumn, titleReport, imageLogo);
                }
                ws.Cells["A" + initRow.ToString()].LoadFromDataTable(table, PrintHeaders, tableStyles);
                var rangeHeader = ws.SelectedRange[initRow, 1, initRow, totalColumn];
                rangeHeader.AutoFitColumns();

                int colNumber = 1;
                foreach (DataColumn col in table.Columns)
                {
                    if (titleColumn != null && colNumber <= titleColumn.Length)
                    {
                        ws.Cells["A" + initRow].Value = titleColumn[colNumber - 1];
                    }
                    if (col.DataType == typeof(DateTime) || col.DataType == typeof(DateTime?))
                        ws.Column(colNumber).Style.Numberformat.Format = formatDate;
                    colNumber++;
                }
                count++;
            }

            return excel;

        }

        public ExcelPackage AsExcelPackage(DataTable dataTable, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null)
        {
            if (dataTable == null)
                throw new ArgumentNullException(nameof(dataTable), $"El parametro {nameof(dataTable)} no debe ser nulo.");
            else if (dataTable.Rows.Count == 0)
                throw new ArgumentException($"El parametro {nameof(dataTable)} debe tener datos.", nameof(dataTable));

            var excel = new ExcelPackage();
            ExcelWorksheet ws;
            if (!string.IsNullOrWhiteSpace(dataTable.TableName))
                ws = excel.Workbook.Worksheets.Add(dataTable.TableName);
            else
                ws = excel.Workbook.Worksheets.Add("Hoja1");

            var initRow = 1;
            var totalColumn = dataTable.Columns.Count;
            if (imageLogo != null || titleReport != null)
            {
                initRow = 7;
                setImageTitle(ws, totalColumn, titleReport, imageLogo);
            }
            ws.Cells["A" + initRow.ToString()].LoadFromDataTable(dataTable, PrintHeaders, tableStyles);
            var rangeHeader = ws.SelectedRange[initRow, 1, initRow, totalColumn];
            rangeHeader.AutoFitColumns();

            int colNumber = 1;
            foreach (DataColumn col in dataTable.Columns)
            {
                if (col.DataType == typeof(DateTime) || col.DataType == typeof(DateTime?))
                    ws.Column(colNumber).Style.Numberformat.Format = formatDate;
                colNumber++;
            }
            return excel;
        }

        public ExcelPackage AsExcelPackage(DataTable[] dataTable, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null)
        {
            if (dataTable == null)
                throw new ArgumentNullException(nameof(dataTable), $"El parametro {nameof(dataTable)} no debe ser nulo.");
            else if (dataTable[0].Rows.Count == 0)
                throw new ArgumentException($"El parametro {nameof(dataTable)} debe tener datos.", nameof(dataTable));

            var excel = new ExcelPackage();
            ExcelWorksheet ws;
            if (!string.IsNullOrWhiteSpace(dataTable[0].TableName))
                ws = excel.Workbook.Worksheets.Add(dataTable[0].TableName);
            else
                ws = excel.Workbook.Worksheets.Add("Hoja1");

            var initRow = 1;
            var totalColumn = dataTable[0].Columns.Count;
            if (imageLogo != null || titleReport != null)
            {
                initRow = 7;
                setImageTitle(ws, totalColumn, titleReport, imageLogo);
            }

            foreach (var dt in dataTable)
            {
                ws.Cells["A" + initRow.ToString()].LoadFromDataTable(dt, PrintHeaders, tableStyles);
                var rangeHeader = ws.SelectedRange[initRow, 1, initRow, totalColumn];
                rangeHeader.AutoFitColumns();

                int colNumber = 1;
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.DataType == typeof(DateTime) || col.DataType == typeof(DateTime?))
                        ws.Column(colNumber).Style.Numberformat.Format = formatDate;
                    colNumber++;
                }
                initRow = initRow + dt.Rows.Count + 2;
            }
            return excel;
        }

        public ExcelPackage AsExcelPackage<TObject>(IEnumerable<TObject> listData, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null, string[] titleColumn = null)
        {
            if (listData == null)
                throw new ArgumentNullException(nameof(listData), $"El parametro {nameof(listData)} no debe ser nulo.");

            var excel = new ExcelPackage();
            ExcelWorksheet ws = excel.Workbook.Worksheets.Add($"Hoja1");

            var initRow = 1;
            var totalColumn = listData.FirstOrDefault().GetType().GetProperties().Length;
            if (imageLogo != null || titleReport != null)
            {
                initRow = 7;
                setImageTitle(ws, totalColumn, titleReport, imageLogo);
            }

            setData(ws, listData, PrintHeaders, tableStyles, initRow, 1, titleColumn);
            return excel;
        }

        public void setData<TObject>(ExcelWorksheet ws, IEnumerable<TObject> lstData,
            bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            int row = 1, int col = 1, string[] titleColumn = null)
        {
            var colMax = lstData.FirstOrDefault().GetType().GetProperties().Length;
            var range = ws.SelectedRange[row, col, row, col + colMax];
            range.LoadFromCollection(lstData, PrintHeaders, tableStyles);
            range.AutoFitColumns();

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(TObject));
            int colNumber = 1;
            foreach (PropertyDescriptor prop in properties)
            {
                if (titleColumn != null && colNumber <= titleColumn.Length)
                {
                    var rangeTitle = ws.SelectedRange[row, colNumber, row, colNumber];
                    rangeTitle.Value = titleColumn[colNumber - 1];
                }
                if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                    ws.Column(colNumber).Style.Numberformat.Format = formatDate;
                colNumber++;
            }
        }

        public static void setImageTitle(ExcelWorksheet ws, int totalColumn, string titleReport = null, Image imageLogo = null)
        {
            if (titleReport != null)
            {
                var range = ws.SelectedRange[3, 3, 3, totalColumn > 6 ? totalColumn : 6];
                range.Merge = true;
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                range.Style.Font.Bold = true;
                range.Style.Font.Size = 15;
                range.Value = titleReport;
                range.AutoFitColumns();
            }
            if (imageLogo != null)
            {
                imageLogo = resizeImage(imageLogo, 150, 100);
                ws.Drawings.AddPicture("image", imageLogo);
            }
        }

        public byte[] SaveToBytes(DataSet dataSet, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null, string[] titleColumn = null)
        {
            var excel = AsExcelPackage(dataSet, PrintHeaders, tableStyles, imageLogo, titleReport, titleColumn);
            var file = excel.GetAsByteArray();
            excel.Dispose();
            return file;
        }

        public byte[] SaveToBytes(DataTable dataTable, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null)
        {
            var excel = AsExcelPackage(dataTable, PrintHeaders, tableStyles, imageLogo, titleReport);
            var file = excel.GetAsByteArray();
            excel.Dispose();
            return file;
        }

        public byte[] SaveToBytes(DataTable[] dataTable, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null)
        {
            var excel = AsExcelPackage(dataTable, PrintHeaders, tableStyles, imageLogo, titleReport);
            var file = excel.GetAsByteArray();
            excel.Dispose();
            return file;
        }

        public byte[] SaveToBytes<TObject>(IEnumerable<TObject> listData, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None,
            Image imageLogo = null, string titleReport = null, string[] titleColumn = null)
        {
            var excel = AsExcelPackage(listData, PrintHeaders, tableStyles, imageLogo, titleReport, titleColumn);
            var file = excel.GetAsByteArray();
            excel.Dispose();
            return file;
        }

        public MemoryStream SaveToStream(DataSet dataSet, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None)
        {
            var excel = AsExcelPackage(dataSet, PrintHeaders, tableStyles);
            MemoryStream memory = new MemoryStream();
            excel.SaveAs(memory);
            excel.Dispose();
            memory.Position = 0;
            return memory;
        }

        public MemoryStream SaveToStream(DataTable dataTable, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None)
        {
            var excel = AsExcelPackage(dataTable, PrintHeaders, tableStyles);
            MemoryStream memory = new MemoryStream();
            excel.SaveAs(memory);
            excel.Dispose();
            memory.Position = 0;
            return memory;
        }

        public MemoryStream SaveToStream<TObject>(IEnumerable<TObject> listData, bool PrintHeaders = true, TableStyles tableStyles = TableStyles.None)
        {
            var excel = AsExcelPackage(listData, PrintHeaders, tableStyles);
            MemoryStream memory = new MemoryStream();
            excel.SaveAs(memory);
            excel.Dispose();
            memory.Position = 0;
            return memory;
        }

        public static Image resizeImage(Image image, int width, int height)
        {
            var destinationRect = new Rectangle(0, 0, width, height);
            var destinationImage = new Bitmap(width, height);

            destinationImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destinationImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destinationRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return (Image)destinationImage;
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
    }
}
