using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLiteFramework.Condition;
using SQLiteFramework.Condition.Column;

namespace SQLiteFramework.Command {
	public class SQLiteInsertCommand : SQLiteBaseTableOperationCommand {
		private const string InsertCommandName = "INSERT";
		private const string IntoCommandName = "INTO";
		private const string ValuesCommandName = "VALUES";
		public IEnumerable<SQLiteColumnValue> Columns { get; }
		public SQLiteInsertCommand(object obj) {
			var objType = obj.GetType();
			TableName = objType.Name;
			Columns = objType.GetProperties().Select(propertyInfo =>
				new SQLiteColumnValue(propertyInfo.Name, propertyInfo.GetValue(obj)));
		}
		public SQLiteInsertCommand(string tableName, IEnumerable<SQLiteColumnValue> columns) : base(tableName) {
			Columns = columns;
		}
		public override string GetCommandSql() {
			return $"{InsertCommandName} {IntoCommandName} {TableName}({GetColumnNamesSql()}) {ValuesCommandName} ({GetColumnValuesSql()}) ";
		}
		protected virtual string GetColumnNamesSql() {
			return string.Join(",", Columns.Select(column => column.Name));
		}
		protected virtual string GetColumnValuesSql() {
			return string.Join(",", Columns.Select(column => column.Value.GetValue()));
		}
	}
}
