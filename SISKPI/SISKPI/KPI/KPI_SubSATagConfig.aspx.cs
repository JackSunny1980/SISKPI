using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using SIS.DataAccess;
using SIS.Assistant;
using SIS.DataEntity;
using SIS.Loger;
using SIS.Assistant.WS;
using SIS.DBControl;

namespace SISKPI
{
    public partial class KPI_SubSATagConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility='';progress_update();");
                            
                //机组信息
                DataTable dt = KPI_UnitDal.GetUnits("");
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_UnitID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //设备信息
                dt = KPI_SeqDal.GetSeqs();
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_SeqID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //指标信息
                dt = KpiDal.GetKpis();
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_KpiID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //单位信息
                dt = EngunitDal.GetEngunits();
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_EngunitID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //周期信息
                dt = CycleDal.GetCycles();
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_CycleID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //网页集信息
                //信息在KPI_Constant表中
                dt = KPI_ConstantDal.GetSAWebs();
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_SAWeb.Items.Add(new ListItem(dr["Name"].ToString(), dr["Value"].ToString()));
                }

                //判断是否新建或编辑
                if (Request.QueryString["said"] != null)
                {
                    ViewState["said"] = Request.QueryString["said"].ToString();

                    BindValues(); 
                }
                else
                {
                    //添加
                    ViewState["said"] = "";

                    //
                    ddl_SAIsValid.Value = "1";
                    ddl_SAIsCalc.Value = "1";
                    ddl_SAIsDisplay.Value = "1";
                    ddl_SAIsTotal.Value = "1";

                    SetSACodeAndName();

                    txt_SAIndex.Value = "100";
                }
              
                btnApply.Visible = true;
            }
            
        }



        void BindSecurity()
        {
            string SAID = ViewState["said"].ToString();

            if (SAID == "")
            {
                //btnAddSecurity.Enabled = false;

                return;
            }

            DataTable dt = KPI_SecurityDal.GetSecurityList(SAID);

            //gvSecurity.DataSource = dt;

            //gvSecurity.DataBind();
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindValues()
        {
            string SAID = ViewState["said"].ToString();

            if (SAID == "")
            {
                return;
            }

            KPI_SATagEntity mEntity = KPI_SATagDal.GetEntity(SAID);

            ddl_UnitID.SelectedValue = mEntity.UnitID;
            ddl_SeqID.Value = mEntity.SeqID;
            ddl_KpiID.SelectedValue = mEntity.KpiID;
            ddl_EngunitID.Value = mEntity.EngunitID;
            ddl_CycleID.Value = mEntity.CycleID;

            ddl_SAIsValid.Value = mEntity.SAIsValid.ToString();
            ddl_SAIsCalc.Value = mEntity.SAIsCalc.ToString();
            ddl_SAIsDisplay.Value = mEntity.SAIsDisplay.ToString();
            ddl_SAIsTotal.Value = mEntity.SAIsTotal.ToString();
            ddl_SAWeb.Value = mEntity.SAWeb;

            txt_SACode.Value = mEntity.SACode;
            txt_SAName.Value = mEntity.SAName;
            txt_SADesc.Value = mEntity.SADesc;
            txt_SAIndex.Value = mEntity.SAIndex.ToString();

            //tbx_SAFilterExp.Text = mEntity.SAFilterExp;
            tbx_SACalcExp.Text = mEntity.SACalcExp;

            txt_SANote.Value = mEntity.SANote;
            txtCountExpression.Text = mEntity.SACountExpression;
            txtDurationExpression.Text = mEntity.SADurationExpression;

            //BindSecurity();

        }

        /// <summary>
        /// 页面信息检查
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CheckVal(out string msg)
        {
            bool flag=false;
            msg = "";

            string sID =  ViewState["said"].ToString();

            txt_SACode.Value = txt_SACode.Value.Trim();
            txt_SAName.Value = txt_SAName.Value.Trim();

            /////////////////////////////////////////////////////////////////////
            //
            if (txt_SACode.Value.Equals(""))
            {
                msg += "代码不能为空！\r\n";
                flag = true;
                return flag;
            }
            else
            {
                if (!Regex.IsMatch(txt_SACode.Value, @"^\w+$"))                    
                {
                    msg += "代码只能由数字、字母和下划线组成！\r\n";
                    flag = true;
                    return flag;
                }

                if (string.IsNullOrWhiteSpace(sID))
                {
                    if (KPI_SATagDal.CodeExist(txt_SACode.Value, sID)
                   || ALLDal.CodeExist(txt_SACode.Value, ""))
                    {
                        msg += "命名已存在，请检查！\r\n";
                        flag = true;
                        return flag;
                    }
                }
                else 
                {
                    
                    if (KPI_SATagDal.CodeExist(txt_SACode.Value, sID)
                        || ALLDal.CodeExist(txt_SACode.Value, sID))
                    {
                        msg += "命名已存在，请检查！\r\n";
                        flag = true;
                        return flag;
                    }
                }
                

            }

            if (txt_SAName.Value.Equals(""))
            {
                msg += "名称不能为空！\r\n";
                flag = true;
                return flag;
            }


            return flag;
        }
                       
  


        #region 【插入、编辑方法】

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <returns></returns>
        bool Insert(string said)
        {
            KPI_SATagEntity mEntity = new KPI_SATagEntity();

            mEntity.SAID = said;
            mEntity.UnitID = ddl_UnitID.SelectedValue;
            mEntity.SeqID = ddl_SeqID.Value.Trim();
            mEntity.KpiID = ddl_KpiID.SelectedValue;
            mEntity.EngunitID = ddl_EngunitID.Value.Trim();
            mEntity.CycleID = ddl_CycleID.Value.Trim();

            mEntity.SAIsValid = int.Parse(ddl_SAIsValid.Value);
            mEntity.SAIsCalc = int.Parse(ddl_SAIsCalc.Value);
            mEntity.SAIsDisplay = int.Parse(ddl_SAIsDisplay.Value);
            mEntity.SAIsTotal = int.Parse(ddl_SAIsTotal.Value);

            mEntity.SACode = txt_SACode.Value.Trim();
            mEntity.SAName = txt_SAName.Value.Trim();
            mEntity.SADesc = txt_SADesc.Value.Trim();
            mEntity.SAIndex = int.Parse(txt_SAIndex.Value.Trim());
            mEntity.SAWeb = ddl_SAWeb.Value;

            //mEntity.SAFilterExp = tbx_SAFilterExp.Text;
            mEntity.SACalcExp = tbx_SACalcExp.Text;

            //
            mEntity.SANote = txt_SANote.Value.Trim();
            mEntity.SACreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            mEntity.SAModifyTime = mEntity.SACreateTime;
            mEntity.SACountExpression = txtCountExpression.Text.Trim();
            mEntity.SADurationExpression = txtDurationExpression.Text.Trim();


            return KPI_SATagDal.Insert(mEntity);
        }


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        bool Update()
        {
            string said = ViewState["said"].ToString();

            KPI_SATagEntity mEntity = new KPI_SATagEntity();

            mEntity.SAID = said;
            mEntity.UnitID = ddl_UnitID.SelectedValue;
            mEntity.SeqID = ddl_SeqID.Value.Trim();
            mEntity.KpiID = ddl_KpiID.SelectedValue;
            mEntity.EngunitID = ddl_EngunitID.Value.Trim();
            mEntity.CycleID = ddl_CycleID.Value.Trim();

            mEntity.SAIsValid = int.Parse(ddl_SAIsValid.Value);
            mEntity.SAIsCalc = int.Parse(ddl_SAIsCalc.Value);
            mEntity.SAIsDisplay = int.Parse(ddl_SAIsDisplay.Value);
            mEntity.SAIsTotal = int.Parse(ddl_SAIsTotal.Value);

            mEntity.SACode = txt_SACode.Value.Trim();
            mEntity.SAName = txt_SAName.Value.Trim();
            mEntity.SADesc = txt_SADesc.Value.Trim();
            mEntity.SAIndex = int.Parse(txt_SAIndex.Value.Trim());
            mEntity.SAWeb = ddl_SAWeb.Value;


            //mEntity.SAFilterExp = tbx_SAFilterExp.Text;
            mEntity.SACalcExp = tbx_SACalcExp.Text;

            //

            mEntity.SANote = txt_SANote.Value.Trim();
            mEntity.SACountExpression = txtCountExpression.Text.Trim();
            mEntity.SADurationExpression = txtDurationExpression.Text.Trim();
            //mEntity.SACreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            mEntity.SAModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");


            return KPI_SATagDal.Update(mEntity);
        }
       
        #endregion

        #region 安全区间表配置

        protected void gvSecurity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                DataRowView drv = (DataRowView)e.Row.DataItem;

                if ((DropDownList)e.Row.FindControl("ddlOptimal") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlOptimal");

                    ddlCollege.SelectedValue = drv["SecurityOptimal"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlAlarm") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlAlarm");

                    ddlCollege.SelectedValue = drv["SecurityAlarm"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlValid") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlValid");

                    ddlCollege.SelectedValue = drv["SecurityIsValid"].ToString();
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvSecurity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string[] keyvalue = e.CommandArgument.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string keyid = e.CommandArgument.ToString();

            //string keyid = "";
            //string keytype = "";
            //if (keyvalue.Length == 2)
            //{
            //    keyid = keyvalue[0];
            //    keytype = keyvalue[1];
            //}

            if (e.CommandName == "dataDelete")
            {
                if (KPI_SecurityDal.DeleteSecurity(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }
            }
            else if (e.CommandName == "dataTest")
            {
                //

            }

        }


        protected void gvSecurity_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //gvSecurity.EditIndex = -1;

            BindSecurity();

        }

        protected void gvSecurity_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //gvSecurity.EditIndex = e.NewEditIndex;

            BindSecurity();

        }

        protected void gvSecurity_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //string sID = ((HtmlInputHidden)(gvSecurity.Rows[e.RowIndex].Cells[0].FindControl("securityid"))).Value.ToString().Trim();

            //string sCalc = ((TextBox)(gvSecurity.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            //string sGain = ((TextBox)(gvSecurity.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();

            //string sOptimal = ((DropDownList)(gvSecurity.Rows[e.RowIndex].Cells[4].FindControl("ddlOptimal"))).SelectedValue;
            //string sAlarm = ((DropDownList)(gvSecurity.Rows[e.RowIndex].Cells[5].FindControl("ddlAlarm"))).SelectedValue;
            //string sValid = ((DropDownList)(gvSecurity.Rows[e.RowIndex].Cells[6].FindControl("ddlValid"))).SelectedValue;

            //string sNote = ((TextBox)(gvSecurity.Rows[e.RowIndex].Cells[7].Controls[0])).Text.ToString().Trim();

            ////更新
            //KPI_SecurityEntity ote = new KPI_SecurityEntity();
            //ote.SecurityID = sID;
            //ote.SecurityCalcExp = sCalc;
            //ote.SecurityGainExp = sGain;

            //ote.SecurityOptimal = int.Parse(sOptimal);
            //ote.SecurityAlarm = int.Parse(sAlarm);
            //ote.SecurityIsValid = int.Parse(sValid);
            //ote.SecurityNote = sNote;

            //ote.SecurityModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            //if (KPI_SecurityDal.Update(ote))
            //{
            //    MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

            //}
            //else
            //{
            //    MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
            //}

            //gvSecurity.EditIndex = -1;

            BindSecurity();

        }



        protected void btnAddSecurity_Click(object sender, EventArgs e)
        {
            string said = ViewState["said"].ToString();

            if (said == "")
            {
                //btnAddSecurity.Enabled = false;
                
                MessageBox.popupClientMessage(this.Page, "请先点击应用按钮生成新的安全指标！", "call();");

                return;
            }

            int index = KPI_SecurityDal.SecurityIDCounts(said);

            KPI_SecurityEntity ote = new KPI_SecurityEntity();

            ote.SecurityID = PageControl.GetGuid();
            ote.SAID = said;
            ote.SecurityCalcExp = "Input Calc Exp";
            ote.SecurityGainExp = "1";

            ote.SecurityOptimal = 0;
            ote.SecurityAlarm = 0;
            ote.SecurityIsValid = 1;

            //ote.SecurityNote = "";
            //ote.SecurityCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            //ote.SecurityModifyTime = ote.SecurityCreateTime;

            //if (KPI_SecurityDal.Insert(ote))
            //{
            //    //MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");

            //    gvSecurity.EditIndex = index;

            //    BindSecurity();
            //}
            //else
            //{
            //    MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");

            //}
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
            string msg = "";
            if (CheckVal(out msg))
            {
                MessageBox.popupClientMessage(this.Page, msg, "call();");
                return;
            }

            if (ViewState["said"].ToString() == "")
            {
                string said = PageControl.GetGuid();
                if (Insert(said))
                {
                    ViewState["said"] = said;

                    MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");
                    //btnAddSecurity.Enabled = true;
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");
                }
            }
            else
            {
                if (Update())
                {
                    MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");
                    //btnAddSecurity.Enabled = true;
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
                }
            }
        }

 
        #endregion
        
       protected void btnReturn_Click(object sender, EventArgs e)
       {
           Response.Redirect("KPI_SATagConfig.aspx");
       }

       protected void ddl_UnitID_SelectedIndexChanged(object sender, EventArgs e)
       {
           SetSACodeAndName();
       }

       protected void ddl_KpiID_SelectedIndexChanged(object sender, EventArgs e)
       {
           SetSACodeAndName();
       }

       void SetSACodeAndName()
       {
           string unitcode = KPI_UnitDal.GetUnitCode(ddl_UnitID.SelectedValue);
           string kpicode = KpiDal.GetKpiCode(ddl_KpiID.SelectedValue);

           txt_SACode.Value = unitcode+"_" + kpicode + "_";
           txt_SAName.Value = ddl_UnitID.SelectedItem.Text + ddl_KpiID.SelectedItem.Text;
       }
        
    }
}
