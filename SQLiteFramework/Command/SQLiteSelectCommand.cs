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
		protected const string LeftCommandName = "LEFT";
		protected const string JoinCommandName = "JOIN";
		protected const string OnCommandName = "ON";
		protected IEnumerable<SQLiteColumn> Columns { get; set; }
		public SQLiteSelectCommand(string tableName, IEnumerable<SQLiteColumn> columns, IEnumerable<ISQLiteCondition> conditions = null): base(tableName, conditions) {
			Columns = columns;
		}
		public override string GetCommandSql() {
			return $"{SelectCommandName} {GetColumnsSql()} " + 
				$"{FromCommandName} {TableName} " + 
				$"{GetColumnsJoin()} " +
				$"{GetConditionsSql()}";
		}
		protected virtual string GetColumnsJoin() {
			var sql = string.Empty;
			var joinTables = new List<string>();
			foreach (var sqLiteColumn in Columns) {
				if (GetJoinPath(sqLiteColumn.Name, out var info)) {
					if (joinTables.Contains(info.tableName)) {
						continue;
					}
					sql += $"{LeftCommandName} {JoinCommandName} {info.tableName} {OnCommandName} {info.tableName}.Id = {TableName}.{info.tableName}Id\n";
					joinTables.Add(info.tableName);
				}
			}
			return sql;
		}
		
		protected virtual string GetColumnsSql() {
			return string.Join(",", Columns.Select(GetColumnSql));
		}
		protected override string GetColumnSql(SQLiteColumn column) {
			if (GetJoinPath(column.Name, out var info)) {
				return $"{info.tableName}.{info.columnName} AS {info.tableName}_{info.columnName}";
			}
			return $"{TableName}.{column.Name}";
		}
	}
}
