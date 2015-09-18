using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.DataEntity
{
    public class KPI_ScorebookEntity
    {
        public string PositionID { get; set; }
        public string PositionName { get; set; }
        public string Shift { get; set; }
		public string UnitName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }      
		public Decimal ECScore { get; set; }
		public Decimal SAScore { get; set; }
        public Decimal TotalScore { get; set; }
		
    }
}
