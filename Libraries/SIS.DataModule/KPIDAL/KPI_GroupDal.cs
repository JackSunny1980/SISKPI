using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SIS.DataEntity;

using SIS.DBControl;

namespace SIS.DataAccess
{
    public class GroupDal : DalBase<GroupEntity>  
    {
        /// <summary>
        /// 得到菜单列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetGroupsForMenu()
        {
            string sql = @"select GroupCode, GroupName, '0'GroupSelected  from KPI_Group where GroupCode<>'AA' order by GroupCode ";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到菜单列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetGroupsForUser()
        {
            string sql = @"select GroupCode, GroupName, '0'GroupSelected  from KPI_Group  order by GroupCode";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 初始化 数据库 角色表
        /// </summary>
        /// <returns></returns>
        public static bool InitilizeKPI_Group()
        {
            //clear
            string sql = @"delete From KPI_Group ";

            DBAccess.GetRelation().ExecuteNonQuery(sql);


            //add sisadmin
            GroupEntity mEntity = new GroupEntity();
            mEntity.GroupID = Guid.NewGuid().ToString();
            mEntity.GroupCode = "AA";
            mEntity.GroupName = "sisadmin";
            mEntity.GroupDesc = "SIS系统管理组";
            mEntity.GroupIsValid = 1;
            mEntity.GroupNote = "";

            mEntity.GroupCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            mEntity.GroupModifyTime = mEntity.GroupCreateTime;

            GroupDal.Insert(mEntity);
            //add sisdemo
            mEntity = new GroupEntity();

            mEntity.GroupID = Guid.NewGuid().ToString();
            mEntity.GroupCode = "AB";
            mEntity.GroupName = "sisdemo";
            mEntity.GroupDesc = "SIS系统游客组";
            mEntity.GroupIsValid = 1;
            mEntity.GroupNote = "";

            mEntity.GroupCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            mEntity.GroupModifyTime = mEntity.GroupCreateTime;
            GroupDal.Insert(mEntity);
            return true;

        }


        /// <summary>
        /// 得到菜单列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetGroups(string strName)
        {
            string sql = @"select * from KPI_Group {0}";
            if (strName != "")
            {
                strName = " where GroupName like '%" + strName + "%'";
                sql = string.Format(sql, strName);
            }
            else
            {
                sql = string.Format(sql, "");
            }

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public static bool GroupExist(string GroupName, string GroupID)
        {
            return int.Parse(DBAccess.GetRelation().ExecuteScalar("select count(1) from KPI_Group where GroupName='" + GroupName + "' and GroupID<>'" + GroupID + "'").ToString()) > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public static bool DeleteGroupID(string GroupID)
        {
            return DBAccess.GetRelation().ExecuteNonQuery("delete from KPI_Group where GroupID='" + GroupID + "'") > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public static string GetNextCode()
        {
            //
            //支持最多26 * 26个Code
            //
            string [] AllCodes=new string[26]{"A", "B", "C", "D", "E", "F", "G", "H", "I", "J","K", "L", "M", "N", "O", "P", "Q", "R", "S", "T","U", "V", "W", "X", "Y", "Z"};

            string sql = "select GroupCode from KPI_Group order by GroupCode";
            int ncount = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;

            //string[] OldCodes = new string[ncount];
            //if (ncount <= 0)
            //{
            //    return "AA";
            //}

            //个位，十位，百位：units, tens, hundreds
            int units = 0;
            int tens = 0;

            if (ncount <=0)
            {
                //0
                tens = 0;
                units = 0;
            }
            else if (ncount % 26 > 0)
            {
                //不存在十位数变化的情况
                tens = (int)Math.Floor(ncount/26.0);
                units = ncount % 26;
            }
            else if (ncount % 26 == 0)
            {
                //十位数变化的情况
                tens = (int)Math.Floor(ncount / 26.0) +1;
                units = 0;
            }
            
            string newcode = AllCodes[tens] + AllCodes[units];

            return newcode;
           
        }



        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="KeyID">主键</param>
        /// <returns></returns>
        public static KPI_MenuEntity GetEntity(string MenuID)
        {
            KPI_MenuEntity entity = new KPI_MenuEntity();

            string sql = "select * from KPI_Menu where MenuID='{0}'";
            sql = string.Format(sql, MenuID);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                entity.DrToMember(dt.Rows[0]);
            }

            return entity;
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
}
