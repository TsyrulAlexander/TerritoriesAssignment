namespace SQLiteFramework.Condition.Value {
	public class BooleanConditionValue : IConditionValue {
		public bool Value { get; }
		public BooleanConditionValue(bool value) {
			Value = value;
		}
		public string GetValue() {
			return Value ? "1" : "0";
		}
	}
}
