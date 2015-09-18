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
using SIS.DataEntity;
using SIS.Assistant;
using SIS.Loger;
using SIS.Assistant.WS;
using SIS.DBControl;

namespace SISKPI
{
    public partial class KPI_SubPlantConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility='';progress_update();");
             
                btnCancel.Attributes.Add("onclick", "javascript:return confirm('确认关闭吗？');");

                ddl_PlantIsValid.Value = "1";

                //添加或编辑电厂信息；
                //
                if (Request.QueryString["plantid"] != null)
                {
                    //编辑
                    ViewState["plantid"] = Request.QueryString["plantid"].ToString();

                    BindValues();                 

                }
                else
                {
                    //添加
                    ViewState["plantid"] = "";  
                }
                
            }
        }

        void BindValues()
        {
            string PlantID = ViewState["plantid"].ToString();

            if (PlantID.Equals(""))
            {
                return;
            }

            KPI_PlantEntity KPIPE = KPI_PlantDal.GetEntity(PlantID);

            txt_PlantCode.Value = KPIPE.PlantCode;
            txt_PlantName.Value = KPIPE.PlantName;
            txt_PlantDesc.Value = KPIPE.PlantDesc;
            txt_PlantIndex.Value = KPIPE.PlantIndex.ToString();
            ddl_PlantIsValid.Value = KPIPE.PlantIsValid.ToString();
            txt_PlantAddress.Value = KPIPE.PlantAddress;
            txt_PlantNote.Value = KPIPE.PlantNote;

            return;
        }


        #region 【参数部分验证】

        /// <summary>
        /// 页面信息检查
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CheckVal(out string msg)
        {
            bool flag=false;
            msg = "";

            txt_PlantCode.Value = txt_PlantCode.Value.Trim();
            txt_PlantName.Value = txt_PlantName.Value.Trim();
            txt_PlantIndex.Value = txt_PlantIndex.Value.Trim();


            if (txt_PlantCode.Value.Equals(""))
            {
                msg += "名称不能为空！\r\n";
                flag = true;
                return flag;
            }
            else
            {
                if (txt_PlantCode.Value.Trim() != "")
                {
                    //在SQL表中是否存在
                    if (KPI_PlantDal.PlantCodeExists(txt_PlantCode.Value.Trim(), ViewState["plantid"].ToString()))
                    {
                        msg += "代码已存在，请检查！\r\n";
                        return true;
                    }
                }
            }

            if (txt_PlantName.Value.Equals(""))
            {
                msg += "名称不能为空！\r\n";
                flag = true;
                return flag;
            }

            if (txt_PlantIndex.Value.Equals(""))
            {
                msg += "序号不能为空！\r\n";
                flag = true;
                return flag;
            }

            if (!Regex.IsMatch(txt_PlantIndex.Value, "^[0-9]+$"))
            {
                msg += "序号格式不这正确！\r\n";
            }

            if (ddl_PlantIsValid.Value == "")
            {
                msg += "有效性选择错误！\r\n";
                flag = true;
                return flag;
            }
            
            return flag;
        }               
        
        #endregion


        #region 【插入、编辑、绑定方法】

        string msg = "";

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <returns></returns>
        bool Insert()
        {
            string plantid = PageControl.GetGuid();

            KPI_PlantEntity mEntity = new KPI_PlantEntity();

            mEntity.PlantID = plantid;
            mEntity.PlantCode = txt_PlantCode.Value.Trim();
            mEntity.PlantName = txt_PlantName.Value.Trim();
            mEntity.PlantDesc = txt_PlantDesc.Value.Trim();
            mEntity.PlantIndex = int.Parse(txt_PlantIndex.Value.Trim());
            mEntity.PlantIsValid = int.Parse(ddl_PlantIsValid.Value);
            mEntity.PlantAddress = txt_PlantAddress.Value.Trim();
            mEntity.PlantNote = txt_PlantNote.Value.Trim();

            mEntity.PlantCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            mEntity.PlantModifyTime = mEntity.PlantCreateTime;

            return KPI_PlantDal.Insert(mEntity);
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <returns></returns>
        bool Update()
        {
            string plantid = ViewState["plantid"].ToString();

            KPI_PlantEntity mEntity = new KPI_PlantEntity();

            mEntity.PlantID = plantid;
            mEntity.PlantCode = txt_PlantCode.Value.Trim();
            mEntity.PlantName = txt_PlantName.Value.Trim();
            mEntity.PlantDesc = txt_PlantDesc.Value.Trim();
            mEntity.PlantIndex = int.Parse(txt_PlantIndex.Value.Trim());
            mEntity.PlantIsValid = int.Parse(ddl_PlantIsValid.Value);
            mEntity.PlantAddress = txt_PlantAddress.Value.Trim();
            mEntity.PlantNote = txt_PlantNote.Value.Trim();

            //mEntity.PlantCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            mEntity.PlantModifyTime = mEntity.PlantCreateTime;

            return KPI_PlantDal.Update(mEntity);
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
            if (CheckVal(out msg))
            {
                MessageBox.popupClientMessage(this.Page, msg, "call();");
                return;
            }

            if (ViewState["plantid"].ToString() == "")
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
