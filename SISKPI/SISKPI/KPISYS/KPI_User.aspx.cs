using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

using SIS.DataAccess;
using SIS.DataEntity;
using SIS.Loger;
using SIS.Assistant;
using SIS.DBControl;
using SIS.Exceler;


using System.IO;
using System.Data.OleDb;

namespace SISKPI
{
    public partial class KPI_User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnSISAdmin.Attributes.Add("onclick", "javascript:return confirm('此操作将删除所有用户！确认初始化吗？');");

                BindGrid();
            }
        }

        void BindGrid()
        {
            string username = tbx_UserName.Text.Trim();

            DataTable dt = KPI_UserDal.GetUsers(username);

            gvUser.DataSource = dt;

            gvUser.DataBind();
        }

        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlInputHidden key = (HtmlInputHidden)e.Row.Cells[0].FindControl("userid");

                string userisvalid = ((HtmlInputHidden)e.Row.Cells[0].FindControl("userisvalid")).Value;

                if ((ImageButton)e.Row.Cells[6].FindControl("lb_valid") != null)
                {
                    ImageButton lb = (ImageButton)e.Row.Cells[6].FindControl("lb_valid");

                    if (userisvalid == "0")
                    {
                        lb.ImageUrl = "../imgs/block.jpg";
                        lb.Attributes.Add("onclick", "javascript:return confirm('开启此用户吗？');");
                    }
                    else
                    {
                        lb.ImageUrl = "../imgs/valid.jpg";
                        lb.Attributes.Add("onclick", "javascript:return confirm('关闭此用户吗？');");
                    }
                }

                if ((LinkButton)e.Row.Cells[7].FindControl("password") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.Cells[7].FindControl("password");
                    lb.Attributes.Add("onclick", "javascript:return confirm('重置密码吗？');");
                }


                if ((HtmlInputButton)e.Row.Cells[8].FindControl("userupdate") != null)
                {
                    HtmlInputButton btn = (HtmlInputButton)e.Row.Cells[8].FindControl("userupdate");
                    btn.Attributes.Add("onclick", "javascript:UserUpdate('" + key.Value + "');");
                }


                if ((LinkButton)e.Row.Cells[9].FindControl("lb_delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.Cells[9].FindControl("lb_delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }
        }


        protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] estr = e.CommandArgument.ToString().Split(new string[] { "$"}, StringSplitOptions.RemoveEmptyEntries);
            if (estr.Length < 2)
            {
                MessageBox.popupClientMessage(this.Page, "参数无法获取正确,请联系开发人员。", "call();");
                return;
            }

            string userid = Convert.ToString(estr[0]);
            string nextparam = Convert.ToString(estr[1]);                

            if (e.CommandName == "passwordUpdate")
            {
                string newpassword = nextparam;

                if (KPI_UserDal.SetInitPassword(newpassword, userid))
                {
                    MessageBox.popupClientMessage(this.Page, "初始化成功,密码为:123456", "call();");

                    BindGrid();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "初始化错误，请联系技术支持！", "call();");
                }

            }
            else if (e.CommandName == "validUpdate")
            {
                string userisvalid = nextparam;

                userisvalid = userisvalid == "1" ? "0" : "1";

                if (KPI_UserDal.SetUserValid(userisvalid, userid))
                {
                    MessageBox.popupClientMessage(this.Page, "设置成功！", "call();");

                    BindGrid();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "设置错误，请联系技术支持！", "call();");
                }

            }
            else if (e.CommandName == "dataDelete")
            {

                if (KPI_UserDal.DeleteUserID(userid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");
                    BindGrid();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                    return;
                }

            }
        }
      
        protected void btnMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_Menu.aspx");
        }

        protected void btnUser_Click(object sender, EventArgs e)
        {
            //Response.Redirect("KPI_User.aspx");

        }

        protected void btnGroup_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_Group.aspx");

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        
        //protected void btnSISAdmin_Click(object sender, EventArgs e)
        //{
        //    KPI_UserDal.SetSISAdmin();

        //    MessageBox.popupClientMessage(this.Page, "初始化成功！", "call();");
        //}


        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            string strjs = "<script language=javascript>window.open('KPI_SubUserConfig.aspx?opr=add','newwindow','width=600,height=500')</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", strjs);
        }


        protected void btnAddGroup_Click(object sender, EventArgs e)
        {
            string strjs = "<script language=javascript>window.open('KPI_SubUserGroup.aspx','newwindow','width=600,height=500')</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", strjs);


        }

        #region Excel Import Done

        protected void btnImport_Click(object sender, EventArgs e)
        {
            string sheetname = tbxSheet.Text.Trim();
            if (sheetname == "")
            {
                MessageBox.popupClientMessage(this.Page, "请输入表单名称！", "call();");

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
                MessageBox.popupClientMessage(this.Page, "只允许选择(.xls)文件！", "call();");

                return;
            }

            //删除临时文件夹的文件
            System.IO.DirectoryInfo path = new System.IO.DirectoryInfo(Server.MapPath("~\\excel\\"));
            foreach (System.IO.FileInfo f in path.GetFiles())
            {
                f.Delete();
            }

            //获取Execle文件名  DateTime日期函数   
            string filename = DateTime.Now.ToString("yyyymmddhhMMss") + fuExcel.FileName;
            //Server.MapPath 获得虚拟服务器相对路径
            string savePath = Server.MapPath(("~\\excel\\") + filename);
            //SaveAs 将上传的文件内容保存在服务器上
            fuExcel.SaveAs(savePath);

            //
            DataSet ds = ImportExeclToDataSet(savePath, filename, sheetname);

            ImportFromExcelToCreate(ds);

            //

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


        protected void ImportFromExcelToCreate(DataSet ds)
        {
            try
            {
                System.Data.DataTable dt = ds.Tables[0];

                int nAll = dt.Rows.Count;
                int nCreate = 0;
                int nExist = 0;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (dr["SelectX"].ToString().ToLower() == "x")
                    {
                        string userid = Guid.NewGuid().ToString();
                        string usercode = dr["UserCode"].ToString().Trim();

                        //判断是否存在
                        if (KPI_UserDal.UserCodeExist(usercode, userid))
                        {
                            nExist += 1;
                            continue;
                        }

                        string username = dr["UserName"].ToString().Trim();
                        string userdesc = dr["UserDesc"].ToString().Trim();
                        string userpassword = KPI_UserDal.GetDESString(dr["UserPassword"].ToString().Trim());
                        string useremail = dr["UserEmail"].ToString().Trim();
                        string userphone = dr["UserPhone"].ToString().Trim();
                        string usertitle = dr["UserTitle"].ToString().Trim();
                        string usergroups = "";
                        int userisvalid = int.Parse(dr["UserIsValid"].ToString().Trim());
                        string usernote = dr["UserNote"].ToString().Trim();
                        string usercreatetime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                        //string usermodifytime = usercreatetime;

                        KPI_UserEntity usMenu = new KPI_UserEntity();
                        usMenu.UserID = userid;
                        usMenu.UserCode = usercode;
                        usMenu.UserName = username;
                        usMenu.UserDesc = userdesc;
                        usMenu.UserPassword = userpassword;
                        usMenu.UserEMail = useremail;
                        usMenu.UserPhone = userphone;
                        usMenu.UserTitle = usertitle;
                        usMenu.UserGroups = usergroups;
                        usMenu.UserIsValid = userisvalid;
                        usMenu.UserNote = usernote;
                        usMenu.UserCreateTime = usercreatetime;
                        usMenu.UserModifyTime = usercreatetime;

                        if (KPI_UserDal.Insert(usMenu))
                        {
                            nCreate += 1;
                        }

                    }
                }


                string strInfor = "用户总数：{0}个, 创建成功:{1}个，已存在用户: {2}个。";
                strInfor = string.Format(strInfor, nAll, nCreate, nExist);

                MessageBox.popupClientMessage(this.Page, strInfor, "call();");


                BindGrid();

                return;

            }
            catch (Exception)
            {
                //
                //MessageBox.popupClientMessage(this.Page, ee.Message, "call();");

                return;
            }
        }


        protected void btnExport_Click(object sender, EventArgs e)
        {
            string strExcelFile = "SISUser";

            try
            {
                System.Data.DataTable dt = KPI_UserDal.GetUsersForExcel();

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

        #endregion

       
    }
}
