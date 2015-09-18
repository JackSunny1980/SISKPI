using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DataEntity;
using System.Data;
using System.Data.SqlClient;
using SIS.DBControl;

namespace SIS.DataAccess {

	/// <summary>
	/// 值监盘时间长数据访问对象
	/// </summary>
	public class MonitorDurationDal : IDisposable {

		private RelaInterface m_DB;

        public MonitorDurationDal() {
			m_DB = DBAccess.GetRelation();
		}

        public void SaveMonitorDuration(MonitorDurationEntity MonitorDuration) {
            if (MonitorDuration.Duration <= 0) return;
			IDbDataParameter[] parames = new SqlParameter[] {
              	new SqlParameter("@UnitID",DbType.String),
                new SqlParameter("@Shift",DbType.String),
                new SqlParameter("@CheckDate",DbType.Date),  
                new SqlParameter("@Duration",DbType.Decimal)};
			parames[0].Value = MonitorDuration.UnitID;
			parames[1].Value = MonitorDuration.Shift;
			parames[2].Value = MonitorDuration.CheckDate;
			parames[3].Value = MonitorDuration.Duration;
			String SqlText = @"IF NOT EXISTS(SELECT * FROM KPI_MonitorDuration WHERE UnitID=@UnitID AND CheckDate=@CheckDate AND Shift=@Shift)
								BEGIN
								  INSERT INTO KPI_MonitorDuration(UnitID,Shift,CheckDate,Duration)VALUES(@UnitID,@Shift,@CheckDate,@Duration)
								END";
			m_DB.ExecuteNonQuery(SqlText, parames);
		}


		public void Dispose() {
			m_DB = null;
		}
	}
}
