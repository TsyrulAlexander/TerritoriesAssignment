using System.Collections.Generic;
using SQLiteFramework.Command;
using SQLiteFramework.Condition;
using SQLiteFramework.Condition.Column;

namespace SQLiteFramework.Query {
	public class SQLiteUpdate {
		public SQLiteEngine Engine { get; }
		public SQLiteUpdate(SQLiteEngine engine) {
			Engine = engine;
		}

		public void Execute(object value, ISQLiteCondition[] conditions, IEnumerable<string> columnNames) {
				var valueType = value.GetType();
			var columns = SQLiteUtilities.GetColumnValues(value, columnNames);
			Execute(valueType.Name, columns, conditions);
		}
		public void Execute(string tableName, IEnumerable<SQLiteColumnValue> columns, IEnumerable<ISQLiteCondition> conditions) {
			var updateCommand = new SQLiteUpdateCommand(tableName, columns, conditions);
			updateCommand.ExecuteNonQuery(Engine);
		}
	}
}