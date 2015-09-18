using SIS.DataEntity;
using SIS.DBControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SIS.DataAccess {


    /// <summary>
    /// 数据访问类:ManagementScoreDataAccess
    /// 文  件  名:ManagementScoreDataAccess.cs
    /// 说      明:日常管理考核得分数据访问对象
    /// </summary>
    public class ManagementScoreDataAccess : IDisposable {

        private RelaInterface m_DataAccess = DBAccess.GetRelation();

        #region 构造器

        public ManagementScoreDataAccess() {

        }

        #endregion

        #region 公共方法

        /// <summary>
        ///获取日常管理考核得分所有数据
        ///<param name="Shift">值别</param>
        /// </summary>
        public List<ManagementScoreEntity> GetManagementScores(String Shift,String YearMonth) {
            List<ManagementScoreEntity> Result = null;
            string SqlText = @"SELECT A.*,B.InputDesc TagName FROM  KPI_ManagementScore A 
                               JOIN KPI_InputTag B ON A.TagID=B.InputID 
                               WHERE CONVERT(varchar(7),A.CheckDate,120)=@YearMonth AND Shift=@Shift";
            IDbDataParameter[] Parameters = new SqlParameter[] { 
                new SqlParameter("@YearMonth",SqlDbType.VarChar),
                new SqlParameter("@Shift",SqlDbType.Int)
            };
            Parameters[0].Value = YearMonth;
            Parameters[1].Value = Shift;
            using (IDataReader DataReader = m_DataAccess.ExecuteReader(CommandType.Text, SqlText, Parameters)) {
                Result = DataReader.FillGenericList<ManagementScoreEntity>();
            };
            return Result;
        }

        public List<ManagementScoreEntity> GetManagementScores(int PageIndex,int PageSize,String Shift,
            String YearMonth,out int RecordCount) {
            List<ManagementScoreEntity> Result = null;
            string SqlText = @"WITH CTE AS(
                                    SELECT A.*,B.InputDesc TagName,
                                    ROW_NUMBER() OVER(ORDER BY A.TagID,A.PersonID) AS R
                                    FROM  KPI_ManagementScore A  JOIN KPI_InputTag B ON A.TagID=B.InputID 
                                    WHERE CONVERT(varchar(7),A.CheckDate,120)=@YearMonth AND Shift=@Shift)
                              SELECT * FROM CTE WHERE R BETWEEN (@pageindex - 1) * @pagesize + 1 AND (@pageindex - 1) * @pagesize + @pagesize";
            IDbDataParameter[] Parameters = new SqlParameter[] { 
                new SqlParameter("@pageindex",SqlDbType.Int),
                new SqlParameter("@pagesize",SqlDbType.Int),
                new SqlParameter("@YearMonth",SqlDbType.VarChar),
                new SqlParameter("@Shift",SqlDbType.Int)
            };
            Parameters[0].Value = PageIndex;
            Parameters[1].Value = PageSize;
            Parameters[2].Value = YearMonth;
            Parameters[3].Value = Shift;
            using (IDataReader DataReader = m_DataAccess.ExecuteReader(CommandType.Text, SqlText, Parameters)) {
                Result = DataReader.FillGenericList<ManagementScoreEntity>();
            };
            SqlText = @"SELECT COUNT(PersonID) FROM  KPI_ManagementScore A 
                        JOIN KPI_InputTag B ON A.TagID=B.InputID 
                        WHERE CONVERT(varchar(7),A.CheckDate,120)=@YearMonth AND Shift=@Shift";
            RecordCount = (int)m_DataAccess.ExecuteScalar(SqlText, Parameters); 
            return Result;
        }


        /// <summary>
        /// 获取日常管理考核得分数据
        /// </summary>
        /// <param name="PersonID">个人编码</param> 
        /// <param name="CheckDate">考核日期</param> 
        /// <param name="TagID">指标编码</param> 
        /// <returns>日常管理考核得分实体</returns>
        public ManagementScoreEntity GetManagementScore(string PersonID, DateTime CheckDate, string TagID) {
            string SqlText = @"SELECT * FROM  KPI_ManagementScore WHERE PersonID=@PersonID AND 
                                CheckDate=@CheckDate AND TagID=@TagID ";
            IDbDataParameter[] Parameters = new SqlParameter[] { 
                new SqlParameter("@PersonID",SqlDbType.VarChar),
                new SqlParameter("@CheckDate",SqlDbType.Date),                 
                new SqlParameter("@TagID",SqlDbType.VarChar)
            };
            Parameters[0].Value = PersonID;
            Parameters[1].Value = CheckDate;
            Parameters[2].Value = TagID;
            ManagementScoreEntity Result = null;
            using (IDataReader DataReader = m_DataAccess.ExecuteReader(CommandType.Text, SqlText, Parameters)) {
                Result = DataReader.FillEntity<ManagementScoreEntity>();
            };
            return Result;
        }

        /// <summary>
        /// 保存日常管理考核得分数据
        /// </summary>
        /// <param name="ManagementScore">日常管理考核得分实体</param>        
        /// <returns>成功保存的行数</returns>
        public int SaveManagementScore(ManagementScoreEntity ManagementScore) {
            int Result = 0;
            if (Exists(ManagementScore)) {
                Result = UpdateManagementScore(ManagementScore);
            }
            else {
                Result = AddManagementScore(ManagementScore);
            }
            return Result;
        }

        /// <summary>
        /// 删除日常管理考核得分数据
        /// </summary>
        /// <param name="ManagementScore">日常管理考核得分实体</param>        
        /// <returns>删除的数据行数</returns>
        public int DeleteManagementScore(ManagementScoreEntity ManagementScore) {           
            string SqlText = @"DELETE FROM KPI_ManagementScore WHERE PersonID=@PersonID AND 
                               CheckDate=@CheckDate AND TagID=@TagID ";
            return m_DataAccess.ExecuteNonQuery(CommandType.Text, SqlText, GetParameters(ManagementScore));
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 新增日常管理考核得分数据
        /// </summary>
        /// <param name="ManagementScore">日常管理考核得分实体</param>        
        /// <returns>新增的数据行数</returns>
        private int AddManagementScore(ManagementScoreEntity ManagementScore) {           
            string SqlText = @"Insert KPI_ManagementScore (PersonID,CheckDate,Shift,TagID,Score,IsValid,
                               TagScore,Rate) Values (@PersonID,@CheckDate,@Shift,@TagID,@Score,@IsValid,
                               @TagScore,@Rate) ";          
            return m_DataAccess.ExecuteNonQuery(CommandType.Text, SqlText, GetParameters(ManagementScore));
        }


        /// <summary>
        /// 更新日常管理考核得分数据
        /// </summary>
        /// <param name="ManagementScore">日常管理考核得分实体</param>        
        /// <returns>更新的数据行数</returns>
        private int UpdateManagementScore(ManagementScoreEntity ManagementScore) {           
            string SqlText = @"Update KPI_ManagementScore Set Shift=@Shift, Score=@Score,  
                               IsValid=@IsValid,TagScore=@TagScor , Rate=@Rate 
                               Where PersonID = @PersonID and CheckDate = @CheckDate and TagID = @TagID ";
            return m_DataAccess.ExecuteNonQuery(CommandType.Text, SqlText, GetParameters(ManagementScore));
        }


        /// <summary>
        /// 判断日常管理考核得分数据是否存在
        /// </summary>
        /// <param name="ManagementScore">日常管理考核得分实体</param>        
        /// <returns>数据存在则返回true否则返回false</returns>
        private bool Exists(ManagementScoreEntity ManagementScore) {          
            string SqlText = @"SELECT COUNT(PersonID) FROM  KPI_ManagementScore WHERE PersonID=@PersonID AND CheckDate=@CheckDate AND TagID=@TagID ";
            return (int)m_DataAccess.ExecuteScalar(SqlText, GetParameters(ManagementScore))>0;           
        }

        private IDbDataParameter[] GetParameters(ManagementScoreEntity ManagementScore) {
            IDbDataParameter[] Parameters = new SqlParameter[] { 
                new SqlParameter("@PersonID",SqlDbType.VarChar),
                new SqlParameter("@CheckDate",SqlDbType.Date),  
                new SqlParameter("@Shift",SqlDbType.Int),
                new SqlParameter("@TagID",SqlDbType.VarChar),
                new SqlParameter("@Score",SqlDbType.Decimal),
                new SqlParameter("@IsValid",SqlDbType.Bit),
                new SqlParameter("@TagScore",SqlDbType.Decimal),
                new SqlParameter("@Rate",SqlDbType.Decimal)
            };
            Parameters[0].Value = ManagementScore.PersonID;
            Parameters[1].Value = ManagementScore.CheckDate;
            Parameters[2].Value = ManagementScore.Shift;
            Parameters[3].Value = ManagementScore.TagID;
            Parameters[4].Value = ManagementScore.Score;
            Parameters[5].Value = ManagementScore.IsValid;
            Parameters[6].Value = ManagementScore.TagScore;
            Parameters[7].Value = ManagementScore.Rate;
            return Parameters;
        }

        #endregion

        #region IDisposable 成员

        public  void Dispose() {

        }

        #endregion
    }

}
