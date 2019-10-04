using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLiteFramework.Condition;
using SQLiteFramework.Table;

namespace SQLiteFramework.Command
{
	public class SQLiteCreateTableCommand: SQLiteCommand {
		private const string CreateTableCommandName = "CREATE TABLE";
		private const string PrimaryKeyCommandName = "PRIMARY KEY";
		private const string ForeignKeyCommandName = "FOREIGN KEY";
		private const string ReferencesCommandName = "REFERENCES";
		private const string UniqueCommandName = "UNIQUE";
		private const string NullCommandName = "NULL";
		public string TableName { get; }
		public IEnumerable<SQLiteTableColumn> Columns { get; }
		public SQLiteCreateTableCommand(string tableName, IEnumerable<SQLiteTableColumn> columns) {
			TableName = tableName;
			Columns = columns;
		}
		public override string GetCommandSql() {
			return $"{CreateTableCommandName} {IfCommandName} {NotCommandName} {ExistsCommandName}" + 
				$" [{TableName}] {GetCreateColumnsSql(Columns)}";
		}
		protected virtual string GetCreateColumnsSql(IEnumerable<SQLiteTableColumn> columns) {
			var columnsSql = columns.Select(GetCreateColumnSql);
			var foreignKeys = columns.Where(column => column.ForeignKey != null)
				.Select(GetCreateForeignKeySql);
			return $"({string.Join(",", columnsSql.Concat(foreignKeys))})";
		}
		protected virtual string GetCreateColumnSql(SQLiteTableColumn column) {
			var columnSql = "[" + column.Name + "] ";
			columnSql += SQLiteUtilities.ToString(column.Type) + " ";
			if (column.IsPrimaryKey) {
				columnSql += PrimaryKeyCommandName + " ";
			}
			if (column.IsUnique) {
				columnSql += UniqueCommandName + " ";
			}
			if (column.IsRequired) {
				columnSql += $"{NotCommandName} {NullCommandName} ";
			}
			return columnSql;
		}

		protected virtual string GetCreateForeignKeySql(SQLiteTableColumn column) {
			return
				$"{ForeignKeyCommandName} ({column.Name}) " + 
				$"{ReferencesCommandName} {column.ForeignKey.ReferenceTableName}({column.ForeignKey.ReferenceTableColumnName})";
		}
	}
}
/*CREATE TABLE child ( 
    id           INTEGER PRIMARY KEY, 
    parent_id    INTEGER, 
    description  TEXT,
    FOREIGN KEY (parent_id) REFERENCES parent(id)
); */
