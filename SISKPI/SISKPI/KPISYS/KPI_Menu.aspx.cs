using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using System.IO;
using System.Data.OleDb;

using SIS.DBControl;
using SIS.Loger;

using SIS.DataAccess;
using SIS.DataEntity;

namespace SISKPI
{
    /// <summary>
    /// SIS系统管理
    /// 菜单管理：添加、删除、修改
    /// </summary>
    public partial class KPI_Menu : System.Web.UI.Page
    {
        //public static DataTable dtGroups = null;
        //public static string strGroupCollections = "";

        private static DataSet dsMenus = null;
        private static string strGroupSigle = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnImport.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility=''; progress_update();");

                TVWMenu.Attributes.Add("onclick", "postBackByObject()");

                btnMenuAdd.Attributes.Add("onclick", "javascript:return confirm('确认添加吗？');");
                btnMenuModify.Attributes.Add("onclick", "javascript:return confirm('确认修改吗？');");

                btnDel.Attributes.Add("onclick", "javascript:return confirm('确认删除该节点及其所有子节点吗？');");

                //
                lbl_MenuID.Text = "";
                //lbl_MenuGroups.Text = "";

                //授权列表

                //权限集合
                //dtGroups For Menu
                DataTable dtGroups = GroupDal.GetGroupsForMenu();

                ddl_MenuGroups.Items.Add(new ListItem("sisadmin", "AA"));

                foreach (DataRow dr in dtGroups.Rows)
                {
                    ddl_MenuGroups.Items.Add(new ListItem(dr["GroupName"].ToString(), dr["GroupCode"].ToString()));
                }                

                //目录列表
                DataTable dt = KPI_MenuDal.GetDirectories();
                ddl_MenuParentID.Items.Add(new ListItem("", ""));

                foreach (DataRow dr in dt.Rows)
                {
                    ddl_MenuParentID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }


                //Treeview                
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定 Treeview Controls
        /// </summary>
        void BindGrid()
        {
            //ddl_PlantIsValid.Value = "1";
            this.TVWMenu.Nodes.Clear();

            //
            lbl_MenuID.Text = "";

            //
            //string GroupCode = ddl_MenuGroups.Value;

            strGroupSigle = ddl_MenuGroups.SelectedValue;

            //DataSet dsMenus = KPI_MenuDal.GetMenus("");
            //if (dsMenus == null)
            {
                dsMenus = KPI_MenuDal.GetMenus("");
            }

            TreeNode pNode = new TreeNode();

            AddTree(strGroupSigle, dsMenus, "", null);

            //this.TVWMenu.DataSource = pNode;
            
            //this.TVWMenu.Nodes.Add(pNode);

            this.TVWMenu.ExpandAll();

        }


        public void AddTree(string groupselected, DataSet ds, string parentid, TreeNode pNode)
        {
            DataView dv=new DataView(ds.Tables[0]);
            dv.RowFilter="MenuParentID= '"+parentid +"'";

            foreach(DataRowView row in dv)
            {
                TreeNode node=new TreeNode();

                string strParentID = row["MenuID"].ToString();

                node.Text = row["MenuName"].ToString();
                node.Value = strParentID;

                //图片
                string strGIF = row["MenuGIF"].ToString();
                node.ImageUrl = "../images/" + strGIF;

                //权限
                string groups = row["MenuGroups"].ToString();
                Regex re = new Regex(groupselected);
                Match m = re.Match(groups); // 在字符串中匹配
                if (m.Success)
                {
                    //输入匹配字符的位置
                    node.Checked = true;
                }

                node.Expanded=true;

                if (pNode == null)
                {
                    this.TVWMenu.Nodes.Add(node);
                }
                else
                {
                    pNode.ChildNodes.Add(node);
                }

                //递归添加子节点
                AddTree(groupselected, ds, strParentID, node);
            }
        }


        protected void TVWMenu_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            TreeNode tnode = e.Node;
            TreeNode pnode = tnode.Parent;

            bool check = tnode.Checked;
            
            //子节点
            if (tnode.ChildNodes.Count > 0)
            {
                SetChildNodeCheckStatus(tnode);
            }

            if (pnode!=null)
            {
                //父节点
                pnode.Checked = GetChildNodeCheckStatus(pnode);
            }

        }

        protected void SetChildNodeCheckStatus(TreeNode node)
        {
            TreeNodeCollection cnodes = node.ChildNodes;

            if (cnodes.Count > 0)
            {
                foreach (TreeNode tnode in cnodes)
                {
                    tnode.Checked = node.Checked;

                    if (tnode.ChildNodes.Count > 0)
                    {
                        SetChildNodeCheckStatus(tnode);
                    }
                }
            }
            
        }

        protected bool GetChildNodeCheckStatus(TreeNode node)
        {
            TreeNodeCollection cnodes = node.ChildNodes;
            
            bool check = false;

            foreach (TreeNode tnode in cnodes)
            {
                check = check||tnode.Checked;
            }   

            return true;

        }


        protected void TVWMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            string menuid = TVWMenu.SelectedNode.Value;

            KPI_MenuEntity sysMenu = KPI_MenuDal.GetEntity(menuid);

            if (sysMenu != null)
            {
                lbl_MenuID.Text = sysMenu.MenuID;
                ddl_MenuParentID.Value = sysMenu.MenuParentID;
                tbx_MenuCode.Text = sysMenu.MenuCode;
                tbx_MenuName.Text= sysMenu.MenuName;
                tbx_MenuDesc.Text = sysMenu.MenuDesc;
                rbl_MenuIsDisplay.SelectedValue = sysMenu.MenuIsDisplay.ToString();
                rbl_MenuType.SelectedValue = sysMenu.MenuType.ToString();
                tbx_MenuURL.Text = sysMenu.MenuURL;
                tbx_MenuGIF.Text = sysMenu.MenuGIF;
                rbl_MenuTarget.SelectedValue = sysMenu.MenuTarget.ToString();
                //lbl_MenuGroups.Text = sysMenu.MenuGroups;
                rbl_MenuIsValid.SelectedValue = sysMenu.MenuIsValid.ToString();
                tbx_MenuNote.Text = sysMenu.MenuNote;

                //弹出的框需要用到
                //strGroupCollections = sysMenu.MenuGroups;

                //SetGroupSelected(dtGroups, strGroupCollections);

            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="groups"></param>
        protected void SetGroupSelected(DataTable dt, string groups)
        {
            string[] estr = groups.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int k = estr.Length;
            if ( k<=0)
            {
                return;
            }

            int j = 0;           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strcode = dt.Rows[i]["GroupCode"].ToString();
                Regex re = new Regex(strcode);
                Match m = re.Match(groups); // 在字符串中匹配
                if (m.Success)
                {
                    //输入匹配字符的位置
                    dt.Rows[i]["GroupSelected"]="1";
                    j++;
                }

                if (j >= k)
                {
                    break;
                } 
            }

            return;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="groups"></param>
        protected string GetGroupSelected(DataTable dt)
        {
            string strcodes = "AA,";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["GroupSelected"].ToString() == "1")
                {
                    strcodes += dt.Rows[i]["GroupCode"].ToString() + ",";
                }
            }

            return strcodes;
        }

        /// <summary>
        /// 添加新的节点到菜单集合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuAdd_Click(object sender, EventArgs e)
        {
            //protected String _MenuID = null;
            //protected String _MenuParentID = null;
            //protected String _MenuCode = null;
            //protected String _MenuName = null;
            //protected String _MenuDesc = null;
            //protected int _MenuIsDisplay = int.MinValue;
            //protected int _MenuType = int.MinValue;
            //protected String _MenuURL = null;
            //protected String _MenuGIF = null;
            //protected int _MenuTarget = int.MinValue;
            //protected String _MenuGroups = null;
            //protected int _MenuIsValid = int.MinValue;
            //protected String _MenuNote = null;
            //protected String _MenuCreateTime = null;
            //protected String _MenuModifyTime = null;

            //strGroupCollections= GetGroupSelected(dtGroups);

            //lbl_MenuGroups.Text = strGroupCollections;

            string menuid = ""; // lbl_MenuID.Text.Trim();           
            string menuparentid = ddl_MenuParentID.Value;
            string menucode = tbx_MenuCode.Text.Trim();
            string menuname = tbx_MenuName.Text.Trim();
            string menudesc = tbx_MenuDesc.Text.Trim();
            int menuisdisplay = int.Parse(rbl_MenuIsDisplay.SelectedValue);
            int menuindex = 0;
            int menutype = int.Parse(rbl_MenuType.SelectedValue);
            string menuurl = tbx_MenuURL.Text.Trim();
            string menugif = tbx_MenuGIF.Text.Trim();
            int menutarget = int.Parse(rbl_MenuTarget.SelectedValue);

            string menugroups = "AA,";
            int menuisvalid = int.Parse(rbl_MenuIsValid.SelectedValue);
            string menunote = tbx_MenuNote.Text.Trim();
            string menucreatetime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            string menumodifytime = menucreatetime;

            if (menuid == "")
            {
                menuid = Guid.NewGuid().ToString();
            }

            KPI_MenuEntity sysMenu = new KPI_MenuEntity();
            sysMenu.MenuID = menuid;
            sysMenu.MenuParentID = menuparentid;
            sysMenu.MenuCode = menucode;
            sysMenu.MenuName = menuname;
            sysMenu.MenuDesc = menudesc;
            sysMenu.MenuIsDisplay = menuisdisplay;
            sysMenu.MenuIndex = menuindex;
            sysMenu.MenuType = menutype;
            sysMenu.MenuURL = menuurl;
            sysMenu.MenuGIF = menugif;
            sysMenu.MenuTarget = menutarget;
            sysMenu.MenuGroups = menugroups;
            sysMenu.MenuIsValid = menuisvalid;
            sysMenu.MenuNote = menunote;
            sysMenu.MenuCreateTime = menucreatetime;
            sysMenu.MenuModifyTime = menumodifytime;

            if (KPI_MenuDal.Insert(sysMenu))
            {
                MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");
            }

             BindGrid();

        }

        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            string menuid = lbl_MenuID.Text.Trim();

            if (menuid=="")
            {
                MessageBox.popupClientMessage(this.Page, "请选择节点！", "call();");
                
                return;
            }

            ////////////////////////////////////////////////////////////////////////

            //strGroupCollections = GetGroupSelected(dtGroups);

            //lbl_MenuGroups.Text = strGroupCollections;

            string menuparentid = ddl_MenuParentID.Value;
            string menucode = tbx_MenuCode.Text.Trim();
            string menuname = tbx_MenuName.Text.Trim();
            string menudesc = tbx_MenuDesc.Text.Trim();
            int menuisdisplay = int.Parse(rbl_MenuIsDisplay.SelectedValue);
            //int menuindex = 0;
            int menutype = int.Parse(rbl_MenuType.SelectedValue);
            string menuurl = tbx_MenuURL.Text.Trim();
            string menugif = tbx_MenuGIF.Text.Trim();
            int menutarget = int.Parse(rbl_MenuTarget.SelectedValue);
            //string menugroups = lbl_MenuGroups.Text.Trim();
            int menuisvalid = int.Parse(rbl_MenuIsValid.SelectedValue);
            string menunote = tbx_MenuNote.Text.Trim();
            //string menucreatetime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            string menumodifytime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");;

            KPI_MenuEntity sysMenu = new KPI_MenuEntity();
            sysMenu.MenuID = menuid;
            sysMenu.MenuParentID = menuparentid;
            sysMenu.MenuCode = menucode;
            sysMenu.MenuName = menuname;
            sysMenu.MenuDesc = menudesc;
            sysMenu.MenuIsDisplay = menuisdisplay;
            //sysMenu.MenuIndex = menuindex;
            sysMenu.MenuType = menutype;
            sysMenu.MenuURL = menuurl;
            sysMenu.MenuGIF = menugif;
            sysMenu.MenuTarget = menutarget;
            //sysMenu.MenuGroups = menugroups;
            sysMenu.MenuIsValid = menuisvalid;
            sysMenu.MenuNote = menunote;
            //sysMenu.MenuCreateTime = menucreatetime;
            sysMenu.MenuModifyTime = menumodifytime;

            if (KPI_MenuDal.Update(sysMenu))
            {
                MessageBox.popupClientMessage(this.Page, "修改成功！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "修改错误！", "call();");
            }

            BindGrid();

        }

        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            

        }

        protected void btnMenu_Click(object sender, EventArgs e)
        {
            //Response.Redirect("KPI_Menu.aspx");
        }

        protected void btnUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_User.aspx");

        }

        protected void btnGroup_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_Group.aspx");

        }

        protected void btnMenuGroups_Click(object sender, EventArgs e)
        {
            string strjs = "<script language=javascript>window.open('KPI_SubGroupSelect.aspx?mode=menu','newwindow','width=400,height=500')</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", strjs);
        }

        protected void ddl_MenuGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            string menugroup = ddl_MenuGroups.SelectedValue;

            //更新顺序和权限
            TreeNodeCollection cnodes = this.TVWMenu.Nodes;
            int menuindex = 0;

            if (menugroup == "AA")
            {                
                SetNodeIndex(cnodes, ref menuindex);

                MessageBox.popupClientMessage(this.Page, "目录树的顺序保存成功！", "call();");
            }
            else
            {
                SetNodeGroupAndIndex(cnodes, menugroup, ref menuindex);

                MessageBox.popupClientMessage(this.Page, "修改成功！", "call();");
            }

            return;

        }

        protected void btnDel_Click(object sender, ImageClickEventArgs e)
        {
            string menuid = lbl_MenuID.Text.Trim();

            if (menuid == "")
            {
                MessageBox.popupClientMessage(this.Page, "请选择节点！", "call();");

                return;
            }

            if (KPI_MenuDal.Delete(menuid))
            {
                MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
            }

            BindGrid();

        }

        protected void SetNodeIndex(TreeNodeCollection cnodes, ref int menuindex)
        {
            for (int i = 0; i < cnodes.Count; i++)
            {
                menuindex = menuindex + 1;

                //本节点
                KPI_MenuEntity mnEntity = new KPI_MenuEntity();
                mnEntity.MenuID = cnodes[i].Value;
                if (cnodes[i].Parent != null)
                {
                    mnEntity.MenuParentID = cnodes[i].Parent.Value;
                }

                mnEntity.MenuIndex = menuindex;                
                KPI_MenuDal.Update(mnEntity);

                //子节点
                SetNodeIndex(cnodes[i].ChildNodes, ref menuindex);
            }


        }

        protected void SetNodeGroupAndIndex(TreeNodeCollection cnodes, string menugroup, ref int menuindex)
        {
            //确认 static DataSet变量修改并保存。

            for (int i = 0; i < cnodes.Count; i++)
            {
                menuindex = menuindex + 1;

                //本节点
                KPI_MenuEntity mnEntity = new KPI_MenuEntity();
                mnEntity.MenuID = cnodes[i].Value;
                if (cnodes[i].Parent != null)
                {
                    mnEntity.MenuParentID = cnodes[i].Parent.Value;
                }
                mnEntity.MenuIndex = menuindex;

                //check
                bool check = cnodes[i].Checked;

                //group
                string groups = dsMenus.Tables[0].Select("MenuID='"+mnEntity.MenuID+"'")[0]["MenuGroups"].ToString();

                if (check)
                {
                    if(!groups.Contains(menugroup))
                    {
                        groups += menugroup + ",";
                    }
                //    Regex re = new Regex(menugroup);
                //    Match m = re.Match(groups); // 在字符串中匹配
                //    if (m.Success)
                //    {

                //    }                
                }
                else
                {
                    if (groups.Contains(menugroup))
                    {
                        groups = groups.Replace(menugroup + ",", "");
                    }
                }


                mnEntity.MenuGroups = groups;
                //
                KPI_MenuDal.Update(mnEntity);

                //子节点
                SetNodeGroupAndIndex(cnodes[i].ChildNodes, menugroup, ref menuindex);

            }


        }


        #region  TreeView的上下左右移动操作

        /// <summary>
        /// 左移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLeft_Click(object sender, ImageClickEventArgs e)
        {            
            if (TVWMenu.SelectedNode != null)
            {
                TreeNode sourceNode = TVWMenu.SelectedNode;
                int intDepth = TVWMenu.SelectedNode.Depth;

                 if (intDepth <= 0)
                {
                    //顶级节点无法左移
                    //------------
                }
                 else if (intDepth > 0)
                {
                    TreeNode tnParent = TVWMenu.SelectedNode.Parent;

                    if (tnParent.Parent == null)
                    {
                        int index = TVWMenu.Nodes.IndexOf(tnParent);
                        TVWMenu.Nodes.AddAt(index + 1, sourceNode);

                        //设置移动后要选择的节点
                        TVWMenu.Nodes[index + 1].Select();
                    }
                    else
                    {
                        int index = tnParent.Parent.ChildNodes.IndexOf(tnParent);
                        tnParent.Parent.ChildNodes.AddAt(index + 1, sourceNode);

                        //设置移动后要选择的节点
                        tnParent.Parent.ChildNodes[index + 1].Select();
                    }
                }
            } 

        }

        /// <summary>
        /// 右移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRight_Click(object sender, ImageClickEventArgs e)
        {
            if (TVWMenu.SelectedNode != null)
            {
                TreeNode sourceNode = TVWMenu.SelectedNode;
                //int intIndex = 
                int intDepth = TVWMenu.SelectedNode.Depth;

                if (intDepth <= 0)
                {
                    //顶级节点
                    int index = TVWMenu.Nodes.IndexOf(sourceNode);
                    if (index <= 0)
                    {
                        return;
                    }
                    else
                    {
                        int count = TVWMenu.Nodes[index - 1].ChildNodes.Count;
                        TVWMenu.Nodes[index - 1].ChildNodes.AddAt(count, sourceNode);

                        //设置移动后要选择的节点
                        TVWMenu.Nodes[index - 1].ChildNodes[count].Select();
                    }

                }
                else if (intDepth > 0)
                {
                    TreeNode tnParent = TVWMenu.SelectedNode.Parent;
                    int index = tnParent.ChildNodes.IndexOf(sourceNode);

                    if (index <= 0)
                    {
                        return;
                    }
                    else
                    {
                        int count = tnParent.ChildNodes[index - 1].ChildNodes.Count;
                        tnParent.ChildNodes[index - 1].ChildNodes.AddAt(count, sourceNode);

                        //设置移动后要选择的节点
                        tnParent.ChildNodes[index - 1].ChildNodes[count].Select();
                    }

                }
            } 

        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUp_Click(object sender, ImageClickEventArgs e)
        {
            if (TVWMenu.SelectedNode != null)
            {
                TreeNode sourceNode = TVWMenu.SelectedNode;
                int intIndex = TVWMenu.Nodes.IndexOf(TVWMenu.SelectedNode);

                if (intIndex > 0)
                {
                    TVWMenu.Nodes.AddAt(intIndex - 1, sourceNode);

                    //设置移动后要选择的节点
                    TVWMenu.Nodes[intIndex - 1].Select();
                }
                else if (intIndex != 0)
                {
                    // -1 
                    TreeNode tnParent = TVWMenu.SelectedNode.Parent;
                    int index = tnParent.ChildNodes.IndexOf(TVWMenu.SelectedNode);

                    foreach (TreeNode tnNode in tnParent.ChildNodes)
                    {
                        if (TVWMenu.SelectedNode.Value == tnNode.Value && index != 0)
                        {
                            TreeNode tnTemp = new TreeNode();
                            tnTemp = tnNode;
                            tnParent.ChildNodes.Remove(TVWMenu.SelectedNode);
                            tnParent.ChildNodes.AddAt(index - 1, tnTemp);

                            //设置移动后要选择的节点
                            tnParent.ChildNodes[index - 1].Select();

                            break;
                        }
                    }
                }
            } 

        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDown_Click(object sender, ImageClickEventArgs e)
        {
            if (TVWMenu.SelectedNode != null)
            {
                TreeNode sourceNode = TVWMenu.SelectedNode;
                int intCount = TVWMenu.Nodes.Count;

                if (TVWMenu.Nodes.IndexOf(TVWMenu.SelectedNode) < intCount - 1 && TVWMenu.Nodes.IndexOf(TVWMenu.SelectedNode) >= 0)
                {
                    int intIndex = TVWMenu.Nodes.IndexOf(TVWMenu.SelectedNode) + 1;
                    TVWMenu.Nodes.AddAt(intIndex, sourceNode);

                    //设置移动后要选择的节点
                    TVWMenu.Nodes[intIndex].Select();
                }
                else if (TVWMenu.Nodes.IndexOf(TVWMenu.SelectedNode) != intCount && TVWMenu.Nodes.IndexOf(TVWMenu.SelectedNode) < 0)
                {
                    TreeNode tnParent = TVWMenu.SelectedNode.Parent;
                    int nodeCount = tnParent.ChildNodes.Count;
                    TreeNode tnSelectedNode = new TreeNode();
                    bool count = false;
                    int index = tnParent.ChildNodes.IndexOf(TVWMenu.SelectedNode);
                    foreach (TreeNode tnNode in tnParent.ChildNodes)
                    {
                        if (TVWMenu.SelectedNode.Value == tnNode.Value)
                        {
                            count = true;
                            tnSelectedNode = tnNode;
                        }

                        if (TVWMenu.SelectedNode.Value != tnNode.Value && count == true)
                        {
                            tnParent.ChildNodes.Remove(TVWMenu.SelectedNode);
                            tnParent.ChildNodes.AddAt(index + 1, tnSelectedNode);

                            //设置移动后要选择的节点
                            if (index + 1 >= nodeCount)
                            {
                                tnParent.ChildNodes[nodeCount - 1].Select();
                            }
                            else
                            {
                                tnParent.ChildNodes[index + 1].Select();
                            }

                            break;
                        }
                    }

                }
            }
        }

        #endregion


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

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (dr["SelectX"].ToString().ToLower() == "x")
                    {
                        string menuid = Guid.NewGuid().ToString();
                        string menuparentid = lbl_MenuID.Text.Trim();
                        //string menucode = tbx_MenuCode.Text.Trim();
                        string menuname = dr["MenuName"].ToString().Trim();
                        string menudesc = dr["MenuDesc"].ToString().Trim();
                        int menuisdisplay = int.Parse(dr["MenuIsDisplay"].ToString().Trim());
                        int menuindex = int.Parse(dr["MenuIndex"].ToString().Trim());
                        int menutype = int.Parse(dr["MenuType"].ToString().Trim());
                        string menuurl = dr["MenuURL"].ToString().Trim();
                        string menugif = dr["MenuGIF"].ToString().Trim();
                        int menutarget = int.Parse(dr["MenuTarget"].ToString().Trim());
                        string menugroups = "AA,";
                        int menuisvalid = int.Parse(dr["MenuIsValid"].ToString().Trim());
                        string menunote = dr["MenuNote"].ToString().Trim();
                        string menucreatetime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                        string menumodifytime = menucreatetime;

                        KPI_MenuEntity sysMenu = new KPI_MenuEntity();
                        sysMenu.MenuID = menuid;
                        sysMenu.MenuParentID = menuparentid;
                        //sysMenu.MenuCode = menucode;
                        sysMenu.MenuName = menuname;
                        sysMenu.MenuDesc = menudesc;
                        sysMenu.MenuIsDisplay = menuisdisplay;
                        sysMenu.MenuIndex = menuindex;
                        sysMenu.MenuType = menutype;
                        sysMenu.MenuURL = menuurl;
                        sysMenu.MenuGIF = menugif;
                        sysMenu.MenuTarget = menutarget;
                        sysMenu.MenuGroups = menugroups;
                        sysMenu.MenuIsValid = menuisvalid;
                        sysMenu.MenuNote = menunote;
                        sysMenu.MenuCreateTime = menucreatetime;
                        sysMenu.MenuModifyTime = menumodifytime;

                        if (KPI_MenuDal.Insert(sysMenu))
                        {
                            nCreate += 1;
                        }

                    }
                }

                string strInfor = "菜单总数：{0}个, 创建成功：{1}个。";

                strInfor = string.Format(strInfor, nAll, nCreate);

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


        #endregion



    }
}
