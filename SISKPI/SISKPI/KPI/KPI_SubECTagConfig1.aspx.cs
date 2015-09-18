using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using SIS.DataAccess;
using SIS.Assistant;
using SIS.DataEntity;
using SIS.Loger;
using SIS.Assistant.WS;
using SIS.DBControl;

namespace SISKPI
{
    public partial class KPI_SubECTagConfig1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility='';progress_update();");
                            
                //机组信息
                DataTable dt = KPI_UnitDal.GetUnits("");
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_UnitID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //设备信息
                dt = KPI_SeqDal.GetSeqs();
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_SeqID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //指标信息
                dt = KpiDal.GetKpis();
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_KpiID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //单位信息
                dt = EngunitDal.GetEngunits();
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_EngunitID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //周期信息
                dt = CycleDal.GetCycles();
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_CycleID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //网页集信息
                //信息在KPI_Constant表中
                dt = KPI_ConstantDal.GetECWebs();
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_ECWeb.Items.Add(new ListItem(dr["Name"].ToString(), dr["Value"].ToString()));
                }

                //判断是否新建或编辑
                if (Request.QueryString["ecid"] != null)
                {
                    ViewState["ecid"] = Request.QueryString["ecid"].ToString();

                    BindValues(); 
                }
                else
                {
                    //添加
                    ViewState["ecid"] = "";

                    //
                    ddl_ECIsValid.Value = "1";
                    ddl_ECIsCalc.Value = "1";
                    ddl_ECIsDisplay.Value = "1";
                    ddl_ECIsTotal.Value = "1";

                    SetECCodeAndName();

                    txt_ECIndex.Value = "100";
                    txt_ECWeight.Value = "1";
                    txt_ECDenom.Value = "1";
                    txt_ECCalcClass.Value = "1";
                }

                btnApply.Visible = true;
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindValues()
        {
            string ECID = ViewState["ecid"].ToString();

            if (ECID == "")
            {
                return;
            }

            ECTagEntity mEntity = ECTagDal.GetEntity(ECID);

            ddl_UnitID.SelectedValue = mEntity.UnitID;
            ddl_SeqID.Value = mEntity.SeqID;
            ddl_KpiID.SelectedValue = mEntity.KpiID;
            ddl_EngunitID.Value = mEntity.EngunitID;
            ddl_CycleID.Value = mEntity.CycleID;

            ddl_ECIsValid.Value = mEntity.ECIsValid.ToString();
            ddl_ECIsCalc.Value = mEntity.ECIsCalc.ToString();
            ddl_ECIsAsses.Value = mEntity.ECIsAsses.ToString();
            ddl_ECIsZero.Value = mEntity.ECIsZero.ToString();

            ddl_ECIsDisplay.Value = mEntity.ECIsDisplay.ToString();
            ddl_ECIsTotal.Value = mEntity.ECIsTotal.ToString();
            ddl_ECWeb.Value = mEntity.ECWeb;

            txt_ECCode.Value = mEntity.ECCode;
            txt_ECName.Value = mEntity.ECName;
            txt_ECDesc.Value = mEntity.ECDesc;
            txt_ECIndex.Value = mEntity.ECIndex.ToString();
            txt_ECDesign.Value = mEntity.ECDesign;
			if (mEntity.ECMaxValue == decimal.MinValue)
            {
                txt_ECMaxValue.Value = "";
            }
            else
            {
                txt_ECMaxValue.Value = mEntity.ECMaxValue.ToString();
            }
			if (mEntity.ECMinValue == decimal.MinValue)
            {
                txt_ECMinValue.Value = "";
            }
            else
            {
                txt_ECMinValue.Value = mEntity.ECMinValue.ToString();
            }
            //txt_ECMinValue.Value = mEntity.ECMinValue.ToString();
            txt_ECWeight.Value = mEntity.ECWeight.ToString();
            txt_ECDenom.Value = mEntity.ECDenom.ToString();
            txt_ECCalcClass.Value = mEntity.ECCalcClass.ToString();

            txt_ECNote.Value = mEntity.ECNote;

        }


        #region 【参数验证】  

        /// <summary>
        /// 页面信息检查
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CheckVal(out string msg)
        {
            bool flag=false;
            msg = "";

            string sID = ViewState["ecid"].ToString();

            txt_ECCode.Value = txt_ECCode.Value.Trim();
            txt_ECName.Value = txt_ECName.Value.Trim();
            txt_ECIndex.Value = txt_ECIndex.Value.Trim();
            txt_ECMaxValue.Value = txt_ECMaxValue.Value.Trim();
            txt_ECMinValue.Value = txt_ECMaxValue.Value.Trim();
            txt_ECWeight.Value = txt_ECWeight.Value.Trim();
            txt_ECDenom.Value = txt_ECDenom.Value.Trim();
            txt_ECCalcClass.Value = txt_ECCalcClass.Value.Trim();

            /////////////////////////////////////////////////////////////////////
            //
            if (txt_ECCode.Value.Equals(""))
            {
                msg += "代码不能为空！\r\n";
                flag = true;
                return flag;
            }
            else
            {
                if (!Regex.IsMatch(txt_ECCode.Value, "[A-Za-z0-9_]*$"))                    
                {
                    msg += "代码只能由数字、字母和下划线组成！\r\n";
                    flag = true;
                    return flag;
                }

                //在SQL表中是否存在
                if (ECTagDal.CodeExist(txt_ECCode.Value, sID)
                    || ALLDal.CodeExist(txt_ECCode.Value, sID))
                {
                    msg += "命名已存在，请检查！\r\n";
                    flag = true;
                    return flag;
                }

            }

            if (txt_ECName.Value.Equals(""))
            {
                msg += "名称不能为空！\r\n";
                flag = true;
                return flag;
            }

            if (txt_ECIndex.Value.Equals(""))
            {
                msg += "序号不能为空！\r\n";
                flag = true;
                return flag;
            }
            else
            {
                if (!Regex.IsMatch(txt_ECIndex.Value, "[0-9]*$"))
                {
                    msg += "序号只能由数字组成！\r\n";
                    flag = true;
                    return flag;
                }
            }
            
            if (!txt_ECMaxValue.Value.Equals(""))
            {
                if (!Regex.IsMatch(txt_ECMaxValue.Value, @"^\d*[.]?\d*$"))
                {
                    msg += "最值只能为数字组成！\r\n";
                }
            }

            if (!txt_ECMinValue.Value.Equals(""))
            {
                if (!Regex.IsMatch(txt_ECMinValue.Value, @"^\d*[.]?\d*$"))
                {
                    msg += "最值只能为数字组成！\r\n";
                }
            }

            if (txt_ECWeight.Value.Equals(""))
            {
                msg += "权重不能为空！\r\n";
                flag = true;
                return flag;
            }
            else
            {
                if (!Regex.IsMatch(txt_ECWeight.Value, @"^\d*[.]?\d*$"))
                {
                    msg += "权重只能为数字组成！\r\n";
                }
            }

            if (txt_ECDenom.Value.Equals(""))
            {
                msg += "分母不能为空！\r\n";
                flag = true;
                return flag;
            }
            else
            {
                double dout = 0;
                if (!double.TryParse(txt_ECDenom.Value, out dout) || dout==0)
                {
                    msg += "分母格式不正确且不能为0！\r\n";
                }
            }
            
            if (txt_ECCalcClass.Value.Equals(""))
            {
                msg += "计算等级不能为空！\r\n";
                flag = true;
                return flag;
            }
            else
            {
                if (!Regex.IsMatch(txt_ECCalcClass.Value, "[0-9]*$"))
                {
                    msg += "计算等级只能由数字组成！\r\n";
                    flag = true;
                    return flag;
                }
            }

            return flag;
        }


        #endregion


        #region 【插入、编辑方法】

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <returns></returns>
        bool Insert(string ecid)
        {
            //
            //插入时更新所有的字段
            //
            ECTagEntity mEntity = new ECTagEntity();

            mEntity.ECID = ecid;
            mEntity.UnitID = ddl_UnitID.SelectedValue;
            mEntity.SeqID = ddl_SeqID.Value.Trim();
            mEntity.KpiID = ddl_KpiID.SelectedValue;
            mEntity.EngunitID = ddl_EngunitID.Value.Trim();
            mEntity.CycleID = ddl_CycleID.Value.Trim();

            mEntity.ECCode = txt_ECCode.Value.Trim();
            mEntity.ECName = txt_ECName.Value.Trim();
            mEntity.ECDesc = txt_ECDesc.Value.Trim();
            mEntity.ECIndex = int.Parse(txt_ECIndex.Value);            
            mEntity.ECWeb = ddl_ECWeb.Value;
            mEntity.ECIsValid = int.Parse(ddl_ECIsValid.Value);

            mEntity.ECIsCalc = int.Parse(ddl_ECIsCalc.Value);
            mEntity.ECIsAsses = int.Parse(ddl_ECIsAsses.Value);
            mEntity.ECIsZero = int.Parse(ddl_ECIsZero.Value);
            mEntity.ECIsDisplay = int.Parse(ddl_ECIsDisplay.Value);
            mEntity.ECIsTotal = int.Parse(ddl_ECIsTotal.Value);
            mEntity.ECDesign = txt_ECDesign.Value;

            mEntity.ECOptimal = txt_ECOptimal.Value;
            if (txt_ECMaxValue.Value != "")
            {
				mEntity.ECMaxValue = decimal.Parse(txt_ECMaxValue.Value);
            }
            if (txt_ECMinValue.Value != "")
            {
				mEntity.ECMinValue = decimal.Parse(txt_ECMinValue.Value);
            }
			mEntity.ECWeight = decimal.Parse(txt_ECWeight.Value.Trim());
			mEntity.ECDenom = decimal.Parse(txt_ECDenom.Value.Trim());
            mEntity.ECCalcClass = int.Parse(txt_ECCalcClass.Value.Trim());
            mEntity.ECFilterExp = "";

            mEntity.ECCalcExp = "";
            mEntity.ECCalcDesc = "";
            mEntity.ECIsSnapshot = 0;
            mEntity.ECXLineType = 0;
            mEntity.ECXLineGetType = 0;
            mEntity.ECXLineXRealTag = "";

            mEntity.ECXLineYRealTag = "";
            mEntity.ECXLineZRealTag = "";
            mEntity.ECXLineXYZ = "";
            mEntity.ECScoreExp = "";
            mEntity.ECCurveGroup = "";
            mEntity.ECIsSort = 0;
            mEntity.ECType = 0;

            mEntity.ECSort = 0;
            mEntity.ECScore = "";
            mEntity.ECExExp = "";
            mEntity.ECExScore = "";
            mEntity.ECNote = txt_ECNote.Value.Trim();
            mEntity.ECCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            mEntity.ECModifyTime = mEntity.ECCreateTime;

            return ECTagDal.Insert(mEntity);
        }


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        bool Update()
        {
            string ecid = ViewState["ecid"].ToString();

            ECTagEntity mEntity = new ECTagEntity();

            mEntity.ECID = ecid;
            mEntity.UnitID = ddl_UnitID.SelectedValue;
            mEntity.SeqID = ddl_SeqID.Value.Trim();
            mEntity.KpiID = ddl_KpiID.SelectedValue;
            mEntity.EngunitID = ddl_EngunitID.Value.Trim();
            mEntity.CycleID = ddl_CycleID.Value.Trim();

            mEntity.ECIsValid = int.Parse(ddl_ECIsValid.Value);
            mEntity.ECIsCalc = int.Parse(ddl_ECIsCalc.Value);
            mEntity.ECIsAsses = int.Parse(ddl_ECIsAsses.Value);
            mEntity.ECIsZero = int.Parse(ddl_ECIsZero.Value);
            mEntity.ECIsDisplay = int.Parse(ddl_ECIsDisplay.Value);
            mEntity.ECIsTotal = int.Parse(ddl_ECIsTotal.Value);

            mEntity.ECCode = txt_ECCode.Value.Trim();
            mEntity.ECName = txt_ECName.Value.Trim();
            mEntity.ECDesc = txt_ECDesc.Value.Trim();
            //if (txt_ECIndex.Value != "")
            //{}
            mEntity.ECIndex = int.Parse(txt_ECIndex.Value);

            mEntity.ECWeb = ddl_ECWeb.Value;

            //
            mEntity.ECDesign = txt_ECDesign.Value;
            mEntity.ECOptimal = txt_ECOptimal.Value;
            if (txt_ECMaxValue.Value != "")
            {
				mEntity.ECMaxValue = decimal.Parse(txt_ECMaxValue.Value);
            }
            if (txt_ECMinValue.Value != "")
            {
				mEntity.ECMinValue = decimal.Parse(txt_ECMinValue.Value);
            }

			mEntity.ECWeight = decimal.Parse(txt_ECWeight.Value.Trim());
			mEntity.ECDenom = decimal.Parse(txt_ECDenom.Value.Trim());
            mEntity.ECCalcClass = int.Parse(txt_ECCalcClass.Value.Trim());


            mEntity.ECNote = txt_ECNote.Value.Trim();
            //mEntity.ECCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            mEntity.ECModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");


            return ECTagDal.Update(mEntity);
        }
       
        #endregion


        #region 【按钮事件、调用方法】   
   
         
        /// <summary>
        /// 数据提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApply_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (CheckVal(out msg))
            {
                MessageBox.popupClientMessage(this.Page, msg, "call();");
                return;
            }

            if (ViewState["ecid"].ToString() == "")
            {
                string ecid = PageControl.GetGuid();
                if (Insert(ecid))
                {
                    ViewState["ecid"] = ecid;

                    MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");
                }
            }
            else
            {
                if (Update())
                {
                    MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
                }
            }
        }

 
        #endregion

       protected void btnStep1_Click(object sender, EventArgs e)
       {
           //当前页不做什么事情。

       }

       protected void btnStep2_Click(object sender, EventArgs e)
       {
           Response.Redirect("KPI_SubECTagConfig2.aspx?ecid=" + ViewState["ecid"].ToString());
       }

       protected void btnStep3_Click(object sender, EventArgs e)
       {
           Response.Redirect("KPI_SubECTagConfig3.aspx?ecid=" + ViewState["ecid"].ToString());
       }

       protected void btnStep4_Click(object sender, EventArgs e)
       {
           Response.Redirect("KPI_SubECTagConfig4.aspx?ecid=" + ViewState["ecid"].ToString());

       }

       protected void btnReturn_Click(object sender, EventArgs e)
       {
           Response.Redirect("KPI_ECTagConfig.aspx");
       }

       protected void ddl_UnitID_SelectedIndexChanged(object sender, EventArgs e)
       {
           SetECCodeAndName();
       }

       protected void ddl_KpiID_SelectedIndexChanged(object sender, EventArgs e)
       {
           SetECCodeAndName();
       }

       void SetECCodeAndName()
       {
           string unitcode = KPI_UnitDal.GetUnitCode(ddl_UnitID.SelectedValue);
           string kpicode = KpiDal.GetKpiCode(ddl_KpiID.SelectedValue);

           txt_ECCode.Value = unitcode+"_" + kpicode + "_";
           txt_ECName.Value = ddl_UnitID.SelectedItem.Text + ddl_KpiID.SelectedItem.Text;
       }

    }
}
