using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

using SIS.DataAccess;
using SIS.DataEntity;
using SIS.Assistant;
using SIS.Loger;
using SIS.Assistant.WS;
using SIS.DBControl;

namespace SISKPI
{
    public partial class KPI_SubECTagConfig2 : System.Web.UI.Page
    {
        public static string eccalcexp = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility='';progress_update();");
                            
                //机组信息
                //DataTable dt = KPI_UnitDal.GetUnits("");
                //foreach (DataRow dr in dt.Rows)
                //{
                //    ddl_UnitID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                //}


                //判断是否新建或编辑
                if (Request.QueryString["ecid"] != null)
                {
                    ViewState["ecid"] = Request.QueryString["ecid"].ToString();

                    btnApply.Enabled = true;

                    BindValues();

                }
                else
                {
                    //添加
                    ViewState["ecid"] = "";

                    btnApply.Enabled = false;
                    
                    //
                }

                //
                BindListBox();
            }
        }


        void BindListBox()
        {
            string ecid = ViewState["ecid"].ToString();

            DataTable dt = KPI_RealTagDal.GetTAGs();
            lbx_TAG.DataSource = dt;
            lbx_TAG.DataTextField = "Name";
            lbx_TAG.DataValueField = "Code";
            lbx_TAG.DataBind();


            dt = ECTagDal.GetKPIs(ecid);
            lbx_KPI.DataSource = dt;
            lbx_KPI.DataTextField = "Name";
            lbx_KPI.DataValueField = "Code";
            lbx_KPI.DataBind();
			ExpDone Parser = new ExpDone();
			Dictionary<String, String> dic = Parser.CustomFunctionsListing();
            ddlCustomFunction.Items.Add(new ListItem("请选择", "NULL"));

            foreach (KeyValuePair<String, String> kvp in dic)
            {
                ddlCustomFunction.Items.Add(new  ListItem(kvp.Value, kvp.Key));
            }
            
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindValues()
        {
            string ECID = ViewState["ecid"].ToString();

            ECTagEntity mEntity = ECTagDal.GetEntity(ECID);

            lbl_ECCode.Text = "指标代码：" + mEntity.ECCode;
            lbl_ECName.Text = "指标名称：" + mEntity.ECName;

            //
            tbx_ECCalcExp.Text = mEntity.ECCalcExp;
            tbx_ECCalcDesc.Text = mEntity.ECCalcDesc;           
           

        }

        #region 【参数验证】

        /// <summary>
        /// 页面信息检查
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CheckVal(out string msg)
        {
            bool flag=false;
            msg = "";

            string sexp = tbx_ECCalcExp.Text;

            //if (txt_ECWeight.Value.Equals(""))
            //{
            //    msg += "权重不能为空！\r\n";
            //    flag = true;
            //    return flag;
            //}

            return flag;
        }
                       
        
        #endregion


        #region 【插入、编辑、绑定方法】

        string msg = "";



        /// <summary>
        /// 插入数据
        /// </summary>
        /// <returns></returns>
        bool Insert(string ecid)
        {
            //KPI_ECTagEntity mEntity = new KPI_ECTagEntity();

            //mEntity.ECID = ecid;
            //mEntity.ECCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            //mEntity.ECModifyTime = mEntity.ECCreateTime;

            //return KPI_ECTagDal.Insert(mEntity);
            return true;
        }


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        bool Update()
        {
            string ecid = ViewState["ecid"].ToString();

            ECTagEntity mEntity = new ECTagEntity();

            mEntity.ECID = ecid;
            mEntity.ECCalcExp = tbx_ECCalcExp.Text;//.Replace("'", "''");
            mEntity.ECCalcDesc = tbx_ECCalcDesc.Text;

            mEntity.ECModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            return ECTagDal.Update(mEntity);
        }
       
        #endregion


        #region 【按钮事件、调用方法】   
   
         
        /// <summary>
        /// 数据提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApply_Click(object sender, EventArgs e)
        {           
            if (CheckVal(out msg))
            {
                MessageBox.popupClientMessage(this.Page, msg, "call();");
                return;
            }

            if (ViewState["ecid"].ToString() == "")
            {
                //string ecid = PageControl.GetGuid();
                //if (Insert(ecid))
                //{
                //    ViewState["ecid"] = ecid;

                //    MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");
                //}
                //else
                //{
                //    MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
                //}
            }
            else
            {
                if (Update())
                {
                    MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
                }
            }
        }

       /// <summary>
       /// 取消按钮
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       protected void btnCancel_Click(object sender, EventArgs e)
        {
            string strjs = "<script language=javascript>  window.close();</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", strjs);
       }
 
        #endregion

       protected void btnStep1_Click(object sender, EventArgs e)
       {
           Response.Redirect("KPI_SubECTagConfig1.aspx?ecid=" + ViewState["ecid"].ToString());

       }
       protected void btnStep2_Click(object sender, EventArgs e)
       {
           //当前页不做什么事情。

       }

       protected void btnStep3_Click(object sender, EventArgs e)
       {
           Response.Redirect("KPI_SubECTagConfig3.aspx?ecid=" + ViewState["ecid"].ToString());
       }

       protected void btnStep4_Click(object sender, EventArgs e)
       {
           Response.Redirect("KPI_SubECTagConfig4.aspx?ecid=" + ViewState["ecid"].ToString());
       }

       protected void btnReturn_Click(object sender, EventArgs e)
       {
           Response.Redirect("KPI_ECTagConfig.aspx");
       }

       protected void ddlCustomFunction_SelectedIndexChanged(object sender, EventArgs e)
       {
           if (ddlCustomFunction.SelectedValue != "NULL")
           {
                tbx_ECCalcExp.Text += ddlCustomFunction.SelectedValue;
           }
       }

       protected void lbx_TAG_SelectedIndexChanged(object sender, EventArgs e)
       {
           tbx_ECCalcExp.Text += "'" + lbx_TAG.SelectedValue + "'";

       }

       protected void lbx_KPI_SelectedIndexChanged(object sender, EventArgs e)
       {
           tbx_ECCalcExp.Text += "'" + lbx_KPI.SelectedValue + "'";

       }

       protected void btnExample_Click(object sender, EventArgs e)
       {
           string strjs = "<script language=javascript>window.open('KPI_Calc.htm','newwindow')</script>";

           ClientScript.RegisterStartupScript(this.GetType(), "", strjs);
       }

       protected void btnTest_Click(object sender, EventArgs e)
       {
           eccalcexp = tbx_ECCalcExp.Text;

           string strjs = "<script language=javascript>window.open('KPI_SubECTagTest.aspx','newwindow')</script>";

           ClientScript.RegisterStartupScript(this.GetType(), "", strjs);

       }

       protected void btnSearchTAG_Click(object sender, EventArgs e)
       {
           string tagname = txt_TAG.Value;

            DataTable dt = KPI_RealTagDal.GetTAGsLikeName(tagname);
            lbx_TAG.DataSource = dt;
            lbx_TAG.DataTextField = "Name";
            lbx_TAG.DataValueField = "Code";
            lbx_TAG.DataBind();

       }

       protected void btnSearchKPI_Click(object sender, EventArgs e)
       {
           string ecid = ViewState["ecid"].ToString();
           string name = txt_KPI.Value;

           DataTable dt = ECTagDal.GetKPIs(ecid);
           lbx_KPI.DataSource = dt;
           lbx_KPI.DataTextField = "Name";
           lbx_KPI.DataValueField = "Code";
           lbx_KPI.DataBind();
       }


    }
}
