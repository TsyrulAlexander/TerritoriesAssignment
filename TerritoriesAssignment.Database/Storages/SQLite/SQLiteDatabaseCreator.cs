using System;
using System.Collections.Generic;
using System.Text;
using SQLiteFramework;
using SQLiteFramework.Table;

namespace TerritoriesAssignment.Database.Storages.SQLite {
	public class SQLiteDatabaseCreator {
		private SQLiteEngine Engine { get; }
		public SQLiteDatabaseCreator(string path) {
			Engine = new SQLiteEngine(path);
		}
		public SQLiteDatabaseCreator(SQLiteEngine engine) {
			Engine = engine;
		}
		public void CreateIfNotExist() {
			if (Engine.IsExistDatabase()) {
				return;
			}
			CreateDatabase();
			CreateTables();
		}
		protected virtual void CreateDatabase() {
			Engine.CreateDatabase();
		}
		protected virtual void CreateTables() {
			CreateCountryTable();
			CreateAreaTable();
			CreateRegionTable();
		}

		protected virtual void CreateCountryTable() {
			Engine.CreateTable("Country", new[] {
				CreatePrimaryColumn(),
				CreateStringColumn("Name"),
				CreateStringColumn("Path", false, false)
			});
		}

		protected virtual void CreateAreaTable() {
			Engine.CreateTable("Area", new[] {
				CreatePrimaryColumn(),
				CreateStringColumn("Name"),
				CreateStringColumn("Path", false, false),
				CreateGuidColumn("CountryId", true, false, new SQLiteForeignKey("Country", "Id"))
			});
		}

		protected virtual void CreateRegionTable() {
			Engine.CreateTable("Region", new[] {
				CreatePrimaryColumn(),
				CreateStringColumn("Name"),
				CreateStringColumn("Path", false, false),
				CreateGuidColumn("AreaId", true, false, new SQLiteForeignKey("Area", "Id"))
			});
		}

		public virtual void CreateAttributeTable() {
			Engine.CreateTable("Attribute", new[] {
				CreatePrimaryColumn(),
				CreateStringColumn("Name")
			});
		}

		public virtual void CreateAttributeValueTable() {
			Engine.CreateTable("AttributeValue", new[] {
				CreatePrimaryColumn(),
				CreateDoubleColumn("DoubleValue"),
				CreateGuidColumn("RegionId", true, false, new SQLiteForeignKey("Region", "Id")),
				CreateGuidColumn("AttributeId", true, false, new SQLiteForeignKey("Attribute", "Id"))
			});
		}

		protected virtual SQLiteTableColumn CreatePrimaryColumn(string columnName = "Id") {
			var guidColumn = CreateGuidColumn(columnName);
			guidColumn.IsPrimaryKey = true;
			return guidColumn;
		}
		protected virtual SQLiteTableColumn CreateGuidColumn(string columnName, bool isRequired = true,
			bool isUnique = true, SQLiteForeignKey foreignKey = null) {
			return new SQLiteTableColumn {
				Name = columnName,
				Type = SQLiteColumnType.Guid,
				IsRequired = isRequired,
				IsUnique = isUnique,
				ForeignKey = foreignKey
			};
		}
		protected virtual SQLiteTableColumn CreateStringColumn(string columnName, bool isRequired = true,
			bool isUnique = true) {
			return new SQLiteTableColumn {
				Name = columnName,
				Type = SQLiteColumnType.String,
				IsRequired = isRequired,
				IsUnique = isUnique
			};
		}
		protected virtual SQLiteTableColumn CreateDoubleColumn(string columnName, bool isRequired = true) {
			return new SQLiteTableColumn {
				Name = columnName,
				Type = SQLiteColumnType.Double,
				IsRequired = isRequired
			};
		}
	}
}