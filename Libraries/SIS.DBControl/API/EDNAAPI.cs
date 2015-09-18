using System;
using System.Text;
using System.Runtime.InteropServices;

//const   int*，用"ref   int"来替换
//int*   -〉IntPtr   
//char*   ->   StringBulderTop

namespace SIS.DBControl
{
    public class Service
    {
        [DllImport("ezdnaapi.dll", EntryPoint = "DnaGetServiceEntry", CharSet = CharSet.Ansi)]
        public static extern int DnaGetServiceEntry(string szType, string szStartSvcName, ref System.UInt32 key,
            StringBuilder szSvrName, System.UInt16 nName,
            StringBuilder szSvrDesc, System.UInt16 nDesc,
            StringBuilder szSvrType, System.UInt16 nType,
            StringBuilder szSvrStatus, System.UInt16 nStatus
            );

        [DllImport("ezdnaapi.dll", EntryPoint = "DnaGetNextServiceEntry", CharSet = CharSet.Ansi)]
        public static extern int DnaGetNextServiceEntry(System.UInt32 key,
            StringBuilder szSvrName, System.UInt16 nName,
            StringBuilder szSvrDesc, System.UInt16 nDesc,
            StringBuilder szSvrType, System.UInt16 nType,
            StringBuilder szSvrStatus, System.UInt16 nStatus
            );


        [DllImport("ezdnaapi.dll", EntryPoint = "DnaGetNextPointEntry", CharSet = CharSet.Ansi)]
        public static extern int DnaGetNextPointEntry(System.UInt32 ulKey,
            StringBuilder szPoint, ushort nPoint,
            out double pdValue,
            StringBuilder szTime, ushort nTime,
            StringBuilder szStatus, ushort nStatus,
            StringBuilder szDesc, ushort nDesc,
            StringBuilder szUnits, ushort nUnits
            );


        [DllImport("ezdnaapi.dll", EntryPoint = "DnaGetPointEntry", CharSet = CharSet.Ansi)]
        public static extern int DnaGetPointEntry(string szServiceName,
               ushort nStarting,
               out System.UInt32 key,
               StringBuilder szPoint, System.UInt16 nPoint,
               out double pdValue,
               StringBuilder szTime, System.UInt16 nTime,
               StringBuilder szStatus, System.UInt16 nStatus,
               StringBuilder szDesc, System.UInt16 nDesc,
               StringBuilder szUnits, System.UInt16 nUnits
            );


        //通过longid查询出Shortid
        [DllImport("ezdnaapi.dll", EntryPoint = "ShortIdFromLongId", CharSet = CharSet.Ansi)]
        public static extern int ShortIdFromLongId(
                              string szService,
                              string szLongId,
                              StringBuilder szPoint,
                              System.UInt16 nPoint);
        //服务对话框
        [DllImport("ezdnaapi.dll", EntryPoint = "DnaSelectService", CharSet = CharSet.Ansi)]
        public static extern int DnaSelectService(string szType, StringBuilder szService, System.UInt16 nService);

        //标签名对话框 
        [DllImport("ezdnaapi.dll", EntryPoint = "DnaSelectPoint", CharSet = CharSet.Ansi)]
        public static extern int DnaSelectPoint(StringBuilder szPoint, System.UInt16 nPoint);




    }

    public class RealTime
    {
        //读取标签点所有信息
        [DllImport("ezdnaapi.dll", EntryPoint = "DNAGetRTAll", CharSet = CharSet.Ansi)]
        public static extern int DNAGetRTAll(
                        string szPoint,
                       ref  double pdValue,
                       StringBuilder szTime, System.UInt16 nTime,
                        StringBuilder szStatus, System.UInt16 nStatus,
                        StringBuilder szDesc, System.UInt16 nDesc,
                        StringBuilder szUnits, System.UInt16 nUnits);

        //读取标签点读数值
        [DllImport("ezdnaapi.dll", EntryPoint = "DNAGetRTValue", CharSet = CharSet.Ansi)]
        public static extern int DNAGetRTValue(string szPoint,					//完整点名
            ref double pdValue);

        //读取标签点时间
        [DllImport("ezdnaapi.dll", EntryPoint = "DNAGetRTTime", CharSet = CharSet.Ansi)]
        public static extern int DNAGetRTTime(string szPoint,					//完整点名
            StringBuilder szTime,
            System.UInt16 nTime);

        //读取标签点时间（UTC）
        [DllImport("ezdnaapi.dll", EntryPoint = "DNAGetRTTimeUTC", CharSet = CharSet.Ansi)]
        public static extern int DNAGetRTTimeUTC(
            string szPoint, ref int lTime);

        //UCTToStringTime将UTC格式的时间转换成字符串格式
        [DllImport("ezdnaapi.dll", EntryPoint = "UCTToStringTime", CharSet = CharSet.Ansi)]
        public static extern int UCTToStringTime(
            int iTime, StringBuilder szTime, System.UInt16 nTime);

        //将string的时间转换成int类型的时间
        [DllImport("ezdnaapi.dll", EntryPoint = "StringToUTCTime", CharSet = CharSet.Ansi)]
        public static extern int StringToUTCTime(string szTime);

    }

    public class PushWrite
    {
        [DllImport("EZDnaServApi.dll", EntryPoint = "DnaAddAnalogShortIdRecordNoStatus", CharSet = CharSet.Ansi)]
        public static extern int DnaAddAnalogShortIdRecordNoStatus(
            string Service,//由实时服务的"站点名.服务名"组成的名称
            string PointId,//short ID
            int tTime,//数值的UTC时间标签,若指定为0,则使用实时服务所在计算机的时间标签
            double dValue//模拟量数值
            );

        // #region 强制刷新数据记录.返回值:0表示成功,非0表示出错
        [DllImport("EZDnaServApi.dll", EntryPoint = "DnaFlushShortIdRecords", CharSet = CharSet.Ansi)]
        public static extern int DnaFlushShortIdRecords(
            string Service,					//由实时服务的"站点名.服务名"组成的名称
            System.Text.StringBuilder Message,					//强制刷新的错误描述
            System.UInt16 MsgLength					//错误描述字段的长度
            );


        [DllImport("EZDnaServApi.dll", EntryPoint = "DnaFlushLongIdRecords", CharSet = CharSet.Ansi)]
        public static extern int DnaFlushLongIdRecords(
            string Service,					//由实时服务的"站点名.服务名"组成的名称
            System.Text.StringBuilder Message,					//强制刷新的错误描述
            System.UInt16 MsgLength					//错误描述字段的长度
            );
        //数字量写入函数（shortid）
        [DllImport("EZDnaServApi.dll", EntryPoint = "DnaAddDigitalShortIdRecord", CharSet = CharSet.Ansi)]
        public static extern int DnaAddDigitalShortIdRecord(
            string Service,					//由实时服务的"站点名.服务名"组成的名称
            string PointId,					//short ID
            int tTime,						//数值的UTC时间标签,若指定为0,则使用实时服务所在计算机的时间标签
            int bSet,						//表示开或关:非0值表示真,0值表示假
            string ValueString,				//由16个字节组成的描述来描述当前状态的字符串
            int bDigitalWarning,			//数字是否处于警告状态:非0值表示真,0值表示假
            int bDigitalChattering,			//数字是否处于跳变状态:非0值表示真,0值表示假
            int bUnReliable,				//是否不可靠:非0值表示真,0值表示假
            int bManual);					//是否手动设置:非0值表示真,0值表示假
        [DllImport("EZDnaServApi.dll", EntryPoint = "DnaAddAnalogLongIdRecordNoStatus", CharSet = CharSet.Ansi)]
        public static extern int DnaAddAnalogLongIdRecordNoStatus(
            string Service,					//由实时服务的"站点名.服务名"组成的名称
            string intId,					//Long ID
            int tTime,						//数值的UTC时间标签,若指定为0,则使用实时服务所在计算机的时间标签
            double dValue					//模拟量数值
            );

        [DllImport("EZDnaServApi.dll", EntryPoint = "DnaAddAnalogExtIdRecordNoStatus", CharSet = CharSet.Ansi)]
        public static extern int DnaAddAnalogExtIdRecordNoStatus(
            string Service,					//由实时服务的"站点名.服务名"组成的名称
            string intId,					//Extended ID
            int tTime,						//数值的UTC时间标签,若指定为0,则使用实时服务所在计算机的时间标签
            double dValue					//模拟量数值
            );




        [DllImport("EZDnaServApi.dll", EntryPoint = "DnaFlushAllRecords", CharSet = CharSet.Ansi)]
        public static extern int DnaFlushAllRecords(
            string Service,					//由实时服务的"站点名.服务名"组成的名称
            System.Text.StringBuilder Message,					//强制刷新的错误描述
            System.UInt16 MsgLength					//错误描述字段的长度
            );
    }

    public class UNIVSERV
    {
        //对Universal service服务初始化
        [DllImport("eDnaLink.dll", EntryPoint = "eDnaLinkInit", CharSet = CharSet.Ansi)]
        public static extern int eDnaLinkInit(
            bool bAckDataPacket,
            bool bQueueEnabled,
            bool bCacheEnabled,
            int nMaxCacheKBytes,
            string szCacheFileName,
            string szCacheDriveDir
            );

        //向Universal service服务连接
        [DllImport("eDnaLink.dll", EntryPoint = "eDnaLinkConnect", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int eDnaLinkConnect(
            string szIPAddress, //172.16.35.53
            int usPort      //9999
            );

        //向Universal service服务写数据(shortid)
        [DllImport("eDnaLink.dll", EntryPoint = "eDnaLinkAddRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int eDnaLinkAddRec(string ShortId, int tTime, ushort usMsec, short usStatus, double data);

        //向Universal service服务写数据(longid)
        [DllImport("eDnaLink.dll", EntryPoint = "eDnaLinkAddLongIdRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int eDnaLinkAddLongIdRec(string LongId, int tTime, ushort usMsec, short usStatus, double data);


        //刷新
        [DllImport("eDnaLink.dll", EntryPoint = "eDnaLinkFlush", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int eDnaLinkFlush(int nRequestType);


        //关闭连接
        [DllImport("eDnaLink.dll", EntryPoint = "eDnaLinkClear", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int eDnaLinkClear(int nRet);

        //显示连接状态
        [DllImport("eDnaLink.dll", EntryPoint = "IseDnaLinkConnected", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int IseDnaLinkConnected(int nRet);

        //==============================================================================
        //初始化UNIVSERV
        [DllImport("ezdnaservapi.dll", EntryPoint = "eDnaMxUniversalInitialize", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int eDnaMxUniversalInitialize(out int key, bool bAckDataPacket, bool bQueueEnabled, bool bCacheEnabled, int nMaxCacheKBytes, string szCacheFileName, string szCacheDriveDir);
        //连接UNIVSERV
        [DllImport("ezdnaservapi.dll", EntryPoint = "eDnaMxUniversalDataConnect", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int eDnaMxUniversalDataConnect(int key, string IPAddress, int usPort, string IPAddress2, int usPort2);
        //写数
        [DllImport("ezdnaservapi.dll", EntryPoint = "eDnaMxAddRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int eDnaMxAddRec(int key, string pointid, int Timeset, int usms, int usstatus, double value);
        //刷新服务点记录
        [DllImport("ezdnaservapi.dll", EntryPoint = "eDnaMxFlushUniversalRecord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int eDnaMxFlushUniversalRecord(int key, int requesttype);
        //断开连接
        [DllImport("ezdnaservapi.dll", EntryPoint = "eDnaMxUniversalCloseSocket", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int eDnaMxUniversalCloseSocket(int key);
        
        //==============================================================================



    }

    /// <summary>
    /// 
    /// </summary>
    public class History
    {
        //平均值
        [DllImport("ezdnaapi.dll", EntryPoint = "DnaGetHistAvgUTC", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int DnaGetHistAvgUTC(string szPoint, int tStart, int tEnd, int tPeriod, ref System.UInt32 pulKey);

        //最大值
        [DllImport("ezdnaapi.dll", EntryPoint = "DnaGetHistMinUTC", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int DnaGetHistMinUTC(string szPoint, int tStart, int tEnd, int tPeriod, ref System.UInt32 pulKey);

        //最大值
        [DllImport("ezdnaapi.dll", EntryPoint = "DnaGetHistMaxUTC", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int DnaGetHistMaxUTC(string szPoint, int tStart, int tEnd, int tPeriod, ref System.UInt32 pulKey);


        //查找历史数据库中的原始数据
        [DllImport("ezdnaapi.dll", EntryPoint = "DnaGetHistDirectRawUTC", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int DnaGetHistDirectRawUTC(
                                    string szHistory,
                                   string szPoint,
                                   int tStart,
                                   int tEnd,
                                   ref System.UInt32 pulKey);
        //查找下一个数据.
        [DllImport("ezdnaapi.dll", EntryPoint = "DnaGetNextHistUTC", CharSet = CharSet.Ansi)]
        public static extern int DnaGetNextHistUTC(
            System.UInt32 ulKey,
            ref double pdValue,
            ref int ptTime,
            StringBuilder szStatus, System.UInt16 nStatus);

        //查找历史数据库中的快照值
        [DllImport("ezdnaapi.dll", EntryPoint = "DnaGetHistSnapUTC", CharSet = CharSet.Ansi)]
        public static extern int DnaGetHistSnapUTC(string szPoint, int tStart, int tEnd, int tPeriod, ref System.UInt32 pulKey);


        //向服务队列中追加一条在历史库中最晚时间标签后面的记录
        [DllImport("ezdnaapi.dll", EntryPoint = "DnaHistQueueAppendValue", CharSet = CharSet.Ansi)]
        public static extern int DnaHistQueueAppendValue(
            string szService, string szPoint,
            int iTime,
            System.UInt16 nStatus,
            string nValue,
            StringBuilder szError,
            System.UInt16 nError);

        //刷新数据
        [DllImport("ezdnaapi.dll", EntryPoint = "DnaHistFlushAppendValues", CharSet = CharSet.Ansi)]
        public static extern int DnaHistFlushAppendValues(
            string szService, string szPoint,
            StringBuilder szError,
            System.UInt16 nError);



        //向服务中添加一条数据
        [DllImport("ezdnaapi.dll", EntryPoint = "DnaHistQueueUpdateInsertValue", CharSet = CharSet.Ansi)]
        public static extern int DnaHistQueueUpdateInsertValue(string szService, string szPoint,
            int iTime, System.UInt16 nStatus, string nValue, StringBuilder szError, System.UInt16 nError);

        //刷新数据
        [DllImport("ezdnaapi.dll", EntryPoint = "DnaHistFlushUpdateInsertValues", CharSet = CharSet.Ansi)]
        public static extern int DnaHistFlushUpdateInsertValues(string szService, string szPoint, StringBuilder szError, System.UInt16 nError);




        //删除一条记录
        [DllImport("ezdnaapi.dll", EntryPoint = "DnaHistDeleteValue", CharSet = CharSet.Ansi)]
        public static extern int DnaHistDeleteValue(string szService, string szPoint,
            int iTime, StringBuilder szError, System.UInt16 nError);

        //批量删除记录
        //[DllImport("ezdnaapi.dll", EntryPoint = "DnaHistDeleteBlock", CharSet = CharSet.Ansi)]
        //public static extern int DnaHistDeleteBlock(string szService, 
        //    string szPoint,
        //    int iTime, StringBuilder szError, System.UInt16 nError);

    }

    public class ConfigService
    {
        [DllImport("EZDnaServApi.dll", EntryPoint = "DnaSendConfigRecord", CharSet = CharSet.Ansi)]
        public static extern int DnaSendConfigRecord(
            //服务器信息
            string Point,					//由实时服务的"站点名.服务名.点名"组成的名称
            string intId,					//ID
            string Desc,					//点描述信息
            string Units,					//点单位
            int bAnalog,					//标识是模拟量还是开关量 0表示假,非0表示真

            //报警信息
            int bReportAlarms,				//是否报告报警信息
            System.UInt16 nAlarmPriority,			//报警优先级

            // digital point information
            int bInvertDigitalSense,		//反转开关量含义,目前不支持
            string szDigitalAlarmString,	//变为报警状态时发送给报警服务的字符串
            string szDigitalNormString,		//变为正常状态时发送给报警服务的字符串

            // analog point inforamtion
            int bHighOOREnabled,			//是否允许高越界
            double dHighOORValue,			//高越界数值,用双浮点型书写
            int bHighAlarmEnabled,			//是否允许高报警
            double dHighAlarmValue,			//高报警数值
            int bHighWarningEnabled,		//是否允许高警告
            double dHighWarningValue,		//高警告数值
            int bLowWarningEnabled,			//是否允许低警告
            double dLowWarningValue,		//低警告数值
            int bLowAlarmEnabled,			//是否允许抵报警
            double dLowAlarmValue,			//低报警数值
            int bLowOOREnabled,				//是否允许抵越界
            double dLowOORValue,			//低越界数值

            // history information
            int bReportHistory,				//是否报告历史
            int bReportForceHistory,		//是否强制写历史
            int tForcePeriod,				//强制间隔
            int tMinimumPeriod,			//送历史的最小间隔,如果刷新频率高于最小间隔,那么最小间隔不起作用
            System.UInt16 nHistoryExpireDays,		//在历史服务中保留的天数,最大9999
            double dHistoryDeadBand,		//数值死区

            // the following are presently unsupported
            System.UInt16 nSystemNumber,				//点的系统编码,目前不支持
            double dAnalogDisplayTop,		//保留
            double dAnalogDisplayBottom,	//保留
            string psSpare01,				//保留
            string psSpare02,				//保留
            string psSpare03,				//保留
            string psSpare04,				//保留
            string psSpare05,				//保留
            string psSpare06,				//保留
            string psSpare07,				//保留
            string psSpare08,				//保留
            string psSpare09,				//保留
            string psSpare10,				//保留
            string psSpare11,				//保留
            string psSpare12,				//保留
            string psSpare13,				//保留
            string psSpare14,				//保留
            string psSpare15,				//保留
            string psSpare16,				//保留
            string psSpare17,				//保留
            string psSpare18,				//保留
            string psSpare19,				//保留

            // return information
            ref string szError,				//接受错误返回消息
            System.UInt16 nError						//错误信息的最大长度
            );

    }
}
