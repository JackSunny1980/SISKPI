using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

using SIS.Loger;
using SIS.DataAccess;
using SIS.DataEntity;
using SIS.DBControl;
using SIS.Arithmetic;

namespace SIS.Assistant.WS {
	 
	/// <summary>
	/// 指标考核计算
	/// </summary>
	public class WS_KPISubMethod:IDisposable {

		//Added by pyf 2013-09-10
		private readonly string KPIValue_TableName = "KPI_ECSSSnapshot";
		private readonly string TagValue_TableName = "KPI_RealValue";
		private  RTInterface m_RTDataAccess;
		private WS_KPIVar m_KPIVar;
		private ExpDone m_ExpressionParse;
		private WS_KPIDBClient m_DBClient;		
		private DataTable m_KPIValueTable;
		private DataTable m_TagValueTable;
		//End of Added.
		/// <summary>
		/// 
		/// </summary>
		public  int KIsTest = 0;

		public WS_KPISubMethod() {
			m_RTDataAccess = DBAccess.GetRealTime();
			m_KPIVar = new WS_KPIVar();
			m_ExpressionParse = new ExpDone();
			m_DBClient = new WS_KPIDBClient();
			m_KPIValueTable = m_DBClient.GetTableSchema("kpivalue");
			m_TagValueTable = m_DBClient.GetTableSchema("tagvalue");
		}

		/// <summary>
		/// 关系库是否连接正常
		/// </summary>
		/// <returns></returns>
		public  bool RelationConnection() {
			return DBAccess.GetRelation().Connection();
		}

		/// <summary>
		/// 实时库是否连接正常
		/// </summary>
		/// <returns></returns>
		public  bool RealTimeConnection() {
            return  m_RTDataAccess.Connection();
		}

		/// <summary>
		/// 是否满足系统滤波计算条件
		/// </summary>
		/// <returns></returns>
		public  bool CanRun() {
			if (!RelationConnection()) {
				LogUtil.LogMessage("关系库连接异常，终止指标考核计算");
				return false;
			}

			if (!m_DBClient.Connection()) {
				LogUtil.LogMessage("关系库连接异常，终止计算");
				return false;
			}
			return true;
		}

		#region KPI Real Calc 操作

		/// <summary>
		/// 计算KPI实时值
		/// </summary>
		/// <returns></returns>
		public  bool KPIRealCalc(bool bsnap) {			

			#region 系统初始化

			///////////////////////////////////////////////////////////////////////////////////                
			//0. 如果重启服务，则进行变量初始化
			if (!m_KPIVar.bFirst && m_KPIVar.KPISetReload()) {
				m_KPIVar.bFirst = true;
			}

			////////////////////////////////////////////////////////////////////////////////////////////
			//1. 系统参数初始化
			if (m_KPIVar.bFirst) {
				if (!m_KPIVar.KPIInitialVar()) {
					LogUtil.LogMessage("系统初始化错误!");
					return false;
				}				
			}

			///////////////////////////////////////////////////////////////////////////////////////////
			//历史计算单独在Recalculator Service中计算！！！
			////////////////////////////////////////////////////////////////////////////////////////////
			//2. 获取服务器的当前时间
			DateTime dtCurrentTime = DateTime.Now;
			if (KIsTest == 0 && m_KPIVar.bTimeMode) {
				//非测试环境时需要使用PI Server Time
				dtCurrentTime = m_RTDataAccess.GetServerTime();
			}

			//当前分钟
			DateTime dtCurrentMinute = dtCurrentTime.AddSeconds(dtCurrentTime.Second * -1);
			dtCurrentMinute = dtCurrentMinute.AddSeconds(m_KPIVar.nOffset * -1);
			//时间分钟
			String strCurrentMinute = dtCurrentMinute.ToString("yyyy-MM-dd HH:mm:00");

			//////////////////////////////////////////////
			//更新KPI System 时间
			KPI_SystemDal.SetKPISrvTime(strCurrentMinute);
			//////////////////////////////////////////////////////////////////////////////////
			//错误信息
			String strError = "";

			#endregion

			#region 获得实时、曲线、手动录入数据

			//////////////////////////////////////////////////////////////////////////////////////////////
			//Deleted by pyf 2013-09-10
            foreach (KPI_UnitEntity ue in m_KPIVar.ltUnits) {
                GetUnitRealState(ue, dtCurrentMinute);
            }
			//3 实时数据
			if (!GetUnitRealValue(dtCurrentMinute, out strError)) {
				LogUtil.LogMessage(strError);
				return false;
			}
			//End of Deleted.

			#endregion

			#region 机组循环
			//string BulkTable = "KPI_ECSSSnapshot";
			//DataTable dt = m_DBClient.GetTableSchema("kpivalue");
			foreach (KPI_UnitEntity ue in m_KPIVar.ltUnits) {
                if (!m_KPIVar.dicUnitStatus[ue.UnitID]) continue;

				#region 获得状态和值次

				///////////////////////////////////////////////////////////////////////
				//6.1 计算条件
				//机组状态和负荷
                //if (!GetUnitRealState(ue, dtCurrentMinute)) {
                //}

				///////////////////////////////////////////////////////////////////////
				//6.2 班次信息
				//获取值班信息
				String strWorkID = ue.WorkID;
				if (strWorkID == null || strWorkID == "") {
					continue;
				}

				//获取当前时间的值与班
				String strCurrentShift = "";
				String strCurrentPeriod = "";
				String strStartTime = "";
				String strEndTime = "";
				//获取班次与值次
				KPI_WorkDal.GetShiftAndPeriod(strWorkID, strCurrentMinute,
					ref strCurrentShift, ref strCurrentPeriod, ref strStartTime, ref strEndTime);

				#endregion

				///////////////////////////////////////////////////////////////////////
				//6.3 指标计算

				#region 指标循环

				//获取该机组、有效设备、有效指标集的所有指标
				var KPIList = from kpi in m_KPIVar.ltECs
							  where (kpi.UnitID == ue.UnitID)
							  //        && m_KPIVar.ltSeqs.Contains(kpi.SeqID) && m_KPIVar.ltKpis.Contains(kpi.KpiID))
							  orderby kpi.ECCalcClass
							  select kpi;


				////////////////////////////////////////////////////////////////////////////
				//实时数据
				//BulkWrite to SQL
				string ECTX = ""; 
				//指标计算
                foreach (ECTagEntity ecte in KPIList) {
					#region 计算周期

					//一律以0点开始的分钟、小时循环。
					//不按值开始的时间进行循环。

					//机组状态
					bool bRunning = m_KPIVar.dicUnitStatus[ue.UnitID];

					//获取周期
                    CycleEntity cye = m_KPIVar.dicCYs[ecte.CycleID];
					int ncyc = cye.CycleValue;
					int nmin = dtCurrentMinute.Hour * 60 + dtCurrentMinute.Minute;

					//Value Entity
                    ECSSValueEntity kpiECV = new ECSSValueEntity();
					
					if (cye.CycleName == "TN")//按班次计算
                    {
						//按班周期循环计算的
						if (KPICalcForTN(bsnap, ecte, cye, dtCurrentMinute, strCurrentShift, strCurrentPeriod, out kpiECV)) {

						}
					}
					else if (cye.CycleName == "TD") {
						//按日周期循环计算的
						if (KPICalcForTD(bsnap, ecte, cye, dtCurrentMinute, strCurrentShift, strCurrentPeriod, out kpiECV)) {

						}

					}
					else {
						//第一次时必须全部计算
						if (m_KPIVar.bFirst) {
							if (KPICalcForTM(bsnap, bRunning, ecte, dtCurrentMinute, strCurrentShift, strCurrentPeriod, out kpiECV)) {

							}

						}
						else {
							//按分周期循环计算的
							if (nmin % ncyc == 0) {
								if (KPICalcForTM(bsnap, bRunning, ecte, dtCurrentMinute, strCurrentShift, strCurrentPeriod, out kpiECV)) {

								}
							}
						}
					}


					#endregion

					#region Add to BulkTable

					///////////////////////////////////////////////////////
					
					//添加到DataTable
					if ((kpiECV != null) && (!String.IsNullOrEmpty(kpiECV.UnitID))) {
						ECTX += "'" + ecte.ECCode + "',";
						DataRow dr = m_KPIValueTable.NewRow();
						dr["SSID"] = kpiECV.SSID;
						dr["UnitID"] = kpiECV.UnitID;
						dr["SeqID"] = kpiECV.SeqID;
						dr["KpiID"] = kpiECV.KpiID;
						dr["ECID"] = kpiECV.ECID;
						dr["ECName"] = kpiECV.ECName;
						dr["ECTX"] = ecte.ECCode;
						dr["ECTime"] = kpiECV.ECTime;
						dr["ECValue"] = 0;
						if (kpiECV.ECValue != double.MinValue) {
							dr["ECValue"] = kpiECV.ECValue;
						}
						dr["ECOpt"] = 0;
						if (kpiECV.ECOpt != double.MinValue) {
							dr["ECOpt"] = kpiECV.ECOpt;
						}

						dr["ECOptExp"] = kpiECV.ECOptExp;
						dr["ECExpression"] = kpiECV.ECExpression;
						dr["ECScore"] = 0;
						if (kpiECV.ECScore != double.MinValue) {
							dr["ECScore"] = kpiECV.ECScore;
						}
						dr["ECQulity"] = 0;
						if (kpiECV.ECQulity != int.MinValue) {
							dr["ECQulity"] = kpiECV.ECQulity;
						}

						dr["ECPeriod"] = kpiECV.ECPeriod;
						dr["ECShift"] = kpiECV.ECShift;

						dr["ECIsRemove"] = 0;
						if (kpiECV.ECIsRemove != int.MinValue) {
							dr["ECIsRemove"] = kpiECV.ECIsRemove;
						}
						m_KPIValueTable.Rows.Add(dr);
					}
					#endregion

				}

				if (m_KPIValueTable.Rows.Count > 0) {
					//删除
					ECTX = ECTX.TrimEnd(',');
					m_DBClient.DeleteData(ue.UnitID, "",ECTX, KPIValue_TableName);
					//insert
					m_DBClient.BulkToDB(m_KPIValueTable, KPIValue_TableName);
				}
				m_KPIValueTable.Rows.Clear();
				//dt.Dispose();

				#endregion
                ECSSSnapshotDal.GenerateSnapshotCoalConsumption(ue.UnitID, ue.UnitName, strCurrentMinute);               
				LogUtil.LogMessage(ue.UnitName + "计算完毕!");
			}

			#endregion
			
			//第一次计算完成后在赋值为false
			m_KPIVar.bFirst = false;
			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns> 
		public  bool GetUnitRealValue(DateTime dtValid, out string strError) {
			strError = "";
			if (m_KPIVar.ltReals.Count <= 0) {
				strError = "没有实时标签点";
				return false;
			}
			String strCM = dtValid.ToString("yyyy-MM-dd HH:mm:00");

			#region 测试环境

			//当前只考虑了实时值（在调试环境下由机器自动生成随机数）
			if (KIsTest == 1) {
				//string BulkTable = "KPI_RealValue";
				//DataTable dt = m_DBClient.GetTableSchema("tagvalue");
				foreach (KPI_RealTagEntity rte in m_KPIVar.ltReals)//reltags
                    {
					Random rand = new Random();
					double dvtemp = rand.Next(500, 1000);
					String strCode = "'" + rte.RealCode.ToUpper().Trim() + "'";
					m_KPIVar.dicTags[strCode] = dvtemp;
					DataRow dr = m_TagValueTable.NewRow();
					dr["RVID"] = Guid.NewGuid().ToString();
					dr["UnitID"] = rte.UnitID;
					dr["RealID"] = rte.RealID;
					dr["RealCode"] = rte.RealCode;
					dr["RealDesc"] = rte.RealDesc;
					dr["RealEngunit"] = rte.RealEngunit;
					dr["RealTime"] = strCM;
					dr["RealValue"] = dvtemp;
					dr["RealQulity"] = "0";
					m_TagValueTable.Rows.Add(dr);
				}

				if (m_TagValueTable.Rows.Count > 0) {
					m_DBClient.DeleteData("", "","", TagValue_TableName);
					//插入
					m_DBClient.BulkToDB(m_TagValueTable, TagValue_TableName);
				}
				m_TagValueTable.Rows.Clear();
			}
			#endregion

			#region 生产环境

			if (KIsTest == 0) {
				Dictionary<String, SIS.DBControl.TagValue> lttvs = new Dictionary<String, SIS.DBControl.TagValue>();
				foreach (KPI_RealTagEntity rte in m_KPIVar.ltReals) {
					String strCode = rte.RealCode.ToUpper().Trim();
					lttvs[strCode] = new TagValue(strCode);
				}
				if (m_KPIVar.bFirst) {
					if (!m_RTDataAccess.SetPointList(lttvs, out strError)) {
						LogUtil.LogMessage("SetPointList Exception: " + strError);
						return false;
					}					
				}

				//获取实时数据
				if (!m_RTDataAccess.GetSnapshotListData(ref lttvs, out strError)) {
					LogUtil.LogMessage("GetSnapshotListData Exception: " + strError);
					return false;
				}

				//实时数据				
				//string BulkTable = "KPI_RealValue";
				//DataTable dt = m_DBClient.GetTableSchema("tagvalue");
                //m_KPIVar.ltReals.
				foreach (KPI_RealTagEntity rte in m_KPIVar.ltReals) {
                    //LogUtil.LogMessage("UnitID=" + rte.UnitID);                    
                    if (m_KPIVar.dicUnitStatus.ContainsKey(rte.UnitID)&&(!m_KPIVar.dicUnitStatus[rte.UnitID])) continue;
					String strCode = rte.RealCode.ToUpper().Trim();
					String strTagCode = "'" + rte.RealCode.ToUpper().Trim() + "'";
                    //LogUtil.LogMessage("TagCode=" + strTagCode);
					m_KPIVar.dicTags[strTagCode] = 0.0;
					if (lttvs.ContainsKey(strCode) && lttvs[strCode].TagQulity == 0) {
                        m_KPIVar.dicTags[strTagCode] =  double.Parse(lttvs[strCode].TagStringValue);
					}
					DataRow dr = m_TagValueTable.NewRow();
					dr["RVID"] = Guid.NewGuid().ToString();
					dr["UnitID"] = rte.UnitID;
					dr["RealID"] = rte.RealID;
					dr["RealCode"] = rte.RealCode;
					dr["RealDesc"] = rte.RealDesc;
					dr["RealEngunit"] = rte.RealEngunit;
					dr["RealTime"] = strCM;
					dr["RealValue"] = m_KPIVar.dicTags[strTagCode];//.ToString();
					dr["RealQulity"] = "0";
					m_TagValueTable.Rows.Add(dr);
				}

				if (m_TagValueTable.Rows.Count > 0) {
					m_DBClient.DeleteData("", "", "", TagValue_TableName);
					m_DBClient.BulkToDB(m_TagValueTable, TagValue_TableName);
				}
				m_TagValueTable.Rows.Clear();				
				lttvs.Clear();
				lttvs = null;				
			}

			#endregion
			return true;
		}

		/// <summary>
		/// 判断机组运行状态
		/// 获得机组有效负荷
		/// </summary>
		private  bool GetUnitRealState(KPI_UnitEntity ue, DateTime dtValid) {

			#region 测试环境

			if (KIsTest == 1) {
				m_KPIVar.dicUnitStatus[ue.UnitID] = true;
				Random rand = new Random();
				m_KPIVar.dicUnitPEs[ue.UnitID] = rand.Next(260, 300);
			}
			#endregion

			#region 生产环境

			if (KIsTest == 0) {
				//获得机组运行条件
				string expression = ue.UnitCondition;
				m_KPIVar.dicUnitStatus[ue.UnitID] = false;
				if (!String.IsNullOrEmpty(ue.UnitCondition)) {
                    double dexp = m_RTDataAccess.ExpCurrentValue(expression);
					m_KPIVar.dicUnitStatus[ue.UnitID] = false;
					if (dexp != double.MinValue && dexp > 0) {
						m_KPIVar.dicUnitStatus[ue.UnitID] = true;
					}
				}

				//获得机组负荷
				string tag = ue.UnitMWTag;
				m_KPIVar.dicUnitPEs[ue.UnitID] = 0.0;
				if (!String.IsNullOrEmpty(ue.UnitMWTag)) {
                    double dvalue = m_RTDataAccess.GetSnapshotValue(tag);
					if (dvalue != double.MinValue) {
						m_KPIVar.dicUnitPEs[ue.UnitID] = dvalue;
					}
				}
			}
			#endregion

			return true;
		}

		#endregion

		#region KPI Archive Calc 操作

		/// <summary>
		/// KPI_Archive的操作
		/// </summary>
		/// <returns></returns>
		public  bool KPIArchiveCalc() {

			#region 系统初始化

			////////////////////////////////////////////////////////////////////////////////////////////
			//1. 系统参数初始化
			m_KPIVar.bFirst = true;
			if (!m_KPIVar.KPIInitialVar()) {
				LogUtil.LogMessage("系统初始化错误!");
				return false;
			}

			////////////////////////////////////////////////////////////////////////////////////////////
			//2. 获取历史计算列表
			List<KPI_RedoEntity> lts = m_KPIVar.KPIGetRedos();
			if (lts.Count <= 0) {
				return true;
			}

			#endregion

			#region 机组循环

			/////////////////////////////////////////////////////////////////
			//0-	机组集,当前只有 机组有效................................
			////////////////////////////////////////////////////////////////
			//1-	设备集
			//2-	指标集
			//3-	经济指标
			//4-	安全指标
			foreach (KPI_RedoEntity redo in lts) {
				#region 初始化

				if (redo.RDIsCalced > 0) {
					continue;
				}

				////////////////////////////////////////////////////////////////////////////////

				//机组信息
				KPI_UnitEntity ue = new KPI_UnitEntity();
				foreach (KPI_UnitEntity ueone in m_KPIVar.ltUnits) {
					if (ueone.UnitID == redo.RDKPIID) {
						ue = ueone;
					}
				}

				if (ue == null) {
					continue;
				}

				DateTime dtST = DateTime.Parse(redo.RDStartTime);
				DateTime dtET = DateTime.Parse(redo.RDEndTime);

				//历史重算时一律使用数据库服务器时间
				DateTime dtServerTime = DateTime.Now;

				if (KIsTest == 0) {
					//非测试环境时需要使用PI Server Time
					//dtServerTime = m_RTDataAccess.GetServerTime();
				}

				//记录相关信息!!
				string strInfor = "开始计算机组:" + ue.UnitCode + ", " + ue.UnitName;
				strInfor += "\r\n" + "开始时间：" + redo.RDStartTime + ", 结束时间：" + redo.RDEndTime;

				LogUtil.LogMessage(strInfor);

				//初始化开始时间
				DateTime dtStartTime = dtST.AddSeconds(dtST.Second * -1);

				bool bFirst = true;
				bool bsnap = false;
				bool bCollect = redo.RDIsCollect == 1 ? true : false;

				#endregion

				#region 时间周期循环


				//计算时间必须小于结束服务器时间，服务器时间，本地时间
				for (DateTime dtCurrentMinute = dtStartTime;                   //开始
						(dtCurrentMinute < dtET
							&& dtCurrentMinute < dtServerTime
							&& dtCurrentMinute < DateTime.Now.AddMinutes(-2));  //判断
					dtCurrentMinute = dtCurrentMinute.AddMinutes(1))            //增加1分钟
                {

					//时间分钟
					String strCurrentMinute = dtCurrentMinute.ToString("yyyy-MM-dd HH:mm:00");

					Console.WriteLine("正在计算" + dtCurrentMinute.ToString("yyyy-MM-dd HH:mm:ss") + "指标数据。");

					#region 同一台机组的时间循环
					//////////////////////////////////////////////////////////////////////////////////
					//错误信息
					String strError = "";

					#region  获得历史数据

					//////////////////////////////////////////////////////////////////////////////////////////////
					//3 实时数据
					if (!GetUnitArchiveValue(ue, bFirst, dtCurrentMinute, bCollect, out strError)) {
						LogUtil.LogMessage(strError);
						bFirst = false;
						LogUtil.LogMessage(ue.UnitCode + "," + ue.UnitName + "历史数据无法获取!" + strError);
						continue;
					}
					//Console.WriteLine("获取测点历史数据成功。");
					#endregion


					#region 获得状态和值次

					///////////////////////////////////////////////////////////////////////
					//6.1 计算条件
					//机组状态和负荷
					if (!GetUnitArchiveState(ue, dtCurrentMinute)) {
					}
					//Console.WriteLine("获取机组状态和负荷历史数据成功。");
					///////////////////////////////////////////////////////////////////////
					//6.2 班次信息
					//获取值班信息
					String strWorkID = ue.WorkID;
					if (strWorkID == null || strWorkID == "") {
						bFirst = false;

						continue;
					}

					//获取当前时间的值与班
					String strCurrentShift = "";
					String strCurrentPeriod = "";
					String strStartTime = "";
					String strEndTime = "";

					KPI_WorkDal.GetShiftAndPeriod(strWorkID, strCurrentMinute,
						ref strCurrentShift, ref strCurrentPeriod, ref strStartTime, ref strEndTime);
					//Console.WriteLine("获取值与班数据成功。");

					#endregion

					///////////////////////////////////////////////////////////////////////
					//6.3 指标计算

					//获取该机组、有效设备、有效指标集的所有指标
					var kpiresult = from kpi in m_KPIVar.ltECs
									where (kpi.UnitID == ue.UnitID) //&&(kpi.ECIndex==1028)
									//        && m_KPIVar.ltSeqs.Contains(kpi.SeqID) && m_KPIVar.ltKpis.Contains(kpi.KpiID))
									orderby kpi.ECCalcClass
									select kpi;

					//Console.WriteLine("获取该机组、有效设备、有效指标集的所有指标成功。");
					////////////////////////////////////////////////////////////////////////////
					//历史数据
					string ECTX = "";  //删除数据时使用，IN 查询
					//BulkWrite to SQL
					string BulkTable = "KPI_ECSSArchive";
					DataTable dt = m_DBClient.GetTableSchema("kpivalue");

					#region 指标循环

					//指标计算
                    foreach (ECTagEntity ecte in kpiresult) {
						#region 计算周期

						//一律以0点开始的分钟、小时循环。
						//不按值开始的时间进行循环。

						//机组状态
						bool bRunning = m_KPIVar.dicUnitStatus[ue.UnitID];
						//Console.WriteLine("ECIndex=" + ecte.ECIndex);
						//获取周期
                        CycleEntity cye = m_KPIVar.dicCYs[ecte.CycleID];
						int ncyc = cye.CycleValue;
						int nmin = dtCurrentMinute.Hour * 60 + dtCurrentMinute.Minute;

						//Value Entity
                        ECSSValueEntity kpiECV = new ECSSValueEntity();

						if (cye.CycleName == "TN") {
							//按班周期循环计算的
							if (KPICalcForTN(bsnap, ecte, cye, dtCurrentMinute, strCurrentShift, strCurrentPeriod, out kpiECV)) {

							}

						}
						else if (cye.CycleName == "TD") {
							//按日周期循环计算的
							if (KPICalcForTD(bsnap, ecte, cye, dtCurrentMinute, strCurrentShift, strCurrentPeriod, out kpiECV)) {

							}

						}
						else {
							//第一次时必须全部计算
							if (bFirst) {
								if (KPICalcForTM(bsnap, bRunning, ecte, dtCurrentMinute, strCurrentShift, strCurrentPeriod, out kpiECV)) {

								}

							}
							else {
								//按分周期循环计算的
								if (nmin % ncyc == 0) {
									if (KPICalcForTM(bsnap, bRunning, ecte, dtCurrentMinute, strCurrentShift, strCurrentPeriod, out kpiECV)) {

									}
								}
							}
						}


						#endregion

						#region Add to BulkTable

						///////////////////////////////////////////////////////
						//添加到DataTable
						//Console.WriteLine("指标得分计算完成,准备写数据库。");
						if ((kpiECV != null) && (!String.IsNullOrEmpty(kpiECV.UnitID))) {
							ECTX += "'" + ecte.ECCode + "',";
							
							DataRow dr = dt.NewRow();
							dr["SSID"] = kpiECV.SSID;
							dr["UnitID"] = kpiECV.UnitID;
							dr["SeqID"] = kpiECV.SeqID;
							dr["KpiID"] = kpiECV.KpiID;
							dr["ECID"] = kpiECV.ECID;
							dr["ECName"] = kpiECV.ECName;
							dr["ECTX"] = ecte.ECCode;
							dr["ECTime"] = kpiECV.ECTime;
							//dr["ECValue"] = kpiECV.ECValue;
							//dr["ECOpt"] = kpiECV.ECOpt
							if (kpiECV.ECValue == double.MinValue) {
								dr["ECValue"] = 0;// System.DBNull.Value;
							}
							else {
								dr["ECValue"] = kpiECV.ECValue;
							}
							if (kpiECV.ECOpt == double.MinValue) {
								dr["ECOpt"] = System.DBNull.Value;
							}
							else {
								dr["ECOpt"] = kpiECV.ECOpt;
							}

							dr["ECOptExp"] = "";// kpiECV.ECOptExp;
							dr["ECExpression"] = kpiECV.ECExpression;

							//dr["ECScore"] = kpiECV.ECScore;
							//dr["ECQulity"] = kpiECV.ECQulity;

							if (kpiECV.ECScore == double.MinValue) {
								dr["ECScore"] = 0;// System.DBNull.Value;
							}
							else {
								dr["ECScore"] = kpiECV.ECScore;
							}
							if (kpiECV.ECQulity == int.MinValue) {
								dr["ECQulity"] = 0;// System.DBNull.Value;
							}
							else {
								dr["ECQulity"] = kpiECV.ECQulity;
							}

							dr["ECPeriod"] = kpiECV.ECPeriod;
							dr["ECShift"] = kpiECV.ECShift;

							//dr["ECIsRemove"] = kpiECV.ECIsRemove;
							if (kpiECV.ECIsRemove == int.MinValue) {
								dr["ECIsRemove"] = 0;// System.DBNull.Value;
							}
							else {
								dr["ECIsRemove"] = kpiECV.ECIsRemove;
							}
							
							dt.Rows.Add(dr);
						}
						bFirst = false;
						#endregion
					}
					//Console.WriteLine("ECTX=" + ECTX);
					//Console.WriteLine("指标得分计算完成。");
					if (dt.Rows.Count > 0) {
						//删除
						//Console.WriteLine("ECTX=" + ECTX);
						ECTX = ECTX.TrimEnd(',');
						//Console.WriteLine("删除历史数据完成。");
						m_DBClient.DeleteData(ue.UnitID, strCurrentMinute, ECTX, BulkTable);
						//insert
						m_DBClient.BulkToDB(dt, BulkTable);
					}
					dt.Dispose();
					//Console.WriteLine("写数据库完成。");
					#endregion
                    ECSSSnapshotDal.GenerateArchiveCoalConsumption(ue.UnitID, ue.UnitName, strCurrentMinute);
					LogUtil.LogMessage(ue.UnitName + ":  " + strCurrentMinute + "计算完毕!");
					if (KIsTest == 0) {
						//非测试环境时需要使用PI Server Time
						//dtServerTime = m_RTDataAccess.GetServerTime();
					}
					//for 
					//dtCurrentMinute 自动加 1 分钟。。。。。。。。。。。。。

					#endregion

				}
				LogUtil.LogMessage(ue.UnitCode + "," + ue.UnitName + "计算完毕!");

				#endregion

				////////////////////////////////////////////////////////////////////////////////
				//更新数据库
				redo.RDIsValid = 0;
				redo.RDIsCalced = 1;
				redo.RDCalcedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				KPI_RedoDal.Update(redo);
			}
			#endregion
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns> 
		public bool GetUnitArchiveValue(KPI_UnitEntity ue, bool bFirst, DateTime dtValid, bool bCollect, out string strError) {
			strError = "";
			if (m_KPIVar.ltReals.Count <= 0) {
				strError = "没有标签点";
				return false;
			}

			//不过滤了，全部一次取完！！！！
			//过滤当前机组的Tags
			var reltags = from kpi in m_KPIVar.ltReals
						  where (kpi.UnitID == ue.UnitID)
						  select kpi;

			if (reltags.Count() <= 0) {
				strError = "该机组的标签点为0";
				return false;
			}

			String strCM = dtValid.ToString("yyyy-MM-dd HH:mm:00");
			//当前只考虑了实时值。
			#region 测试环境

			if (KIsTest == 1) {
				string BulkTable = "KPI_ArchiveValue";
				DataTable dt = m_DBClient.GetTableSchema("tagvalue");
				foreach (KPI_RealTagEntity rte in m_KPIVar.ltReals) {
					Random rand = new Random();
					double dvtemp = rand.Next(500, 1000);
					String strCode = "'" + rte.RealCode.ToUpper().Trim() + "'";
					m_KPIVar.dicTags[strCode] = dvtemp;
					DataRow dr = dt.NewRow();

					dr["RVID"] = Guid.NewGuid().ToString();
					dr["UnitID"] = rte.UnitID;
					dr["RealID"] = rte.RealID;
					dr["RealCode"] = rte.RealCode;
					dr["RealDesc"] = rte.RealDesc;
					dr["RealEngunit"] = rte.RealEngunit;
					dr["RealTime"] = strCM;
					dr["RealValue"] = dvtemp;
					dr["RealQulity"] = "0";
					dt.Rows.Add(dr);
				}
				if (dt.Rows.Count > 0) {
					//删除
					m_DBClient.DeleteData(ue.UnitID, strCM,"", BulkTable);
					//插入
					m_DBClient.BulkToDB(dt, BulkTable);
				}
				dt.Dispose();
			}
			#endregion

			#region 生产环境

			if (KIsTest == 0) {
				//从数据库获取数据
				if (bCollect) {
					#region 采集数据

					Dictionary<String, SIS.DBControl.TagValue> lttvs = new Dictionary<String, SIS.DBControl.TagValue>();
					foreach (KPI_RealTagEntity rte in reltags) {
						//点名
						String strCode = rte.RealCode.ToUpper().Trim();
						lttvs[strCode] = new TagValue(strCode);
					}

					//每个机组只添加一次
					//if (bFirst) {
					//    if (!m_RTDataAccess.SetPointList(lttvs, out strError)) {
					//        LogUtil.LogMessage("SetPointList Exception: " + strError);
					//        return false;
					//    }					
					//}

					//获取历史数据
					//if (!DBAccess.GetRealTime().GetSnapshotListData(ref lttvs, out strError))
					if (!m_RTDataAccess.GetArchiveListData(ref lttvs, dtValid, out strError)) {
						LogUtil.LogMessage("GetArchiveListData Exception: " + strError);
						return false;
					}

					#endregion

					#region BulkTable

					//历史数据
					//BulkWrite to SQL
					string BulkTable = "KPI_ArchiveValue";
					DataTable dt = m_DBClient.GetTableSchema("tagvalue");
					foreach (KPI_RealTagEntity rte in m_KPIVar.ltReals) {
						//点名
						String strCode = rte.RealCode.ToUpper().Trim();
						String strTagCode = "'" + rte.RealCode.ToUpper().Trim() + "'";

						//更新Dictory
						m_KPIVar.dicTags[strTagCode] = 0.0;
						if (lttvs.ContainsKey(strCode) && lttvs[strCode].TagQulity == 0) {
							m_KPIVar.dicTags[strTagCode] = double.Parse(lttvs[strCode].TagStringValue);
						}
						DataRow dr = dt.NewRow();
						dr["RVID"] = Guid.NewGuid().ToString();
						dr["UnitID"] = rte.UnitID;
						dr["RealID"] = rte.RealID;
						dr["RealCode"] = rte.RealCode;
						dr["RealDesc"] = rte.RealDesc;
						dr["RealEngunit"] = rte.RealEngunit;
						dr["RealTime"] = strCM;
						dr["RealValue"] = m_KPIVar.dicTags[strTagCode];//.ToString();
						dr["RealQulity"] = "0";
						dt.Rows.Add(dr);
					}

					if (dt.Rows.Count > 0) {
						//删除
						m_DBClient.DeleteData(ue.UnitID, strCM, "",BulkTable);
						//插入
						m_DBClient.BulkToDB(dt, BulkTable);
					}

					dt.Dispose();
					lttvs.Clear();
					lttvs = null;
					#endregion
				}
				else {
					//从SQL数据库中查询获得只查询，不再写入了
					List<ValueEntity> lrvs = ArchiveValueDal.GetValuesByTime(ue.UnitID, strCM);
                    foreach (ValueEntity rte in lrvs) {
						//点名
						String strCode = rte.RealCode.ToUpper().Trim();
						String strTagCode = "'" + rte.RealCode.ToUpper().Trim() + "'";
						//更新Dictory
						m_KPIVar.dicTags[strTagCode] = rte.RealValue;
					}

				}
			}
			#endregion
			return true;
		}

		/// <summary>
		/// 判断机组运行状态
		/// 获得机组有效负荷
		/// </summary>
		private bool GetUnitArchiveState(KPI_UnitEntity ue, DateTime dtValid) {
			
			#region 测试环境
			if (KIsTest == 1) {				
				m_KPIVar.dicUnitStatus[ue.UnitID] = true;
				Random rand = new Random();
				m_KPIVar.dicUnitPEs[ue.UnitID] = rand.Next(260, 300);
			}
			#endregion

			#region 生产环境

			if (KIsTest == 0) {				
				////////////////////////////////////////////////////////////////////////////////
				//获得机组运行条件
				string expression = ue.UnitCondition;
				m_KPIVar.dicUnitStatus[ue.UnitID] = false;
				if (!String.IsNullOrEmpty(ue.UnitCondition)) {	
					double dexp = m_RTDataAccess.TimedCalculate(expression, dtValid);
					m_KPIVar.dicUnitStatus[ue.UnitID] = false;
					if (dexp != double.MinValue && dexp > 0) {
						m_KPIVar.dicUnitStatus[ue.UnitID] = true;
					}				
				}

				////////////////////////////////////////////////////////////////////////////////
				//获得机组负荷
				string tag = ue.UnitMWTag;
				m_KPIVar.dicUnitPEs[ue.UnitID] = 0.0;
				if (!String.IsNullOrEmpty(ue.UnitMWTag)) {	
					double dvalue = m_RTDataAccess.GetArchiveValue(tag, dtValid);
					m_KPIVar.dicUnitPEs[ue.UnitID] = 0.0;
					if (dvalue != double.MinValue) {
						m_KPIVar.dicUnitPEs[ue.UnitID] = dvalue;
					}				
				}
			}
			#endregion
			return true;
		}


		#endregion

		#region KPI Calc 公共操作

		/// <summary>
		/// KPI_Snapshot And KPI_Archive的操作
		/// xmin 周期的指标计算
		/// </summary>
		/// <returns></returns>
        public bool KPICalcForTM(bool bsnap, bool bRunning, ECTagEntity ecte, DateTime dtValid,
										string strCurrentShift, string strCurrentPeriod,
                                        out ECSSValueEntity kpiEV) {
			//bsnap== false时，更新 EC_SSArchive即可。
			if (bRunning == false) {
				kpiEV = null;
				return false;
			}
            string strCM = dtValid.ToString("yyyy-MM-dd HH:mm:00");
            bool bRight = true;
            double dResult = 0.0;
            String strResult = "";
            String strOptExp = "";
            double dScore = 0.0;
            int nAlarm = 0;		
            if ((String.IsNullOrWhiteSpace(ecte.ECCalcExp)) && (!String.IsNullOrWhiteSpace(ecte.ECScoreExp))) {
                String strresult = "";
                String  strexpression="";
                double result = 0;
                Dictionary<String, double> dcexp = new Dictionary<string, double>();
                m_ExpressionParse.ExpCalculate(ecte.ECScoreExp, dcexp, out result, out strresult, out strexpression);
                kpiEV = new ECSSValueEntity();
                kpiEV.SSID = PageControl.GetGuid();
                kpiEV.UnitID = ecte.UnitID;
                kpiEV.SeqID = ecte.SeqID;
                kpiEV.KpiID = ecte.KpiID;
                kpiEV.ECID = ecte.ECID;
                kpiEV.ECName = ecte.ECName;
                kpiEV.ECTime = strCM;
                kpiEV.ECValue = 0;
                kpiEV.ECScore = result;
                kpiEV.ECOptExp = "";
                kpiEV.ECExpression = strexpression;
                kpiEV.ECQulity = 0;
                kpiEV.ECPeriod = strCurrentPeriod;
                kpiEV.ECShift = strCurrentShift;
                return true;
            }
			try {	

				#region 指标计算

				////////////////////////////////////////////////////////////////////////////////
				//指标计算
				//获得计算表达式
				string expression = ecte.ECCalcExp.ToUpper().Trim();
				string strexpression = "";

				if (expression == null || expression == "") {
					LogUtil.LogMessage(ecte.ECCode + ecte.ECName + "计算公式为空!");
					kpiEV = null;
					return true;
				}

				//解析计算表达式：获得标签及指标
				Dictionary<String, double> dic1 = new Dictionary<String, double>();
				if (m_ExpressionParse.ExpEvaluate(expression, ref dic1) != 0) {
					LogUtil.LogMessage(ecte.ECCode + ecte.ECName + "指标解析错误,确保实时标签存在，计算指标提前计算:" + ecte.ECCalcExp);
					bRight = false;
					dic1 = null;
				}
				else {
					//执行计算表达式: 计算变量赋值与执行
					for (int i = 0; i < dic1.Count; i++) {
						String strkey = dic1.ElementAt(i).Key.ToUpper().Trim();
						dic1[strkey] = m_KPIVar.dicTags[strkey];
					}
					//strexpression只在此使用，保证存储到数据库中
					if (m_ExpressionParse.ExpCalculate(expression, dic1, out dResult, out strResult, out strexpression) != 0) {
						LogUtil.LogMessage(ecte.ECCode + ecte.ECName + "指标计算错误:" + ecte.ECCalcExp);
						dResult = 0.0;
						bRight = false;
					}
					dic1.Clear();


					//是否负值归零
					if (ecte.ECIsZero == 1 && dResult < 0) {
						dResult = 0;
					}
					////////////////////////////////////////////////////////////////////////////////
					//指标赋值
					//该指标添加到dicTags集合，其他指标使用时
					String strCode = "'" + ecte.ECCode.ToUpper().Trim() + "'";
					m_KPIVar.dicTags[strCode] = bRight ? dResult : 0.0;
				}
				//Console.WriteLine("指标计算结束");
				#endregion

				#region 得分计算

				if (bRight && bRunning) {
					/////////////////////////////////////////////////////////////////////////////////////
					//经济指标目标值计算
					if (ecte.ECIsSnapshot == 1) {
						//机组负荷，曲线默认基准值。
						double dUnitPE = m_KPIVar.dicUnitPEs[ecte.UnitID];

						#region 系数计算

						//////////////////////////////////////////////
						//@ref
						m_KPIVar.dicTags["@REF"] = dResult;

						//获得所有的得分计算公式
						var scoreresult = from kpi in m_KPIVar.ltScores
										  where (kpi.ECID == ecte.ECID && kpi.ScoreIsValid == 1)
										  select kpi;

						/////////////////////////////////////////////
						//@a1, @a2, @a3, @a4, @a5, @a6, @a7, @a8
						if (scoreresult.Count() > 0 && ecte.ECXLineType >= 0) {
							//限定该指标的 相关曲线
							var xlresult = (from kpi in m_KPIVar.ltXLines
											where kpi.ECID == ecte.ECID
											orderby kpi.XLineCoef
											select kpi).ToList();

							if (xlresult.Count() > 0) {
								var coefresult = (from kpi in xlresult
												  orderby kpi.XLineCoef
												  select kpi.XLineCoef).Distinct().ToList();

								//只有一维(1)类型的区间可以有@a1,@a2,@a3,@a4, @a5添加, 定值及二维(0,2)类型的只能有1个@a1
								//int num = ecte.ECXLineType == 1 ? coefresult.Count : 1;
								int num = coefresult.Count;

								//获得基准值
								double dXBase = dUnitPE;
								string xtag = "'" + xlresult[0].XLineXBase.ToUpper().Trim() + "'";
								if (ecte.ECXLineXRealTag != "" && m_KPIVar.dicTags.ContainsKey(xtag)) {
									dXBase = double.Parse(m_KPIVar.dicTags[xtag].ToString());
								}

								double dYBase = dUnitPE;
								string ytag = "'" + xlresult[0].XLineYBase.ToUpper().Trim() + "'";
								if (ecte.ECXLineYRealTag != "" && m_KPIVar.dicTags.ContainsKey(ytag)) {
									dYBase = double.Parse(m_KPIVar.dicTags[ytag].ToString());
								}

								//计算组别
								string sGroup = ecte.ECCurveGroup;

								//计算月份
								int nMonth = dtValid.Month;

								//计算系数
								double[] dAA = KPI_XLineDal.GetXLineCoefs(xlresult, num, ecte.ECXLineType, ecte.ECXLineGetType, dXBase, dYBase, sGroup, nMonth);

								if (num >= 1) {
									m_KPIVar.dicTags["@A1"] = dAA[0];
								}
								if (num >= 2) {
									m_KPIVar.dicTags["@A2"] = dAA[1];
								}
								if (num >= 3) {
									m_KPIVar.dicTags["@A3"] = dAA[2];
								}
								if (num >= 4) {
									m_KPIVar.dicTags["@A4"] = dAA[3];
								}
								if (num >= 5) {
									m_KPIVar.dicTags["@A5"] = dAA[4];
								}
								if (num >= 6) {
									m_KPIVar.dicTags["@A6"] = dAA[5];
								}
								if (num >= 7) {
									m_KPIVar.dicTags["@A7"] = dAA[6];
								}
								if (num >= 8) {
									m_KPIVar.dicTags["@A8"] = dAA[7];
								}
							}
						}

						//Console.WriteLine("得分计算结束");
						
						#endregion

						#region 区间计算

						//Console.WriteLine("得分区间计算结束");

						foreach (KPI_ScoreEntity kse in scoreresult) {

							#region 得分区间计算
							//////////////////////////////////////////////////////
							//获取得分表达式
							expression = kse.ScoreCalcExp.ToUpper();
							if (expression == null || expression == "") {
								continue;
							}

							//解析得分表达式：获得标签及指标
							Dictionary<String, double> dic2 = new Dictionary<String, double>();
							if (m_ExpressionParse.ExpEvaluate(expression, ref dic2) != 0) {
								LogUtil.LogMessage(ecte.ECCode + ecte.ECName + "指标的得分区间解析错误:" + kse.ScoreCalcExp);
								dic2 = null;
								continue;
							}

							//执行计算表达式: 计算变量赋值与执行
							for (int i = 0; i < dic2.Count; i++) {
								String strkey = dic2.ElementAt(i).Key;
								dic2[strkey] = m_KPIVar.dicTags[strkey];
							}

							bool bResult = false;
							if (m_ExpressionParse.ExpBool(expression, dic2, out bResult, out strResult) != 0) {
								LogUtil.LogMessage(ecte.ECCode + ecte.ECName + "指标的得分区间计算错误:" + kse.ScoreCalcExp);
								continue;
							}
							dic2.Clear();
							dic2 = null;
							//判断是否满足
							if (!bResult) {
								continue;
							}

							//判断是否报警
							if (kse.ScoreAlarm == 1) {
								nAlarm = 1;
							}
							//Console.WriteLine("分区间计算结束");
							#endregion

							#region 得分计算
							
							///////////////////////////////////////////////
							//得分计算
							expression = kse.ScoreGainExp.ToUpper();

							if (expression == null || expression == "") {
								continue;
							}

							//标签及指标解析
							Dictionary<String, double> dic3 = new Dictionary<String, double>();
							if (m_ExpressionParse.ExpEvaluate(expression, ref dic3) != 0) {
								LogUtil.LogMessage(ecte.ECCode + ecte.ECName + "指标的得分公式解析错误:" + kse.ScoreCalcExp);
								continue;
							}

							//计算变量赋值
							for (int i = 0; i < dic3.Count; i++) {
								String strkey = dic3.ElementAt(i).Key;
								dic3[strkey] = m_KPIVar.dicTags[strkey];
							}

							//标签及指标计算
							string strscore = "";
							if (m_ExpressionParse.ExpCalculate(expression, dic3, out dScore, out strResult, out strscore) != 0) {
								LogUtil.LogMessage(ecte.ECCode + ecte.ECName + "得分的得分公式计算错误:" + kse.ScoreGainExp);
								dScore = 0.0;
								dic3.Clear();
								dic3 = null;
								continue;
							}
							dic3.Clear();
							dic3 = null;
							if (dScore != double.MinValue) {
								dScore = dScore * Convert.ToDouble(ecte.ECWeight / ecte.ECDenom);
							}
							//如果计算了得分后就不用再计算后面的得分了。
							break;
							#endregion
						}
						//Console.WriteLine("得分计算结束");
						#endregion

						#region  最优区间计算
						
						foreach (KPI_ScoreEntity kse in scoreresult) {

							//只需要有效的最优区间
							if (kse.ScoreIsValid == 1 && kse.ScoreOptimal == 1) {
								//////////////////////////////////////////////////////
								//获取得分表达式
								strOptExp = kse.ScoreCalcExp.ToUpper();
								if (strOptExp != null && strOptExp != "") {
									//解析得分表达式：获得标签及指标
									Dictionary<String, double> dic4 = new Dictionary<String, double>();
									if (m_ExpressionParse.ExpEvaluate(strOptExp, ref dic4) != 0) {
										LogUtil.LogMessage(ecte.ECCode + ecte.ECName + "指标的最优区间解析错误:" + kse.ScoreCalcExp);
										break;
									}
									//执行计算表达式: 计算变量赋值与执行
									for (int i = 0; i < dic4.Count; i++) {
										String strkey = dic4.ElementAt(i).Key;
										dic4[strkey] = m_KPIVar.dicTags[strkey];
									}

									//生成最优区间表达式
									foreach (KeyValuePair<string, double> kvp in dic4) {
										if (kvp.Key == "@REF") {
											strOptExp = strOptExp.Replace(kvp.Key, "x");
										}
										else {
											strOptExp = strOptExp.Replace(kvp.Key, kvp.Value.ToString("0.00"));
										}
									}
									dic4.Clear();
									dic4 = null;
								}
								//搜索到最优区间后就结束。
								break;
							}
						}

						//Console.WriteLine("最优区间计算结束");
						#endregion
					}
					else {
						strOptExp = "非考核状态;";
						dScore = 0;
						nAlarm = 0;
					}
				}

				if (!bRunning) {
					if (ecte.ECIsSnapshot == 1) {
						strOptExp = "非考核状态;";
						dScore = Convert.ToDouble(ecte.ECWeight);
						nAlarm = 0;
					}
					else {
						strOptExp = "非考核状态;";
						dScore = 0; //ecte.ECWeight;
						nAlarm = 0;
					}
				}
				else if (!bRight) {
					strOptExp = "计算失败;";
					dScore = 0;
					nAlarm = 1;
				}

				#endregion

				#region 数据库
				

                ECSSValueEntity ssse = new ECSSValueEntity();
				ssse.SSID = PageControl.GetGuid();
				ssse.UnitID = ecte.UnitID;
				ssse.SeqID = ecte.SeqID;
				ssse.KpiID = ecte.KpiID;
				ssse.ECID = ecte.ECID;
				ssse.ECName = ecte.ECName;
				ssse.ECTime = strCM;
				ssse.ECValue = dResult == double.MinValue ? 0 : dResult;
				ssse.ECScore = dScore == double.MinValue ? 0 : dScore;
				ssse.ECOptExp = strOptExp;
				ssse.ECExpression = strexpression;
				ssse.ECQulity = nAlarm == int.MinValue ? 0 : nAlarm;
				ssse.ECPeriod = strCurrentPeriod;
				ssse.ECShift = strCurrentShift;
				ssse.ECIsRemove = 0;
                if ((!String.IsNullOrWhiteSpace(ecte.ECCode)) && (ecte.ECCode.ToUpper() == "GHND_SNDH")) {
                    ssse.ECScore = -1*ssse.ECValue;
                }

				///////////////////////////////////////////////////////////////
				//out   
				kpiEV = ssse;

				//if (bsnap)
				//{
				//    //写入Snapshot DB
				//    KPI_ECSSSnapshotEntity ssse = new KPI_ECSSSnapshotEntity();
				//    ssse.SSID = PageControl.GetGuid();
				//    ssse.UnitID = ecte.UnitID;
				//    ssse.SeqID = ecte.SeqID;
				//    ssse.KpiID = ecte.KpiID;
				//    ssse.ECID = ecte.ECID;
				//    ssse.ECName = ecte.ECName;
				//    ssse.ECTime = strCM;
				//    ssse.ECValue = dResult;
				//    ssse.ECScore = dScore;
				//    ssse.ECOptExp = strOptExp;
				//    ssse.ECExpression = strexpression;
				//    ssse.ECQulity = nAlarm;
				//    ssse.ECPeriod = strCurrentPeriod;
				//    ssse.ECShift = strCurrentShift;
				//    ssse.ECIsRemove = 0;

				//    KPI_ECSSSnapshotDal.DeleteTag(ssse.ECID);

				//    KPI_ECSSSnapshotDal.Insert(ssse);
				//}
				//else
				//{
				//    //写入Archive DB
				//    KPI_ECSSArchiveEntity ssse = new KPI_ECSSArchiveEntity();
				//    ssse.SSID = PageControl.GetGuid();
				//    ssse.UnitID = ecte.UnitID;
				//    ssse.SeqID = ecte.SeqID;
				//    ssse.KpiID = ecte.KpiID;
				//    ssse.ECID = ecte.ECID;
				//    ssse.ECName = ecte.ECName;
				//    ssse.ECTime = strCM;
				//    ssse.ECValue = dResult;
				//    ssse.ECScore = dScore;
				//    ssse.ECOptExp = strOptExp;
				//    ssse.ECExpression = strexpression;
				//    ssse.ECQulity = nAlarm;
				//    ssse.ECPeriod = strCurrentPeriod;
				//    ssse.ECShift = strCurrentShift;
				//    ssse.ECIsRemove = 0;

				//    KPI_ECSSArchiveDal.DeleteTag(ssse.ECID, strCM);

				//    KPI_ECSSArchiveDal.Insert(ssse);
				//}

				#endregion

				return true;

			}
			catch (Exception ex) {
                LogUtil.LogMessage("CalcTM:" + ecte.ECCode + ecte.ECName + ex.Message +
                    System.Environment.NewLine + ex.StackTrace);
				kpiEV = null;
				return false;
			}
		}

		/// <summary>
		/// KPI_Snapshot And KPI_Archive的操作
		/// </summary>
		/// <returns></returns>
        public bool KPICalcForTN(bool bsnap, ECTagEntity ecte, CycleEntity cye, DateTime dtValid,
										string strCurrentShift, string strCurrentPeriod,
                                        out ECSSValueEntity kpiEV) {
			//bsnap== false时，更新 EC_SSArchive即可。
			kpiEV = null;
			return true;
		}


		/// <summary>
		/// KPI_Snapshot And KPI_Archive的操作
		/// </summary>
		/// <returns></returns>
        public bool KPICalcForTD(bool bsnap, ECTagEntity ecte, CycleEntity cye, DateTime dtValid,
										string strCurrentShift, string strCurrentPeriod,
                                        out ECSSValueEntity kpiEV) {
			//bsnap== false时，更新 EC_SSArchive即可。
			kpiEV = null;
			return true;
		}

		#endregion


		public void Dispose() {
			//throw new NotImplementedException();
		}
	}
}
