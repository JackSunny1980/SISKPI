using SIS.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.DataAccess
{
    public interface IKPI_PersonScoreDal
    {
        List<KPI_PersonScore> SearchPersonScore(string personId, DateTime startTime, DateTime endTime);
    }
}
