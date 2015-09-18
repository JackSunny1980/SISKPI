using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SIS.DBControl;
using SIS.Loger;
using System.Web.UI.HtmlControls;

using System.Data;
using SIS.DataAccess;

namespace SISKPI
{
    public partial class KPI_XLine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            } 
            
        }

        void BindGrid()
        {
            txt_XLineName.Text=txt_XLineName.Text.Trim();

            string condition = "";

            if (txt_XLineName.Text != "")
            {
                condition = " and XLineName like '%" + txt_XLineName.Text + "%'";
            }
            else
            {
                condition = "";
            }

            //
            //DataTable dt = OPM_XLineDal.GetXLineInfor(condition);

            //gvXLine.DataSource = dt;

            //gvXLine.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void gvXLine_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {    
                //Get the unique ID for this record
                //string appID = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "XLineID")); 

                HtmlInputHidden key = (HtmlInputHidden)e.Row.Cells[0].FindControl("xlineid");


                if ((HtmlInputButton)e.Row.Cells[5].FindControl("lbquery") != null)
                {
                    HtmlInputButton btn = (HtmlInputButton)e.Row.Cells[5].FindControl("lbquery");
                    btn.Attributes.Add("onclick", "javascript:XLineQuery('" + key.Value + "');");
                }


                if ((HtmlInputButton)e.Row.Cells[6].FindControl("lbconfig") != null)
                {
                    HtmlInputButton btn = (HtmlInputButton)e.Row.Cells[6].FindControl("lbconfig");
                    btn.Attributes.Add("onclick", "javascript:XLineConfig('" + key.Value + "');");
                }

                //编辑

                if ((LinkButton)e.Row.Cells[8].FindControl("lbdelete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.Cells[8].FindControl("lbdelete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                #region 嵌套gridview ，不在使用
                //////////////////////////////////////////////////////////////////////////////////////////////
                ////Master and  Detail
                //string condition = " and XLineID='" + appID + "'";

                //DataTable dt = OPM_XLineDal.GetXLineData(condition);

                ////Create a new GridView for displaying the expanded details
                //GridView gvDetail = new GridView();
                //gvDetail.DataSource = dt;
                //gvDetail.ID = "Detail" + e.Row.RowIndex.ToString();

                ////Since a gridview is being created for each row they each need a unique ID, so append the row index
                //gvDetail.AutoGenerateColumns = false;
                //gvDetail.CssClass = "subgridview";

                //gvDetail.RowDataBound += new GridViewRowEventHandler(gvDetail_RowDataBound);
                ////AddHandler(gv.RowDataBound, AddressOf grdOverLimitDetails_RowDataBound)

                ////Add a rowdatabound method for the new GridView
                ////Add fields to the expanded details GridView
                //BoundField bf1 =  new BoundField();
                //bf1.DataField = "XLineName";
                //bf1.HeaderText = "名称";
                //gvDetail.Columns.Add(bf1);

                //BoundField bf2 =  new BoundField();
                //bf2.DataField = "XLineEngunit";
                //bf2.HeaderText = "单位";
                //gvDetail.Columns.Add(bf2);

                //BoundField bf3 =  new BoundField();
                //bf3.DataField = "XLine30";
                //bf3.HeaderText = "30%";
                //gvDetail.Columns.Add(bf3);

                //BoundField bf4 = new BoundField();
                //bf4.DataField = "XLine40";
                //bf4.HeaderText = "40%";
                //gvDetail.Columns.Add(bf4);

                //BoundField bf5 = new BoundField();
                //bf5.DataField = "XLine50";
                //bf5.HeaderText = "50%";
                //gvDetail.Columns.Add(bf5);

                //BoundField bf6 = new BoundField();
                //bf6.DataField = "XLine60";
                //bf6.HeaderText = "60%";
                //gvDetail.Columns.Add(bf6);

                //BoundField bf7 = new BoundField();
                //bf7.DataField = "XLine70";
                //bf7.HeaderText = "70%";
                //gvDetail.Columns.Add(bf7);

                //BoundField bf75 = new BoundField();
                //bf75.DataField = "XLine75";
                //bf75.HeaderText = "75%";
                //gvDetail.Columns.Add(bf75);

                //BoundField bf8 = new BoundField();
                //bf8.DataField = "XLine80";
                //bf8.HeaderText = "80%";
                //gvDetail.Columns.Add(bf8);

                //BoundField bf9 = new BoundField();
                //bf9.DataField = "XLine90";
                //bf9.HeaderText = "90%";
                //gvDetail.Columns.Add(bf9);

                //BoundField bf10 = new BoundField();
                //bf10.DataField = "XLine100";
                //bf10.HeaderText = "100%";
                //gvDetail.Columns.Add(bf10);

                //BoundField bf105 = new BoundField();
                //bf105.DataField = "XLine105";
                //bf105.HeaderText = "105%";                
                //gvDetail.Columns.Add(bf105);

                ////宽度
                //Unit an = new Unit(120, UnitType.Pixel);
                //gvDetail.Columns[0].ItemStyle.Width = an;
                //gvDetail.Columns[0].ItemStyle.HorizontalAlign =  HorizontalAlign.Left;
                //Unit ae = new Unit(40, UnitType.Pixel);
                //gvDetail.Columns[1].ItemStyle.Width = ae;
                //gvDetail.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                //Unit a1 = new Unit(8, UnitType.Percentage);
                //gvDetail.Columns[2].ItemStyle.Width = a1;
                //gvDetail.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                //gvDetail.Columns[3].ItemStyle.Width = a1;
                //gvDetail.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                //gvDetail.Columns[4].ItemStyle.Width = a1;
                //gvDetail.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                //gvDetail.Columns[5].ItemStyle.Width = a1;
                //gvDetail.Columns[5].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                //gvDetail.Columns[6].ItemStyle.Width = a1;
                //gvDetail.Columns[6].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                //gvDetail.Columns[7].ItemStyle.Width = a1;
                //gvDetail.Columns[7].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                //gvDetail.Columns[8].ItemStyle.Width = a1;
                //gvDetail.Columns[8].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                //gvDetail.Columns[9].ItemStyle.Width = a1;
                //gvDetail.Columns[9].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                //gvDetail.Columns[10].ItemStyle.Width = a1;
                //gvDetail.Columns[10].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                //gvDetail.Columns[11].ItemStyle.Width = a1;
                //gvDetail.Columns[11].ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                ////对齐

                ////Bind the expanded details GridView to its datasource
                //gvDetail.DataBind();

                //////////////////////////////////////////////////////////////////////////////////////////////
                ////DirectCast(e.Row.Parent, Table)
                //Table tbl = (Table)e.Row.Parent;

                //GridViewRow tr = new GridViewRow(e.Row.RowIndex + 1, -1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
                //tr.CssClass = "hidden";
                //TableCell tc = new TableCell();
                //tc.ColumnSpan = gvXLine.Columns.Count;
                //tc.BorderStyle = BorderStyle.None;
                //tc.BackColor = System.Drawing.Color.AliceBlue;

                //tc.Controls.Add(gvDetail);  //Add the expanded details GridView to the newly-created cell
                //tr.Cells.Add(tc);   //Add the newly-created cell to the newly-created row
                //tbl.Rows.Add(tr);   //Add the newly-ccreated row to the main GridView  



                //////////////////////////////////////////////////////////////////////////////////////////////
                ////Create the show/hide button which will be displayed on each row of the main GridView
                
                //System.Web.UI.WebControls.Image btngs= new System.Web.UI.WebControls.Image();
                //btngs.ID = "btnDetail";
                //btngs.ImageUrl = "../imgs/detail.png";
                ////'Adds the javascript function to the show/hide button, passing the row to be toggled as a parameter
                ////Add the expanded details row after each record in the main GridView
                //btngs.Attributes.Add("onclick", "javascript: gvrowtoggle(" + (e.Row.RowIndex + e.Row.RowIndex + 2).ToString() + ")");


                //e.Row.Cells[0].Controls.Add(btngs);  //Add the show/hide button to the main GridView row
                                
#endregion 

                ////////////////////////////////////////////////////////////////////////////////////////////
                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
                
            }
        }

        protected void gvXLine_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string xlineid = e.CommandArgument.ToString();

            if (e.CommandName == "delete")
            {
                //删除时先判断曲线是否被使用
                string sql = "";
                sql = "select count(1) from Filter_TagMain where XLineID='{0}'";
                sql = string.Format(sql, xlineid);
                if(int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString())>0)
                {
                    MessageBox.popupClientMessage(this.Page, "曲线已经被使用,不能删除！", "call();");
                    return;
                }

                sql = "delete from OPM_XLine where XLineID ='{0}'";
                sql = string.Format(sql, xlineid);
                if (DBAccess.GetRelation().ExecuteNonQuery(sql) < 1)
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                    return;
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！","call();");

                    BindGrid();
                }

            }
        }

        protected void gvXLine_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvXLine.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvXLine_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvXLine.EditIndex = -1;
            BindGrid();
        }
        
        protected void gvXLine_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            HtmlInputHidden key = (HtmlInputHidden)gvXLine.Rows[e.RowIndex].Cells[0].FindControl("xlineid");

            string sID = key.Value.Trim();
            string sName = ((TextBox)(gvXLine.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim().ToUpper();
            string sDesc = ((TextBox)(gvXLine.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim().ToUpper();
            string sEngunit = ((TextBox)(gvXLine.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            string sNote = ((TextBox)(gvXLine.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
           
            string msg = "";
            if (sName == "")
            {
                msg += "曲线名称不能为空！\r\n";
            }

            if (msg != "")
            {
                MessageBox.popupClientMessage(this.Page, msg);
                return;
            }

            //通过曲线名称
            //判断曲线是否存在
            string sql = "select count(1) from OPM_XLine where XLineID<>'{0}' and XLineName='{1}'";
            sql = string.Format(sql, sID, sName);

            if (int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0)
            {
                MessageBox.popupClientMessage(this.Page, "曲线已经存在，请更改名称");
                return;
            }

            //if (!OPM_XLineDal.UpdateXLine(sID, sName, sDesc, sEngunit, sNote))
            //{
            //    MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
            //}
            //else
            //{
            //    MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

            //    gvXLine.EditIndex = -1;

            //    BindGrid();
            //}                 


        }      


        protected void btnAddXline_Click(object sender, EventArgs e)
        {
            string url = "OPM_SubXConfig.aspx?opr=add"; // call();

            Response.Redirect(url);
      }

        protected void btnBatchPro_Click(object sender, EventArgs e)
        {
            Response.Redirect("OPM_SubXBatch.aspx");
        }


    }
}
