using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

using SIS.DataAccess;
using SIS.Assistant;
using SIS.DataEntity;
using SIS.Loger;
using SIS.Assistant.WS;
using SIS.DBControl;

namespace SISKPI {
    public partial class KPI_SubLinqTag : System.Web.UI.Page {
        //static string PlantName = "";
        //static string ZBName = "";
        //static string PlantID = "";

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                btnAdd.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility='';progress_update();");

                //btnClose.Attributes.Add("onclick", "javascript:return confirm('确认关闭吗？');");

                if (Request.QueryString["linqid"] != null) {
                    string LinqID = Request.QueryString["linqid"].ToString();

                    string LinqName = KPI_LinqDal.GetLinqName(LinqID);

                    //标签信息
                    lbl_Name.Text = "当前配置指标为：" + LinqName;


                    ViewState["linqid"] = LinqID;

                    //////////////////////////////////////////////

                    //机组信息
                    DataTable dt = KPI_UnitDal.GetUnits("");
                    foreach (DataRow dr in dt.Rows) {
                        ddl_UnitID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                    }


                    BindValues();

                    BindECs();
                }

            }
        }

        void BindECs() {
            string UnitID = ddl_UnitID.SelectedValue;

            if (UnitID.Equals("")) {
                return;
            }

            ddl_ECID.Items.Clear();

            DataTable dt = ECTagDal.GetECByUnit(UnitID);
            foreach (DataRow dr in dt.Rows) {
                ddl_ECID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
            }

        }

        void BindValues() {
            string LinqID = ViewState["linqid"].ToString();

            DataTable dt = LinqTagDal.GetTags(LinqID);

            gvTag.DataSource = dt;

            gvTag.DataBind();
        }


        #region 【参数部分验证】

        string msg = "";

        /// <summary>
        /// 页面信息检查
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CheckVal(out string msg) {
            bool flag = false;
            msg = "";

            string LinqID = ViewState["linqid"].ToString();

            string sUnitID = ddl_UnitID.SelectedValue;
            string sECID = ddl_UnitID.SelectedValue;

            //该指标在该机组是否已配置了标签点
            //在SQL表中是否存在
            if (LinqTagDal.TagExists(LinqID, sUnitID)) {
                msg += "该机组的指标已配置，不能再添加，请检查！\r\n";
                return true;
            }

            if (sECID == "") {
                msg += "指标不能为空！\r\n";
            }

            return flag;
        }

        #endregion


        #region 【插入方法】

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <returns></returns>
        bool Insert() {
            string LinqID = ViewState["linqid"].ToString();
            string keyid = PageControl.GetGuid();
            LinqTagEntity mEntity = new LinqTagEntity();
            mEntity.TagID = keyid;
            mEntity.LinqID = LinqID;
            mEntity.UnitID = ddl_UnitID.SelectedValue;
            mEntity.UnitName = ddl_UnitID.SelectedItem.Text;
            mEntity.ECID = ddl_ECID.SelectedValue;
            mEntity.ECName = ddl_ECID.SelectedItem.Text;
            return LinqTagDal.Insert(mEntity);
        }

        #endregion

        #region 【Gridview事件】

        protected void gvTag_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                if ((LinkButton)e.Row.FindControl("delete") != null) {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");

            }

        }

        protected void gvTag_RowCommand(object sender, GridViewCommandEventArgs e) {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete") {
                if (LinqTagDal.DeleteTag(keyid)) {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                    gvTag.EditIndex = -1;

                    BindValues();
                    return;
                }
                else {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                    BindValues();
                }


            }

        }
        #endregion


        #region 【按钮事件】

        protected void ddl_UnitID_SelectedIndexChanged(object sender, EventArgs e) {
            BindECs();
        }

        protected void btnAdd_Click(object sender, EventArgs e) {
            if (CheckVal(out msg)) {
                MessageBox.popupClientMessage(this.Page, msg, "call();");
                return;
            }


            string LinqID = ViewState["linqid"].ToString();

            if (LinqID != "") {
                if (Insert()) {
                    MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");

                    gvTag.EditIndex = -1;
                    BindValues();
                }
                else {
                    MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");
                }
            }
        }



        protected void btnClose_Click(object sender, EventArgs e) {
            //string strjs = "<script language=javascript>window.close();</script>";
            //ClientScript.RegisterStartupScript(this.GetType(), "", strjs);

            Response.Redirect("KPI_LinqConfig.aspx");

        }



        #endregion
    }
}
