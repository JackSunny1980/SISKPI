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
    public partial class KPI_SubECTagConfig3 : System.Web.UI.Page
    {
        private static ECTagEntity mEntity = null;
        private static DataTable dtXLine = null;
        private static DataTable dtScore = null;
        private static DataTable dtCurve = null;
                
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility='';progress_update();");
                            
                //数据信息
                DataTable dt = KPI_RealTagDal.GetRealXYZLists();
                ddl_ECXLineXRealTag.Items.Add(new ListItem("默认机组负荷", "NULLDATA"));
                ddl_ECXLineYRealTag.Items.Add(new ListItem("默认机组负荷", "NULLDATA"));

                foreach (DataRow dr in dt.Rows)
                {
                    ddl_ECXLineXRealTag.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                    ddl_ECXLineYRealTag.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //初始化列表
                dtCurve = CurveTagDal.GetCurvesByGroup();
                foreach (DataRow dr in dtCurve.Rows)
                {
                    rblCurveTags.Items.Add(new ListItem(dr["Name"].ToString(), dr["Group"].ToString()));
                }

                //初始化
                dtScore = ECTagDal.GetInitScore();

                //判断是否新建或编辑
                if (Request.QueryString["ecid"] != null )
                {
                    ViewState["ecid"] = Request.QueryString["ecid"].ToString();

                    btnAddScore.Enabled = true;

                    BindValues();        
                }
                else
                {
                    //添加
                    ViewState["ecid"] = "";

                    btnAddScore.Enabled = false;
                }
                
                tbxXLineXYZ.Text = "0.00";

                gvXLine.ShowHeader = false;

                BindXLine(true);   
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
                btnAddScore.Enabled = false;

                return;
            }

            mEntity = ECTagDal.GetEntity(ECID);

            if (mEntity == null)
            {
                return;
            }
            
            lbl_ECCode.Text = "指标代码：" + mEntity.ECCode;
            lbl_ECName.Text = "指标名称：" + mEntity.ECName;

            cbx_ECIsSnapshot.Checked = mEntity.ECIsSnapshot == 1 ? true : false;

            BindScore(true);
        }
        
        void BindXLine(bool binit)
        {
            if (mEntity == null)
            {
                return;
            }

            int xlinetype = int.Parse(ddl_ECXLineType.SelectedValue);
            int xlinegettype = int.Parse(ddl_ECXLineGetType.SelectedValue);
            double dout = 0;
            //bool bResult = true;
            string curvegroup = "";

            if (binit)
            {
                curvegroup = mEntity.ECCurveGroup;

                xlinetype = mEntity.ECXLineType;
                xlinegettype = mEntity.ECXLineGetType;
                dtXLine = new DataTable();

                ddl_ECXLineType.SelectedValue = xlinetype.ToString();
                ddl_ECXLineGetType.SelectedValue = xlinegettype.ToString();

                ECTagDal.GetXLineXYZ(xlinetype, mEntity.ECXLineXYZ, out dtXLine, out dout);

            }
            
            if (xlinetype == 0)
            {
                lblGetType.Visible = false;
                ddl_ECXLineGetType.Visible = false;

                spanX.Visible = false;
                spanXX.Visible = false;
                spanY.Visible = false;
                spanYY.Visible = false;
                btnAddX.Visible = false;
                btnAddY.Visible = false;

                rblCurveTags.Visible = false;
                rblCurveTags.Items.Clear();

                //
                tbxXLineXYZ.Visible = true;
                tbxXLineXYZ.Text = dout.ToString("0.000");

                gvXLine.Visible = false;
                
                gvXLine.DataSource = null;
                gvXLine.DataBind();

                tdcurve.Visible = false;
                tdxline.Visible = true;
            }
            else if (xlinetype == 1)
            {
                lblGetType.Visible = true;
                ddl_ECXLineGetType.Visible = true;

                spanX.Visible = true;
                spanXX.Visible = true;
                spanY.Visible = false;
                spanYY.Visible = false;
                btnAddX.Visible = true;
                btnAddY.Visible = true;

                rblCurveTags.Visible = false;
                rblCurveTags.Items.Clear();

                ddl_ECXLineXRealTag.SelectedValue = mEntity.ECXLineXRealTag != "" ? mEntity.ECXLineXRealTag : "NULLDATA";
                //ddl_ECXLineYRealTag.SelectedValue = mEntity.ECXLineYRealTag != "" ? mEntity.ECXLineYRealTag : "NULLDATA";

                tbxXLineXYZ.Visible = false;
                //tbxXLineXYZ.Text = dout.ToString("0.000");

                gvXLine.Visible = true;

                gvXLine.DataSource = dtXLine;
                gvXLine.DataBind();

                tdcurve.Visible = false;
                tdxline.Visible = true;
            }
            else if (xlinetype == 2)
            {
                lblGetType.Visible = true;
                ddl_ECXLineGetType.Visible = true;

                spanX.Visible = true;
                spanXX.Visible = true;
                spanY.Visible = true;
                spanYY.Visible = true;
                btnAddX.Visible = true;
                btnAddY.Visible = true;

                rblCurveTags.Visible = false;
                rblCurveTags.Items.Clear();

                ddl_ECXLineXRealTag.SelectedValue = mEntity.ECXLineXRealTag != "" ? mEntity.ECXLineXRealTag : "NULLDATA";
                ddl_ECXLineYRealTag.SelectedValue = mEntity.ECXLineYRealTag != "" ? mEntity.ECXLineYRealTag : "NULLDATA";

                tbxXLineXYZ.Visible = false;
                //tbxXLineXYZ.Text = dout.ToString("0.000");

                gvXLine.Visible = true;

                gvXLine.DataSource = dtXLine;
                gvXLine.DataBind();

                tdcurve.Visible = false;
                tdxline.Visible = true;
            }
            else if (xlinetype == 3)
            {
            }
            else if (xlinetype == 4)
            {
                lblGetType.Visible = true;
                ddl_ECXLineGetType.Visible = true;

                spanX.Visible = false;
                spanXX.Visible = false;
                spanY.Visible = false;
                spanYY.Visible = false;
                btnAddX.Visible = false;
                btnAddY.Visible = false;

                rblCurveTags.Items.Clear();
                rblCurveTags.Visible = true;
                foreach (DataRow dr in dtCurve.Rows)
                {
                    rblCurveTags.Items.Add(new ListItem(dr["Name"].ToString(), dr["Group"].ToString()));
                }
                foreach (ListItem ltm in rblCurveTags.Items)
                {
                    if (curvegroup.Contains(ltm.Value))
                    {
                        ltm.Selected = true;
                        break;
                    }
                }
              
                tbxXLineXYZ.Visible = false;
                //tbxXLineXYZ.Text = dout.ToString("0.000");
                
                gvXLine.Visible = false;
                gvXLine.DataSource = null;
                gvXLine.DataBind();


                tdcurve.Visible = true;
                tdxline.Visible = false;

            }

        }

        void BindScore(bool binit)
        {
            if (mEntity == null)
            {
                return;
            }

            if (binit)
            {
                if (mEntity.ECScoreExp != null && mEntity.ECScoreExp.Trim() != "")
                {
                    dtScore = ECTagDal.GetScoreExp(mEntity.ECScoreExp.ToString());
                }
            }

            gvScore.DataSource = dtScore;
            
            gvScore.DataBind();
        }
        
        protected void cbx_ECIsSnapshot_CheckedChanged(object sender, EventArgs e)
        {
            string ecid = ViewState["ecid"].ToString();

            if (ecid == "")
            {
                btnAddScore.Enabled = false;

                return;
            }
            else
            {
                btnAddScore.Enabled = true;
            }

            ECTagEntity ect = new ECTagEntity();
            ect.ECID = ecid;
            ect.ECIsSnapshot = cbx_ECIsSnapshot.Checked ? 1 : 0;

            ECTagDal.Update(ect);

        }

        #region 区间信息配置

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

                    txtFlag.Value = "0";
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
            
            txtFlag.Value = "0";

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

        protected void ddl_ECXLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int xlinetype = int.Parse(ddl_ECXLineType.SelectedValue);

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

                //ddl_ECXLineXRealTag.SelectedValue = mEntity.ECXLineXRealTag;

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

                //ddl_ECXLineXRealTag.SelectedValue = mEntity.ECXLineXRealTag;
                //ddl_ECXLineYRealTag.SelectedValue = mEntity.ECXLineYRealTag;

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
            int xlinetype = int.Parse(ddl_ECXLineType.SelectedValue);
            tbxXLineXYZ.Text = "0.00";

            dtXLine = ECTagDal.GetInitXYZ(xlinetype);

            BindXLine(false);

            txtFlag.Value = "0";
        }

        /// <summary>
        /// 增加列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddX_Click(object sender, EventArgs e)
        {
            int xlinetype = int.Parse(ddl_ECXLineType.SelectedValue);

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
            
            txtFlag.Value = "0";
        }

        /// <summary>
        /// 增加行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddY_Click(object sender, EventArgs e)
        {
            int xlinetype = int.Parse(ddl_ECXLineType.SelectedValue);

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


            txtFlag.Value = "0";
        }

        protected void btnXLineSave_Click(object sender, EventArgs e)
        {
            int xlinetype = int.Parse(ddl_ECXLineType.SelectedValue);
            int xlinegetype = int.Parse(ddl_ECXLineGetType.SelectedValue);
            double dout = 0;

            string xtag = ddl_ECXLineXRealTag.SelectedValue != "NULLDATA" ? ddl_ECXLineXRealTag.SelectedValue : "";
            string ytag = ddl_ECXLineYRealTag.SelectedValue != "NULLDATA" ? ddl_ECXLineYRealTag.SelectedValue : "";
            string ztag = "";

            string xlinexyz = "";
            string curvegroup = "";


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
                
                xlinexyz = ECTagDal.SetXLineXYZ(xlinetype, dtXLine, dout);

            }
            else if (xlinetype == 2)
            {
                if (dtXLine.Rows.Count < 2 && dtXLine.Columns.Count < 2)
                {
                    MessageBox.popupClientMessage(this.Page, "区间定义不完整！无法保存！", "call();");
                    return;
                }

                xlinexyz = ECTagDal.SetXLineXYZ(xlinetype, dtXLine, dout);

            }
            // ==3 
            else if (xlinetype == 4)
            {
                //
                foreach (ListItem ltm in rblCurveTags.Items)
                {
                    if (ltm.Selected)
                    {
                        curvegroup = ltm.Value;
                        break;
                    }
                }

                DataTable dtc = CurveTagDal.GetCurvesByGroup(curvegroup);
                if (dtc.Rows.Count >= 1)
                {
                    xtag = dtc.Rows[0]["CurveXRealTag"].ToString();
                    ytag = dtc.Rows[0]["CurveYRealTag"].ToString();
                    ztag = "";

                    //借用此字段 存储曲线Group中的CurveType 类型
                    xlinexyz = dtc.Rows[0]["CurveType"].ToString();
                }
            }

            mEntity.ECXLineXRealTag = xtag;
            mEntity.ECXLineYRealTag = ytag;
            mEntity.ECXLineZRealTag = ztag;

            mEntity.ECXLineXYZ = xlinexyz;
            mEntity.ECCurveGroup = curvegroup;

            ECTagDal.Update(mEntity);

            txtFlag.Value = "0";
        }
        

        #endregion

        #region 得分区间表配置

        protected void gvScore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                DataRowView drv = (DataRowView)e.Row.DataItem;

                if ((DropDownList)e.Row.FindControl("ddlOptimal") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlOptimal");

                    ddlCollege.SelectedValue = drv["ScoreOptimal"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlAlarm") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlAlarm");

                    ddlCollege.SelectedValue = drv["ScoreAlarm"].ToString();
                }
                
                if ((DropDownList)e.Row.FindControl("ddlValid") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlValid");

                    ddlCollege.SelectedValue = drv["ScoreIsValid"].ToString();
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvScore_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string[] keyvalue = e.CommandArgument.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
             //string keyid = "";
            //string keytype = "";
            //if (keyvalue.Length == 2)
            //{
            //    keyid = keyvalue[0];
            //    keytype = keyvalue[1];
            //}          
            
            int scoreid = int.Parse(e.CommandArgument.ToString());
            if(scoreid<0 && scoreid>dtScore.Rows.Count)
            {
                return;
            }

            if (e.CommandName == "dataDelete")
            {
                dtScore.Rows[scoreid].Delete();

                //BindXLine();

                gvScore.DataSource = dtScore;
                gvScore.DataBind();

                MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");
                //MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");

            }
            else if (e.CommandName == "dataUpdate")
            {
                //
                btnAddScore.Text = "保存区间";

                DataRow dr = dtScore.Rows[scoreid];

                tbx_ScoreCalcExp.Text = dr["ScoreCalcExp"].ToString();
                tbx_ScoreGainExp.Text = dr["ScoreGainExp"].ToString();
                ddlScoreAlarm.Value = dr["ScoreAlarm"].ToString();
                ddlScoreOptimal.Value = dr["ScoreOptimal"].ToString();
                ddlScoreIsValid.Value = dr["ScoreIsValid"].ToString();
            }


            txtFlag.Value = "1";

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddScore_Click(object sender, EventArgs e)
        {
            if (mEntity == null)
            {
                MessageBox.popupClientMessage(this.Page, "使用系数前,请先绑定指标实体！", "call();");
                return;
            }

            string ScoreCalcExp = tbx_ScoreCalcExp.Text.Trim();
            string ScoreGainExp = tbx_ScoreGainExp.Text.Trim();

            //判断是否有系数
            //if ((ScoreCalcExp.ToLower().IndexOf("@a1") > -1
            //    || ScoreCalcExp.ToLower().IndexOf("@a2") > -1
            //    || ScoreCalcExp.ToLower().IndexOf("@a3") > -1
            //    || ScoreCalcExp.ToLower().IndexOf("@a4") > -1
            //    || ScoreGainExp.ToLower().IndexOf("@a1") > -1
            //    || ScoreGainExp.ToLower().IndexOf("@a2") > -1
            //    || ScoreGainExp.ToLower().IndexOf("@a3") > -1
            //    || ScoreGainExp.ToLower().IndexOf("@a4") > -1) )
            //{
            //    MessageBox.popupClientMessage(this.Page, "使用系数前,请先绑定区间信息！", "call();");
            //    return;
            //}

            int scoretype = mEntity.ECXLineType;

            //判断类型引用
            if ((ScoreCalcExp.ToLower().IndexOf("@a2") > -1
                || ScoreCalcExp.ToLower().IndexOf("@a3") > -1
                || ScoreCalcExp.ToLower().IndexOf("@a4") > -1
                || ScoreCalcExp.ToLower().IndexOf("@a5") > -1
                || ScoreGainExp.ToLower().IndexOf("@a2") > -1
                || ScoreGainExp.ToLower().IndexOf("@a3") > -1
                || ScoreGainExp.ToLower().IndexOf("@a4") > -1
                || ScoreGainExp.ToLower().IndexOf("@a5") > -1)
                )
                //&& (scoretype != 1 ||scoretype != 4))
            {
                //MessageBox.popupClientMessage(this.Page, "提示: 区间类型为一维或曲线指标时可以使用@2~@a8!", "call();");

                //return;
            }

            //Check ScoreExp 是否重复
            foreach (DataRow dr in dtScore.Rows)
            {
                if (dr["ScoreCalcExp"].ToString().ToLower() == ScoreCalcExp.ToLower())
                {
                    MessageBox.popupClientMessage(this.Page, "该得分方法已存在！", "call();");
                    return;
                }
            }

            //插入
            DataRow drc = dtScore.NewRow();
            drc[0] = dtScore.Rows.Count.ToString();
            drc[1] = ScoreCalcExp;
            drc[2] = ScoreGainExp;
            drc[3] = int.Parse(ddlScoreOptimal.Value);
            drc[4] = int.Parse(ddlScoreAlarm.Value);
            drc[5] = int.Parse(ddlScoreIsValid.Value);

            dtScore.Rows.Add(drc);

            gvScore.DataSource = dtScore;
            gvScore.DataBind();

            MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");
            
        }

        protected void btnScoreSave_Click(object sender, EventArgs e)
        {
            if (mEntity == null)
            {
                MessageBox.popupClientMessage(this.Page, "使用系数前,请先绑定指标实体！", "call();");
                return;
            }

            string scoreexp = "";

            foreach (DataRow dr in dtScore.Rows)
            {
                string scoreexpindex = dr["ScoreCalcExp"].ToString();
                scoreexpindex += ",";
                scoreexpindex += dr["ScoreGainExp"].ToString();
                scoreexpindex += ",";
                scoreexpindex += dr["ScoreOptimal"].ToString();
                scoreexpindex += ",";
                scoreexpindex += dr["ScoreAlarm"].ToString();
                scoreexpindex += ",";
                scoreexpindex += dr["ScoreIsValid"].ToString();
                scoreexpindex += ";";

                scoreexp += scoreexpindex;
            }

            mEntity.ECScoreExp = scoreexp;

            ECTagDal.Update(mEntity);

            txtFlag.Value = "0";
        }


        #endregion

        #region 【按钮事件】

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
           //当前页不做什么事情。

       }

       protected void btnStep4_Click(object sender, EventArgs e)
       {
           Response.Redirect("KPI_SubECTagConfig4.aspx?ecid=" + ViewState["ecid"].ToString());

       }

       protected void btnReturn_Click(object sender, EventArgs e)
       {
           Response.Redirect("KPI_ECTagConfig.aspx");
       }

      #endregion      

    }
}
