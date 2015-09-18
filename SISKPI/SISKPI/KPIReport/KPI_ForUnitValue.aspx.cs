using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using SIS.DataAccess;
using SIS.DataEntity;
using SIS.DBControl;
using SIS.Loger;
using SIS.Assistant;
using SIS.Exceler;

using System.Linq;
using System.Text;
using System.IO;

namespace SISKPI
{
    /// <summary>
    /// 显示、编辑
    /// 数据信息
    /// </summary>
    public partial class KPI_ForUnitValue : System.Web.UI.Page
    {
        static string WebCode = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //指标信息
                if (Request.QueryString["webcode"] != null)
                {
                    WebCode = Request.QueryString["webcode"].ToString();

                    lblInfor.Text = "报表名称: " + KPI_WebDal.GetWebDesc(WebCode);
                }
                else
                {
                    WebCode = "";
                }


                ////机组信息
                DataTable dt = KPI_ShiftDal.GetShifts();
                ddlShift.Items.Add(new ListItem("全部", "ALL"));
                foreach (DataRow dr in dt.Rows)
                {
                    ddlShift.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //
                txt_ST.Value = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                txt_ET.Value = DateTime.Now.ToString("yyyy-MM-dd");

                BindValue();
            }
        }

        /// <summary>
        /// 机组信息
        /// </summary>
        void BindValue()
        {
            if (WebCode == "")
            {
                return;
            }
            
            DateTime StartTime =DateTime.Now;
            DateTime EndTime  = DateTime.Now;

            //都是 日期的形式
            if(!DateTime.TryParse(txt_ST.Value, out StartTime)
                || !DateTime.TryParse(txt_ET.Value, out EndTime)
                || StartTime >= EndTime)
            {
                MessageBox.popupClientMessage(this.Page, "时间格式不正确 或 时间范围不正确!");
                return;
            }
            
            //加 1小时
            StartTime = StartTime.AddHours(1);
            EndTime = EndTime.AddHours(1);


            string shiftname = ddlShift.SelectedItem.Text;
            if (shiftname == "全部")
            {
                shiftname = "";
            }
            //WebCode

            //得分、得分率、合格率、最大值、最小值、算数平均值、加权平均值、累计值、累计除法
            string calctype = KPI_WebKeyDal.GetCalcType(WebCode);

            DataTable dtValue = ECSSArchiveDal.GetForUnitValue(shiftname, WebCode, calctype, StartTime, EndTime);

            DataTable dtGD = ConvertToTable(dtValue, calctype);
            gvData.DataSource = dtGD;
            gvData.DataBind();

        }


        #region  转换表

        protected DataTable ConvertToTable(DataTable source, string calctype)
        {
            DataTable dt = new DataTable();

            //第一列是固定的
            dt.Columns.Add("指标");

            //ECName, ECShift, KeyIndex, Sum(ECScore) AS ECScore
            // x[1] 是字段 ECShift 
            //以 ECShift 字段为筛选条件 列转为行
            var columns = (from x in source.Rows.Cast<DataRow>() orderby x[1] select x[1].ToString()).Distinct();

            //把 ECShift 字段做为新字段添加进去
            foreach (var item in columns)
            {
                dt.Columns.Add(item).DefaultValue = "";
            }

            //最后1列是总和
            dt.Columns.Add("合计");

            // x[0] 是字段 ECName 
            // 按  ECName 分组 g 是分组后的信息  g.Key 就是名字  如果不懂就去查一个linq group子句进行分组
            var data = from x in source.Rows.Cast<DataRow>()
                       group x by x[0] into g
                       select new { Key = g.Key.ToString(), Items = g };

            int cl = 1;

            data.ToList().ForEach(x =>
            {
                //这里用的是一个string 数组 也可以用DataRow根据个人需要用
                string[] array = new string[dt.Columns.Count];

                List<double> arraydb = new List<double>();

                //array[0]就是存名字的
                array[0] = x.Key;

                //从第二列开始遍历
                for (int i = 1; i < dt.Columns.Count-1; i++)
                {
                    //array[i]就是 各种提成
                    array[i] = (from y in x.Items
                                where y[1].ToString() == dt.Columns[i].ToString()        //  y[1] 各种指标名字等于 table中列的名字
                                select y[3].ToString()                                   //  y[3] 就是我们要找的  LinqValue 的各种数值
                              ).SingleOrDefault();

                    if(array[i] != null)
                    {
                        array[i] = double.Parse(array[i]).ToString("0.000");
                        arraydb.Add(double.Parse(array[i]));
                    }

                }

                if (arraydb.Count == 0)
                {
                    array[dt.Columns.Count - 1] = null ;
                }
                else if (arraydb.Count == 1)
                {
                    array[dt.Columns.Count - 1] = arraydb[0].ToString();
                }
                else
                {

                    switch (calctype)
                    {
                        //得分
                        case "0":
                        //累计值
                        case "7":
                            array[dt.Columns.Count - 1] = arraydb.Sum().ToString();

                            break;

                        //得分率
                        case "1":
                        //合格率
                        case "2":
                        //算数平均值
                        case "5":
                        //加权平均值
                        case "6":

                            array[dt.Columns.Count - 1] = arraydb.Average().ToString();
                            break;

                        //最大值
                        case "3":

                            array[dt.Columns.Count - 1] = arraydb.Max().ToString();
                            break;

                        //最小值
                        case "4":

                            array[dt.Columns.Count - 1] = arraydb.Min().ToString();
                            break;


                        //累计除法
                        case "8":
                            array[dt.Columns.Count - 1] = null;
                            break;

                        default:
                            array[dt.Columns.Count - 1] = null;
                            break;
                    }
                }

                cl++;

                dt.Rows.Add(array);  //添加到table中
            });


            return dt;
        }

        #endregion

        #region 信息

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        #endregion


		public override void VerifyRenderingInServerForm(Control control) {
			//base.VerifyRenderingInServerForm(control);
		}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindValue();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
			//if (gvData.Rows.Count > 0)
			//{
			//    DocExport docEexport = new DocExport();

			//    docEexport.Dt = GridViewHelper.GridView2DataTable(gvData);

			//    docEexport.Export("SIS实时运行绩效报表");
			//}

			Response.ContentEncoding = Encoding.UTF8;
			Response.ContentType = "application/ms-excel";
			Response.Charset = "utf-8";
			Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("SIS实时运行绩效报表.xls", System.Text.Encoding.UTF8).ToString());
			StringWriter sw = new StringWriter();
			HtmlTextWriter htw = new HtmlTextWriter(sw);
			gvData.RenderControl(htw);
			Response.Write(sw.ToString());
			Response.Flush();
			Response.End();
			sw.Close();
			sw.Dispose();
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_ForValue.aspx");
        }
    }
}
