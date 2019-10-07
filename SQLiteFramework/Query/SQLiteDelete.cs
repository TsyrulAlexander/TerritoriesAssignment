using System;
using System.Collections.Generic;
using System.Text;
using SQLiteFramework.Command;
using SQLiteFramework.Condition;

namespace SQLiteFramework.Query {
	public class SQLiteDelete {
		public SQLiteEngine Engine { get; }
		public SQLiteDelete(SQLiteEngine engine) {
			Engine = engine;
		}
		public void Execute(string tableName, params ISQLiteCondition[] conditions) {
			var deleteCommand = new SQLiteDeleteCommand(tableName, conditions);
			deleteCommand.ExecuteNonQuery(Engine);
		}
	}
}
