using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace SQLiteFramework.Command
{
	class SQLiteCreateDataBaseCommand: SQLiteCommand
	{
		public string Path { get; }
		public SQLiteCreateDataBaseCommand(string path) {
			Path = path;
		}
		public virtual void Create(string path) {
			SQLiteConnection.CreateFile(path);
		}
		public override string GetCommandSql() {
			return "PRAGMA auto_vacuum = FULL;";
		}
		public override void ExecuteNonQuery(SQLiteConnection connection) {
			Create(Path);
			base.ExecuteNonQuery(connection);
		}
	}
}
