using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SIS.DBControl;
using System.IO;
using System.Web.UI.HtmlControls;

using SIS.Loger;

using SIS.DataAccess;
using SIS.DataEntity;
using SIS.Exceler;


namespace SISKPI
{
    public partial class KPI_ZJScorePlant : System.Web.UI.Page
    {
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
                string plantid = "";
                if (Request.QueryString["plantcode"] != null)
                {
                    string plantcode = Request.QueryString["plantcode"].ToString();

                    if (plantcode != "")
                    {
                        plantid = KPI_PlantDal.GetPlantIDByCode(plantcode);
                    }
                } 
                
                //机组信息
                DataTable dt = KPI_UnitDal.GetUnits(plantid);
                foreach (DataRow dr in dt.Rows)
                {
                    ddlUnit.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }


                //获得参数集合
                //unitcode,为了获得值次
                if (Request.QueryString["unitcode"] != null)
                {
                    ViewState["unitcode"] = Request.QueryString["unitcode"].ToString();
                }
                else
                {
                    ViewState["unitcode"] = "";
                }
                                
                //页面集合
                //
                if (Request.QueryString["ecweb"] != null)
                {
                    ViewState["ecweb"] = Request.QueryString["ecweb"].ToString();
                }
                else
                {
                    ViewState["ecweb"] = "";
                }

                //
                DateTime dtST = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime dtET = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                if (dtET == dtST)
                {
                    dtST = dtST.AddMonths(-1);
                }

                txt_ST.Value = dtST.ToString("yyyy-MM-dd");
                txt_ET.Value = dtET.ToString("yyyy-MM-dd");
                                
                BindValues();

            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        void BindValues()
        {
            //需要显示的集合
            string ecweb = ViewState["ecweb"].ToString();

            //if (ecweb.Equals(""))
            //{
            //    return;
            //}

            string unitid = ddlUnit.SelectedValue;

            //时间
            DateTime dtST = DateTime.Parse(txt_ST.Value);
            DateTime dtET = DateTime.Parse(txt_ET.Value);

            DataTable dt = ECSSArchiveDal.GetScoreForUnit(unitid, ecweb, dtST, dtET);

            //绑定参数
            gvScore.DataSource = dt;
            gvScore.DataBind();
        }

        protected void gvScore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");   

                //报警              
                //string color = ((HtmlInputHidden)e.Row.Cells[0].FindControl("qulity")).Value;

                //if (color != "" && color == "1")
                //{
                //    setColor(e, 0, 10, System.Drawing.Color.FromName("Red"));
                //}

            }


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                GroupRows(gvScore, 0);
            }

        }

        void setColor(GridViewRowEventArgs e, int from, int to, System.Drawing.Color c)
        {
            for (int i = from; i <= to; i++)
            {
                //e.Row.Cells[i].Font.Bold = true;
                //e.Row.Cells[i].ForeColor = c;
                e.Row.BackColor = c;
            }
        }


        /// <summary> 
        /// 合并GridView中某列相同信息的行（单元格） 
        /// </summary> 
        /// <param name="GridView1">GridView</param> 
        /// <param name="cellNum">第几列</param>
        public static void GroupRows(GridView GridView1, int cellNum)
        {
            int i = 0, rowSpanNum = 1;
            while (i < GridView1.Rows.Count - 1)
            {
                GridViewRow gvr = GridView1.Rows[i];
                for (++i; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gvrNext = GridView1.Rows[i];
                    if (gvr.Cells[cellNum].Text == gvrNext.Cells[cellNum].Text)
                    {
                        gvrNext.Cells[cellNum].Visible = false;
                        rowSpanNum++;
                    }
                    else
                    {
                        gvr.Cells[cellNum].RowSpan = rowSpanNum;
                        rowSpanNum = 1;
                        break;
                    }
                    if (i == GridView1.Rows.Count - 1)
                    {
                        gvr.Cells[cellNum].RowSpan = rowSpanNum;
                    }
                }
            }
        } 

        protected void gvScore_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ECID = e.CommandArgument.ToString();

            if (e.CommandName == "runMore")
            {
                ////判断是否存在
                //if (ECID != "")
                //{
                //    //弹出配置指导卡
                //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "",
                //            "window.open('KPI_ECData.aspx?ECID=" + ECID + "','newwindow','width=800,height=600')", true);
                    
              
                //    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "",
                //    //    "alert('数据库中还未对该参数进行配置指导配置.请联系管理员完善!')", true);
                //}
                //else
                //{
                //}

            }
            else if (e.CommandName == "runTrend")
            {
                //string keyid = e.CommandArgument.ToString();

                ////弹出配置指导
                ////ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "",
                ////        "window.open('DTree.aspx?KeyID=" + keyid + "&admin=" + admin + "','newwindow','width=1000,height=618,top=10,left=200')", true);

            }


        }

        // <summary>
        /// 导出到EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvScore.Rows.Count > 0)
            {
                DocExport docEexport = new DocExport();

                docEexport.Dt = GridViewHelper.GridView2DataTable(gvScore);

                docEexport.Export("SIS系统实时指标分析报表");
            }

            return;
        }

        //protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //更新Gridview
        //    BindValues();

        //}

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            //更新Gridview
            BindValues();

        }

    }


}
