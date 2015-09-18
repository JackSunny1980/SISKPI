using SIS.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.DataEntity
{
    public class KPI_MainChartsEntity: EntityBase
    {
        public string Shift
        {
            get;
            set;
        }
        public string TotalScore
        {
            get;
            set;
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["Shift"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.Shift = dr["Shift"].ToString();
                }

                if (dr["TotalScore"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TotalScore = dr["TotalScore"].ToString();
                }
            }
            catch (System.Exception)
            {
                // 如果有必要,请处理你的异常代码
                return false;
            }
            finally
            {
                // 异常的finally代码
            }
            return true;
        }
    }
}
