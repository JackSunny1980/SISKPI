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
    public partial class KPI_InputTag : System.Web.UI.Page
    {
        //static DataTable gvTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["iptype"] != null)
                {
                    ViewState["iptype"] = Request.QueryString["iptype"].ToString();
                }
                else
                {
                    ViewState["iptype"] = "0";
                }


                BindGrid();
            }
        }

        void BindGrid()
        {
            string inputtype = ViewState["iptype"].ToString();
            
            DataTable gvTable = KPI_InputTagDal.GetInputTagForDisplay(inputtype);

            gvTag.DataSource = gvTable;

            gvTag.DataBind();

            if (gvTable.Rows.Count > 0 && gvTable.Rows[0]["InputTime"].ToString()!="")
            {
                lblInfor.Text = "录入时间:" + DateTime.Parse(gvTable.Rows[0]["InputTime"].ToString()).ToString("yyyy-MM-dd");
            }
            else
            {
                lblInfor.Text = "录入时间:无";
            }
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
                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);"); 
            }
        }

        #endregion   

    }
}
