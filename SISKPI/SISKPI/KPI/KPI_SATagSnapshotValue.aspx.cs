using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIS.DataEntity;
using SIS.DataAccess;

namespace SISKPI.KPI {

	public partial class KPI_SATagSnapshotValue : System.Web.UI.Page {

		private String Shift {
			get;
			set;
		}

		private DateTime ShiftStartTime {
			get;
			set;
		}

		private DateTime ShiftEndTime {
			get;
			set;
		}

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			if (!IsPostBack) {
				String strStartTime = "";
				String strEndTime = "";
				String CurrentShift = "";
				String Period = "";
				List<KPI_UnitEntity> UnitList = KPI_UnitDal.GetAllEntity();
				if (UnitList.Count > 0) {
					String UnitID = UnitList.First().UnitID;
					KPI_UnitEntity Entity = KPI_UnitDal.GetEntity(UnitID);
					if (Entity != null) {
						String strWorkID = Entity.WorkID;
						String strCurrentMinute = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
						KPI_WorkDal.GetShiftAndPeriod(strWorkID, strCurrentMinute,
							ref CurrentShift, ref Period, ref strStartTime, ref strEndTime);
					}
					lblShift.Text = CurrentShift;
					Shift = CurrentShift;
					ShiftStartTime = Convert.ToDateTime(strStartTime);
					ShiftEndTime = Convert.ToDateTime(strEndTime);
				}
				DataBind();
			}
		}

		public override void DataBind() {
			using (KPI_SATagValueDal SATagValue = new KPI_SATagValueDal()) {
				SATagValueRepeater.DataSource = SATagValue.GetKPI_SATagSnapshotValues(Shift, ShiftStartTime, ShiftEndTime);
			}
			base.DataBind();
		}

	}
}