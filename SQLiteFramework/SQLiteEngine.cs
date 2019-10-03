using System;

namespace SQLiteFramework {
	public class SQLiteEngine {
		protected virtual void Exists(string path) {

		}
		protected virtual void Create(string path) {
			System.Data.SQLite.SQLiteConnection.CreateFile(path);
		}

		protected virtual void StartCommand() {

		}

		protected virtual void EndCommand() {

		}

		public virtual void ExecuteCommand() {
			StartCommand();
			//todo
			EndCommand();
		}
	}
}
