using SIS.DataEntity;
using SIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SISKPI.KPIAlarm {

    public partial class OverLimitConfigDialog : System.Web.UI.Page {

        #region 重写方法

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (!IsPostBack) {

            }
        }

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);
        }
        #endregion

        #region 事件

        protected void btnDataImport_Click(object sender, EventArgs e) {
            string IsXls = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();
            if ((IsXls != ".xls") && (IsXls != ".xlsx")) {
                ShowMessage("文件格式不正确，导入的文件必须是Excel文件");
                return;
            }
            try {
                DirectoryInfo path = new DirectoryInfo(Server.MapPath("~\\excel\\"));
                foreach (FileInfo f in path.GetFiles()) {
                    f.Delete();
                }
                string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName;
                //Server.MapPath 获得虚拟服务器相对路径
                string savePath = Server.MapPath(("~\\excel\\") + filename);
                //SaveAs 将上传的文件内容保存在服务器上
                FileUpload1.SaveAs(savePath);
                ImportData(savePath);
                ShowMessage("文件导入成功！");
            }
            catch (Exception ex) {
                ShowMessage("文件导入失败，请核对文件重新导入！");
                Console.Write(ex.StackTrace);
            }
        }

        #endregion

        #region 私有方法

        private void ImportData(String filePath) {
            FileInfo fileInfo = new FileInfo(filePath);
            String fileType = fileInfo.Extension;
            if ((fileType != ".xlsx") && (fileType != ".xls")) return;
            String connStr;
            if (fileType == ".xls")
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fileInfo.FullName + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            else
                connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileInfo.FullName + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
            using (OleDbConnection Connection = new OleDbConnection(connStr)) {
                Connection.Open();
                using (DataTable schemaTable = Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null)) {
                    DataSet ds;
                    OleDbDataAdapter DataAdapter;
                    String sheetName = (String)schemaTable.Rows[0]["TABLE_NAME"];
                    String CommandText = " SELECT * FROM [" + sheetName + "]";
                    DataAdapter = new OleDbDataAdapter(CommandText, Connection);
                    ds = new DataSet();
                    DataAdapter.Fill(ds);
                    SaveDataToDB(ds.Tables[0]);
                    ds.Dispose();
                    DataAdapter.Dispose();
                }
                Connection.Close();
            }
        }

        private void SaveDataToDB(DataTable SourceTable) {
            using (KPI_OverLimitConfigDal DataAccess = new KPI_OverLimitConfigDal()) {
                OverLimitConfigEntity OverLimitConfig;
                String OverLimitType = "";
                String Message = "<ul>";
                DataRowCollection Rows = SourceTable.Rows;
                foreach (DataRow Row in Rows) {
                    OverLimitConfig = new OverLimitConfigEntity();
                    OverLimitConfig.TagName = GetTagID(Convert.ToString(Row[0]));
                    OverLimitConfig.TagCode = Convert.ToString(Row[0]);
                    if (String.IsNullOrWhiteSpace(OverLimitConfig.TagName)) {
                        //Message += "<li>测点" + OverLimitConfig.TagCode + "导入失败。</li>";
                        continue;
                    }
                    OverLimitConfig.FirstLimitingValue = ConvertToDecimal(Row[1]);
                    OverLimitConfig.SecondLimitingValue = ConvertToDecimal(Row[2]);
                    OverLimitConfig.ThirdLimitingValue = ConvertToDecimal(Row[3]);
                    OverLimitConfig.FourthLimitingValue = ConvertToDecimal(Row[4]);
                    OverLimitConfig.LowLimit1Value = ConvertToDecimal(Row[5]);
                    OverLimitConfig.LowLimit2Value = ConvertToDecimal(Row[6]);
                    OverLimitConfig.LowLimit3Value = ConvertToDecimal(Row[7]);
                    OverLimitConfig.OverLimitComputeType = 0;
                    OverLimitType =Convert.ToString(Row[8]);
                    if ((!String.IsNullOrWhiteSpace(OverLimitType)) && (OverLimitType == "曲线")) {
                        OverLimitConfig.OverLimitComputeType = 1;
                    }
                    OverLimitConfig.FirstLimitingTag = Convert.ToString(Row[9]);
                    OverLimitConfig.SecondLimitingTag = Convert.ToString(Row[10]);
                    OverLimitConfig.ThirdLimitingTag = Convert.ToString(Row[11]);
                    OverLimitConfig.FourthLimitingTag = Convert.ToString(Row[12]);
                    OverLimitConfig.LowLimit1Tag = Convert.ToString(Row[13]);
                    OverLimitConfig.LowLimit2Tag = Convert.ToString(Row[14]);
                    OverLimitConfig.LowLimit3Tag = Convert.ToString(Row[15]);
                    OverLimitConfig.Comment = Convert.ToString(Row[16]);
                    try {
                        DataAccess.SavOverLimitConfigs(OverLimitConfig);
                    }
                    catch (Exception ex) {
                        Message += "<li>" + ex.Message + "</li>";
                    }
                }
                Message += "</ul>";
                lblError.Text = Message;
            }
        }

        private decimal? ConvertToDecimal(Object Value) {
            if (Value == null) return null;
            String V = Value.ToString();
            if (String.IsNullOrWhiteSpace(V)) return null;
            return Convert.ToDecimal(V);
        }

        private String GetTagID(String TagCode) {
            if (String.IsNullOrWhiteSpace(TagCode)) return "";
            return KPI_RealTagDal.GetID(TagCode);
        }

        private void ShowMessage(String msg) {
            String js = "alert(\"" + msg + "\");";
            ClientScript.RegisterClientScriptBlock(GetType(), "ShowMessage", js, true);
        }

        #endregion

    }
}