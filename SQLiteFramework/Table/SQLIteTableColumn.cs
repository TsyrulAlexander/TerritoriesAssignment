namespace SQLiteFramework.Table
{
	public class SQLiteTableColumn
	{
		public bool IsPrimaryKey { get; set; }
		public bool IsUnique { get; set; }
		public bool IsRequired { get; set; }
		public string Name { get; set; }
		public SQLiteColumnType Type { get; set; }
		public SQLiteForeignKey ForeignKey { get; set; }

	}
}
