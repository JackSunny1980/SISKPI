using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SIS.DBControl;
using SIS.Loger;
using SIS.Assistant;

using SIS.DataAccess;
using SIS.DataEntity;

namespace SISKPI.KPIReport
{
    /// <summary>
    /// KPI_Services 的摘要说明
    /// </summary>
    public class KPI_Services : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["Method"])
            {
                case "KPI_Web_Add":
                    KPI_Web_Add(context);
                    break;
                case "KPI_Web_Edit":
                    KPI_Web_Edit(context);
                    break;
                case "KPI_Web_Del":
                    KPI_Web_Del(context);
                    break;
                case "KPI_SubWeb_Add":
                    KPI_SubWeb_Add(context);
                    break;
                case "KPI_SubWeb_Edit":
                    KPI_SubWeb_Edit(context);
                    break;
                case "KPI_SubWeb_Del":
                    KPI_SubWeb_Del(context);
                    break;
                case "KPI_Average_Add":
                    KPI_Average_Add(context);
                    break;
                case "KPI_Average_Edit":
                    KPI_Average_Edit(context);
                    break;
                case "KPI_Average_Del":
                    KPI_Average_Del(context);
                    break;
                case "KPI_SubWeb_GetECNameByECCode":
                    KPI_SubWeb_GetECNameByECCode(context);
                    break;
                default:
                    context.Response.Write("{\"status\":\"无效参数！\"}");
                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region KPI_Web

        public void KPI_Web_Add(HttpContext context)
        {
            string sID = PageControl.GetGuid();
            string sCode = context.Request["WebCode"];
            string sDesc = context.Request["WebDesc"];
            string sType = context.Request["WebType"];
            string sNote = context.Request["WebNote"];

            if (sCode == "")
            {
                context.Response.Write("{\"status\":\"代码不能为空！\"}");
                return;
            }
            //名称是否重复
            if (KPI_WebDal.WebCodeExists(sCode, sID))
            {
                context.Response.Write("{\"status\":\"已存在相同的代码！\"}");
                return;
            }

            //更新
            KPI_WebEntity ote = new KPI_WebEntity();
            ote.WebID = sID;
            ote.WebCode = sCode;
            ote.WebDesc = sDesc;
            ote.WebType = int.Parse(sType);
            ote.WebNote = sNote;

            ote.WebCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            ote.WebModifyTime = ote.WebCreateTime;

            if (KPI_WebDal.Insert(ote))
            {
                context.Response.Write("{\"status\":\"ok\"}");
                return;
            }
            else
            {
                context.Response.Write("{\"status\":\"添加失败！\"}");
                return;
            }

        }
        public void KPI_Web_Edit(HttpContext context)
        {
            string sID = context.Request["WebID"];
            if (sID == "")
            {
                context.Response.Write("{\"status\":\"没有对象！\"}");
                return;
            }
            //string sCode = context.Request["WebCode"];
            string sDesc = context.Request["WebDesc"];
            string sType = context.Request["WebType"];
            string sNote = context.Request["Note"];

            //if (sCode == "")
            //{
            //    context.Response.Write("{\"status\":\"代码不能为空！\"}");
            //    return;
            //}
            ////名称是否重复
            //if (KPI_WebDal.WebCodeExists(sCode, sID))
            //{
            //    context.Response.Write("{\"status\":\"已存在相同的代码！\"}");
            //    return;
            //}

            //更新
            KPI_WebEntity ote = new KPI_WebEntity();
            ote.WebID = sID;
            //ote.WebCode = sCode;
            ote.WebDesc = sDesc;
            ote.WebType = int.Parse(sType);
            ote.WebNote = sNote;

            ote.WebModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (KPI_WebDal.Update(ote))
            {
                context.Response.Write("{\"status\":\"ok\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"修改失败！\"}");
            }

        }
        public void KPI_Web_Del(HttpContext context)
        {
            string WebCode = context.Request["WebCode"];
            if (KPI_WebDal.DeleteWebCode(WebCode))
            {
                context.Response.Write("{\"status\":\"ok\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"删除失败！\"}");
            }
        }

        #endregion

        #region KPI_SubWeb

        public void KPI_SubWeb_Add(HttpContext context)
        {
            //KeyID,WebCode,ECCode,ECName,KeyEngunit,KeyCalcType,KeyIndex
            string KeyID = Guid.NewGuid().ToString();
            string WebCode = context.Request["WebCode"];
            string ECCode = context.Request["ECCode"];
            string ECName = context.Request["ECName"];
            string KeyEngunit = context.Request["KeyEngunit"];
            string KeyCalcType = context.Request["KeyCalcType"];
            string KeyIndex = context.Request["KeyIndex"];
            int iKeyIndex = 0;

            if (!int.TryParse(KeyIndex, out iKeyIndex))
            {
                context.Response.Write("{\"status\":\"序号数字不合法！\"}");
                return;
            }

            if (KPI_WebKeyDal.KeyExists(ECCode, WebCode))
            {
                context.Response.Write("{\"status\":\"该指标已存在！\"}");
                return;
            }

            KPI_WebKeyEntity kwk = new KPI_WebKeyEntity();
            //
            kwk.KeyID = KeyID;
            kwk.ECID = ECTagDal.GetECIDByCode(ECCode);
            kwk.ECCode = ECCode;
            kwk.ECName = ECName;
            kwk.WebCode = WebCode;
            kwk.KeyEngunit = KeyEngunit;
            kwk.KeyCalcType = int.Parse(KeyCalcType);
            kwk.KeyIndex = iKeyIndex;
            kwk.KeyIsValid = 1;

            kwk.KeyNote = "";
            kwk.KeyCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            kwk.KeyModifyTime = kwk.KeyCreateTime;

            if (KPI_WebKeyDal.Insert(kwk))
            {
                context.Response.Write("{\"status\":\"ok\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"添加失败！\"}");
                return;
            }
        }
        public void KPI_SubWeb_Edit(HttpContext context)
        {
            string KeyID = context.Request["KeyID"];
            if (KeyID == "")
            {
                context.Response.Write("{\"status\":\"没有对象！\"}");
                return;
            }

            //string WebCode = context.Request["WebCode"];
            //string ECCode = context.Request["ECCode"];
            //string ECName = context.Request["ECName"];
            //string KeyEngunit = context.Request["KeyEngunit"];
            //string KeyCalcType = context.Request["KeyCalcType"];
            string KeyIndex = context.Request["KeyIndex"];
            int iKeyIndex = 0;

            if (!int.TryParse(KeyIndex, out iKeyIndex))
            {
                context.Response.Write("{\"status\":\"序号数字不合法！\"}");
                return;
            }

            //if (KPI_WebKeyDal.KeyExists(ECCode, WebCode))
            //{
            //    context.Response.Write("{\"status\":\"该指标已存在！\"}");
            //    return;
            //}

            KPI_WebKeyEntity kwk = new KPI_WebKeyEntity();
            //
            kwk.KeyID = KeyID;
            //kwk.ECID = KPI_ECTagDal.GetECIDByCode(ECCode);
            //kwk.ECCode = ECCode;
            //kwk.ECName = ECName;
            //kwk.WebCode = WebCode;
            //kwk.KeyEngunit = KeyEngunit;
            //kwk.KeyCalcType = int.Parse(KeyCalcType);
            kwk.KeyIndex = iKeyIndex;
            kwk.KeyIsValid = 1;

            //kwk.KeyNote = "";
            //kwk.KeyCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            kwk.KeyModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (KPI_WebKeyDal.Update(kwk))
            {
                context.Response.Write("{\"status\":\"ok\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"修改失败！\"}");
                return;
            }
        }
        public void KPI_SubWeb_Del(HttpContext context)
        {
            string KeyID = context.Request["KeyID"];
            if (KPI_WebKeyDal.DeleteKey(KeyID))
            {
                context.Response.Write("{\"status\":\"ok\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"删除失败！\"}");
            }
        }

        #endregion


        #region KPI_Average

        public void KPI_Average_Add(HttpContext context)
        {
            //KeyID,ECCode,ECName,KeyEngunit,KeyTarget1,KeyTarget2,KeyDesign,KeyDiffMoney,KeyOptMoney,KeyIndex

            string KeyID = Guid.NewGuid().ToString();

            string ECCode = context.Request["ECCode"];
            string ECName = context.Request["ECName"];
            string KeyEngunit = context.Request["KeyEngunit"];
            string KeyTarget1 = context.Request["KeyTarget1"];
            string KeyTarget2 = context.Request["KeyTarget2"];
            string KeyDesign = context.Request["KeyDesign"];
            string KeyDiffMoney = context.Request["KeyDiffMoney"];
            string KeyOptMoney = context.Request["KeyOptMoney"];
            string KeyIndex = context.Request["KeyIndex"];

            double dKeyDiffMoney = 0;
            double dKeyOptMoney = 0;
            int iKeyIndex = 0;

            if (!int.TryParse(KeyIndex, out iKeyIndex))
            {
                context.Response.Write("{\"status\":\"序号数字不合法！\"}");
                return;
            }
            if (!double.TryParse(KeyDiffMoney, out dKeyDiffMoney))
            {
                context.Response.Write("{\"status\":\"偏差奖金数字不合法！\"}");
                return;
            }
            if (!double.TryParse(KeyOptMoney, out dKeyOptMoney))
            {
                context.Response.Write("{\"status\":\"最优奖金数字不合法！\"}");
                return;
            }
            if (AVGDal.KeyExists(ECCode))
            {
                context.Response.Write("{\"status\":\"该指标已存在！\"}");
                return;
            }

            AVGEntity kwk = new AVGEntity();
            //
            kwk.KeyID = Guid.NewGuid().ToString();
            kwk.ECID = ECTagDal.GetECIDByCode(ECCode);
            kwk.ECCode = ECCode;
            kwk.ECName = ECName;
            kwk.KeyEngunit = KeyEngunit;
            kwk.KeyTarget1 = KeyTarget1 == "NOTAG" ? "" : KeyTarget1;
            kwk.KeyTarget2 = KeyTarget2 == "NOTAG" ? "" : KeyTarget2;
            kwk.KeyDesign = KeyDesign;
            kwk.KeyDIffMoney = dKeyDiffMoney;
            kwk.KeyOptMoney = dKeyOptMoney;
            kwk.KeyIndex = iKeyIndex;

            if (AVGDal.Insert(kwk))
            {
                context.Response.Write("{\"status\":\"ok\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"添加失败！\"}");
                return;
            }
        }
        public void KPI_Average_Edit(HttpContext context)
        {
            //KeyID,ECCode,ECName,KeyEngunit,KeyTarget1,KeyTarget2,KeyDesign,KeyDiffMoney,KeyOptMoney,KeyIndex

            string KeyID = context.Request["KeyID"];
            if (KeyID == "")
            {
                context.Response.Write("{\"status\":\"没有对象！\"}");
                return;
            }
            //string ECCode = context.Request["ECCode"];
            //string ECName = context.Request["ECName"];
            //string KeyEngunit = context.Request["KeyEngunit"];
            //string KeyTarget1 = context.Request["KeyTarget1"];
            //string KeyTarget2 = context.Request["KeyTarget2"];
            string KeyDesign = context.Request["KeyDesign"];
            string KeyDiffMoney = context.Request["KeyDiffMoney"];
            string KeyOptMoney = context.Request["KeyOptMoney"];
            string KeyIndex = context.Request["KeyIndex"];

            double dKeyDiffMoney = 0;
            double dKeyOptMoney = 0;
            int iKeyIndex = 0;

            if (!int.TryParse(KeyIndex, out iKeyIndex))
            {
                context.Response.Write("{\"status\":\"序号数字不合法！\"}");
                return;
            }
            if (!double.TryParse(KeyDiffMoney, out dKeyDiffMoney))
            {
                context.Response.Write("{\"status\":\"偏差奖金数字不合法！\"}");
                return;
            }
            if (!double.TryParse(KeyOptMoney, out dKeyOptMoney))
            {
                context.Response.Write("{\"status\":\"最优奖金数字不合法！\"}");
                return;
            }

            AVGEntity kwk = new AVGEntity();
            //
            kwk.KeyID = KeyID;
            //kwk.ECID = KPI_ECTagDal.GetECIDByCode(ECCode);
            //kwk.ECCode = ECCode;
            //kwk.ECName = ECName;
            //kwk.KeyEngunit = KeyEngunit;
            //kwk.KeyTarget1 = KeyTarget1 == "NOTAG" ? "" : KeyTarget1;
            //kwk.KeyTarget2 = KeyTarget2 == "NOTAG" ? "" : KeyTarget2;
            kwk.KeyDesign = KeyDesign;
            kwk.KeyDIffMoney = dKeyDiffMoney;
            kwk.KeyOptMoney = dKeyOptMoney;
            kwk.KeyIndex = iKeyIndex;

            if (AVGDal.Update(kwk))
            {
                context.Response.Write("{\"status\":\"ok\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"修改失败！\"}");
                return;
            }
        }
        public void KPI_Average_Del(HttpContext context)
        {
            string KeyID = context.Request["KeyID"];
            if (AVGDal.DeleteKey(KeyID))
            {
                context.Response.Write("{\"status\":\"ok\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"删除失败！\"}");
            }
        }

        #endregion

        public void KPI_SubWeb_GetECNameByECCode(HttpContext context)
        {
            string ECCode = context.Request["ECCode"];
            string ECName = "";
            string KeyEngunit = "";
            string[] strnt = ECTagDal.GetNameEngunit(ECCode).Split(new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
            if (strnt.Length == 2)
            {
                ECName = strnt[0];
                KeyEngunit = strnt[1];
            }
            else if (strnt.Length == 1)
            {
                ECName = strnt[0];
                KeyEngunit = "";
            }
            else
            {
                ECName = "";
                KeyEngunit = "";
            }
            context.Response.Write("{\"status\":\"ok\",\"ECName\":\"" + ECName + "\",\"KeyEngunit\":\"" + KeyEngunit + "\"}");
        }
    }
}