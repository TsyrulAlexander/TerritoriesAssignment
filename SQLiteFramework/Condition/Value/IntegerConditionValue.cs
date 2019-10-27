namespace SQLiteFramework.Condition.Value {
	public class IntegerConditionValue : IConditionValue {
		public int Value { get; }
		public IntegerConditionValue(int value) {
			Value = value;
		}
		public string GetValue() {
			return Value.ToString();
		}
	}
}
