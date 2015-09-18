using SIS.DataEntity;
using SIS.DBControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SISKPI.AlarmService.AlarmHand {
    public class FixedParaOverLimitExecutor : AbstractOverLimitExecutor {
        protected override void Calculate(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList) {
            OverLimitConfigParameter parameter = InitOverLimitParameter(overLimitConfig);

            if (parameter.High4 != double.MaxValue) H4(overLimitConfig, tagAttributeList, parameter.High4);
            if (parameter.High3 != double.MaxValue) H3(overLimitConfig, tagAttributeList, parameter.High3, parameter.High4);
            if (parameter.High2 != double.MaxValue) H2(overLimitConfig, tagAttributeList, parameter.High2, parameter.High3);
            if (parameter.High1 != double.MaxValue) H1(overLimitConfig, tagAttributeList, parameter.High1, parameter.High2);

            if (parameter.Low1 != double.MinValue) L1(overLimitConfig, tagAttributeList, parameter.Low2, parameter.Low1);
            if (parameter.Low2 != double.MinValue) L2(overLimitConfig, tagAttributeList, parameter.Low3, parameter.Low2);
            if (parameter.Low3 != double.MinValue) L3(overLimitConfig, tagAttributeList, parameter.Low3);
        }

        private OverLimitConfigParameter InitOverLimitParameter(OverLimitConfigEntity faultConfiguration) {
            OverLimitConfigParameter parameter = new OverLimitConfigParameter();

            if (faultConfiguration.LowLimit1Value != null) parameter.Low1 = Convert.ToDouble(faultConfiguration.LowLimit1Value.Value);//低1限值
            if (faultConfiguration.LowLimit2Value != null) parameter.Low2 = Convert.ToDouble(faultConfiguration.LowLimit2Value.Value);//低2限值
            if (faultConfiguration.LowLimit3Value != null) parameter.Low3 = Convert.ToDouble(faultConfiguration.LowLimit3Value.Value);//低3限值

            if (faultConfiguration.FirstLimitingValue != null) parameter.High1 = Convert.ToDouble(faultConfiguration.FirstLimitingValue.Value);//高1限值
            if (faultConfiguration.SecondLimitingValue != null) parameter.High2 = Convert.ToDouble(faultConfiguration.SecondLimitingValue.Value);//高2限值
            if (faultConfiguration.ThirdLimitingValue != null) parameter.High3 = Convert.ToDouble(faultConfiguration.ThirdLimitingValue.Value);//高3限值
            if (faultConfiguration.FourthLimitingValue != null) parameter.High4 = Convert.ToDouble(faultConfiguration.FourthLimitingValue.Value);//高4限值

            return parameter;
        }

        #region 报警参数比较方法

        private void H4(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList, double high4) {
            double maxValue = tagAttributeList.Max(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                if (item.TagDoubleValue > high4) {
                    AddOrUpdateOverLimitRecordToDB(overLimitConfig, 4, item.TimeStamp, high4, maxValue);
                }
                else {
                    UpdateExistOverLimitRecordToDB(overLimitConfig, 4, item.TimeStamp);
                }
            }
        }

        private void H3(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList,
            double lowValue, double hightValue) {
            double maxValue = tagAttributeList.Max(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                if ((item.TagDoubleValue > lowValue) && (item.TagDoubleValue <= hightValue)) {
                    AddOrUpdateOverLimitRecordToDB(overLimitConfig, 3, item.TimeStamp, lowValue, maxValue);
                }
                else {
                    UpdateExistOverLimitRecordToDB(overLimitConfig, 3, item.TimeStamp);
                }
            }
        }

        private void H2(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList,
            double lowValue, double hightValue) {
            double maxValue = tagAttributeList.Max(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                if ((item.TagDoubleValue > lowValue) && (item.TagDoubleValue <= hightValue)) {
                    AddOrUpdateOverLimitRecordToDB(overLimitConfig, 2, item.TimeStamp, lowValue, maxValue);
                }
                else {
                    UpdateExistOverLimitRecordToDB(overLimitConfig, 2, item.TimeStamp);
                }
            }
        }

        private void H1(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList,
            double lowValue, double hightValue) {
            double maxValue = tagAttributeList.Max(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                if ((item.TagDoubleValue > lowValue) && (item.TagDoubleValue <= hightValue)) {
                    AddOrUpdateOverLimitRecordToDB(overLimitConfig, 1, item.TimeStamp, lowValue, maxValue);
                }
                else {
                    UpdateExistOverLimitRecordToDB(overLimitConfig, 1, item.TimeStamp);
                }
            }
        }

        private void L1(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList,
            double lowValue, double hightValue) {
            double minValue = tagAttributeList.Min(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                if ((item.TagDoubleValue < hightValue) && (item.TagDoubleValue >= lowValue)) {
                    AddOrUpdateOverLimitRecordToDB(overLimitConfig, -1, item.TimeStamp, hightValue, minValue);
                }
                else {
                    UpdateExistOverLimitRecordToDB(overLimitConfig, -1, item.TimeStamp);
                }
            }
        }


        private void L2(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList,
            double lowValue, double hightValue) {
            double minValue = tagAttributeList.Min(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                if ((item.TagDoubleValue < hightValue) && (item.TagDoubleValue >= lowValue)) {
                    AddOrUpdateOverLimitRecordToDB(overLimitConfig, -2, item.TimeStamp, hightValue, minValue);
                }
                else {
                    UpdateExistOverLimitRecordToDB(overLimitConfig, -2, item.TimeStamp);
                }
            }
        }

        private void L3(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList,
            double standardValue) {
            double minValue = tagAttributeList.Min(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                if (item.TagDoubleValue <= standardValue) {
                    AddOrUpdateOverLimitRecordToDB(overLimitConfig, -3, item.TimeStamp, standardValue, minValue);
                }
                else {
                    UpdateExistOverLimitRecordToDB(overLimitConfig, -3, item.TimeStamp);
                }
            }
        }

        #endregion
    }
}
