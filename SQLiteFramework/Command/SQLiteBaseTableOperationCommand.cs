using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLiteFramework.Condition;
using SQLiteFramework.Condition.Column;

namespace SQLiteFramework.Command
{
	public abstract class SQLiteBaseTableOperationCommand: SQLiteCommand
	{
		protected const string FromCommandName = "FROM";
		protected const string WhereCommandName = "WHERE";
		protected string TableName { get; set; }
		protected IEnumerable<ISQLiteCondition> Conditions { get; set; }
		
		public SQLiteBaseTableOperationCommand(string tableName = null, IEnumerable<ISQLiteCondition> conditions = null) {
			TableName = tableName;
			Conditions = conditions;
		}

		protected virtual string GetConditionsSql() {
			if (Conditions == null || !Conditions.Any()) {
				return string.Empty;
			}
			return $"{WhereCommandName} {string.Join("AND", Conditions.Select(GetConditionSql))}";
		}
		protected virtual string GetConditionSql(ISQLiteCondition condition) {
			return condition.GetSqlText();
		}
		protected virtual bool GetJoinPath(string columnName, out (string tableName, string columnName) info) {
			return SQLiteUtilities.GetJoinPath(columnName, out info);
		}
		protected virtual string GetColumnSql(SQLiteColumn column) {
			if (GetJoinPath(column.Name, out var info)) {
				return $"{info.tableName}{info.columnName}";
			}
			return column.Name;
		}
	}
}
