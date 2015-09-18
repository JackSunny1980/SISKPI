using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Web.UI.HtmlControls;


//using SIS.Assistant;
using SIS.Loger;
using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;

namespace SISKPI
{
    public partial class KPI_SubRDLCSort : System.Web.UI.Page
    {
        static DataTable gvTable=null;
        static string WebID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility=''; progress_update();");

                //机组信息
                if (Request.QueryString["webid"] != null)
                {
                    WebID = Request.QueryString["webid"].ToString();
                    ViewState["webid"] = WebID;

                }
                else
                {
                    ViewState["webid"] = "";
                }

                BindGrid();
            }
        }

        void BindGrid()
        {
            //string WebID = ViewState["webid"].ToString();

            if (WebID.Equals(""))
            {
                return;
            }

            lblInfor.Text = "当前集合名称为：" + KPI_WebDal.GetKeyName(WebID);

            //gvTable = KPI_KeyDal.GetKeyList(WebID);

            //gvKey.DataSource = gvTable;

            //gvKey.DataBind();

        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (gvTable.Rows.Count <= 0)
            {
                return;
            }

            //for (int i = 0; i < gvTable.Rows.Count; i++)
            //{
            //    KPI_KeyEntity keyE = new KPI_KeyEntity();
            //    keyE.KeyID = gvTable.Rows[i]["KeyID"].ToString();
            //    keyE.KeyIndex = i + 1;

            //    KPI_KeyDal.Update(keyE);
            //}

            MessageBox.popupClientMessage(this.Page, "排序成功！", "call();");
        }

        protected void gvKey_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvKey.PageIndex = e.NewPageIndex;

            BindGrid();
        }

        protected void gvKey_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick ", "if(window.oldtr!=null){window.oldtr.runtimeStyle.cssText='';}this.runtimeStyle.cssText= 'background-color:#e6c5fc';" +
                     "window.oldtr=this; document.getElementById('txtIndex').value='" + e.Row.RowIndex.ToString() + "';");                
                e.Row.Attributes["style"] = "cursor:hand";
            }
        }
 
        protected void btnTop_Click(object sender, EventArgs e)
        {
            int itop = 0;

            int i = int.Parse(txtIndex.Value.Trim());

            if (i <= itop)
            {
                return;
            }

            for (int j = i; j > itop; j--)
            {
                object[] _rowData = gvTable.Rows[j].ItemArray;
                gvTable.Rows[j].ItemArray = gvTable.Rows[j - 1].ItemArray;
                gvTable.Rows[j - 1].ItemArray = _rowData;
            }

            gvKey.DataSource = gvTable;

            gvKey.DataBind();

        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            int itop = 0;
            int i = int.Parse(txtIndex.Value.Trim());

            if (i <= itop)
            {
                return;
            }

            object[] _rowData = gvTable.Rows[i].ItemArray;
            gvTable.Rows[i].ItemArray = gvTable.Rows[i - 1].ItemArray;
            gvTable.Rows[i - 1].ItemArray = _rowData;

            gvKey.DataSource = gvTable;

            gvKey.DataBind();

        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            int ibottom = gvTable.Rows.Count-1;

            int i = int.Parse(txtIndex.Value.Trim());

            if (i >= ibottom)
            {
                return;
            }

            object[] _rowData = gvTable.Rows[i].ItemArray;
            gvTable.Rows[i].ItemArray = gvTable.Rows[i + 1].ItemArray;
            gvTable.Rows[i + 1].ItemArray = _rowData;

            gvKey.DataSource = gvTable;

            gvKey.DataBind();

        }

        protected void btnBottom_Click(object sender, EventArgs e)
        {
            int ibottom = gvTable.Rows.Count-1;

            int i = int.Parse(txtIndex.Value.Trim());

            if (i >= ibottom)
            {
                return;
            }

            for (int j = i; j < ibottom; j++)
            {
                object[] _rowData = gvTable.Rows[j].ItemArray;
                gvTable.Rows[j].ItemArray = gvTable.Rows[j + 1].ItemArray;
                gvTable.Rows[j + 1].ItemArray = _rowData;
            }

            gvKey.DataSource = gvTable;

            gvKey.DataBind();

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_RDLCConfig.aspx");
        }
      
    }
}
