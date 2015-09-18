using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Assistant
{
    public class DataCalculate
    {
        /// <summary>
        /// 得到最大值
        /// </summary>
        /// <param name="TagValueList">数据集合</param>
        /// <returns></returns>
        public static double GetMax(List<double> TagValueList)
        {
            double d = double.MinValue;
            foreach (double val in TagValueList)
            {
                if (val > d)
                    d = val;
            }
            return d;
        }
        /// <summary>
        /// 得到最小值
        /// </summary>
        /// <param name="TagValueList">数据集合</param>
        /// <returns></returns>
        public static double GetMin(List<double> TagValueList)
        {
            double d = double.MaxValue;
            foreach (double val in TagValueList)
            {
                if (val < d)
                    d = val;
            }
            return d;
        }
        /// <summary>
        /// 得到均值
        /// </summary>
        /// <param name="TagValueList">数据集合</param>
        /// <returns></returns>
        public static double GetAvg(List<double> TagValueList)
        {
            double d = double.MinValue;
            double temp = .00;
            foreach (double val in TagValueList)
            {
                temp += val;
            }
            d = temp / TagValueList.Count;
            return d;
        }

        ///wuguanhui
        ///2010-7-23
        ///添加和
        /// <summary>
        /// 得到和
        /// </summary>
        /// <param name="TagValueList">数据集合</param>
        /// <returns></returns>
        public static double GetSum(List<double> TagValueList)
        {
            double d = 0.00;
            foreach (double val in TagValueList)
            {
                d += val;
            }
            return d;
        }
        ///wuguanhui
        ///2010-7-23
        ///添加差
        /// <summary>
        /// 得到差
        /// </summary>
        /// <param name="TagValueList">数据集合</param>
        /// <returns></returns>
        public static double GetDif(List<double> TagValueList)
        {
            double d = 0.00;
            if (TagValueList.Count != 2)
            {
                return d;
            }
            else
            {
                d = TagValueList[0] - TagValueList[1];
                return d;
            }

        }             
        
        /// <summary>
        /// 得到数据集的分项最大 最小 均值
        /// </summary>
        /// <param name="values">数据集合</param>
        /// <param name="type">得到数据的类型 1 最大 2 最小 3 均值</param>
        /// <returns></returns>
        public static double[] GetCalculateData(List<object[]> values,int type)
        {
            int count = values[0].Length;
            double[] result = new double[count];
            List<double> vals = null;
            for (int i = 0; i < count; i++)
            {
                //得到相同位置的数据
                vals = new List<double>();
                for (int j = 0; j < values.Count; j++)
                {
                    vals.Add(double.Parse(values[j][i].ToString()));
                }
                //根据类型对相同位置的数据进行运算
                if (type == 1)
                    result[i] = GetMax(vals);
                else if (type == 2)
                    result[i] = GetMin(vals);
                else if (type == 3)
                    result[i] = GetAvg(vals);
            }
            return result;
        }
    }
}
