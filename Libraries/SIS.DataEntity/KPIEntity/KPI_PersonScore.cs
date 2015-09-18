using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.DataEntity
{
    public class KPI_PersonScore
    {
        public string PersonID { get; set; }
        public DateTime CheckDate { get; set; }
        public Decimal? Score { get; set; }
    }
}
