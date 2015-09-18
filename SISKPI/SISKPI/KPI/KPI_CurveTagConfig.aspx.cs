using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

using SIS.DataAccess;
using SIS.DataEntity;
using SIS.Assistant;
using SIS.Loger;
using SIS.Assistant.WS;
using SIS.DBControl;

namespace SISKPI
{
    public partial class KPI_CurveTagConfig : System.Web.UI.Page
    {
        private static string CurveID = "";
        private static CurveTagEntity mEntity = null;
        private static DataTable dtXLine = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility='';progress_update();");
                            
                //数据信息
                DataTable dt = KPI_RealTagDal.GetRealXYZLists();
                ddl_CurveXRealTag.Items.Add(new ListItem("默认机组负荷", "NULLDATA"));
                ddl_CurveYRealTag.Items.Add(new ListItem("默认机组负荷", "NULLDATA"));

                foreach (DataRow dr in dt.Rows)
                {
                    ddl_CurveXRealTag.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                    ddl_CurveYRealTag.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                BindValues();

                //tbxXLineXYZ.Text = "0.00";

                //gvXLine.ShowHeader = false;

                //BindXLine(true); 
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindValues()
        {
            DataTable dt = CurveTagDal.GetTagLists("");
            gvCurve.DataSource = dt;
            gvCurve.DataBind();

        }
        
        void BindXLine(bool binit)
        {
            if (CurveID == "")
            {
                return;
            }
            
            int xlinetype = int.Parse(ddl_CurveType.SelectedValue);
            int xlinegettype = int.Parse(ddl_CurveGetType.SelectedValue);
            double dout = 0;
            //bool bResult = true;

            if (binit)
            {
                mEntity = CurveTagDal.GetEntity(CurveID);
                
                lblInfor.Text = "当前曲线为： " + mEntity.CurveCode + mEntity.CurveName; 

                xlinetype = mEntity.CurveType;
                xlinegettype = mEntity.CurveGetType;
                dtXLine = new DataTable();

                ddl_CurveType.SelectedValue = xlinetype.ToString();
                ddl_CurveGetType.SelectedValue = xlinegettype.ToString();

                ECTagDal.GetXLineXYZ(xlinetype, mEntity.CurveXYZ, out dtXLine, out dout);
            }

            //if (binit)
            //{

            //}
            
            if (xlinetype == 0)
            {
                lblGetType.Visible = false;
                ddl_CurveGetType.Visible = false;

                spanX.Visible = false;
                spanXX.Visible = false;
                spanY.Visible = false;
                spanYY.Visible = false;
                btnAddX.Visible = false;
                btnAddY.Visible = false;

                //
                tbxXLineXYZ.Visible = true;
                tbxXLineXYZ.Text = dout.ToString("0.000");

                gvXLine.Visible = false;
                
                gvXLine.DataSource = null;
                gvXLine.DataBind();
            }
            else if (xlinetype == 1)
            {
                lblGetType.Visible = true;
                ddl_CurveGetType.Visible = true;

                spanX.Visible = true;
                spanXX.Visible = true;
                spanY.Visible = false;
                spanYY.Visible = false;
                btnAddX.Visible = true;
                btnAddY.Visible = true;

                ddl_CurveXRealTag.SelectedValue = mEntity.CurveXRealTag != "" ? mEntity.CurveXRealTag : "NULLDATA";
                //ddl_CurveYRealTag.SelectedValue = mEntity.CurveYRealTag != "" ? mEntity.CurveYRealTag : "NULLDATA";

                tbxXLineXYZ.Visible = false;
                //tbxXLineXYZ.Text = dout.ToString("0.000");

                gvXLine.Visible = true;

                gvXLine.DataSource = dtXLine;
                gvXLine.DataBind();
            }
            else if (xlinetype == 2)
            {
                lblGetType.Visible = true;
                ddl_CurveGetType.Visible = true;

                spanX.Visible = true;
                spanXX.Visible = true;
                spanY.Visible = true;
                spanYY.Visible = true;
                btnAddX.Visible = true;
                btnAddY.Visible = true;

                ddl_CurveXRealTag.SelectedValue = mEntity.CurveXRealTag != "" ? mEntity.CurveXRealTag : "NULLDATA";
                ddl_CurveYRealTag.SelectedValue = mEntity.CurveYRealTag != "" ? mEntity.CurveYRealTag : "NULLDATA";

                tbxXLineXYZ.Visible = false;
                //tbxXLineXYZ.Text = dout.ToString("0.000");

                gvXLine.Visible = true;

                gvXLine.DataSource = dtXLine;
                gvXLine.DataBind();
            }

        }
        

        #region 曲线信息列表

        protected void gvCurve_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                DataRowView drv = (DataRowView)e.Row.DataItem;
                
                if ((DropDownList)e.Row.FindControl("ddlValid") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlValid");

                    ddlCollege.SelectedValue = drv["CurveIsValid"].ToString();
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvCurve_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string[] keyvalue = e.CommandArgument.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
             //string keyid = "";
            //string keytype = "";
            //if (keyvalue.Length == 2)
            //{
            //    keyid = keyvalue[0];
            //    keytype = keyvalue[1];
            //}          
            
            string curveid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {

                if (CurveTagDal.DeleteTag(curveid))
                {
                    BindValues();

                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");
                }

                //MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");

            }
            else if (e.CommandName == "dataCopy")
            {
                //CurveID = curveid;
                if (CurveTagDal.CopyCurveTag(curveid))
                {
                    MessageBox.popupClientMessage(this.Page, "复制成功，请修改相关配置！", "call();");
                    
                    BindXLine(true);
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "复制错误, xxx_copy的名称已存在！", "call();");
                }

            }
            else if (e.CommandName == "dataConfig")
            {
                CurveID = curveid;
                
                BindXLine(true);
            }
            
        }


        protected void gvCurve_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCurve.EditIndex = e.NewEditIndex;

            BindValues();
        }

        protected void gvCurve_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCurve.EditIndex = -1;

            BindValues();
        }

        protected void gvCurve_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            HtmlInputHidden key = (HtmlInputHidden)gvCurve.Rows[e.RowIndex].Cells[0].FindControl("curveid");

            string sID = key.Value;
            string sCode = ((TextBox)(gvCurve.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
            string sName = ((TextBox)(gvCurve.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            string sDesc = ((TextBox)(gvCurve.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            string sGroup = ((TextBox)(gvCurve.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
            string sMonth = ((TextBox)(gvCurve.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();
            string sValid = ((DropDownList)(gvCurve.Rows[e.RowIndex].Cells[6].FindControl("ddlValid"))).SelectedValue;

            string msg = "";
            if (sCode == "")
            {
                msg += "代码不能为空！\r\n";
            }

            if (sName == "")
            {
                msg += "名称不能为空！\r\n";
            }

            if (msg != "")
            {
                MessageBox.popupClientMessage(this.Page, msg);
                return;
            }

            //代码是否重复
            if (CurveTagDal.CodeExist(sCode, sID) || ALLDal.CodeExist(sCode, sID))
            {
                MessageBox.popupClientMessage(this.Page, "已存在相同的代码!");
                return;
            }

            //更新
            CurveTagEntity ote = new CurveTagEntity();
            ote.CurveID = sID;
            ote.CurveCode = sCode;
            ote.CurveName = sName;
            ote.CurveDesc = sDesc;
            ote.CurveGroup = sGroup;
            ote.CurveMonth = sMonth;
            ote.CurveIndex = 100;
            ote.CurveIsValid = int.Parse(sValid);
          
            ote.CurveModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (CurveTagDal.Update(ote))
            {
                MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");

            }

            gvCurve.EditIndex = -1;

            BindValues();

        }             

        protected void btnAddCurve_Click(object sender, EventArgs e)
        {
            int index = CurveTagDal.CurveIDCounts();

            string sID = PageControl.GetGuid();

            CurveTagEntity ote = new CurveTagEntity();
            ote.CurveID = sID;
            ote.CurveCode = "InputCode";
            ote.CurveName = "InputName";
            ote.CurveDesc = "";
            ote.CurveGroup = "InputGroup";
            ote.CurveMonth = "1,2,3,4,5,6,7,8,9,10,11,12,";
            ote.CurveIsValid = 0;
            ote.CurveIndex = 100;
            ote.CurveType = 0;
            ote.CurveGetType = 0;
            ote.CurveXRealTag = "";
            ote.CurveYRealTag = "";
            ote.CurveZRealTag = "";
            ote.CurveXYZ = "";
            ote.CurveNote = "";

            ote.CurveCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            ote.CurveModifyTime = ote.CurveCreateTime;

            if (CurveTagDal.Insert(ote))
            {
                //MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");

                gvCurve.EditIndex = index;

                BindValues();
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");

            }
            
        }
        
        #endregion

        #region 曲线配置

        protected void gvXLine_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                //DataRowView drv = (DataRowView)e.Row.DataItem;

                //if ((DropDownList)e.Row.FindControl("ddlValid") != null)
                //{
                //    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlValid");

                //    ddlCollege.SelectedValue = drv["ShiftIsValid"].ToString();
                //}

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvXLine_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string[] keyvalue = e.CommandArgument.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            //string keyid = "";
            //string keytype = "";
            //if (keyvalue.Length == 2)
            //{
            //    keyid = keyvalue[0];
            //    keytype = keyvalue[1];
            //}
            
            if (e.CommandName == "dataDelete")
            {
                if (e.CommandArgument.ToString().Trim() == "")
                {
                    return;
                }

                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //需要按顺序删除
                if (rowIndex != (dtXLine.Rows.Count - 1))
                {
                    MessageBox.popupClientMessage(this.Page, "请按倒序顺序删除", "call();");
                    return;
                }
                else
                {
                    dtXLine.Rows[rowIndex].Delete();
                    
                    dtXLine.AcceptChanges();

                    BindXLine(false);
                }

                //MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");
                //MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");

            }

        }

        protected void gvXLine_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvXLine.EditIndex = -1;

            BindXLine(false);

        }

        protected void gvXLine_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvXLine.EditIndex = e.NewEditIndex;

            BindXLine(false);

        }

        protected void gvXLine_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = e.RowIndex;
            for (int i = 2; i < (2+dtXLine.Columns.Count); i++)
            {
                dtXLine.Rows[index][i - 2] = ((TextBox)(gvXLine.Rows[e.RowIndex].Cells[i].Controls[0])).Text.ToString().Trim();
            }
            
            //if (KPI_XLineDal.Update(ote))
            //{
            //    MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

            //}
            //else
            //{
            //    MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
            //}

            gvXLine.EditIndex = -1;

            BindXLine(false);         

        }

        protected void ddl_CurveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int xlinetype = int.Parse(ddl_CurveType.SelectedValue);

            if (xlinetype == 0)
            {
                spanX.Visible = false;
                spanXX.Visible = false;
                spanY.Visible = false;
                spanYY.Visible = false;
                btnAddX.Visible = false;
                btnAddY.Visible = false;

                //
                tbxXLineXYZ.Visible = true;
                //tbxXLineXYZ.Text = dout.ToString("0.000");

                gvXLine.Visible = false;

                //初始化表格
                dtXLine = null;

            }
            else if (xlinetype == 1)
            {
                spanX.Visible = true;
                spanXX.Visible = true;
                spanY.Visible = false;
                spanYY.Visible = false;
                btnAddX.Visible = true;
                btnAddY.Visible = true;

                //ddl_CurveXRealTag.SelectedValue = mEntity.CurveXRealTag;

                tbxXLineXYZ.Visible = false;
                //tbxXLineXYZ.Text = dout.ToString("0.000");

                gvXLine.Visible = true;

                //初始化表格
                dtXLine = ECTagDal.GetInitXYZ(xlinetype);

                //gvXLine.DataSource = dtXLine;
                //gvXLine.DataBind();
            }
            else if (xlinetype == 2)
            {
                spanX.Visible = true;
                spanXX.Visible = true;
                spanY.Visible = true;
                spanYY.Visible = true;
                btnAddX.Visible = true;
                btnAddY.Visible = true;

                //ddl_CurveXRealTag.SelectedValue = mEntity.CurveXRealTag;
                //ddl_CurveYRealTag.SelectedValue = mEntity.CurveYRealTag;

                tbxXLineXYZ.Visible = false;
                //tbxXLineXYZ.Text = dout.ToString("0.000");

                gvXLine.Visible = true;

                dtXLine = ECTagDal.GetInitXYZ(xlinetype);

               // gvXLine.DataSource = dtXLine;
               // gvXLine.DataBind();
            }


            BindXLine(false);
        }
        
        protected void btnXLineDel_Click(object sender, EventArgs e)
        {
            int xlinetype = int.Parse(ddl_CurveType.SelectedValue);
            tbxXLineXYZ.Text = "0.00";

            dtXLine = ECTagDal.GetInitXYZ(xlinetype);

            BindXLine(false);
        }

        /// <summary>
        /// 增加列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddX_Click(object sender, EventArgs e)
        {
            int xlinetype = int.Parse(ddl_CurveType.SelectedValue);

            if (dtXLine.Columns.Count >= 16)
            {
                MessageBox.popupClientMessage(this.Page, "区间基准值不能超过15个！", "call();");

                return;
            }

            if (xlinetype == 1)
            {
                dtXLine.Columns.Add((dtXLine.Columns.Count-1).ToString());

                dtXLine.AcceptChanges();

                BindXLine(false);
            }
            else if (xlinetype == 2)
            {
                //DataRow dr = dtXLine.NewRow();
                //for (int i = 1; i < dtXLine.Columns.Count; i++)
                //{
                //    dr[i] = "0.00";
                //}

                //dtXLine.Rows.Add(dr);


                dtXLine.Columns.Add((dtXLine.Columns.Count + 1).ToString());

                dtXLine.AcceptChanges();

                BindXLine(false);
            }
            
        }

        /// <summary>
        /// 增加行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddY_Click(object sender, EventArgs e)
        {
            int xlinetype = int.Parse(ddl_CurveType.SelectedValue);

            if (xlinetype == 1)
            {
                if (dtXLine.Rows.Count >= 9 )
                {
                    MessageBox.popupClientMessage(this.Page, "区间系数不能超过8个！", "call();");

                    return;
                }

                DataRow dr = dtXLine.NewRow();
                dr[0] = "a"+dtXLine.Rows.Count.ToString();
                for (int i = 1; i < dtXLine.Columns.Count; i++)
                {
                    dr[i] = "0.00";
                }

                dtXLine.Rows.Add(dr);

                dtXLine.AcceptChanges();

                
                BindXLine(false);

            }else if (xlinetype == 2)
            {
                if (dtXLine.Rows.Count >= 16)
                {
                    MessageBox.popupClientMessage(this.Page, "Y轴向数量不能超过15个！", "call();");

                    return;
                }

                DataRow dr = dtXLine.NewRow();
                for (int i = 1; i < dtXLine.Columns.Count; i++)
                {
                    dr[i] = "0.00";
                }

                dtXLine.Rows.Add(dr);


                dtXLine.AcceptChanges();

                BindXLine(false);
            }
        }

        protected void btnXLineSave_Click(object sender, EventArgs e)
        {
            if (CurveID == "")
            {
                MessageBox.popupClientMessage(this.Page, "请选择具体曲线！", "call();");
                
                return;
            }

            int xlinetype = int.Parse(ddl_CurveType.SelectedValue);
            int xlinegetype = int.Parse(ddl_CurveGetType.SelectedValue);
            double dout = 0;
            string xlinexyz = "";

            if (xlinetype == 0)
            {
                if(!double.TryParse(tbxXLineXYZ.Text, out dout))
                {
                    MessageBox.popupClientMessage(this.Page, "定值格式不正确！无法保存！", "call();");
                    return;
                }

            }else if(xlinetype==1)
            {
                if(dtXLine.Rows.Count<2)
                {
                    MessageBox.popupClientMessage(this.Page, "区间定义不完整！无法保存！", "call();");
                    return;
                }

            }
            else if (xlinetype == 2)
            {
                if (dtXLine.Rows.Count < 2 && dtXLine.Columns.Count < 2)
                {
                    MessageBox.popupClientMessage(this.Page, "区间定义不完整！无法保存！", "call();");
                    return;
                }

            }

            CurveTagEntity mEntity = CurveTagDal.GetEntity(CurveID);

            mEntity.CurveType = xlinetype;
            mEntity.CurveGetType = xlinegetype;
            mEntity.CurveXRealTag = ddl_CurveXRealTag.SelectedValue != "NULLDATA" ? ddl_CurveXRealTag.SelectedValue : "";
            mEntity.CurveYRealTag = ddl_CurveYRealTag.SelectedValue != "NULLDATA" ? ddl_CurveYRealTag.SelectedValue : "";
            mEntity.CurveZRealTag = "";

            xlinexyz = ECTagDal.SetXLineXYZ(xlinetype, dtXLine, dout);

            mEntity.CurveXYZ = xlinexyz;

            CurveTagDal.Update(mEntity);
        }
        

        #endregion  
    }
}
