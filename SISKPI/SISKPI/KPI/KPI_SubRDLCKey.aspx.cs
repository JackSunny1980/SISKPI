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
    public partial class KPI_SubRDLCKey : System.Web.UI.Page
    {
        static DataTable gvTable = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility=''; progress_update();");
                
                //指标信息
                if (Request.QueryString["webid"] != null)
                {
                    ViewState["webid"] = Request.QueryString["webid"].ToString();

                    //string unitid = Ana_WebDal.GetUnitID(ViewState["webid"].ToString());

                    //ddl_Unit.SelectedValue = unitid;

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
            string WebID = ViewState["webid"].ToString();


            string WebName = KPI_WebDal.GetKeyName(WebID);
            lblInfor.Text = "当前操作集为：" + WebName;

            //
            //gvTable = KPI_KeyDal.GetSearchListNot(WebID);

            //cbxKey.DataSource = gvTable;

            //cbxKey.DataTextField = "Name";
            //cbxKey.DataValueField = "ID";

            //cbxKey.DataBind();
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (gvTable.Rows.Count <= 0)
            {
                return;
            }

            string WebID = ViewState["webid"].ToString();         

            //List<string> ladd = new List<string>();

            for (int i = 0; i <cbxKey.Items.Count; i++)
            {
                if (cbxKey.Items[i].Selected)
                {
                    //KPI_KeyEntity keyE = new KPI_KeyEntity();
                    //keyE.KeyID = PageControl.GetGuid();
                    //keyE.ECID = gvTable.Rows[i]["ECID"].ToString(); ;
                    //keyE.ECCode = gvTable.Rows[i]["ECCode"].ToString();
                    //keyE.ECName = gvTable.Rows[i]["ECName"].ToString();
                    //keyE.WebID = WebID;
                    //keyE.KeyEngunit = gvTable.Rows[i]["EngunitName"].ToString();
                    //keyE.KeyCalcType = 0;
                    //keyE.KeyIndex = 100;
                    //keyE.KeyIsValid = 1;

                    //keyE.KeyNote = "";
                    //keyE.KeyCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    //keyE.KeyModifyTime = keyE.KeyCreateTime;

                    //KPI_KeyDal.Insert(keyE);

                    //ladd.Add(keyE.KeyID);
                    
                }
            }

            MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {         

            Response.Redirect("KPI_RDLCConfig.aspx");
        }


        //protected void btnAll_Click(object sender, EventArgs e)
        //{
        //    if (btnAll.Text == "全 选")
        //    {
        //        btnAll.Text = "全不选";
        //    }
        //    else
        //    {
        //        btnAll.Text = "全 选";
        //    }

        //    for (int i = 0; i <cbxKey.Items.Count; i++)
        //    {
        //        cbxKey.Items[i].Selected = !cbxKey.Items[i].Selected;
        //    }
        //}
      
    }
}
