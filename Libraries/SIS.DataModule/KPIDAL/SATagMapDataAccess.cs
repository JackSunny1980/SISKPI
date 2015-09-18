using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DataEntity;
using SIS.DBControl;
using System.Data;
using System.Data.SqlClient;

namespace SIS.DataAccess {
	/// <summary>
	/// 数据访问类:KPI_SATagMapDataAccess
	/// 文  件  名:KPI_SATagMapDataAccess.cs
	/// 说      明:安全指标与测点对应关系表数据访问对象
	/// </summary>
	public class SATagMapDataAccess : IDisposable {

		private RelaInterface m_DB;

		#region 构造器

		public SATagMapDataAccess() {

			m_DB = DBAccess.GetRelation();
		}

		#endregion

		#region 公共方法

		/// <summary>
		///获取安全指标与测点对应关系表所有数据
		/// </summary>
		public List<SATagMapEntity> GetSATagMaps(String SAID) {
			List<SATagMapEntity> Result = null;
			string sqlText = @"SELECT A.*,B.RealCode,B.RealDesc FROM KPI_SATagMap A 
								JOIN KPI_RealTag B ON A.RealID=B.RealID WHERE SAID=@SAID";
			IDbDataParameter SAIDParam = new SqlParameter("@SAID", SAID);
			IDataReader Reader = m_DB.ExecuteReader(CommandType.Text, sqlText, SAIDParam);
			try {
				Result = Reader.FillGenericList<SATagMapEntity>();
			}
			finally {
				Reader.Close();
				Reader.Dispose();
			}

			return Result;
		}

		/// <summary>
		/// 保存安全指标与测点对应关系表数据
		/// </summary>
		/// <param name="KPI_SATagMap">安全指标与测点对应关系表实体</param>        
		/// <returns>成功保存的行数</returns>
		public int SaveSATagMap(SATagMapEntity SATagMap) {
			if (Exists(SATagMap)) return 0;
			return AddSATagMap(SATagMap);
		}

		/// <summary>
		/// 删除安全指标与测点对应关系表数据
		/// </summary>
		/// <param name="SAID">安全指标与测点对应关系表实体</param>        
		/// <returns>删除的数据行数</returns>
		public int DeleteSATagMap(String SAID) {
			int Result = 0;
			string SqlText = @"DELETE FROM KPI_SATagMap WHERE SAID=@SAID ";
			SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter("@SAID",SqlDbType.VarChar)};
			parames[0].Value = SAID;			
			Result = m_DB.ExecuteNonQuery(SqlText, parames);
			return Result;
		}

		#endregion

		#region 私有方法

		/// <summary>
		/// 新增安全指标与测点对应关系表数据
		/// </summary>
		/// <param name="SATagMap">安全指标与测点对应关系表实体</param>        
		/// <returns>新增的数据行数</returns>
		private int AddSATagMap(SATagMapEntity SATagMap) {
			int Result = 0;
			string SqlText = @"Insert KPI_SATagMap (SAID,RealID) Values (@SAID,@RealID) ";
			SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter("@SAID",SqlDbType.VarChar),
                new SqlParameter("@RealID",SqlDbType.VarChar)};
			parames[0].Value = SATagMap.SAID;
			parames[1].Value = SATagMap.RealID;
			Result = m_DB.ExecuteNonQuery(SqlText, parames);
			return Result;
		}

		/// <summary>
		/// 判断安全指标与测点对应关系表数据是否存在
		/// </summary>
		/// <param name="SATagMap">安全指标与测点对应关系表实体</param>        
		/// <returns>数据存在则返回true否则返回false</returns>
		private bool Exists(SATagMapEntity SATagMap) {			
			string SqlText = "SELECT SAID FROM  KPI_SATagMap WHERE SAID=@SAID AND RealID=@RealID ";
			SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter("@SAID",SqlDbType.VarChar),
                new SqlParameter("@RealID",SqlDbType.VarChar)};
			parames[0].Value = SATagMap.SAID;
			parames[1].Value = SATagMap.RealID;
			return m_DB.ExecuteScalar(SqlText, parames)!= null;			
		}

		#endregion

		#region IDisposable 成员

		public void Dispose() {

		}

		#endregion
	}
}
