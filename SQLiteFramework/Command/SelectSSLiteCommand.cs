using System;
using System.Collections.Generic;
using SQLiteFramework.Condition;

namespace SQLiteFramework.Command {
	class SelectSSLiteCommand : SQLiteCommand {
		public string TableName { get; set; }
		public IEnumerable<SQLiteCondition> Conditions { get; set; }
		public override string GetCommandSql() {
			throw new NotImplementedException();
		}
	}
}
