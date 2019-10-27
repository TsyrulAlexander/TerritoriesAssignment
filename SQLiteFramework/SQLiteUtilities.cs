using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SQLiteFramework.Condition;
using SQLiteFramework.Condition.Column;
using SQLiteFramework.Condition.Value;
using SQLiteFramework.Table;

namespace SQLiteFramework {
	public static class SQLiteUtilities {
		public static SQLiteColumn CreateDateTimeColumn(string columnName) {
			return new SQLiteColumn(columnName, SQLiteColumnType.DateTime);
		}
		public static SQLiteColumn CreateDoubleColumn(string columnName) {
			return new SQLiteColumn(columnName, SQLiteColumnType.Double);
		}
		public static SQLiteColumn CreateIntegerColumn(string columnName) {
			return new SQLiteColumn(columnName, SQLiteColumnType.Integer);
		}
		public static SQLiteColumn CreateBooleanColumn(string columnName) {
			return new SQLiteColumn(columnName, SQLiteColumnType.Boolean);
		}
		public static SQLiteColumn CreateStringColumn(string columnName) {
			return new SQLiteColumn(columnName, SQLiteColumnType.String);
		}
		public static SQLiteColumn CreateGuidColumn(string columnName) {
			return new SQLiteColumn(columnName, SQLiteColumnType.Guid);
		}
		public static IConditionValue Value(string value) {
			return new StringConditionValue(value);
		}
		public static IConditionValue Value(Guid value) {
			return new GuidConditionValue(value);
		}
		public static ISQLiteCondition CreateCondition(this string value, string columnName,
			SQLiteComparisonType comparisonType = SQLiteComparisonType.Equal) {
			return new SQLiteCondition(columnName, comparisonType, Value(value));
		}
		public static ISQLiteCondition CreateCondition(this Guid value, string columnName, 
				SQLiteComparisonType comparisonType = SQLiteComparisonType.Equal) {
			return new SQLiteCondition(columnName, comparisonType, Value(value));
		}
		public static string ToString(this SQLiteColumnType columnType) {
			switch (columnType) {
				case SQLiteColumnType.Guid:
					return "NVARCHAR(36)";
				case SQLiteColumnType.String:
					return "TEXT";
				case SQLiteColumnType.Integer:
					return "INTEGER";
				case SQLiteColumnType.Double:
					return "DOUBLE";
				case SQLiteColumnType.Boolean:
					return "BOOLEAN";
				case SQLiteColumnType.DateTime:
					return "DATETIME";
				default:
					throw new NotSupportedException(nameof(columnType));
			}
		}

		public static void SatValue(this object obj, string columnName, object value) {
			var objType = obj.GetType();
			var objProperty = objType.GetProperty(columnName);
			if (objProperty == null) {
				throw new NullReferenceException(columnName);
			}
			objProperty.SetValue(obj, value);
		}

		public static IEnumerable<SQLiteColumn> GetColumns(Type type) {
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			return properties.Select(info => new SQLiteColumn(GetColumnName(info), GetColumnType(info)));
		}

		public static IEnumerable<SQLiteColumnValue> GetColumnValues(object value, IEnumerable<string> columns = null) {
			var type = value.GetType();
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			if (columns != null) {
				properties = properties.Where(info => columns.Contains(GetColumnName(info))).ToArray();
			}
			return properties.Select(info => new SQLiteColumnValue(GetColumnName(info), GetColumnValue(info, value)));
		}
		private static object GetColumnValue(PropertyInfo info, object value) {
			var propertyType = info.PropertyType;
			var columnValue = info.GetValue(value);
			if (columnValue == null) {
				return null;
			}
			if (IsClass(propertyType)) {
				var primaryColumn = propertyType.GetProperty("Id");
				return primaryColumn.GetValue(columnValue);
			}
			return info.GetValue(value);
		}
		private static Type GetColumnType(PropertyInfo info) {
			var propertyType = info.PropertyType;
			if (IsClass(propertyType)) {
				return typeof(Guid);
			}
			if (propertyType.IsEnum) {
				return typeof(int);
			}
			return info.PropertyType;
		}
		private static bool IsClass(Type type) {
			return type.IsClass && type != typeof(string);
		}
		private static string GetColumnName(PropertyInfo info) {
			var columnName = info.Name;
			var propertyType = info.PropertyType;
			if (IsClass(propertyType)) {
				return columnName + ".Id";
			}
			if (info.PropertyType.IsEnum) {
				return columnName;//todo
			}
			return columnName;
		}

		public static bool GetJoinPath(string columnName, out SQLiteJoinPath[] info, out string lastColumnName) {
			info = new SQLiteJoinPath[0];
			var paths = columnName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
			lastColumnName = paths[paths.Count - 1];
			if (paths.Count == 1) {
				return false;
			}
			var infoList = new List<SQLiteJoinPath>();
			for (int i = 0; i < paths.Count - 1; i++) {
				var path = paths[i];
				if (GetIsBackwardPath(path, out var joinPath)) {
					infoList.Add(joinPath);
				} else {
					infoList.Add(new SQLiteJoinPath {
						TableName = path,
						ColumnName = path + "Id"
					});
					
				}
				
			}
			info = infoList.ToArray();
			return true;
		}
		private static bool GetIsBackwardPath(string path, out SQLiteJoinPath joinPath) {
			joinPath = null;
			var startIndex = path.IndexOf('(');
			var endIndex = path.IndexOf(')');
			if (startIndex < 0 || endIndex < 0) {
				return false;
			}
			joinPath = new SQLiteJoinPath {
				TableName = path.Replace(path.Remove(0, startIndex), ""),
				ColumnName = path.Remove(0, startIndex).Replace("(", "").Replace(")", ""),
				IsBackward = true
			};
			return true;
		}
		public static void SetPropertyValue(object obj, string columnName, object value) {
			var objType = obj.GetType();
			var property = objType.GetProperty(columnName);
			if (columnName.Contains("_") && property == null) {
				var path = columnName.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
				var tableName = path[0];
				var tableColumnName = path[1];
				SetPropertyClassValue(obj, tableName, tableColumnName, value);
			} else {
				obj.SatValue(columnName, value);
			}
		}
		private static void SetPropertyClassValue(object obj, string tableName, string columnName, object value) {
			var objType = obj.GetType();
			var property = objType.GetProperty(tableName);
			if (property == null) {
				throw new ArgumentException(tableName);
			}
			var instance = property.GetValue(obj);
			if (instance == null) {
				var propertyType = property.PropertyType;
				instance = Activator.CreateInstance(propertyType);
				obj.SatValue(tableName, instance);
			}
			instance.SatValue(columnName, value);
		}
	}
}