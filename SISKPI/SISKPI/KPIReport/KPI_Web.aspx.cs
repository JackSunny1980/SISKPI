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
    public partial class KPI_Web : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SISKPI.KPIDataSetTableAdapters.GetWebTypeTableAdapter a = new KPIDataSetTableAdapters.GetWebTypeTableAdapter();
                this.WebType.DataSource = a.GetData();
                this.WebType.DataBind();


                BindWebs();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void BindWebs()
        {
            //绑定参数
            DataTable dt = KPI_WebDal.GetAllWebList();
            gvData.DataSource = dt;
            gvData.DataBind();
        }

        #region GridView Web

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string sWebID = gvData.DataKeys[e.Row.RowIndex]["WebID"].ToString();
                string sWebCode = gvData.DataKeys[e.Row.RowIndex]["WebCode"].ToString();
                string sWebDesc = gvData.DataKeys[e.Row.RowIndex]["WebDesc"].ToString();
                string sWebType = gvData.DataKeys[e.Row.RowIndex]["WebType"].ToString();
                string sWebNote = gvData.DataKeys[e.Row.RowIndex]["WebNote"].ToString();
                e.Row.Attributes.Add("id", sWebID + "," + sWebCode + "," + sWebDesc + "," + sWebType + "," + sWebNote);

                DataRowView drv = (DataRowView)e.Row.DataItem;
                foreach (ListItem i in WebType.Items)
                {
                    if (i.Value == e.Row.Cells[3].Text.ToString())
                    {
                        e.Row.Cells[3].Text = i.Text;
                        break;
                    }
                }
            }

        }     

        #endregion


        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindWebs();
        }
    }


}