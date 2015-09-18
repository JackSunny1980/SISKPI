using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Data;
using System.Linq;

using SIS.DBControl;
using SIS.DataEntity;

namespace SIS.DataAccess
{
    public class KPI_XLineDal:DalBase<KPI_XLineEntity>
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xlineid"></param>
        /// <param name="num"></param>
        /// <param name="dPE"></param>
        /// <returns></returns>
        public static double[] GetXLineCoefs(List<KPI_XLineEntity> xlines, 
            int num, int xlinetype, int xlinegettype, double dXBase, double dYBase, 
            string sGroup, int nMonth)
        {
            //nMonth

            double[] AA = new double[num];

            //
            if (xlinetype == 0)
            {
                //忽略其它参数, gettype, xbase, ybase
                AA[0] = xlines[0].XLineValue;
            }
            else if (xlinetype == 1)
            {
                if (num >= 1)
                {
                    AA[0] = GetInterpolateValueX1(xlines, "a1", xlinegettype, dXBase);
                }
                 
                if (num >= 2)
                {
                    AA[1] = GetInterpolateValueX1(xlines, "a2", xlinegettype, dXBase);
                }

                if (num >= 3)
                {
                    AA[2] = GetInterpolateValueX1(xlines, "a3", xlinegettype, dXBase);
                }

                if (num >= 4)
                {
                    AA[3] = GetInterpolateValueX1(xlines, "a4", xlinegettype, dXBase);
                }

                if (num >= 5)
                {
                    AA[4] = GetInterpolateValueX1(xlines, "a5", xlinegettype, dXBase);
                }

                if (num >= 6)
                {
                    AA[5] = GetInterpolateValueX1(xlines, "a6", xlinegettype, dXBase);
                }


                if (num >= 7)
                {
                    AA[6] = GetInterpolateValueX1(xlines, "a7", xlinegettype, dXBase);
                }


                if (num >= 8)
                {
                    AA[7] = GetInterpolateValueX1(xlines, "a8", xlinegettype, dXBase);
                }
                
            }
            else if (xlinetype == 2)
            {
                //二维
                AA[0] = GetInterpolateValueXY(xlines, "a1", xlinegettype, dXBase, dYBase);
            }
            //else if (xlinetype == 3)
            //{
            //    //三维
            //    //AA[0] = GetInterpolateValueXY(xlines, "a1", xlinegettype, dXBase, dYBase);
            //}
            else if (xlinetype == 4)
            {
                
                //XLineMonth在生成 XLine时 添加了,

                //从该限定的曲线中 查找符合  月份的曲线集合
                string smonth = ","+nMonth.ToString()+",";

                var xlresult = (from kpi in xlines
                                where kpi.XLineMonth.Contains(smonth)
                                orderby kpi.XLineCoef
                                select kpi).ToList();

                AA = GetCurveCoefs(xlresult, num, xlinetype, xlinegettype, dXBase, dYBase);
            }
            
            return AA;
        }

        public static double[] GetCurveCoefs(List<KPI_XLineEntity> xlines,
            int num, int xlinetype, int xlinegettype, double dXBase, double dYBase)
        {
            //nMonth

            double[] AA = new double[num];

            //
            if (xlinetype == 0)
            {
                //忽略其它参数, gettype, xbase, ybase
                AA[0] = xlines[0].XLineValue;
            }
            else if (xlinetype == 1)
            {
                if (num >= 1)
                {
                    AA[0] = GetInterpolateValueX1(xlines, "a1", xlinegettype, dXBase);
                }

                if (num >= 2)
                {
                    AA[1] = GetInterpolateValueX1(xlines, "a2", xlinegettype, dXBase);
                }

                if (num >= 3)
                {
                    AA[2] = GetInterpolateValueX1(xlines, "a3", xlinegettype, dXBase);
                }

                if (num >= 4)
                {
                    AA[3] = GetInterpolateValueX1(xlines, "a4", xlinegettype, dXBase);
                }

                if (num >= 5)
                {
                    AA[4] = GetInterpolateValueX1(xlines, "a5", xlinegettype, dXBase);
                }

                if (num >= 6)
                {
                    AA[5] = GetInterpolateValueX1(xlines, "a6", xlinegettype, dXBase);
                }


                if (num >= 7)
                {
                    AA[6] = GetInterpolateValueX1(xlines, "a7", xlinegettype, dXBase);
                }


                if (num >= 8)
                {
                    AA[7] = GetInterpolateValueX1(xlines, "a8", xlinegettype, dXBase);
                }

            }
            else if (xlinetype == 2)
            {
                //二维
                AA[0] = GetInterpolateValueXY(xlines, "a1", xlinegettype, dXBase, dYBase);
            }

            return AA;
        }

        /// <summary>
        /// 一维数组一元线性取值
        /// </summary>
        /// <param name="xLineID">设计曲线主键</param>
        /// <returns></returns>
        public static double GetInterpolateValueX1(List<KPI_XLineEntity> xlines, string coef, int xlinegettype, double dXBase)
        {
            double d = double.MinValue;  

            try
            {    
                var kpiresult = from kpi in xlines
                                where (kpi.XLineCoef == coef)
                                //orderby kpi.XLineX
                                select kpi;

                int n=kpiresult.Count();

                if (n <= 0)
                {
                    return d;
                }
                else if (n == 1)
                {
                    d = kpiresult.ElementAt(0).XLineValue;
                    return d;
                }

                double [,] xlineArray = new double [2,n];
                for (int i = 0; i < n; i++)
                {
                    xlineArray[0, i] = kpiresult.ElementAt(i).XLineX;
                    xlineArray[1, i] = kpiresult.ElementAt(i).XLineValue;
                }

                //根据负荷和设计曲线，得到负荷对应的结果
                d = GetX(xlineArray, xlinegettype, dXBase);

                
            }
            catch (Exception ex)
            {
                string strex = ex.ToString();
            }

            return d;
        } 
        
        
        /// <summary>
        /// 二维数组一元线性取值
        /// </summary>
        /// <param name="xLineID">设计曲线主键</param>
        /// <returns></returns>
        public static double GetInterpolateValueXY(List<KPI_XLineEntity> xlines, string coef, int xlinegettype, double dXBase, double dYBase)
        {
            double d = double.MinValue;            

            try
            {
                var kpiresult = from kpi in xlines
                                where (kpi.XLineCoef == coef)
                                //orderby kpi.XLineX, kpi.XLineY
                                select kpi;

                /////////////////////////////////////////////////////////////////////////////////////////
                //获得数组大小
                //行的数量
                var YAxies = (from kpi in kpiresult select kpi.XLineY).Distinct().ToList();

                int m = YAxies.Count;
                if (m <= 0)
                {
                    return d;
                }

                //列的数量
                int n = kpiresult.Count() / m;

                //获得行基准值、列的基准值、对应数值
                double[] X_Values = new double[n];
                double[] Y_Values = new double[m];
                double[,] XY_Values = new double[m,n];

                for (int i = 0; i < m; i++)
                {
                    Y_Values[i] = kpiresult.ElementAt(i*(n-1) + i).XLineY;

                    for (int j = 0; j < n; j++)
                    {
                        XY_Values[i, j] = kpiresult.ElementAt(i * n + j).XLineValue;

                        if (i == 0)
                        {
                            X_Values[j] = kpiresult.ElementAt(j).XLineX;
                        }
                    }
                }

                /////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////

                if (m == 1 && n == 1)
                {
                    //一行一列
                    d= XY_Values[0, 0];
                    return d;
                }
                else if (m == 1 && n > 1)
                {
                    //一行多列
                    double[,] xlineArray = new double[2, n];
                    for (int i = 0; i < n; i++)
                    {
                        xlineArray[0, i] = X_Values[i];
                        xlineArray[1, i] = XY_Values[0, i];
                    }

                    d = GetX(xlineArray, xlinegettype, dXBase);                 

                }
                else if (m > 1 && n ==1)
                {
                    //多行一列
                    //转置一下通过直接插值Y数组获得
                    double[,] xlineArray = new double[2, m];
                    for (int i = 0; i < m; i++)
                    {
                        xlineArray[0, i] = Y_Values[i];
                        xlineArray[1, i] = XY_Values[i, 0];
                    }

                    d = GetX(xlineArray, xlinegettype, dXBase);
                }
                else
                {
                    //多行多列
                    //根据数组,先获得每行的数据插值结果。
                    double [] Y_Base = new double[m];
                    for (int i = 0; i < m; i++)
                    {
                        double[,] xlineArrayY = new double[2, n];
                        for (int j = 0; j < n; j++)
                        {
                            xlineArrayY[0, j] = X_Values[j];
                            xlineArrayY[1, j] = XY_Values[i, j];
                        }

                        Y_Base[i] = GetX(xlineArrayY, xlinegettype, dXBase);
                    }
                    
                    //在根据列基准、行插值结果获得最终的结果
                    double[,] xlineArray = new double[2, m];
                    for (int j = 0; j < m; j++)
                    {
                        xlineArray[0, j] = Y_Values[j];
                        xlineArray[1, j] = Y_Base[j];
                    }

                    d = GetX(xlineArray, xlinegettype, dYBase);
                    
                }
               
            }
            catch (Exception ex)
            {
                string strex = ex.ToString();
            }

            return d;
        }

        /// <summary>
        ///  得到对应的插值数据
        /// </summary>
        /// <param name="XLineID">设计曲线主键</param>
        /// <param name="fuheval">负荷值</param>
        /// <param name="xy1">输出前点</param>
        /// <param name="xy2">输出后点</param>
        public static double GetX(double[,] xlineArray, int xlinegettype, double dXBase)
        {
            //xlineArray: 2行,n列
            double d = double.MinValue;

            string xy1 = "";
            string xy2 = "";
            
            //列数问题
            int n = xlineArray.GetLength(1);

            if (n <= 0)
            {
            }
            else if (n == 1)
            {
                xy1 = xlineArray[0, 0].ToString() + "," + xlineArray[1, 0].ToString();
                xy2 = xy1;
            }
            else
            {
                int i=0;
                for (i= 0; i < n; i++)
                {
                    if (dXBase <= xlineArray[0, i])
                    {
                        break;
                    }
                }

                if (i <= 0)
                {
                    xy1 = xlineArray[0, 0].ToString() + "," + xlineArray[1, 0].ToString();
                    xy2 = xlineArray[0, 1].ToString() + "," + xlineArray[1, 1].ToString();
                }
                else if (i < n)
                {
                    xy1 = xlineArray[0, i-1].ToString() + "," + xlineArray[1, i-1].ToString();
                    xy2 = xlineArray[0, i].ToString() + "," + xlineArray[1, i].ToString();
                }
                else if(i>=n)
                {
                    xy1 = xlineArray[0, n - 2].ToString() + "," + xlineArray[1, n - 2].ToString();
                    xy2 = xlineArray[0, n - 1].ToString() + "," + xlineArray[1, n - 1].ToString();
                }

            }

            if (xy1 == "" || xy2 == "")
            {
                return d;
            }
            else
            {
                double x1 = double.Parse(xy1.Split(',')[0]);
                double y1 = double.Parse(xy1.Split(',')[1]);
                double x2 = double.Parse(xy2.Split(',')[0]);
                double y2 = double.Parse(xy2.Split(',')[1]);

                //根据插值方法获得对应值
                if (xlinegettype == -1)
                {
                    d = y1;
                }
                else if (xlinegettype == 0)
                {
                    d = y1 + (y2 - y1) * (dXBase - x1) / (x2 - x1);
                }
                else
                {
                    d = y2;
                }
            }
            
            return d;
        }


        /// <summary>
        ///  得到对应的设计曲线最近点
        /// </summary>
        /// <param name="XLineID">设计曲线主键</param>
        /// <param name="fuheval">负荷值</param>
        /// <param name="xy1">输出前点</param>
        /// <param name="xy2">输出后点</param>
        public static void GetXY(List<KPI_XLineEntity> xlines, string coef, 
                                 double dXBase, ref string xy1, ref string xy2, 
                                    double dYBase,ref string xy3, ref string xy4)
        {
            xy1 = "";
            xy2 = "";
            xy3 = "";
            xy4 = "";


            //
            //if (m == 1)
            //{
            //    xy1 = kpiresult.ElementAt(0).XLineX.ToString() + "," + kpiresult.ElementAt(0).XLineValue.ToString();
            //    xy2 = xy1;
            //}

            ////else if (kpiresult.Count() == 1)
            ////{
            ////    xy1 = kpiresult.ElementAt(0).XLineX.ToString() + "," + kpiresult.ElementAt(0).XLineValue.ToString();
            ////    xy2 = xy1;
            ////}
            //else
            //{
            //    int m = kpiresult.Count();
            //    for (int i = 0; i < kpiresult.Count(); i++)
            //    {
            //        if (dXBase <= kpiresult.ElementAt(i).XLineX)
            //        {
            //            m = i;
            //        }
            //    }

            //    if (m <= 0)
            //    {
            //        xy1 = kpiresult.ElementAt(0).XLineX.ToString() + "," + kpiresult.ElementAt(0).XLineValue.ToString();
            //        xy2 = xy1;
            //    }
            //    else if (m < kpiresult.Count())
            //    {
            //        xy1 = kpiresult.ElementAt(m - 1).XLineX.ToString() + "," + kpiresult.ElementAt(m - 1).XLineValue.ToString();
            //        xy2 = kpiresult.ElementAt(m).XLineX.ToString() + "," + kpiresult.ElementAt(m).XLineValue.ToString();
            //    }
            //    else if (m == kpiresult.Count())
            //    {
            //        xy1 = kpiresult.ElementAt(m - 1).XLineX.ToString() + "," + kpiresult.ElementAt(m - 1).XLineValue.ToString();
            //        xy2 = xy1;
            //    }

            //}

            
            ///

            

            return;
        }


        #region 放弃.不使用

        //        //线性回归方程-计算系数
        //        [DllImport("RegressDLL.dll")]
        //        static extern void Regress(double[] x, double[] y, int len, int pow, double[] result);

        //        /// <summary>
        //        /// 删除区间
        //        /// </summary>
        //        /// <param name="XLineID">主键</param>
        //        /// <returns></returns>
        //        public static bool DeleteXLine(string XLineID)
        //        {
        //            string sql = "delete from KPI_XLine where XLineID='{0}'";
        //            sql = string.Format(sql, XLineID);

        //            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        //        }

        //        /// <summary>
        //        /// 删除某个系数
        //        /// </summary>
        //        /// <param name="XLineID">主键</param>
        //        /// <returns></returns>
        //        public static bool DeleteXLineAA(string XLineID, string XLineType)
        //        {
        //            string sql = "delete from KPI_XLine where XLineID='{0}' and XLineType='{1}'";
        //            sql = string.Format(sql, XLineID, XLineType);

        //            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        //        }

        //        /// <summary>
        //        /// 得到设计曲线列表
        //        /// </summary>
        //        /// <returns></returns>
        //        public static DataTable GetXLines()
        //        {
        //            string sql = "select distinct XLineID[ID], XLineName[Name] from KPI_XLine order by XLineName";

        //            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        //        }


        //        /// <summary>
        //        /// 得到设计曲线列表
        //        /// </summary>
        //        /// <returns></returns>
        //        public static bool GetXLineList(string xlineid, out string xlinetype, out string xlinegettype, out DataTable dtnew, out double dout)
        //        {
        //            //XLineID, XLineName, XLineEngunit, XLineGetType, 
        //            string sql = @"select XLineID, XLineType, XLineGetType, XLineCoeType, XLineXYZ
        //                            from KPI_XLine 
        //                            where XLineID='{0}'
        //                            order by XLineCoeType";

        //            sql = string.Format(sql, xlineid);

        //            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

        //            dtnew = new DataTable();

        //            if (dt.Rows.Count > 0)
        //            {
        //                xlinetype = dt.Rows[0]["XLineType"].ToString();
        //                xlinegettype = dt.Rows[0]["XLineGetType"].ToString();

        //                string xlinexyz = dt.Rows[0]["XLineXYZ"].ToString();

        //                GetXYZ(xlinetype, xlinexyz, out dtnew, out dout);

        //            }
        //            else
        //            {

        //                xlinetype = "0";
        //                xlinegettype = "0";
        //                dout = 0;
        //            }

        //            return true;
        //        }

        //        public static void GetXYZ(string xlinetype, string xlinexyz, out DataTable dtnew, out double dout)
        //        {
        //            //解析
        //            //XLineType==0,定值;XLineXYZ=32;
        //            //XLineType==1,一维;XLineXYZ=175,350;25,26;26,27;
        //            //XLineType==2,一维;XLineXYZ=175,350;-30,0,30;-98;-87;-70;
        //            //XLineType==3,三维;暂不适用

        //            string[] rows = xlinexyz.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

        //            if (rows.Length == 0)
        //            {
        //                dtnew = null;
        //                dout = 0;
        //                return;
        //            }

        //            dtnew = new DataTable();
        //            dout = 0;

        //            if (xlinetype == "0")
        //            {
        //                dtnew = null;
        //                dout = double.Parse(rows[0].ToString());

        //                return;
        //            }
        //            else if (xlinetype == "1")
        //            {
        //                if (rows.Length < 2)
        //                {
        //                    dtnew = null;
        //                    dout = 0;
        //                    return;
        //                }

        //                int ncoefs = rows.Length;
        //                string[] columns = rows[0].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        //                //生成DataTable表的列。
        //                dtnew.Columns.Add("aa");
        //                for (int i = 0; i < columns.Length; i++)
        //                {
        //                    dtnew.Columns.Add(i.ToString());
        //                }

        //                //添加行。
        //                for (int i = 0; i < rows.Length; i++)
        //                {
        //                    string[] values = rows[i].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        //                    DataRow dr = dtnew.NewRow();
        //                    dr[0] = "基准值";
        //                    for (int j = 0; j < values.Length; j++)
        //                    {
        //                        dr[j + 1] = values[j];
        //                    }

        //                    dtnew.Rows.Add(dr);
        //                }

        //                return;
        //            }
        //            else if (xlinetype == "2")
        //            {
        //                if (rows.Length < 3)
        //                {
        //                    dtnew = null;
        //                    dout = 0;
        //                    return;
        //                }

        //                int ncoefs = rows.Length;
        //                string[] columns = rows[0].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        //                //第一行为X行
        //                //第二行为Y列。

        //                //生成DataTable表的列。
        //                dtnew.Columns.Add("aa");
        //                for (int i = 0; i < columns.Length; i++)
        //                {
        //                    dtnew.Columns.Add(i.ToString());
        //                }

        //                string[] y_valus = rows[1].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        //                //添加行。
        //                for (int i = 0; i < rows.Length; i++)
        //                {
        //                    string[] x_values = rows[i].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        //                    if (i == 0)
        //                    {
        //                        DataRow dr = dtnew.NewRow();
        //                        dr[0] = "基准值";
        //                        for (int j = 0; j < columns.Length; j++)
        //                        {
        //                            dr[j + 1] = x_values[j].ToString();
        //                        }

        //                        dtnew.Rows.Add(dr);
        //                    }
        //                    //j==1 不添加
        //                    else if (i > 1)
        //                    {
        //                        DataRow dr = dtnew.NewRow();
        //                        dr[0] = y_valus[i];
        //                        for (int j = 0; j < columns.Length; j++)
        //                        {
        //                            dr[j + 1] = x_values[j];
        //                        }

        //                        dtnew.Rows.Add(dr);
        //                    }

        //                }

        //                return;
        //            }

        //            dtnew = null;
        //            dout = 0;

        //        }


        //        /////////////////////////////////////////////////////////////////////////////////
        //        /// <summary>
        //        /// 获得与主键对应的实体对象
        //        /// </summary>
        //        /// <returns></returns>
        //        public static List<KPI_XLineEntity> GetAllEntity()
        //        {
        //            List<KPI_XLineEntity> ltxls = new List<KPI_XLineEntity>();

        //            string sql = "select * from KPI_XLine";

        //            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                KPI_XLineEntity entity = new KPI_XLineEntity();
        //                entity.DrToMember(dr);

        //                ltxls.Add(entity);
        //            }

        //            return ltxls;
        //        }

        //        /// <summary>
        //        /// 获得与主键对应的实体对象
        //        /// </summary>
        //        /// <returns></returns>
        //        public static KPI_XLineEntity GetEntity(string XLineID, string XLineType)
        //        {
        //            KPI_XLineEntity entity = new KPI_XLineEntity();

        //            string sql = "select * from KPI_XLine where XLineID='{0}' and XLineType='{1}'";
        //            sql = string.Format(sql, XLineID, XLineType);

        //            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                entity.DrToMember(dt.Rows[0]);
        //            }

        //            return entity;
        //        }


        //        /// <summary>
        //        /// 判断曲线名称是否存在
        //        /// </summary>
        //        /// <returns></returns>
        //        public static bool XLineExists(string XLineName)
        //        {
        //            string sql = "select * from KPI_XLine where XLineName='" + XLineName + "'";

        //            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }

        //        /// <summary>
        //        /// 得到相同ID的曲线数量
        //        /// </summary>
        //        /// <param name="XLineID"></param>
        //        /// <returns></returns>
        //        public static int XLineIDCounts(string XLineID)
        //        {
        //            string sql = "select XLineID from KPI_XLine where XLineID='" + XLineID + "'";

        //            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;

        //        }
        /// <summary>
        /// 二次拟合取值
        /// </summary>
        /// <param name="xLineID">设计曲线主键</param>
        /// <returns></returns>
        //public static double GetInterpolateValueX2(string xLineID, string xLineType, double dMW)
        //{
        //    int pow = 2;
        //    double[] result = new double[pow + 1];//存储算法返回的系数

        //    double Y = double.MinValue;
        //    double fuhe = dMW;

        //    if (fuhe == double.MinValue)
        //    {
        //        return double.MinValue;
        //    }

        //    try
        //    {
        //        string sql = "";
        //        sql = "select * from KPI_XLine where XLineID='{0}' and XLineType='{1}' ";

        //        ////////////////////////////////////////////////////////////////////////////////////////////
        //        sql = string.Format(sql, xLineID, xLineType);

        //        DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

        //        if (dt.Rows.Count == 1)
        //        {
        //            double[] x = new double[8];
        //            double[] y = new double[8];

        //            DataRow dr = dt.Rows[0];

        //            x[0] = 0.3 * double.Parse(dr["XLineBase"].ToString());
        //            y[0] = double.Parse(dr["XLineP3"].ToString());

        //            x[1] = 0.4 * double.Parse(dr["XLineBase"].ToString());
        //            y[1] = double.Parse(dr["XLineP4"].ToString());

        //            x[2] = 0.5 * double.Parse(dr["XLineBase"].ToString());
        //            y[2] = double.Parse(dr["XLineP5"].ToString());

        //            x[3] = 0.6 * double.Parse(dr["XLineBase"].ToString());
        //            y[3] = double.Parse(dr["XLineP6"].ToString());

        //            x[4] = 0.7 * double.Parse(dr["XLineBase"].ToString());
        //            y[4] = double.Parse(dr["XLineP7"].ToString());

        //            x[5] = 0.8 * double.Parse(dr["XLineBase"].ToString());
        //            y[5] = double.Parse(dr["XLineP8"].ToString());

        //            x[6] = 0.9 * double.Parse(dr["XLineBase"].ToString());
        //            y[6] = double.Parse(dr["XLineP9"].ToString());

        //            x[7] = 1.0 * double.Parse(dr["XLineBase"].ToString());
        //            y[7] = double.Parse(dr["XLinePP"].ToString());


        //            //int i = 0;
        //            //foreach (DataRow dr in dt.Rows)
        //            //{
        //            //    x[i] = double.Parse(dr["XLineAbs"].ToString());
        //            //    y[i] = double.Parse(dr["XLineValue"].ToString());
        //            //    i++;
        //            //}

        //            Regress(x, y, 8, pow, result);

        //            Y = 0;
        //            for (int j = 0; j <= pow; j++)
        //            {
        //                Y += result[j] * Math.Pow(fuhe, j);
        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }

        //    return Y;
        //}

        #endregion

        #region  No Use Functions
  
        public static DataTable GetXLineForExcel()
        {
            string sql = @"select 'x'SelectX, XLineName,XLineDesc,XLineEngunit, XLineBase,XLineNote,
                        cast(sum(case XLinePercent when 30 then XLineValue else null end) as real) 'XLine30', 
                        cast(sum(case XLinePercent when 40 then XLineValue else null   end) as real)  'XLine40',
                        cast(sum(case XLinePercent when 50 then XLineValue else null   end) as real)  'XLine50',
                        cast(sum(case XLinePercent when 60 then XLineValue else null   end) as real)  'XLine60',
                        cast(sum(case XLinePercent when 70 then XLineValue else null   end) as real)  'XLine70',
                        cast(sum(case XLinePercent when 75 then XLineValue else null   end) as real)  'XLine75',
                        cast(sum(case XLinePercent when 80 then XLineValue else null   end) as real)  'XLine80',
                        cast(sum(case XLinePercent when 90 then XLineValue else null   end) as real)  'XLine90',
                        cast(sum(case XLinePercent when 100 then XLineValue else null   end) as real)  'XLine100',
                        cast(sum(case XLinePercent when 105 then XLineValue else null   end) as real)  'XLine105'
                        from KPI_XLine
                        group by XLineID, XLineName, XLineDesc, XLineEngunit, XLineBase, XLineNote
                        order by XLineName";

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            return dt;
        }
        
        public static DataTable GetXLineInfor(string condition)
        {
            string sql = @"select XLineID, XLineName, XLineDesc, XLineEngunit, XLineNote 
                            from KPI_XLine
                            where 1=1 {0}
                            Group by XLineID, XLineName, XLineDesc, XLineEngunit, XLineNote";

            //if (condition != "")
            //{
            sql = string.Format(sql, condition);
            //}

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            return dt;
        }

        public static DataTable GetXLineData(string condition)
        {

            string sql = @"select XLineID, XLineName, XLineDesc, XLineType, XLineEngunit, XLineBase, XLineNote,
                        cast(sum(case XLinePercent when 30 then XLineValue else null end) as real) 'XLine30', 
                        cast(sum(case XLinePercent when 40 then XLineValue else null   end) as real)  'XLine40',
                        cast(sum(case XLinePercent when 50 then XLineValue else null   end) as real)  'XLine50',
                        cast(sum(case XLinePercent when 60 then XLineValue else null   end) as real)  'XLine60',
                        cast(sum(case XLinePercent when 70 then XLineValue else null   end) as real)  'XLine70',
                        cast(sum(case XLinePercent when 75 then XLineValue else null   end) as real)  'XLine75',
                        cast(sum(case XLinePercent when 80 then XLineValue else null   end) as real)  'XLine80',
                        cast(sum(case XLinePercent when 90 then XLineValue else null   end) as real)  'XLine90',
                        cast(sum(case XLinePercent when 100 then XLineValue else null   end) as real)  'XLine100',
                        cast(sum(case XLinePercent when 105 then XLineValue else null   end) as real)  'XLine105'
                        from KPI_XLine 
                        where 1=1 {0}
                        group by XLineID, XLineName, XLineDesc, XLineType, XLineEngunit, XLineBase, XLineNote
                        order by XLineName";

            sql = string.Format(sql, condition);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            return dt;
        }
        
        public static DataTable GetXLineXY(string XLineID, int XLineType)
        {

            string sql = @"select XLineID, XLinePercent, XLineType, XLineBase, XLineValue
                        from KPI_XLine 
                        where 1=1 {0}
                        order by XLinePercent";

            string condition = " and XLineID = '" + XLineID + "' and XLineType=" + XLineType.ToString();
            sql = string.Format(sql, condition);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            return dt;
        }
        
        public static bool UpdateXLine(string sID, string sName, string sDesc, string sEngunit, string sNote)
        {
            bool flag = false;

            try
            {
                using (IDbConnection conn = DBAccess.GetRelation().GetConnection())
                {
                    conn.Open();
                    using (IDbTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                            tmpBuild.Append("update KPI_XLine set ");
                            tmpBuild.Append("XLineName='" + sName + "'");
                            tmpBuild.Append(",");

                            tmpBuild.Append("XLineDesc='" + sDesc + "'");
                            tmpBuild.Append(",");

                            tmpBuild.Append("XLineEngunit='" + sEngunit + "'");
                            tmpBuild.Append(",");

                            tmpBuild.Append("XLineNote='" + sNote + "'");
                            tmpBuild.Append(",");

                            if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                            {
                                tmpBuild.Remove((tmpBuild.Length - 1), 1);
                            }

                            tmpBuild.Append(" where ");
                            tmpBuild.Append("XLineID='" + sID + "'");

                            string __tmpSql = tmpBuild.ToString();

                            if (DBAccess.GetRelation().ExecuteNonQuery(trans, __tmpSql) < 1)
                            {
                                trans.Rollback();
                                return false;
                            }
                            trans.Commit();
                            flag = true;
                        }
                        catch
                        {
                            trans.Rollback();
                            return false;
                        }
                    }
                }
            }
            catch
            {

            }

            return flag;
        }

        public static string GetXLineEngunit(string XLineID)
        {
            string sql = "select top 1 XLineEngunit from KPI_XLine where XLineID ='{0}'";
            sql = string.Format(sql, XLineID);
            return DBAccess.GetRelation().ExecuteScalar(sql).ToString();
        }

        public static string GetXLineID(string XLineName)
        {
            string sql = "select XLineID from KPI_XLine where XLineName='{0}' ";
            sql = string.Format(sql, XLineName);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count < 1)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["XLineID"].ToString();
            }

        }

        public static bool Delete(string XLineID)
        {
            bool flag = false;

            try
            {
                using (IDbConnection conn = DBAccess.GetRelation().GetConnection())
                {
                    conn.Open();
                    using (IDbTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            string sql = "delete from KPI_XLine where  XLineID='{0}'";
                            sql = string.Format(sql, XLineID);
                            if (DBAccess.GetRelation().ExecuteNonQuery(trans, sql) < 1)
                            {
                                trans.Rollback();
                                return false;
                            }
                            trans.Commit();
                            flag = true;
                        }
                        catch
                        {
                            trans.Rollback();
                            return false;
                        }
                    }
                }
            }
            catch
            {

            }

            return flag;
        }


        #endregion

    }
}
