using Newtonsoft.Json;
using SIS.DataAccess;
using SIS.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SISKPI.KPI {

    public partial class KPI_ManagementScore : Page,ICallbackEventHandler {

        private String m_CallbackResult;
        protected string ClientCallback;

        #region 重写方法

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (!IsPostBack) {               
                DataBind();
                ClientCallback = ClientScript.GetCallbackEventReference(this, "argument", "processCallback", "null");
            }
        }

        public override void DataBind() {
            using (ManagementScoreDataAccess DataAccess = new ManagementScoreDataAccess()) {
                ManagementScores.DataSource = DataAccess.GetManagementScores("1", "2015-01");
            }
            base.DataBind();
        }

        #endregion

      
    

        #region 事件

        #endregion

        #region ICallbackEventHandler

        public string GetCallbackResult() {
            return m_CallbackResult;
        }

        public void RaiseCallbackEvent(string eventArgument) {
            String Action = eventArgument.Split(',')[0].Split('=')[1];
            String Argument = eventArgument.Split(',')[1].Split('=')[1];
            if (Action == "InputTags") {
                KPI_InputTagDal InputTagDataAccess = new KPI_InputTagDal();
                List<InputTagEntity> InputTags = InputTagDataAccess.GetInputTags(Argument);
                var Result = new {
                    Action = Action,
                    data = InputTags.Select(p => new {
                        InputID = p.InputID,
                        InputDesc = p.InputDesc
                    }).ToList()
                };
                m_CallbackResult = JsonConvert.SerializeObject(Result);
            }
            if (Action == "InitialPerson") {
                List<ManagementScoreEntity> List = new List<ManagementScoreEntity>();
                ManagementScoreEntity Entity;
                for (int i = 0; i < 10; i++) {
                    Entity = new ManagementScoreEntity {
                        Rate = 80.0m,
                        Shift = 1,
                        PersonName = "韩娜",
                        PositionName = "值长"
                    };
                    List.Add(Entity);
                }
                var Result = new {
                    Action = Action,
                    data = List
                };
                m_CallbackResult = JsonConvert.SerializeObject(Result);
            }
        }

        #endregion

    }
}