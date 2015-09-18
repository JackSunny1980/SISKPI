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
    public partial class KPI_ForValue : System.Web.UI.Page
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
            DataTable dtShift = KPI_WebDal.GetWebListbyType("0");
            gvData.DataSource = dtShift;
            gvData.DataBind();

            DataTable dtUnit = KPI_WebDal.GetWebListbyType("1");
            gvData1.DataSource = dtUnit;
            gvData1.DataBind();
        }

        #region GridView Web

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string WebCode = gvData.DataKeys[e.Row.RowIndex]["WebCode"].ToString();
                e.Row.Attributes.Add("id", WebCode);

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

        protected void gvData1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string WebCode = gvData1.DataKeys[e.Row.RowIndex]["WebCode"].ToString();
                e.Row.Attributes.Add("id", WebCode);

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
           
 
    }


}