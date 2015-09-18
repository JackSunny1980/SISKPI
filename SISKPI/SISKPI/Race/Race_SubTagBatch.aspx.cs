using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

using System.IO;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

using SIS.Assistant;
using SIS.Loger;
using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
using SIS.Exceler;


namespace SISKPI
{
    public partial class Race_SubTagBatch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtPU;
                //PlantID
                //dtPU = OPM_PlantDal.GetPlants("");
                //foreach (DataRow dr in dtPU.Rows)
                //{
                //    ddlExcelPlantID.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                //}
                
                //UnitID
                dtPU = KPI_UnitDal.GetUnits("");
                //
                //ddlExcelUnitID.Items.Add(new ListItem("不指定", "NON"));
                foreach (DataRow dr in dtPU.Rows)
                {
                    ddlExcelUnitID.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }


                //string sql = "select UnitID, UnitName from OPM_Unit where 1=1 order by UnitID";

                //System.Data.DataTable dtPU = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

                //foreach (DataRow dr in dtPU.Rows)
                //{
                //    ddlExcelUnitID.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));

                //    ddlSynUnitID.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                    
                //    ddlDelUnitID.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                //}

                //Excel
                //btnExcelExport.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility=''; progress_update();");
                btnExcelImport.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility=''; progress_update();");
                                
                ddlExcelMode.Items.Add(new ListItem("创建", "0"));
                ddlExcelMode.Items.Add(new ListItem("创建并编辑", "1"));
                ddlExcelMode.Items.Add(new ListItem("编辑", "2"));
                ddlExcelMode.Items.Add(new ListItem("删除", "3"));

                ////SyconPI
                //btnSynPI.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility=''; progress_update();");
                
                ////Del
                ////btnDelApply.Attributes.Add("onclick ", "javascript:return confirm( '确定删除该机组的相关标签点吗?'); ");
                //btnDelApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility=''; progress_update();");

            }


        }

        #region Excel标签点配置

        //Excel
        public static void ExportToSpreadsheet(DataTable dtData, string name)
        {

            DocExport docEexport = new DocExport();

            docEexport.Dt = dtData; //GridViewHelper.GridView2DataTable(gvValue);

            docEexport.Export(name);

            //System.Web.UI.WebControls.DataGrid dgExport = null;
            //// 当前对话 
            //System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            //// IO用于导出并返回excel文件 
            //System.IO.StringWriter strWriter = null;
            //System.Web.UI.HtmlTextWriter htmlWriter = null;

            //if (dtData != null)
            //{
            //    // 设置编码和附件格式 
            //    curContext.Response.ContentType = "application/vnd.ms-excel";
            //    curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            //    curContext.Response.Charset = "GB2312";

            //    // 导出excel文件 
            //    strWriter = new System.IO.StringWriter();
            //    htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);

            //    // 为了解决dgData中可能进行了分页的情况，需要重新定义一个无分页的DataGrid 
            //    dgExport = new System.Web.UI.WebControls.DataGrid();
            //    dgExport.DataSource = dtData.DefaultView;
            //    dgExport.AllowPaging = false;
            //    dgExport.DataBind();

            //    // 返回客户端 
            //    dgExport.RenderControl(htmlWriter);
            //    curContext.Response.Write(strWriter.ToString());
            //    curContext.Response.End();
            //}

        }
        
        //导出
        protected void btnExcelExport_Click(object sender, EventArgs e)
        {
            //if (ddlExcelPlantID.Value == "")
            //{
            //    MessageBox.popupClientMessage(this.Page, "请选择需要导出的电厂！", "call();");

            //    return;
            //}

            if (ddlExcelUnitID.Value == "")
            {
                MessageBox.popupClientMessage(this.Page, "请选择需要导出的机组！", "call();");

                return;
            }

            string strExcelFile = "Race";
            //string strExcelFile = rblTagType.SelectedItem.Text;

            try
            {

                System.Data.DataTable dt = Race_TagDal.GetTagListForExcel(ddlExcelUnitID.Value);

                if (dt == null || dt.Rows.Count<=0)
                {
                    MessageBox.popupClientMessage(this.Page, "无信息可以导出！", "call();");

                    return;
                }

                ExportToSpreadsheet(dt, strExcelFile);    

            }
            catch (Exception ee)
            {
                MessageBox.popupClientMessage(this.Page, "导出信息错误！" + ee.Message, "call();");

                return;
            }
       
            return;
        }

        //导入
        protected void btnExcelImport_Click(object sender, EventArgs e)
        {
            //if (ddlExcelPlantID.Value == "")
            //{
            //    MessageBox.popupClientMessage(this.Page, "请选择需要导入的电厂！", "call();");

            //    return;
            //}

            if (ddlExcelUnitID.Value == "")
            {
                MessageBox.popupClientMessage(this.Page, "请选择需要导入的机组！", "call();");

                return;
            }

            if (ddlExcelMode.Value == "")
            {
                MessageBox.popupClientMessage(this.Page, "请选择Excel表配置方式！", "call();");
        
                return;
            }

            if (!fuExcel.HasFile)
            {
                MessageBox.popupClientMessage(this.Page, "请选择Excel文件！", "call();");

                return;
            }
            
            string IsXls = System.IO.Path.GetExtension(fuExcel.FileName).ToString().ToLower();
            if (IsXls != ".xls")
            {
                MessageBox.popupClientMessage(this.Page, "只允许选择Excel文件！", "call();");

                return;
            }

            
             try
            {
                //删除临时文件夹的文件
                System.IO.DirectoryInfo path = new System.IO.DirectoryInfo(Server.MapPath("~\\excel\\"));
                foreach (System.IO.FileInfo f in path.GetFiles())
                {
                    f.Delete();
                }  

                //获取Execle文件名  DateTime日期函数   
                string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + fuExcel.FileName;
                //Server.MapPath 获得虚拟服务器相对路径
                string savePath = Server.MapPath(("~\\excel\\") + filename);
                //SaveAs 将上传的文件内容保存在服务器上
                fuExcel.SaveAs(savePath);

                string sheetname = tbxSheet.Text;

                //switch (rblTagType.SelectedValue)
                //{
                //    case "HOUR":
                //        sheetname = "Race_Hour";
                //        break;
                //    case "SHIFT":
                //        sheetname = "Race_Shift";
                //        break;
                //    //case "DAY":
                //    //    sheetname = "Report_Day";
                //    //    break;
                //    case "MONTH":
                //        sheetname = "Race_Month";
                //        break;
                //    //case "YEAR":
                //    //    sheetname = "Report_Year";
                //    //    break;
                //    default:
                //        sheet = "";
                //        break;
                //}

                //
                DataSet ds = ImportExeclToDataSet(savePath, filename, sheetname);
            
                int nDo = int.Parse(ddlExcelMode.Value);

                switch (nDo)
                {
                    case 0:
                        //创建
                        ImportFromExcelToCreate(ds);

                        break;

                    case 1:
                        //创建并编辑
                        ImportFromExcelToCreateAndModify(ds);
                        break;

                    case 2:
                        //编辑
                        ImportFromExcelToModify(ds);
                        break;

                    case 3:
                        //删除
                        ImportFromExcelToDelete(ds);
                        break;

                    default:
                        break;
                }

            }
             catch (Exception ee)
             {
                 MessageBox.popupClientMessage(this.Page, "检查Excle文件！" + ee.Message, "call();");

                 return;
             }

        }

        protected DataSet ImportExeclToDataSet(string filenameurl, string table, string sheetname)   
        {
            string strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + filenameurl 
                                                              + ";Extended Properties='Excel 8.0; HDR=YES; IMEX=1'";

            OleDbConnection conn = new OleDbConnection(strConn);

            conn.Open();

            DataSet ds = new DataSet();

            OleDbDataAdapter odda = new OleDbDataAdapter("select * from [" + sheetname + "$]", conn);

            odda.Fill(ds, table);

            conn.Close();

            return ds;
        }
        
        protected bool ImportFromExcelToCreate(DataSet ds)
        {
            //需要导入到机组ID
            string UnitID = ddlExcelUnitID.Value;
            //string PlantID = ddlExcelPlantID.Value;

            //if (UnitID == "NON")
            //{
            //    UnitID = "";
            //}

            //////////////////////////////////////////////////////

            try
            {
                System.Data.DataTable dt = ds.Tables[0];

                int nAll = dt.Rows.Count;
                int nCreate = 0;
                int nExist = 0;

                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    if (dr["SelectX"].ToString().ToLower() == "x")
                    {
                        string TagName = dr["TagName"].ToString().Trim();
                        //判断是否存在
                        if (Race_TagDal.TagNameExist(TagName))
                        {
                            //MessageBox.popupClientMessage(this.Page, " 该机组的输出标签已存在！", "call();");
                            nExist += 1;
                            continue;
                        }

                        //main tag
                        string keyid = PageControl.GetGuid();
                        Race_TagEntity mEntity = new Race_TagEntity();

                        mEntity.TagID = keyid;
                        mEntity.UnitID = UnitID;
                        mEntity.TagName = TagName;
                        mEntity.TagDesc = dr["TagDesc"].ToString().Trim();
                        mEntity.TagType = dr["TagType"].ToString().Trim();
                        mEntity.TagEngunit = dr["TagEngunit"].ToString().Trim();

                        string sv = dr["TagIsValid"].ToString().Trim();
                        if (sv != "")
                        {
                            mEntity.TagIsValid = int.Parse(sv);
                        }

                        sv = dr["TagIndex"].ToString().Trim();
                        if (sv != "")
                        {
                            mEntity.TagIndex = int.Parse(sv);
                        }

                        //表达式的都需要注意单引号的问题(')
                        mEntity.TagFilterExp = dr["TagFilterExp"].ToString().Trim();//.Replace("'", "''");
                        mEntity.TagCalcExp = dr["TagCalcExp"].ToString().Trim();//.Replace("'", "''");

                        sv = dr["TagCalcExpType"].ToString().Trim();
                        if (sv != "")
                        {
                            mEntity.TagCalcExpType = int.Parse(sv);
                        }

                        sv = dr["TagCalcType"].ToString().Trim();
                        if (sv != "")
                        {
                            mEntity.TagCalcType = int.Parse(sv);
                        }

                        sv = dr["TagFactor"].ToString().Trim();
                        if (sv != "")
                        {
                            mEntity.TagFactor = decimal.Parse(sv);
                        }

                        sv = dr["TagOffset"].ToString().Trim();
                        if (sv != "")
                        {
							mEntity.TagOffset = decimal.Parse(sv);
                        }

                        //mEntity.TagUnitName = dr["TagUnitName"].ToString().Trim();
                        //mEntity.TagTableName = dr["TagTableName"].ToString().Trim();                    

                        Race_TagDal.Insert(mEntity);

                        nCreate += 1;

                    }


                }    
                
                string strInfor = "标签点总数为：{0}个, 创建成功:{1}个，已存在标签点: {2}个。";
                strInfor = string.Format(strInfor, nAll, nCreate, nExist);

                MessageBox.popupClientMessage(this.Page, strInfor, "call();");

                return true;
                                
            }
            catch(Exception ee)
            {
                //
                MessageBox.popupClientMessage(this.Page, ee.Message, "call();");

                return false;
            }              
        }

        protected bool ImportFromExcelToCreateAndModify(DataSet ds)
        {
            //需要导入到机组ID
            string UnitID = ddlExcelUnitID.Value;
            //string PlantID = ddlExcelPlantID.Value;

            //if (UnitID == "NON")
            //{
            //    UnitID = "";
            //}

            //////////////////////////////////////////////////////
            try
            {
                System.Data.DataTable dt = ds.Tables[0];

                int nAll = dt.Rows.Count;
                int nCreate = 0;
                int nModify = 0;
                bool bExist = false;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (dr["SelectX"].ToString().ToLower() == "x")
                    {
                        string TagName = dr["TagName"].ToString().Trim();

                        //判断是否存在
                        bExist = Race_TagDal.TagNameExist(TagName);

                        //main tag
                        string keyid = PageControl.GetGuid();
                        if (bExist)
                        {
                            keyid = Race_TagDal.GetTagID(TagName, UnitID);
                        }

                        //main tag
                        Race_TagEntity mEntity = new Race_TagEntity();

                        mEntity.TagID = keyid;
                        mEntity.UnitID = UnitID;
                        mEntity.TagName = TagName;
                        mEntity.TagDesc = dr["TagDesc"].ToString().Trim();
                        mEntity.TagType = dr["TagType"].ToString().Trim();
                        mEntity.TagEngunit = dr["TagEngunit"].ToString().Trim();

                        string sv = dr["TagIsValid"].ToString().Trim();
                        if (sv != "")
                        {
                            mEntity.TagIsValid = int.Parse(sv);
                        }
                        
                        sv = dr["TagIndex"].ToString().Trim();
                        if (sv != "")
                        {
                            mEntity.TagIndex = int.Parse(sv);
                        }

                        mEntity.TagFilterExp = dr["TagFilterExp"].ToString().Trim();//.Replace("'", "''");
                        mEntity.TagCalcExp = dr["TagCalcExp"].ToString().Trim();//.Replace("'", "''");

                        sv = dr["TagCalcExpType"].ToString().Trim();
                        if (sv != "")
                        {
                            mEntity.TagCalcExpType = int.Parse(sv);
                        }

                        sv = dr["TagCalcType"].ToString().Trim();
                        if (sv != "")
                        {
                            mEntity.TagCalcType = int.Parse(sv);
                        }

                        sv = dr["TagFactor"].ToString().Trim();
                        if (sv != "")
                        {
							mEntity.TagFactor = decimal.Parse(sv);
                        }

                        sv = dr["TagOffset"].ToString().Trim();
                        if (sv != "")
                        {
							mEntity.TagOffset = decimal.Parse(sv);
                        }                  

                        if (bExist)
                        {
                            Race_TagDal.Update(mEntity);
                            nModify += 1;
                        }
                        else
                        {
                            Race_TagDal.Insert(mEntity);
                            nCreate += 1;
                        }                        

                    }


                }

                string strInfor = "标签点总数为：{0}个, 创建成功:{1}个， 编辑成功: {2}个。";
                strInfor = string.Format(strInfor, nAll, nCreate, nModify);

                MessageBox.popupClientMessage(this.Page, strInfor, "call();");

                return true;

            }
            catch (Exception ee)
            {
                //
                MessageBox.popupClientMessage(this.Page, ee.Message, "call();");

                return false;
            }
        }

        protected bool ImportFromExcelToModify(DataSet ds)
        {
            //需要导入到机组ID
            string UnitID = ddlExcelUnitID.Value;
            //string PlantID = ddlExcelPlantID.Value;

            //if (UnitID == "NON")
            //{
            //    UnitID = "";
            //}

            //////////////////////////////////////////////////////
            try
            {
                System.Data.DataTable dt = ds.Tables[0];

                int nAll = dt.Rows.Count;
                int nModify = 0;
                int nNoExist = 0;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (dr["SelectX"].ToString().ToLower() == "x")
                    {
                        string TagName = dr["TagName"].ToString().Trim();
                        //判断是否存在
                        if (!Race_TagDal.TagNameExist(TagName))
                        {
                            nNoExist += 1;

                            continue;
                        }

                        //main tag
                        string keyid = Race_TagDal.GetTagID(TagName, UnitID);

                        Race_TagEntity mEntity = new Race_TagEntity();

                        mEntity.TagID = keyid;
                        mEntity.UnitID = UnitID;
                        mEntity.TagName = TagName;
                        mEntity.TagDesc = dr["TagDesc"].ToString().Trim();
                        mEntity.TagType = dr["TagType"].ToString().Trim();
                        mEntity.TagEngunit = dr["TagEngunit"].ToString().Trim();

                        string sv = dr["TagIsValid"].ToString().Trim();
                        if (sv != "")
                        {
                            mEntity.TagIsValid = int.Parse(sv);
                        }
                        
                        sv = dr["TagIndex"].ToString().Trim();
                        if (sv != "")
                        {
                            mEntity.TagIndex = int.Parse(sv);
                        }

                        mEntity.TagFilterExp = dr["TagFilterExp"].ToString().Trim();//.Replace("'", "''");
                        mEntity.TagCalcExp = dr["TagCalcExp"].ToString().Trim();//.Replace("'", "''");

                        sv = dr["TagCalcExpType"].ToString().Trim();
                        if (sv != "")
                        {
                            mEntity.TagCalcExpType = int.Parse(sv);
                        }

                        sv = dr["TagCalcType"].ToString().Trim();
                        if (sv != "")
                        {
                            mEntity.TagCalcType = int.Parse(sv);
                        }

                        sv = dr["TagFactor"].ToString().Trim();
                        if (sv != "")
                        {
							mEntity.TagFactor = decimal.Parse(sv);
                        }

                        sv = dr["TagOffset"].ToString().Trim();
                        if (sv != "")
                        {
							mEntity.TagOffset = decimal.Parse(sv);
                        }


                        Race_TagDal.Update(mEntity);

                        nModify += 1;

                    }


                }

                string strInfor = "标签点总数为：{0}个, 编辑成功:{1}个，不存在标签点: {2}个。";
                strInfor = string.Format(strInfor, nAll, nModify, nNoExist);

                MessageBox.popupClientMessage(this.Page, strInfor, "call();");

                return true;

            }
            catch (Exception ee)
            {
                //
                MessageBox.popupClientMessage(this.Page, ee.Message, "call();");

                return false;
            }
        }

        protected bool ImportFromExcelToDelete(DataSet ds)
        {
            //需要导入到机组ID
            string UnitID = ddlExcelUnitID.Value;
            //string PlantID = ddlExcelPlantID.Value;

            //if (UnitID == "NON")
            //{
            //    UnitID = "";
            //}

            //////////////////////////////////////////////////////

            try
            {
                System.Data.DataTable dt = ds.Tables[0];

                int nAll = dt.Rows.Count;
                int nDelete = 0;
                int nEmpty = 0;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (dr["SelectX"].ToString().ToLower() == "x")
                    {
                        string TagName = dr["TagName"].ToString().Trim();

                        //判断是否存在
                        string strTagID = Race_TagDal.GetTagID(TagName, UnitID);
                        if (strTagID == "")
                        {
                            nEmpty += 1;
                            continue;
                        }
                        else
                        {
                            Race_TagDal.Delete(strTagID);
                            nDelete += 1;
                        }
                    }
                }

                string strInfor = "标签点总数为：{0}个, 删除成功:{1}个，空标签点: {2}个。";
                strInfor = string.Format(strInfor, nAll, nDelete, nEmpty);

                MessageBox.popupClientMessage(this.Page, strInfor, "call();");

                return true;

            }
            catch (Exception ee)
            {
                //
                MessageBox.popupClientMessage(this.Page, ee.Message, "call();");

                return false;
            }

        }

        #endregion 

        #region 标签点与实时数据库同步配置

        /// <summary>
        /// 同步标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSynPI_Click(object sender, EventArgs e)
        {
            //if (ddlSynUnitID.Value == "")
            //{
            //    MessageBox.popupClientMessage(this.Page, "请选择机组！", "call();");
            //    return;
            //}


            ////先执行关闭连接
            ////DBAccess.GetRealTime().ReConnect();

            //if (!DBAccess.GetRealTime().Connection())
            //{
            //    MessageBox.popupClientMessage(this.Page, "实时库连接错误，请检查！", "call();");
            //    return;
            //}

            //System.Data.DataTable dt = Filter_TagMainDal.SynchronPI(ddlSynUnitID.Value);

            //if (dt.Rows.Count < 1)
            //{
            //    //gvTagAlarm.Visible = false;
            //    MessageBox.popupClientMessage(this.Page, "同步完成，无异常数据！", "call();");
            //    return;
            //}
            //else
            //{
            //    //gvTag.Visible = false;
            //    //gvTagAlarm.Visible = true;
            //    //gvTagAlarm.DataSource = dt;
            //    //gvTagAlarm.DataBind();
            //}
        }
        
        #endregion

        #region 数据库标签点删除配置

        protected void btnDelApply_Click(object sender, EventArgs e)
        {
            //txtDelCondition.Text = txtDelCondition.Text.Trim();

            ////Delete
            //if (ddlDelUnitID.Value == "")
            //{
            //    lblDelInfor.Text = "源机组不能为空！";
            //    return;
            //}

            //if (txtDelCondition.Text == "")
            //{
            //    lblDelInfor.Text = "源标签点查询规则必须输入！";
            //    return;
            //}

            TagDelete();

            return;

        }

        protected void TagDelete()
        {
            //string UnitID = ddlDelUnitID.Value;
            //string DelCN = txtDelCondition.Text.Trim();

            //string condition = "";

            //if (UnitID != "")
            //{
            //    condition += " and UnitID='" + UnitID + "'";
            //}

            //if (DelCN != "")
            //{
            //    condition += " and MainOutTagName like '" + DelCN + "'";
            //}

            //System.Data.DataTable dt = Filter_TagMainDal.GetSearchList3(condition);

            //if (dt.Rows.Count <= 0)
            //{
            //    lblDelInfor.Text = "无记录满足查询要求！";

            //    return;
            //}

            //foreach (DataRow dr in dt.Rows)
            //{
            //    //dr["color"] = "green";
            //    Filter_TagMainDal.Delete(dr["MainID"].ToString());
            //}

            //lblDelInfor.Text = "共删除记录： " + dt.Rows.Count.ToString() + " 条！";

            //return;
        }

        #endregion

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Race_Tag.aspx");
        }

    }
}
