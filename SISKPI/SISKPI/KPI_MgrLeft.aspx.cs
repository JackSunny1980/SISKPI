using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

namespace SISKPI
{
    public partial class KPI_MgrLeft : System.Web.UI.Page
    {
        //private static DataSet dsMenus = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //目录列表
                BindGrid();

            }
        }


        /// <summary>
        /// 绑定 Treeview Controls
        /// </summary>
        void BindGrid()
        {
            this.TVWMenu.Nodes.Clear();

            DataSet dsParent = new DataSet();
            dsParent.ReadXml(Server.MapPath("~/XMLMenu/RootMenu.xml"));
            DataSet dsChild = new DataSet();
            dsChild.ReadXml(Server.MapPath("~/XMLMenu/SubMenu.xml"));

            DataTable dtbl = dsParent.Tables[0];

            foreach (DataRow drow in dtbl.Rows)
            {
                TreeNode node = new TreeNode();

                node.Text = drow["Name"].ToString();
                node.Value = drow["ID"].ToString();

                AddTree(dsChild, drow["ID"].ToString(), node);

                this.TVWMenu.Nodes.Add(node);
            }

            this.TVWMenu.ExpandAll();
        }



        public void AddTree(DataSet ds, string parentid, TreeNode pNode)
        {
            DataView dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "ParentID= '" + parentid + "'";

            foreach (DataRowView drow in dv)
            {
                TreeNode node = new TreeNode();

                node.Text = drow["Name"].ToString();
                node.Value = drow["URL"].ToString();
                node.NavigateUrl = drow["URL"].ToString();
                node.Target = "IframeMgrRight";

                pNode.ChildNodes.Add(node);

                //递归添加子节点
                //AddTree(ds, drow["ID"].ToString(), node);

            }
        }

        //protected void TVWMenu_SelectedNodeChanged(object sender, EventArgs e)
        //{
        //    string url = TVWMenu.SelectedValue;
        //    int nid = 0;

        //    if (!int.TryParse(url, out nid))
        //    {
        //        //kpiReport.Attributes.Add("src", url);
        //    }

        //}

    }
}