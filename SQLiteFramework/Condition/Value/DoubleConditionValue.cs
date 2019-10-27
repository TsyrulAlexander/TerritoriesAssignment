using System.Globalization;

namespace SQLiteFramework.Condition.Value {
	public class DoubleConditionValue : IConditionValue {
		public double Value { get; }
		public DoubleConditionValue(double value) {
			Value = value;
		}
		public string GetValue() {
			return Value.ToString(CultureInfo.InvariantCulture);
		}
	}
}
