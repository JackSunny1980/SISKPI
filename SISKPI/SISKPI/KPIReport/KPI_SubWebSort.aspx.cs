using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using SIS.Assistant;
using SIS.Loger;
using System.Web.UI.HtmlControls;
using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;

namespace SISKPI
{
    public partial class KPI_SubWebSort : System.Web.UI.Page
    {
        static string WebCode;
        static DataTable gvTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //指标信息
                if (Request.QueryString["webcode"] != null)
                {
                    WebCode = Request.QueryString["webcode"].ToString();
                }
                else
                {
                    WebCode = "";
                }


                BindGrid();
            }
        }

        void BindGrid()
        {
            lblInfor2.Text = "" + WebCode;

            gvTable = KPI_WebKeyDal.GetDisplayForSort(WebCode);

            gvData.DataSource = gvTable;

            gvData.DataBind();
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (gvTable.Rows.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < gvTable.Rows.Count; i++)
            {
                KPI_WebKeyEntity csE = new KPI_WebKeyEntity();
                csE.KeyID = gvTable.Rows[i]["KeyID"].ToString();
                csE.KeyIndex = (10+i);

                KPI_WebKeyDal.Update(csE);
            }

            MessageBox.popupClientMessage(this.Page, "排序成功！", "call();");
        }

        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;

            BindGrid();
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
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

            gvData.DataSource = gvTable;

            gvData.DataBind();

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

            gvData.DataSource = gvTable;

            gvData.DataBind();

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

            gvData.DataSource = gvTable;

            gvData.DataBind();

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

            gvData.DataSource = gvTable;

            gvData.DataBind();

        }
      
    }
}
