using System;
using System.Collections.Generic;
using System.Linq;
using SQLiteFramework;
using SQLiteFramework.Condition;
using SQLiteFramework.Condition.Column;
using SQLiteFramework.Exception;
using SQLiteFramework.Query;
using SQLiteFramework.Table;
using TerritoriesAssignment.Core;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.Database.Storages.SQLite {
	public class SQLiteDataStorage : IDataStorage {
		public string Path { get; }
		protected SQLiteEngine Engine { get; set; }
		public SQLiteDataStorage(string path) {
			Path = path;
			Engine = new SQLiteEngine(path);
			CreateDatabase();
		}
		private void CreateDatabase() {
			var creator = new SQLiteDatabaseCreator(Engine);
			creator.CreateIfNotExist();
		}
		public virtual Country GetCountry(Guid id) {
			return GetRecord<Country>(id, GetBaseRecordSelectColumns());
		}
		public virtual Area GetArea(Guid id) {
			return GetRecord<Area>(id, GetAreaRecordSelectColumns());
		}
		public virtual Region GetRegion(Guid id) {
			return GetRecord<Region>(id, GetRegionRecordSelectColumns());
		}
		public virtual void AddCountry(Country country) {
			AddRecord(country, GetBaseRecordColumns());
		}
		public virtual void AddArea(Area area) {
			AddRecord(area, GetAreaRecordColumns());
		}
		public virtual void AddRegion(Region region) {
			AddRecord(region, GetRegionRecordColumns());
		}
		public virtual void UpdateCountry(Country country) {
			UpdateRecord(country, country.Id, GetBaseRecordColumns());
		}
		public virtual void UpdateArea(Area area) {
			UpdateRecord(area, area.Id, GetAreaRecordColumns());
		}
		public virtual void UpdateRegion(Region region) {
			UpdateRecord(region, region.Id, GetRegionRecordColumns());
		}
		public virtual IEnumerable<BaseLookup> GetCountries(string search = null) {
			return GetRecords(nameof(Country), search);
		}
		public virtual IEnumerable<BaseLookup> GetAreas(Guid countryId, string search = null) {
			return GetRecords(nameof(Area), search, "Name", countryId.CreateCondition("CountryId"));
		}
		public virtual IEnumerable<BaseLookup> GetRegions(Guid areaId, string search = null) {
			return GetRecords(nameof(Region), search, "Name", areaId.CreateCondition("AreaId"));
		}
		public virtual void DeleteCountry(Guid countryId) {
			DeleteRecord(nameof(Country), countryId);
		}
		public virtual void DeleteArea(Guid areaId) {
			DeleteRecord(nameof(Area), areaId);
		}
		public virtual void DeleteRegion(Guid regionId) {
			DeleteRecord(nameof(Region), regionId);
		}
		protected virtual T GetRecord<T>(Guid recordId, IEnumerable<SQLiteColumn> columns) {
			var select = new SQLiteSelect(Engine);
			select.AddCondition(GetPrimaryCondition(recordId));
			return select.GetEntities<T>(typeof(T).Name, columns.ToArray()).FirstOrDefault();
		}
		protected virtual void AddRecord(object entity, IEnumerable<string> columnNames) {
			var insert = new SQLiteInsert(Engine);
			insert.Execute(entity, columnNames);
		}
		protected virtual void UpdateRecord(object entity, Guid recordId, IEnumerable<string> columnNames) {
			var update = new SQLiteUpdate(Engine);
			update.Execute(entity, new []{ GetPrimaryCondition(recordId) }, columnNames);
		}
		protected virtual IEnumerable<string> GetAreaRecordColumns() {
			return GetBaseRecordColumns().Concat(new[] {
				"Country.Id"
			});
		}
		protected virtual IEnumerable<string> GetRegionRecordColumns() {
			return GetBaseRecordColumns().Concat(new[] {
				"Area.Id"
			});
		}
		protected virtual IEnumerable<string> GetBaseRecordColumns() {
			return new[] {
				"Id",
				"Name",
				"MapPoint"
			};
		}
		protected virtual IEnumerable<SQLiteColumn> GetAreaRecordSelectColumns() {
			return GetBaseRecordSelectColumns().Concat(new[] {
				SQLiteUtilities.CreateGuidColumn("Country.Id"),
				SQLiteUtilities.CreateStringColumn("Country.Name")
			});
		}
		protected virtual IEnumerable<SQLiteColumn> GetRegionRecordSelectColumns() {
			return GetBaseRecordSelectColumns().Concat(new[] {
				SQLiteUtilities.CreateGuidColumn("Area.Id"),
				SQLiteUtilities.CreateStringColumn("Area.Name")
			});
		}
		protected virtual IEnumerable<SQLiteColumn> GetBaseRecordSelectColumns() {
			return new[] {
				SQLiteUtilities.CreateGuidColumn("Id"),
				SQLiteUtilities.CreateStringColumn("Name"),
				SQLiteUtilities.CreateStringColumn("MapPoint")
			};
		}
		protected virtual IEnumerable<BaseLookup> GetRecords(string tableName, string search, string displayColumnName = "Name",
			params ISQLiteCondition[] conditions) {
			var select = new SQLiteSelect(Engine);
			if (!string.IsNullOrEmpty(search)) {
				select.AddCondition(search.CreateCondition(displayColumnName, SQLiteComparisonType.Contains));
			}
			select.AddCondition(conditions);
			return select.GetEntities<BaseLookup>(tableName, 
				new SQLiteColumn("Id", SQLiteColumnType.Guid),
				new SQLiteColumn(displayColumnName, SQLiteColumnType.String)
			);
		}
		protected virtual void DeleteRecord(string tableName, Guid id) {
			var delete = new SQLiteDelete(Engine);
			delete.Execute(tableName, GetPrimaryCondition(id));
		}
		protected virtual ISQLiteCondition GetPrimaryCondition(Guid id) {
			string columnName = "Id";
			return id.CreateCondition(columnName);
		}
	}
}