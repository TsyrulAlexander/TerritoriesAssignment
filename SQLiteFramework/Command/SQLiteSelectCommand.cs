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
		public SQLiteSelectCommand(Type type, IEnumerable<ISQLiteCondition> conditions = null) : base(type.Name, conditions) {
			Columns = type.GetProperties().Select(info => new SQLiteColumn(info.Name, info.PropertyType));
		}
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

		protected override T Read<T>(SQLiteDataReader dataReader) {
			var instance = Activator.CreateInstance<T>();
			foreach (var sqLiteColumn in Columns) {
				instance.SatValue(sqLiteColumn.Name, GetReaderValue(dataReader, sqLiteColumn));
			}
			return instance;
		}

		protected virtual object GetReaderValue(SQLiteDataReader dataReader, SQLiteColumn column) {
			var index = dataReader.GetOrdinal(column.Name);
			if (dataReader.IsDBNull(index)) {
				return null;
			}
			switch (column.Type) {
				case SQLiteColumnType.Guid:
					return Guid.Parse(dataReader.GetString(index));
				case SQLiteColumnType.String:
					return dataReader.GetString(index);
				default:
					return dataReader.GetValue(index);
			}
		}
	}
}
