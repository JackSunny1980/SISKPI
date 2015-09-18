using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

using SIS.DBControl;
using SIS.Loger;
using SIS.Assistant;

using SIS.DataAccess;
using SIS.DataEntity;
using SIS.Exceler;


namespace SISKPI
{
    public partial class KPI_ForAvg : System.Web.UI.Page
    {
        //static string WebCode = "";
        static double totalvalue = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                //if (Request.QueryString["title"] != null)
                //{
                //    string title = OPM_TitleDal.GetTitle(Request.QueryString["title"].ToString());
                //    if (title != "")
                //    {
                //        lblTitle.Text = title;
                //    }
                //}

                ///////////////////////////////////////////////////////////////////////////////////////////
                //是否需要显示单元
                //string plantid = "";
                //if (Request.QueryString["plantcode"] != null)
                //{
                //    string plantcode = Request.QueryString["plantcode"].ToString();

                //    if (plantcode != "")
                //    {
                //        plantid = KPI_PlantDal.GetPlantIDByCode(plantcode);
                //    }
                //} 

                ////机组信息
                //DataTable dt = KPI_UnitDal.GetUnits(plantid);
                //foreach (DataRow dr in dt.Rows)
                //{
                //    ddlUnit.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                //}

                //
                txt_ST.Value = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:00:00");
                txt_ET.Value = DateTime.Now.ToString("yyyy-MM-dd HH:00:00");

                BindValue();
            }
            
        }
        
        public void BindValue()
        {
            DateTime StartTime = DateTime.Now;
            DateTime EndTime = DateTime.Now;

            //都是 日期的形式
            if (!DateTime.TryParse(txt_ST.Value, out StartTime)
                || !DateTime.TryParse(txt_ET.Value, out EndTime)
                || StartTime >= EndTime)
            {
                MessageBox.popupClientMessage(this.Page, "时间格式不正确 或 时间范围不正确!");
                return;
            }


            //不用添加
            //加 1小时
            //StartTime = StartTime.AddHours(1);
            //EndTime = EndTime.AddHours(1);

            //string UnitID = ddlUnit.SelectedValue;
            //WebCode

            totalvalue = 0;

            //平均值查询

            DataTable dtValue = ECSSArchiveDal.GetForAvgValue(StartTime, EndTime);

            //值处理 
            foreach (DataRow dr in dtValue.Rows)
            {
                double dAVG = 0;
                double dDSG = 0;
                double dOPT = 0;
                double dMNY = 0;
                if (dr["KeyAVG"] != null && double.TryParse(dr["KeyAVG"].ToString(), out dAVG))
                {
                    //设计偏差与百分比
                    if (dr["KeyDesign"] != null && double.TryParse(dr["KeyDesign"].ToString(), out dDSG))
                    {
                        dr["KeyOptDiff"] = (dAVG-dDSG).ToString("0.000");
                        if (dDSG == 0)
                        {
                            dr["KeyOptPercent"] = "100";
                            dMNY = 0;
                        }
                        else
                        {
                            dr["KeyOptPercent"] = ((dAVG - dDSG)/dDSG).ToString("0.000");
                            dMNY = double.Parse(dr["KeyDIffMoney"].ToString()) * (1 - Math.Abs((dAVG - dDSG) / dDSG));
                        }
                    }
                    
                    //目标偏差与百分比
                    if (dr["KeyTarget"] != null && double.TryParse(dr["KeyTarget"].ToString(), out dOPT))
                    {
                        dr["KeyTarDiff"] = (dAVG - dOPT).ToString("0.000");
                        if (dDSG == 0)
                        {
                            dr["KeyTarPercent"] = "100";
                            dMNY += 0;
                        }
                        else
                        {
                            dr["KeyTarPercent"] = ((dAVG - dDSG) / dDSG).ToString("0.000");
                            dMNY += double.Parse(dr["KeyOptMoney"].ToString()) * (1 - Math.Abs((dAVG - dOPT) / dOPT));
                        }
                    }

                    //奖金
                    dr["KeyMoney"] = dMNY.ToString("0.000");
                    totalvalue += dMNY;

                }

            }



            gvData.DataSource = dtValue;
            gvData.DataBind();

 
        }
                
        #region GridView Key

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "合计";
                e.Row.Cells[10].Text = totalvalue.ToString("0.000");
            }

        }              


        #endregion


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindValue();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvData.Rows.Count > 0)
            {
                DocExport docEexport = new DocExport();

                docEexport.Dt = GridViewHelper.GridView2DataTable(gvData);

                docEexport.Export("SIS实时运行绩效报表");
            }

        }



    }


}