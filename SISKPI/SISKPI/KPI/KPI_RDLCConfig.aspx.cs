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


namespace SISKPI
{
    public partial class KPI_RDLCConfig : System.Web.UI.Page
    {
        static string WebID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnSync.Visible = true;
                btnAdd.Visible = true;

                //网页信息
                //DataTable dt = KPI_WebDal.GetWebs("");

                //foreach (DataRow dr in dt.Rows)
                //{
                //    ddl_WebID.Items.Add(new ListItem(dr["KeyWebName"].ToString(), dr["KeyWebID"].ToString()));
                //}
                
                ////机组信息
                //if (Request.QueryString["unitid"] != null)
                //{
                //    ddl_UnitID.Value = Request.QueryString["unitid"].ToString();
                //}

                BindWebs();

            }
            

        }

        /// <summary>
        /// 
        /// </summary>
        public void BindWebs()
        {
            //绑定参数
            DataTable dt = KPI_WebDal.GetWebList();
            
            gvWeb.DataSource = dt;

            gvWeb.DataBind();

            if (dt.Rows.Count > 0)
            {
                WebID = dt.Rows[0]["WebID"].ToString();
            }
            else
            {
                WebID = "";
            }

            BindKeys(WebID);

        }

        public void BindKeys(string webid)
        {
            string WebName = KPI_WebDal.GetKeyName(webid);
            lblInfor.Text = WebName;

            WebID = webid;

            //DataTable dt = KPI_KeyDal.GetKeyList(webid);

            //gvKey.DataSource = dt;
            //gvKey.DataBind();
 
        }

        #region GridView Web

        protected void gvWeb_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }
                
                DataRowView drv = (DataRowView)e.Row.DataItem;

                if ((DropDownList)e.Row.FindControl("ddlType") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlType");

                    ddlCollege.SelectedValue = drv["WebType"].ToString();
                }


                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");  

            }

        }     

        protected void gvWeb_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string[] estr = e.CommandArgument.ToString().Split(',');
            //string keyid = Convert.ToString(estr[0]);

            string webid = e.CommandArgument.ToString();

            if (e.CommandName == "dataSelect")
            {
                //
                BindKeys(webid);
                //MessageBox.popupClientMessage(this.Page, "OK！", "call();");

            }else if (e.CommandName == "dataDelete")
            {
                //if (KPI_WebDal.DeleteKey(webid))
                //{
                //    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");
                //    // ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('删除成功')", true);

                //    BindWebs();
                //}
                //else
                //{
                //    //MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                //}
            }

        }
        
        protected void gvWeb_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvWeb.EditIndex = -1;

            BindWebs();
        }

        protected void gvWeb_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvWeb.EditIndex = e.NewEditIndex;

            BindWebs();
        }



        protected void gvWeb_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string sID = ((HtmlInputHidden)(gvWeb.Rows[e.RowIndex].Cells[0].FindControl("webid"))).Value.ToString().Trim();

            string sCode = ((TextBox)(gvWeb.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
            string sDesc = ((TextBox)(gvWeb.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            string sType = ((DropDownList)(gvWeb.Rows[e.RowIndex].Cells[3].FindControl("ddlType"))).SelectedValue;
            string sNote = ((TextBox)(gvWeb.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();

            if (sCode == "")
            {
                MessageBox.popupClientMessage(this.Page, "代码不能为空！", "call();");
                return;
            }

            //名称是否重复
            if (KPI_WebDal.WebCodeExists(sCode, sID))
            {
                MessageBox.popupClientMessage(this.Page, "已存在相同的代码!");
                return;
            }

            //更新
            KPI_WebEntity ote = new KPI_WebEntity();
            ote.WebID = sID;
            ote.WebCode = sCode;
            ote.WebDesc = sDesc;
            ote.WebType = int.Parse(sType);
            ote.WebNote = sNote;

            ote.WebModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (KPI_WebDal.Update(ote))
            {
                MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
            }

            gvWeb.EditIndex = -1;

            BindWebs();
        }

        
        protected void btnAddWeb_Click(object sender, EventArgs e)
        {
            int index = KPI_WebDal.WebIDCounts();

            KPI_WebEntity gwe = new KPI_WebEntity();
            gwe.WebID = PageControl.GetGuid();
            gwe.WebCode = "InputCode";
            gwe.WebDesc = "Input Description";
            gwe.WebType = 0;
            gwe.WebNote = "";

            gwe.WebCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            gwe.WebModifyTime = gwe.WebCreateTime;

            if (KPI_WebDal.Insert(gwe))
            {

                gvWeb.EditIndex = index;

                BindWebs();

                //MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加失败！", "call();");
            }

            return;

        }

        #endregion

        #region GridView Key

        protected void gvKey_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                DataRowView drv = (DataRowView)e.Row.DataItem;

                if ((DropDownList)e.Row.FindControl("ddlCalc") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlCalc");

                    ddlCollege.SelectedValue = drv["KeyCalcType"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlValid") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlValid");

                    ddlCollege.SelectedValue = drv["KeyIsValid"].ToString();
                }


                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");

            }

        }

        protected void gvKey_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string[] estr = e.CommandArgument.ToString().Split(',');
            //string keyid = Convert.ToString(estr[0]);

            string keyid = e.CommandArgument.ToString();

            //string weibid = WebID;

            if (e.CommandName == "dataDelete")
            {
                //if (KPI_KeyDal.DeleteKey(keyid))
                //{
                //    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");
                //    // ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('删除成功')", true);

                //    BindKeys(WebID);
                //}
                //else
                //{
                //    //MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                //}
            }
            
        }
        

        protected void gvKey_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvKey.EditIndex = -1;

            BindKeys(WebID);

        }

        protected void gvKey_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvKey.EditIndex = e.NewEditIndex;

            BindKeys(WebID);

        }

        protected void gvKey_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string sID = ((HtmlInputHidden)(gvKey.Rows[e.RowIndex].Cells[0].FindControl("keyid"))).Value.ToString().Trim();

            string sCalc = ((DropDownList)(gvKey.Rows[e.RowIndex].Cells[5].FindControl("ddlCalc"))).SelectedValue;
            string sValid = ((DropDownList)(gvKey.Rows[e.RowIndex].Cells[6].FindControl("ddlValid"))).SelectedValue;

            //string sNote = ((TextBox)(gvWeb.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();

            //更新
            //KPI_KeyEntity ote = new KPI_KeyEntity();
            //ote.KeyID = sID;

            //ote.KeyCalcType = int.Parse(sCalc);
            //ote.KeyIsValid = int.Parse(sValid);

            //ote.KeyModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            //if (KPI_KeyDal.Update(ote))
            //{
            //    MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

            //}
            //else
            //{
            //    MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
            //}

            //gvKey.EditIndex = -1;

            //BindKeys(WebID);

        }


        #endregion

        #region button

        protected void btnSync_Click(object sender, EventArgs e)
        {

            //if (KPI_KeyDal.KeySync(WebID))
            //{
            //    MessageBox.popupClientMessage(this.Page, "同步成功!", "call();");
            //}

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_SubRDLCKey.aspx?webid=" + WebID);
            
        }

        protected void btnSort_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_SubRDLCSort.aspx?webid=" + WebID);

        }       

        #endregion
    }


}