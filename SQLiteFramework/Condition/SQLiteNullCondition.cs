using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteFramework.Condition
{
	public class SQLiteNullCondition: ISQLiteCondition {
		public string ColumnPath { get; }
		public SQLiteNullType Type { get; }
		private const string IsCommandName = "IS";
		private const string NotCommandName = "NOT";
		private const string NullCommandName = "NULL";
		public SQLiteNullCondition(string columnPath = null, SQLiteNullType type = SQLiteNullType.Null) {
			ColumnPath = columnPath;
			Type = type;
		}
		public string GetSqlText(string tableName) {
			switch (Type) {
				case SQLiteNullType.Null:
					return $" {tableName}.{ColumnPath} {IsCommandName} {NullCommandName} ";
				case SQLiteNullType.NotNull:
					return $" {tableName}.{ColumnPath} {IsCommandName} {NotCommandName} {NullCommandName} ";
				default:
					throw new NotImplementedException(nameof(Type));
			}
		}
	}
}
