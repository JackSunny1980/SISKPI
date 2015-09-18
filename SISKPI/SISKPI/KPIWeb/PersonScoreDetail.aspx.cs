using SIS.DataAccess;
using SIS.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SISKPI.KPIWeb {
    public partial class PersonScoreDetail : System.Web.UI.Page {
        //private readonly IKPI_PersonScoreDal personScoreDal = DataModuleFactory.CreatePersonScoreDal();

        protected override void OnLoad(EventArgs e) {
            if (!IsPostBack) {
                DataBind();
            }
            base.OnLoad(e);
        }

        public override void DataBind() {
            DateTime startTime = DateTime.Now;//Convert.ToDateTime(searchTime);
            DateTime endTime = startTime.AddMonths(1).AddDays(-1);
            String PersonID = Request.Params["PersonID"];
            IKPI_PersonScoreDal personScoreDal = DataModuleFactory.CreatePersonScoreDal();
            DetailRepeater.DataSource = personScoreDal.SearchPersonScore(PersonID, startTime, endTime);
            base.DataBind();
            //decimal maxScore = personScoreList.Max(p => p.Score).HasValue ? (decimal)personScoreList.Max(p => p.Score) : 0;
            //decimal minScore = personScoreList.Min(p => p.Score).HasValue ? (decimal)personScoreList.Min(p => p.Score) : 0;
            //lbSearchTime.Text = startTime.ToString("yyyy-MM") + " 个人得分明细";
            //lbHigh.InnerText = maxScore.ToString();
            //lbLow.InnerText = minScore.ToString();
            //lbHighTime.InnerText = personScoreList.FirstOrDefault(p => p.PersonID == personId && p.Score == maxScore).CheckDate.ToString("yyyy-MM-dd");
            //lbLowTime.InnerText = personScoreList.FirstOrDefault(p => p.PersonID == personId && p.Score == minScore).CheckDate.ToString("yyyy-MM-dd");
        }
    }
}