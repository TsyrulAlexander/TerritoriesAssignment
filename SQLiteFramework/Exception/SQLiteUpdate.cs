using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLiteFramework.Command;
using SQLiteFramework.Condition;
using SQLiteFramework.Condition.Column;

namespace SQLiteFramework.Exception {
	public class SQLiteUpdate {
		public SQLiteEngine Engine { get; }
		public SQLiteUpdate(SQLiteEngine engine) {
			Engine = engine;
		}

		public void Execute(object value, params ISQLiteCondition[] conditions) {
				var valueType = value.GetType();
				var columns = valueType.GetProperties().Select(propertyInfo =>
					new SQLiteColumnValue(propertyInfo.Name, propertyInfo.GetValue(value)));
				Execute(valueType.Name, columns, conditions);
		}
		public void Execute(string tableName, IEnumerable<SQLiteColumnValue> columns, IEnumerable<ISQLiteCondition> conditions) {
			var updateCommand = new SQLiteUpdateCommand(tableName, columns, conditions);
			updateCommand.ExecuteNonQuery(Engine);
		}
	}
}