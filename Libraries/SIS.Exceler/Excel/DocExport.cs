using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//using SISOPM.Excel;

namespace SIS.Exceler
{

    public class DocExport
    {
        private DataTable dt;

        public DataTable Dt
        {
            get { return dt; }
            set { dt = value; }
        }

        public void Export(string fileName, ExportFormat exportFormat, Encoding encoding)
        {
           
            Common.Export(dt, exportFormat, fileName, encoding);
        }

        /// <summary>
        /// 导出SmartGridView的数据源的数据
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="exportFormat">导出文件的格式</param>
        public void Export(string fileName, ExportFormat exportFormat)
        {
            Export(fileName, exportFormat, Encoding.GetEncoding("GB2312"));
        }

        /// <summary>
        /// 导出SmartGridView的数据源的数据为Excel
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void Export(string fileName)
        {
            Export(fileName, ExportFormat.CSV);
        }

        /// <summary>
        /// 导出SmartGridView的数据源的数据
        /// </summary>
        /// <param name="fileName">输出文件名</param>
        /// <param name="columnIndexList">导出的列索引数组</param>
        /// <param name="exportFormat">导出文件的格式</param>
        /// <param name="encoding">编码</param>
        public void Export(string fileName, int[] columnIndexList, ExportFormat exportFormat, Encoding encoding)
        {
            Common.Export(dt, columnIndexList, exportFormat, fileName, encoding);
        }

        /// <summary>
        /// 导出SmartGridView的数据源的数据
        /// </summary>
        /// <param name="fileName">输出文件名</param>
        /// <param name="columnNameList">导出的列的列名数组</param>
        /// <param name="exportFormat">导出文件的格式</param>
        /// <param name="encoding">编码</param>
        public void Export(string fileName, string[] columnNameList, ExportFormat exportFormat, Encoding encoding)
        {
            Common.Export(dt, columnNameList, exportFormat, fileName, encoding);
        }

        /// <summary>
        /// 导出SmartGridView的数据源的数据
        /// </summary>
        /// <param name="fileName">输出文件名</param>
        /// <param name="columnIndexList">导出的列索引数组</param>
        /// <param name="headers">导出的列标题数组</param>
        /// <param name="exportFormat">导出文件的格式</param>
        /// <param name="encoding">编码</param>
        public void Export(string fileName, int[] columnIndexList, string[] headers, ExportFormat exportFormat, Encoding encoding)
        {
            Common.Export(dt, columnIndexList, headers, exportFormat, fileName, encoding);
        }

        /// <summary>
        /// 导出SmartGridView的数据源的数据
        /// </summary>
        /// <param name="fileName">输出文件名</param>
        /// <param name="columnNameList">导出的列的列名数组</param>
        /// <param name="headers">导出的列标题数组</param>
        /// <param name="exportFormat">导出文件的格式</param>
        /// <param name="encoding">编码</param>
        public void Export(string fileName, string[] columnNameList, string[] headers, ExportFormat exportFormat, Encoding encoding)
        {
            Common.Export(dt, columnNameList, headers, exportFormat, fileName, encoding);
        }

        /// <summary>
        /// 获取数据源（DataTable）
        /// </summary>
    }
}
