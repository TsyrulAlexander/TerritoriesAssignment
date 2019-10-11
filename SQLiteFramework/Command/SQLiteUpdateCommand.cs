using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLiteFramework.Condition;
using SQLiteFramework.Condition.Column;

namespace SQLiteFramework.Command
{
	public class SQLiteUpdateCommand : SQLiteBaseTableOperationCommand
	{
		private const string UpdateCommandName = "UPDATE";
		private const string SetCommandName = "SET";
		public IEnumerable<SQLiteColumnValue> Columns {
			get; set;
		}
		public SQLiteUpdateCommand(string tableName, IEnumerable<SQLiteColumnValue> columns,
			IEnumerable<ISQLiteCondition> conditions = null) : base(tableName, conditions) {
			Columns = columns;
		}
		public override string GetCommandSql() {
			return $"{UpdateCommandName} {TableName} {SetCommandName} {GetColumnsSql()} " +
				$"{GetConditionsSql()}";
		}
		protected virtual string GetColumnsSql() {
			return string.Join(",", Columns.Select(column => $"{GetColumnSql(column)} = {column.Value.GetValue()}"));
		}
	}
}