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
		public SQLiteUpdateCommand(object obj, IEnumerable<SQLiteCondition> conditions = null) : base(
			obj.GetType().Name, conditions) {
			var objType = obj.GetType();
			Columns = objType.GetProperties().Select(propertyInfo =>
				new SQLiteColumnValue(propertyInfo.Name, propertyInfo.GetValue(obj)));
		}
		public SQLiteUpdateCommand(string tableName, IEnumerable<SQLiteColumnValue> columns,
			IEnumerable<SQLiteCondition> conditions = null) : base(tableName, conditions) {
			Columns = columns;
		}
		public override string GetCommandSql() {
			return $"{UpdateCommandName} {TableName} {SetCommandName} {GetColumnsSql()} " +
				$"{GetConditionsSql()}";
		}
		protected virtual string GetColumnsSql() {
			return string.Join(",", Columns.Select(column => $"{column.Name} = {column.Value.GetValue()}"));
		}
	}
}