using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;

namespace SISKPI.SACalcService.Job {

	/// <summary>
	/// 机组安全得分计算
	/// </summary>
	internal class SACalcJob : IJob {

		public void Execute(IJobExecutionContext context) {
			using (SACalculate Service = new SACalculate()) {
				Service.Calculation();
			}
		}
	}
}
