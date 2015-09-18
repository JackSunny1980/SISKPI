using SIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SISKPI.KPI {
    public partial class SelectPersonDialog : System.Web.UI.Page {


        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (!IsPostBack) {
                ClientInitial();
                DataBind();
            }
        }

        public override void DataBind() {
            PersonRepeater.DataSource = KPI_PersonDal.GetPersons(drpUnits.SelectedValue,drpShifts.SelectedValue);
            base.DataBind();
        }

        private void ClientInitial() {
            DataTable Plants = KPI_PlantDal.GetDataTable();
            drpPlants.DataSource = Plants;
            drpPlants.DataValueField = "PlantID";
            drpPlants.DataTextField = "PlantName";
            drpPlants.DataBind();
            if (Plants.Rows.Count > 0) {
                String PlantID = Plants.Rows[0]["PlantID"] + "";
                drpUnits.DataSource = KPI_UnitDal.GetUnits(PlantID);
                drpUnits.DataValueField = "ID";
                drpUnits.DataTextField = "Name";
                drpUnits.DataBind();
            }
        }

        protected void btnSearch_Click(Object sender, EventArgs e) {
            DataBind();
        }

        protected void btnClose_Click(Object sender, EventArgs e) {
            DataBind();
        }

        protected void drpPlants_SelectedIndexChanged(object sender, EventArgs e) {
            drpUnits.DataSource = KPI_UnitDal.GetUnits(drpPlants.SelectedValue);
            drpUnits.DataValueField = "ID";
            drpUnits.DataTextField = "Name";
            drpUnits.DataBind();
        }
    }
}