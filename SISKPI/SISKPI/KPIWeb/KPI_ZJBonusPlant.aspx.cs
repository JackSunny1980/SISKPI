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
    public partial class KPI_ZJBonusPlant : System.Web.UI.Page
    {
        private static double dBonus = 0.0;
        private static DataTable dtMoney = null;

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

                //电厂信息
                DataTable dt = KPI_PlantDal.GetPlants("");
                //ddlPlant.Items.Add(new ListItem("全部", "ALL"));
                foreach (DataRow dr in dt.Rows)
                {
                    ddlPlant.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //机组信息
                //dt = KPI_UnitDal.GetUnits("");
                ////ddlUnit.Items.Add(new ListItem("全部", "ALL"));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    ddlUnit.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                //}

                //是否显示单元
                if (Request.QueryString["plantcode"] != null)
                {
                    string plantcode = Request.QueryString["plantcode"].ToString();

                    if (plantcode != "")
                    {
                        string plantid = KPI_PlantDal.GetPlantIDByCode(plantcode);

                        ddlPlant.Visible = false;
                        lblPlant.Visible = false;

                        ddlPlant.SelectedValue = plantid;
                    }
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

                //初始化时间
                txt_Month.Value = DateTime.Now.ToString("yyyy-MM");

                //
                tbxKPIMoney.Text = KPI_SystemDal.GetKPIMoney().ToString();
                                
                BindValues();

            }
        }

        

        /// <summary>
        /// 
        /// </summary>
        void BindValues()
        {
            //判断
            string QueryTime = txt_Month.Value;
            DateTime dtQT = DateTime.Now;
            if (!DateTime.TryParse(QueryTime, out dtQT))
            {
                MessageBox.popupClientMessage(this.Page, "时间选择不正确！", "call();");
                return;
            }

            string QueryMoney = tbxKPIMoney.Text;
            double dQM = 0.0;
            if (!double.TryParse(QueryMoney, out dQM) && dQM <= 0)
            {
                MessageBox.popupClientMessage(this.Page, "奖金金额不正确！", "call();");
                return;
            }

            string plantid = ddlPlant.SelectedValue;

            string ecweb = ViewState["ecweb"].ToString();


            //
            //查询前会处理。在DAL中增加1小时
            
            dtMoney = ECSSArchiveDal.GetBonusForPlant(plantid, ecweb, dtQT, dQM);
            dBonus = dQM;

            //绑定参数
            gvBonus.DataSource = dtMoney;
            gvBonus.DataBind();

        }

        protected void gvBonus_RowDataBound(object sender, GridViewRowEventArgs e)
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
                GroupRows(gvBonus, 0);
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
            while (i < GridView1.Rows.Count-1)
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

        protected void gvBonus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ECID = e.CommandArgument.ToString();

            if (e.CommandName == "runMore")
            {
                //判断是否存在
                if (ECID != "")
                {
                    //弹出配置指导卡
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "",
                    //        "window.open('KPI_SubXZB.aspx?ECID=" + ECID + "','newwindow','width=600,height=500')", true);
                    
              
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "",
                    //    "alert('数据库中还未对该参数进行配置指导配置.请联系管理员完善!')", true);
                }
                else
                {
                     }

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
            if (gvBonus.Rows.Count > 0)
            {
                DocExport docEexport = new DocExport();

                docEexport.Dt = GridViewHelper.GridView2DataTable(gvBonus);

                docEexport.Export("SIS系统实时指标分析报表");
            }

            return;
        }

        //protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //更新Gridview
        //    BindValues();
        //}

        protected void btnCalc_Click(object sender, EventArgs e)
        {
            string QueryMoney = tbxKPIMoney.Text;
            double dYMB = 0.0;

            if (!double.TryParse(QueryMoney, out dYMB)  && dYMB <= 0)
            {
                MessageBox.popupClientMessage(this.Page, "奖金金额不正确！", "call();");
                return;
            }

            foreach (DataRow dr in dtMoney.Rows)
            {
                //''TotalScore, ''ECBonus, '0'MGBonus, ''TotalBonus, '1'TotalSort
				//指标得奖= 指标得分*奖金/总得分。先在要做个判断如果是负分的话，他这个得分就不计入总分，并且其指标得奖也要为0.
                if(dr["ECBonus"] != null)
                {
                    dr["ECBonus"] = (double.Parse(dr["ECBonus"].ToString()) * dYMB / dBonus).ToString("0.000");
                    dr["TotalBonus"] = (double.Parse(dr["TotalBonus"].ToString()) * dYMB / dBonus).ToString("0.000");
                }
            }

            dBonus = dYMB;

            //绑定参数
            gvBonus.DataSource = dtMoney;
            gvBonus.DataBind();

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindValues();
        }
        
    }


}
