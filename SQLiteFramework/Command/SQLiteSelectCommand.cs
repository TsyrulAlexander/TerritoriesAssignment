using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using SQLiteFramework.Condition;
using SQLiteFramework.Condition.Column;
using SQLiteFramework.Table;

namespace SQLiteFramework.Command {
	public class SQLiteSelectCommand : SQLiteBaseTableOperationCommand
	{
		protected const string SelectCommandName = "SELECT";
		protected IEnumerable<SQLiteColumn> Columns { get; set; }
		public SQLiteSelectCommand(string tableName, IEnumerable<SQLiteColumn> columns, IEnumerable<ISQLiteCondition> conditions = null): base(tableName, conditions) {
			Columns = columns;
		}
		public override string GetCommandSql() {
			return $"{SelectCommandName} {GetColumnsSql()} " + 
				$"{FromCommandName} {TableName} " + 
				$"{GetConditionsSql()}";
		}
		protected virtual string GetColumnsSql() {
			return string.Join(",", Columns.Select(column => column.Name));
		}
	}
}
