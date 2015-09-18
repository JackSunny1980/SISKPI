using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Web.UI.HtmlControls;


using SIS.Assistant;
using SIS.Loger;
using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;

namespace SISKPI
{
    public partial class KPI_InputValue : System.Web.UI.Page
    {
        //static DataTable gvTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnApply.Attributes.Add("onclick", "return confirm('确认录入数据库吗?');");

                //btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility=''; progress_update();");

                txt_Time.Value = DateTime.Now.ToString("yyyy-MM-dd");

                BindGrid();
            }
        }

        void BindGrid()
        {
            string inputtype = rbl_InputType.SelectedValue;
            string inputtime = DateTime.Parse(txt_Time.Value).ToString("yyyy-MM-dd 00:00:00");

            DataTable gvTable = KPI_InputTagDal.GetInputTagWithValue(inputtype, inputtime);

            gvTag.DataSource = gvTable;

            gvTag.DataBind();
        }

        protected void rbl_InputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            //string inputtype = rbl_InputType.SelectedValue;
            //string inputtime = DateTime.Parse(txt_Time.Value).ToString("yyyy-MM-dd 00:00:00");

            //DataTable gvTable = KPI_InputTagDal.GetInputTagWithValue(inputtype, inputtime);

            //gvTag.DataSource = gvTable;

            //gvTag.DataBind();

            BindGrid();

        }

        /// <summary>
        /// 写入数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (!this.IsValid)
            {
                MessageBox.popupClientMessage(this.Page, "输入数据有错误!", "call();");
                return;
            }

            string inputtype = rbl_InputType.SelectedValue;
            string inputtime = DateTime.Parse(txt_Time.Value).ToString("yyyy-MM-dd 00:00:00");

            List<InputValueEntity> lstRV = new List<InputValueEntity>();

            // 遍历GridView中的每一行
            for (int i = 0; i < gvTag.Rows.Count; i++)
            {
                //获行当前行
                GridViewRow gridRow = gvTag.Rows[i];
                
                string sCode = gridRow.Cells[1].Text.ToString();
                string sDesc = gridRow.Cells[2].Text.ToString();
                string sEngunit = gridRow.Cells[4].Text.ToString();

                string sValue = ((TextBox)gridRow.FindControl("inputvalue")).Text ;

                //if ((TextBox)gridRow.FindControl("inputvalue") != null)
                //{
                //    TextBox ddlCollege = (TextBox)gridRow.FindControl("inputvalue");

                //    sValue = ddlCollege.Text;
                //}

                InputValueEntity lve = new InputValueEntity();                
                lve.RVID = Guid.NewGuid().ToString();
                lve.InputCode = sCode;
                lve.InputDesc = sDesc;
                lve.InputEngunit = sEngunit;
                lve.InputTime = inputtime;
                lve.InputValue = sValue;
                lve.InputSnap = 1;

                lstRV.Add(lve);               
                
            }

            //设置为0
            InputValueDal.SetInputValueNotSnap();
            foreach (InputValueEntity oneRV in lstRV)
            {
                InputValueDal.Insert(oneRV);
            }

            MessageBox.popupClientMessage(this.Page, "录入成功!", "call();");

            return;
        }

        #region GridView 操作

        protected void gvTag_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTag.PageIndex = e.NewPageIndex;

            BindGrid();
        }

        protected void gvTag_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
                DataRowView drv = (DataRowView)e.Row.DataItem;

                if ((TextBox)e.Row.FindControl("inputvalue") != null)
                {
                    TextBox ddlCollege = (TextBox)e.Row.FindControl("inputvalue");

                    ddlCollege.Text = drv["InputValue"].ToString();
                }
                
                //行属性
                //e.Row.Attributes.Add("onclick ", "if(window.oldtr!=null){window.oldtr.runtimeStyle.cssText='';}this.runtimeStyle.cssText= 'background-color:#e6c5fc';" +
                //     "window.oldtr=this; document.getElementById('txtIndex').value='" + e.Row.RowIndex.ToString() + "';");                
                //e.Row.Attributes["style"] = "cursor:hand";


                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);"); 
            }
        }

        #endregion   

    }
}
