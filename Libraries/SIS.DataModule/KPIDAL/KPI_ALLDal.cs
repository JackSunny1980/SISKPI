using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
 

namespace SIS.DataAccess
{
    public class ALLDal
    {
        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="GroupName"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool CodeExist(string Code, string ID)
        {
            //insert时， ID == ""
            //update时， ID == 必须为 原来的ID
            //Excel find  时， 对应表单操作 ID == ""

            bool bExist = false;

            bExist = KPI_RealTagDal.CodeExist(Code, ID)
                     || KPI_InputTagDal.CodeExist(Code, ID)
                     || CurveTagDal.CodeExist(Code, ID)
                     || ECTagDal.CodeExist(Code, ID)
                     || KPI_SATagDal.CodeExist(Code, ID);
            

            return bExist;
        }
        
    }
}
