using log4net;
using SIS.DataEntity;
using SIS.DataAccess;
using SIS.DBControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SISKPI.AlarmService.AlarmHand {
    public class OverLimitProxy : IDisposable {
        public int AlarmInterval {
            get;
            set;
        }
        private ILog m_Logger = LogHelper.Logger;

        private RTInterface m_DataAccess;
        protected RTInterface RTDataAccess {
            get {
                if (m_DataAccess == null) {
                    m_DataAccess = DBAccess.GetRealTime();
                }

                return m_DataAccess;
            }
        }

        public void Calculate() {
            try {
                List<OverLimitConfigEntity> faultOverLimitConfigList = new List<OverLimitConfigEntity>();
                m_Logger.Info("获取超限配置数据");
                try {
                    faultOverLimitConfigList = GetOverLimitConfigList();
                }
                catch (Exception ex) {
                    m_Logger.Error("获取超限配置数据错误", ex);
                }
                if (faultOverLimitConfigList == null || faultOverLimitConfigList.Count <= 0) {
                    return;
                }

                DateTime startDate;
                DateTime endDate;
                InitDateTime(out startDate, out endDate);
                m_Logger.InfoFormat("开始计算{0}至{1}的超限数据", startDate, endDate);
                BatchCalculate(faultOverLimitConfigList, startDate, endDate);
            }
            catch (Exception ex) {
                m_Logger.InfoFormat("PorcessExceedLimit安全指标超限报警计算错误，错误信息是:{0},调用栈信息是:{1}",
                    ex.Message, ex.StackTrace);
                m_Logger.Error(ex);
            }
        }

        private List<OverLimitConfigEntity> GetOverLimitConfigList() {
            IKPI_OverLimitConfigDal dataAccess = new KPI_OverLimitConfigDal();
            List<OverLimitConfigEntity> faultOverLimitConfigList = dataAccess.GetOverLimitConfigs();
            return faultOverLimitConfigList;
        }

        private void InitDateTime(out DateTime startDate, out DateTime endDate) {
            DateTime currentTime = DateTime.Now;
            endDate = currentTime.AddSeconds(-1 * currentTime.Second);
            startDate = endDate.AddMinutes(-1 * AlarmInterval);
        }

        private void BatchCalculate(List<OverLimitConfigEntity> overLimitConfiList, DateTime startDate, DateTime endDate) {
            foreach (OverLimitConfigEntity overLimitConfig in overLimitConfiList) {
                if (overLimitConfig != null) {
                    IOverLimitExecutor executor = null;
                    if (overLimitConfig.OverLimitComputeType == 0) {
                        executor = new FixedParaOverLimitExecutor();
                    }
                    else {
                        executor = new RealTimeOverLimitExecutor();
                    }
                    executor.InitCalculate(RTDataAccess, overLimitConfig, startDate, endDate);
                }
            }
        }

        public void Dispose() {
            if (m_DataAccess != null) {
                m_DataAccess.Dispose();
            }
            m_DataAccess = null;
        }
    }
}
