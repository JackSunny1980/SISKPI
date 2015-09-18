using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIS.DataEntity;
using SIS.DataAccess;
using Common.Web.UI;

namespace SISKPI.KPI
{
    public partial class KPI_TeamSetting : System.Web.UI.Page
    {
        private readonly IKPI_TeamSettingDal teamSettingDal = DataModuleFactory.CreateKPI_TeamSettingDal();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Pager.CurrentPageIndex = 1;
                BindingData(this.Pager.CurrentPageIndex, this.Pager.PageSize);
                BindPlantData();
                BindShiftData();
                BindPersonData();
                BindTeamPersonData();
            }
        }
        private void BindingData(int startIndex, int pageSize)
        {
            int totalCount = 0;
            gvValue.DataSource = teamSettingDal.GetTeamList(startIndex, pageSize, ref totalCount);
            gvValue.DataBind();
            SettingPaging(totalCount);

        }

        private void BindPlantData()
        {
            List<KPI_PlantEntity> plantList = teamSettingDal.GetPlantList();
            plantList.Insert(0, new KPI_PlantEntity { PlantID = "P01", PlantName = "选择单元组" });
            dropPlantList.DataSource = plantList;
            dropPlantList.DataTextField = "PlantName";
            dropPlantList.DataValueField = "PlantID";
            dropPlantList.DataBind();
        }

        private void BindShiftData()
        {
            List<KPI_ShiftEntity> shiftList = teamSettingDal.GetShiftList();
            shiftList.Insert(0, new KPI_ShiftEntity { ShiftID = "S01", ShiftName = "选择运行值" });
            dropShiftList.DataSource = shiftList;
            dropShiftList.DataTextField = "ShiftName";
            dropShiftList.DataValueField = "ShiftID";
            dropShiftList.DataBind();
        }

        private void BindPersonData()
        {
            List<KPI_PersonEntity> personList = teamSettingDal.GetPersonList();
            foreach (var item in personList)
            {
                item.BindingValue = item.PersonID + "/" + item.PositionID + "/" + item.PositionName;
            }
            personList.Insert(0, new KPI_PersonEntity { BindingValue = "PE01", PersonName = "选择人员" });

            dropPersonList.DataSource = personList;
            dropPersonList.DataTextField = "PersonName";
            dropPersonList.DataValueField = "BindingValue";
            dropPersonList.DataBind();

        }

        private void BindTeamPersonData()
        {
            List<KPI_PersonEntity> personList = teamSettingDal.GetPersonList();
            personList.Insert(0, new KPI_PersonEntity { PersonID = "TP01", PersonName = "选择替班人员" });
            dropTeamPersonList.DataSource = personList;
            dropTeamPersonList.DataTextField = "PersonName";
            dropTeamPersonList.DataValueField = "PersonID";
            dropTeamPersonList.DataBind();
        }

        private void SettingPaging(int recordCount)
        {
            if (recordCount == 0)
            {
                this.Pager.Visible = false;
            }
            else
            {
                this.Pager.Visible = true;
                this.Pager.RecordCount = recordCount;
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            this.Pager.CurrentPageIndex = 1;
            BindingData(this.Pager.CurrentPageIndex, this.Pager.PageSize);
        }

        protected void Pager_PageChanged(object src, EventArgs e)
        {
            AspNetPager pager = src as AspNetPager;
            BindingData(pager.CurrentPageIndex, pager.PageSize);

        }

        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            if (hidFlag.Value == "CLEAR")
            {
                if (teamSettingDal.Delete())
                {
                    hidCode.Value = string.Empty;
                    this.Pager.CurrentPageIndex = 1;
                    BindingData(this.Pager.CurrentPageIndex, this.Pager.PageSize);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(hidCode.Value))
                {
                    string deleteId = hidCode.Value;
                    if (teamSettingDal.Delete(deleteId))
                    {
                        hidCode.Value = string.Empty;
                        this.Pager.CurrentPageIndex = 1;
                        BindingData(this.Pager.CurrentPageIndex, this.Pager.PageSize);
                    }
                }
                else if (!string.IsNullOrWhiteSpace(hidMultipleCode.Value))
                {
                    string[] deleteItem = hidMultipleCode.Value.Remove(0, 1).Split('&');
                    int i = 0;
                    foreach (var item in deleteItem)
                    {
                        if (teamSettingDal.Delete(item))
                        {
                            i++;
                        }
                        else
                        {
                            i--;
                        }
                    }

                    if (i == deleteItem.Length)
                    {
                        hidMultipleCode.Value = string.Empty;
                        this.Pager.CurrentPageIndex = 1;
                        BindingData(this.Pager.CurrentPageIndex, this.Pager.PageSize);
                    }
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string selectedPlant = hidSelectedPlant.Value;
            string selectedShift = hidSelectedShift.Value;
            string selectedShiftName = hidSelectedShiftName.Value;
            string selectedPerson = hidSelectPerson.Value;
            string selectedPosition = hidPosition.Value;
            string selectedTeamPerson = hidSelectedTeamPerson.Value;
            string teamNote = txtTeamNote.Text;

            KPI_TeamEntity teamEntity = new KPI_TeamEntity
            {
                PlantID = selectedPlant,
                ShiftID = selectedShift,
                ShiftName=selectedShiftName,
                PersonID = selectedPerson,
                PositionID = selectedPosition,
                TeamPersonID = selectedTeamPerson,
                TeamNote = teamNote
            };

            if (hidFlag.Value == "INSERT")
            {
                if (teamSettingDal.Insert(teamEntity))
                {
                    this.Pager.CurrentPageIndex = 1;
                    BindingData(this.Pager.CurrentPageIndex, this.Pager.PageSize);
                }
            }
            else
            {
                teamEntity.TeamID = hidCode.Value;
                if (teamSettingDal.Update(teamEntity))
                {
                    hidCode.Value = string.Empty;
                    this.Pager.CurrentPageIndex = 1;
                    BindingData(this.Pager.CurrentPageIndex, this.Pager.PageSize);
                }
            }
        }

        protected void btnBatch_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_SubTeamBatch.aspx");
        }
    }
}
