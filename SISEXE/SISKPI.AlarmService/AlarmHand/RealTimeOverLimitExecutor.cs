using SIS.DataEntity;
using SIS.DBControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SISKPI.AlarmService.AlarmHand {
    public class RealTimeOverLimitExecutor : AbstractOverLimitExecutor {
        protected override void Calculate(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList) {
            if (!string.IsNullOrEmpty(overLimitConfig.FourthLimitingTag)) {
                H4(overLimitConfig, tagAttributeList);
            }
            if (!string.IsNullOrEmpty(overLimitConfig.ThirdLimitingTag)) {
                H3(overLimitConfig, tagAttributeList);
            }
            if (!string.IsNullOrEmpty(overLimitConfig.SecondLimitingTag)) {
                H2(overLimitConfig, tagAttributeList);
            }
            if (!string.IsNullOrEmpty(overLimitConfig.FirstLimitingTag)) {
                H1(overLimitConfig, tagAttributeList);
            }

            if (!string.IsNullOrEmpty(overLimitConfig.LowLimit1Tag)) {
                L1(overLimitConfig, tagAttributeList);
            }
            if (!string.IsNullOrEmpty(overLimitConfig.LowLimit2Tag)) {
                L2(overLimitConfig, tagAttributeList);
            }
            if (!string.IsNullOrEmpty(overLimitConfig.LowLimit3Tag)) {
                L3(overLimitConfig, tagAttributeList);
            }
        }

        private double GetTagValue(string tag, DateTime time) {
            return RTDataAccess.GetArchiveValue(tag, time);
        }

        #region 报警参数比较方法

        private void H4(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList) {
            double maxValue = tagAttributeList.Max(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                double high4 = GetTagValue(overLimitConfig.FourthLimitingTag, item.TimeStamp);

                if (item.TagDoubleValue > high4) {
                    AddOrUpdateOverLimitRecordToDB(overLimitConfig, 4, item.TimeStamp, high4, maxValue);
                }
                else {
                    UpdateExistOverLimitRecordToDB(overLimitConfig, 4, item.TimeStamp);
                }
            }
        }

        private void H3(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList) {
            double maxValue = tagAttributeList.Max(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                double High3 = GetTagValue(overLimitConfig.ThirdLimitingTag, item.TimeStamp);
                double High4 = double.MaxValue;
                if (string.IsNullOrEmpty(overLimitConfig.FourthLimitingTag)) {
                    High4 = GetTagValue(overLimitConfig.FourthLimitingTag, item.TimeStamp);
                }

                if ((item.TagDoubleValue > High3) && (item.TagDoubleValue <= High4)) {
                    AddOrUpdateOverLimitRecordToDB(overLimitConfig, 3, item.TimeStamp, High3, maxValue);
                }
                else {
                    UpdateExistOverLimitRecordToDB(overLimitConfig, 3, item.TimeStamp);
                }
            }
        }

        private void H2(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList) {
            double maxValue = tagAttributeList.Max(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                double High2 = GetTagValue(overLimitConfig.SecondLimitingTag, item.TimeStamp);
                double High3 = double.MaxValue;
                if (!string.IsNullOrEmpty(overLimitConfig.ThirdLimitingTag)) {
                    High3 = GetTagValue(overLimitConfig.ThirdLimitingTag, item.TimeStamp);
                }

                if ((item.TagDoubleValue > High2) && (item.TagDoubleValue <= High3)) {
                    AddOrUpdateOverLimitRecordToDB(overLimitConfig, 2, item.TimeStamp, High2, maxValue);
                }
                else {
                    UpdateExistOverLimitRecordToDB(overLimitConfig, 2, item.TimeStamp);
                }
            }
        }

        private void H1(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList) {
            double maxValue = tagAttributeList.Max(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                double High1 = GetTagValue(overLimitConfig.FirstLimitingTag, item.TimeStamp);
                double High2 = double.MaxValue;
                if (!string.IsNullOrEmpty(overLimitConfig.SecondLimitingTag)) {
                    High2 = GetTagValue(overLimitConfig.SecondLimitingTag, item.TimeStamp);
                }

                if ((item.TagDoubleValue > High1) && (item.TagDoubleValue <= High2)) {
                    AddOrUpdateOverLimitRecordToDB(overLimitConfig, 1, item.TimeStamp, High1, maxValue);
                }
                else {
                    UpdateExistOverLimitRecordToDB(overLimitConfig, 1, item.TimeStamp);
                }
            }
        }

        private void L1(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList) {
            double minValue = tagAttributeList.Min(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                double low2 = double.MinValue;
                if (!string.IsNullOrEmpty(overLimitConfig.LowLimit2Tag)) {
                    low2 = GetTagValue(overLimitConfig.LowLimit2Tag, item.TimeStamp);
                }
                double low1 = GetTagValue(overLimitConfig.LowLimit1Tag, item.TimeStamp);

                if ((item.TagDoubleValue < low1) && (item.TagDoubleValue >= low2)) {
                    AddOrUpdateOverLimitRecordToDB(overLimitConfig, -1, item.TimeStamp, low1, minValue);
                }
                else {
                    UpdateExistOverLimitRecordToDB(overLimitConfig, -1, item.TimeStamp);
                }
            }
        }

        private void L2(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList) {
            double minValue = tagAttributeList.Min(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                double low2 = GetTagValue(overLimitConfig.LowLimit2Tag, item.TimeStamp);
                double low3 = double.MinValue;
                if (!string.IsNullOrEmpty(overLimitConfig.LowLimit3Tag)) {
                    low3 = GetTagValue(overLimitConfig.LowLimit3Tag, item.TimeStamp);
                }

                if ((item.TagDoubleValue < low2) && (item.TagDoubleValue >= low3)) {
                    AddOrUpdateOverLimitRecordToDB(overLimitConfig, -2, item.TimeStamp, low2, minValue);
                }
                else {
                    UpdateExistOverLimitRecordToDB(overLimitConfig, -2, item.TimeStamp);
                }
            }
        }

        private void L3(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList) {
            double minValue = tagAttributeList.Min(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                double low3 = GetTagValue(overLimitConfig.LowLimit3Tag, item.TimeStamp);
                if (item.TagDoubleValue <= low3) {
                    AddOrUpdateOverLimitRecordToDB(overLimitConfig, -3, item.TimeStamp, low3, minValue);
                }
                else {
                    UpdateExistOverLimitRecordToDB(overLimitConfig, -3, item.TimeStamp);
                }
            }
        }

        #endregion
    }
}
