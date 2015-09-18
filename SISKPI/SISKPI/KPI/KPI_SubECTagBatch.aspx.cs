using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.OleDb;


using SIS.Assistant;
using SIS.Loger;
using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
using SIS.Exceler;

namespace SISKPI
{
    public partial class KPI_SubECTagBatch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //机组信息
                //DataTable dt = KPI_UnitDal.GetUnits("");
                //ddl_UnitID.Items.Add(new ListItem("所有机组集", "ALL"));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    ddl_UnitID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                //}

                //Excel
                //btnExcelExport.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility=''; progress_update();");
                btnExcelImport.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility=''; progress_update();");
                                
                ddlExcelMode.Items.Add(new ListItem("创建", "0"));
                ddlExcelMode.Items.Add(new ListItem("创建并修改", "1"));
                ddlExcelMode.Items.Add(new ListItem("修改", "2"));
                ddlExcelMode.Items.Add(new ListItem("删除", "3"));
            }


        }

        #region Excel标签点操作

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

            //    //通过超链接跳转到下载页面，解决弹出该页面的aspx文件要求下载的问题
            //    //指定下载文件名称
            //    curContext.Response.AddHeader("Content-Disposition", "attachment;filename=KPIECTag.xls");

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
            string strExcelFile = "SIS经济指标表";

            try
            {
                System.Data.DataTable dt = ECTagDal.GetTagListForExcel("");

                if (dt == null)
                    return;

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
            //if (ddl_UnitID.Value == "ALL")
            //{
            //    MessageBox.popupClientMessage(this.Page, "请选择具体机组！", "call();");
            //    return;
            //}

            if (ddlExcelMode.Value == "")
            {
                MessageBox.popupClientMessage(this.Page, "请选择Excel表操作方式！", "call();");
        
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
                //
                DataSet ds = ImportExeclToDataSet(savePath, filename, sheetname);
            
                int nDo = int.Parse(ddlExcelMode.Value);

                //string UnitID = ddl_UnitID.Value.Trim();

                switch (nDo)
                {
                    case 0:
                        //创建
                        ImportFromExcelToCreate(ds);

                        break;

                    case 1:
                        //创建并修改
                        ImportFromExcelToCreateAndModify(ds);
                        break;

                    case 2:
                        //修改
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
            string strKPIError = "";

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
                        string ECCode = dr["ECCode"].ToString().Trim();
                        
                        strKPIError = ECCode;

                        //判断是否存在
                        if (ECTagDal.CodeExist(ECCode, "") || ALLDal.CodeExist(ECCode, ""))
                        {
                            //MessageBox.popupClientMessage(this.Page, " 该机组的输出标签已存在！", "call();");
                            nExist += 1;
                            continue;
                        }

                        //main tag
                        string keyid = PageControl.GetGuid();
                        ECTagEntity mEntity = new ECTagEntity();

                        mEntity.ECID = keyid;
                        string UnitName = dr["UnitName"].ToString().Trim();
                        mEntity.UnitID = KPI_UnitDal.GetUnitID(UnitName);
                        string SeqName = dr["SeqName"].ToString().Trim();
                        mEntity.SeqID = KPI_SeqDal.GetSeqID(SeqName);
                        string KpiName = dr["KpiName"].ToString().Trim();
                        mEntity.KpiID = KpiDal.GetKpiID(KpiName);
                        string EngunitName = dr["EngunitName"].ToString().Trim();
                        mEntity.EngunitID = EngunitDal.GetEngunitID(EngunitName);
                        string CycleName = dr["CycleName"].ToString().Trim();
                        mEntity.CycleID = CycleDal.GetCycleID(CycleName);

                        mEntity.ECCode = dr["ECCode"].ToString().Trim();
                        mEntity.ECName = dr["ECName"].ToString().Trim();
                        mEntity.ECDesc = dr["ECDesc"].ToString().Trim();
                        mEntity.ECIndex = int.Parse(dr["ECIndex"].ToString().Trim());
                        mEntity.ECWeb = dr["ECWeb"].ToString().Trim();
                        mEntity.ECIsValid = int.Parse(dr["ECIsValid"].ToString().Trim());

                        mEntity.ECIsCalc = int.Parse(dr["ECIsCalc"].ToString().Trim());
                        mEntity.ECIsAsses = int.Parse(dr["ECIsAsses"].ToString().Trim());
                        mEntity.ECIsZero = int.Parse(dr["ECIsZero"].ToString().Trim());
                        mEntity.ECIsDisplay = int.Parse(dr["ECIsDisplay"].ToString().Trim());
                        mEntity.ECIsTotal = int.Parse(dr["ECIsTotal"].ToString().Trim());
                        mEntity.ECDesign = dr["ECDesign"].ToString().Trim();

                        mEntity.ECOptimal = dr["ECOptimal"].ToString().Trim();
                        if (dr["ECMaxValue"].ToString().Trim() != "")
                        {
							mEntity.ECMaxValue = decimal.Parse(dr["ECMaxValue"].ToString().Trim());
                        }
                        if (dr["ECMinValue"].ToString().Trim() != "")
                        {
							mEntity.ECMinValue = decimal.Parse(dr["ECMinValue"].ToString().Trim());
                        }
						mEntity.ECWeight = decimal.Parse(dr["ECWeight"].ToString().Trim());
                        mEntity.ECCalcClass = int.Parse(dr["ECCalcClass"].ToString().Trim());
                        mEntity.ECFilterExp = dr["ECFilterExp"].ToString().Trim();

                        mEntity.ECCalcExp = dr["ECCalcExp"].ToString().Trim();
                        mEntity.ECCalcDesc = dr["ECCalcDesc"].ToString().Trim();
                        mEntity.ECIsSnapshot = int.Parse(dr["ECIsSnapshot"].ToString().Trim());
                        mEntity.ECXLineType = int.Parse(dr["ECXLineType"].ToString().Trim());
                        mEntity.ECXLineGetType = int.Parse(dr["ECXLineGetType"].ToString().Trim());
                        mEntity.ECXLineXRealTag = dr["ECXLineXRealTag"].ToString().Trim();

                        mEntity.ECXLineYRealTag = dr["ECXLineYRealTag"].ToString().Trim();
                        mEntity.ECXLineZRealTag = dr["ECXLineZRealTag"].ToString().Trim();
                        mEntity.ECXLineXYZ = dr["ECXLineXYZ"].ToString().Trim();
                        mEntity.ECScoreExp = dr["ECScoreExp"].ToString().Trim();
                        mEntity.ECCurveGroup = dr["ECCurveGroup"].ToString().Trim();
                        mEntity.ECIsSort = int.Parse(dr["ECIsSort"].ToString().Trim());
                        mEntity.ECType = int.Parse(dr["ECType"].ToString().Trim());

                        mEntity.ECSort = int.Parse(dr["ECSort"].ToString().Trim());
                        mEntity.ECScore = dr["ECScore"].ToString().Trim();
                        mEntity.ECExExp = dr["ECExExp"].ToString().Trim();
                        mEntity.ECExScore = dr["ECExScore"].ToString().Trim();
                        mEntity.ECNote = dr["ECNote"].ToString().Trim();

                        mEntity.ECCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                        mEntity.ECModifyTime = mEntity.ECCreateTime;


                        ECTagDal.Insert(mEntity);

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
                MessageBox.popupClientMessage(this.Page, strKPIError +": "+ ee.Message, "call();");

                return false;
            }              
        }

        protected bool ImportFromExcelToCreateAndModify(DataSet ds)
        {
            string strKPIError = "";

            try
            {
                System.Data.DataTable dt = ds.Tables[0];

                int nAll = dt.Rows.Count;
                int nCreate = 0;
                int nModify = 0;
                int nNotValid = 0;
                bool bExist = false;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (dr["SelectX"].ToString().ToLower() == "x")
                    {
                        string ECCode = dr["ECCode"].ToString().Trim();

                        strKPIError = ECCode;

                        //判断是否存在
                        bExist = ECTagDal.CodeExist(ECCode, "");

                        string keyid = PageControl.GetGuid();
                        if (bExist)
                        {
                            keyid = ECTagDal.GetECIDByCode(ECCode);

                            //MessageBox.popupClientMessage(this.Page, " 该机组的输出标签已存在！", "call();");
                            //nExist += 1;
                            //continue;
                        }
                        else
                        {
                            //其他表存在也不能创建
                            if (ALLDal.CodeExist(ECCode, ""))
                            {
                                nNotValid += 1;
                                continue;
                            }

                        }

                        ECTagEntity mEntity = new ECTagEntity();

                        mEntity.ECID = keyid;
                        string UnitName = dr["UnitName"].ToString().Trim();
                        mEntity.UnitID = KPI_UnitDal.GetUnitID(UnitName);
                        string SeqName = dr["SeqName"].ToString().Trim();
                        mEntity.SeqID = KPI_SeqDal.GetSeqID(SeqName);
                        string KpiName = dr["KpiName"].ToString().Trim();
                        mEntity.KpiID = KpiDal.GetKpiID(KpiName);
                        string EngunitName = dr["EngunitName"].ToString().Trim();
                        mEntity.EngunitID = EngunitDal.GetEngunitID(EngunitName);
                        string CycleName = dr["CycleName"].ToString().Trim();
                        mEntity.CycleID = CycleDal.GetCycleID(CycleName);

                        mEntity.ECCode = dr["ECCode"].ToString().Trim();
                        mEntity.ECName = dr["ECName"].ToString().Trim();
                        mEntity.ECDesc = dr["ECDesc"].ToString().Trim();
                        mEntity.ECIndex = int.Parse(dr["ECIndex"].ToString().Trim());
                        mEntity.ECWeb = dr["ECWeb"].ToString().Trim();
                        mEntity.ECIsValid = int.Parse(dr["ECIsValid"].ToString().Trim());

                        mEntity.ECIsCalc = int.Parse(dr["ECIsCalc"].ToString().Trim());
                        mEntity.ECIsAsses = int.Parse(dr["ECIsAsses"].ToString().Trim());
                        mEntity.ECIsZero = int.Parse(dr["ECIsZero"].ToString().Trim());
                        mEntity.ECIsDisplay = int.Parse(dr["ECIsDisplay"].ToString().Trim());
                        mEntity.ECIsTotal = int.Parse(dr["ECIsTotal"].ToString().Trim());
                        mEntity.ECDesign = dr["ECDesign"].ToString().Trim();

                        mEntity.ECOptimal = dr["ECOptimal"].ToString().Trim();
                        if (dr["ECMaxValue"].ToString().Trim() != "")
                        {
                            mEntity.ECMaxValue = decimal.Parse(dr["ECMaxValue"].ToString().Trim());
                        }
                        if (dr["ECMinValue"].ToString().Trim() != "")
                        {
							mEntity.ECMinValue = decimal.Parse(dr["ECMinValue"].ToString().Trim());
                        }
						mEntity.ECWeight = decimal.Parse(dr["ECWeight"].ToString().Trim());
                        mEntity.ECCalcClass = int.Parse(dr["ECCalcClass"].ToString().Trim());
                        mEntity.ECFilterExp = dr["ECFilterExp"].ToString().Trim();

                        mEntity.ECCalcExp = dr["ECCalcExp"].ToString().Trim();
                        mEntity.ECCalcDesc = dr["ECCalcDesc"].ToString().Trim();
                        mEntity.ECIsSnapshot = int.Parse(dr["ECIsSnapshot"].ToString().Trim());
                        mEntity.ECXLineType = int.Parse(dr["ECXLineType"].ToString().Trim());
                        mEntity.ECXLineGetType = int.Parse(dr["ECXLineGetType"].ToString().Trim());
                        mEntity.ECXLineXRealTag = dr["ECXLineXRealTag"].ToString().Trim();

                        mEntity.ECXLineYRealTag = dr["ECXLineYRealTag"].ToString().Trim();
                        mEntity.ECXLineZRealTag = dr["ECXLineZRealTag"].ToString().Trim();
                        mEntity.ECXLineXYZ = dr["ECXLineXYZ"].ToString().Trim();
                        mEntity.ECScoreExp = dr["ECScoreExp"].ToString().Trim();
                        mEntity.ECCurveGroup = dr["ECCurveGroup"].ToString().Trim();
                        mEntity.ECIsSort = int.Parse(dr["ECIsSort"].ToString().Trim());
                        mEntity.ECType = int.Parse(dr["ECType"].ToString().Trim());

                        mEntity.ECSort = int.Parse(dr["ECSort"].ToString().Trim());
                        mEntity.ECScore = dr["ECScore"].ToString().Trim();
                        mEntity.ECExExp = dr["ECExExp"].ToString().Trim();
                        mEntity.ECExScore = dr["ECExScore"].ToString().Trim();
                        mEntity.ECNote = dr["ECNote"].ToString().Trim();

                        mEntity.ECCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                        mEntity.ECModifyTime = mEntity.ECCreateTime;
                                              

                        if (bExist)
                        {
                            ECTagDal.Update(mEntity);
                            nModify += 1;
                        }
                        else
                        {
                            ECTagDal.Insert(mEntity);
                            nCreate += 1;
                        }                        

                    }
                }

                string strInfor = "标签点总数为：{0}个, 创建成功:{1}个， 修改成功: {2}个，其他表存在Code: {3}个。";
                strInfor = string.Format(strInfor, nAll, nCreate, nModify, nNotValid);

                MessageBox.popupClientMessage(this.Page, strInfor, "call();");

                return true;

            }
            catch (Exception ee)
            {
                //
                MessageBox.popupClientMessage(this.Page, strKPIError +": "+ ee.Message, "call();");

                return false;
            }
        }

        protected bool ImportFromExcelToModify(DataSet ds)
        {
            string strError = "";
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
                        string ECCode = dr["ECCode"].ToString().Trim();
                        strError = ECCode;

                        //判断是否存在
                        if (!ECTagDal.CodeExist(ECCode, ""))
                        {
                            //MessageBox.popupClientMessage(this.Page, " 该机组的输出标签已存在！", "call();");
                            nNoExist += 1;
                            continue;
                        }

                        //main tag
                        string keyid = ECTagDal.GetECIDByCode(ECCode);


                        ECTagEntity mEntity = new ECTagEntity();

                        mEntity.ECID = keyid;
                        string UnitName = dr["UnitName"].ToString().Trim();
                        mEntity.UnitID = KPI_UnitDal.GetUnitID(UnitName);
                        string SeqName = dr["SeqName"].ToString().Trim();
                        mEntity.SeqID = KPI_SeqDal.GetSeqID(SeqName);
                        string KpiName = dr["KpiName"].ToString().Trim();
                        mEntity.KpiID = KpiDal.GetKpiID(KpiName);
                        string EngunitName = dr["EngunitName"].ToString().Trim();
                        mEntity.EngunitID = EngunitDal.GetEngunitID(EngunitName);
                        string CycleName = dr["CycleName"].ToString().Trim();
                        mEntity.CycleID = CycleDal.GetCycleID(CycleName);

                        mEntity.ECCode = dr["ECCode"].ToString().Trim();
                        mEntity.ECName = dr["ECName"].ToString().Trim();
                        mEntity.ECDesc = dr["ECDesc"].ToString().Trim();
                        mEntity.ECIndex = int.Parse(dr["ECIndex"].ToString().Trim());
                        mEntity.ECWeb = dr["ECWeb"].ToString().Trim();
                        mEntity.ECIsValid = int.Parse(dr["ECIsValid"].ToString().Trim());

                        mEntity.ECIsCalc = int.Parse(dr["ECIsCalc"].ToString().Trim());
                        mEntity.ECIsAsses = int.Parse(dr["ECIsAsses"].ToString().Trim());
                        mEntity.ECIsZero = int.Parse(dr["ECIsZero"].ToString().Trim());
                        mEntity.ECIsDisplay = int.Parse(dr["ECIsDisplay"].ToString().Trim());
                        mEntity.ECIsTotal = int.Parse(dr["ECIsTotal"].ToString().Trim());
                        mEntity.ECDesign = dr["ECDesign"].ToString().Trim();

                        mEntity.ECOptimal = dr["ECOptimal"].ToString().Trim();
                        if (dr["ECMaxValue"].ToString().Trim() != "")
                        {
							mEntity.ECMaxValue = decimal.Parse(dr["ECMaxValue"].ToString().Trim());
                        }
                        if (dr["ECMinValue"].ToString().Trim() != "")
                        {
							mEntity.ECMinValue = decimal.Parse(dr["ECMinValue"].ToString().Trim());
                        }
						mEntity.ECWeight = decimal.Parse(dr["ECWeight"].ToString().Trim());
                        mEntity.ECCalcClass = int.Parse(dr["ECCalcClass"].ToString().Trim());
                        mEntity.ECFilterExp = dr["ECFilterExp"].ToString().Trim();

                        mEntity.ECCalcExp = dr["ECCalcExp"].ToString().Trim();
                        mEntity.ECCalcDesc = dr["ECCalcDesc"].ToString().Trim();
                        mEntity.ECIsSnapshot = int.Parse(dr["ECIsSnapshot"].ToString().Trim());
                        mEntity.ECXLineType = int.Parse(dr["ECXLineType"].ToString().Trim());
                        mEntity.ECXLineGetType = int.Parse(dr["ECXLineGetType"].ToString().Trim());
                        mEntity.ECXLineXRealTag = dr["ECXLineXRealTag"].ToString().Trim();

                        mEntity.ECXLineYRealTag = dr["ECXLineYRealTag"].ToString().Trim();
                        mEntity.ECXLineZRealTag = dr["ECXLineZRealTag"].ToString().Trim();
                        mEntity.ECXLineXYZ = dr["ECXLineXYZ"].ToString().Trim();
                        mEntity.ECScoreExp = dr["ECScoreExp"].ToString().Trim();
                        mEntity.ECCurveGroup = dr["ECCurveGroup"].ToString().Trim();
                        mEntity.ECIsSort = int.Parse(dr["ECIsSort"].ToString().Trim());
                        mEntity.ECType = int.Parse(dr["ECType"].ToString().Trim());

                        mEntity.ECSort = int.Parse(dr["ECSort"].ToString().Trim());
                        mEntity.ECScore = dr["ECScore"].ToString().Trim();
                        mEntity.ECExExp = dr["ECExExp"].ToString().Trim();
                        mEntity.ECExScore = dr["ECExScore"].ToString().Trim();
                        mEntity.ECNote = dr["ECNote"].ToString().Trim();

                        mEntity.ECCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                        mEntity.ECModifyTime = mEntity.ECCreateTime;


                        ECTagDal.Update(mEntity);

                        nModify += 1;
                    }


                }

                string strInfor = "标签点总数为：{0}个, 修改成功:{1}个，不存在标签点: {2}个。";
                strInfor = string.Format(strInfor, nAll, nModify, nNoExist);

                MessageBox.popupClientMessage(this.Page, strInfor, "call();");

                return true;

            }
            catch (Exception ee)
            {
                //
                MessageBox.popupClientMessage(this.Page, strError + ": " + ee.Message, "call();");

                return false;
            }
        }

        protected bool ImportFromExcelToDelete(DataSet ds)
        {
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
                        string ECCode = dr["ECCode"].ToString().Trim();

                        //判断是否存在
                        if (!ECTagDal.CodeExist(ECCode, ""))
                        {
                            //MessageBox.popupClientMessage(this.Page, " 该机组的输出标签已存在！", "call();");
                            nEmpty += 1;
                            continue;
                        }
                        else
                        {
                            //main tag
                            ECTagEntity mEntity = new ECTagEntity();
                            mEntity.ECID = ECTagDal.GetECIDByCode(ECCode);

                            ECTagDal.Delete(mEntity);
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


        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_ECTagConfig.aspx");
        }
    }
}
