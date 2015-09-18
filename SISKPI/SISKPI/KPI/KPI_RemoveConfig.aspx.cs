using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using SIS.DataAccess;
using SIS.DataEntity;
using SIS.DBControl;
using SIS.Loger;
using SIS.Assistant;

namespace SISKPI {
    /// <summary>
    /// 查询、显示、添加、编辑
    /// 设备信息；
    /// 指标考核基本配置；
    /// </summary>
    public partial class KPI_RemoveConfig : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                btnExcute.Attributes.Add("onclick", "return confirm('确认执行吗?');");

                //全部指标
                rblRMType.SelectedValue = "0";

                //机组信息
                DataTable dt = KPI_UnitDal.GetUnits("");
                foreach (DataRow dr in dt.Rows) {
                    ddlRMKPIID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                txt_ST.Value = DateTime.Now.AddMonths(-1).AddDays(-10).ToString("yyyy-MM-dd HH:mm:00");
                txt_ET.Value = DateTime.Now.AddMonths(-1).AddDays(-5).ToString("yyyy-MM-dd HH:mm:00");

                BindRM();
            }
        }

        /// <summary>
        ///  //设备信息
        /// </summary>
        void BindRM() {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            string StartTime = "";
            string EndTime = "";

            if (rblRemove.SelectedValue == "0") {
                StartTime = dt.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:00");
                EndTime = dt.ToString("yyyy-MM-dd HH:mm:00");
            }
            else if (rblRemove.SelectedValue == "1") {
                StartTime = dt.ToString("yyyy-MM-dd HH:mm:00");
                //EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
            }
            //设备信息
            DataTable dtTags = KPI_RemoveDal.GetRecords(StartTime, EndTime);
            gvRM.DataSource = dtTags;
            gvRM.DataBind();
        }

        #region 事后考核信息

        protected void gvRM_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                if ((LinkButton)e.Row.FindControl("delete") != null) {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                DataRowView drv = (DataRowView)e.Row.DataItem;

                if ((DropDownList)e.Row.FindControl("ddlValid") != null) {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlValid");

                    ddlCollege.SelectedValue = drv["RMIsValid"].ToString();
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvRM_RowCommand(object sender, GridViewCommandEventArgs e) {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete") {
                if (KPI_RemoveDal.DeleteRM(keyid)) {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                    BindRM();
                }
                else {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }
            }
            else if (e.CommandName == "dataDone") {
                KPI_RemoveDal.RemoveOneRescord(keyid);
                KPI_RemoveEntity Entity = KPI_RemoveDal.GetEntity(keyid);
                DateTime StartDate = Convert.ToDateTime(Entity.RMStartTime);
                StartDate = new DateTime(StartDate.Year, StartDate.Month, 1);
                DateTime EndDate = Convert.ToDateTime(Entity.RMEndTime);
                EndDate = new DateTime(EndDate.Year, EndDate.Month, 1);
                EndDate = EndDate.AddMonths(1).AddSeconds(-1);
                KPI_RemoveDal.RecalcScoreAndBonus(StartDate, EndDate); 
                BindRM();
                MessageBox.popupClientMessage(this.Page, "事后剔除标记成功！", "call();");
            }
            else if (e.CommandName == "dataRestore") {
                KPI_RemoveDal.RestoreOneRescord(keyid);               
                KPI_RemoveEntity Entity = KPI_RemoveDal.GetEntity(keyid);
                DateTime StartDate = Convert.ToDateTime(Entity.RMStartTime);
                StartDate = new DateTime(StartDate.Year, StartDate.Month, 1);
                DateTime EndDate = Convert.ToDateTime(Entity.RMEndTime);
                EndDate = new DateTime(EndDate.Year, EndDate.Month, 1);
                EndDate = EndDate.AddMonths(1).AddSeconds(-1);
                KPI_RemoveDal.RecalcScoreAndBonus(StartDate, EndDate); 
                BindRM();
                MessageBox.popupClientMessage(this.Page, "事后剔除恢复成功！", "call();");
            }

        }

        protected void gvRM_RowEditing(object sender, GridViewEditEventArgs e) {
            gvRM.EditIndex = e.NewEditIndex;

            BindRM();
        }

        protected void gvRM_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            gvRM.EditIndex = -1;

            BindRM();
        }

        #endregion

        #region Button Click

        protected void rblRemove_SelectedIndexChanged(object sender, EventArgs e) {
            //在查看全部的时候不能全部执行
            if (rblRemove.SelectedValue == "2") {
                btnExcute.Enabled = false;
            }
            else {
                btnExcute.Enabled = true;
            }

            BindRM();
        }

        protected void btnExcute_Click(object sender, EventArgs e) {
            if (gvRM.Rows.Count <= 0) {
                return;
            }

            //
            string[] rmids = new string[gvRM.Rows.Count];

            for (int i = 0; i < gvRM.Rows.Count; i++) {
                rmids[i] = gvRM.Rows[i].Cells[1].ToString();
            }

            KPI_RemoveDal.RemoveMultiRescord(rmids);

            BindRM();

            MessageBox.popupClientMessage(this.Page, "事后剔除标记成功！", "call();");


        }


        protected void rblRMType_SelectedIndexChanged(object sender, EventArgs e) {
            if (rblRMType.SelectedValue == "0") {
                cbxRMKPIID.Visible = false;
                ddlRMKPIID.Visible = true;
                ddlRMKPIID.Items.Clear();

                DataTable dt = KPI_UnitDal.GetUnits("");
                foreach (DataRow dr in dt.Rows) {
                    ddlRMKPIID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

            }
            else if (rblRMType.SelectedValue == "1") {
                cbxRMKPIID.Visible = false;
                ddlRMKPIID.Visible = true;
                ddlRMKPIID.Items.Clear();

                DataTable dt = KPI_SeqDal.GetSeqs();
                foreach (DataRow dr in dt.Rows) {
                    ddlRMKPIID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

            }
            else if (rblRMType.SelectedValue == "2") {
                cbxRMKPIID.Visible = false;
                ddlRMKPIID.Visible = true;
                ddlRMKPIID.Items.Clear();

                DataTable dt = KpiDal.GetKpis();
                foreach (DataRow dr in dt.Rows) {
                    ddlRMKPIID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

            }
            else if (rblRMType.SelectedValue == "3") {
                cbxRMKPIID.Visible = true;
                cbxRMKPIID.Items.Clear();

                ddlRMKPIID.Visible = false;
                ddlRMKPIID.Items.Clear();

                DataTable dt = ECTagDal.GetECs();
                foreach (DataRow dr in dt.Rows) {
                    cbxRMKPIID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e) {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            //判断时间
            if (DateTime.Parse(txt_ST.Value) >= DateTime.Parse(txt_ET.Value)
                || DateTime.Parse(txt_ST.Value) < dt.AddMonths(-1)
                || DateTime.Parse(txt_ET.Value) > DateTime.Now) {
                MessageBox.popupClientMessage(this.Page, "开始时间大于结束时间 或 开始时间小于上月1号 或 结束时间大于当前时间！", "call();");

                return;
            }


            if (rblRMType.SelectedValue == "0") {

                KPI_RemoveEntity rEntity = new KPI_RemoveEntity();

                rEntity.RMID = Guid.NewGuid().ToString();
                rEntity.RMType = 0;
                rEntity.RMKPIID = ddlRMKPIID.SelectedValue;
                rEntity.RMName = ddlRMKPIID.SelectedItem.Text;
                rEntity.RMStartTime = txt_ST.Value;
                rEntity.RMEndTime = txt_ET.Value;
                rEntity.RMIsValid = 0;
                rEntity.RMNote = tbxRMNote.Text.Trim();

                KPI_RemoveDal.Insert(rEntity);

            }
            else if (rblRMType.SelectedValue == "1") {
                KPI_RemoveEntity rEntity = new KPI_RemoveEntity();

                rEntity.RMID = Guid.NewGuid().ToString();
                rEntity.RMType = 1;
                rEntity.RMKPIID = ddlRMKPIID.SelectedValue;
                rEntity.RMName = ddlRMKPIID.SelectedItem.Text;
                rEntity.RMStartTime = txt_ST.Value;
                rEntity.RMEndTime = txt_ET.Value;
                rEntity.RMIsValid = 0;
                rEntity.RMNote = tbxRMNote.Text.Trim();

                KPI_RemoveDal.Insert(rEntity);


            }
            else if (rblRMType.SelectedValue == "2") {
                KPI_RemoveEntity rEntity = new KPI_RemoveEntity();

                rEntity.RMID = Guid.NewGuid().ToString();
                rEntity.RMType = 2;
                rEntity.RMKPIID = ddlRMKPIID.SelectedValue;
                rEntity.RMName = ddlRMKPIID.SelectedItem.Text;
                rEntity.RMStartTime = txt_ST.Value;
                rEntity.RMEndTime = txt_ET.Value;
                rEntity.RMIsValid = 0;
                rEntity.RMNote = tbxRMNote.Text.Trim();

                KPI_RemoveDal.Insert(rEntity);

            }
            else if (rblRMType.SelectedValue == "3") {
                foreach (ListItem lt in cbxRMKPIID.Items) {
                    if (lt.Selected) {
                        KPI_RemoveEntity rEntity = new KPI_RemoveEntity();

                        rEntity.RMID = Guid.NewGuid().ToString();
                        rEntity.RMType = 3;
                        rEntity.RMKPIID = lt.Value;
                        rEntity.RMName = lt.Text;
                        rEntity.RMStartTime = txt_ST.Value;
                        rEntity.RMEndTime = txt_ET.Value;
                        rEntity.RMIsValid = 0;
                        rEntity.RMNote = tbxRMNote.Text.Trim();

                        KPI_RemoveDal.Insert(rEntity);
                    }
                }

            }


            BindRM();

        }

        #endregion
    }
}
