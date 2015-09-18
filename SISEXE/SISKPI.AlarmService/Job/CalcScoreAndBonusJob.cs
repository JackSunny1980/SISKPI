using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using log4net;
using SIS.DataAccess;

namespace SISKPI.AlarmService.Job {

	internal class CalcScoreAndBonusJob : IJob {

		ILog m_Logger = LogHelper.Logger;

		public void Execute(IJobExecutionContext context) {
			using (ScoreAndBonusDAL DataAccess = new ScoreAndBonusDAL()) {
				try {
					DataAccess.CalcScoreAndBonus();
				}
				catch (Exception ex) {
					m_Logger.Error("计算奖金错误!", ex);
				}
			}
		}
	}
}
