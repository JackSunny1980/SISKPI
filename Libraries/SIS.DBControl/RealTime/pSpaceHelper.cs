using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using pSpaceCTLNET;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Configuration;

namespace SIS.DBControl {
	public class pSpaceHelper : RTInterface {
		#region DllImport


		[DllImport("pSpaceAPI.dll")]
		public extern static int psAPI_Server_GetTime(UInt16 handle, ref PS_TIME ps_tm);

		public struct PS_TIME {
			public UInt32 Second;	/* 秒 */
			public UInt16 Millisec;	/* 毫秒 */
		};

		#endregion

		#region 变量

		/// <summary>
		/// 数据库连接适配器
		/// </summary>
		private DbConnector dbConnector = null;

		/// <summary>
		/// 标记树
		/// </summary>
		private TagTree tagTree = null;

		/// <summary>
		/// 测点管理器
		/// </summary>
		private TagManager tagManager = null;

		/// <summary>
		/// 测点树的根节点
		/// </summary>
		private TagNode rootNode = null;

		/// <summary>
		/// 实时数据变更订阅器类
		/// </summary>
		private RealSubscriber realSubscriber = null;

		/// <summary>
		/// 错误信息
		/// </summary>
		private String _ErrorInfo = String.Empty;

		/// <summary>
		/// 根路径
		/// </summary>
		private String root_name = String.Empty;

		#endregion

		#region 属性

		/// <summary>
		/// 错误信息
		/// </summary>
		public String ErrorInfo {
			get { return _ErrorInfo; }
			set { _ErrorInfo = value; }
		}

		#endregion

		#region 构造函数

		private static pSpaceHelper _mInstance = null;

		public static pSpaceHelper Instance() {
			return Instance(false);
		}

		/// <summary>
		/// 为了缓存问题，需要执行关闭连接再打开连接
		/// </summary>
		/// <param name="needclose"></param>
		/// <returns></returns>
		public static pSpaceHelper Instance(bool needclose) {
			if (_mInstance == null) {
				_mInstance = new pSpaceHelper();
				_mInstance.Connection();
				//if (!_mInstance.Connection()) {
				//    _mInstance.Dispose();
				//    return null;
				//}
			}
			//if (needclose) {
			//    _mInstance.Dispose();
			//}
			//else {
			//    _mInstance = new pSpaceHelper();
			//}	

			return _mInstance;
		}

		public pSpaceHelper() {
			Common.StartAPI();
		}

		public void Dispose() {
            try {

                if (null != dbConnector && dbConnector.IsConnected()) {
                    dbConnector.Disconnect();
                }
                //Common.StopAPI();
            }
            catch (Exception) {

            }
			_mInstance = null;
			tagTree = null;
			tagManager = null;
			dbConnector = null;
			rootNode = null;
			realSubscriber = null;
			//Common.StopAPI();
		}
		#endregion

		#region 私有函数

		private void GetUserIDAndPwd(String userNameAndpassword, out String userID, out String pwd) {
			userID = String.Empty;
			pwd = String.Empty;
			if (!String.IsNullOrEmpty(userNameAndpassword)) {
				String[] strArray = userNameAndpassword.Split(';');
				if (null != strArray && 0 < strArray.Length && 2 == strArray.Length) {
					userID = strArray[0].Substring(strArray[0].IndexOf('=') + 1, strArray[0].Length - strArray[0].IndexOf('=') - 1).Trim();
					pwd = strArray[1].Substring(strArray[1].IndexOf('=') + 1, strArray[1].Length - strArray[1].IndexOf('=') - 1).Trim();
				}
			}
		}

		private Double ConvertToDouble(Object obj) {
			try {
				return Double.Parse(obj.ToString());
			}
			catch {
				return Double.MinValue;
			}
		}

		private Int32 ConvertToInt32(Object obj) {
			try {
				return Int32.Parse(obj.ToString());
			}
			catch {
				return Int32.MinValue;
			}
		}

		#endregion

		#region 数据库操作

		/// <summary>
		/// 是否连接实时库
		/// </summary>
		/// <param name="serverName">服务器名称（如：localhost)</param>
		/// <param name="userNameAndpassword">用户名及密码："UID=kkk;PWD="</param>
		/// <returns></returns>
		public Boolean Connection(String serverName, String userNameAndpassword) {
			try {
				if (null == dbConnector) {
					dbConnector = new DbConnector();
				}
				dbConnector.ServerName = serverName;
				String userName = String.Empty;
				String password = String.Empty;
				GetUserIDAndPwd(userNameAndpassword, out userName, out password);
				dbConnector.UserName = userName;
				dbConnector.Password = password;
				dbConnector.TimeOut = 10;  //超时时间，单位为秒
				if (null != dbConnector && dbConnector.IsConnected()) {
					tagTree = TagTree.CreateInstance(dbConnector);
					tagManager = tagTree.GetMgr();
					rootNode = tagTree.GetTreeRoot();
					return true;
				}
				DbError error = dbConnector.Connect();
				if (error.HasErrors) {
					_ErrorInfo = "pSpaceApi类Connect函数出错:" + error.ErrorMessage + ",错误编码为： " + error.ErrorCode;
					return false;
				}
				tagTree = TagTree.CreateInstance(dbConnector);
				tagManager = tagTree.GetMgr();
				rootNode = tagTree.GetTreeRoot();
				return true;
			}
			catch (Exception e) {
				_ErrorInfo = "pSpaceApi类Connect函数异常:" + e.Message + " $$$ " + e.StackTrace;
				return false;
			}
		}

		/// <summary>
		/// 是否连接实时库
		/// </summary>
		/// <returns></returns>
		public Boolean Connection() {
			try {
				String serverName = ConfigurationManager.AppSettings["PIServer"];
				//格式："UID=kkk;PWD="
				String userNameAndpassword = ConfigurationManager.AppSettings["PIConnectionString"];
				if (null == dbConnector) {
					dbConnector = new DbConnector();
				}
				dbConnector.ServerName = serverName;
				String userName = String.Empty;
				String password = String.Empty;
				GetUserIDAndPwd(userNameAndpassword, out userName, out password);
				dbConnector.UserName = userName;
				dbConnector.Password = password;
				dbConnector.TimeOut = 10;  //超时时间，单位为秒
				if (null != dbConnector && dbConnector.IsConnected()) {
					tagTree = TagTree.CreateInstance(dbConnector);
					tagManager = tagTree.GetMgr();
					rootNode = tagTree.GetTreeRoot();
					return true;
				}
				DbError error = dbConnector.Connect();
				if (error.HasErrors) {
					_ErrorInfo = "pSpaceApi类Connect函数出错:" + error.ErrorMessage + ",错误编码为： " + error.ErrorCode;
					return false;
				}
				tagTree = TagTree.CreateInstance(dbConnector);
				tagManager = tagTree.GetMgr();
				rootNode = tagTree.GetTreeRoot();
				return true;
			}
			catch (Exception e) {
				_ErrorInfo = "pSpaceApi类Connect函数异常:" + e.Message + " $$$ " + e.StackTrace;
				return false;
			}
		}

		/// <summary>
		/// 获得服务器时间
		/// </summary>
		/// <returns></returns>
		public DateTime GetServerTime() {
			PS_TIME ps_tm = new PS_TIME();
			Int32 ret = psAPI_Server_GetTime(dbConnector.Handle, ref ps_tm);
			if (0 == ret) {
				return DateTime.Parse("1970-01-01 00:00:00").AddSeconds(ps_tm.Second).AddMilliseconds(ps_tm.Millisec);
			}
			else {
				return DateTime.Parse("1970-01-01 00:00:00");
			}
		}

		/// <summary>
		/// 关闭连接
		/// </summary>
		/// <returns></returns>
		public Boolean CloseConnect() {
			if (null == dbConnector) {
				return true;
			}
			if (null != dbConnector && dbConnector.IsConnected()) {
				dbConnector.Disconnect();
				return true;
			}
			return true;
		}

		#endregion

		#region Point操作

		/// <summary>
		/// 建点
		/// 注：添加到根节点上
		/// </summary>
		/// <param name="tagname"></param>
		/// <param name="isdigital"></param>
		/// <param name="desc"></param>
		/// <returns></returns>
		public Boolean AddPoint(string tagname, bool isdigital, object desc) {
			if (isdigital) {
				DigitalTagElement digitalTag = tagTree.CreateTag<DigitalTagElement>(tagname);
				digitalTag.Properties["Desc"] = desc;
				rootNode.AppendChild(digitalTag);
			}
			else {
				AnalogTagElement analogTag = tagTree.CreateTag<AnalogTagElement>(tagname);
				analogTag.Properties["Desc"] = desc;
				rootNode.AppendChild(analogTag);
			}
			return true;
		}

		/// <summary>
		/// 改点
		/// </summary>
		/// <param name="oKey"></param>
		/// <param name="nKey"></param>
		/// <returns></returns>
		public Boolean UpdatePoint(string oLongName, string newName) {
			try {
				TagTypeManager tagTypeManager = new TagTypeManager(dbConnector);
				ITag tag = tagManager.GetTagByName(oLongName, tagTypeManager.GetAllPropFields());
				tag["Name"] = newName;
				tag.Update();
				return true;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return false;
			}
		}

		/// <summary>
		/// 删点
		/// </summary>
		/// <param name="tagname"></param>
		/// <returns></returns>
		public Boolean DeletePoint(string tagname) {
			try {
				TagTypeManager tagTypeManager = new TagTypeManager(dbConnector);
				ITag tag = tagManager.GetTagByName(tagname, tagTypeManager.GetAllPropFields());
				if (0 == tagManager.RemoveTag(tag)) {
					return true;
				}
				else {
					return false;
				}
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return false;
			}
		}

		/// <summary>
		/// 某个点是否存在
		/// </summary>
		/// <param name="tagname">点名</param>
		/// <returns></returns>
		public Boolean ExistPoint(string tagname) {
			if (null != tagManager) {
				return tagManager.IsTagExist(tagname);
			}
			return false;
		}

		/// <summary>
		/// 判断标签是否是数字量
		/// </summary>
		/// <param name="tagname"></param>
		/// <returns></returns>
		public Boolean PointIsDigital(string tagname) {
			try {
				TagTypeManager tagTypeManager = new TagTypeManager(dbConnector);
				TagNode tag = tagManager.GetTagByName(tagname, tagTypeManager.GetAllPropFields()) as TagNode;
				if (null == tag) {
					return false;
				}
				else {
					return tag.TagType.TagTypeId == (UInt16)TagTypeEnum.DigitalType;
				}
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return false;
			}
		}

		/// <summary>
		/// 得到测点信息
		/// </summary>
		/// <param name="tagname"></param>
		/// <returns></returns>
		public TagInfo GetPointInfo(String tagname) {
			try {
				if (String.IsNullOrEmpty(tagname)) {
					return null;
				}

				TagTypeManager tagTypeManager = new TagTypeManager(dbConnector);
				TagNode tag = tagManager.GetTagByName(tagname, tagTypeManager.GetAllPropFields()) as TagNode;
				if (null == tag) {
					return null;
				}
				else {
					TagInfo info = new TagInfo();
					info.TagName = tag.TagName;
					info.TagIsDigital = tag.TagType.TagTypeId == (UInt16)TagTypeEnum.DigitalType;
					info.TagEngunit = tag.Properties["ENGINEERINGUNIT"].ToString();  //?
					info.TagDesc = tag.Properties["Desc"].ToString();   //?
					return info;
				}
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return null;
			}
		}

		/// <summary>
		/// 根据条件得到测点集合  测点信息
		/// 注：点名称
		/// </summary>
		/// <returns></returns>
		public List<TagInfo> GetPointInfoList(string filterexp) {
			try {
				List<TagInfo> tagInfoList = new List<TagInfo>();
				List<ITag> tagList = GetPointListByTagName(filterexp);
				foreach (ITag tag in tagList) {
					TagInfo taginfo = new TagInfo();
					taginfo.TagName = tag.TagName;
					taginfo.TagIsDigital = tag.TagType.TagTypeId == (UInt16)TagTypeEnum.DigitalType;
					taginfo.TagEngunit = tag.Properties["ENGINEERINGUNIT"].ToString();  //?
					taginfo.TagDesc = tag.Properties["Desc"].ToString();   //?
					tagInfoList.Add(taginfo);
				}
				return tagInfoList;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return null;
			}
		}

		/// <summary>
		/// 根据点名称获取点列表
		/// </summary>
		/// <param name="tagName"></param>
		/// <returns></returns>
		private TagVector GetPointListByTagName(String tagName) {
			try {
				TagQueryFilter filter = new TagQueryFilter();
				PropField propField = (from p in TagTypeSystem.BasePropFields
									   where p.PropName == "LongName"
									   select p).First<PropField>();

				//filter.Conditions.InsertAndCompileExpression(new TagQueryExpression(propField,
				//    TagQueryExpression.CompareOperator.Equal, tagName));
				List<PropField> list = new List<PropField>();
				list.Add(propField);
				//filter.StartNode = rootNode;
				//filter.Fields = null;
				ITag tag = tagManager.GetTagByName(tagName, list);
				if (null == tag) {
					return null;
				}
				filter = null;
				propField = null;
				list = null;
				TagVector tagVector = new TagVector();
				tagVector.Add(tag);
				return tagVector;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return null;
			}
		}

		/// <summary>
		/// 根据条件得到测点集合  测点属性之间逗号分隔 id,点名,描述,单位,数值
		/// </summary>
		/// <param name="flag">是否实时值</param>
		/// <returns></returns>
		public List<string> GetPointList(string filterexp, bool flag, DateTime time) {
			try {
				if (String.IsNullOrEmpty(filterexp) || !filterexp.Contains(",")) {
					return null;
				}

				String[] temp = filterexp.Split(',');
				if (null == temp || 0 >= temp.Length) {
					return null;
				}
				TagQueryFilter filter = new TagQueryFilter();
				PropField propField = (from p in TagTypeSystem.BasePropFields
									   where p.PropName == "Name"
									   select p).First<PropField>();

				filter.Conditions.InsertAndCompileExpression(new TagQueryExpression(propField,
					TagQueryExpression.CompareOperator.Equal, temp[0]));
				filter.StartNode = rootNode;
				filter.Fields = null;
				TagVector tagVector = GetPointListByTagName(temp[0]);
				if (null == tagVector) {
					return null;
				}
				else {
					List<String> result = new List<String>();
					if (flag) {
						RealDataSet realDataSet = new RealDataSet();
						BatchResults results = DataIO.Snapshot(dbConnector, tagVector, realDataSet);
						foreach (var item in realDataSet) {
							String tagName = tagVector.First(x => x.TagId == item.TagId).TagName;
							String tagStringValue = null != item.Value ? item.Value.ToString() : "0";
							String tagDesc = tagVector.First(x => x.TagId == item.TagId).Properties["Desc"].ToString();
							String tagEngunit = tagVector.First(x => x.TagId == item.TagId).Properties["ENGINEERINGUNIT"].ToString();

							result.Add(String.Format("{0},{1},{2},{3},{4}", item.TagId, tagName, tagDesc, tagEngunit, tagStringValue));
						}
					}
					else {
						HisDataSet realDataSet = new HisDataSet();
						BatchResults results = DataIO.ReadRaw(dbConnector, tagVector, time.AddSeconds(-1), time, realDataSet);
						foreach (var item in realDataSet) {
							String tagName = tagVector.First(x => x.TagId == item.TagId).TagName;
							String tagStringValue = "0";
							if (null != item.Data && 0 < item.Data.Count) {
								tagStringValue = null != item.Data[0].Value ? item.Data[0].Value.ToString() : "0";
							}
							String tagDesc = tagVector.First(x => x.TagId == item.TagId).Properties["Desc"].ToString();
							String tagEngunit = tagVector.First(x => x.TagId == item.TagId).Properties["ENGINEERINGUNIT"].ToString();

							result.Add(String.Format("{0},{1},{2},{3},{4}", item.TagId, tagName, tagDesc, tagEngunit, tagStringValue));
						}
					}
					return result;
				}
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return null;
			}
		}

		/// <summary>
		/// 根据条件得到测点集合 测点属性之间逗号分隔 点名,描述,单位
		/// </summary>
		/// <param name="WhereClause"></param>
		/// <returns></returns>
		public List<string> GetPointList(string filterexp) {
			return GetPointList(filterexp, true, DateTime.MinValue);
		}

		/// <summary>
		/// 通过弹出查询框得到测点集合
		/// </summary>
		/// <returns></returns>
		public List<string> GetPointList() {
			return null;
		}

		/// <summary>
		/// 通过条件查询得到符合手动录入的标签点。
		/// 注：只能用点名称  测点属性之间逗号分隔 点名,描述,单位
		/// </summary>
		/// <returns></returns>
		public List<TagValue> GetPointListForSDLR(string condition) {
			try {
				if (String.IsNullOrEmpty(condition) || !condition.Contains(',')) {
					return null;
				}
				String[] temp = condition.Split(',');
				if (null == temp || 0 >= temp.Length) {
					return null;
				}
				TagVector tagVector = GetPointListByTagName(temp[0]);
				if (null == tagVector) {
					return null;
				}
				else {
					RealDataSet realDataSet = new RealDataSet();
					BatchResults results = DataIO.Snapshot(dbConnector, tagVector, realDataSet);
					if (results.HasErrors) {
						foreach (DbError dberror in results.Errors) {
							_ErrorInfo += dberror.ErrorMessage;
						}
						return null;
					}
					List<TagValue> tagValList = new List<TagValue>();
					foreach (var item in realDataSet) {
						TagValue tagValue = new TagValue();
						tagValue.TagID = item.TagId;
						tagValue.TagName = tagVector.First(x => x.TagId == item.TagId).TagName;
						Double val = Double.MinValue;
						tagValue.TagSnapshot = (null != item.Value && Double.TryParse(item.Value.ToString(), out val)) ? val : Double.MinValue;
						tagValue.TagTime = item.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss");
						tagValue.TagQulity = (Int32)item.QualityStamp;
						tagValue.TagStringValue = null != item.Value ? item.Value.ToString() : Double.MinValue.ToString();
						tagValue.NewTime = String.Empty;
						tagValue.NewValue = double.MinValue;
						tagValue.TagDesc = tagVector.First(x => x.TagId == item.TagId).Properties["Desc"].ToString();
						tagValue.TagEngunit = tagVector.First(x => x.TagId == item.TagId).Properties["ENGINEERINGUNIT"].ToString();

						tagValList.Add(tagValue);
					}
					return null;
				}
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return null;
			}
		}

		#endregion

		#region Value读操作

		/// <summary>
		/// 判断标签值是否正常
		/// </summary>
		/// <param name="tagname"></param>
		/// <returns></returns>
		public bool PointValueIsGood(string tagname) {
			try {
				if (String.IsNullOrEmpty(tagname)) {
					return false;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null != tagVector && 0 < tagVector.Count) {
					RealHisData data = DataIO.Snapshot(dbConnector, tagVector[0] as ITagElement);
					if (null != data && data.QualityStamp == QUALITY_MASK.GOOD) {
						return true;
					}
				}
				return false;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return false;
			}
		}

		/// <summary>
		/// 得到实时数据，返回double类型
		/// </summary>
		/// <param name="tagname"></param>
		/// <returns></returns>
		public Double GetSnapshotValue(String tagname) {
			String strError = "";
			double Result = double.MinValue;
			TagVector tagVector = GetPointListByTagName(tagname);
			RealDataSet realDataSet = new RealDataSet();
			BatchResults results = DataIO.Snapshot(dbConnector, tagVector, realDataSet);
			if (results.HasErrors) {
				foreach (DbError dberror in results.Errors) {
					strError += dberror.ErrorMessage;
				}
				if (!String.IsNullOrEmpty(strError)) throw new Exception(strError);
			}
			if (realDataSet.Count > 0) {
				Result = Convert.ToDouble(realDataSet[0].Value);
			}
			tagVector = null;
			realDataSet = null;
			results = null;
			return Result;
			/*try
			{
				if (String.IsNullOrEmpty(tagname))
				{
					return Double.MinValue;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null != tagVector && 0 < tagVector.Count)
				{
					RealHisData data = DataIO.Snapshot(dbConnector, tagVector[0] as ITagElement);
					if (null != data && data.QualityStamp == QUALITY_MASK.GOOD)
					{
						return ConvertToDouble(data.Value);
					}
				}
				return Double.MinValue;
			}
			catch (Exception ex)
			{
				_ErrorInfo = ex.Message;
				return Double.MinValue;
			}*/
		}


		/// <summary>
		/// 得到字符串类型的的实时值
		/// </summary>
		/// <param name="tagname"></param>
		/// <returns></returns>
		public string GetSnapshotStringValue(string tagname) {
			try {
				if (String.IsNullOrEmpty(tagname)) {
					return String.Empty;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null != tagVector && 0 < tagVector.Count) {
					RealHisData data = DataIO.Snapshot(dbConnector, tagVector[0] as ITagElement);
					if (null != data && data.QualityStamp == QUALITY_MASK.GOOD) {
						return data.Value.ToString();
					}
				}
				return String.Empty;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return String.Empty;
			}
		}

		/// <summary>
		/// 得到实时数据，返回double类型及数值时间
		/// </summary>
		/// <param name="tagname"></param>
		/// <returns></returns>
		public double GetSnapshotValue(string tagname, out object timeStamp) {
			timeStamp = DateTime.MinValue;
			try {
				if (String.IsNullOrEmpty(tagname)) {
					return Double.MinValue;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null != tagVector && 0 < tagVector.Count) {
					RealHisData data = DataIO.Snapshot(dbConnector, tagVector[0] as ITagElement);
					if (null != data && data.QualityStamp == QUALITY_MASK.GOOD) {
						timeStamp = data.TimeStamp;
						return ConvertToDouble(data.Value);
					}
				}
				return Double.MinValue;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return Double.MinValue;
			}
		}

		/// <summary>
		/// 将实时数据库的PointList赋值
		/// 注：pSpace 不需要此操作
		/// </summary>
		/// <param name="lttvs">键为长名称</param>
		/// <returns></returns>
		public bool SetPointList(Dictionary<String, TagValue> lttvs, out string strError) {
			try {
				strError = string.Empty;
				if (null == lttvs || 0 >= lttvs.Count) {
					strError = "字典参数为空。";
					return false;
				}
				foreach (String str in lttvs.Keys) {
					if (!tagManager.IsTagExist(str)) {
						strError += str + ",";
					}
				}
				if (String.IsNullOrEmpty(strError)) {
					return true;
				}
				else {
					strError = "实时数据库中未找到的点: " + strError;
					return false;
				}
			}
			catch (Exception ex) {
				strError = ex.Message;
				return false;
			}
		}

		/// <summary>
		/// 得到List表所有标签点的实时数据
		/// </summary>
		/// <param name="tagname"></param>
		/// <returns></returns>
		public Boolean GetSnapshotListData(ref Dictionary<String, TagValue> lttvs, out String strError) {
			try {
				strError = String.Empty;
				if (null == lttvs || 0 >= lttvs.Count) {
					strError = "字典参数为空。";
					return false;
				}
				TagVector tagVector = new TagVector();
				foreach (String str in lttvs.Keys) {
					tagVector.AddRange(GetPointListByTagName(str));
				}
				RealDataSet realDataSet = new RealDataSet();
				BatchResults results = DataIO.Snapshot(dbConnector, tagVector, realDataSet);
				if (results.HasErrors) {
					foreach (DbError dberror in results.Errors) {
						strError += dberror.ErrorMessage;
					}
					return false;
				}
				foreach (var item in realDataSet) {
					TagValue tagValue = new TagValue();
					tagValue.TagID = item.TagId;
					tagValue.TagName = tagVector.First(x => x.TagId == item.TagId).TagLongName.ToUpper();
					Double val = 0.0;
					tagValue.TagSnapshot = (null != item.Value && Double.TryParse(item.Value.ToString(), out val)) ? val : 0;
					tagValue.TagTime = item.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss");
					tagValue.TagQulity = 0;
					tagValue.TagStringValue = "0";
					//if ((item.Value != null) && (item.Value.ToString() != "null")) tagValue.TagStringValue = item.Value + "";
					tagValue.TagStringValue = null != item.Value ? item.Value.ToString() : "0";
					tagValue.NewTime = String.Empty;
					tagValue.NewValue = double.MinValue;
					//tagValue.TagDesc = tagVector.First(x => x.TagId == item.TagId).Properties["Desc"].ToString();
					//tagValue.TagEngunit = tagVector.First(x => x.TagId == item.TagId).Properties["ENGINEERINGUNIT"].ToString();

					if (lttvs.ContainsKey(tagValue.TagName)) {
						lttvs[tagValue.TagName] = tagValue;
					}
				}
				results = null;
				tagVector = null;
				realDataSet = null;
				return true;
			}
			catch (Exception ex) {
				strError = ex.Message;
				return false;
			}
		}

		/// <summary>
		/// 得到List表所有标签点的历史数据
		/// </summary>
		/// <param name="tagname"></param>
		/// <returns></returns>
		public Boolean GetArchiveListData(ref Dictionary<string, TagValue> lttvs, DateTime stime, out string strError) {
			try {
				strError = String.Empty;
				if (null == lttvs || 0 >= lttvs.Count) {
					strError = "字典参数为空。";
					return false;
				}
				TagVector tagVector = new TagVector();
				foreach (String str in lttvs.Keys) {
					tagVector.AddRange(GetPointListByTagName(str));
				}
				HisDataSet realDataSet = new HisDataSet();
				DateTime[] HisTime = {stime};
				BatchResults results = DataIO.ReadAtTime(dbConnector, tagVector, HisTime, realDataSet);
				if (results.HasErrors) {
					foreach (DbError dberror in results.Errors) {
						strError += dberror.ErrorMessage;
					}
					return false;
				}
				foreach (var item in realDataSet) {
					/*TagValue tagValue = new TagValue();
					tagValue.TagID = item.TagId;
					tagValue.TagName = tagVector.First(x => x.TagId == item.TagId).TagName;
					if (null != item.Data && 0 < item.Data.Count) {
						Double val = Double.MinValue;
						tagValue.TagSnapshot = (null != item.Data[0].Value && Double.TryParse(item.Data[0].Value.ToString(), out val)) ? val : Double.MinValue;
						tagValue.TagTime = item.Data[0].TimeStamp.ToString("yyyy-MM-dd HH:mm:ss");
						tagValue.TagQulity = (Int32)item.Data[0].QualityStamp;
						tagValue.TagStringValue = null != item.Data[0].Value ? item.Data[0].Value.ToString() : Double.MinValue.ToString();
					}
					else {
						tagValue.TagSnapshot = Double.MinValue;
						tagValue.TagTime = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
						tagValue.TagQulity = 0;  //bad
						tagValue.TagStringValue = Double.MinValue.ToString();
					}
					tagValue.NewTime = String.Empty;
					tagValue.NewValue = double.MinValue;
					tagValue.TagDesc = tagVector.First(x => x.TagId == item.TagId).Properties["Desc"].ToString();
					tagValue.TagEngunit = tagVector.First(x => x.TagId == item.TagId).Properties["ENGINEERINGUNIT"].ToString();
					*/
					TagValue tagValue = new TagValue();
					tagValue.TagID = item.TagId;
					tagValue.TagName = tagVector.First(x => x.TagId == item.TagId).TagLongName.ToUpper();
					Double val = 0.0;
					tagValue.TagSnapshot = (null != item.Data[0].Value && Double.TryParse(item.Data[0].Value.ToString(), out val)) ? val : 0;
					tagValue.TagTime = item.Data[0].TimeStamp.ToString("yyyy-MM-dd HH:mm:ss");
					tagValue.TagQulity = 0;
					tagValue.TagStringValue = "0";
					//if ((item.Value != null) && (item.Value.ToString() != "null")) tagValue.TagStringValue = item.Value + "";
					tagValue.TagStringValue = null != item.Data[0].Value ? item.Data[0].Value.ToString() : "0";
					tagValue.NewTime = String.Empty;
					tagValue.NewValue = double.MinValue;

					if (lttvs.ContainsKey(tagValue.TagName)) {
						lttvs[tagValue.TagName] = tagValue;
					}
				}
				return true;
			}
			catch (Exception ex) {
				strError = ex.Message;
				return false;
			}
		}

		/// <summary>
		/// 根据输入时间查询归档数据，返回double类型
		/// </summary>
		/// <param name="tagname"></param>
		/// <param name="dt"></param>
		/// <returns></returns>
		public double GetArchiveValue(string tagname, DateTime dt) {
			double Result = double.MinValue;
			try {
				if (String.IsNullOrEmpty(tagname)) {
					_ErrorInfo = "点名称为空。";
					return Result;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				HisDataSet realDataSet = new HisDataSet();
				DateTime[] hisDateTime = { dt };
				//DataIO.ReadAtTime
				BatchResults results = DataIO.ReadAtTime(dbConnector, tagVector, hisDateTime, realDataSet);
				//BatchResults results = DataIO.ReadRaw(dbConnector, tagVector, dt.AddSeconds(-1), dt, realDataSet);
				if (results.HasErrors) {
					foreach (DbError dberror in results.Errors) {
						_ErrorInfo += dberror.ErrorMessage;
					}
					return Result;
				}
				if (realDataSet.Count > 0) {
					Result = Convert.ToDouble(realDataSet[0].Data[0].Value);
				}
				tagVector = null;
				realDataSet = null;
				results = null;
				return Result;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return Double.MinValue;
			}
		}

		/// <summary>
		/// 获得最后一次写入的归档值及间,返回double类型
		/// </summary>
		/// <param name="tagname"></param>
		/// <param name="timeStamp"></param>
		/// <returns></returns>
		public double GetArchiveValue(string tagname, out object timeStamp) {
			timeStamp = DateTime.MinValue;
			DateTime dt = DateTime.Now;
			try {
				if (String.IsNullOrEmpty(tagname)) {
					_ErrorInfo = "点名称为空。";
					return Double.MinValue;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				HisDataSet realDataSet = new HisDataSet();
				DateTime[] hisDateTime = { dt };
				BatchResults results = DataIO.ReadAtTime(dbConnector, tagVector, hisDateTime, realDataSet);
				if (results.HasErrors) {
					foreach (DbError dberror in results.Errors) {
						_ErrorInfo += dberror.ErrorMessage;
					}
					return Double.MinValue;
				}
				foreach (var item in realDataSet) {
					if (null != item.Data && 0 < item.Data.Count) {
						timeStamp = item.Data[0].TimeStamp;
						return ConvertToDouble(item.Data[0].Value);
					}
					else {
						return Double.MinValue;
					}
				}
				return Double.MinValue;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return Double.MinValue;
			}
		}

		/// <summary>
		/// 得到数字量的实时数值
		/// </summary>
		/// <param name="tagname"></param>
		/// <returns></returns>
		public string GetDigitalSnapshotValue(string tagname) {
			try {
				if (String.IsNullOrEmpty(tagname)) {
					return String.Empty;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null != tagVector && 0 < tagVector.Count) {
					RealHisData data = DataIO.Snapshot(dbConnector, tagVector[0] as ITagElement);
					if (null != data && data.QualityStamp == QUALITY_MASK.GOOD) {
						return data.Value.ToString();
					}
				}
				return String.Empty;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return String.Empty;
			}
		}

		/// <summary>
		/// 得到数字量的实时值名称（on off）
		/// </summary>
		/// <param name="tagname"></param>
		/// <returns></returns>
		public string GetDigitalSnapshotValueName(string tagname) {
			try {
				if (String.IsNullOrEmpty(tagname)) {
					return String.Empty;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null != tagVector && 0 < tagVector.Count) {
					RealHisData data = DataIO.Snapshot(dbConnector, tagVector[0] as ITagElement);
					if (null != data && data.QualityStamp == QUALITY_MASK.GOOD) {
						return ConvertToInt32(data.Value.ToString()) == 1 ? "On" : "Off";
					}
				}
				return String.Empty;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return String.Empty;
			}
		}

		/// <summary>
		/// 得到数字量的归档值及时间
		/// </summary>
		/// <param name="tagname"></param>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string GetDigitalArchiveValue(string tagname, DateTime dt) {
			try {
				if (String.IsNullOrEmpty(tagname)) {
					_ErrorInfo = "点名称为空。";
					return String.Empty;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null == tagVector || 0 >= tagVector.Count) {
					return String.Empty;
				}
				HisDataSet realDataSet = new HisDataSet();
				BatchResults results = DataIO.ReadRaw(dbConnector, tagVector, dt.AddSeconds(-1), dt, realDataSet);
				if (results.HasErrors) {
					foreach (DbError dberror in results.Errors) {
						_ErrorInfo += dberror.ErrorMessage;
					}
					return String.Empty;
				}
				foreach (var item in realDataSet) {
					if (null != item.Data && 0 < item.Data.Count) {
						return ConvertToInt32(item.Data[0].Value).ToString();
					}
					else {
						return String.Empty;
					}
				}
				return String.Empty;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return String.Empty;
			}
		}

		/// <summary>
		/// 得到数字量的归档值及时间
		/// </summary>
		/// <param name="tagname"></param>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string GetDigitalArchiveValueName(string tagname, DateTime dt) {
			try {
				if (String.IsNullOrEmpty(tagname)) {
					_ErrorInfo = "点名称为空。";
					return String.Empty;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null == tagVector || 0 >= tagVector.Count) {
					return String.Empty;
				}
				HisDataSet realDataSet = new HisDataSet();
				BatchResults results = DataIO.ReadRaw(dbConnector, tagVector, dt.AddSeconds(-1), dt, realDataSet);
				if (results.HasErrors) {
					foreach (DbError dberror in results.Errors) {
						_ErrorInfo += dberror.ErrorMessage;
					}
					return String.Empty;
				}
				foreach (var item in realDataSet) {
					if (null != item.Data && 0 < item.Data.Count) {
						return ConvertToInt32(item.Data[0].Value).ToString();
					}
					else {
						return String.Empty;
					}
				}
				return String.Empty;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return String.Empty;
			}
		}

		//Added by pyf 2013-09-16
		public List<TagValue> GetHistoryDataList(string tagname, DateTime stime, DateTime etime) {
			List<TagValue> Result = new List<TagValue>();
			if (String.IsNullOrEmpty(tagname)) {
				_ErrorInfo = "点名称为空。";
				return Result;
			}

			TagVector tagVector = GetPointListByTagName(tagname);
			if (null == tagVector || 0 >= tagVector.Count) {
				return Result;
			}
			HisDataSet realDataSet = new HisDataSet();
			BatchResults results = DataIO.ReadRaw(dbConnector, tagVector, stime, etime, realDataSet);
			if (results.HasErrors) {
				foreach (DbError dberror in results.Errors) {
					_ErrorInfo += dberror.ErrorMessage;
				}
				throw new Exception(ErrorInfo);
			}
			foreach (var item in realDataSet) {
				if (null != item.Data && 0 < item.Data.Count) {
					var q = from p in item.Data
							where p.QualityStamp != QUALITY_MASK.NOBOUND
							select new TagValue {
								TagName = tagname,
								TagStringValue = p.Value + "",
								TagQulity = 0,
								TagDoubleValue = Convert.ToDouble(p.Value),
								TimeStamp = p.TimeStamp
							};
					Result = q.ToList<TagValue>();
				}
			}
			return Result;
		}
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
		public List<double> GetHistoryDataListBySecondSpan(string tagname, bool isdigital, DateTime stime, DateTime etime, int interval) {
			try {
				if (String.IsNullOrEmpty(tagname)) {
					_ErrorInfo = "点名称为空。";
					return null;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null == tagVector || 0 >= tagVector.Count) {
					return null;
				}
				HisDataSet realDataSet = new HisDataSet();
				BatchResults results = DataIO.ReadRaw(dbConnector, tagVector, stime, etime, realDataSet);
				if (results.HasErrors) {
					foreach (DbError dberror in results.Errors) {
						_ErrorInfo += dberror.ErrorMessage;
					}
					return null;
				}
				foreach (var item in realDataSet) {
					if (null != item.Data && 0 < item.Data.Count) {
						var q = from p in item.Data
								where p.QualityStamp != QUALITY_MASK.NOBOUND
								select Convert.ToDouble(p.Value);
						return q.ToList();
						//return item.Data.ToArray();
					}
					else {
						return null;
					}
				}
				return null;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return null;
			}
		}

		/// <summary>
		/// 得到时间周期内的测点数据集(分钟取数)
		/// </summary>
		/// <param name="tagname">测点</param>
		/// <param name="isdigital">是否为模拟量</param>
		/// <param name="stime">开始日期</param>
		/// <param name="etime">结束日期</param>
		/// <param name="interval">取数时间间隔</param>
		/// <returns>结果集</returns>
		public List<double> GetHistoryDataListByMinuteSpan(string tagname, bool isdigital, DateTime stime, DateTime etime, int interval) {
			try {
				if (String.IsNullOrEmpty(tagname)) {
					_ErrorInfo = "点名称为空。";
					return null;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null == tagVector || 0 >= tagVector.Count) {
					return null;
				}
                List<double> Results = new List<double>();
				HisDataSet realDataSet = new HisDataSet();
				BatchResults results = DataIO.ReadRaw(dbConnector, tagVector, stime, etime, realDataSet);
				if (results.HasErrors) {
					foreach (DbError dberror in results.Errors) {
						_ErrorInfo += dberror.ErrorMessage;
					}
					return null;
				}
				foreach (var item in realDataSet) {
					if (null != item.Data && 0 < item.Data.Count) {
                        for (int i = 0; i < item.Data.Count; i++) {
                            Results.Add(Convert.ToDouble(item.Data[i].Value));
                        }
                        return Results;
					}
					else {
                        return Results;
					}
				}
				return null;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return null;
			}
		}


		/// <summary>
		/// 得到时间周期内的测点数据集(按个数取数)
		/// </summary>
		/// <param name="tagname">测点</param>
		/// <param name="isdigital">是否为模拟量</param>
		/// <param name="stime">开始日期</param>
		/// <param name="etime">结束日期</param>
		/// <param name="interval">取数个数</param>
		/// <returns>结果集</returns>
		public Object[] GetHistoryDataListByCount(string tagname, bool isdigital, DateTime stime, DateTime etime, int interval) {
			try {
				if (String.IsNullOrEmpty(tagname)) {
					_ErrorInfo = "点名称为空。";
					return null;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null == tagVector || 0 >= tagVector.Count) {
					return null;
				}
				HisDataSet realDataSet = new HisDataSet();
				BatchResults results = DataIO.ReadRaw(dbConnector, tagVector, stime, etime, realDataSet);
				if (results.HasErrors) {
					foreach (DbError dberror in results.Errors) {
						_ErrorInfo += dberror.ErrorMessage;
					}
					return null;
				}
				foreach (var item in realDataSet) {
					if (null != item.Data && 0 < item.Data.Count) {
						return item.Data.ToArray();
					}
					else {
						return null;
					}
				}
				return null;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return null;
			}
		}

		#endregion

		#region  Value写操作

		/// <summary>
		/// 回写数据
		/// </summary>
		/// <param name="tagname">测点</param>
		/// <param name="value">数值</param>
		/// <returns></returns>
		public Boolean WriteSnapshotValue(string tagname, string value) //回写数据库
		{
			try {
				if (String.IsNullOrEmpty(tagname)) {
					return false;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null != tagVector && 0 < tagVector.Count) {
					RealDataSet data = new RealDataSet();
					pSpaceCTLNET.TagValue tagVal = new pSpaceCTLNET.TagValue();
					tagVal.TagId = tagVector[0].TagId;
					tagVal.TimeStamp = DateTime.Now;
					tagVal.QualityStamp = QUALITY_MASK.GOOD;
					tagVal.Value = value;
					data.Add(tagVal);
					BatchResults results = DataIO.Insert(dbConnector, data, DataField.All);
					if (results.HasErrors) {
						foreach (DbError dberror in results.Errors) {
							_ErrorInfo += dberror.ErrorMessage;
						}
						return false;
					}
					return true;
				}
				return false;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return false;
			}
		}

		/// <summary>
		/// 回写数据
		/// </summary>
		/// <param name="tagname"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public Boolean WriteArchiveValue(string tagname, object time, string value) {
			try {
				if (String.IsNullOrEmpty(tagname)) {
					return false;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null != tagVector && 0 < tagVector.Count) {
					RealDataSet data = new RealDataSet();
					pSpaceCTLNET.TagValue tagVal = new pSpaceCTLNET.TagValue();
					tagVal.TagId = tagVector[0].TagId;
					tagVal.TimeStamp = DateTime.Parse(time.ToString());
					tagVal.QualityStamp = QUALITY_MASK.GOOD;
					tagVal.Value = value;
					data.Add(tagVal);
					BatchResults results = DataIO.Insert(dbConnector, data, DataField.All);
					if (results.HasErrors) {
						foreach (DbError dberror in results.Errors) {
							_ErrorInfo += dberror.ErrorMessage;
						}
						return false;
					}
					return true;
				}
				return false;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return false;
			}
		}

		#endregion

		#region  Value删除操作

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <param name="tagname">测点</param>
		/// <param name="value">数值</param>
		/// <returns></returns>
		public bool DeleteValue(string tagname, DateTime stime, DateTime etime) {
			try {
				if (String.IsNullOrEmpty(tagname)) {
					return false;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null != tagVector && 0 < tagVector.Count) {
					BatchResults results = DataIO.DeleteRaw(dbConnector, tagVector, stime, etime);
					if (results.HasErrors) {
						foreach (DbError dberror in results.Errors) {
							_ErrorInfo += dberror.ErrorMessage;
						}
						return false;
					}
					return true;
				}
				return false;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return false;
			}
		}

		#endregion

		#region  统计操作(Expression\Tag)

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//double类型
		//任意开始结束时间

		/// <summary>
		/// 某计算表达式的实时值
		/// </summary>
		/// <param name="expression">Total\Max\Min\StdDev\Range\Average\PStdDev
		/// 注：格式："点名,计算类型,开始时间,结束时间"
		/// 例如："\\DCS1\\P_tag001,Total,2013-09-08 13:24:50,2013-09-11 13:54:30"
		/// </param>
		/// <returns></returns>
		public Double ExpCurrentValue(string expression) {
			//String tagName = expression.StartsWith(
			try {
				if (String.IsNullOrEmpty(expression)) return Double.MinValue;
				int pos = expression.LastIndexOf("'") - 1;
				if (pos <= 0) return double.MinValue;
				String tagName = expression.Substring(1, pos);
				double tagValue = GetSnapshotValue(tagName);
				String Token = expression.Substring(pos + 2, 1);
				pos = expression.IndexOf(Token) + 1;
				String Val = expression.Substring(pos, expression.Length - pos);
				double targetValue = Convert.ToDouble(Val);
				return tagValue - targetValue;
			}
			catch  {
				return double.MinValue;
			}

		}


		/// <summary>
		/// 指定开始结束时间
		/// 统计时间内的Total\Max\Min\StdDev\Range\Average\PStdDev,返回单个数据
		/// 注：expression 存放的是点名称  filter没用到
		/// </summary>
		public Double ExpCalculatedData(string expression, DateTime stime, DateTime etime, string filter, SummaryType type) {
			try {
				if (String.IsNullOrEmpty(expression)) {
					return Double.MinValue;
				}
				TagVector tagVector = GetPointListByTagName(expression);
				if (null == tagVector || 0 >= tagVector.Count) {
					return Double.MinValue;
				}
				HisDataSet hisDataSet = new HisDataSet();
				AggregateEnum aggregateType;
				switch (type) {
					case SummaryType.asTotal:
						aggregateType = AggregateEnum.TOTAL;
						break;
					case SummaryType.asMinimum:
						aggregateType = AggregateEnum.MINIMUM;
						break;
					case SummaryType.asMaximum:
						aggregateType = AggregateEnum.MAXIMUM;
						break;
					case SummaryType.asStdDev:
						aggregateType = AggregateEnum.STDEV;
						break;
					case SummaryType.asRange:
						aggregateType = AggregateEnum.RANGE;
						break;
					case SummaryType.asAverage:
						aggregateType = AggregateEnum.AVERAGE;
						break;
					case SummaryType.asPStdDev:  //注：pSpace没有这类型
						aggregateType = AggregateEnum.STDEV;
						break;
					default:
						aggregateType = AggregateEnum.AVERAGE;
						break;
				}

				BatchResults results = DataIO.ReadProcessed(dbConnector, tagVector, stime, etime, etime - stime, aggregateType, hisDataSet);
				if (results.HasErrors) {
					foreach (DbError dberror in results.Errors) {
						_ErrorInfo += dberror.ErrorMessage;
					}
					return Double.MinValue;
				}
				if (null != hisDataSet && 0 < hisDataSet.Count) {
					return ConvertToDouble(hisDataSet[0].Data[0].Value);
				}
				return Double.MinValue;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return Double.MinValue;
			}
		}

		/// <summary>
		/// 指定开始结束时间
		/// 统计时间内的Total\Max\Min\StdDev\Range\Average\PStdDev,根据时间间隔返回数据组
		/// </summary>
		public bool ExpCalculatedData(string expression, DateTime stime, DateTime etime, string duration, string filter, SummaryType type, out double[] pdata) {
			pdata = null;
			return true;
		}

		/// <summary>
		/// 指定开始结束时间
		/// 统计时间内的Total\Max\Min\StdDev\Range\Average\PStdDev,返回单个数据
		/// 注：filter没用到
		/// </summary>
		public double TagCalculatedData(string tagname, DateTime stime, DateTime etime, string filter, SummaryType type) {
			try {
				if (String.IsNullOrEmpty(tagname)) {
					return Double.MinValue;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null == tagVector || 0 >= tagVector.Count) {
					return Double.MinValue;
				}
				HisDataSet hisDataSet = new HisDataSet();
				AggregateEnum aggregateType;
				switch (type) {
					case SummaryType.asTotal:
						aggregateType = AggregateEnum.TOTAL;
						break;
					case SummaryType.asMinimum:
						aggregateType = AggregateEnum.MINIMUM;
						break;
					case SummaryType.asMaximum:
						aggregateType = AggregateEnum.MAXIMUM;
						break;
					case SummaryType.asStdDev:
						aggregateType = AggregateEnum.STDEV;
						break;
					case SummaryType.asRange:
						aggregateType = AggregateEnum.RANGE;
						break;
					case SummaryType.asAverage:
						aggregateType = AggregateEnum.AVERAGE;
						break;
					case SummaryType.asPStdDev:  //注：pSpace没有这类型
						aggregateType = AggregateEnum.STDEV;
						break;
					default:
						aggregateType = AggregateEnum.AVERAGE;
						break;
				}

				BatchResults results = DataIO.ReadProcessed(dbConnector, tagVector, stime, etime, etime - stime, aggregateType, hisDataSet);
				if (results.HasErrors) {
					foreach (DbError dberror in results.Errors) {
						_ErrorInfo += dberror.ErrorMessage;
					}
					return Double.MinValue;
				}
				if (null != hisDataSet && 0 < hisDataSet.Count) {
					return ConvertToDouble(hisDataSet[0].Data[0].Value);
				}
				return Double.MinValue;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return Double.MinValue;
			}
		}

		/// <summary>
		/// 指定开始结束时间
		/// 统计时间内的Total\Max\Min\StdDev\Range\Average\PStdDev,根据时间间隔返回数据组
		/// </summary>
		public bool TagCalculatedData(string tagname, DateTime stime, DateTime etime, string duration, string filter, SummaryType type, out double[] pdata) {
			try {
				pdata = null;
				if (String.IsNullOrEmpty(tagname)) {
					return false;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null == tagVector || 0 >= tagVector.Count) {
					return false;
				}
				HisDataSet hisDataSet = new HisDataSet();
				AggregateEnum aggregateType;
				switch (type) {
					case SummaryType.asTotal:
						aggregateType = AggregateEnum.TOTAL;
						break;
					case SummaryType.asMinimum:
						aggregateType = AggregateEnum.MINIMUM;
						break;
					case SummaryType.asMaximum:
						aggregateType = AggregateEnum.MAXIMUM;
						break;
					case SummaryType.asStdDev:
						aggregateType = AggregateEnum.STDEV;
						break;
					case SummaryType.asRange:
						aggregateType = AggregateEnum.RANGE;
						break;
					case SummaryType.asAverage:
						aggregateType = AggregateEnum.AVERAGE;
						break;
					case SummaryType.asPStdDev:  //注：pSpace没有这类型
						aggregateType = AggregateEnum.STDEV;
						break;
					default:
						aggregateType = AggregateEnum.AVERAGE;
						break;
				}

				BatchResults results = DataIO.ReadProcessed(dbConnector, tagVector, stime, etime, etime - stime, aggregateType, hisDataSet);
				if (results.HasErrors) {
					foreach (DbError dberror in results.Errors) {
						_ErrorInfo += dberror.ErrorMessage;
					}
					return false;
				}
				List<Double> list = new List<Double>();
				if (null != hisDataSet && 0 < hisDataSet.Count) {
					foreach (var items in hisDataSet) {
						if (null == items.Data || 0 >= items.Data.Count) {
							continue;
						}
						foreach (RealHisData item in items.Data) {
							list.Add(ConvertToDouble(item.Value));
						}
					}
					pdata = list.ToArray();
					return true;
				}
				return false;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				pdata = null;
				return false;
			}
		}

		/// <summary>
		/// 指定开始结束时间
		/// 统计时间内的Total\Snapshot\Max\Min\StdDev\Range\Average\PStdDev, 返回数据组
		/// </summary>
		public bool TagSummaryData(string tagname, DateTime stime, DateTime etime, string filter, out TagAllValue pdata) {
			try {
				pdata = null;
				if (String.IsNullOrEmpty(tagname)) {
					return false;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null == tagVector || 0 >= tagVector.Count) {
					return false;
				}
				pdata = new TagAllValue();
				pdata.TagName = tagname;
				pdata.TagSnapshot = GetSnapshotValue(tagname);
				pdata.TagDesc = tagVector[0].Properties["Desc"].ToString();
				pdata.TagEngunit = tagVector[0].Properties["ENGINEERINGUNIT"].ToString();

				pdata.TagTotal = TagCalculatedData(tagname, stime, etime, filter, SummaryType.asTotal);
				pdata.TagMinimum = TagCalculatedData(tagname, stime, etime, filter, SummaryType.asMinimum);
				pdata.TagMaximum = TagCalculatedData(tagname, stime, etime, filter, SummaryType.asMaximum);
				pdata.TagStdDev = TagCalculatedData(tagname, stime, etime, filter, SummaryType.asStdDev);
				pdata.TagRange = TagCalculatedData(tagname, stime, etime, filter, SummaryType.asRange);
				pdata.TagAverage = TagCalculatedData(tagname, stime, etime, filter, SummaryType.asAverage);
				pdata.TagPStdDev = TagCalculatedData(tagname, stime, etime, filter, SummaryType.asPStdDev);
				pdata.TagAverage = TagCalculatedData(tagname, stime, etime, filter, SummaryType.asAverage);

				return true;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				pdata = null;
				return false;
			}
		}

		#endregion

		#region 基于Time区间的数据统计操作

		/// <summary>
		/// 统计测点运行时间
		/// </summary>
		/// <param name="tagname">测点</param>
		/// <param name="stime">开始时间</param>
		/// <param name="etime">结束时间</param>
		/// <returns></returns>
		public double GetExpressionTrueSecondTime(string tagname, DateTime stime, DateTime etime) {
			try {
				if (String.IsNullOrEmpty(tagname)) {
					return Double.MinValue;
				}
				TagVector tagVector = GetPointListByTagName(tagname);
				if (null == tagVector || 0 >= tagVector.Count) {
					return Double.MinValue;
				}
				HisDataSet hisDataSet = new HisDataSet();
				BatchResults results = DataIO.ReadProcessed(dbConnector, tagVector, stime, etime, etime - stime, AggregateEnum.TOTALIZEAVERAGE, hisDataSet);
				if (results.HasErrors) {
					foreach (DbError dberror in results.Errors) {
						_ErrorInfo += dberror.ErrorMessage;
					}
					return Double.MinValue;
				}
				if (null != hisDataSet && 0 < hisDataSet.Count) {
					return ConvertToDouble(hisDataSet[0].Data[0].Value);
				}
				return Double.MinValue;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return Double.MinValue;
			}
		}

		/// <summary>
		/// 计算某时刻某表达式的值
		/// 注：expression的格式为点名称,计算类型
		/// </summary>
		/// <param name="expression"></param>
		/// <param name="dttime"></param>
		/// <returns></returns>
		public double TimedCalculate(string expression, DateTime dttime) {
			/*try {
				if (String.IsNullOrEmpty(expression) || !expression.Contains(',')) {
					return Double.MinValue;
				}
				String[] strArray = expression.Split(',');
				if (null == strArray || 0 >= strArray.Length) {
					return Double.MinValue;
				}
				TagVector tagVector = GetPointListByTagName(strArray[0]);
				if (null == tagVector || 0 >= tagVector.Count) {
					return Double.MinValue;
				}
				Double result = Double.MinValue;
				switch (strArray[0].ToUpper()) {
					case "ASTOTAL":
						result = TagCalculatedData(strArray[0], dttime.AddSeconds(-1), dttime, String.Empty, SummaryType.asTotal);
						break;
					case "ASMINIMUM":
						result = TagCalculatedData(strArray[0], dttime.AddSeconds(-1), dttime, String.Empty, SummaryType.asMinimum);
						break;
					case "ASMAXIMUM":
						result = TagCalculatedData(strArray[0], dttime.AddSeconds(-1), dttime, String.Empty, SummaryType.asMaximum);
						break;
					case "ASSTDDEV":
						result = TagCalculatedData(strArray[0], dttime.AddSeconds(-1), dttime, String.Empty, SummaryType.asStdDev);
						break;
					case "ASRANGE":
						result = TagCalculatedData(strArray[0], dttime.AddSeconds(-1), dttime, String.Empty, SummaryType.asRange);
						break;
					case "ASAVERAGE":
						result = TagCalculatedData(strArray[0], dttime.AddSeconds(-1), dttime, String.Empty, SummaryType.asAverage);
						break;
					case "ASPSTDDEV":  //注：pSpace没有这类型
						result = TagCalculatedData(strArray[0], dttime.AddSeconds(-1), dttime, String.Empty, SummaryType.asPStdDev);
						break;
					default:
						result = TagCalculatedData(strArray[0], dttime.AddSeconds(-1), dttime, String.Empty, SummaryType.asAverage);
						break;
				}
				return result;
			}
			catch (Exception ex) {
				_ErrorInfo = ex.Message;
				return Double.MinValue;
			}*/
			try {
				int pos = expression.LastIndexOf("'") - 1;
				if (pos <= 0) return double.MinValue;
				String tagName = expression.Substring(1, pos);
				double tagValue = GetArchiveValue(tagName, dttime);
				String Token = expression.Substring(pos + 2, 1);
				pos = expression.IndexOf(Token) + 1;
				String Val = expression.Substring(pos, expression.Length - pos);
				double targetValue = Convert.ToDouble(Val);
				return tagValue - targetValue;
			}
			catch (Exception ex) {
				return double.MinValue;
			}
		}

		#endregion
	}
}
