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
		public SQLiteSelectCommand(string tableName, IEnumerable<SQLiteColumn> columns,
				IEnumerable<ISQLiteCondition> conditions = null): base(tableName, conditions) {
			Columns = columns;
		}
		public override string GetCommandSql() {
			return $"{SelectCommandName} {GetColumnsSql()} \n" + 
				$"{FromCommandName} {TableName} \n" + 
				$"{GetColumnsJoin()}" + 
				$"{GetConditionsSql()}";
		}
		protected virtual string GetColumnsJoin() {
			var sql = string.Empty;
			var joinTables = new List<SQLiteJoinPath>();
			foreach (var joinPath in Columns.Select(column => column.Name)
				.Concat(Conditions.SelectMany(condition => condition.GetJoinPath()))) {
				if (GetJoinPath(joinPath, out var info, out _)) {
					for (var i = 0; i < info.Length; i++) {
						var sqLiteJoinPath = info[i];
						if (joinTables.Exists(path =>
							path.TableName == sqLiteJoinPath.TableName &&
							path.ColumnName == sqLiteJoinPath.ColumnName)) {
							continue;
						}
						string lastTableName = null;
						string lastFirstColumnName = null;
						string lastSecondColumnName = null;
						lastTableName = i > 0 ? info[i - 1].TableName : TableName;
						if (sqLiteJoinPath.IsBackward) {
							lastSecondColumnName = "Id";
							lastFirstColumnName = sqLiteJoinPath.ColumnName;
						} else {
							lastSecondColumnName = sqLiteJoinPath.ColumnName;
							lastFirstColumnName = "Id";
						}
						sql +=
							$"{LeftCommandName} {JoinCommandName} {sqLiteJoinPath.TableName} " +
							$"{OnCommandName} {sqLiteJoinPath.TableName}.{lastFirstColumnName} = {lastTableName}.{lastSecondColumnName}\n";
						joinTables.Add(sqLiteJoinPath);
					}
				}
			}
			return sql;
		}
		
		protected virtual string GetColumnsSql() {
			return string.Join(",", Columns.Select(GetColumnSql));
		}
		protected override string GetColumnSql(SQLiteColumn column) {
			if (GetJoinPath(column.Name, out var info, out var lastColumnName)) {
				var lastJoin = info[info.Length - 1];
				return $"{lastJoin.TableName}.{lastColumnName} AS {lastJoin.TableName}_{lastColumnName}";
			}
			return $"{TableName}.{column.Name}";
		}
	}
}
