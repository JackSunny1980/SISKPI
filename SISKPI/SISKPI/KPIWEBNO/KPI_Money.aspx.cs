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
    public partial class KPI_Money : System.Web.UI.Page
    {
        public static int nPIC = 0;

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
                
                //初始化时间
                //if (DateTime.Now.Hour <= 1)
                //{
                //    txt_Month.Value = DateTime.Now.AddDays(-1).ToString("yyyy-MM");
                //}
                //else
                //{
                //    txt_Month.Value = DateTime.Now.ToString("yyyy-MM");
                //}

                //获得参数集合
                if (Request.QueryString["ecweb"] != null)
                {
                    ViewState["ecweb"] = Request.QueryString["ecweb"].ToString();
                }
                else
                {
                    ViewState["ecweb"] = "";
                }

                nPIC = int.Parse(System.Configuration.ConfigurationManager.AppSettings["IsTest"].ToString());
                if (nPIC == 1)
                {
                    BindSampleValues();
                }
                else
                {
                    BindValues();
                }

                BindLabel();
            }
        }

        void BindLabel()
        {
            if (nPIC == 1)
            {
                //nowtime.Text = "当前时间：" + GetWeek(DateTime.Now);
                nowtime.Text = "当前时间：" + DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:00");

                //更新值别
                nowshift.Text = "当前值班：" + "一值";
            }

        }

        /// <summary>
        /// 
        /// </summary>
        void BindValues()
        {
            //需要显示的集合
            //string WebID = ViewState["webid"].ToString();

            //if (WebID.Equals(""))
            //{
            //    return;
            //}
            

            //选择的时间
            //DateTime SelectDate = DateTime.Parse(txt_Month.Value);

            ////查询的时间,今天为
            //string ValueDate = "";
            //string ValueLMonth = "";
            //string ValueSMonth = "";

            //if (SelectDate.Year == DateTime.Now.Year && SelectDate.Month == DateTime.Now.Month)
            //{
            //    //本月今日
            //    ValueDate = DateTime.Now.ToString("yyyy-MM-dd");

            //    //今年上月
            //    ValueLMonth = DateTime.Parse(ValueDate).AddDays(-DateTime.Parse(ValueDate).Day).ToString("yyyy-MM-dd");

            //    //去年同月
            //    ValueSMonth = DateTime.Parse(ValueDate).AddDays(-DateTime.Parse(ValueDate).Day).AddMonths(-11).ToString("yyyy-MM-dd");
            //}
            //else
            //    ////if (SelectDate.Year < DateTime.Now.Year 
            //    ////|| (SelectDate.Year == DateTime.Now.Year && SelectDate.Month < DateTime.Now.Month))
            //{
            //    //历史月最后一天
            //    ValueDate = SelectDate.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

            //    //今年上月
            //    ValueLMonth = DateTime.Parse(ValueDate).AddMonths(-1).ToString("yyyy-MM-dd");

            //    //去年同月
            //    ValueSMonth = DateTime.Parse(ValueDate).AddYears(-1).ToString("yyyy-MM-dd");
            //}
            ////else
            ////{

            ////    MessageBox.popupClientMessage(this.Page, "实时库连接错误!", "call();");

            ////    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "",
            ////    //    "alert('月份选择错误！请重新选择！')", true);

            ////    return;
            ////}
            
            ////本月
            //DataTable dt = Ana_ParamDal.GetKPIListForValue(WebID, ValueDate);

            ////今年上月
            //DataTable dtL = Ana_ParamDal.GetHistoryValue(WebID, ValueLMonth);    

            ////去年同月
            //DataTable dtS = Ana_ParamDal.GetHistoryValue(WebID, ValueSMonth);

            ////            @"select a.KeyID, c.UnitDesc, a.KeyName, a.KeyEngunit,  
            ////            ValueDate, ''BKColor, ''BKGood,                            
            ////            ValueMonthRMW, ValueMonthR, 
            ////            ''ValueLMonthRMW, ''ValueLMonthR, ''ValueLMonthD, ''ValueLMonthB, 
            ////            ''ValueSMonthRMW, ''ValueSMonthR, ''ValueSMonthD, ''ValueSMonthB, 
            ////            KeyIsCalc
            ////            from Ana_Param a, Ana_Value b,OPM_Unit c
            ////            where a.KeyID=b.KeyID and a.UnitID=c.UnitID and KeyIsValid=1 and a.WebID='{0}' and ValueDate='{1}'
            ////            order by KeyIndex ";
            
            string ValueDate = DateTime.Now.ToString();

            DataTable dt = ECSSSnapshotDal.GetSearchList("", ValueDate);

            ////绑定实时参数 及 计算参数
            foreach (DataRow dr in dt.Rows)
            {
                string keyid = dr["KeyID"].ToString();

                //是否计算煤耗
                int calc = int.Parse(dr["KeyIsCalc"].ToString());
                double valueb = double.Parse(dr["XLineType1Unit"].ToString());

                //今年上月   
                //DataRow[] drl = dtL.Select("KeyID='" + keyid + "'");
                //if (drl.Length == 1)
                //{
                //    dr["ValueLMonthR"] = drl[0]["ValueMonthR"];
                //}
                
                ////去年同月
                //DataRow[] drs = dtS.Select("KeyID='" + keyid + "'");
                //if (drs.Length == 1)
                //{
                //    dr["ValueSMonthR"] = drs[0]["ValueMonthR"];
                //}

                //偏差及煤耗计算
                //if (dr["ValueLMonthR"].ToString() != "" && dr["ValueMonthR"].ToString() != "")
                //{
                //    double lmonthd = double.Parse(dr["ValueMonthR"].ToString()) - double.Parse(dr["ValueLMonthR"].ToString());

                //    if (lmonthd != double.MinValue) dr["ValueLMonthD"] = lmonthd.ToString("0.000");
                    
                //    if (calc == 1)
                //    {
                //        double lmonthb = lmonthd * valueb;
                //        if (lmonthb != double.MinValue) dr["ValueLMonthB"] = lmonthb.ToString("0.000");
                //    }

                //}

                //if (dr["ValueSMonthR"].ToString() != "" && dr["ValueMonthR"].ToString() != "")
                //{
                //    double smonthd = double.Parse(dr["ValueMonthR"].ToString()) - double.Parse(dr["ValueSMonthR"].ToString());
           
                //    if (smonthd != double.MinValue) dr["ValueSMonthD"] = smonthd.ToString("0.000");
                //    if (calc == 1)
                //    {
                //        double smonthb = smonthd * valueb;
                //        if (smonthb != double.MinValue) dr["ValueSMonthB"] = smonthb.ToString("0.000");
                //    }

                //}           

                //赋值

            }

            #region  DataTable排序方法
            //
            //dt.Columns.Add("SortValue", typeof(Double));

            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (dr["KeyBValue"].ToString() == "--")
            //    {
            //        dr["SortValue"] = 0;
            //    }
            //    else
            //    {
            //        dr["SortValue"] = Convert.ToDouble(dr["KeyBValue"].ToString());
            //    }
            //}

            ////
            //dt.DefaultView.Sort = "KeyIsMan, SortValue desc";

            #endregion

            //绑定最优参数
            gvSnapshot.DataSource = dt;
            gvSnapshot.DataBind();

        }

        /// <summary>
        /// 
        /// </summary>
        void BindSampleValues()
        {
            //string ECWeb = ViewState["ecweb"].ToString();

            //if (ECWeb.Equals(""))
            //{
            //    return;
            //}
            
            //绑定参数
            DataTable dt = KPI_PersonDal.GetSearchList("");


            //
//"select PersonID, PersonCode, PersonName, ShiftName, PositionName, PositionWeight, 
//                            ''ECScore, ''SASCore, ''AllScore, ''PersonSort
//                            from KPI_Person  a
            dt.Clear();

            DataRow dr = dt.NewRow();
            dr["PersonID"] = "1111";
            dr["ShiftName"] = "一值";
            dr["ECScore"] = "1560";
            dr["SAScore"] = "2458";
            dr["AllScore"] = "4018";
            dr["PersonSort"] = "1";
            dr["PersonMoney"] = "11000";

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["PersonID"] = "1111";
            dr["ShiftName"] = "二值";
            dr["ECScore"] = "1540";
            dr["SASCore"] = "2438";
            dr["AllScore"] = "3978";
            dr["PersonSort"] = "2";
            dr["PersonMoney"] = "7000";

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["PersonID"] = "1111";
            dr["ShiftName"] = "五值";
            dr["ECScore"] = "1240";
            dr["SASCore"] = "2338";
            dr["AllScore"] = "3578";
            dr["PersonSort"] = "3";
            dr["PersonMoney"] = "6000";

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["PersonID"] = "1111";
            dr["ShiftName"] = "四值";
            dr["ECScore"] = "1240";
            dr["SASCore"] = "2338";
            dr["AllScore"] = "3578";
            dr["PersonSort"] = "3";
            dr["PersonMoney"] = "4000";

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["PersonID"] = "1111";
            dr["ShiftName"] = "三值";
            dr["ECScore"] = "1240";
            dr["SASCore"] = "2338";
            dr["AllScore"] = "3578";
            dr["PersonSort"] = "3";
            dr["PersonMoney"] = "2000";

            dt.Rows.Add(dr);
           

            //绑定最优参数
            gvSnapshot.DataSource = dt;

            gvSnapshot.DataBind();

        }



        protected void gvSnapshot_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ////是否上升、降低    
                ////上月耗差            
                //string lmonthb = ((HtmlInputHidden)e.Row.Cells[7].FindControl("lmonthb")).Value;

                //if ((ImageButton)e.Row.Cells[7].FindControl("lb_lmonth") != null)
                //{
                //    ImageButton lb = (ImageButton)e.Row.Cells[7].FindControl("lb_lmonth");

                //    if (lmonthb == "")
                //    {
                //        lb.Visible = false;
                //    }
                //    else
                //    {
                //        lb.Visible = true;
                //        if (double.Parse(lmonthb) > 0) lb.ImageUrl = "../imgs/upred.gif";
                //        if (double.Parse(lmonthb) == 0) lb.ImageUrl = "../imgs/none.gif";
                //        if (double.Parse(lmonthb) < 0) lb.ImageUrl = "../imgs/downgreen.gif";
                //    }
                //}

                ////同月耗差
                //string smonthb = ((HtmlInputHidden)e.Row.Cells[10].FindControl("smonthb")).Value;


                //if ((ImageButton)e.Row.Cells[10].FindControl("lb_smonth") != null)
                //{
                //    ImageButton lb = (ImageButton)e.Row.Cells[10].FindControl("lb_smonth");

                //    if (smonthb == "")
                //    {
                //        lb.Visible = false;
                //    }
                //    else
                //    {
                //        lb.Visible = true;
                //        if (double.Parse(smonthb) > 0) lb.ImageUrl = "../imgs/upred.gif";
                //        if (double.Parse(smonthb) == 0) lb.ImageUrl = "../imgs/none.gif";
                //        if (double.Parse(smonthb) < 0) lb.ImageUrl = "../imgs/downgreen.gif";
                //    }
                //}
                


                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");   

                //偏差较大的数据标记颜色                
                //string color = ((HtmlInputHidden)e.Row.Cells[0].FindControl("color")).Value;

                //if (color != "")
                //    setColor(e, 0, 10, System.Drawing.Color.FromName(color));

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

        protected void gvSnapshot_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "runGuid")
            {
                string[] estr = e.CommandArgument.ToString().Split(',');
                string keyid = Convert.ToString(estr[0]);
                string strreal = Convert.ToString(estr[1]);
                string stropt = Convert.ToString(estr[2]);
                string strdif = Convert.ToString(estr[3]);
                string strper = Convert.ToString(estr[4].TrimEnd('%'));

                //double percent = double.Parse(ddl_Percent.Text);

                ////有些参数偏大好，有些参数偏小好
                //if (Math.Abs(double.Parse(stropt)) < 0.00001)
                //{
                //    //0除数
                //    if (Math.Abs(double.Parse(strdif)) < 1)
                //    {
                //        //MessageBox.popupClientMessage(this.Page, "优化值在偏差范围内，无运行指导！", "call();");
                //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "",
                //        "alert('优化值在偏差范围内，无运行指导！')", true);
                        
                //        return;
                //    }
                //}
                //else
                //{
                //    if (double.Parse(strper) < percent)
                //    {
                //        //MessageBox.popupClientMessage(this.Page, "优化值在偏差范围内，无运行指导！", "call();");
                //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "",
                //        "alert('优化值在偏差范围内，无运行指导！')", true);

                //        return;
                //    }
                //}

                //string MatchCate = "-1";
                //if (double.Parse(strdif) > 0)
                //{
                //    //偏大
                //    MatchCate = "1";                    
                //    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", 
                //    //    "window.open('Guid_Pop.aspx','newwindow','width=600,height=600')", true);
                //    //Microsoft.Web.UI.ScriptManager.RegisterStartupScript(p1, this.GetType(), "click", "<script language=javascript>window.open('Guid_Pop.aspx','newwindow','width=600,height=600')</script>");
              
                //}
                //else
                //{
                //    //偏小
                //    MatchCate = "0";
                //    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('偏小')", true);
              
                //}

                //判断是否存在
                //if (!Guid_MatchRDDal.ParamIDExists(keyid, MatchCate.ToString()))
                //{
                //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "",
                //        "alert('数据库中还未对该参数进行配置指导配置.请联系管理员完善!')", true);
                //}
                //else
                //{
                //    //弹出配置指导卡
                //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "",
                //            "window.open('Guid_SubCard.aspx?KeyID=" + keyid + "&Real=" + strreal + "&Opt=" + stropt + "','newwindow','width=600,height=500')", true);
                ////}

            }
            else if (e.CommandName == "runTrend")
            {
                //string keyid = e.CommandArgument.ToString();

                ////弹出配置指导
                ////ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "",
                ////        "window.open('DTree.aspx?KeyID=" + keyid + "&admin=" + admin + "','newwindow','width=1000,height=618,top=10,left=200')", true);
                //int aheight = int.Parse(saheight.Value);
                //int awidth = int.Parse(sawidth.Value);

                //int height = 600;
                //int width = 800;
                //if (aheight > 600)
                //{
                //    height = 600;
                //}
                //else
                //{
                //    height = aheight;
                //}

                //if (awidth > 800)
                //{
                //    width = 800;
                //}
                //else
                //{
                //    width = awidth;
                //}


                //int atop = (aheight - height) / 2;
                //int aleft = (awidth - width) / 2;

                //string sparam = "height=" + height.ToString() + ",width=" + width.ToString() + ",top=" + atop.ToString() + ",left=" + aleft.ToString();

                //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "",
                //        "window.open('DTrend.aspx?tags=" + keyid + "','newwindow','" + sparam + "');", true);

                //fullscreen=1

            }


        }

        protected string GetWeek(DateTime dtTime)
        {
            //获取当前日期是星期几
            string dtWeek = dtTime.DayOfWeek.ToString();
            string week = "星期一";

            //根据取得的英文单词返回汉字
            switch (dtWeek)
            {
                case "Monday":
                    week = "星期一";
                    break;
                case "Tuesday":
                    week = "星期二";
                    break;
                case "Wednesday":
                    week = "星期三";
                    break;
                case "Thursday":
                    week = "星期四";
                    break;
                case "Friday":
                    week = "星期五";
                    break;
                case "Saturday":
                    week = "星期六";
                    break;
                case "Sunday":
                    week = "星期日";
                    break;

            }

            //返回星期几
            week = dtTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:00, ") + week;
            return week;

        }
        
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //更新时间和运行值
            BindLabel();

            //更新Gridview
            //BindValues();
        }


        // <summary>
        /// 导出到EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvSnapshot.Rows.Count > 0)
            {
                DocExport docEexport = new DocExport();

                docEexport.Dt = GridViewHelper.GridView2DataTable(gvSnapshot);

                docEexport.Export("SIS系统实时指标分析报表");
            }

            return;
        }

    }


}
