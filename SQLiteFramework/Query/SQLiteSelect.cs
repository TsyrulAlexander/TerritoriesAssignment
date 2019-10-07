using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using SQLiteFramework.Command;
using SQLiteFramework.Condition;
using SQLiteFramework.Condition.Column;

namespace SQLiteFramework.Query {
	public class SQLiteSelect {
		public SQLiteEngine Engine { get; }
		public List<ISQLiteCondition> Conditions { get; } 
		public SQLiteSelect(SQLiteEngine engine, IEnumerable<ISQLiteCondition> conditions = null) {
			Engine = engine;
			Conditions = new List<ISQLiteCondition>(conditions ?? new ISQLiteCondition[0]);
		}
		public IEnumerable<T> GetEntities<T>() {
			var select = new SQLiteSelectCommand(typeof(T), Conditions);
			return Engine.ExecuteCommand<T>(select);
		}
		public IEnumerable<QueryRowValue> GetEntities(string tableName, IEnumerable<SQLiteColumn> columns) {
			var select = new SQLiteSelectCommand(tableName, columns, Conditions);
			return select.Execute(Engine, reader => GetQueryColumnValue(reader, columns));

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
	}
}