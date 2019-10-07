using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteFramework.Query {
	public class QueryRowValue {
		public IEnumerable<QueryColumnValue> Values { get; set; }
	}
}