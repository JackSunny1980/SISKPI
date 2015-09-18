using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
using System.Data.SqlClient;


namespace SIS.DataAccess {
	public class KPI_PersonDal : DalBase<KPI_PersonEntity>, IDisposable {

		private RelaInterface m_DB;


		public KPI_PersonDal() {
			m_DB = DBAccess.GetRelation();
		}
		/// <summary>
		/// 删除数据
		/// </summary>
		/// <returns></returns>
		public static bool DeletePerson(string PersonID) {
			//删除参数信息
			string sql = "delete from KPI_Person ";
			if (PersonID != "") {
				sql += " where PersonID = '" + PersonID + "'";
			}

			return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
		}


		/// <summary>
		/// 获得记录个数
		/// </summary>
		/// <returns></returns>
		public static int PersonIDCounts() {
			string sql = "select PersonID from KPI_Person";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
		}


		/// <summary>
		/// 得到所有子表数据
		/// </summary>
		/// <returns></returns>
		public static DataTable GetDataTable() {
			string sql = "select * from KPI_Person";
			DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			return dataTable;
		}

		/// <summary>
		/// 判断名称的唯一性
		/// </summary>
		/// <param name="PersonName"></param>
		/// <param name="PersonID"></param>
		/// <returns></returns>
		public static bool PersonCodeExists(string PersonCode, string PersonID) {
			string sql = "select count(1) from KPI_Person where 1=1 and PersonCode='{0}' ";
			sql = string.Format(sql, PersonCode);

			if (PersonID != "")
				sql = sql + " and PersonID <> '" + PersonID + "'";

			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
		}

		/// <summary>
		/// 获得名称
		/// </summary>
		/// <param name="PersonName"></param>
		/// <param name="PersonID"></param>
		/// <returns></returns>
		public static string GetPersonName(string PersonID) {
			string sql = "select PersonID, PersonName from KPI_Person where PersonID='{0}' ";
			sql = string.Format(sql, PersonID);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			if (dt == null || dt.Rows.Count <= 0) {
				return "";
			}
			else {

				return dt.Rows[0]["PersonName"].ToString();
			}
		}

		/// <summary>
		/// 获得名称
		/// </summary>
		/// <param name="PersonName"></param>
		/// <param name="PersonID"></param>
		/// <returns></returns>
		public static string GetPersonID(string PersonCode) {
			string sql = "select PersonID, PersonCode from KPI_Person where PersonCode='{0}' ";
			sql = string.Format(sql, PersonCode);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			if (dt == null || dt.Rows.Count <= 0) {
				return "";
			}
			else {
				return dt.Rows[0]["PersonID"].ToString();
			}
		}


		/// <summary>
		/// 获得岗位ID
		/// </summary>
		/// <param name="PersonName"></param>
		/// <param name="PersonID"></param>
		/// <returns></returns>
		public static string GetPositionID(string PersonID) {
			string sql = "select PositionID from KPI_Person where PersonID='{0}' ";
			sql = string.Format(sql, PersonID);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			if (dt == null || dt.Rows.Count <= 0) {
				return "";
			}
			else {
				return dt.Rows[0]["PositionID"].ToString();
			}
		}

		/// <summary>
		/// 得到有效的定义
		/// </summary>
		/// <param name="PersonID">主键</param>
		/// <returns></returns>
		public static DataTable GetPersons() {
			string sql = "select PersonID[ID], PersonName[Name]  from KPI_Person where PersonIsValid=1";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}


		/// <summary>
		/// 通过主键得到实体对象
		/// </summary>
		/// <param name="PersonID">主键</param>
		/// <returns></returns>
		public static KPI_PersonEntity GetEntity(string PersonID) {
			KPI_PersonEntity entity = new KPI_PersonEntity();

			string sql = "select * from KPI_Person where PersonID='{0}'";
			sql = string.Format(sql, PersonID);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count > 0) {
				entity.DrToMember(dt.Rows[0]);
			}

			return entity;
		}


		/// <summary>
		/// 得到查询结果
		/// </summary>
		/// <param name="condition">条件字符串</param>
		/// <returns></returns>
		public static DataTable GetPersonList() {
			string sql = @"select PersonID, PersonCode, PersonName, PersonDesc, PositionID, PersonIsValid, PersonNote
                            from KPI_Person";
			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}



		/// <summary>
		/// 得到查询结果
		/// </summary>
		/// <param name="condition">条件字符串</param>
		/// <returns></returns>
		public static DataTable GetSearchList(string condition) {
			string sql = @"select a.PersonID, PersonCode, PersonName, ShiftName, PositionName, PositionWeight, 
                            ''ECScore, ''SASCore, ''AllScore, ''PersonSort, ''PersonMoney
                            from KPI_Person  a
                            left outer join KPI_Position b on b.PositionID = a.PositionID
                            left outer join KPI_Team c on c.PersonID = a.PersonID
                            left outer join KPI_Shift d on d.ShiftID = c.ShiftID
                            where 1=1 {0}";

			sql = string.Format(sql, condition);
			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			return dt;
		}


		/// <summary>
		/// 得到人员列表为导出使用
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetPersonListForExcel() {
			string sql = @"select 'x'SelectX, PositionName, PersonCode, PersonName, PersonDesc, PersonIsValid, PersonNote
                            from KPI_Person a
                            left outer join KPI_Position b on ( a.PositionID=b.PositionID)
                            order by PersonCode";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

        public static DataTable GetPersons(String UnitID,String Shift) {
            String sql = @"SELECT A.PersonID,A.PersonName,A.Shift,A.SpecialField,B.PositionName,C.UnitName 
                           FROM KPI_Person A 
                           JOIN KPI_Position B ON A.PositionID = B.PositionID
                           JOIN KPI_Unit C ON A.UnitID=C.UnitID
                           WHERE A.UnitID='{0}' AND A.Shift='{1}' AND A.PersonIsValid =1";
            sql = String.Format(sql, UnitID,Shift);
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

		//Added by pyf 2013-11-01

		/// <summary>
		/// 获取运行人员配置数据
		/// </summary>
		/// <returns>运行人员配置实体</returns>
		public KPI_PersonEntity GetKPI_Person(String personID) {
			string SqlText = @"SELECT A.*,B.UnitName,C.PositionName FROM KPI_Person A 
                                        LEFT JOIN KPI_Unit     B ON A.UnitID=B.UnitID
                                        LEFT JOIN KPI_Position C ON A.PositionID = C.PositionID
                               WHERE  A.PersonID=@PersonID";
			KPI_PersonEntity Result = null;
			IDbDataParameter[] parames = new SqlParameter[] {
                new SqlParameter("@PersonID",DbType.String)};
			parames[0].Value = personID;
			using (IDataReader DataReader = m_DB.ExecuteReader(CommandType.Text, SqlText, parames)) {
				Result = DataReader.FillEntity<KPI_PersonEntity>();
				DataReader.Close();
			}
			return Result;
		}

        public List<KPI_PersonEntity> GetPersons(string specialField, string unitID,
            string shift,int PageIndex,int PageSize,out int RecordCount) {
			List<KPI_PersonEntity> Result = null;
			String Where = "";
            String SqlText = @"SELECT A.*,B.UnitName,C.PositionName,
                                      ROW_NUMBER() OVER(ORDER BY A.PersonID) OrderNo FROM KPI_Person A 
                                        LEFT JOIN KPI_Unit     B ON A.UnitID=B.UnitID
                                        LEFT JOIN KPI_Position C ON A.PositionID = C.PositionID";
			if (!String.IsNullOrEmpty(unitID)) Where = string.Format(" A.UnitID = '{0}'", unitID);
			if (!String.IsNullOrEmpty(shift)) Where += string.Format(" AND A.Shift = '{0}'", shift);
			if (!String.IsNullOrEmpty(specialField)) Where += string.Format(" AND A.specialField = '{0}'", specialField); 
			if ((Where.Length > 5) && (Where.Substring(1, 4) == "AND ")) Where = Where.Substring(4, Where.Length - 4);
			if (!string.IsNullOrEmpty(Where)) Where = "  WHERE " + Where;
			SqlText += Where;
            String RecordCountCTE = "WITH CTE AS (" + SqlText + " ) SELECT COUNT(PersonID) FROM CTE";
            RecordCount = Convert.ToInt32(m_DB.ExecuteScalar(RecordCountCTE));
            SqlText = @"WITH CTE AS (" + SqlText + " ) " +
                      @"SELECT * FROM CTE WHERE OrderNo BETWEEN (@PageIndex -1)*@PageSize AND @PageIndex * @PageSize";
            IDbDataParameter[] parames = new SqlParameter[] {
			   new SqlParameter("@PageIndex",DbType.Int32),
               new SqlParameter("@PageSize",DbType.Int32)};
            parames[0].Value = PageIndex;
            parames[1].Value = PageSize;
			using (IDataReader Reader = m_DB.ExecuteReader(CommandType.Text,SqlText,parames)) {
				Result = Reader.FillGenericList<KPI_PersonEntity>();
			}
			return Result;
		}

		/// <summary>
		/// 保存运行人员配置数据
		/// </summary>
		/// <param name="KPI_Person">运行人员配置实体</param>        
		/// <returns>成功保存的行数</returns>
		public int SavePerson(KPI_PersonEntity person) {
			int Result = 0;
			if (Exists(person)) {
				Result = UpdatePerson(person);
			}
			else {
				Result = AddPerson(person);
			}
			return Result;
		}

		/// <summary>
		/// 删除运行人员配置数据
		/// </summary>
		/// <param name="KPI_Person">运行人员配置实体</param>        
		/// <returns>删除的数据行数</returns>
		public int DeletePerson(KPI_PersonEntity person) {
			int Result = 0;
			string SqlText = @"DELETE FROM KPI_Person WHERE PersonID=@PersonID  ";
			IDbDataParameter[] parames = new SqlParameter[] {
                new SqlParameter("@PersonID",DbType.String)};
			parames[0].Value = person.PersonID;
			Result = m_DB.ExecuteNonQuery(SqlText, parames);
			return Result;
		}

		#region 私有方法

		/// <summary>
		/// 新增运行人员配置数据
		/// </summary>
		/// <param name="KPI_Person">运行人员配置实体</param>        
		/// <returns>新增的数据行数</returns>
		private int AddPerson(KPI_PersonEntity person) {
			int Result = 0;
			string SqlText = @"Insert KPI_Person (PersonID,PositionID,PersonCode,PersonName,PersonDesc,
							   PersonIsValid,PersonNote,UnitID,Shift,SpecialField) Values (@PersonID,@PositionID,
							   @PersonCode,@PersonName,@PersonDesc,@PersonIsValid,@PersonNote,@UnitID,@Shift,
							   @SpecialField) ";
			IDbDataParameter[] parames = new SqlParameter[] {
			   new SqlParameter("@PersonID",DbType.String),
               new SqlParameter("@PositionID",DbType.String),
			   new SqlParameter("@PersonCode",DbType.String),
			   new SqlParameter("@PersonName",DbType.String),
			   new SqlParameter("@PersonDesc",DbType.String),
			   new SqlParameter("@PersonIsValid",DbType.String),
			   new SqlParameter("@PersonNote",DbType.String),
			   new SqlParameter("@UnitID",DbType.String),
			   new SqlParameter("@Shift",DbType.String), 
			   new SqlParameter("@SpecialField",DbType.String)                              
            };
			parames[0].Value = Guid.NewGuid().ToString();
			parames[1].Value = person.PositionID;
			parames[2].Value = person.PersonCode;
			parames[3].Value = person.PersonName;
			parames[4].Value = person.PersonDesc;
			parames[5].Value = person.PersonIsValid;
			parames[6].Value = person.PersonNote;
			parames[7].Value = person.UnitID;
			parames[8].Value = person.Shift;
			parames[9].Value = person.SpecialField;
			
			Result = m_DB.ExecuteNonQuery(SqlText, parames);
			return Result;
		}


		/// <summary>
		/// 更新运行人员配置数据
		/// </summary>
		/// <param name="KPI_Person">运行人员配置实体</param>        
		/// <returns>更新的数据行数</returns>
		private int UpdatePerson(KPI_PersonEntity person) {
			int Result = 0;
			string SqlText = @"Update KPI_Person Set PositionID=@PositionID,PersonCode=@PersonCode,
								PersonName=@PersonName,PersonDesc=@PersonDesc,PersonIsValid=@PersonIsValid,
                                PersonNote=@PersonNote,UnitID=@UnitID,Shift=@Shift, 
                                SpecialField=@SpecialField  WHERE PersonID=@PersonID ";
			IDbDataParameter[] parames = new SqlParameter[] {
               new SqlParameter("@PositionID",DbType.String),
			   new SqlParameter("@PersonCode",DbType.String),
			   new SqlParameter("@PersonName",DbType.String),
			   new SqlParameter("@PersonDesc",DbType.String),
			   new SqlParameter("@PersonIsValid",DbType.String),
			   new SqlParameter("@PersonNote",DbType.String),
			   new SqlParameter("@UnitID",DbType.String),
			   new SqlParameter("@Shift",DbType.String), 
			   new SqlParameter("@SpecialField",DbType.String),  
               new SqlParameter("@PersonID",DbType.String)  
            };
			parames[0].Value = person.PositionID;
			parames[1].Value = person.PersonCode;
			parames[2].Value = person.PersonName;
			parames[3].Value = person.PersonDesc;
			parames[4].Value = person.PersonIsValid;
			parames[5].Value = person.PersonNote;
			parames[6].Value = person.UnitID;
			parames[7].Value = person.Shift;
			parames[8].Value = person.SpecialField;
			parames[9].Value = person.PersonID;
			Result = m_DB.ExecuteNonQuery(SqlText, parames);
			return Result;
		}


		/// <summary>
		/// 判断运行人员配置数据是否存在
		/// </summary>
		/// <param name="KPI_Person">运行人员配置实体</param>        
		/// <returns>数据存在则返回true否则返回false</returns>
		private bool Exists(KPI_PersonEntity person) {
			bool Result = false;
			string SqlText = "SELECT * FROM  KPI_Person WHERE  PersonID=@PersonID ";
			IDbDataParameter[] parames = new SqlParameter[] {
                new SqlParameter("@PersonID",DbType.String)};
			parames[0].Value = person.PersonID;
			using (IDataReader Reader = m_DB.ExecuteReader(CommandType.Text, SqlText, parames)) {
				Result = Reader.Read();
				Reader.Close();
			}
			return Result;
		}

		#endregion

		public void Dispose() {
			m_DB = null;
		}

		//End of Added.
	}
}
