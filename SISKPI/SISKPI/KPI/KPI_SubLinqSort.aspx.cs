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
    public partial class KPI_SubLinqSort : System.Web.UI.Page
    {
        static DataTable gvTable=null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility=''; progress_update();");

                //机组信息
                //DataTable dt = OPM_PlantDal.GetPlants("");
                //foreach (DataRow dr in dt.Rows)
                //{
                //    ddl_PlantID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                //}

                BindGrid();
            }
        }

        void BindGrid()
        {
            if (ddl_PlantID.Value.Equals("") && ddl_ZBCate.Value.Equals(""))
            {
                return;
            }

            string sql = " and a.PlantID ='" + ddl_PlantID.Value + "'";
            
            sql += " and a.ZBCate = " + ddl_ZBCate.Value;

            //gvTable = Report_ZBDal.GetSearchList(sql);
            //gvCS.DataSource = gvTable;
            //gvCS.DataBind();
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (gvTable.Rows.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < gvTable.Rows.Count; i++)
            {
                //Report_ZBEntity csE = new Report_ZBEntity();
                //csE.ZBID = gvTable.Rows[i]["ZBID"].ToString();
                //csE.ZBIndex = i + 1;

                //Report_ZBDal.Update(csE);
            }

            MessageBox.popupClientMessage(this.Page, "排序成功！", "call();");
        }

        protected void gvCS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCS.PageIndex = e.NewPageIndex;

            BindGrid();
        }

        protected void gvCS_RowDataBound(object sender, GridViewRowEventArgs e)
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

            gvCS.DataSource = gvTable;

            gvCS.DataBind();

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

            gvCS.DataSource = gvTable;

            gvCS.DataBind();

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

            gvCS.DataSource = gvTable;

            gvCS.DataBind();

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

            gvCS.DataSource = gvTable;

            gvCS.DataBind();

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            string strjs = "<script language=javascript>window.close();</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", strjs);
        }
      
    }
}
