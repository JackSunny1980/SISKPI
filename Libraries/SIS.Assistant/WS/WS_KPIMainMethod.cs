using System;
using System.Collections.Generic;
using System.Text;

using SIS.DataAccess;
using SIS.Loger;

namespace SIS.Assistant.WS {
	/// <summary>
	/// 主方法
	/// </summary>
	public class WS_KPIMainMethod {
		public int KIsTest = 0;
		private WS_KPISubMethod m_KPISubMethod;


		public WS_KPIMainMethod() {
			m_KPISubMethod = new WS_KPISubMethod();
		}

		/// <summary>
		/// 实时运行接口（计算实时KPI）
		/// </summary>
		/// <param name="bSnap">是否实时，bSnap=true为实时</param>
		public void KPIAppRunForPerformance(bool bSnap) {
			try {
				//检查数据库连接
				//及参数配置
				//是否满足要求
				if (!m_KPISubMethod.CanRun()) {
					LogUtil.LogMessage("数据库连接问题!!!");
					return;
				}
				m_KPISubMethod.KIsTest = KIsTest;//true为测试环境如果在测试环境由系统产生模拟数据。在配置文件中配置
				m_KPISubMethod.KPIRealCalc(bSnap);

			}
			catch (Exception ex) {
				LogUtil.LogMessage(ex);
				return;
			}
		}

		/// <summary>
		/// 实时运行接口（KPI历史重算）
		/// </summary>
		public void KPIAppRunForRecalculate() {
			try {
				m_KPISubMethod.KIsTest = KIsTest;
				m_KPISubMethod.KPIArchiveCalc();
			}
			catch (Exception ex) {
				LogUtil.LogMessage(ex);
				return;
			}
		}

	}
}
