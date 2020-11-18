using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Freed.Common.Helpers
{
    public class ExcelHelper
    {
        public int startRow = 0;
        public int startDataRow = 1;
        public short rowHeight = 20 * 20;
        public int cellWidth = 20 * 256;
        IWorkbook book = null;

        #region DataTable 转Excel
        /// <summary>
        /// 导出DataTable的数据到Excel文件中
        /// </summary>
        /// <param name="t">将要导出的数据表</param>
        /// <param name="cols">Excel 和 datatable的列信息</param>
        /// <returns></returns>
        /// <remarks>
        /// cols: Dictionary<string, Col> students = new Dictionary<string, Col>(){{ "name", new Col { tColName="name",eColName="姓名",colWidth=256*30}}, };
        /// </remarks>
        public MemoryStream DataTableToExcel(DataTable t, Dictionary<string, Col> cols)
        {
            if (cols == null || cols.Count <= 0) return null;
            book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("数据");
            // 添加表头
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(startRow);

            NPOI.SS.UserModel.ICellStyle style = book.CreateCellStyle();
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            int c = 0;
            foreach (string k in cols.Keys)
            {
                NPOI.SS.UserModel.ICell cell = row.CreateCell(c);
                cols[k].colIndex = c;
                cell.SetCellValue(cols[k].eColName);
                cell.CellStyle = style;
                sheet.SetColumnWidth(c, cols[k].colWidth);
                c++;
            }
            for (int i = 0; i < t.Rows.Count; i++)
            {
                row = sheet.CreateRow(startDataRow + 1);
                foreach (string k in cols.Keys)
                {
                    if (t.Rows[i][k] != null)
                    {
                        NPOI.SS.UserModel.ICell cell = row.CreateCell(cols[k].colIndex);
                        cell.SetCellValue(t.Rows[i][k].ToString());
                        cell.CellStyle = style;
                    }
                }
            }


            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            //book.Dispose();
            book = null;
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            return ms;
        }

        /// <summary>
        /// 将DataTable的数据导出到Excel 中
        /// </summary>
        /// <param name="t">将要导出的数据表 </param>
        /// <returns></returns>
        /// <remarks>
        /// 表导出后的Excel列名为表的列名
        /// </remarks>
        public MemoryStream DataTableToExcel(DataTable t)
        {
            book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("数据");
            // 添加表头
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
            row.Height = rowHeight;
            NPOI.SS.UserModel.ICellStyle style = book.CreateCellStyle();
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            IFont font = book.CreateFont();
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            style.SetFont(font);

            NPOI.SS.UserModel.ICellStyle style1 = book.CreateCellStyle();
            style1.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style1.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            NPOI.SS.UserModel.ICell cell = null;
            for (int c = 0; c < t.Columns.Count; c++)
            {
                cell = row.CreateCell(c);
                cell.SetCellValue(t.Columns[c].ColumnName);
                cell.CellStyle = style;
                sheet.SetColumnWidth(c, cellWidth);
            }
            for (int r = 0; r < t.Rows.Count; r++)
            {
                row = sheet.CreateRow(r + startDataRow);
                int c = 0;
                for (; c < t.Columns.Count; c++)
                {
                    if (t.Rows[r][c] != null)
                    {
                        cell = row.CreateCell(c);
                        cell.CellStyle = style1;
                        cell.SetCellValue(t.Rows[r][c].ToString());
                    }
                }
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            //book.Dispose();
            book = null;
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            //return File(ms, "application/vnd.ms-excel", "材料送样进度 资料导出  " + DateTime.Now.ToString("YYYYMMddhhmm") + ".xls");
            return ms;
        }
        /// <summary>
        /// 将DataTable的数据导出到Excel
        /// </summary>
        /// <param name="t">将要导出的数据表</param>
        /// <param name="cols">Excel 的列信息 按顺序对应DataTable的列</param>
        /// <returns></returns>
        /// <remarks>
        /// cols : string[] cols = new string[]
        //{
        //    "申请编号", "当前状态", "承认书编号",    "料件编号", "料件名称", "料件规格", "供方材料型号",   "供方材料描述",   "供应商编号",    "供应商名称",    "收件数量", "发起人",  "快递信息", "计量单位", "备注",   "附件需求", "收件人",  "发起时间", "修改时间", "提交时间"
        //};
        /// </remarks>
        public MemoryStream DataTableToExcel(DataTable t, string[] cols)
        {
            book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("数据");
            // 添加表头
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
            row.Height = rowHeight;

            NPOI.SS.UserModel.ICellStyle style = book.CreateCellStyle();
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            NPOI.SS.UserModel.ICell cell = null;
            for (int c = 0; c < cols.Length; c++)
            {
                cell = row.CreateCell(c);
                cell.SetCellValue(cols[c]);
                cell.CellStyle = style;
                sheet.SetColumnWidth(c, cellWidth);
            }

            for (int r = 0; r < t.Rows.Count; r++)
            {
                row = sheet.CreateRow(r + startDataRow);
                int c = 0;
                for (; c < t.Columns.Count; c++)
                {
                    if (t.Rows[r][c] != null)
                    {
                        cell = row.CreateCell(c);
                        cell.SetCellValue(t.Rows[r][c].ToString());
                    }
                }

            }

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            //book.Dispose();
            book = null;
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            //return File(ms, "application/vnd.ms-excel", "材料送样进度 资料导出  " + DateTime.Now.ToString("YYYYMMddhhmm") + ".xls");
            return ms;
        }
        /// <summary>
        /// 将DataTable的数据导出到Excel 中
        /// </summary>
        /// <param name="t">将要导出的数据表</param>
        /// <param name="cols">Excel中的列名，按顺序对应t中的列，列名之间按‘；'分割</param>
        /// <returns></returns>
        /// <remarks>
        /// cols:"申请编号；当前状态；承认书编号"
        /// </remarks>
        public MemoryStream DataTableToExcel(DataTable t, string cols)
        {
            return DataTableToExcel(t, cols.Split('；'));
        }
        #endregion

        #region Excel转DataTable
        public delegate bool ExcuteIRow(IRow r);
        public ExcuteIRow ExcuteDataRow;
        public ExcuteIRow ExcuteHeadRow;
        public delegate bool ExcuteCell(int rIndex, int cIndex, object cellVal);
        public ExcuteCell ExcuteHeadCell;
        public ExcuteCell ExcuteDataCell;

        /// <summary>
        /// Excel表转换成DataTable
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="file">文件的数据流</param>
        /// <param name="maxMB">文件大小限制</param>
        /// <param name="sheetIndex">表格的数字索引</param>
        /// <returns></returns>
        public DataTable ExcelToDataTable(string fileName, Stream file, int maxMB = 20, int sheetIndex = 0)
        {
            int i = 0;
            int c = 0;
            try
            {
                if (file.Length > maxMB * 1024 * 1024) throw new Exception(string.Format("文件大于{0}M", maxMB));
                string extion = new FileInfo(fileName).Extension.Replace(".", "").ToLower();
                if (extion != "xls" && extion != "xlsx") throw new Exception(string.Format("{0} ,该文件不是Excel文件，或者我们无法识别此格式的Excel文件"));
                if (extion == "xlsx")
                {
                    book = new XSSFWorkbook(file);
                }
                else
                {
                    book = new HSSFWorkbook(file);
                }
                ISheet sheet = book.GetSheetAt(sheetIndex);
                if (sheet == null) throw new Exception("根据表格索引无法获取文件中的 表格 ");

                DataTable dt = new DataTable();
                if (sheet.LastRowNum <= 0)
                    return dt;
                IRow r = sheet.GetRow(startRow);
                if (ExcuteHeadRow != null) ExcuteHeadRow(r);
                for (c = 0; c < r.LastCellNum; c++)
                {
                    string val = string.IsNullOrEmpty(r.GetCell(c).StringCellValue) ? c.ToString() + "列 无列名" : r.GetCell(c).StringCellValue;
                    dt.Columns.Add(new DataColumn(val));
                    if (ExcuteHeadCell != null) ExcuteHeadCell(startDataRow, c, val);
                }

                for (i = startDataRow; i <= sheet.LastRowNum; i++)
                {

                    r = sheet.GetRow(i);
                    if (r == null) continue;
                    if (ExcuteDataRow != null) ExcuteDataRow(r);
                    DataRow dr = dt.NewRow();
                    for (c = 0; c < dt.Columns.Count; c++)
                    {
                        ICell cell = r.GetCell(c);
                        if (cell == null) continue;
                        switch (cell.CellType)
                        {
                            case CellType.Blank:
                                dr[c] = string.Empty;

                                break;
                            case CellType.Boolean:

                                if (ExcuteHeadCell != null) ExcuteHeadCell(startDataRow, c, cell.BooleanCellValue);
                                dr[c] = cell.BooleanCellValue;
                                break;
                            case CellType.Numeric:
                                if (DateUtil.IsCellDateFormatted(cell))//日期
                                {
                                    if (ExcuteHeadCell != null) ExcuteHeadCell(startDataRow, c, cell.DateCellValue);
                                    //dr[c] =cell.DateCellValue.ToLocalTime().ToString().Replace(",","");
                                    dr[c] = cell.DateCellValue.ToString("yyyy-MM-dd HH:mm:ss").Replace(",", "");
                                }
                                else
                                {
                                    if (ExcuteHeadCell != null) ExcuteHeadCell(startDataRow, c, cell.NumericCellValue);
                                    dr[c] = cell.NumericCellValue.ToString().Replace(",", "");
                                }
                                break;
                            case CellType.String:
                                if (ExcuteHeadCell != null) ExcuteHeadCell(startDataRow, c, cell.StringCellValue.Trim());
                                dr[c] = cell.StringCellValue.Trim();
                                break;
                            case CellType.Error:
                                if (ExcuteHeadCell != null) ExcuteHeadCell(startDataRow, c, cell.ErrorCellValue);
                                dr[c] = cell.ErrorCellValue;
                                break;
                            case CellType.Formula://公式
                                try
                                {
                                    HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                                    e.EvaluateInCell(cell);
                                    dr[c] = cell.ToString();
                                    if (ExcuteHeadCell != null) ExcuteHeadCell(startDataRow, c, cell.ToString());
                                }
                                catch
                                {
                                    try
                                    {
                                        if (DateUtil.IsCellDateFormatted(cell))//日期
                                        {
                                            dr[c] = cell.DateCellValue;
                                            if (ExcuteHeadCell != null) ExcuteHeadCell(startDataRow, c, cell.DateCellValue);
                                        }
                                        else
                                        {
                                            dr[c] = cell.NumericCellValue;
                                            if (ExcuteHeadCell != null) ExcuteHeadCell(startDataRow, c, cell.NumericCellValue);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        ;
                                    }

                                }
                                break;
                            default:
                                dr[c] = cell.ToString();
                                if (ExcuteHeadCell != null) ExcuteHeadCell(startDataRow, c, cell.ToString());
                                break;
                        }
                    }

                    dt.Rows.Add(dr);
                }

                return dt;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + string.Format("[{0},{1}]", i, c));
            }
            finally
            {
                //if(book!=null)
                //    book.Dispose();
                file.Dispose();
            }

        }



        #endregion



    }

    /// <summary>
    /// Copyright (C) 领益智造 版权所有
    /// 
    /// 文件名称：ExcelHelper.cs
    /// 类      ：Excel 列信息控制类
    /// 数 据 表：/
    /// 创    建： 杨先金 2018-01-04
    /// 功能描述： 
    /// 1.列和DataTable的列的映射关系
    /// 2.列宽的控制
    /// 
    /// 修改记录：
    ///     R1:
    ///         修 改 人： 杨先金 2018-12-09
    ///         修改内容： 
    ///             
    /// </summary>
    public class Col
    {
        /// <summary>
        /// DataTable的列名
        /// </summary>
        public string tColName { get; set; }
        /// <summary>
        /// Excel的列名
        /// </summary>
        public string eColName { get; set; }
        /// <summary>
        /// Excel的列宽
        /// </summary>
        public int colWidth = 20 * 256;
        /// <summary>
        /// 辅助用，不需要设置值
        /// </summary>
        public int colIndex { get; set; }
    }
}
