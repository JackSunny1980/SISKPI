using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DBControl;
using System.Data;

namespace SIS.DataAccess {
	
	/// <summary>
	/// 计算奖金与得分
	/// </summary>
	public class ScoreAndBonusDAL:IDisposable {

		private RelaInterface m_DB;

		public ScoreAndBonusDAL() {
			m_DB = DBAccess.GetRelation();
		}

		/// <summary>
		/// 计算各值奖金与得分、个人奖金与得分
		/// </summary>
		public void CalcScoreAndBonus() {
			try {
				m_DB.ExecuteNonQuery("Proc_CalcScoreAndBonus", CommandType.StoredProcedure, null);
			}
			catch (Exception ex) {
				throw ex;
			}
		}

		public void Dispose() {
			
		}
	}
}
