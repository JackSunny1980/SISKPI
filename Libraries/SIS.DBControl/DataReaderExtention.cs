using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace SIS.DBControl {
	public static class DataReaderExtention {

		public static T SafeRead<T>(this IDataReader reader, string fieldName, T defaultValue) {
			try {
				int ordinal = reader.GetOrdinal(fieldName);
				if (reader.IsDBNull(ordinal))
					return defaultValue;				
				return (T)Convert.ChangeType(reader[ordinal], defaultValue.GetType());
			}
			catch (InvalidCastException) {
				return defaultValue;
			}
		}

		public static T FillEntity<T>(this IDataReader reader) where T : class,new() {
			T RowInstance = default(T);
			if (reader.Read()) {
				RowInstance = Activator.CreateInstance<T>();
				PropertyInfo[] Properties =  typeof(T).GetProperties();
				foreach (PropertyInfo property in Properties) {
					object[] objs = property.GetCustomAttributes(typeof(DescriptionAttribute), false);
					if ((objs == null) || (objs.Length <= 0)) continue;
					string fieldName = ((DescriptionAttribute)objs[0]).Description;
					try {
						int index = reader.GetOrdinal(fieldName);
						object value = null;
						if (property.PropertyType == typeof(System.Boolean)) {
							value = reader[index] == null ? false : Convert.ToBoolean(reader[index]);
						}
						else {
							value = reader[index];
						}
						property.SetValue(RowInstance, value, null);						
					}
					catch (Exception ex){
						//throw ex;						
					}
				}
				for (int i = 0; i < Properties.Length; i++) {
					Properties[i] = null;
				}
				Properties = null;
			}
			return RowInstance;
		}

		public static List<T> FillGenericList<T>(this IDataReader reader) where T :class, new() {
			List<T> DataList = new List<T>();
			PropertyInfo[] Properties = typeof(T).GetProperties();
			while (reader.Read()) {
				T RowInstance =  Activator.CreateInstance<T>();				
				foreach (PropertyInfo property in Properties) {
					object[] objs = property.GetCustomAttributes(typeof(DescriptionAttribute), false);
					if ((objs == null) || (objs.Length <= 0)) continue;
					string fieldName = ((DescriptionAttribute)objs[0]).Description;
					try {
						int index = reader.GetOrdinal(fieldName);
						object value = null;
						if (property.PropertyType == typeof(System.Boolean)) {
							value = reader[fieldName].ToString() == "" ? false : Convert.ToBoolean(reader[fieldName]);
						}
						else {
							value = reader[fieldName];
						}
						property.SetValue(RowInstance, value, null);
					}
					catch  (Exception ex){
						continue;
						//throw ex;						
					}					
				}
				DataList.Add(RowInstance);
			}
			for (int i = 0; i < Properties.Length; i++) {
				Properties[i] = null;
			}
			Properties = null;
			return DataList;
		}
	}
}
