using System;
using System.Collections.Generic;
using System.Text;
//using SIS.DBControl;

namespace SIS.DBControl
{
    public interface RTInterface:IDisposable
    {
        #region ���ݿ����

        /// <summary>
        /// �Ƿ�����ʵʱ��
        /// </summary>
        /// <returns></returns>
        bool Connection();

        /// <summary>
        /// ��÷�����ʱ��
        /// </summary>
        /// <returns></returns>
        DateTime GetServerTime();
		

        #endregion

        #region Point����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="isdigital"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        bool AddPoint(string tagname, bool isdigital, object desc);

        /// <summary>
        /// �ĵ�
        /// </summary>
        /// <param name="oKey"></param>
        /// <param name="nKey"></param>
        /// <returns></returns>
        bool UpdatePoint(string oKey, string nKey);

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        bool DeletePoint(string tagname);

        /// <summary>
        /// ĳ�����Ƿ����
        /// </summary>
        /// <param name="tagname">����</param>
        /// <returns></returns>
        bool ExistPoint(string tagname);

        /// <summary>
        /// �жϱ�ǩ�Ƿ���������
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        bool PointIsDigital(string tagname);

        /// <summary>
        /// �õ������Ϣ
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        TagInfo GetPointInfo(string tagname);

        /// <summary>
        /// ���������õ���㼯��  �����Ϣ
        /// </summary>
        /// <returns></returns>
        List<TagInfo> GetPointInfoList(string filterexp);

        /// <summary>
        /// ���������õ���㼯��  �������֮�䶺�ŷָ� id,����,����,��λ,��ֵ
        /// </summary>
        /// <returns></returns>
        List<string> GetPointList(string filterexp, bool flag, DateTime time);


        /// <summary>
        /// ���������õ���㼯�� �������֮�䶺�ŷָ� ����,����,��λ
        /// </summary>
        /// <param name="WhereClause"></param>
        /// <returns></returns>
        List<string> GetPointList(string filterexp);

        /// <summary>
        /// ͨ��������ѯ��õ���㼯��
        /// </summary>
        /// <returns></returns>
        List<string> GetPointList();

        /// <summary>
        /// ͨ��������ѯ�õ������ֶ�¼��ı�ǩ�㡣
        /// </summary>
        /// <returns></returns>
        List<TagValue> GetPointListForSDLR(string condition);

        #endregion

        #region Value������

        /// <summary>
        /// �жϱ�ǩֵ�Ƿ�����
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        bool PointValueIsGood(string tagname);

        /// <summary>
        /// �õ�ʵʱ���ݣ�����double����
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        double GetSnapshotValue(string tagname);


        /// <summary>
        /// �õ��ַ������͵ĵ�ʵʱֵ
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        string GetSnapshotStringValue(string tagname);

        /// <summary>
        /// �õ�ʵʱ���ݣ�����double���ͼ���ֵʱ��
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        double GetSnapshotValue(string tagname, out object timeStamp);

        /// <summary>
        /// ��ʵʱ���ݿ��PointList��ֵ
        /// </summary>
        /// <param name="lttvs"></param>
        /// <returns></returns>
        bool SetPointList(Dictionary<string, TagValue> lttvs, out string strError);

        /// <summary>
        /// �õ�List�����б�ǩ���ʵʱ����
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        bool GetSnapshotListData(ref Dictionary<string, TagValue> lttvs, out string strError);

        /// <summary>
        /// �õ�List�����б�ǩ�����ʷ����
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        bool GetArchiveListData(ref Dictionary<string, TagValue> lttvs, DateTime stime, out string strError);

        /// <summary>
        /// ��������ʱ���ѯ�鵵���ݣ�����double����
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        double GetArchiveValue(string tagname, DateTime dt);

        /// <summary>
        /// ������һ��д��Ĺ鵵ֵ����,����double����
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        double GetArchiveValue(string tagname, out object timeStamp);

        /// <summary>
        /// �õ���������ʵʱ��ֵ
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        string GetDigitalSnapshotValue(string tagname);

        /// <summary>
        /// �õ���������ʵʱֵ���ƣ�on off��
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        string GetDigitalSnapshotValueName(string tagname);

        /// <summary>
        /// �õ��������Ĺ鵵ֵ��ʱ��
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        string GetDigitalArchiveValue(string tagname, DateTime dt);

        /// <summary>
        /// �õ��������Ĺ鵵ֵ��ʱ��
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        string GetDigitalArchiveValueName(string tagname, DateTime dt);



		//Added by pyf 2013-09-16
		List<TagValue> GetHistoryDataList(string tagname, DateTime stime, DateTime etime);
		//End of Added.
        /// <summary>
        /// �õ�ʱ�������ڵĲ�����ݼ�(�뼶ȡ��)
        /// </summary>
        /// <param name="tagname">���</param>
        /// <param name="isdigital">�Ƿ�Ϊģ����</param>
        /// <param name="stime">��ʼ����</param>
        /// <param name="etime">��������</param>
        /// <param name="interval">ȡ��ʱ����</param>
        /// <returns>�����</returns>
        List<double> GetHistoryDataListBySecondSpan(string tagname, bool isdigital, DateTime stime, DateTime etime, int interval);

        /// <summary>
        /// �õ�ʱ�������ڵĲ�����ݼ�(����ȡ��)
        /// </summary>
        /// <param name="tagname">���</param>
        /// <param name="isdigital">�Ƿ�Ϊģ����</param>
        /// <param name="stime">��ʼ����</param>
        /// <param name="etime">��������</param>
        /// <param name="interval">ȡ��ʱ����</param>
        /// <returns>�����</returns>
        List<double> GetHistoryDataListByMinuteSpan(string tagname, bool isdigital, DateTime stime, DateTime etime, int interval);
        

        /// <summary>
        /// �õ�ʱ�������ڵĲ�����ݼ�(������ȡ��)
        /// </summary>
        /// <param name="tagname">���</param>
        /// <param name="isdigital">�Ƿ�Ϊģ����</param>
        /// <param name="stime">��ʼ����</param>
        /// <param name="etime">��������</param>
        /// <param name="interval">ȡ������</param>
        /// <returns>�����</returns>
        object[] GetHistoryDataListByCount(string tagname, bool isdigital, DateTime stime, DateTime etime, int interval);

        #endregion

        #region  Valueд����

        /// <summary>
        /// ��д����
        /// </summary>
        /// <param name="tagname">���</param>
        /// <param name="value">��ֵ</param>
        /// <returns></returns>
        bool WriteSnapshotValue(string tagname, string value);//��д���ݿ�

        /// <summary>
        /// ��д����
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool WriteArchiveValue(string tagname, object time, string value);

        #endregion

        #region  Valueɾ������

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="tagname">���</param>
        /// <param name="value">��ֵ</param>
        /// <returns></returns>
        bool DeleteValue(string tagname, DateTime stime, DateTime etime);

        #endregion

        #region  ͳ�Ʋ���(Expression\Tag)

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //double����
        //���⿪ʼ����ʱ��

        /// <summary>
        /// ĳ������ʽ��ʵʱֵ
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        double ExpCurrentValue(string expression);

        /// <summary>
        /// ָ����ʼ����ʱ��
        /// ͳ��ʱ���ڵ�Total\Max\Min\StdDev\Range\Average\PStdDev,���ص�������
        /// </summary>
        double ExpCalculatedData(string expression, DateTime stime, DateTime etime, string filter, SummaryType type);

        /// <summary>
        /// ָ����ʼ����ʱ��
        /// ͳ��ʱ���ڵ�Total\Max\Min\StdDev\Range\Average\PStdDev,����ʱ��������������
        /// </summary>
        bool ExpCalculatedData(string expression, DateTime stime, DateTime etime, string duration, string filter, SummaryType type, out double[] pdata);


        /// <summary>
        /// ָ����ʼ����ʱ��
        /// ͳ��ʱ���ڵ�Total\Max\Min\StdDev\Range\Average\PStdDev,���ص�������
        /// </summary>
        double TagCalculatedData(string tagname, DateTime stime, DateTime etime, string filter, SummaryType type);

        /// <summary>
        /// ָ����ʼ����ʱ��
        /// ͳ��ʱ���ڵ�Total\Max\Min\StdDev\Range\Average\PStdDev,����ʱ��������������
        /// </summary>
        bool TagCalculatedData(string tagname, DateTime stime, DateTime etime, string duration, string filter, SummaryType type, out double[] pdata);

      /// <summary>
        /// ָ����ʼ����ʱ��
        /// ͳ��ʱ���ڵ�Total\Snapshot\Max\Min\StdDev\Range\Average\PStdDev, ����������
        /// </summary>
        bool TagSummaryData(string tagname, DateTime stime, DateTime etime, string filter, out TagAllValue pdata);

        //��������

        //�����·�

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //string����


        #endregion

        #region ����Time���������ͳ�Ʋ���

        /// <summary>
        /// ͳ�Ʋ������ʱ��
        /// </summary>
        /// <param name="tagname">���</param>
        /// <param name="stime">��ʼʱ��</param>
        /// <param name="etime">����ʱ��</param>
        /// <returns></returns>
        double GetExpressionTrueSecondTime(string expression, DateTime stime, DateTime etime);

        /// <summary>
        /// ����ĳʱ��ĳ���ʽ��ֵ
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="dttime"></param>
        /// <returns></returns>
        double TimedCalculate(string expression, DateTime dttime);

        #endregion

    }
}
