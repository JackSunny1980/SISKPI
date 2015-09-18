using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIS.DataEntity;

namespace SISKPI.KPIAlarm.Helper
{
    public class ConverterHelper
    {
        public static int ConverterMinute(int second)
        {

            int minute = second / 60;
            if (minute == 0)
            {
                minute = 1;
            }
            return minute;
        }

        public static string ConverterAlarmType(int typeFlag)
        {
            switch (typeFlag)
            {
                case 1:
                    return "高一限";
				case 2:
					return "高二限";
				case 3:
					return "高三限";
				case 4:
					return "高四限";
				case -1:
					return "低一限";
				case -2:
					return "低二限";
                case -3:
					return "低三限";
                default:
                    throw new InvalidOperationException("类型不匹配");
            }
        }

        public static List<KPI_OverLimitRecordEntity> ConverterItem(List<KPI_OverLimitRecordEntity> overLimitRecordList)
        {
            if (null == overLimitRecordList)
                throw new ArgumentNullException("overLimitRecordList is null.");
            foreach (var item in overLimitRecordList)
            {
                item.Duration = item.Duration == null ? null : (int?)ConverterMinute((int)item.Duration);
                item.AlarmTypeName = ConverterAlarmType((int)item.AlarmType);
            }

            return overLimitRecordList;
        }
    }
}