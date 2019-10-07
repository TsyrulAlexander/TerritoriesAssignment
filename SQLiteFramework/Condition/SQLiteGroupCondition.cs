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
		public string GetSqlText() {
			return $"({FirstCondition.GetSqlText()} {GetLogicalOperatorSql()} {SecondCondition.GetSqlText()})";
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
