using System.Collections.Generic;
using System.Data.SQLite;
using SQLiteFramework.Condition;

namespace SQLiteFramework.Command {
	public abstract class SQLiteCommand {
		public virtual void ExecuteNonQuery(SQLiteConnection connection) {
			var sqlCommand = GetCommandSql();
			using (System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand(connection)) {
				command.CommandText = sqlCommand;
				command.ExecuteNonQuery();
			}
		}
		public virtual IEnumerable<T> Execute<T>(SQLiteConnection connection) {
			var sqlCommand = GetCommandSql();
			using (System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand(connection)) {
				command.CommandText = sqlCommand;
				var reader = command.ExecuteReader();
				return Reads<T>(reader);
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