namespace SQLiteFramework.Condition.Value {
	public class NullConditionValue : IConditionValue {
		public string GetValue() {
			return "NULL";
		}
	}
}