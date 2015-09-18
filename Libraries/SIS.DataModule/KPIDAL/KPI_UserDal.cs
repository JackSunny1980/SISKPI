using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SIS.DataEntity;

using System.IO;
using System.Security.Cryptography;

using SIS.DBControl;

namespace SIS.DataAccess
{
    public class KPI_UserDal : DalBase<KPI_UserEntity>  
    {
        /// <summary>
        /// 得到用户列表
        /// </summary>
        /// <returns></returns>
        public static bool DeleteUserID(string UserID)
        {
            string sql = @"delete KPI_User where UserID='{0}'";
            sql = string.Format(sql, UserID);

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 得到用户列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUsers(string strUserName)
        {
            string sql = @"select * from KPI_User {0} order by UserCode";
            if (strUserName != "")
            {
                strUserName = " where UserName like '%" + strUserName + "%'";
                sql = string.Format(sql, strUserName);
            }
            else
            {
                sql = string.Format(sql, "");
            }

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 设置用户初始密码为：123456
        /// </summary>
        /// <returns></returns>
        public static bool UpdateUserGroups(string UserID, string UserGroups)
        {
            string sql = @"update KPI_User set UserGroups='{0}' where UserID='{1}'";
            sql = string.Format(sql, UserGroups, UserID);

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;

        }



        /// <summary>
        /// 得到用户Group
        /// </summary>
        /// <returns></returns>
        public static string GetUserGroups(string strUserCode)
        {
            string sql = "select UserCode, UserName, UserGroups from KPI_User where UserCode = '" + strUserCode + "' order by UserCode";

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count == 1)
            {
                return dt.Rows[0]["UserGroups"].ToString();
            }
            
            return "";
        }


        /// <summary>
        /// 得到用户URL
        /// </summary>
        /// <returns></returns>
        public static string GetUserURL(string strUserCode)
        {
            string sql = "select UserURL from KPI_User where UserCode = '" + strUserCode + "' order by UserCode";

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count == 1)
            {
                return dt.Rows[0]["UserURL"].ToString();
            }

            return "";
        }


        /// <summary>
        /// 设置用户URL
        /// </summary>
        /// <returns></returns>
        public static bool SetUserURL(string strUserCode, string strUserURL)
        {
            string sql = "update KPI_User set UserURL='{0}' where UserCode = '{1}'";
            sql = string.Format(sql, strUserURL, strUserCode);

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }


        /// <summary>
        /// 得到用户列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetNotUsersForGroup(string strGroup)
        {
            string sql = @"select UserID, UserCode, UserName, UserGroups, '工号: '+ UserCode + ', 姓名: '+UserName AS DisplayName from KPI_User {0} order by UserCode";
            if (strGroup != "")
            {
                strGroup = " where UserGroups not like '%"+ strGroup +"%'";
                sql = string.Format(sql, strGroup);
            }
            else
            {
                sql = string.Format(sql, "");
            }

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到用户列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUsersForGroup(string strGroup)
        {
            string sql = @"select UserID, UserCode, UserName, UserGroups, '工号: '+ UserCode + ', 姓名: '+UserName AS DisplayName from KPI_User {0} order by UserCode";
            if (strGroup != "")
            {
                strGroup = " where UserGroups like '%" + strGroup + "%'";
                sql = string.Format(sql, strGroup);
            }
            else
            {
                sql = string.Format(sql, "");
            }

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 初始化 数据库 用户表
        /// </summary>
        /// <returns></returns>
        public static bool InitilizeKPI_User()
        {
            //clear
            string sql = @"delete From KPI_User ";

            DBAccess.GetRelation().ExecuteNonQuery(sql);

            //add sisadmin
            KPI_UserEntity usEntity = new KPI_UserEntity();

            usEntity.UserID = Guid.NewGuid().ToString();
            usEntity.UserCode = "sisadmin";
            usEntity.UserName = "sisadmin";
            usEntity.UserPassword = KPI_UserDal.GetDESString("123456");
            usEntity.UserEMail = "sisadmin@bdxyit.com";
            usEntity.UserPhone = "13800138000";
            usEntity.UserTitle = "SIS系统管理员";

            usEntity.UserGroups = "AA,";

            usEntity.UserIsValid = 1;

            usEntity.UserCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            usEntity.UserModifyTime = usEntity.UserCreateTime;

            KPI_UserDal.Insert(usEntity);



            //add sisdemo
            usEntity = new KPI_UserEntity();

            usEntity.UserID = Guid.NewGuid().ToString();
            usEntity.UserCode = "sisdemo";
            usEntity.UserName = "sisdemo";
            usEntity.UserPassword = KPI_UserDal.GetDESString("123456");
            usEntity.UserEMail = "sisdemo@bdxyit.com";
            usEntity.UserPhone = "13800138000";
            usEntity.UserTitle = "SIS系统游客帐户";

            usEntity.UserGroups = "AB,";

            usEntity.UserIsValid = 1;

            usEntity.UserCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            usEntity.UserModifyTime = usEntity.UserCreateTime;

            KPI_UserDal.Insert(usEntity);

            return true;

        }


        /// <summary>
        /// 得到用户ID
        /// </summary>
        /// <returns></returns>
        public static string GetUserID(string UserCode)
        {
            string sql = @"select UserID from KPI_User  where UserCode='" + UserCode + "'"; 

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            if(dt.Rows.Count != 1)
            {
                return "";
            }else
            {
                return dt.Rows[0]["UserID"].ToString();
            }
             
        }
        
        /// <summary>
        /// 密码是否正确
        /// </summary>
        /// <returns></returns>
        public static bool CheckUserPassword(string UserCode, string UserPassword)
        {
            string strPWD = GetDESString(UserPassword);
            string strSQLPWD = GetUserPasswordByCode(UserCode);

            if (strPWD != strSQLPWD)
            {
                return false;
            }

            return true;
        }



        /// <summary>
        /// 得到用户加密后的密码
        /// </summary>
        /// <returns></returns>
        public static string GetUserPassword(string UserID)
        {
            string sql = @"select UserPassword from KPI_User  where UserID='" + UserID + "'";

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            if (dt.Rows.Count != 1)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["UserPassword"].ToString();
            }

        }

        /// <summary>
        /// 得到用户加密后的密码
        /// </summary>
        /// <returns></returns>
        public static string GetUserPasswordByCode(string UserCode)
        {
            string sql = @"select UserPassword from KPI_User  where UserCode='" + UserCode + "'";

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            if (dt.Rows.Count != 1)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["UserPassword"].ToString();
            }

        }

        /// <summary>
        /// 设置用户初始密码为：123456
        /// </summary>
        /// <returns></returns>
        public static bool SetInitPassword(string newpassword, string UserID)
        {
            //string newpassword = "123456";
            newpassword = KPI_Security.DesEncryptString(newpassword);
            
            string sql = @"update KPI_User set UserPassword='{0}' where UserID='{1}'";
            sql = string.Format(sql, newpassword,  UserID);

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
            
        }


        /// <summary>
        /// 设置用户的有效性
        /// </summary>
        /// <returns></returns>
        public static bool SetUserValid(string UserIsValid, string UserID)
        {
            if (UserIsValid != "0" && UserIsValid != "1")
            {
                UserIsValid = "1";
            }

            string sql = @"update KPI_User set UserIsValid={0} where UserID='{1}'";
            sql = string.Format(sql, UserIsValid, UserID);

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;

        }

        /// <summary>
        /// 用户Code是否存在
        /// </summary>
        /// <returns></returns>
        public static bool UserCodeExist(string UserCode, string UserID)
        {
            return int.Parse(DBAccess.GetRelation().ExecuteScalar("select count(1) from KPI_User where UserCode='" + UserCode + "' and UserID<>'" + UserID + "'").ToString()) > 0;    

        }
        /// <summary>
        /// 得到加密密码
        /// </summary>
        /// <returns></returns>
        public static string GetDESString(string oldpassword)
        {
            string newpassword = KPI_Security.DesEncryptString(oldpassword);

            return newpassword;
        }


        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="KeyID">主键</param>
        /// <returns></returns>
        public static KPI_UserEntity GetEntity(string UserID)
        {
            KPI_UserEntity entity = new KPI_UserEntity();

            string sql = "select * from KPI_User where UserID='{0}'";
            sql = string.Format(sql, UserID);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                entity.DrToMember(dt.Rows[0]);
            }

            return entity;
        }

        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="KeyID">主键</param>
        /// <returns></returns>
        public static KPI_UserEntity GetEntityByCode(string UserCode)
        {
            KPI_UserEntity entity = new KPI_UserEntity();

            string sql = "select * from KPI_User where UserCode='{0}'";
            sql = string.Format(sql, UserCode);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                entity.DrToMember(dt.Rows[0]);
            }

            return entity;
        }



        /// <summary>
        /// 得到测点配置主表
        /// </summary>
        /// <returns></returns>       
        public static DataTable GetUsersForExcel()
        {
            string sql = @"select 'x'SelectX, UserCode, UserName, UserDesc,''UserPassword, UserEMail, UserPhone,
                                    UserTitle, UserIsValid, UserNote
                        from KPI_User
                        order by UserCode";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

        }

        /// <summary>
        /// 删除节点及其所有子节点
        /// </summary>
        /// <returns></returns>
        public static bool Delete(string MenuID)
        {
            //节点
            string sql = "select MenuID, MenuParentID from KPI_Menu where MenuParentID='{0}'";
            sql = string.Format(sql, MenuID);
            
            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            foreach(DataRow dr in dt.Rows)
            {
                string menuparentid = dr["MenuID"].ToString();

                Delete(menuparentid);
            }
                        
            sql = "delete From KPI_Menu where MenuID='{0}'";
            sql = string.Format(sql, MenuID);


            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 修改参数
        /// </summary>
        /// <param name="keyid">主键</param>
        /// <param name="value">设定值</param>
        /// <returns></returns>
        public static bool Update(string keyid, string valid, string value, string note)
        {
            string sql = "update OPM_System set SysIsValid='{1}', SysValue='{2}', SysNote='{3}'  where SysID='{0}'";

            sql = string.Format(sql, keyid, valid, value, note);

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }
    }


    /// <summary>
    /// 功能描述：数据加密/解密操作类
    /// 创建日期：2012-10-20
    /// 创 建 人：吴观辉
    /// 电子邮箱：
    /// </summary>
    public class KPI_Security
    {
        ///<summary>
        ///DES加密算法
        ///</summary>
        ///<returns>加密后的字符串</returns>
        ///<param name="strText">加密前的字符串</param>
        ///<param name="strEncrKey">秘钥 长度>=8</param>
        public static string DesEncryptString(string strText)
        {
            string strEncrKey = "abcdefghijk";
            byte[] byKey = null;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                return "";
            }
        }

        ///<summary>
        ///DES解密算法
        ///</summary>
        ///<returns>解密后的字符串</returns>
        ///<param name="strText">解密前的字符串</param>
        ///<param name="sDecrKey">秘钥 长度>=8</param>
        public static string DesDecryptString(string strText, string sDecrKey)
        {
            byte[] byKey = null;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] inputByteArray = new Byte[strText.Length];
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                System.Text.Encoding encoding = new System.Text.UTF8Encoding();
                return encoding.GetString(ms.ToArray());
            }
            catch
            {
                return "";
            }
        }
    }

}
