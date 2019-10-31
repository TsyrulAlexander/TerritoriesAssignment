using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using SQLiteFramework.Condition.Column;
using SQLiteFramework.Query;

namespace SQLiteFramework.Command {
	public abstract class SQLiteCommand {
		protected const string IfCommandName = "IF";
		protected const string NotCommandName = "NOT";
		protected const string ExistsCommandName = "EXISTS";
		public virtual void ExecuteNonQuery(SQLiteEngine engine) {
			using (var connection = engine.CreateConnection()) {
				ExecuteNonQuery(connection);
			}
		}
		public virtual void ExecuteNonQuery(SQLiteConnection connection) {
			var sqlCommand = GetCommandSql();
			using (System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand(connection)) {
				command.CommandText = sqlCommand;
				try {
					connection.Open();
					command.ExecuteNonQuery();
				} finally {
					connection.Close();
				}
			}
		}
		public virtual IEnumerable<T> Execute<T>(SQLiteEngine engine) {
			using (var connection = engine.CreateConnection()) {
				return Execute(connection, Read<T>);
			}
		}
		public virtual IEnumerable<QueryRowValue> Execute(SQLiteEngine engine, IEnumerable<SQLiteColumn> columns) {
			using (var connection = engine.CreateConnection()) {
				return Execute(connection, reader => GetQueryColumnValue(reader, columns));
			}
		}
		public virtual IEnumerable<T> Execute<T>(SQLiteEngine engine, Func<SQLiteDataReader, T> read) {
			using (var connection = engine.CreateConnection()) {
				return Execute(connection, read);
			}
		}
		protected virtual IEnumerable<T> Execute<T>(SQLiteConnection connection, Func<SQLiteDataReader, T> read) {
			var sqlCommand = GetCommandSql();
			using (System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand(connection)) {
				command.CommandText = sqlCommand;
				try {
					connection.Open();
					var reader = command.ExecuteReader();
					return Reads(reader, read);
				} finally {
					connection.Close();
				}
			}
		}
		protected virtual IEnumerable<T> Reads<T>(SQLiteDataReader dataReader, Func<SQLiteDataReader, T> func) {
			var list = new List<T>();
			while (dataReader.Read()) {
				list.Add(func(dataReader));
			}
			return list;
		}
		protected virtual T Read<T>(SQLiteDataReader dataReader) {
			return default(T);
		}
		protected virtual QueryRowValue GetQueryColumnValue(SQLiteDataReader dataReader,
			IEnumerable<SQLiteColumn> columns) {
			return new QueryRowValue {
				Values = columns.Select(column => {
					var index = dataReader.GetOrdinal(column.Name);
					return new QueryColumnValue {
						ColumnName = column.Name,
						Value = dataReader.GetValue(index)
					};
				})
			};
		}


		public abstract string GetCommandSql();
	}
}