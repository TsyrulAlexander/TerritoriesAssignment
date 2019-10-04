using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using SQLiteFramework.Command;
using SQLiteFramework.Exception;
using SQLiteFramework.Table;
using SQLiteCommand = SQLiteFramework.Command.SQLiteCommand;

namespace SQLiteFramework {
	public class SQLiteEngine {
		public string Path { get; }
		public SQLiteEngine(string path) {
			Path = path;
		}
		public virtual bool IsExistDatabase() {
			return File.Exists(Path);
		}
		public virtual void CreateDatabaseIfNotExist() {
			if (!IsExistDatabase()) {
				CreateDatabase();
			}
		}
		public virtual void CreateDatabase() {
			if (IsExistDatabase()) {
				throw new ExistFileException(Path);
			}
			ExecuteCommand(GetCreateDatabaseCommand());
		}
		protected virtual SQLiteCommand GetCreateDatabaseCommand() {
			return new SQLiteCreateDataBaseCommand(Path);
		}
		public virtual void ExecuteCommand(SQLiteCommand command) {
			using (var connection = CreateConnection()) {
				command.ExecuteNonQuery(connection);
			}
		}

		public virtual IEnumerable<T> ExecuteCommand<T>(SQLiteCommand command) {
			using (var connection = CreateConnection()) {
				return command.Execute<T>(connection);
			}
		}

		protected virtual SQLiteConnection CreateConnection() {
			var connectionString = GetConnectionString();
			return new SQLiteConnection(connectionString);
		}

		protected virtual string GetConnectionString() {
			return $"Data Source={Path};Foreign Key Constraints=On;Version=3;";
		}

		public virtual void CreateTable(string tableName, IEnumerable<SQLiteTableColumn> columns) {
			var command = new SQLiteCreateTableCommand(tableName, columns);
			ExecuteCommand(command);
		}
	}
}