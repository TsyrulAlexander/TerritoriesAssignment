using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using SQLiteFramework.Command;
using SQLiteFramework.Condition;
using SQLiteFramework.Condition.Column;
using SQLiteFramework.Table;

namespace SQLiteFramework.Query {
	public class SQLiteSelect {
		public SQLiteEngine Engine { get; }
		public List<ISQLiteCondition> Conditions { get; } 
		public SQLiteSelect(SQLiteEngine engine, IEnumerable<ISQLiteCondition> conditions = null) {
			Engine = engine;
			Conditions = new List<ISQLiteCondition>(conditions ?? new ISQLiteCondition[0]);
		}
		public void AddCondition(params ISQLiteCondition[] conditions) {
			foreach (var condition in conditions) {
				Conditions.Add(condition);
			}
		}
		public IEnumerable<T> GetEntities<T>() {
			var type = typeof(T);
			var columns = SQLiteUtilities.GetColumns(type).ToArray();
			var select = new SQLiteSelectCommand(type.Name, columns, Conditions);
			return select.Execute(Engine, reader => Read<T>(reader, columns));
		}
		public IEnumerable<T> GetEntities<T>(string tableName, params SQLiteColumn[] columns) {
			var select = new SQLiteSelectCommand(tableName, columns, Conditions);
			return select.Execute(Engine, reader => Read<T>(reader, columns));
		}
		public IEnumerable<QueryRowValue> GetEntities(string tableName, params SQLiteColumn[] columns) {
			var select = new SQLiteSelectCommand(tableName, columns, Conditions);
			return select.Execute(Engine, columns);
		}
		
		protected T Read<T>(SQLiteDataReader dataReader, params SQLiteColumn[] columns) {
			var instance = Activator.CreateInstance<T>();
			foreach (var sqLiteColumn in columns) {
				SQLiteUtilities.SetPropertyValue(instance, GetColumnDataReaderName(sqLiteColumn), GetReaderValue(dataReader, sqLiteColumn));
			}
			return instance;
		}
		protected virtual object GetReaderValue(SQLiteDataReader dataReader, SQLiteColumn column) {
			var index = dataReader.GetOrdinal(GetColumnDataReaderName(column));
			if (dataReader.IsDBNull(index)) {
				return null;
			}
			switch (column.Type) {
				case SQLiteColumnType.Guid:
					return Guid.Parse(dataReader.GetString(index));
				case SQLiteColumnType.String:
					return dataReader.GetString(index);
				case SQLiteColumnType.Double:
					return dataReader.GetDouble(index);
				case SQLiteColumnType.Integer:
					return dataReader.GetInt32(index);
				case SQLiteColumnType.Boolean:
					return dataReader.GetBoolean(index);
				case SQLiteColumnType.DateTime:
					return dataReader.GetDateTime(index);
				default:
					return dataReader.GetValue(index);
			}
		}

		protected virtual string GetColumnDataReaderName(SQLiteColumn column) {
			return column.Name.Replace('.', '_');
		}
	}
}