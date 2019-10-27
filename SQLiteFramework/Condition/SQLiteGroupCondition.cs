using System;

namespace SQLiteFramework.Condition
{
	public class SQLiteGroupCondition : ISQLiteCondition {
		private const string AndCommandName = "AND";
		private const string OrCommandName = "OR";
		public ISQLiteCondition FirstCondition { get; }
		public ISQLiteCondition SecondCondition { get; }
		public SQLiteLogicalOperation LogicalOperation { get; }
		public SQLiteGroupCondition(ISQLiteCondition firstCondition, ISQLiteCondition secondCondition,
			SQLiteLogicalOperation logicalOperation = SQLiteLogicalOperation.And) {
			FirstCondition = firstCondition;
			SecondCondition = secondCondition;
			LogicalOperation = logicalOperation;
		}
		public string[] GetJoinPath() {
			return new string[0];
		}
		public string GetSqlText(string tableName) {
			return $"({FirstCondition.GetSqlText(tableName)} {GetLogicalOperatorSql()} {SecondCondition.GetSqlText(tableName)})";
		}

		protected virtual string GetLogicalOperatorSql() {
			switch (LogicalOperation) {
				case SQLiteLogicalOperation.And:
					return AndCommandName;
				case SQLiteLogicalOperation.Or:
					return OrCommandName;
				default:
					throw new NotImplementedException(nameof(LogicalOperation));
			}
		}
	}
}
