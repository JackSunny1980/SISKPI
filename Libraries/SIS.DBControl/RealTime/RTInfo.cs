using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SIS.DBControl {
	//统计类型
	public enum SummaryType {
		asTotal,        //Total over time range. 
		asMinimum,      //Minimum for time range. 
		asMaximum,      //Maximum for time range. 
		asStdDev,       //The standard deviation over the time range. 
		asRange,        //The range of values over the time range. 
		asAverage,      //The average value over the time range. 
		asPStdDev,      //The population standard deviation over the time range. 

		asCount,        //The sum of event count over the time range when calculationbasis is eventweighted, sum of event time duration over the time range when calculationbasis is timeweighted. 
		asAll,          //Total, minimum, maximum, range, standard deviation, population standard deviation, average and count. 
		asMinMaxRange,  //Minimum, maximum and range of values. 
		asTotalAndAvg,  //Total and average. 
		asStdDevAndAvg, //Standard deviation and average. 
	};

	//查询时Tag信息
	public class TagInfo {
		public string TagName {
			get;
			set;
		}

		public string TagDesc {
			get;
			set;
		}

		public string TagEngunit {
			get;
			set;
		}

		public bool TagIsDigital {
			get;
			set;
		}
	}

	//查询时数据信息
	public class TagValue {
		/// <summary>
		/// 点名
		/// </summary>
		public long TagID {
			get;
			set;
		}

		/// <summary>
		/// 点名
		/// </summary>
		public string TagName {
			get;
			set;
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string TagDesc {
			get;
			set;
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string TagEngunit {
			get;
			set;
		}

		public double TagSnapshot {
			get;
			set;
		}

		public string TagStringValue {
			get;
			set;
		}

		public string TagTime {
			get;
			set;
		}

		//0-good, 1-bad
		public int TagQulity {
			get;
			set;
		}

		public double NewValue {
			get;
			set;
		}

		public string NewTime {
			get;
			set;
		}

		//Added by pyf 2013-09-16

		public double TagDoubleValue {
			get;
			set;
		}

		public DateTime TimeStamp {
			get;
			set;
		}
		//End of Added.


		public TagValue() {

		}

		public TagValue(string name) {
			this.TagName = name;
		}

		public TagValue(long id, string name, string descriptor, string engunit, double snapshot, string time, double newvalue, string newtime) {
			this.TagID = id;
			this.TagName = name;
			this.TagDesc = descriptor;
			this.TagEngunit = engunit;
			this.TagSnapshot = snapshot;
			this.TagTime = time;

			this.NewValue = newvalue;
			this.NewTime = newtime;
		}
	}



	//查询统计信息
	public class TagAllValue {
		/// <summary>
		/// 点名
		/// </summary>
		public string TagName {
			get;
			set;
		}

		/// <summary>
		/// 描述
		/// </summary>
		public string TagDesc {
			get;
			set;
		}

		/// <summary>
		/// 单位
		/// </summary>
		public string TagEngunit {
			get;
			set;
		}

		public double TagSnapshot {
			get;
			set;
		}

		public double TagAverage {
			get;
			set;
		}


		public double TagMinimum {
			get;
			set;
		}


		public double TagMaximum {
			get;
			set;
		}


		public double TagStdDev {
			get;
			set;
		}


		public double TagRange {
			get;
			set;
		}


		public double TagPStdDev {
			get;
			set;
		}

		public double TagTotal {
			get;
			set;
		}
	}



	//数据预处理存储数据用
	public class ParamDictionary : IEnumerable {
		private Dictionary<string, List<double>> listparam = new Dictionary<string, List<double>>();

		#region Constructors
		public ParamDictionary() { }
		#endregion


		#region 函数

		//索引器函数
		public List<double> this[String key] {
			get {
				return listparam[key];
			}

			set {
				listparam[key] = value;
			}
		}

		public int Count {
			get {
				return listparam.Count;
			}
		}

		//添加类对象函数 
		public void Add(String key, List<double> value) {
			listparam.Add(key, value);
		}

		//移除类对象函数
		public void Remove(String key) {
			listparam.Remove(key);
		}


		//清空类对象函数
		public void Clear() {
			listparam.Clear();
		}

		// Foreach enumeration support. 
		IEnumerator IEnumerable.GetEnumerator() { return listparam.GetEnumerator(); }

		#endregion

	}

	public class UnitDictionary : IEnumerable {
		private Dictionary<string, ParamDictionary> listunit = new Dictionary<string, ParamDictionary>();

		#region Constructors
		public UnitDictionary() { }
		#endregion


		#region 函数

		//索引器函数
		public ParamDictionary this[String key] {
			get {
				return listunit[key];
			}

			set {
				listunit[key] = value;
			}
		}

		public int Count {
			get {
				return listunit.Count;
			}
		}

		//添加类对象函数 
		public void Add(String key, ParamDictionary value) {
			listunit.Add(key, value);
		}

		//移除类对象函数
		public void Remove(String key) {
			listunit.Remove(key);
		}


		//清空类对象函数
		public void Clear() {
			listunit.Clear();
		}

		// Foreach enumeration support. 
		IEnumerator IEnumerable.GetEnumerator() { return listunit.GetEnumerator(); }

		#endregion

	}


}
