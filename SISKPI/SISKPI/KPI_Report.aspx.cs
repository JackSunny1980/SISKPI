using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

using SIS.DataAccess;

namespace SISKPI
{
    public partial class KPI_Report : System.Web.UI.Page
    {
        
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
       
            //dsMenus = KPI_MenuDal.GetMenus("");        

            //TreeNode pNode = new TreeNode();

            //AddTree(strGroupSigle, dsMenus, "", null);

            TreeNode node = new TreeNode();

            node.Text = "报表集";
            node.Value = "1000";

            this.TVWMenu.Nodes.Add(node);


            //自定义报表 
            TreeNode znode = new TreeNode();
            znode.Text = "自定义报表";
            znode.Value = "KPI\\KPI_RDLCConfig.aspx";
            znode.Selected = true;
            node.ChildNodes.Add(znode);

            //
            DataTable dt = KPI_WebDal.GetDataTable();
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode cnode = new TreeNode();
                cnode.Text = dr["WebDesc"].ToString();
                if (dr["WebType"].ToString() == "0")
                {
                    //日报
                    cnode.Value = "KPIRDLC\\KPI_ECDay.aspx?webcode=" + dr["WebCode"].ToString();
                }
                else
                {
                    //月报
                    cnode.Value = "KPIRDLC\\KPI_ECMonth.aspx?webcode=" + dr["WebCode"].ToString();
                }

                node.ChildNodes.Add(cnode);
            }           

            this.TVWMenu.ExpandAll();

        }



        public void AddTree(DataSet ds, string parentid, TreeNode pNode)
        {
            //DataView dv = new DataView(ds.Tables[0]);
            //dv.RowFilter = "MenuParentID= '" + parentid + "'";

            //foreach (DataRowView row in dv)
            //{
            //    TreeNode node = new TreeNode();

            //    string strParentID = row["MenuID"].ToString();

            //    node.Text = row["MenuName"].ToString();
            //    node.Value = strParentID;

            //    //图片
            //    string strGIF = row["MenuGIF"].ToString();
            //    node.ImageUrl = "../images/" + strGIF;

            //    //权限
            //    string groups = row["MenuGroups"].ToString();
            //    Regex re = new Regex(groupselected);
            //    Match m = re.Match(groups); // 在字符串中匹配
            //    if (m.Success)
            //    {
            //        //输入匹配字符的位置
            //        node.Checked = true;
            //    }

            //    node.Expanded = true;

            //    if (pNode == null)
            //    {
            //        this.TVWMenu.Nodes.Add(node);
            //    }
            //    else
            //    {
            //        pNode.ChildNodes.Add(node);
            //    }

            //    //递归添加子节点
            //    AddTree(groupselected, ds, strParentID, node);
            //}
        }

        protected void TVWMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            string url = TVWMenu.SelectedValue;
            int nid = 0;

            if (!int.TryParse(url, out nid))
            {
                kpiReport.Attributes.Add("src", url);
            }

        }

    }
}