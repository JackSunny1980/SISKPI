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

namespace SISKPI
{
    /// <summary>
    /// 查询、显示、添加、编辑
    /// 设备信息；
    /// 指标考核基本配置；
    /// </summary>
    public partial class KPI_RedoConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //全部指标
                rblRDType.SelectedValue = "0";

                //初始化
                //机组信息
                DataTable dt = KPI_UnitDal.GetUnits("");
                foreach (DataRow dr in dt.Rows)
                {
                    cbxRDID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                txt_ST.Value = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd HH:mm:00");
                txt_ET.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
                
                BindRD();
            }
        }

        /// <summary>
        ///  //设备信息
        /// </summary>
        void BindRD()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            string StartTime = "";
            string EndTime = "";

            if (rblRedo.SelectedValue == "0")
            {
                StartTime = dt.ToString("yyyy-MM-dd HH:mm:00");
                //EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
            }else if (rblRedo.SelectedValue == "1")
            {
                StartTime = dt.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:00");
                EndTime = dt.ToString("yyyy-MM-dd HH:mm:00");
            }

            //设备信息
            DataTable dtTags = KPI_RedoDal.GetRecords(StartTime, EndTime);

            gvRD.DataSource = dtTags;
            gvRD.DataBind();
        }

        #region 历史重算信息

        protected void gvRD_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                DataRowView drv = (DataRowView)e.Row.DataItem;

                if ((DropDownList)e.Row.FindControl("ddlValid") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlValid");

                    ddlCollege.SelectedValue = drv["RDIsValid"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlCollect") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlCollect");

                    ddlCollege.SelectedValue = drv["RDIsCollect"].ToString();
                }


                if ((DropDownList)e.Row.FindControl("ddlCalced") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlCalced");

                    ddlCollege.SelectedValue = drv["RDIsCalced"].ToString();
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvRD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (KPI_RedoDal.DeleteRD(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                    BindRD();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }
            }
            else if (e.CommandName == "dataDone")
            {
                KPI_RedoDal.ValidOneRescord(keyid);

                BindRD();

                //MessageBox.popupClientMessage(this.Page, "事后剔除标记成功！", "call();");
            }
        }

        protected void gvRD_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvRD.EditIndex = e.NewEditIndex;

            BindRD();
        }

        protected void gvRD_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvRD.EditIndex = -1;

            BindRD();
        }

        #endregion

        #region Button Click

        protected void rblRedo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRD();
        }
        
        protected void rblRDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblRDType.SelectedValue == "0")
            {
                cbxRDID.Items.Clear();

                DataTable dt = KPI_UnitDal.GetUnits("");
                foreach (DataRow dr in dt.Rows)
                {
                    cbxRDID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

            }
            else if (rblRDType.SelectedValue == "1")
            {
                cbxRDID.Items.Clear();

                DataTable dt = KPI_SeqDal.GetSeqs();
                foreach (DataRow dr in dt.Rows)
                {
                    cbxRDID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

            }
            else if (rblRDType.SelectedValue == "2")
            {
                cbxRDID.Items.Clear();

                DataTable dt = KpiDal.GetKpis();
                foreach (DataRow dr in dt.Rows)
                {
                    cbxRDID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

            }
            else if (rblRDType.SelectedValue == "3")
            {
                cbxRDID.Items.Clear();

                DataTable dt = ECTagDal.GetECs();
                foreach (DataRow dr in dt.Rows)
                {
                    cbxRDID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {            
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            //判断时间
            if (DateTime.Parse(txt_ST.Value) >= DateTime.Parse(txt_ET.Value)
                || DateTime.Parse(txt_ST.Value) < dt.AddMonths(-1)
                || DateTime.Parse(txt_ET.Value) > DateTime.Now)
            {
                MessageBox.popupClientMessage(this.Page, "开始时间大于结束时间 或 开始时间小于上月1号 或 结束时间大于当前时间！", "call();");

                return;
            }

            if (rblRDType.SelectedValue == "")
            {
                MessageBox.popupClientMessage(this.Page, "请选择集合类型!", "call();");

                return;

            }
             
            foreach (ListItem lt in cbxRDID.Items)
            {
                if (lt.Selected)
                {     
                    KPI_RedoEntity rEntity = new KPI_RedoEntity();

                    rEntity.RDID = Guid.NewGuid().ToString();

                    rEntity.RDType = int.Parse(rblRDType.SelectedValue);

                    rEntity.RDKPIID = lt.Value;
                    rEntity.RDName = lt.Text;
                    rEntity.RDStartTime = txt_ST.Value;
                    rEntity.RDEndTime = txt_ET.Value;
                    rEntity.RDIsValid = 0;
                    rEntity.RDIsCollect = int.Parse(ddlRDIsCollect.SelectedValue);
                    rEntity.RDIsCalced = 0;
                    rEntity.RDCalcedTime = "";
                    rEntity.RDNote = tbxRDNote.Text.Trim();

                    KPI_RedoDal.Insert(rEntity);  
                    
                }
            }  


            BindRD();

        }
        
        #endregion

    }
}
