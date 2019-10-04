using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteFramework.Condition.Value
{
	public class NullConditionValue: IConditionValue
	{
		public string GetValue() {
			return "NULL";
		}
	}
}
