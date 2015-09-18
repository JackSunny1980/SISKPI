using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIS.Exceler;
using SISKPI.KPIAlarm.Helper;
using SIS.DataAccess;
using SIS.DataEntity;
using Common.Web.UI;

namespace SISKPI.KPIAlarm {
    public partial class KPI_OverLimitStatistics : System.Web.UI.Page {
        private readonly IKPI_OverLimitRecordDal overLimitRecordDal = DataModuleFactory.CreateKPI_OverLimitRecordDal();

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                BindUnitData();
                InitPageData();
                this.Pager.CurrentPageIndex = 1;
                DateTime startTime = Convert.ToDateTime(txtBeginTime.Value);
                DateTime endTime = Convert.ToDateTime(txtEndTime.Value);
                BindingData(startTime, endTime, this.Pager.CurrentPageIndex, this.Pager.PageSize);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e) {
            string kpiName = string.IsNullOrWhiteSpace(drpTags.SelectedValue) ? null : drpTags.SelectedValue;
            string unityValue = selectedValue.Value;
            string beginTime = txtBeginTime.Value;
            string endTime = txtEndTime.Value;
            int totalCount = 0;
            List<KPI_OverLimitRecordEntity> overLimitRecordList = overLimitRecordDal.SearchOverLimitRecord(Pager.CurrentPageIndex, Pager.PageSize, kpiName, unityValue, beginTime, endTime, true, ref totalCount);
            overLimitRecordList = ConverterHelper.ConverterItem(overLimitRecordList);
            gvValue.DataSource = overLimitRecordList;
            gvValue.DataBind();
            SettingPaging(totalCount);

        }

        protected void btnRefresh_Click(object sender, EventArgs e) {
            InitPageData();
            //txtSearchKey.Value = "";
            dropUnityList.SelectedIndex = 0;
            selectedValue.Value = "U01";
            this.Pager.CurrentPageIndex = 1;
            DateTime startTime = Convert.ToDateTime(txtBeginTime.Value);
            DateTime endTime = Convert.ToDateTime(txtEndTime.Value);
            BindingData(startTime, endTime, this.Pager.CurrentPageIndex, this.Pager.PageSize);
        }

        protected void btnExport_Click(object sender, EventArgs e) {
            string unityValue = selectedValue.Value;
            string kpiName = string.IsNullOrWhiteSpace(drpTags.SelectedValue) ? null : drpTags.SelectedValue;

            string beginTime = txtBeginTime.Value;
            string endTime = txtEndTime.Value;
            int totalCount = 0;

            List<KPI_OverLimitRecordEntity> overLimitRecordList = null;
            if (unityValue == "U01" || string.IsNullOrWhiteSpace(unityValue)) {

                overLimitRecordList = overLimitRecordDal.GetOverLimitRecordEntityList(Convert.ToDateTime(beginTime),
                    Convert.ToDateTime(endTime), 1, int.MaxValue, out totalCount);
            }
            else {
                overLimitRecordList = overLimitRecordDal.SearchOverLimitRecord(Pager.CurrentPageIndex, Pager.PageSize, kpiName, unityValue, beginTime, endTime, true, ref totalCount);

            }
            overLimitRecordList = ConverterHelper.ConverterItem(overLimitRecordList);
            gvValue.DataSource = overLimitRecordList;
            gvValue.DataBind();
            if (gvValue.Rows.Count > 0) {
                DocExport docEexport = new DocExport();

                docEexport.Dt = GridViewHelper.GridView2DataTable(gvValue);

                docEexport.Export("SIS超限统计报表");
            }
        }

        protected void Pager_PageChanged(object src, EventArgs e) {
            AspNetPager pager = src as AspNetPager;
            string unityValue = selectedValue.Value;
            string kpiName = string.IsNullOrWhiteSpace(drpTags.SelectedValue) ? null : drpTags.SelectedValue;

            DateTime startTime = Convert.ToDateTime(txtBeginTime.Value);
            DateTime endTime = Convert.ToDateTime(txtEndTime.Value);

            if (unityValue == "U01" || string.IsNullOrWhiteSpace(unityValue)) {
                BindingData(startTime, endTime, pager.CurrentPageIndex, pager.PageSize);
            }
            else {
                int totalCount = 0;
                List<KPI_OverLimitRecordEntity> overLimitRecordList = overLimitRecordDal.SearchOverLimitRecord(Pager.CurrentPageIndex, Pager.PageSize, kpiName, unityValue, startTime.ToString(), endTime.ToString(), true, ref totalCount);
                overLimitRecordList = ConverterHelper.ConverterItem(overLimitRecordList);
                gvValue.DataSource = overLimitRecordList;
                gvValue.DataBind();
            }

        }

        private void InitPageData() {
            txtBeginTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void BindingData(DateTime startTime, DateTime endTime, int startIndex, int pageSize) {
            int totalCount = 0;
            List<KPI_OverLimitRecordEntity> overLimitRecordList = overLimitRecordDal.GetOverLimitRecordEntityList(startTime, endTime, startIndex, pageSize, out totalCount);
            overLimitRecordList = ConverterHelper.ConverterItem(overLimitRecordList);
            gvValue.DataSource = overLimitRecordList;
            gvValue.DataBind();

            SettingPaging(totalCount);

        }

        private void BindUnitData() {
            List<KPI_UnitEntity> unityEntityList = overLimitRecordDal.GetUnitEntityList();
            unityEntityList.Insert(0, new KPI_UnitEntity { UnitID = "U01", UnitName = "选择机组" });
            dropUnityList.DataSource = unityEntityList;
            dropUnityList.DataTextField = "UnitName";
            dropUnityList.DataValueField = "UnitID";
            dropUnityList.DataBind();
        }

        private void SettingPaging(int recordCount) {
            if (recordCount == 0) {
                this.Pager.Visible = false;
            }
            else {
                this.Pager.Visible = true;
                this.Pager.RecordCount = recordCount;
            }
        }
    }
}