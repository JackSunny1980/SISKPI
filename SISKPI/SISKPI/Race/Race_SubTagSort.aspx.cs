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
    public partial class Race_SubTagSort : System.Web.UI.Page
    {
        static DataTable gvTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility=''; progress_update();");

                //机组信息
                DataTable dt = KPI_UnitDal.GetUnits("");
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_UnitID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                BindGrid();
            }
        }

        void BindGrid()
        {
            if (ddl_UnitID.Value.Equals(""))
            {
                return;
            }

            string sql = " and a.UnitID ='" + ddl_UnitID.Value + "'";
            
            //sql += " and a.ZBCate = " + ddl_ZBCate.Value;

            gvTable = Race_TagDal.GetSearchList3(sql);
            gvTag.DataSource = gvTable;
            gvTag.DataBind();
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (gvTable.Rows.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < gvTable.Rows.Count; i++)
            {
                Race_TagEntity csE = new Race_TagEntity();
                csE.TagID = gvTable.Rows[i]["TagID"].ToString();
                csE.TagIndex = i + 1;

                Race_TagDal.Update(csE);
            }

            MessageBox.popupClientMessage(this.Page, "排序成功！", "call();");
        }

        protected void gvTag_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTag.PageIndex = e.NewPageIndex;

            BindGrid();
        }

        protected void gvTag_RowDataBound(object sender, GridViewRowEventArgs e)
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

            gvTag.DataSource = gvTable;

            gvTag.DataBind();

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

            gvTag.DataSource = gvTable;

            gvTag.DataBind();

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

            gvTag.DataSource = gvTable;

            gvTag.DataBind();

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

            gvTag.DataSource = gvTable;

            gvTag.DataBind();

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
