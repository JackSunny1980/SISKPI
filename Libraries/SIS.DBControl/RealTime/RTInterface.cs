using System;
using System.Collections.Generic;
using System.Text;
//using SIS.DBControl;

namespace SIS.DBControl
{
    public interface RTInterface:IDisposable
    {
        #region 数据库操作

        /// <summary>
        /// 是否连接实时库
        /// </summary>
        /// <returns></returns>
        bool Connection();

        /// <summary>
        /// 获得服务器时间
        /// </summary>
        /// <returns></returns>
        DateTime GetServerTime();
		

        #endregion

        #region Point操作

        /// <summary>
        /// 建点
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="isdigital"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        bool AddPoint(string tagname, bool isdigital, object desc);

        /// <summary>
        /// 改点
        /// </summary>
        /// <param name="oKey"></param>
        /// <param name="nKey"></param>
        /// <returns></returns>
        bool UpdatePoint(string oKey, string nKey);

        /// <summary>
        /// 删点
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        bool DeletePoint(string tagname);

        /// <summary>
        /// 某个点是否存在
        /// </summary>
        /// <param name="tagname">点名</param>
        /// <returns></returns>
        bool ExistPoint(string tagname);

        /// <summary>
        /// 判断标签是否是数字量
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        bool PointIsDigital(string tagname);

        /// <summary>
        /// 得到测点信息
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        TagInfo GetPointInfo(string tagname);

        /// <summary>
        /// 根据条件得到测点集合  测点信息
        /// </summary>
        /// <returns></returns>
        List<TagInfo> GetPointInfoList(string filterexp);

        /// <summary>
        /// 根据条件得到测点集合  测点属性之间逗号分隔 id,点名,描述,单位,数值
        /// </summary>
        /// <returns></returns>
        List<string> GetPointList(string filterexp, bool flag, DateTime time);


        /// <summary>
        /// 根据条件得到测点集合 测点属性之间逗号分隔 点名,描述,单位
        /// </summary>
        /// <param name="WhereClause"></param>
        /// <returns></returns>
        List<string> GetPointList(string filterexp);

        /// <summary>
        /// 通过弹出查询框得到测点集合
        /// </summary>
        /// <returns></returns>
        List<string> GetPointList();

        /// <summary>
        /// 通过条件查询得到符合手动录入的标签点。
        /// </summary>
        /// <returns></returns>
        List<TagValue> GetPointListForSDLR(string condition);

        #endregion

        #region Value读操作

        /// <summary>
        /// 判断标签值是否正常
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        bool PointValueIsGood(string tagname);

        /// <summary>
        /// 得到实时数据，返回double类型
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        double GetSnapshotValue(string tagname);


        /// <summary>
        /// 得到字符串类型的的实时值
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        string GetSnapshotStringValue(string tagname);

        /// <summary>
        /// 得到实时数据，返回double类型及数值时间
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        double GetSnapshotValue(string tagname, out object timeStamp);

        /// <summary>
        /// 将实时数据库的PointList赋值
        /// </summary>
        /// <param name="lttvs"></param>
        /// <returns></returns>
        bool SetPointList(Dictionary<string, TagValue> lttvs, out string strError);

        /// <summary>
        /// 得到List表所有标签点的实时数据
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        bool GetSnapshotListData(ref Dictionary<string, TagValue> lttvs, out string strError);

        /// <summary>
        /// 得到List表所有标签点的历史数据
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        bool GetArchiveListData(ref Dictionary<string, TagValue> lttvs, DateTime stime, out string strError);

        /// <summary>
        /// 根据输入时间查询归档数据，返回double类型
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        double GetArchiveValue(string tagname, DateTime dt);

        /// <summary>
        /// 获得最后一次写入的归档值及间,返回double类型
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        double GetArchiveValue(string tagname, out object timeStamp);

        /// <summary>
        /// 得到数字量的实时数值
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        string GetDigitalSnapshotValue(string tagname);

        /// <summary>
        /// 得到数字量的实时值名称（on off）
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        string GetDigitalSnapshotValueName(string tagname);

        /// <summary>
        /// 得到数字量的归档值及时间
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        string GetDigitalArchiveValue(string tagname, DateTime dt);

        /// <summary>
        /// 得到数字量的归档值及时间
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        string GetDigitalArchiveValueName(string tagname, DateTime dt);



		//Added by pyf 2013-09-16
		List<TagValue> GetHistoryDataList(string tagname, DateTime stime, DateTime etime);
		//End of Added.
        /// <summary>
        /// 得到时间周期内的测点数据集(秒级取数)
        /// </summary>
        /// <param name="tagname">测点</param>
        /// <param name="isdigital">是否为模拟量</param>
        /// <param name="stime">开始日期</param>
        /// <param name="etime">结束日期</param>
        /// <param name="interval">取数时间间隔</param>
        /// <returns>结果集</returns>
        List<double> GetHistoryDataListBySecondSpan(string tagname, bool isdigital, DateTime stime, DateTime etime, int interval);

        /// <summary>
        /// 得到时间周期内的测点数据集(分钟取数)
        /// </summary>
        /// <param name="tagname">测点</param>
        /// <param name="isdigital">是否为模拟量</param>
        /// <param name="stime">开始日期</param>
        /// <param name="etime">结束日期</param>
        /// <param name="interval">取数时间间隔</param>
        /// <returns>结果集</returns>
        List<double> GetHistoryDataListByMinuteSpan(string tagname, bool isdigital, DateTime stime, DateTime etime, int interval);
        

        /// <summary>
        /// 得到时间周期内的测点数据集(按个数取数)
        /// </summary>
        /// <param name="tagname">测点</param>
        /// <param name="isdigital">是否为模拟量</param>
        /// <param name="stime">开始日期</param>
        /// <param name="etime">结束日期</param>
        /// <param name="interval">取数个数</param>
        /// <returns>结果集</returns>
        object[] GetHistoryDataListByCount(string tagname, bool isdigital, DateTime stime, DateTime etime, int interval);

        #endregion

        #region  Value写操作

        /// <summary>
        /// 回写数据
        /// </summary>
        /// <param name="tagname">测点</param>
        /// <param name="value">数值</param>
        /// <returns></returns>
        bool WriteSnapshotValue(string tagname, string value);//回写数据库

        /// <summary>
        /// 回写数据
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool WriteArchiveValue(string tagname, object time, string value);

        #endregion

        #region  Value删除操作

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tagname">测点</param>
        /// <param name="value">数值</param>
        /// <returns></returns>
        bool DeleteValue(string tagname, DateTime stime, DateTime etime);

        #endregion

        #region  统计操作(Expression\Tag)

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //double类型
        //任意开始结束时间

        /// <summary>
        /// 某计算表达式的实时值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        double ExpCurrentValue(string expression);

        /// <summary>
        /// 指定开始结束时间
        /// 统计时间内的Total\Max\Min\StdDev\Range\Average\PStdDev,返回单个数据
        /// </summary>
        double ExpCalculatedData(string expression, DateTime stime, DateTime etime, string filter, SummaryType type);

        /// <summary>
        /// 指定开始结束时间
        /// 统计时间内的Total\Max\Min\StdDev\Range\Average\PStdDev,根据时间间隔返回数据组
        /// </summary>
        bool ExpCalculatedData(string expression, DateTime stime, DateTime etime, string duration, string filter, SummaryType type, out double[] pdata);


        /// <summary>
        /// 指定开始结束时间
        /// 统计时间内的Total\Max\Min\StdDev\Range\Average\PStdDev,返回单个数据
        /// </summary>
        double TagCalculatedData(string tagname, DateTime stime, DateTime etime, string filter, SummaryType type);

        /// <summary>
        /// 指定开始结束时间
        /// 统计时间内的Total\Max\Min\StdDev\Range\Average\PStdDev,根据时间间隔返回数据组
        /// </summary>
        bool TagCalculatedData(string tagname, DateTime stime, DateTime etime, string duration, string filter, SummaryType type, out double[] pdata);

      /// <summary>
        /// 指定开始结束时间
        /// 统计时间内的Total\Snapshot\Max\Min\StdDev\Range\Average\PStdDev, 返回数据组
        /// </summary>
        bool TagSummaryData(string tagname, DateTime stime, DateTime etime, string filter, out TagAllValue pdata);

        //任意日期

        //任意月份

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //string类型


        #endregion

        #region 基于Time区间的数据统计操作

        /// <summary>
        /// 统计测点运行时间
        /// </summary>
        /// <param name="tagname">测点</param>
        /// <param name="stime">开始时间</param>
        /// <param name="etime">结束时间</param>
        /// <returns></returns>
        double GetExpressionTrueSecondTime(string expression, DateTime stime, DateTime etime);

        /// <summary>
        /// 计算某时刻某表达式的值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="dttime"></param>
        /// <returns></returns>
        double TimedCalculate(string expression, DateTime dttime);

        #endregion

    }
}
