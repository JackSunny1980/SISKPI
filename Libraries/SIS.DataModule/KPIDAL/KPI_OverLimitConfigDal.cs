using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SIS.DataEntity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using SIS.DBControl;


namespace SIS.DataAccess {
    public class KPI_OverLimitConfigDal : IKPI_OverLimitConfigDal, IDisposable {
        private const string sql_SearchOverLimitConfigByID = @"SELECT
	                                                        kolc.OverLimitConfigID,kolc.TagName,krt.RealDesc,kolc.FirstLimitingValue,kolc.SecondLimitingValue,
                                                            kolc.ThirdLimitingValue,kolc.FourthLimitingValue,kolc.LowLimit1Value,kolc.LowLimit2Value,kolc.LowLimit3Value,
															kolc.Comment,kolc.CreatedDate,kolc.ModifiedDate FROM
	                                                        KPI_OverLimitConfig AS kolc JOIN KPI_RealTag AS krt 
				                                            ON krt.RealID=kolc.TagName WHERE kolc.OverLimitConfigID=@OverLimitConfigID";

        private const string sql_InsertInfo = @"INSERT INTO KPI_OverLimitConfig(OverLimitConfigID,TagName,FirstLimitingValue,SecondLimitingValue,ThirdLimitingValue,FourthLimitingValue,LowLimit1Value,LowLimit2Value,LowLimit3Value,Comment,CreatedDate,ModifiedDate,FirstLimitingTag,SecondLimitingTag,ThirdLimitingTag,FourthLimitingTag,LowLimit1Tag,LowLimit2Tag,LowLimit3Tag,OverLimitComputeType)
                                                VALUES(NEWID(),@TagName ,@FirstLimitingValue,@SecondLimitingValue,@ThirdLimitingValue,@FourthLimitingValue,@LowLimit1Value,@LowLimit2Value,@LowLimit3Value,@Comment,GETDATE(),GETDATE(),@FirstLimitingTag,@SecondLimitingTag,@ThirdLimitingTag,@FourthLimitingTag,@LowLimit1Tag,@LowLimit2Tag,@LowLimit3Tag,@OverLimitComputeType)";

        private const string sql_UpdateInfo = @"UPDATE KPI_OverLimitConfig
                                                SET FirstLimitingValue = @FirstLimitingValue,SecondLimitingValue = @SecondLimitingValue,ThirdLimitingValue = @ThirdLimitingValue,FourthLimitingValue=@FourthLimitingValue,LowLimit1Value=@LowLimit1Value,LowLimit2Value=@LowLimit2Value,LowLimit3Value=@LowLimit3Value,Comment =@Comment,ModifiedDate =GETDATE(),FirstLimitingTag=@FirstLimitingTag,SecondLimitingTag=@SecondLimitingTag,ThirdLimitingTag=@ThirdLimitingTag,FourthLimitingTag=@FourthLimitingTag,LowLimit1Tag=@LowLimit1Tag,LowLimit2Tag=@LowLimit2Tag,LowLimit3Tag=@LowLimit3Tag,OverLimitComputeType=@OverLimitComputeType
                                               WHERE TagName = @TagName";

        private const string sql_DeleteInfo = @"DELETE FROM KPI_OverLimitConfig WHERE OverLimitConfigID=@OverLimitConfigID";
        private const string sql_GetRealTagEntity = @"SELECT krt.RealID,krt.RealDesc FROM KPI_RealTag AS krt";

        private const string pro_OverLimitConfigPaging = "pro_OverLimitConfigPaging";
        private const string pro_SearchOverLimitConfig = "pro_SearchOverLimitConfig";
        private const string param_PageSize = "@PageSize";
        private const string param_PageIndex = "@PageIndex";
        private const string param_RowCount = "@RowCount";
        private const string param_TagName = "@TagName";
        private const string param_OverLimitConfigID = "@OverLimitConfigID";
        private const string param_FirstLimitingValue = "@FirstLimitingValue";
        private const string param_SecondLimitingValue = "@SecondLimitingValue";
        private const string param_ThirdLimitingValue = "@ThirdLimitingValue";
        private const string param_FourthLimitingValue = "@FourthLimitingValue";
        private const string param_LowLimit1Value = "@LowLimit1Value";
        private const string param_LowLimit2Value = "@LowLimit2Value";
        private const string param_LowLimit3Value = "@LowLimit3Value";

        private const string param_Comment = "@Comment";

        private const string param_FirstLimitingTag = "@FirstLimitingTag";
        private const string param_SecondLimitingTag = "@SecondLimitingTag";
        private const string param_ThirdLimitingTag = "@ThirdLimitingTag ";
        private const string param_FourthLimitingTag = "@FourthLimitingTag ";
        private const string param_LowLimit1Tag = "@LowLimit1Tag ";
        private const string param_LowLimit2Tag = "@LowLimit2Tag ";
        private const string param_LowLimit3Tag = "@LowLimit3Tag";
        private const string param_OverLimitComputeType = "@OverLimitComputeType ";

        public List<OverLimitConfigEntity> GetOverLimitConfigList(int startIndex, int pageSize, ref int totalCount) {
            SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter(param_PageIndex,SqlDbType.Int),  
                new SqlParameter(param_PageSize,SqlDbType.Int),
                new SqlParameter(param_RowCount,SqlDbType.Int),
            };

            parames[0].Value = startIndex;
            parames[1].Value = pageSize;
            parames[2].Direction = ParameterDirection.Output;
            IDataReader reader = null;
            List<OverLimitConfigEntity> overLimitConfigList = new List<OverLimitConfigEntity>();

            try {

                reader = DBAccess.GetRelation().ExecuteReader(CommandType.StoredProcedure, pro_OverLimitConfigPaging, parames);

                while (reader.Read()) {
                    overLimitConfigList.Add(new OverLimitConfigEntity {
                        OverLimitConfigID = reader["OverLimitConfigID"].ToString(),
                        TagName = reader["TagName"] == DBNull.Value ? string.Empty : reader["TagName"].ToString(),
                        RealDesc = reader["RealDesc"] == DBNull.Value ? string.Empty : reader["RealDesc"].ToString(),
                        FirstLimitingValue = reader["FirstLimitingValue"] == DBNull.Value ? null : (decimal?)reader["FirstLimitingValue"],
                        SecondLimitingValue = reader["SecondLimitingValue"] == DBNull.Value ? null : (decimal?)reader["SecondLimitingValue"],
                        ThirdLimitingValue = reader["ThirdLimitingValue"] == DBNull.Value ? null : (decimal?)reader["ThirdLimitingValue"],
                        FourthLimitingValue = reader["FourthLimitingValue"] == DBNull.Value ? null : (decimal?)reader["FourthLimitingValue"],
                        LowLimit1Value = reader["LowLimit1Value"] == DBNull.Value ? null : (decimal?)reader["LowLimit1Value"],
                        LowLimit2Value = reader["LowLimit2Value"] == DBNull.Value ? null : (decimal?)reader["LowLimit2Value"],
                        LowLimit3Value = reader["LowLimit3Value"] == DBNull.Value ? null : (decimal?)reader["LowLimit3Value"],

                        FirstLimitingTag = reader["FirstLimitingTag"] == DBNull.Value ? string.Empty : reader["FirstLimitingTag"].ToString(),
                        SecondLimitingTag = reader["SecondLimitingTag"] == DBNull.Value ? string.Empty : reader["SecondLimitingTag"].ToString(),
                        ThirdLimitingTag = reader["ThirdLimitingTag"] == DBNull.Value ? string.Empty : reader["ThirdLimitingTag"].ToString(),
                        FourthLimitingTag = reader["FourthLimitingTag"] == DBNull.Value ? string.Empty : reader["FourthLimitingTag"].ToString(),
                        LowLimit1Tag = reader["LowLimit1Tag"] == DBNull.Value ? string.Empty : reader["LowLimit1Tag"].ToString(),
                        LowLimit2Tag = reader["LowLimit2Tag"] == DBNull.Value ? string.Empty : reader["LowLimit2Tag"].ToString(),
                        LowLimit3Tag = reader["LowLimit3Tag"] == DBNull.Value ? string.Empty : reader["LowLimit3Tag"].ToString(),

                        EnumOverLimitComputeType = (OverLimitComputeTypeEnum)(reader["OverLimitComputeType"] == DBNull.Value ? 0 : (int)reader["OverLimitComputeType"]),

                        Comment = reader["Comment"] == DBNull.Value ? string.Empty : reader["Comment"].ToString(),
                    });
                }

                reader.Close();
                if (parames[2].Value != DBNull.Value) {
                    totalCount = Convert.ToInt32(parames[2].Value);
                }
                else {
                    totalCount = 0;
                }

            }
            catch (Exception ex) {

                throw ex;
            }
            finally {
                if (!reader.IsClosed) {
                    reader.Close();
                }
            }


            return overLimitConfigList;

        }

        public List<OverLimitConfigEntity> GetOverLimitConfigList(int startIndex, int pageSize, string searchName, ref int totalCount) {
            SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter(param_TagName,SqlDbType.VarChar),
                new SqlParameter(param_PageIndex,SqlDbType.Int),  
                new SqlParameter(param_PageSize,SqlDbType.Int),
                new SqlParameter(param_RowCount,SqlDbType.Int),
            };

            parames[0].Value = searchName;
            parames[1].Value = startIndex;
            parames[2].Value = pageSize;
            parames[3].Direction = ParameterDirection.Output;
            IDataReader reader = null;
            List<OverLimitConfigEntity> overLimitConfigList = new List<OverLimitConfigEntity>();

            try {
                reader = DBAccess.GetRelation().ExecuteReader(CommandType.StoredProcedure, pro_SearchOverLimitConfig, parames);

                while (reader.Read()) {
                    overLimitConfigList.Add(new OverLimitConfigEntity {
                        OverLimitConfigID = reader["OverLimitConfigID"].ToString(),
                        TagName = reader["TagName"] == DBNull.Value ? string.Empty : reader["TagName"].ToString(),
                        RealDesc = reader["RealDesc"] == DBNull.Value ? string.Empty : reader["RealDesc"].ToString(),
                        FirstLimitingValue = reader["FirstLimitingValue"] == DBNull.Value ? null : (decimal?)reader["FirstLimitingValue"],
                        SecondLimitingValue = reader["SecondLimitingValue"] == DBNull.Value ? null : (decimal?)reader["SecondLimitingValue"],
                        ThirdLimitingValue = reader["ThirdLimitingValue"] == DBNull.Value ? null : (decimal?)reader["ThirdLimitingValue"],
                        FourthLimitingValue = reader["FourthLimitingValue"] == DBNull.Value ? null : (decimal?)reader["FourthLimitingValue"],
                        LowLimit1Value = reader["LowLimit1Value"] == DBNull.Value ? null : (decimal?)reader["LowLimit1Value"],
                        LowLimit2Value = reader["LowLimit2Value"] == DBNull.Value ? null : (decimal?)reader["LowLimit2Value"],
                        LowLimit3Value = reader["LowLimit3Value"] == DBNull.Value ? null : (decimal?)reader["LowLimit3Value"],

                        FirstLimitingTag = reader["FirstLimitingTag"] == DBNull.Value ? string.Empty : reader["FirstLimitingTag"].ToString(),
                        SecondLimitingTag = reader["SecondLimitingTag"] == DBNull.Value ? string.Empty : reader["SecondLimitingTag"].ToString(),
                        ThirdLimitingTag = reader["ThirdLimitingTag"] == DBNull.Value ? string.Empty : reader["ThirdLimitingTag"].ToString(),
                        FourthLimitingTag = reader["FourthLimitingTag"] == DBNull.Value ? string.Empty : reader["FourthLimitingTag"].ToString(),
                        LowLimit1Tag = reader["LowLimit1Tag"] == DBNull.Value ? string.Empty : reader["LowLimit1Tag"].ToString(),
                        LowLimit2Tag = reader["LowLimit2Tag"] == DBNull.Value ? string.Empty : reader["LowLimit2Tag"].ToString(),
                        LowLimit3Tag = reader["LowLimit3Tag"] == DBNull.Value ? string.Empty : reader["LowLimit3Tag"].ToString(),

                        EnumOverLimitComputeType = (OverLimitComputeTypeEnum)(reader["OverLimitComputeType"] == DBNull.Value ? 0 : (int)reader["OverLimitComputeType"]),

                        Comment = reader["Comment"] == DBNull.Value ? string.Empty : reader["Comment"].ToString(),
                    });
                }

                reader.Close();
                if (parames[3].Value != DBNull.Value) {
                    totalCount = Convert.ToInt32(parames[3].Value);
                }
                else {
                    totalCount = 0;
                }

            }
            catch (Exception ex) {

                throw ex;
            }
            finally {
                if (!reader.IsClosed) {
                    reader.Close();
                }
            }
            return overLimitConfigList;
        }

        public OverLimitConfigEntity GetOverLimitConfig(string overLimitConfigID) {
            OverLimitConfigEntity overLimitConfig = null;
            SqlParameter parame = new SqlParameter(param_OverLimitConfigID, SqlDbType.VarChar);
            parame.Value = overLimitConfigID;
            using (IDataReader dataReader = DBAccess.GetRelation().ExecuteReader(CommandType.Text, sql_SearchOverLimitConfigByID, parame)) {
                while (dataReader.Read()) {
                    overLimitConfig = new OverLimitConfigEntity {
                        OverLimitConfigID = dataReader["OverLimitConfigID"].ToString(),
                        TagName = dataReader["TagName"] == DBNull.Value ? string.Empty : dataReader["TagName"].ToString(),
                        RealDesc = dataReader["RealDesc"] == DBNull.Value ? string.Empty : dataReader["RealDesc"].ToString(),
                        FirstLimitingValue = dataReader["FirstLimitingValue"] == DBNull.Value ? null : (decimal?)dataReader["FirstLimitingValue"],
                        SecondLimitingValue = dataReader["SecondLimitingValue"] == DBNull.Value ? null : (decimal?)dataReader["SecondLimitingValue"],
                        ThirdLimitingValue = dataReader["ThirdLimitingValue"] == DBNull.Value ? null : (decimal?)dataReader["ThirdLimitingValue"],
                        FourthLimitingValue = dataReader["FourthLimitingValue"] == DBNull.Value ? null : (decimal?)dataReader["FourthLimitingValue"],
                        LowLimit1Value = dataReader["LowLimit1Value"] == DBNull.Value ? null : (decimal?)dataReader["LowLimit1Value"],
                        LowLimit2Value = dataReader["LowLimit2Value"] == DBNull.Value ? null : (decimal?)dataReader["LowLimit2Value"],
                        LowLimit3Value = dataReader["LowLimit3Value"] == DBNull.Value ? null : (decimal?)dataReader["LowLimit3Value"],

                        Comment = dataReader["Comment"] == DBNull.Value ? string.Empty : dataReader["Comment"].ToString(),
                    };
                }
            }

            return overLimitConfig;
        }

        public bool Insert(OverLimitConfigEntity overLimitConfig) {
              try {
                int result = DBAccess.GetRelation().ExecuteNonQuery(sql_InsertInfo,
                    GetParameters(overLimitConfig));
                if (result == 0) {
                    return false;
                }
            }
            catch (Exception ex) {

                throw ex;
            }

            return true;
        }

        public bool Update(OverLimitConfigEntity overLimitConfig) {
            try {
                int result = DBAccess.GetRelation().ExecuteNonQuery(sql_UpdateInfo, 
                    GetParameters(overLimitConfig));
                if (result == 0) {
                    return false;
                }
            }
            catch (Exception ex) {
                throw ex;
            }

            return true;
        }

        public bool Delete(string deleteId) {
            SqlParameter[] parame = new SqlParameter[]{
                new SqlParameter(param_OverLimitConfigID, SqlDbType.VarChar,50)
            };
            parame[0].Value = deleteId;
            try {
                int result = DBAccess.GetRelation().ExecuteNonQuery(sql_DeleteInfo, parame);

                if (result == 0) {
                    return false;
                }
            }
            catch (Exception ex) {

                throw ex;
            }

            return true;
        }

        public List<KPI_RealTagEntity> GetRealTagList() {
            List<KPI_RealTagEntity> kpiEntityList = new List<KPI_RealTagEntity>();

            using (IDataReader dataReader = DBAccess.GetRelation().ExecuteReader(sql_GetRealTagEntity)) {
                while (dataReader.Read()) {
                    kpiEntityList.Add(new KPI_RealTagEntity {
                        RealID = dataReader["RealID"].ToString(),
                        RealDesc = dataReader["RealDesc"] == DBNull.Value ? string.Empty : dataReader["RealDesc"].ToString(),
                    });
                }
            }

            return kpiEntityList;
        }

        //Added by pyf 2013-09-17
        public List<OverLimitConfigEntity> GetOverLimitConfigs() {
            List<OverLimitConfigEntity> Result;
            String SqlText = @"SELECT A.*,B.UnitID,B.RealCode TagCode,B.RealDesc TagDesc 
							   FROM KPI_OverLimitConfig A JOIN KPI_RealTag B ON A.TagName=B.RealID";
            //WHERE TagName In(SELECT B.RealID FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID)";
            using (IDataReader dataReader = DBAccess.GetRelation().ExecuteReader(SqlText)) {
                Result = dataReader.FillGenericList<OverLimitConfigEntity>();
            }
            return Result;
        }

        public bool SavOverLimitConfigs(OverLimitConfigEntity OverLimitConfig) {
            if (HasOverLimitConfigs(OverLimitConfig.TagCode)) {
                return Update(OverLimitConfig);
            }
            else {
                return Insert(OverLimitConfig);
            }           
        }

        private bool HasOverLimitConfigs(String TagCode) {
            String SqlText = @"SELECT B.RealCode FROM KPI_OverLimitConfig A 
                              JOIN KPI_RealTag B ON A.TagName=B.RealID
                              WHERE B.RealCode=@TagCode";
            IDbDataParameter[] parames = new SqlParameter[] {
                new SqlParameter("@TagCode",SqlDbType.VarChar,50)};
            parames[0].Value = TagCode;
            return DBAccess.GetRelation().ExecuteScalar(SqlText, parames) != null;
        }

        private IDbDataParameter[] GetParameters(OverLimitConfigEntity OverLimitConfig) {
            IDbDataParameter[] parames = new SqlParameter[] {
                new SqlParameter(param_TagName,SqlDbType.VarChar,50),
                new SqlParameter(param_FirstLimitingValue,SqlDbType.Decimal),
                new SqlParameter(param_SecondLimitingValue,SqlDbType.Decimal),
                new SqlParameter(param_ThirdLimitingValue,SqlDbType.Decimal),
				new SqlParameter(param_FourthLimitingValue,SqlDbType.Decimal),
				new SqlParameter(param_LowLimit1Value,SqlDbType.Decimal),
				new SqlParameter(param_LowLimit2Value,SqlDbType.Decimal),
				new SqlParameter(param_LowLimit3Value,SqlDbType.Decimal),
                new SqlParameter(param_Comment,SqlDbType.VarChar,255),
                new SqlParameter(param_OverLimitConfigID,SqlDbType.VarChar),

                new SqlParameter(param_FirstLimitingTag,SqlDbType.VarChar,50),
                new SqlParameter(param_SecondLimitingTag,SqlDbType.VarChar,50),
                new SqlParameter(param_ThirdLimitingTag,SqlDbType.VarChar,50),
                new SqlParameter(param_FourthLimitingTag,SqlDbType.VarChar,50),
                new SqlParameter(param_LowLimit1Tag,SqlDbType.VarChar,50),
                new SqlParameter(param_LowLimit2Tag,SqlDbType.VarChar,50),
                new SqlParameter(param_LowLimit3Tag,SqlDbType.VarChar,50),
                new SqlParameter(param_OverLimitComputeType,SqlDbType.Int)
            };

            parames[0].Value = OverLimitConfig.TagName;
            parames[1].Value = OverLimitConfig.FirstLimitingValue;
            parames[2].Value = OverLimitConfig.SecondLimitingValue;
            parames[3].Value = OverLimitConfig.ThirdLimitingValue;
            parames[4].Value = OverLimitConfig.FourthLimitingValue;
            parames[5].Value = OverLimitConfig.LowLimit1Value;
            parames[6].Value = OverLimitConfig.LowLimit2Value;
            parames[7].Value = OverLimitConfig.LowLimit3Value;
            parames[8].Value = OverLimitConfig.Comment;
            parames[9].Value = OverLimitConfig.OverLimitConfigID;

            parames[10].Value = OverLimitConfig.FirstLimitingTag;
            parames[11].Value = OverLimitConfig.SecondLimitingTag;
            parames[12].Value = OverLimitConfig.ThirdLimitingTag;
            parames[13].Value = OverLimitConfig.FourthLimitingTag;
            parames[14].Value = OverLimitConfig.LowLimit1Tag;
            parames[15].Value = OverLimitConfig.LowLimit2Tag;
            parames[16].Value = OverLimitConfig.LowLimit3Tag;
            parames[17].Value = OverLimitConfig.OverLimitComputeType;
            return parames;
        }

        public void Dispose() {

        }

        //End of Added.
    }
}
