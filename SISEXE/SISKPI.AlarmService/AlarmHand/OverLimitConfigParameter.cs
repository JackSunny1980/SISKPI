using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SISKPI.AlarmService.AlarmHand
{
    /// <summary>
    /// 超限计算参数
    /// </summary>
    public class OverLimitConfigParameter
    {
        public double High1 = double.MaxValue;

        public double High2 = double.MaxValue;

        public double High3 = double.MaxValue;

        public double High4 = double.MaxValue;

        public double Low1 = double.MinValue;

        public double Low2 = double.MinValue;

        public double Low3 = double.MinValue;
    }
}
