using System;

namespace SQLiteFramework.Condition.Value
{
	public class DateTimeConditionValue: IConditionValue
	{
		public DateTime Value { get; }
		public DateTimeConditionValue(DateTime value) {
			Value = value;
		}
		public string GetValue() {
			return Value.ToString("{0:MM-dd-yy HH:mm:ss}");
		}
	}
}
