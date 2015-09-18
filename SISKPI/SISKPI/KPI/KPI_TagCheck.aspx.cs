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

namespace SISKPI
{
    public partial class KPI_TagCheck : System.Web.UI.Page
    {
        private static List<string> lerror = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbxInfor.Items.Clear();
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
           // Response.Redirect("KPI_ECTagConfig.aspx");
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            lbxInfor.Items.Clear();

            lerror.Clear();
            
            //////////////////////////////////////////////////////////////////////////////
            //指标
            Dictionary<string, double> dicTags = new Dictionary<string,double>();

            //////////////////////////////////////////////////////////////////////////////
            //经济指标
            List<ECTagEntity> lts = ECTagDal.GetAllEntity();

            lbxInfor.Items.Add("查询所有的经济指标!" );

            foreach (ECTagEntity ltone in lts)
            {
                //lbxInfor.Items.Add("开始校验:" + ltone.ECCode + "--" + ltone.ECName );
                
                //有就不变，没有就添加；
                string strTagCode = "'" + ltone.ECCode.ToUpper().Trim() + "'";
                dicTags[strTagCode] = 0.0;

                //校验计算公式
                if (cbxCheckForEcTag.Items[0].Selected && (ltone.ECCalcExp.Replace("'", "''").Length - ltone.ECCalcExp.Length) % 2 != 0)
                {
                    lbxInfor.Items.Add(ltone.ECCode + "--" + ltone.ECName + ": 计算公式解析失败! "+ ltone.ECCalcExp);

                    lerror.Add(ltone.ECCalcExp);
                }

                /////////////////////////////////////////////////////////////////
                //校验Xline曲线
                if (cbxCheckForEcTag.Items[1].Selected && !ECTagDal.CheckEntityForXline(ltone))
                {
                    //相同X不能存在。

                    lbxInfor.Items.Add(ltone.ECCode + "--" + ltone.ECName + ": 曲线解析失败! "+ ltone.ECXLineType.ToString() + "--" + ltone.ECXLineXYZ);

                    lerror.Add(ltone.ECXLineXYZ);
                }

                ////////////////////////////////////////////////////////////////
                //校验ScoreExp
                if (cbxCheckForEcTag.Items[2].Selected && !ECTagDal.CheckEntityForScore(ltone))
                {
                    lbxInfor.Items.Add(ltone.ECCode + "--" + ltone.ECName + ": 得分计算解析失败! "+ ltone.ECScoreExp);

                    lerror.Add(ltone.ECScoreExp);

                }


                ////////////////////////////////////////////////////////////////
                //校验Optimal
                if (cbxCheckForEcTag.Items[2].Selected && !ECTagDal.CheckEntityForOptimal(ltone))
                {
                    lbxInfor.Items.Add(ltone.ECCode + "--" + ltone.ECName + ": 最优区间数量不等于1! " + ltone.ECScoreExp);

                    lerror.Add(ltone.ECScoreExp);

                }
            }

            //////////////////////////////////////////////////////////
            //实时指标
            List<KPI_RealTagEntity> rls = KPI_RealTagDal.GetAllEntity();
            
            lbxInfor.Items.Add("查询所有的实时指标!" );

            foreach (KPI_RealTagEntity ltone in rls)
            {
                //有就不变，没有就添加；
                string strTagCode = "'" + ltone.RealCode.ToUpper().Trim() + "'";
                dicTags[strTagCode] = 0.0;

                if (cbxCheckForRealTag.Items[0].Selected)
                {
                    //lbxInfor.Items.Add("开始校验:" + ltone.RealCode + "--" + ltone.RealDesc);

                    string strtag = ltone.RealCode;
                    if (!DBAccess.GetRealTime().ExistPoint(strtag))
                    {
                        lbxInfor.Items.Add(ltone.RealCode + "--" + ltone.RealDesc + ": 不存在! ");

                        lerror.Add(ltone.RealCode);
                    }
                }

            }

            //////////////////////////////////////////////
            //所有指标
            lbxInfor.Items.Add("校验经济指标的标签引用!");

            foreach (ECTagEntity ltone in lts)
            {
                //lbxInfor.Items.Add("开始校验:" + ltone.ECCode + "--" + ltone.ECName );
                
                ////////////////////////////////////////////////////////////////
                //校验Calc Tag
                Dictionary<String, double> dic1 = new Dictionary<String, double>();
                string expression = ltone.ECCalcExp.Trim();
                if (expression == "")
                {
                    lbxInfor.Items.Add(ltone.ECCode + "--" + ltone.ECName + " 计算表达式为空!");
                    
                    continue;
                }
				ExpDone parser = new ExpDone();
				if (parser.ExpEvaluate(expression, ref dic1) != 0)
                {
                    lbxInfor.Items.Add(ltone.ECCode + "--" + ltone.ECName + " 指标解析错误:" +ltone.ECCalcExp);

                    lerror.Add(ltone.ECCalcExp);
                    
                    continue;
                }

                foreach(KeyValuePair<string, double> kvp in dic1)
                {
                    string tagcode = kvp.Key.ToUpper().Trim();
                    if (!dicTags.ContainsKey(tagcode))
                    {
                        lbxInfor.Items.Add(ltone.ECCode + "--" + ltone.ECName + ": 的标签点引用失败! " + kvp.Key);
                        
                        lerror.Add(kvp.Key);
                    }
                }

            }
        }

        protected void btnExportInfor_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (ListItem lt in lbxInfor.Items)
            {
                sb.Append(lt.Text);
                sb.Append("\r\n");
            }
                        
            Response.Clear();               //清空输出
            
            Response.Buffer = true;

            Response.AppendHeader("Content-Disposition", "attachment;filename=infor.txt");

            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312"); //设置输出流为简体中文

            ContentType = "text/plain";//设置输出文件类型为txt文件。 

            this.EnableViewState = false;

            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);

            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);


            Response.Write(sb.ToString());

            Response.End();


        }

        protected void btnExportError_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (string error in lerror)
            {
                sb.Append(error);
                sb.Append("\r\n");
            }
            
            Response.Clear();               //清空输出

            Response.Buffer = true;

            Response.AppendHeader("Content-Disposition", "attachment;filename=error.txt");

            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文

            ContentType = "text/plain";//设置输出文件类型为txt文件。 

            this.EnableViewState = false;

            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);

            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);


            Response.Write(sb.ToString());

            Response.End();


        }

    }
}
