using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using SISKPI.AlarmService.AlarmHand;

namespace SISKPI.AlarmService.Job {

    /// <summary>
    /// 安全超限报警计算
    /// </summary>
    internal class AlarmJob : IJob {

        //public void Execute(IJobExecutionContext context)
        //{
        //    using (AlarmProductor Service = new AlarmProductor())
        //    {
        //        Service.AlarmInterval = 10;
        //        Service.PorcessExceedLimit();
        //    }
        //} 

        public void Execute(IJobExecutionContext context) {
            //using (OverLimitProxy Service = new OverLimitProxy()) {
            //    Service.AlarmInterval = 10;
            //    Service.Calculate();
            //}
        }
    }
}
