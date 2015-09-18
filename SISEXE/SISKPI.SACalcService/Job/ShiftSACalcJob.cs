using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;

namespace SISKPI.SACalcService.Job {

	internal class ShiftSACalcJob : IJob {
		public void Execute(IJobExecutionContext context) {
			using (SACalculate Service = new SACalculate()) {
				Service.CalcShiftSAScore();
			}
		}
	}
}
