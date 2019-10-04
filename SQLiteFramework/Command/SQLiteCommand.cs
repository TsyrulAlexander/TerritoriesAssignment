using System.Collections.Generic;
using System.Data.SQLite;
using SQLiteFramework.Condition;

namespace SQLiteFramework.Command {
	public abstract class SQLiteCommand {
		protected const string IfCommandName = "IF";
		protected const string NotCommandName = "NOT";
		protected const string ExistsCommandName = "EXISTS";
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
		public virtual IEnumerable<T> Execute<T>(SQLiteConnection connection) {
			var sqlCommand = GetCommandSql();
			using (System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand(connection)) {
				command.CommandText = sqlCommand;
				try {
					connection.Open();
					var reader = command.ExecuteReader();
					return Reads<T>(reader);
				} finally {
					connection.Close();
				}
			}
		}
		protected virtual IEnumerable<T> Reads<T>(SQLiteDataReader dataReader) {
			var list = new List<T>();
			while (dataReader.Read()) {
				list.Add(Read<T>(dataReader));
			}
			return list;
		}
		protected virtual T Read<T>(SQLiteDataReader dataReader) {
			return default(T);
		}
		public abstract string GetCommandSql();
	}
}