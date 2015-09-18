using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

using SIS.DataAccess;
using SIS.Assistant;
using SIS.DataEntity;
using SIS.Loger;
using System.Web.UI.HtmlControls;
using SIS.Assistant.WS;
using SIS.DBControl;

namespace SISKPI
{
    public partial class KPI_SubUnitConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility='';progress_update();");
             
                btnCancel.Attributes.Add("onclick", "javascript:return confirm('确认关闭吗？');");

                //电厂信息
                DataTable dt = KPI_PlantDal.GetPlants("");
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_PlantID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //倒班信息
                dt = KPI_WorkDal.GetWorks();
                ddl_UnitWorkID.Items.Add(new ListItem("无", "NULL"));
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_UnitWorkID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }


                //判断是否新建或编辑
                if (Request.QueryString["unitid"] != null)
                {
                    ViewState["unitid"] = Request.QueryString["unitid"].ToString();

                    BindValues();                   
        
                }
                else
                {
                    //添加
                    ViewState["unitid"] = "";

                    //
                    txt_UnitIndex.Value = "1";
                    ddl_UnitIsValid.Value = "1";
                    ddl_UnitIsKPI.Value = "1";
                    ddl_UnitIsSnapshot.Value = "1";
                    ddl_UnitIsSort.Value = "0";
                    ddl_UnitIsSecurity.Value = "0";
                    ddl_UnitIsPower.Value = "0";
                    

                    btnApply.Visible = true;
                    btnCancel.Visible = true;

                    //ImgSeqReview.Visible = false;
                    //ImgSeqConfig.Visible = false;

                }
            }
        }


        #region 【机组参数标签点部分验证】

        /// <summary>
        /// 页面信息检查
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CheckVal(out string msg)
        {
            bool flag=false;
            msg = "";

            txt_UnitName.Value = txt_UnitName.Value.Trim();
            txt_UnitPrefix.Value = txt_UnitPrefix.Value.Trim();
            txt_UnitIndex.Value = txt_UnitIndex.Value.Trim();
            txt_UnitMW.Value = txt_UnitMW.Value.Trim();
            
            if (ddl_PlantID.Value.Equals(""))
            {
                msg += "电厂选择不能为空！\r\n";
                flag = true;
                return flag;
            }

            if (txt_UnitName.Value.Equals(""))
            {
                msg += "机组名称不能为空！\r\n";
                flag = true;
                return flag;
            }
            else
            {
                if (txt_UnitName.Value.Trim() != "")
                {
                    //在SQL表中是否存在
                    if (KPI_UnitDal.UnitNameExists(txt_UnitName.Value.Trim(), ViewState["unitid"].ToString()))
                    {
                        msg += "命名已存在，请检查！\r\n";
                        return true;
                    }
                }

            }

            if (txt_UnitCode.Value.Equals(""))
            {
                msg += "机组代码不能为空！\r\n";
                flag = true;
                return flag;
            }

            if (txt_UnitIndex.Value.Equals(""))
            {
                msg += "机组序号不能为空！\r\n";
                flag = true;
                return flag;
            }
            else
            {

                int n = 0;
                if (!int.TryParse(txt_UnitIndex.Value, out n))
                {
                    msg += "只能为数字！\r\n";
                    flag = true;
                    return flag;
                }

            }

            if (ddl_UnitIsValid.Value == "")
            {
                msg += "参数有效性选择错误！\r\n";
                flag = true;
                return flag;
            }

            if (txt_UnitPrefix.Value.Equals(""))
            {
                msg += "机组前缀不能为空！\r\n";
                flag = true;
                return flag;
            }

            /////////////////////////////////////////////////////////////////////
            //额定参数
            //
            if (txt_UnitMW.Value.Equals(""))
            {
                msg += "额定负荷值不能为空！\r\n";
                flag = true;
                return flag;
            }

            if (ddl_UnitWorkID.Value == "NULL")
            {
                msg += "倒班配置不能为空！\r\n";
                flag = true;
                return flag;

            }

            //int IsCheck = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["IsCheck"].ToString());

            //if(IsCheck==1 && TagCheck(out msg))
            //{
            //    flag = true;

            //    return flag;
            //}

            return flag;
        }
                       
        
        #endregion


        #region 【插入、编辑、绑定方法】

        string msg = "";


        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindValues()
        {
            string UnitID = ViewState["unitid"].ToString();

            KPI_UnitEntity mEntity = KPI_UnitDal.GetEntity(UnitID);

            ddl_PlantID.Value = mEntity.PlantID;
            txt_UnitCode.Value = mEntity.UnitCode;
            txt_UnitName.Value = mEntity.UnitName;
            txt_UnitDesc.Value = mEntity.UnitDesc;
            txt_UnitIndex.Value = mEntity.UnitIndex.ToString();
            txt_UnitPrefix.Value = mEntity.UnitPrefix;
            ddl_UnitIsValid.Value = mEntity.UnitIsValid.ToString();

            txt_UnitMW.Value = mEntity.UnitMW.ToString();
            txt_UnitMWTag.Value = mEntity.UnitMWTag;
            
            txt_UnitCondition.Value = mEntity.UnitCondition;
            ddl_UnitIsKPI.Value = mEntity.UnitIsKPI.ToString();
            ddl_UnitIsSnapshot.Value = mEntity.UnitIsSnapshot.ToString();
            ddl_UnitIsSort.Value = mEntity.UnitIsSort.ToString();
            ddl_UnitIsSecurity.Value = mEntity.UnitIsSecurity.ToString();
            ddl_UnitIsPower.Value = mEntity.UnitIsPower.ToString();

            if(mEntity.WorkID==null || mEntity.WorkID=="")
            {
                ddl_UnitWorkID.Value = "NULL";
            }
            else
            {
                ddl_UnitWorkID.Value = mEntity.WorkID;

            }

            txt_UnitNote.Value = mEntity.UnitNote;


        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <returns></returns>
        bool Insert()
        {
            string unitid = PageControl.GetGuid();

            KPI_UnitEntity mEntity = new KPI_UnitEntity();

            mEntity.UnitID = unitid;
            mEntity.PlantID = ddl_PlantID.Value;
            mEntity.UnitCode = txt_UnitCode.Value.Trim();
            mEntity.UnitName = txt_UnitName.Value.Trim();
            mEntity.UnitDesc = txt_UnitDesc.Value.Trim();
            mEntity.UnitIndex = int.Parse(txt_UnitIndex.Value.Trim());
            mEntity.UnitIsValid = int.Parse(ddl_UnitIsValid.Value);
            mEntity.UnitPrefix = txt_UnitPrefix.Value;
			mEntity.UnitMW = decimal.Parse(txt_UnitMW.Value);
            mEntity.UnitMWTag = txt_UnitMWTag.Value;
            mEntity.UnitCondition = txt_UnitCondition.Value.Replace("'","''");

            mEntity.UnitIsKPI = int.Parse(ddl_UnitIsKPI.Value);
            mEntity.UnitIsSnapshot = int.Parse(ddl_UnitIsSnapshot.Value);
            mEntity.UnitIsSort = int.Parse(ddl_UnitIsSort.Value);
            mEntity.UnitIsSecurity = int.Parse(ddl_UnitIsSecurity.Value);
            mEntity.UnitIsPower = int.Parse(ddl_UnitIsPower.Value);

            mEntity.WorkID = ddl_UnitWorkID.Value;

            mEntity.UnitNote = txt_UnitNote.Value.Trim();
            mEntity.UnitCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            mEntity.UnitModifyTime = mEntity.UnitCreateTime;


            return KPI_UnitDal.Insert(mEntity);
        }


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        bool Update()
        {    
            string unitid = ViewState["unitid"].ToString();

            KPI_UnitEntity mEntity = new KPI_UnitEntity();

            mEntity.UnitID = unitid;
            mEntity.PlantID = ddl_PlantID.Value;
            mEntity.UnitCode = txt_UnitCode.Value.Trim();
            mEntity.UnitName = txt_UnitName.Value.Trim();
            mEntity.UnitDesc = txt_UnitDesc.Value.Trim();
            mEntity.UnitIndex = int.Parse(txt_UnitIndex.Value.Trim());
            mEntity.UnitIsValid = int.Parse(ddl_UnitIsValid.Value);
            mEntity.UnitPrefix = txt_UnitPrefix.Value;
			mEntity.UnitMW = decimal.Parse(txt_UnitMW.Value);
            mEntity.UnitMWTag = txt_UnitMWTag.Value;
            mEntity.UnitCondition = txt_UnitCondition.Value;//.Replace("'", "''");

            mEntity.UnitIsKPI = int.Parse(ddl_UnitIsKPI.Value);
            mEntity.UnitIsSnapshot = int.Parse(ddl_UnitIsSnapshot.Value);
            mEntity.UnitIsSort = int.Parse(ddl_UnitIsSort.Value);
            mEntity.UnitIsSecurity = int.Parse(ddl_UnitIsSecurity.Value);
            mEntity.UnitIsPower = int.Parse(ddl_UnitIsPower.Value);

            mEntity.WorkID = ddl_UnitWorkID.Value;

            mEntity.UnitNote = txt_UnitNote.Value.Trim();

            //mEntity.UnitCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            mEntity.UnitModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            return KPI_UnitDal.Update(mEntity);
        }
       
        #endregion


        #region 【按钮事件、调用方法】   
   
        protected bool TagCheck(out string  msg)
        {
            msg = "";

            txt_UnitMWTag.Value = txt_UnitMWTag.Value.Trim();

            if (!DBAccess.GetRealTime().Connection())
            {
                msg += "实时库连接错误！";

                return true;
            }

            ////////////////////////////////////////////////////////////////////////////////////
            if (txt_UnitMWTag.Value.Equals(""))
            {
                msg +=  "负荷标签点不能为空！";

                return true;
            }
            else
            {
                if (KPI_UnitDal.MWTagExists(txt_UnitMWTag.Value.Trim(), ViewState["unitid"].ToString()))
                {
                    msg += "负荷标签点已配置，请检查！";
                    return true;
                }
                else if (!DBAccess.GetRealTime().ExistPoint(txt_UnitMWTag.Value))
                {
                    msg +=  "负荷标签点不存在，请检查！";

                    return true;
                }

            }
            
            return false;

        }

        /// <summary>
        /// 审核标签点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTagCheck_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (TagCheck(out msg))
            {
                MessageBox.popupClientMessage(this.Page, msg, "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "该标签点可以使用！", "call();");
            }
            
        }
  
        /// <summary>
        /// 数据提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApply_Click(object sender, EventArgs e)
        {
            //清除缓存
            //DBAccess.GetRealTime().ReConnect();

            //if (!DBAccess.GetRealTime().Connection())
            //{
            //    MessageBox.popupClientMessage(this.Page, "实时库连接错误!", "call();");
            //    return;
            //}

            if (CheckVal(out msg))
            {
                MessageBox.popupClientMessage(this.Page, msg, "call();");
                return;
            }

            if (ViewState["unitid"].ToString() == "")
            {
                if (Insert())
                {
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

       /// <summary>
       /// 取消按钮
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       protected void btnCancel_Click(object sender, EventArgs e)
        {
            string strjs = "<script language=javascript>  window.close();</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", strjs);
       }
 
        #endregion


    }
}
