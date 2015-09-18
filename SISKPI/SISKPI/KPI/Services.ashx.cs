using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft;
using Newtonsoft.Json;
using SIS.DataAccess;
using SIS.DataEntity;

namespace SISKPI.KPI {
    /// <summary>
    /// Services 的摘要说明
    /// </summary>
    public class Services : IHttpHandler {
        private KPI_InputTagDal m_DataAccess;

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "application/json";
            m_DataAccess = new KPI_InputTagDal();
            List<String> list = context.Request.Params.AllKeys.Where(p => p.Contains("DetailList")).ToList();
            String Action = context.Request.Params["Action"];
            if (Action == "SaveInputTag") SaveInputTag(context);
            if (Action == "DeleteInputTag") DeleteInputTag(context);
            if (Action == "SaveInputTagCategory") SaveInputTagCategory(context);
            if (Action == "DeleteInputTagCategory") DeleteInputTagCategory(context);
            
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
        #region InputTag

        private void SaveInputTag(HttpContext context) {
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;
            Msg msg;
            try {
                InputTagEntity InputTag = new InputTagEntity {
                    InputID = Request.Params["InputID"],
                    InputCode = Request.Params["InputCode"],
                    InputDesc = Request.Params["InputDesc"],
                    InputEngunit = Request.Params["InputEngunit"],
                    InputIndex = String.IsNullOrWhiteSpace(Request.Params["InputIndex"]) ? 1 : Convert.ToInt32(Request.Params["InputIndex"]),
                    InputType = String.IsNullOrWhiteSpace(Request.Params["InputType"]) ? 1 : Convert.ToInt32(Request.Params["InputType"])
                };
                bool Successed = m_DataAccess.SaveInputTag(InputTag) > 0;
                msg = new Msg {
                    Status = "ok",
                    Message = "数据保存成功！"
                };
                if (!Successed) {
                    msg = new Msg {
                        Status = "error",
                        Message = "数据保存失败！"
                    };
                }
            }
            catch (Exception ex) {
                msg = new Msg {
                    Status = "error",
                    Message = "数据保存失败！错误信息：" + ex.Message,
                    StackTrace = ex.StackTrace
                };
            }
            Response.Write(JsonConvert.SerializeObject(msg));
        }

        private void DeleteInputTag(HttpContext context) {
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;
            Msg msg;
            try {
                InputTagEntity InputTag = new InputTagEntity {
                    InputID = Request.Params["InputID"],
                    InputCode = Request.Params["InputCode"],
                    InputDesc = Request.Params["InputDesc"],
                    InputEngunit = Request.Params["InputEngunit"]
                };
                m_DataAccess.DeleteInputTag(InputTag);
                msg = new Msg {
                    Status = "ok",
                    Message = "数据删除成功！"
                };
            }
            catch (Exception ex) {
                msg = new Msg {
                    Status = "error",
                    Message = "数据删除失败！错误信息：" + ex.Message,
                    StackTrace = ex.StackTrace
                };
            }
            Response.Write(JsonConvert.SerializeObject(msg));
        }

        #endregion

        #region InputTagCategory

        private void SaveInputTagCategory(HttpContext context) {
            KPI_ConstantDal DataAccess = new KPI_ConstantDal();
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;
            Msg msg;
            try {
                ConstantEntity Constant = new ConstantEntity {
                    ConstantID = Request.Params["ConstantID"],
                    ConstantCode = "M",
                    ConstantName = Request.Params["ConstantName"],
                    ConstantValue = Request.Params["ConstantValue"]
                };
                bool Successed = DataAccess.SaveConstant(Constant);
                msg = new Msg {
                    Status = "ok",
                    Message = "数据保存成功！"
                };
                if (!Successed) {
                    msg = new Msg {
                        Status = "error",
                        Message = "数据保存失败！"
                    };
                }
            }
            catch (Exception ex) {
                msg = new Msg {
                    Status = "error",
                    Message = "数据保存失败！错误信息：" + ex.Message,
                    StackTrace = ex.StackTrace
                };
            }
            Response.Write(JsonConvert.SerializeObject(msg));
        }

        private void DeleteInputTagCategory(HttpContext context) {
            KPI_ConstantDal DataAccess = new KPI_ConstantDal();
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;
            Msg msg;
            try {
                ConstantEntity Constant = new ConstantEntity {
                    ConstantID = Request.Params["ConstantID"],
                    ConstantCode = "M",
                    ConstantName = Request.Params["ConstantName"],
                    ConstantValue = Request.Params["ConstantValue"]
                };
                DataAccess.DeleteConstant(Constant);
                msg = new Msg {
                    Status = "ok",
                    Message = "数据删除成功！"
                };
            }
            catch (Exception ex) {
                msg = new Msg {
                    Status = "error",
                    Message = "数据删除失败！错误信息：" + ex.Message,
                    StackTrace = ex.StackTrace
                };
            }
            Response.Write(JsonConvert.SerializeObject(msg));
        }
        #endregion
    }

    internal class Msg {

        public String Status {
            get;
            set;
        }

        public String Message {
            get;
            set;
        }

        public String StackTrace {
            get;
            set;
        }
    }
}