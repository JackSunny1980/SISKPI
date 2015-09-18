using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIS.DBControl;
using SIS.DataEntity;
using SIS.DataAccess;
using Common.Web.UI;


namespace SISKPI.KPIAlarm {
    public partial class KPI_OverLimitConfigSetting : System.Web.UI.Page {

        private RTInterface m_DataAccess;
        protected RTInterface RTDataAccess {
            get {
                if (m_DataAccess == null) m_DataAccess = DBAccess.GetRealTime();
                return m_DataAccess;
            }
        }

        private readonly IKPI_OverLimitConfigDal overLimitConfigDal = DataModuleFactory.CreateKPI_OverLimitConfigDal();

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                BindKpiData();
                Pager.CurrentPageIndex = 1;
                BindingData(Pager.CurrentPageIndex, this.Pager.PageSize);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e) {
            int totalCount = 0;
            string tagName = txtSearchKey.Text;

            ConfigRepeater.DataSource = SearchOverLimitConfigListByTagName(Pager.CurrentPageIndex, Pager.PageSize, tagName, ref totalCount);
            ConfigRepeater.DataBind();
            SettingPaging(totalCount);
        }

        protected void btnRefresh_Click(object sender, EventArgs e) {
            this.Pager.CurrentPageIndex = 1;
            BindingData(this.Pager.CurrentPageIndex, this.Pager.PageSize);
        }

        protected void Pager_PageChanged(object src, EventArgs e) {
            AspNetPager pager = src as AspNetPager;
            BindingData(pager.CurrentPageIndex, pager.PageSize);
        }

        private void BindingData(int startIndex, int pageSize) {
            int totalCount = 0;
            ConfigRepeater.DataSource = SearchOverLimitConfigList(startIndex, pageSize, ref totalCount);
            ConfigRepeater.DataBind();
            SettingPaging(totalCount);
        }

        public List<UIOverLimitConfig> SearchOverLimitConfigListByTagName(int startIndex, int pageSize, string tagName, ref int totalCount) {
            var list = overLimitConfigDal.GetOverLimitConfigList(startIndex, pageSize, tagName, ref totalCount);
            return ConvertUIOverLimiitConfigList(list);
        }

        public List<UIOverLimitConfig> SearchOverLimitConfigList(int startIndex, int pageSize, ref int totalCount) {
            var list = overLimitConfigDal.GetOverLimitConfigList(startIndex, pageSize, ref totalCount);
            return ConvertUIOverLimiitConfigList(list);
        }

        private static List<UIOverLimitConfig> ConvertUIOverLimiitConfigList(List<OverLimitConfigEntity> list) {
            List<UIOverLimitConfig> resultList = new List<UIOverLimitConfig>();
            if (list == null || list.Count <= 0) return resultList;
            foreach (var item in list) {
                UIOverLimitConfig currentEntity = new UIOverLimitConfig() {
                    TagName = item.TagName,
                    TagCode = item.TagCode,
                    OverLimitConfigID = item.OverLimitConfigID,
                    RealDesc = item.RealDesc,
                    CreatedDate = item.CreatedDate,

                    LowLimit1Value = item.LowLimit1Value == null ? "" : item.LowLimit1Value.ToString(),
                    LowLimit2Value = item.LowLimit2Value == null ? "" : item.LowLimit2Value.ToString(),
                    LowLimit3Value = item.LowLimit2Value == null ? "" : item.LowLimit2Value.ToString(),
                    FirstLimitingValue = item.FirstLimitingValue == null ? "" : item.FirstLimitingValue.ToString(),
                    SecondLimitingValue = item.SecondLimitingValue == null ? "" : item.SecondLimitingValue.ToString(),
                    ThirdLimitingValue = item.ThirdLimitingValue == null ? "" : item.ThirdLimitingValue.ToString(),
                    FourthLimitingValue = item.FourthLimitingValue == null ? "" : item.FourthLimitingValue.ToString(),
                    OverLimitComputeType = item.EnumOverLimitComputeType,
                    Comment = item.Comment
                };

                if (item.EnumOverLimitComputeType == OverLimitComputeTypeEnum.Realtime) {
                    currentEntity.LowLimit1Value = item.LowLimit1Tag;
                    currentEntity.LowLimit2Value = item.LowLimit2Tag;
                    currentEntity.LowLimit3Value = item.LowLimit3Tag;
                    currentEntity.FirstLimitingValue = item.FirstLimitingTag;
                    currentEntity.SecondLimitingValue = item.SecondLimitingTag;
                    currentEntity.ThirdLimitingValue = item.ThirdLimitingTag;
                    currentEntity.FourthLimitingValue = item.FourthLimitingTag;
                }

                resultList.Add(currentEntity);
            }
            return resultList;
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

        private void BindKpiData() {
            List<KPI_RealTagEntity> realTagList = overLimitConfigDal.GetRealTagList();
            realTagList.Insert(0, new KPI_RealTagEntity { RealID = "T01", RealDesc = "选择标签名称" });
            drpTags.DataSource = realTagList;
            drpTags.DataTextField = "RealDesc";
            drpTags.DataValueField = "RealID";
            drpTags.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            string tagName = drpTags.SelectedValue;
            if (rbFixedValue.Checked) {
                SaveFixedParaConfigEntity(tagName);
            }
            else if (rbRealTimeValue.Checked) {
                SaveReadTimeConfigEntity(tagName);
            }
        }

        public void SaveReadTimeConfigEntity(string tagName) {
            string errorInfo = null;
            OverLimitConfigEntity overLimitConfig = InitRealTimeTypeConfigEntity(tagName, out errorInfo);
            if (errorInfo != null) {
                // Response.Write(string.Format("<script>alert('{0}');window.opener.location.href=window.opener.location.href;</script>", errorInfo));
                return;
            }
            if (hidFlag.Value == "INSERT") {
                if (overLimitConfigDal.Insert(overLimitConfig)) {
                    this.Pager.CurrentPageIndex = 1;
                    BindingData(this.Pager.CurrentPageIndex, this.Pager.PageSize);
                }
            }
            else {
                overLimitConfig.OverLimitConfigID = hidCode.Value;
                if (overLimitConfigDal.Update(overLimitConfig)) {
                    hidCode.Value = string.Empty;
                    this.Pager.CurrentPageIndex = 1;
                    BindingData(this.Pager.CurrentPageIndex, this.Pager.PageSize);
                }
            }
        }

        private void SaveFixedParaConfigEntity(string tagName) {
            OverLimitConfigEntity overLimitConfig = GetFixedParaConfigEntity(tagName);

            if (hidFlag.Value == "INSERT") {
                if (overLimitConfigDal.Insert(overLimitConfig)) {
                    this.Pager.CurrentPageIndex = 1;
                    BindingData(this.Pager.CurrentPageIndex, this.Pager.PageSize);
                }
            }
            else {
                overLimitConfig.OverLimitConfigID = hidCode.Value;
                if (overLimitConfigDal.Update(overLimitConfig)) {
                    hidCode.Value = string.Empty;
                    this.Pager.CurrentPageIndex = 1;
                    BindingData(this.Pager.CurrentPageIndex, this.Pager.PageSize);
                }
            }
        }

        private OverLimitConfigEntity InitRealTimeTypeConfigEntity(string tagName, out string errorInfo) {
            errorInfo = null;
            decimal? firstLimiting = null, secondLimiting = null,
               thirdLimiting = null, FourthLimiting = null,
               lowLimit1Value = null, lowLimit2Value = null, lowLimit3Value = null;

            string lowLimit1Tag = null;
            string lowLimit2Tag = null;
            string lowLimit3Tag = null;
            string firstLimitingTag = null;
            string secondLimitingTag = null;
            string thirdLimitingTag = null;
            string fourthLimitingTag = null;

            ///string baseInfo = "在PI中没有找到对应{0}测点";
            if (!string.IsNullOrWhiteSpace(txtLowLimit1Value.Text)) {
                lowLimit1Tag = txtLowLimit1Value.Text.Trim();
                //if (!RTDataAccess.ExistPoint(lowLimit1Tag))
                //{
                //    errorInfo = string.Format(baseInfo, "低一");
                //    return null;
                //}
            }

            if (!string.IsNullOrWhiteSpace(txtLowLimit2Value.Text)) {
                lowLimit2Tag = txtLowLimit2Value.Text.Trim();
                //if (!RTDataAccess.ExistPoint(lowLimit2Tag))
                //{
                //    errorInfo = string.Format(baseInfo, "低二");
                //    return null;
                //}
            }

            if (!string.IsNullOrWhiteSpace(txtLowLimit3Value.Text)) {
                lowLimit3Tag = txtLowLimit3Value.Text.Trim();
                //if (!RTDataAccess.ExistPoint(lowLimit3Tag))
                //{
                //    errorInfo = string.Format(baseInfo, "低三");
                //    return null;
                //}
            }

            if (!string.IsNullOrWhiteSpace(txtFirstLimiting.Text)) {
                firstLimitingTag = txtFirstLimiting.Text.Trim();
                //if (!RTDataAccess.ExistPoint(firstLimitingTag))
                //{
                //    errorInfo = string.Format(baseInfo, "高一");
                //    return null;
                //}
            }

            if (!string.IsNullOrWhiteSpace(txtSecondLimiting.Text)) {
                secondLimitingTag = txtSecondLimiting.Text.Trim();
                //if (!RTDataAccess.ExistPoint(secondLimitingTag))
                //{
                //    errorInfo = string.Format(baseInfo, "高二");
                //    return null;
                //}
            }

            if (!string.IsNullOrWhiteSpace(txtThirdLimiting.Text)) {
                thirdLimitingTag = txtThirdLimiting.Text.Trim();
                //if (!RTDataAccess.ExistPoint(thirdLimitingTag))
                //{
                //    errorInfo = string.Format(baseInfo, "高三");
                //    return null;
                //}
            }

            if (!string.IsNullOrWhiteSpace(txtFourthLimiting.Text)) {
                fourthLimitingTag = txtFourthLimiting.Text.Trim();
                //if (!RTDataAccess.ExistPoint(fourthLimitingTag))
                //{
                //    errorInfo = string.Format(baseInfo, "高四");
                //    return null;
                //}
            }

            string comment = txtComment.Text;
            OverLimitConfigEntity overLimitConfig = new OverLimitConfigEntity {
                TagName = tagName,
                FirstLimitingValue = firstLimiting,
                SecondLimitingValue = secondLimiting,
                ThirdLimitingValue = thirdLimiting,
                FourthLimitingValue = FourthLimiting,
                LowLimit1Value = lowLimit1Value,
                LowLimit2Value = lowLimit2Value,
                LowLimit3Value = lowLimit3Value,

                FirstLimitingTag = firstLimitingTag,
                SecondLimitingTag = secondLimitingTag,
                ThirdLimitingTag = thirdLimitingTag,
                FourthLimitingTag = fourthLimitingTag,
                LowLimit1Tag = lowLimit1Tag,
                LowLimit2Tag = lowLimit2Tag,
                LowLimit3Tag = lowLimit3Tag,

                Comment = comment,
                EnumOverLimitComputeType = OverLimitComputeTypeEnum.Realtime,
                OverLimitComputeType = 1
            };
            return overLimitConfig;
        }

        private OverLimitConfigEntity GetFixedParaConfigEntity(string tagName) {
            decimal? firstLimiting = null, secondLimiting = null,
                thirdLimiting = null, FourthLimiting = null,
                lowLimit1Value = null, lowLimit2Value = null, lowLimit3Value = null;
            if (!string.IsNullOrWhiteSpace(txtLowLimit1Value.Text)) {
                lowLimit1Value = Convert.ToDecimal(txtLowLimit1Value.Text);
            }
            if (!string.IsNullOrWhiteSpace(txtLowLimit2Value.Text)) {
                lowLimit2Value = Convert.ToDecimal(txtLowLimit2Value.Text);
            }
            if (!string.IsNullOrWhiteSpace(txtLowLimit3Value.Text)) {
                lowLimit3Value = Convert.ToDecimal(txtLowLimit3Value.Text);
            }

            if (!string.IsNullOrWhiteSpace(txtFirstLimiting.Text)) {
                firstLimiting = Convert.ToDecimal(txtFirstLimiting.Text);
            }
            if (!string.IsNullOrWhiteSpace(txtSecondLimiting.Text)) {
                secondLimiting = Convert.ToDecimal(txtSecondLimiting.Text);
            }
            if (!string.IsNullOrWhiteSpace(txtThirdLimiting.Text)) {
                thirdLimiting = Convert.ToDecimal(txtThirdLimiting.Text);
            }
            if (!string.IsNullOrWhiteSpace(txtFourthLimiting.Text)) {
                FourthLimiting = Convert.ToDecimal(txtFourthLimiting.Text);
            }

            string comment = txtComment.Text;

            OverLimitConfigEntity overLimitConfig = new OverLimitConfigEntity {
                TagName = tagName,
                FirstLimitingValue = firstLimiting,
                SecondLimitingValue = secondLimiting,
                ThirdLimitingValue = thirdLimiting,
                FourthLimitingValue = FourthLimiting,
                LowLimit1Value = lowLimit1Value,
                LowLimit2Value = lowLimit2Value,
                LowLimit3Value = lowLimit3Value,
                Comment = comment,
                EnumOverLimitComputeType = OverLimitComputeTypeEnum.FixedPara,
                OverLimitComputeType = 0,


                FirstLimitingTag = "",
                SecondLimitingTag = "",
                ThirdLimitingTag = "",
                FourthLimitingTag = "",
                LowLimit1Tag = "",
                LowLimit2Tag = "",
                LowLimit3Tag = "",
            };
            return overLimitConfig;
        }

        protected void btnSumbit_Click(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(hidCode.Value)) {
                string deleteId = hidCode.Value;
                if (overLimitConfigDal.Delete(deleteId)) {
                    hidCode.Value = string.Empty;
                    this.Pager.CurrentPageIndex = 1;
                    BindingData(this.Pager.CurrentPageIndex, this.Pager.PageSize);
                }
            }
            else if (!string.IsNullOrWhiteSpace(hidMultipleCode.Value)) {
                string[] deleteItem = hidMultipleCode.Value.Remove(0, 1).Split('&');
                int i = 0;
                foreach (var item in deleteItem) {
                    if (overLimitConfigDal.Delete(item)) {
                        i++;
                    }
                    else {
                        i--;
                    }
                }

                if (i == deleteItem.Length) {
                    hidMultipleCode.Value = string.Empty;
                    this.Pager.CurrentPageIndex = 1;
                    BindingData(this.Pager.CurrentPageIndex, this.Pager.PageSize);
                }
            }
        }

        protected void btnNew_Click(Object Sender, EventArgs e) {
        }

        protected void btnEdit_Click(Object Sender, EventArgs e) {

        }

        protected void btnDelete_Click(Object Sender, EventArgs e) {

        }

   
        protected void btnCancel_Click(Object Sender, EventArgs e) {

        }
    }

    public class UIOverLimitConfig {
        /// <summary>
        /// 主键
        /// </summary>
        public string OverLimitConfigID { get; set; }

        /// <summary>
        /// 标签ID
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string RealDesc { get; set; }

        /// <summary>
        /// 高一限
        /// </summary>
        public string FirstLimitingValue { get; set; }

        /// <summary>
        /// 高二限
        /// </summary>
        public string SecondLimitingValue { get; set; }

        /// <summary>
        /// 高三限
        /// </summary>
        public string ThirdLimitingValue { get; set; }

        /// <summary>
        /// 高四限
        /// </summary>
        public string FourthLimitingValue { get; set; }

        /// <summary>
        /// 低一限
        /// </summary>
        public string LowLimit1Value { get; set; }

        /// <summary>
        /// 低二限
        /// </summary>
        public string LowLimit2Value { get; set; }

        /// <summary>
        /// 低三限
        /// </summary>
        public string LowLimit3Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// 测点代码
        /// </summary>
        public String TagCode {
            get;
            set;
        }

        /// <summary>
        /// 测点描述
        /// </summary>
        public String TagDesc {
            get;
            set;
        }

        /// <summary>
        /// 机组编号
        /// </summary>
        public String UnitID {
            get;
            set;
        }

        public string OverLimitComputeTypeText {
            get {
                if (OverLimitComputeType == OverLimitComputeTypeEnum.FixedPara) {
                    return "固定值";
                }
                return "实时曲线";
            }
        }

        public OverLimitComputeTypeEnum OverLimitComputeType { get; set; }
    }
}