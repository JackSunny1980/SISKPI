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
    public partial class KPI_ZJRacePlant : System.Web.UI.Page
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

            //
            //查询前会处理。在DAL中增加1小时

            string plantid = ddlPlant.SelectedValue;
            
            string ecweb = ViewState["ecweb"].ToString();

            DataTable dtResult = ECSSArchiveDal.GetRaceForPlant(plantid, ecweb, dtQT);

            //绑定参数
            gvBonus.DataSource = dtResult;

            gvBonus.DataBind();

        }
        
        protected void gvBonus_RowCreated(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    //第一行表头
                    TableCellCollection tcHeader = e.Row.Cells;
                    tcHeader.Clear();

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[0].Attributes.Add("rowspan", "2"); //跨Row
                    tcHeader[0].Attributes.Add("bgcolor", "LightSteelBlue");
                    tcHeader[0].Text = "值别";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[1].Attributes.Add("rowspan", "2"); //跨Row
                    tcHeader[1].Attributes.Add("bgcolor", "Tomato");
                    tcHeader[1].Text = "机组";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[2].Attributes.Add("bgcolor", "DarkSeaGreen");
                    tcHeader[2].Attributes.Add("colspan", "2");
                    tcHeader[2].Text = "分数";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[3].Attributes.Add("bgcolor", "DarkSeaGreen");
                    tcHeader[3].Attributes.Add("colspan", "2");
                    tcHeader[3].Text = "名次";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[4].Attributes.Add("bgcolor", "DarkSeaGreen");
                    tcHeader[4].Attributes.Add("colspan", "2");
                    tcHeader[4].Text = "总分";
                    
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[5].Attributes.Add("bgcolor", "DarkSeaGreen");
                    tcHeader[5].Attributes.Add("colspan", "2");
                    tcHeader[5].Text = "名次</th></tr><tr>";
                    

                    //第二行表头
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[6].Attributes.Add("bgcolor", "Khaki");
                    tcHeader[6].Text = "本月";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[7].Attributes.Add("bgcolor", "Khaki");
                    tcHeader[7].Text = "本年";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[8].Attributes.Add("bgcolor", "Khaki");
                    tcHeader[8].Text = "本月";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[9].Attributes.Add("bgcolor", "Khaki");
                    tcHeader[9].Text = "本年";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[10].Attributes.Add("bgcolor", "Khaki");
                    tcHeader[10].Text = "本月";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[11].Attributes.Add("bgcolor", "Khaki");
                    tcHeader[11].Text = "本年";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[12].Attributes.Add("bgcolor", "Khaki");
                    tcHeader[12].Text = "本月";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[13].Attributes.Add("bgcolor", "Khaki");
                    tcHeader[13].Text = "本年";
                    
                    break;
            }
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
                GroupRows(gvBonus, 6);
                GroupRows(gvBonus, 7);
                GroupRows(gvBonus, 8);
                GroupRows(gvBonus, 9);
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

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindValues();
        }

        
    }


}
