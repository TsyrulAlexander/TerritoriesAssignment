using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SQLiteFramework.Condition;
using SQLiteFramework.Condition.Column;
using SQLiteFramework.Condition.Value;
using SQLiteFramework.Table;

namespace SQLiteFramework {
	public static class SQLiteUtilities {
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
			var properties = type.GetProperties();
			return properties.Select(info => new SQLiteColumn(GetColumnName(info), GetColumnType(info)));
		}

		public static IEnumerable<SQLiteColumnValue> GetColumnValues(object value) {
			var type = value.GetType();
			var properties = type.GetProperties();
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

		public static bool GetJoinPath(string columnName, out (string tableName, string columnName) info) {
			info = (null, null);
			var path = columnName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			if (path.Length == 1) {
				return false;
			}
			if (path.Length > 2) {
				throw new NotImplementedException(columnName);
			}
			info = (path[0], path[1]);
			return true;
		}
		public static void SetPropertyValue(object obj, SQLiteColumn column, object value) {
			var columnName = column.Name;
			var objType = obj.GetType();
			var property = objType.GetProperty(columnName);
			if (columnName.Contains("_") && property == null) {
				var path = columnName.Split(new[] {'_'}, StringSplitOptions.RemoveEmptyEntries);
				var tableName = path[0];
				var tableColumnName = path[1];
				SetPropertyClassValue(objType, tableName, tableColumnName, value);
				return;
			}
			obj.SatValue(columnName, value);
		}
		private static object SetPropertyClassValue(Type objType, string tableName, string columnName, object value) {
			var property = objType.GetProperty(tableName);
			if (property == null) {
				throw new ArgumentException(tableName);
			}
			var instance = property.GetValue(objType);
			if (instance == null) {
				var propertyType = property.PropertyType;
				instance = Activator.CreateInstance(propertyType);
			}
			instance.SatValue(columnName, value);
			return instance;
		}
	}
}