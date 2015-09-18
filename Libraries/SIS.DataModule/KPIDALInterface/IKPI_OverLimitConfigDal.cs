using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DataEntity;


namespace SIS.DataAccess {
    public interface IKPI_OverLimitConfigDal {
        List<OverLimitConfigEntity> GetOverLimitConfigList(int startIndex, int pageSize, ref int totalCount);
        List<OverLimitConfigEntity> GetOverLimitConfigList(int startIndex, int pageSize, string searchName, ref int totalCount);
        OverLimitConfigEntity GetOverLimitConfig(string overLimitConfigID);
        List<KPI_RealTagEntity> GetRealTagList();
        bool Insert(OverLimitConfigEntity overLimitConfig);
        bool Update(OverLimitConfigEntity overLimitConfig);
        bool Delete(string deleteId);
        //Added by pyf 2013-09-17
        List<OverLimitConfigEntity> GetOverLimitConfigs();
        //End of Added.
    }
}
