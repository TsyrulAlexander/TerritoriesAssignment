using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteFramework.Table {
	public class SQLiteForeignKey {
		public string ReferenceTableName { get; set; }
		public string ReferenceTableColumnName { get; set; }
		public SQLiteForeignKey(string referenceTableName, string referenceTableColumnName) {
			ReferenceTableName = referenceTableName;
			ReferenceTableColumnName = referenceTableColumnName;
		}
	}
}