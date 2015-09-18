using System;
using System.Collections.Generic;
using System.Text;

using SIS.DataAccess;
using SIS.Loger;

namespace SIS.Assistant.WS {
	/// <summary>
	/// 主方法
	/// </summary>
	public class WS_RaceMainMethod {

		private WS_RaceSubMethod m_RaceSub;

		public WS_RaceMainMethod() {
			m_RaceSub = new WS_RaceSubMethod();
		}

		public  void RaceAppRun(bool bSnap) {
			try {
				//执行关闭动作，保证数据及属性最新
				//DBControl.DBAccess.GetRealTime(true);

				//检查数据库连接
				//及参数配置
				//是否满足要求
				if (m_RaceSub.CanRun()) {
					m_RaceSub.UnitAnalysis(bSnap);
				}
			}
			catch (Exception ex) {
				LogUtil.LogMessage(ex);
				return;
			}
		}

	}
}
