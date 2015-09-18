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

namespace SISKPI
{
    /// <summary>
    /// 查询、显示、添加、编辑
    /// 设备信息；
    /// 指标考核基本配置；
    /// </summary>
    public partial class KPI_SeqConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSeq();
            }
        }

        /// <summary>
        ///  //设备信息
        /// </summary>
        void BindSeq()
        {
            //设备信息
            DataTable dtSeq = KPI_SeqDal.GetSearchList("");
            gvSeq.DataSource = dtSeq;
            gvSeq.DataBind();
        }

        #region 设备信息配置

        protected void gvSeq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HtmlInputHidden key = (HtmlInputHidden)e.Row.Cells[0].FindControl("unitid");

                //if ((HtmlInputButton)e.Row.Cells[7].FindControl("stab") != null)
                //{
                //    HtmlInputButton btn = (HtmlInputButton)e.Row.Cells[7].FindControl("stab");
                //    btn.Attributes.Add("onclick", "javascript:UnitStab('" + key.Value + "');");
                //}

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvSeq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (KPI_SeqDal.DeleteSeq(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                    BindSeq();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }
            }

        }

        protected void gvSeq_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSeq.EditIndex = e.NewEditIndex;

            BindSeq();
        }

        protected void gvSeq_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSeq.EditIndex = -1;

            BindSeq();
        }

        protected void gvSeq_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            HtmlInputHidden key = (HtmlInputHidden)gvSeq.Rows[e.RowIndex].Cells[0].FindControl("seqid");

            string sID = key.Value; 
            string sCode = ((TextBox)(gvSeq.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();            
            string sName = ((TextBox)(gvSeq.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            string sDesc = ((TextBox)(gvSeq.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            string sNote = ((TextBox)(gvSeq.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();

            string msg = "";
            if (sName == "" || sCode =="")
            {
                msg += "代码或名称不能为空！\r\n";
            }

            if(!Regex.IsMatch(sCode, "^[A-Za-z0-9]+$"))
            {                
                msg += "代码只能为字母和数字组成！\r\n";
            }

            if (msg != "")
            {
                MessageBox.popupClientMessage(this.Page, msg);
                return;
            }

            //代码是否重复
            if (KPI_SeqDal.SeqCodeExists(sCode, sID))
            {
                MessageBox.popupClientMessage(this.Page, "已存在相同的代码!");
                return;
            }

            //更新
            KPI_SeqEntity ote = new KPI_SeqEntity();
            ote.SeqID = sID;
            ote.SeqCode = sCode;
            ote.SeqName = sName;
            ote.SeqDesc = sDesc;
            ote.SeqNote = sNote;
            ote.SeqModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (KPI_SeqDal.Update(ote))
            {
                MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");

            }

            gvSeq.EditIndex = -1;

            BindSeq();


        }

        protected void btnAddSeq_Click(object sender, EventArgs e)
        {
            int index = KPI_SeqDal.SeqIDCounts();

            string sID = PageControl.GetGuid();

            KPI_SeqEntity ote = new KPI_SeqEntity();
            ote.SeqID = sID;
            ote.SeqCode = "InputCode";
            ote.SeqName = "Input Name";
            ote.SeqDesc = "";
            ote.SeqIsValid = 1;
            ote.SeqNote = "";

            ote.SeqCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            ote.SeqModifyTime = ote.SeqCreateTime;

            if (KPI_SeqDal.Insert(ote))
            {
                //MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");

            }

            gvSeq.EditIndex = index;

            BindSeq();

        }

        #endregion

    }
}
