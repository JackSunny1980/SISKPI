/**//*----------------------------------------------------------------
// Copyright (C) 2010  版权所有。 
//  
// 文件名：
// 文件功能描述：
// 
// 创建标识： 
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SIS.Exceler
{
    public class GridViewHelper
    {
        public static string GetCellText(TableCell cell)
        {
            string text = cell.Text;
            if (!string.IsNullOrEmpty(text))
            {
                return text;
            }
            foreach (Control control in cell.Controls)
            {
                if (control != null && control is IButtonControl)
                {
                    IButtonControl btn = control as IButtonControl;
                    text = btn.Text.Replace("\r\n", "").Trim();
                    break; 
                }
                if (control != null && control is ITextControl)
                {
                    LiteralControl lc = control as LiteralControl;
                    if (lc != null)
                    {
                        continue;
                    }
                    ITextControl l = control as ITextControl;

                    text = l.Text.Replace("\r\n", "").Trim();
                    break;
                }
            }
            return text;
        }

        /**//// <summary>
        /// 从GridView的数据生成DataTable
        /// </summary>
        /// <param name="gv">GridView对象</param>
        public static DataTable GridView2DataTable(GridView gv)
        {
            DataTable table = new DataTable();
            int rowIndex = 0;
            List<string> cols = new List<string>();
            if (!gv.ShowHeader && gv.Columns.Count == 0)
            {
                return table;
            }

            GridViewRow headerRow = gv.HeaderRow;
            int columnCount = headerRow.Cells.Count;
            for (int i = 0; i < columnCount; i++)
            {
                string text = GetCellText(headerRow.Cells[i]);
                cols.Add(text);
            }

            foreach (GridViewRow r in gv.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    DataRow row = table.NewRow();
                    //生成行

                    int j = 0;
                    for (int i = 0; i < columnCount; i++)
                    {
                        string text = GetCellText(r.Cells[i]);

                        if (rowIndex == 0)
                        {
                            string columnName = cols[i];
                            if (String.IsNullOrEmpty(columnName))
                            {        
                                continue;
                            }
                            if (table.Columns.Contains(columnName))
                            {
                                continue;
                            }

                            DataColumn dc = table.Columns.Add();
                            dc.ColumnName = columnName;
                            dc.DataType = typeof(string);

                        }

                        if (!String.IsNullOrEmpty(text))
                        {
                            if (text == "&nbsp;")
                            {
                                row[j] = "";
                            }
                            else
                            {
                                row[j] = text;
                            }

                            j++;
                        }        

                    }

                    rowIndex++;
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }
}