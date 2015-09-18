using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
//using System.Text.RegularExpressions;

using SIS.DataAccess;
using SIS.Assistant;
using SIS.DataEntity;
using SIS.Loger;
using SIS.Assistant.WS;
using SIS.DBControl;

namespace SISKPI
{
    public partial class KPI_SubECTagConfig4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility='';progress_update();");
                            
                
                //判断是否新建或编辑
                if (Request.QueryString["ecid"] != null)
                {
                    ViewState["ecid"] = Request.QueryString["ecid"].ToString();

                    btnApply.Enabled = true;

                    BindValues();                   
        
                }
                else
                {
                    //添加
                    ViewState["ecid"] = "";

                    txt_ECScore.Value = "5,4,3,2,1";
                    txt_ECExExp.Value = "'30maint'>610,'30maint'<580";
                    txt_ECExScore.Value = "0,0";

                    btnApply.Enabled = false;
                }
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
                btnApply.Enabled = false;

                return;
            }


            ECTagEntity mEntity = ECTagDal.GetEntity(ECID);

            lbl_ECCode.Text = "指标代码：" + mEntity.ECCode;
            lbl_ECName.Text = "指标名称：" + mEntity.ECName;

            //if(mEntity.ECType
            ddl_ECType.SelectedValue = mEntity.ECType.ToString();
            ddl_ECSort.Value = mEntity.ECSort.ToString();

            txt_ECExExp.Value = mEntity.ECExExp;
            txt_ECExScore.Value = mEntity.ECExScore;


            

        }
        
        protected void cbx_ECIsSort_CheckedChanged(object sender, EventArgs e)
        {
            string ecid = ViewState["ecid"].ToString();

            if (ecid == "")
            {
                btnApply.Enabled = false;

                return;
            }
            else
            {
                btnApply.Enabled = true;
            }

            ECTagEntity ect = new ECTagEntity();
            ect.ECID = ecid;
            ect.ECIsSort = cbx_ECIsSort.Checked ? 1 : 0;

            ECTagDal.Update(ect);
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

            txt_ECScore.Value = txt_ECScore.Value.Trim();
            txt_ECExExp.Value = txt_ECExExp.Value.Trim();
            txt_ECExScore.Value = txt_ECExScore.Value.Trim();

            string [] exexp = txt_ECExExp.Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string [] exscore = txt_ECExScore.Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            if (exexp.Length != exscore.Length)
            {
                msg += "例外表达式与例外得分不一致，请重新配置！\r\n";
                flag = true;
                return flag;
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
            ECTagEntity mEntity = new ECTagEntity();

            mEntity.ECID = ecid;
            //mEntity.UnitID = ddl_UnitID.SelectedValue;
            //mEntity.SeqID = ddl_SeqID.Value.Trim();
            //mEntity.KpiID = ddl_KpiID.SelectedValue;
            //mEntity.EngunitID = ddl_EngunitID.Value.Trim();
            //mEntity.CycleID = ddl_CycleID.Value.Trim();

            //mEntity.ECIsValid = int.Parse(ddl_ECIsValid.Value);
            //mEntity.ECIsCalc = int.Parse(ddl_ECIsCalc.Value);
            //mEntity.ECIsDisplay = int.Parse(ddl_ECIsDisplay.Value);
            //mEntity.ECIsTotal = int.Parse(ddl_ECIsTotal.Value);

            //mEntity.ECCode = txt_ECCode.Value.Trim();
            //mEntity.ECName = txt_ECName.Value.Trim();
            //mEntity.ECDesc = txt_ECDesc.Value.Trim();
            //mEntity.ECIndex = int.Parse(txt_ECIndex.Value.Trim());
            //mEntity.ECWeb = ddl_ECWeb.Value;

            ////
            //mEntity.ECDesign = txt_ECDesign.Value;
            //mEntity.ECOptimal = txt_ECOptimal.Value;
            //mEntity.ECMaxValue = int.Parse(txt_ECMaxValue.Value.Trim());
            //mEntity.ECMinValue = int.Parse(txt_ECMinValue.Value.Trim());
            //mEntity.ECWeight = int.Parse(txt_ECWeight.Value.Trim());

            //mEntity.ECNote = txt_ECNote.Value.Trim();
            //mEntity.ECCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            //mEntity.ECModifyTime = mEntity.ECCreateTime;

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
            mEntity.ECIsSort = cbx_ECIsSort.Checked ? 1 : 0;
            mEntity.ECType = int.Parse(ddl_ECType.SelectedValue);
            mEntity.ECSort= int.Parse(ddl_ECSort.Value);

            mEntity.ECScore = txt_ECScore.Value;
            mEntity.ECExExp = txt_ECExExp.Value;
            mEntity.ECExScore = txt_ECExScore.Value;
            
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

            //if (ViewState["ecid"].ToString() == "")
            //{
            //    //string ecid = PageControl.GetGuid();
            //    if (Insert(ecid))
            //    {
            //        //ViewState["ecid"] = ecid;

            //        MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");
            //    }
            //    else
            //    {
            //        MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");
            //    }
            //}
            //else
            //{
            if (Update())
            {
                MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
            }
            ////}
        }

 
        #endregion

       protected void btnStep1_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_SubECTagConfig1.aspx?ecid=" + ViewState["ecid"].ToString());

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
           //Response.Redirect("KPI_SubECTagConfig4.aspx?ecid=" + ViewState["ecid"].ToString());

       }

       protected void btnReturn_Click(object sender, EventArgs e)
       {
           Response.Redirect("KPI_ECTagConfig.aspx");

       }


    }
}
