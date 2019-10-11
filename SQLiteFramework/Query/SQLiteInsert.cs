using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLiteFramework.Command;
using SQLiteFramework.Condition.Column;

namespace SQLiteFramework.Query {
	public class SQLiteInsert {
		public SQLiteEngine Engine { get; }
		public SQLiteInsert(SQLiteEngine engine) {
			Engine = engine;
		}

		public void Execute(object value, IEnumerable<string> columnNames = null) {
			var objType = value.GetType();
			var tableName = objType.Name;
			var columns = SQLiteUtilities.GetColumnValues(value, columnNames);
			Execute(tableName, columns);
		}

		public void Execute(string tableName, IEnumerable<SQLiteColumnValue> columnValues) {
			var command = new SQLiteInsertCommand(tableName, columnValues);
			command.ExecuteNonQuery(Engine);
		}
	}
}