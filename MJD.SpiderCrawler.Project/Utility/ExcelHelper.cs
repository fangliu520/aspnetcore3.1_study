using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
namespace MJD.SpiderCrawler.Project.Utility
{
    public class ExcelHelper
    {
        private string fileName = null; //文件名
        private IWorkbook workbook = null;
        private FileStream fs = null;
        private bool disposed;
        public ExcelHelper()
        {

        }
        public ExcelHelper(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }

        /// <summary>
        /// Excel导入DataTable
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="isFirstRowColumn"></param>
        /// <returns></returns>
        public DataTable ToDataTable(string sheetName = "", bool isFirstRowColumn = false, bool other = false)
        {
            var dt = new DataTable();
            ISheet sheet = null;
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);
                //如果自定sheetName，否则读取第一个sheet
                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    //列头
                    if (isFirstRowColumn)
                    {
                        foreach (ICell item in sheet.GetRow(sheet.FirstRowNum).Cells)
                        {
                            dt.Columns.Add(item.ToString(), typeof(string));
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        int i = 0;
                        foreach (ICell item in sheet.GetRow(sheet.FirstRowNum).Cells)
                        {
                            dt.Columns.Add("Columns" + i, typeof(string));
                            i++;
                        }
                        startRow = sheet.FirstRowNum;
                    }

                    //写入内容
                    var rows = sheet.GetRowEnumerator();
                    while (rows.MoveNext())
                    {
                        IRow row = null;
                        if (fileName.IndexOf(".xlsx") > 0) row = (XSSFRow)rows.Current;
                        else if (fileName.IndexOf(".xls") > 0) row = (HSSFRow)rows.Current;
                        if (row.RowNum < startRow) continue;

                        DataRow dr = dt.NewRow();
                        foreach (ICell item in row.Cells)
                        {
                            try
                            {

                                switch (item.CellType)
                                {
                                    case CellType.Boolean:
                                        dr[item.ColumnIndex] = item.BooleanCellValue;
                                        break;
                                    case CellType.Error:
                                        dr[item.ColumnIndex] = ErrorEval.GetText(item.ErrorCellValue);
                                        break;
                                    case CellType.Formula:
                                        switch (item.CachedFormulaResultType)
                                        {
                                            case CellType.Boolean:
                                                dr[item.ColumnIndex] = item.BooleanCellValue;
                                                break;
                                            case CellType.Error:
                                                dr[item.ColumnIndex] = ErrorEval.GetText(item.ErrorCellValue);
                                                break;
                                            case CellType.Numeric:
                                                if (DateUtil.IsCellDateFormatted(item))
                                                {
                                                    dr[item.ColumnIndex] = item.DateCellValue.ToString("yyyy-MM-dd hh:MM:ss");
                                                }
                                                else
                                                {
                                                    dr[item.ColumnIndex] = item.NumericCellValue;
                                                }
                                                break;
                                            case CellType.String:
                                                string str = item.StringCellValue;
                                                if (!string.IsNullOrEmpty(str))
                                                {
                                                    dr[item.ColumnIndex] = str.ToString();
                                                }
                                                else
                                                {
                                                    dr[item.ColumnIndex] = null;
                                                }
                                                break;
                                            case CellType.Unknown:
                                            case CellType.Blank:
                                            default:
                                                dr[item.ColumnIndex] = item;
                                                break;
                                        }
                                        break;
                                    case CellType.Numeric:
                                        if (DateUtil.IsCellDateFormatted(item))
                                        {
                                            dr[item.ColumnIndex] = item.DateCellValue.ToString("yyyy-MM-dd hh:MM:ss");
                                        }
                                        else
                                        {
                                            dr[item.ColumnIndex] = item.NumericCellValue;
                                        }
                                        break;
                                    case CellType.String:
                                        string strValue = item.StringCellValue;
                                        if (!string.IsNullOrEmpty(strValue))
                                        {
                                            dr[item.ColumnIndex] = strValue.ToString();
                                        }
                                        else
                                        {
                                            dr[item.ColumnIndex] = null;
                                        }
                                        break;
                                    case CellType.Unknown:
                                    case CellType.Blank:
                                    default:
                                        dr[item.ColumnIndex] = item;
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs); //写入到excel
                workbook.Close();
                //fs.Flush();
                fs.Close();
                return count;
            }
            catch (Exception ex)
            {
                workbook.Close();
                // fs.Flush();
                fs.Close();
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }
        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string sheetName = "", bool isFirstRowColumn = false)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null)
                            {
                                //同理，没有数据的单元格都默认是null
                                var column = row.GetCell(j).ToString();
                                dataRow[j] = column;
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sheetName"></param>
        /// <param name="isColumnWritten"></param>
        /// <returns></returns>
        public int ToExcel(DataTable dt, string sheetName = "", bool isColumnWritten = false)
        {
            using (var ms = Export(dt, sheetName, isColumnWritten))
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sheetName"></param>
        /// <param name="isColumnWritten"></param>
        /// <returns></returns>
        public int ToExcel<T>(List<T> gather, Dictionary<string, string> colums = null, string sheetName = "")
        {
            using (var ms = Export(gather, colums, sheetName))
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sheetName"></param>
        /// <param name="isColumnWritten"></param>
        /// <returns></returns>
        private MemoryStream Export(DataTable dt, string sheetName, bool isColumnWritten)
        {
            ISheet sheet = null;
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();
            if (workbook != null)
            {
                if (string.IsNullOrEmpty(sheetName))
                    sheet = workbook.CreateSheet("Sheet1");
                else
                    sheet = workbook.CreateSheet(sheetName);
            }
            int startRow = 0;
            if (isColumnWritten)
            {
                IRow rowTitle = sheet.CreateRow(0);
                foreach (DataColumn column in dt.Columns)
                {
                    rowTitle.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                }
                startRow += 1;
            }

            foreach (DataRow dtRow in dt.Rows)
            {
                IRow row = sheet.CreateRow(startRow);
                foreach (DataColumn dtColumn in dt.Columns)
                {
                    var val = dtRow[dtColumn].ToString();
                    row.CreateCell(dtColumn.Ordinal).SetCellValue(val);
                }
                startRow++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                //ms.Position = 0;
                ms.Flush();
                return ms;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sheetName"></param>
        /// <param name="isColumnWritten"></param>
        /// <returns></returns>
        private MemoryStream Export<T>(List<T> gather, Dictionary<string, string> colums, string sheetName)
        {
            ISheet sheet = null;
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();
            if (workbook != null)
            {
                if (string.IsNullOrEmpty(sheetName))
                    sheet = workbook.CreateSheet("Sheet1");
                else
                    sheet = workbook.CreateSheet(sheetName);
            }
            int startRow = 0;
            if (colums.Count > 0)
            {
                IRow rowTitle = sheet.CreateRow(0);
                int i = 0;
                foreach (var item in colums.Values)
                {
                    rowTitle.CreateCell(i).SetCellValue(item.ToString());
                    i++;
                }
                startRow += 1;
            }
            for (int i = 0; i < gather.Count; i++)
            {
                T entity = gather[i];
                Type type = entity.GetType();
                IRow row = sheet.CreateRow(startRow);

                int j = 0;
                foreach (var item in colums.Keys)
                {
                    try
                    {
                        var columName = item.ToString();
                        PropertyInfo property = type.GetProperty(columName);
                        if (property != null)
                        {
                            object o = property.GetValue(entity, null);
                            var types = property.PropertyType.FullName;
                            if (o != null)
                            {
                                if (types.Contains("System.Decimal"))
                                {
                                    var value = Math.Round(Convert.ToDecimal(o.ToString()), 2, MidpointRounding.AwayFromZero);
                                    row.CreateCell(j).SetCellValue(Convert.ToDouble(value));
                                }
                                else
                                {
                                    row.CreateCell(j).SetCellValue(o.ToString());
                                }
                            }
                        }
                        j++;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                startRow++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                //ms.Position = 0;
                ms.Flush();
                return ms;
            }
        }

        private static string GetObjectPropertyValue<T>(T t, string propertyname)
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);

            if (o == null) return string.Empty;

            return o.ToString();
        }
        /// <summary>
        /// Excel导入DataTable
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="isFirstRowColumn"></param>
        /// <returns></returns>
        public DataTable ToDataTable(string sheetName = "", bool isFirstRowColumn = false)
        {
            var dt = new DataTable();
            ISheet sheet = null;
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);
                //如果自定sheetName，否则读取第一个sheet
                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    //列头
                    if (isFirstRowColumn)
                    {
                        foreach (ICell item in sheet.GetRow(sheet.FirstRowNum).Cells)
                        {
                            dt.Columns.Add(item.ToString(), typeof(string));
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        int i = 0;
                        foreach (ICell item in sheet.GetRow(sheet.FirstRowNum).Cells)
                        {
                            dt.Columns.Add("Columns" + i, typeof(string));
                            i++;
                        }
                        startRow = sheet.FirstRowNum;
                    }

                    //写入内容
                    var rows = sheet.GetRowEnumerator();
                    while (rows.MoveNext())
                    {
                        IRow row = null;
                        if (fileName.IndexOf(".xlsx") > 0) row = (XSSFRow)rows.Current;
                        else if (fileName.IndexOf(".xls") > 0) row = (HSSFRow)rows.Current;
                        if (row.RowNum < startRow) continue;

                        DataRow dr = dt.NewRow();
                        foreach (ICell item in row.Cells)
                        {
                            try
                            {

                                switch (item.CellType)
                                {
                                    case CellType.Boolean:
                                        dr[item.ColumnIndex] = item.BooleanCellValue;
                                        break;
                                    case CellType.Error:
                                        dr[item.ColumnIndex] = ErrorEval.GetText(item.ErrorCellValue);
                                        break;
                                    case CellType.Formula:
                                        switch (item.CachedFormulaResultType)
                                        {
                                            case CellType.Boolean:
                                                dr[item.ColumnIndex] = item.BooleanCellValue;
                                                break;
                                            case CellType.Error:
                                                dr[item.ColumnIndex] = ErrorEval.GetText(item.ErrorCellValue);
                                                break;
                                            case CellType.Numeric:
                                                if (DateUtil.IsCellDateFormatted(item))
                                                {
                                                    dr[item.ColumnIndex] = item.DateCellValue.ToString("yyyy-MM-dd hh:MM:ss");
                                                }
                                                else
                                                {
                                                    dr[item.ColumnIndex] = item.NumericCellValue;
                                                }
                                                break;
                                            case CellType.String:
                                                string str = item.StringCellValue;
                                                if (!string.IsNullOrEmpty(str))
                                                {
                                                    dr[item.ColumnIndex] = str.ToString();
                                                }
                                                else
                                                {
                                                    dr[item.ColumnIndex] = null;
                                                }
                                                break;
                                            case CellType.Unknown:
                                            case CellType.Blank:
                                            default:
                                                dr[item.ColumnIndex] = item;
                                                break;
                                        }
                                        break;
                                    case CellType.Numeric:
                                        if (DateUtil.IsCellDateFormatted(item))
                                        {
                                            dr[item.ColumnIndex] = item.DateCellValue.ToString("yyyy-MM-dd hh:MM:ss");
                                        }
                                        else
                                        {
                                            dr[item.ColumnIndex] = item.NumericCellValue;
                                        }
                                        break;
                                    case CellType.String:
                                        string strValue = item.StringCellValue;
                                        if (!string.IsNullOrEmpty(strValue))
                                        {
                                            dr[item.ColumnIndex] = strValue.ToString();
                                        }
                                        else
                                        {
                                            dr[item.ColumnIndex] = null;
                                        }
                                        break;
                                    case CellType.Unknown:
                                    case CellType.Blank:
                                    default:
                                        dr[item.ColumnIndex] = item;
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (fs != null)
                        fs.Close();
                }

                fs = null;
                disposed = true;
            }
        }
        /// <summary>
        /// DataTable中的数据导出到Excel
        /// </summary>
        /// <param name="dt">要导出的DataTable</param>
        /// <param name="FileType">类型</param>
        /// <param name="FileName">Excel的文件名</param>
        public void CreateExcel(DataTable table, string file)
        {
            try
            {
                IWorkbook wb = new HSSFWorkbook();

                Dictionary<int, string> dics = new Dictionary<int, string>();
                int i = 0;      //行索引
                int count = 1; //sheet索引
                int index = 0;
                ISheet st = wb.CreateSheet("第" + count + "页");
                ///******生成列名*****///
                IRow row = st.CreateRow(i);
                foreach (DataColumn colo in table.Columns)
                {
                    row.CreateCell(index, CellType.String).SetCellValue(colo.ColumnName);
                    index++;
                }
                ///******生成列名*****///

                i = 1;//从第2列填充数据
                for (var j = 0; j < table.Rows.Count; j++)
                {
                    ///******当数据超过5万行换一个sheet*****///
                    if (i == 50000)
                    {
                        i = 0;
                        count++;
                        st = wb.CreateSheet("第" + count + "页");
                    }
                    IRow row2 = st.CreateRow(i);
                    for (int k = 0; k < index; k++)
                    {
                        row2.CreateCell(k).SetCellValue(table.Rows[j][k].ToString());
                    }
                    i++;

                }
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    wb.Write(fs);
                    fs.Flush();
                }

            }
            catch
            {

            }

        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="list">导出集合</param>
        /// <param name="_object">头部显示实体</param>
        /// <returns></returns>
        public static string CreateExcelHelper(object list, object _object = null)
        {
            string ReStr = "";
            if (!list.GetType().IsGenericType) return "Error:参数必须为泛型";
            List<object> newlist = new List<object>();
            foreach (object o in (IEnumerable)list)
            {
                newlist.Add(o);
            }
            IWorkbook wb = new HSSFWorkbook();
            ISheet st = wb.CreateSheet();
            if (_object != null)
            {
                HSSFRow _HSSFRow = (HSSFRow)st.CreateRow(0);
                int index = 0;
                foreach (PropertyInfo item in _object.GetType().GetProperties())
                {
                    if (item.GetValue(_object, null) != null)
                    {
                        _HSSFRow.CreateCell(index).SetCellValue(item.GetValue(_object, null).ToString());
                    }
                    else
                    {
                        _HSSFRow.CreateCell(index).SetCellValue("");
                    }
                    index++;
                }
            }
            if (newlist.Count > 0)
            {
                int i = _object == null ? 0 : 1;
                foreach (object obj in newlist)
                {
                    if (obj != null)
                    {
                        PropertyInfo[] Arr = obj.GetType().GetProperties();
                        IRow row = st.CreateRow(i);
                        int j = 0;
                        foreach (PropertyInfo item in Arr)
                        {
                            ICell _ICell = row.CreateCell(j, CellType.String);
                            if (item.GetValue(obj, null) != null)
                            {
                                _ICell.SetCellValue(item.GetValue(obj, null).ToString());
                            }
                            else
                            {
                                _ICell.SetCellValue("");
                            }
                            j++;
                        }
                        i++;
                    }
                }
                string direct = $"{System.AppDomain.CurrentDomain.BaseDirectory}WaitFile";//HttpContext.Current.Server.MapPath("~/bigcustomerdownload");
                if (!Directory.Exists(direct))
                {
                    Directory.CreateDirectory(direct);
                }
                Random _Random = new Random();
                int randNum = _Random.Next(4);
                ReStr = DateTime.Now.ToString("yyyyMMddHHmmssfff") + randNum.ToString() + ".xls";
                using (FileStream fs = new FileStream(direct + "/" + ReStr, FileMode.Create))
                {
                    wb.Write(fs);
                    fs.Flush();
                }
            }
            return ReStr;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sheetName"></param>
        /// <param name="suffix"></param>
        /// <param name="isColumnWritten"></param>
        /// <returns></returns>
        public IWorkbook GetWorkbook(DataTable dt, string sheetName, string suffix, bool isColumnWritten)
        {
            ISheet sheet = null;
            if (suffix == ".xlsx") workbook = new XSSFWorkbook(); // 2007版本
            else if (suffix == ".xls") workbook = new HSSFWorkbook(); // 2003版本
            if (workbook != null)
            {
                if (string.IsNullOrEmpty(sheetName)) sheet = workbook.CreateSheet("Sheet1");
                else sheet = workbook.CreateSheet(sheetName);
            }
            int startRow = 0;
            if (isColumnWritten)
            {
                IRow rowTitle = sheet.CreateRow(0);
                foreach (DataColumn column in dt.Columns)
                {
                    rowTitle.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                }
                startRow += 1;
            }

            foreach (DataRow dtRow in dt.Rows)
            {
                IRow row = sheet.CreateRow(startRow);
                foreach (DataColumn dtColumn in dt.Columns)
                {
                    var val = dtRow[dtColumn].ToString();
                    row.CreateCell(dtColumn.Ordinal).SetCellValue(val);
                }
                startRow++;
            }
            return workbook;
        }
    }
}