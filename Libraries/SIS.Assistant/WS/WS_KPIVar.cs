using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.InteropServices;
using SIS.DataAccess;
using SIS.DataEntity;
using SIS.DBControl;
using SIS.Loger;


namespace SIS.Assistant.WS {

	/// <summary>
	/// 实时绩效考核系统变量类
	/// </summary>
	public  class WS_KPIVar {

		
		#region 系统参数
		/// <summary>
		/// 是否第一次计算，包括重新载入指标
		/// </summary>
		public bool bFirst {
			get;
			set;
		}

		#endregion

		//////////////////////////////////////////////////////////////////////////////
		#region 数据库参数

		/// <summary>
		/// 是否取服务器时间
		/// </summary>
		public  bool bTimeMode;

		/// <summary>
		/// 偏置时间,秒
		/// </summary>
		public  int nOffset;

		/// <summary>
		/// 所有机组集合
		/// </summary>
		public  List<KPI_UnitEntity> ltUnits;

		/// <summary>
		/// 所有设备集合
		/// </summary>
		//public static List<KPI_SeqEntity> ltSeqs;
		public  List<String> ltSeqs;

		/// <summary>
		/// 所有指标集合
		/// </summary>
		//public static List<KPI_KpiEntity> ltKpis;
		public  List<string> ltKpis;

		/// <summary>
		/// 所有实时标签集合
		/// </summary>
		public  List<KPI_RealTagEntity> ltReals;

		/// <summary>
		/// 所有手录标签集合
		/// </summary>
        public List<InputTagEntity> ltInputs;


		/// <summary>
		/// 所有曲线标签集合
		/// </summary>
		public  List<CurveTagEntity> ltCurves;

		/// <summary>
		/// 指标计算周期
		/// </summary>
        public Dictionary<string, CycleEntity> dicCYs;


		/// <summary>
		/// 所有经济指标集合
		/// </summary>
        public List<ECTagEntity> ltECs;

		public  List<KPI_XLineEntity> ltXLines;

		public  List<KPI_ScoreEntity> ltScores;

		/// <summary>
		/// 所有安全指标集合
		/// </summary>
		public  List<KPI_SATagEntity> ltSAs;

		#endregion

		//////////////////////////////////////////////////////////////////////////////
		#region 运行参数

		/// <summary>
		/// 所有机组运行状态对照表
		/// </summary>
		public  Dictionary<string, bool> dicUnitStatus;

		/// <summary>
		/// 所有机组负荷对照表
		/// </summary>
		public  Dictionary<string, double> dicUnitPEs;


		/// <summary>
		/// 所有指标的标签、数值对照表，为计算公式使用：实时数据、经济指标
		/// </summary>
		public  Hashtable htTags;
		public  Dictionary<string, RealTag> dicRealTag;


		public  Dictionary<string, double> dicTags;

		#endregion

		////////////////////////////////////////////////////////////////////////////////////////

		#region Static Constuctor

		/// <summary>
		/// 
		/// </summary>
		public WS_KPIVar(){			
			bFirst = true;
			nOffset = 0;
		}

		#endregion

		//////////////////////////////////////////////////////////////////////////////
		#region Define Functions

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public  bool KPIInitialVar() {
			try {
				//是否取服务器时间
				bTimeMode = KPI_SystemDal.GetKPITimeMode() == 1 ? true : false;
				//偏置时间
				nOffset = KPI_SystemDal.GetKPIOffset();
				//初始所有List<>
				ltUnits = KPI_UnitDal.GetValidEntity();
				ltSeqs = KPI_SeqDal.GetValidEntity();
                ltKpis = KpiDal.GetValidEntity();

				//实时、手录、曲线指标
				ltReals = KPI_RealTagDal.GetAllEntity();
				ltInputs = KPI_InputTagDal.GetAllEntity();
                ltCurves = CurveTagDal.GetAllEntity();
				//计算周期
                List<CycleEntity> ltCYs = CycleDal.GetAllEntity();
                dicCYs = new Dictionary<string, CycleEntity>();
                foreach (CycleEntity cye in ltCYs) {
					dicCYs[cye.CycleID] = cye;
				}

				//经济指标
                ltECs = ECTagDal.GetValidEntity();
                ltXLines = ECTagDal.GetAllXLineEntity();
                ltScores = ECTagDal.GetAllScoreEntity();
				/////////////////////////////////////////////////////////////////
				//安全指标
				dicUnitStatus = new Dictionary<string, bool>();
				dicUnitPEs = new Dictionary<string, double>();
				dicTags = new Dictionary<string, double>();
				dicRealTag = new Dictionary<string, RealTag>();
			}
			catch (Exception ex) {
				LogUtil.LogMessage(ex.ToString());

				return false;
			}

			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bFirst"></param>
		/// <returns></returns>
		public  bool KPISetReload() {
			try {
				return KPI_SystemDal.GetKPIReload();
			}
			catch (Exception ex) {
				LogUtil.LogMessage(ex.ToString());

				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bFirst"></param>
		/// <returns></returns>
		public  bool KPIOffReload() {
			try {
				KPI_SystemDal.UpdateByName("KPIReload", "1", "0", "");
				return true;
			}
			catch (Exception ex) {
				LogUtil.LogMessage(ex.ToString());
				return false;
			}
		}


		public  List<KPI_RedoEntity> KPIGetRedos() {
			List<KPI_RedoEntity> lredo = new List<KPI_RedoEntity>();
			//获得所有有效且没有被计算过的集合
			lredo = KPI_RedoDal.GetAllEntity();
			return lredo;
		}

		#endregion

	}
}
