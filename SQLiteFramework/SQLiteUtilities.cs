using System;
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

		public static string ToString(this SQLiteColumnType columnType) {
			switch (columnType) {
				case SQLiteColumnType.Guid:
					return "NVARCHAR(38)";
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
	}
}