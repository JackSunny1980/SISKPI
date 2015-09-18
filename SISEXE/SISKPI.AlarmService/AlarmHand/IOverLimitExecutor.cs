using SIS.DataEntity;
using SIS.DBControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SISKPI.AlarmService.AlarmHand
{
    /// <summary>
    /// 超限统计接口
    /// </summary>
    public interface IOverLimitExecutor
    {
        void InitCalculate(RTInterface m_DataAccess, OverLimitConfigEntity overLimitConfig, DateTime startDate, DateTime endDate);
    }
}
