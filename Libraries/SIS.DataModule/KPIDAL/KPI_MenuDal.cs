using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SIS.DataEntity;

using SIS.DBControl;

namespace SIS.DataAccess
{
    public class KPI_MenuDal : DalBase<KPI_MenuEntity>  
    {
        /// <summary>
        /// 得到菜单列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetMenus(string strParent)
        {
            string sql = @"select * from KPI_Menu where 1=1 {0} order by MenuIndex";
            string condition = "";

            if (strParent != "")
            {
                condition = " and MenuParentID='" + strParent + "'";
            }

            //if (strGroupCode != "")
            //{
            //    condition += " and '" + strGroupCode +"' IN MenuGroups";
            //}

            sql = string.Format(sql, condition);

            return DBAccess.GetRelation().ExecuteDataset(sql);

        }
        
        /// <summary>
        /// 得到菜单列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetTopMenusForUser(string strUserCode)
        {
            //得到当前用户的权限
            string strUserGroup = KPI_UserDal.GetUserGroups(strUserCode);

            string[] estr = strUserGroup.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            string strcondition = "";
            for (int i = 0; i < estr.Length; i++)
            {
                strcondition += " MenuGroups like '%" + estr[i] + "%'";
                
                if(i<(estr.Length-1))
                {
                    strcondition += " or ";                    
                }
            }

            if (strcondition == "")
            {
                strcondition = " ( MenuGroups like '')";
            }
            else
            {
                strcondition = " ( " + strcondition + " ) ";
            }

            string sql = @"select MenuID, MenuParentID, MenuName, MenuGIF, MenuURL, MenuTarget
                            from KPI_Menu 
                            where (MenuParentID = '') and MenuIsDisplay=1 and MenuIsValid=1 and {0} 
                            order by MenuIndex";

            sql = string.Format(sql, strcondition);

            return DBAccess.GetRelation().ExecuteDataset(sql);

        }

        /// <summary>
        /// 得到菜单列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetLeafMenus(string strUserCode)
        {
            //得到当前用户的权限
            string strUserGroup = KPI_UserDal.GetUserGroups(strUserCode);

            string[] estr = strUserGroup.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            string strcondition = "";
            for (int i = 0; i < estr.Length; i++)
            {
                strcondition += " MenuGroups like '%" + estr[i] + "%'";

                if (i < (estr.Length - 1))
                {
                    strcondition += " or ";
                }
            }

            if (strcondition == "")
            {
                strcondition = " ( MenuGroups like '')";
            }
            else
            {
                strcondition = " ( " + strcondition + " ) ";
            }

            string sql = @"select MenuID, MenuParentID, MenuName, MenuGIF, MenuURL, MenuTarget
                            from KPI_Menu 
                            where (MenuParentID <> '') and MenuIsDisplay=1 and MenuIsValid=1 and {0} 
                            order by MenuIndex";

            sql = string.Format(sql, strcondition);

            return DBAccess.GetRelation().ExecuteDataset(sql);

        }

        /// <summary>
        /// 得到目录列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDirectories()
        {
            string sql = "select MenuName[Name], MenuID[ID] from KPI_Menu where MenuType=1 order by MenuIndex";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到目录列表
        /// </summary>
        /// <returns></returns>
        public static bool InitilizeKPI_Menu()
        {
            //clear
            string sql = @"delete From KPI_Menu ";

            DBAccess.GetRelation().ExecuteNonQuery(sql);

            //add 系统菜单
            KPI_MenuEntity sysMenu = new KPI_MenuEntity();
            sysMenu.MenuID = Guid.NewGuid().ToString();
            sysMenu.MenuParentID = "";
            sysMenu.MenuCode = "";
            sysMenu.MenuName = "系统管理";
            sysMenu.MenuDesc = "SIS系统管理，模块配置等导航";
            sysMenu.MenuIsDisplay = 1;
            sysMenu.MenuIndex = 6;
            sysMenu.MenuType = 1;
            sysMenu.MenuURL = "";
            sysMenu.MenuGIF = "6.gif";
            sysMenu.MenuTarget = 1;
            sysMenu.MenuGroups = "AA,AB,";
            sysMenu.MenuIsValid = 1;
            sysMenu.MenuNote = "";
            sysMenu.MenuCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            sysMenu.MenuModifyTime = sysMenu.MenuCreateTime;

            KPI_MenuDal.Insert(sysMenu);

            string parentid = sysMenu.MenuID;

            //add SIS系统菜单
            sysMenu = new KPI_MenuEntity();
            sysMenu.MenuID = Guid.NewGuid().ToString();
            sysMenu.MenuParentID = parentid;
            sysMenu.MenuCode = "";
            sysMenu.MenuName = "SIS系统管理";
            sysMenu.MenuDesc = "SIS系统管理菜单";
            sysMenu.MenuIsDisplay = 1;
            sysMenu.MenuIndex = 0;
            sysMenu.MenuType = 1;
            sysMenu.MenuURL = "..\\\\SISKPI\\\\KPI_Menu.aspx";
            sysMenu.MenuGIF = "101.gif";
            sysMenu.MenuTarget = 1;
            sysMenu.MenuGroups = "AA,";
            sysMenu.MenuIsValid = 1;
            sysMenu.MenuNote = "";
            sysMenu.MenuCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            sysMenu.MenuModifyTime = sysMenu.MenuCreateTime;

            KPI_MenuDal.Insert(sysMenu);

            //add SIS系统菜单
            sysMenu = new KPI_MenuEntity();
            sysMenu.MenuID = Guid.NewGuid().ToString();
            sysMenu.MenuParentID = parentid;
            sysMenu.MenuCode = "";
            sysMenu.MenuName = "SIS系统简介";
            sysMenu.MenuDesc = "SIS系统介绍菜单";
            sysMenu.MenuIsDisplay = 1;
            sysMenu.MenuIndex = 0;
            sysMenu.MenuType = 1;
            sysMenu.MenuURL = "sisintro.htm";
            sysMenu.MenuGIF = "101.gif";
            sysMenu.MenuTarget = 1;
            sysMenu.MenuGroups = "AA,AB,";
            sysMenu.MenuIsValid = 1;
            sysMenu.MenuNote = "";
            sysMenu.MenuCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            sysMenu.MenuModifyTime = sysMenu.MenuCreateTime;

            KPI_MenuDal.Insert(sysMenu);

            //add SIS系统菜单
            sysMenu = new KPI_MenuEntity();
            sysMenu.MenuID = Guid.NewGuid().ToString();
            sysMenu.MenuParentID = parentid;
            sysMenu.MenuCode = "";
            sysMenu.MenuName = "SIS趋势曲线";
            sysMenu.MenuDesc = "SIS CoreSight菜单";
            sysMenu.MenuIsDisplay = 1;
            sysMenu.MenuIndex = 0;
            sysMenu.MenuType = 1;
            sysMenu.MenuURL = "sispitrend.htm";
            sysMenu.MenuGIF = "101.gif";
            sysMenu.MenuTarget = 1;
            sysMenu.MenuGroups = "AA,AB,";
            sysMenu.MenuIsValid = 1;
            sysMenu.MenuNote = "";
            sysMenu.MenuCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            sysMenu.MenuModifyTime = sysMenu.MenuCreateTime;

            KPI_MenuDal.Insert(sysMenu);



            return true;
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
}
