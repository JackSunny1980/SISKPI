using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using SIS.DataEntity;
using SIS.DBControl;


namespace SIS.DataAccess {
    public class KPI_SystemDal {

        /// <summary>
        /// 得到参数列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetParamList() {
            string sql = "select * from KPI_System where 1=1";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }


        /// <summary>
        /// 得到重载指标
        /// </summary>
        /// <returns></returns>
        public static bool GetKPIReload() {
            bool Result = false;
            string sql = "select SysValue from KPI_System where SysName='KPIReload' ";
            object obj = DBAccess.GetRelation().ExecuteScalar(sql);
            if (obj != null) {
                Result = Convert.ToInt32(obj) > 0;
            }
            return Result;
        }

        /// <summary>
        /// 得到时间偏差数值，即当前时间计算N分钟前的实时值
        /// </summary>
        /// <returns></returns>
        public static int GetKPIOffset() {
            int Result = 0;
            string sql = "select SysValue from KPI_System where SysName='KPIOffset' ";
            object obj = DBAccess.GetRelation().ExecuteScalar(sql);
            if (obj != null) {
                Result = Convert.ToInt32(obj);
            }
            return Result;
            //string sql = "select SysValue from KPI_System where SysName='KPIOffset' ";
            //DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            //if (dt.Rows.Count == 1) {
            //    return int.Parse(dt.Rows[0]["SysValue"].ToString());
            //}
            //else {
            //    return 0;
            //}
        }


        /// <summary>
        /// 写值服务运行时间
        /// </summary>
        /// <returns></returns>
        public static bool SetKPISrvTime(string strtime) {
            string sql = "update KPI_System Set SysValue='" + strtime + "' where SysName='KPISrvTime' ";
            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 得到服务运行时间
        /// </summary>
        /// <returns></returns>
        public static string GetKPISrvTime() {
            String Result = "";
            string sql = "select SysValue from KPI_System where SysName='KPISrvTime' ";
            object obj = DBAccess.GetRelation().ExecuteScalar(sql);
            if (obj != null) {
                Result = (String)obj;
            }
            return Result;          
        }

        /// <summary>
        /// 得到交接班运行方式
        /// </summary>
        /// <returns></returns>
        public static int GetKPIAuto() {
            int Result = 0;
            string sql = "select SysValue from KPI_System where SysName='KPIAuot' ";
            object obj = DBAccess.GetRelation().ExecuteScalar(sql);
            if (obj != null) {
                Result = Convert.ToInt32(obj);
            }
            return Result;          
        }

        /// <summary>
        /// 服务器时间定位方式
        /// </summary>
        /// <returns></returns>
        public static int GetKPITimeMode() {
            int Result = 0;
            string sql = "select SysValue from KPI_System where SysName='KPITimeMode' ";
            object obj = DBAccess.GetRelation().ExecuteScalar(sql);
            if (obj != null) {
                Result = Convert.ToInt32(obj);
            }
            return Result;       
        }

        /// <summary>
        /// 得到时间偏差数值，即当前时间计算N分钟前的实时值
        /// </summary>
        /// <returns></returns>
        public static double GetKPIMoney() {
            double Result = 0;
            string sql = "select SysValue from KPI_System where SysName='KPIMoney' ";
            object obj = DBAccess.GetRelation().ExecuteScalar(sql);
            if (obj != null) {
                Result = Convert.ToDouble(obj);
            }
            return Result;          
        }

        /// <summary>
        /// 得到时间偏差数值，即当前时间计算N分钟前的实时值
        /// </summary>
        /// <returns></returns>
        public static double GetKPIHour() {
            double Result = 0;
            string sql = "select SysValue from KPI_System where SysName='KPIHour' ";
            object obj = DBAccess.GetRelation().ExecuteScalar(sql);
            if (obj != null) {
                Result = Convert.ToDouble(obj);
            }
            return Result;
        }

        /// <summary>
        /// 修改参数
        /// </summary>
        /// <param name="keyid">主键</param>
        /// <param name="value">设定值</param>
        /// <returns></returns>
        public static bool UpdateByID(string keyid, string valid, string value, string note) {
            string sql = "update KPI_System set SysIsValid='{1}', SysValue='{2}', SysNote='{3}'  where SysID='{0}'";
            sql = string.Format(sql, keyid, valid, value, note);
            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 修改参数
        /// </summary>
        /// <param name="keyid">主键</param>
        /// <param name="value">设定值</param>
        /// <returns></returns>
        public static bool UpdateByName(string sysname, string sysvalid, string sysvalue, string sysnote) {
            string sql = "update KPI_System set SysIsValid='{1}', SysValue='{2}', SysNote='{3}'  where SysName='{0}'";
            sql = string.Format(sql, sysname, sysvalid, sysvalue, sysnote);
            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 保存系统参数信息
        /// </summary>
        /// <param name="SystemPara"></param>
        /// <returns></returns>
        public int SaveSystemParameter(SystemParameterEntity SystemPara) {
            if (String.IsNullOrWhiteSpace(SystemPara.SysValue)) SystemPara.SysValue = "0.0";
            if (Exists(SystemPara))
                return UpdateSystemParameter(SystemPara);
            else
                return AddSystemParameter(SystemPara);
        }

        /// <summary>
        /// 返回系统参数详细信息
        /// </summary>
        /// <param name="ParameterName"></param>
        /// <returns></returns>
        public SystemParameterEntity GetSystemParameter(String ParameterName) {
            string SqlText = @"SELECT SysID,SysName,SysDesc,SysEngunit,SysIsValid,SysValue,SysNote
								FROM  KPI_System WHERE SysName=@SysName ";
            SystemParameterEntity Result = null;
            IDbDataParameter SysNameParam = new SqlParameter("@SysName", ParameterName);
            IDataReader Reader = DBAccess.GetRelation().ExecuteReader(CommandType.Text, SqlText, SysNameParam);
            try {
                Result = Reader.FillEntity<SystemParameterEntity>();
            }
            finally {
                Reader.Close();
                Reader.Dispose();
            }

            return Result;
        }

        #region 私有方法


        /// <summary>
        /// 新增数据系统参数
        /// </summary>
        /// <param name="SystemPara">系统参数实体</param>        
        /// <returns>新增的数据行数</returns>
        private int AddSystemParameter(SystemParameterEntity SystemPara) {
            int Result = 0;
            string SqlText = @"Insert KPI_System (SysID,SysName,SysDesc,SysEngunit,SysIsValid,SysValue,SysNote) 
								Values (@SysID,@SysName,@SysDesc,@SysEngunit,@SysIsValid,@SysValue,@SysNote) ";
            IDbDataParameter[] parames = new SqlParameter[] {              
                new SqlParameter("@SysID",DbType.String),
                new SqlParameter("@SysName",DbType.String),  
                new SqlParameter("@SysDesc",DbType.String),
				new SqlParameter("@SysEngunit",DbType.String),
				new SqlParameter("@SysIsValid",DbType.Int32),
				new SqlParameter("@SysValue",DbType.String),
				new SqlParameter("@SysNote",DbType.String)};
            parames[0].Value = SystemPara.SysID;
            parames[1].Value = SystemPara.SysName;
            parames[2].Value = SystemPara.SysDesc;
            parames[3].Value = SystemPara.SysEngunit;
            parames[4].Value = SystemPara.SysIsValid;
            parames[5].Value = SystemPara.SysValue;
            parames[6].Value = SystemPara.SysNote;
            Result = DBAccess.GetRelation().ExecuteNonQuery(SqlText, parames);
            return Result;
        }


        /// <summary>
        /// 更新系统参数
        /// </summary>
        /// <param name="SystemPara">系统参数实体</param>        
        /// <returns>更新的数据行数</returns>
        private int UpdateSystemParameter(SystemParameterEntity SystemPara) {
            int Result = 0;
            string SqlText = @"Update KPI_System Set SysDesc=@SysDesc,SysEngunit=@SysEngunit,SysIsValid=@SysIsValid,
								SysValue=@SysValue,SysNote=@SysNote  Where SysName = @SysName";
            IDbDataParameter[] parames = new SqlParameter[] {              
                new SqlParameter("@SysID",DbType.String),
                new SqlParameter("@SysName",DbType.String),  
                new SqlParameter("@SysDesc",DbType.String),
				new SqlParameter("@SysEngunit",DbType.String),
				new SqlParameter("@SysIsValid",DbType.Int32),
				new SqlParameter("@SysValue",DbType.String),
				new SqlParameter("@SysNote",DbType.String)};
            parames[0].Value = SystemPara.SysID;
            parames[1].Value = SystemPara.SysName;
            parames[2].Value = SystemPara.SysDesc;
            parames[3].Value = SystemPara.SysEngunit;
            parames[4].Value = SystemPara.SysIsValid;
            parames[5].Value = SystemPara.SysValue;
            parames[6].Value = SystemPara.SysNote;
            Result = DBAccess.GetRelation().ExecuteNonQuery(SqlText, parames);
            return Result;
        }


        /// <summary>
        /// 判断数据是否存在
        /// </summary>
        /// <param name="KPI_System">实体</param>        
        /// <returns>数据存在则返回true否则返回false</returns>
        private bool Exists(SystemParameterEntity SystemPara) {
            bool Result = false;
            string SqlText = "SELECT SysName FROM  KPI_System WHERE SysName=@SysName ";
            IDbDataParameter SysNameParam = new SqlParameter("@SysName", SystemPara.SysName);
            Result = DBAccess.GetRelation().ExecuteScalar(SqlText, SysNameParam) != null;
            return Result;
        }

        #endregion
    }
}
