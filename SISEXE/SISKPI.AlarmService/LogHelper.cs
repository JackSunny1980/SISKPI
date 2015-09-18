using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.IO;

namespace SISKPI.AlarmService {

	internal class LogHelper {
		private ILog m_Loger;
		//private ILog m_SACalcLoger;
		private static LogHelper m_Instance = null;
		private static Object lockobj = new object();

		private LogHelper() {
			String logConfigFile = AppDomain.CurrentDomain.BaseDirectory + "log4net.config";
			log4net.Config.XmlConfigurator.Configure(new FileInfo(logConfigFile));
			m_Loger = LogManager.GetLogger("Default");
			//m_SACalcLoger = LogManager.GetLogger("SA");
		}



		internal static ILog Logger {
			get {
				lock (lockobj) {
					if (m_Instance == null)
						m_Instance = new LogHelper();
				}
				return m_Instance.m_Loger;
			}
		}

		//internal static ILog SACalcLogger {
		//    get {
		//        lock (lockobj) {
		//            if (m_Instance == null)
		//                m_Instance = new LogHelper();
		//        }
		//        return m_Instance.m_SACalcLoger;
		//    }
		//}
		
	}
}
