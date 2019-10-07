using System;
using System.Collections.Generic;
using System.Linq;
using SQLiteFramework;
using SQLiteFramework.Condition;
using SQLiteFramework.Exception;
using SQLiteFramework.Query;
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
			return GetRecord<Country>(id);
		}
		public virtual Area GetArea(Guid id) {
			return GetRecord<Area>(id);
		}
		public virtual Region GetRegion(Guid id) {
			return GetRecord<Region>(id);
		}
		public virtual void AddCountry(Country country) {
			AddRecord(country);
		}
		public virtual void AddArea(Area area) {
			AddRecord(area);
		}
		public virtual void AddRegion(Region region) {
			AddRecord(region);
		}
		public virtual void UpdateCountry(Country country) {
			UpdateRecord(country, country.Id);
		}
		public virtual void UpdateArea(Area area) {
			UpdateRecord(area, area.Id);
		}
		public virtual void UpdateRegion(Region region) {
			UpdateRecord(region, region.Id);
		}
		public virtual IEnumerable<Country> GetCountries(string search = null) {
			return GetRecords<Country>(search);
		}
		public virtual IEnumerable<Area> GetAreas(Guid countryId, string search = null) {
			return GetRecords<Area>(search, "Name", countryId.CreateCondition("CountryId"));
		}
		public virtual IEnumerable<Region> GetRegions(Guid areaId, string search = null) {
			return GetRecords<Region>(search, "Name", areaId.CreateCondition("AreaId"));
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
		protected virtual T GetRecord<T>(Guid recordId, string primaryColumnName = "Id") {
			var select = new SQLiteSelect(Engine);
			select.AddCondition(GetPrimaryCondition(recordId, primaryColumnName));
			return select.GetEntities<T>().FirstOrDefault();
		}
		protected virtual void AddRecord(object entity) {
			var insert = new SQLiteInsert(Engine);
			insert.Execute(entity);
		}
		protected virtual void UpdateRecord(object entity, Guid recordId, string primaryColumnName = "Id") {
			var update = new SQLiteUpdate(Engine);
			update.Execute(entity, GetPrimaryCondition(recordId, primaryColumnName));
		}
		protected virtual IEnumerable<T> GetRecords<T>(string search, string searchColumnName = "Name",
			params ISQLiteCondition[] conditions) {
			var select = new SQLiteSelect(Engine);
			if (!string.IsNullOrEmpty(search)) {
				select.AddCondition(search.CreateCondition(searchColumnName, SQLiteComparisonType.Contains));
			}
			select.AddCondition(conditions);
			return select.GetEntities<T>();
		}
		protected virtual void DeleteRecord(string tableName, Guid id) {
			var delete = new SQLiteDelete(Engine);
			delete.Execute(tableName, GetPrimaryCondition(id));
		}
		protected virtual ISQLiteCondition GetPrimaryCondition(Guid id, string columnName = "Id") {
			return id.CreateCondition(columnName);
		}
	}
}