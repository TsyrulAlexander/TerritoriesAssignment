using System;
using System.Collections.Generic;
using System.Text;
using SQLiteFramework.Condition.Value;

namespace SQLiteFramework.Condition {
	public interface ISQLiteCondition {
		string GetSqlText(string tableName);
	}
}