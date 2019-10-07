using System;
using System.Collections.Generic;
using System.Text;
using SQLiteFramework.Table;

namespace SQLiteFramework.Condition.Column
{
	public class SQLiteColumn {
		public string Name { get; set; }
		public SQLiteColumnType Type { get; set; }
		public SQLiteColumn(string name, SQLiteColumnType type): this(name) {
			Type = type;
		}
		public SQLiteColumn(string name, Type type): this(name) {
			if (type != null) {
				Type = GetSQLiteColumnType(type);
			}
		}
		public SQLiteColumn(string name) {
			Name = name;
		}
		protected SQLiteColumnType GetSQLiteColumnType(Type type) {
			if (type == typeof(Guid)) {
				return SQLiteColumnType.Guid;
			}
			if (type == typeof(string)) {
				return SQLiteColumnType.String;
			}
			throw new NotSupportedException(type.Name);
		}
	}
}
