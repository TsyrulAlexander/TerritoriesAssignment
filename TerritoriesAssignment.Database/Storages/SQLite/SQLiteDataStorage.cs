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
using TerritoriesAssignment.Core.Entities.Map;
using TerritoriesAssignment.Core.Entities.Setting;
using Attribute = TerritoriesAssignment.Core.Entities.Attribute;

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
		public Attribute GetAttribute(Guid id) {
			return GetRecord<Attribute>(id, GetBaseLookupSelectColumns());
		}
		public Setting GetSetting(Guid id) {
			return GetRecord<Setting>(id, GetBaseLookupSelectColumns().Concat(new [] {
				new SQLiteColumn("Type"), 
			}));
		}
		public AttributeValue GetAttributeValue(Guid attributeId, Guid regionId) {
			var select = new SQLiteSelect(Engine);
			select.AddCondition(
				attributeId.CreateCondition("AttributeId"),
				regionId.CreateCondition("RegionId"));
			return select.GetEntities<AttributeValue>("AttributeValue",
				new SQLiteColumn("Id", SQLiteColumnType.Guid),
				new SQLiteColumn("DoubleValue", SQLiteColumnType.Double),
				new SQLiteColumn("Attribute.Id", SQLiteColumnType.Guid),
				new SQLiteColumn("Attribute.Name", SQLiteColumnType.String),
				new SQLiteColumn("Region.Id", SQLiteColumnType.Guid),
				new SQLiteColumn("Region.Name", SQLiteColumnType.String)
			).FirstOrDefault();
		}
		public SettingValue GetSettingValue(Guid settingId) {
			var select = new SQLiteSelect(Engine);
			select.AddCondition(
				settingId.CreateCondition("SettingId"));
			return select.GetEntities<SettingValue>("SettingValue",
				new SQLiteColumn("Id", SQLiteColumnType.Guid),
				new SQLiteColumn("Setting.Id", SQLiteColumnType.Guid),
				new SQLiteColumn("Setting.Name", SQLiteColumnType.String),
				new SQLiteColumn("DoubleValue", SQLiteColumnType.Double),
				new SQLiteColumn("IntegerValue", SQLiteColumnType.Integer),
				new SQLiteColumn("BooleanValue", SQLiteColumnType.Boolean)
			).FirstOrDefault();
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
		public void AddAttribute(Attribute attribute) {
			AddRecord(attribute, GetBaseLookupColumns());
		}
		public void AddAttributeValue(AttributeValue attributeValue) {
			AddRecord(attributeValue, GetAttributeValueColumns());
		}
		public void AddSetting(Setting setting) {
			AddRecord(setting, GetBaseLookupColumns());
		}
		public void AddSettingValue(SettingValue value) {
			AddRecord(value, GetBaseLookupColumns().Concat(new [] {
				"Setting.Id",
				"IntegerValue",
				"DoubleValue",
				"BooleanValue"
			}));
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
		public void UpdateAttribute(Attribute attribute) {
			UpdateRecord(attribute, attribute.Id, GetBaseLookupColumns());
		}
		public void UpdateAttributeValue(AttributeValue attributeValue) {
			UpdateRecord(attributeValue, attributeValue.Id, GetAttributeValueColumns());
		}
		public void UpdateSetting(Setting setting) {
			UpdateRecord(setting, setting.Id, GetBaseLookupColumns());
		}
		public void UpdateSettingValue(SettingValue value) {
			UpdateRecord(value, value.Id, GetSettingValueColumns());
		}
		public virtual IEnumerable<BaseLookup> GetCountries(string search = null) {
			return GetRecords(nameof(Country), search);
		}
		public virtual IEnumerable<BaseLookup> GetAreas(Guid countryId, string search = null) {
			return GetRecords(nameof(Area), search, "Name", countryId.CreateCondition("CountryId"));
		}
		public IEnumerable<BaseMapLookup> GetAreasMap(Guid countryId) {
			var select = new SQLiteSelect(Engine);
			select.AddCondition(countryId.CreateCondition("CountryId"));
			return select.GetEntities<BaseMapLookup>("Area", 
				new SQLiteColumn("Id", SQLiteColumnType.Guid),
				new SQLiteColumn("Name", SQLiteColumnType.String),
				new SQLiteColumn("Path", SQLiteColumnType.String)
			);
		}
		public virtual IEnumerable<BaseLookup> GetRegions(Guid areaId, string search = null) {
			return GetRecords(nameof(Region), search, "Name", areaId.CreateCondition("AreaId"));
		}
		public IEnumerable<BaseLookup> GetAttributes(string search = null) {
			return GetRecords(nameof(Attribute), search);
		}
		public IEnumerable<AttributeValue> GetAttributeValues(Guid regionId) {
			var select = new SQLiteSelect(Engine);
			select.AddCondition(regionId.CreateCondition("RegionId"));
			return select.GetEntities<AttributeValue>("AttributeValue", GetAttributeValueSelectColumns().ToArray());
		}
		public IEnumerable<AttributeValue> GetAttributeValuesFromCountry(Guid countryId) {
			var select = new SQLiteSelect(Engine);
			select.AddCondition(countryId.CreateCondition("Region.Area.Country.Id"));
			return select.GetEntities<AttributeValue>("AttributeValue", GetAttributeValueSelectColumns().ToArray());
		}
		public IEnumerable<AttributeValue> GetAttributeValuesFromArea(Guid areaId) {
			var select = new SQLiteSelect(Engine);
			select.AddCondition(areaId.CreateCondition("Region.Area.Id"));
			return select.GetEntities<AttributeValue>("AttributeValue", GetAttributeValueSelectColumns().ToArray());
		}
		public IEnumerable<Setting> GetSettings() {
			var select = new SQLiteSelect(Engine);
			return select.GetEntities<Setting>("Setting", GetSettingSelectColumns().ToArray());
		}
		public IEnumerable<SettingValue> GetSettingsValue() {
			var select = new SQLiteSelect(Engine);
			return select.GetEntities<SettingValue>("SettingValue", GetSettingValueSelectColumns().ToArray());
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
		public void DeleteAttribute(Guid attributeId) {
			DeleteRecord(nameof(Attribute), attributeId);
		}
		public void DeleteAttributeValue(Guid attributeValueId) {
			DeleteRecord(nameof(AttributeValue), attributeValueId);
		}
		public void DeleteSetting(Guid settingId) {
			throw new NotImplementedException();
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

		protected virtual IEnumerable<SQLiteColumn> GetAttributeValueSelectColumns() {
			return new[] {
				new SQLiteColumn("Id", SQLiteColumnType.Guid),
				new SQLiteColumn("DoubleValue", SQLiteColumnType.Double),
				new SQLiteColumn("Attribute.Id", SQLiteColumnType.Guid),
				new SQLiteColumn("Attribute.Name", SQLiteColumnType.String),
				new SQLiteColumn("Region.Id", SQLiteColumnType.Guid),
				new SQLiteColumn("Region.Name", SQLiteColumnType.String)
			};
		}
		protected virtual IEnumerable<SQLiteColumn> GetSettingValueSelectColumns() {
			return new[] {
				new SQLiteColumn("Id", SQLiteColumnType.Guid),
				new SQLiteColumn("Setting.Id", SQLiteColumnType.Guid),
				new SQLiteColumn("Setting.Name", SQLiteColumnType.String),
				new SQLiteColumn("Setting.Type", SQLiteColumnType.Integer),
				new SQLiteColumn("DoubleValue", SQLiteColumnType.Double),
				new SQLiteColumn("IntegerValue", SQLiteColumnType.Integer),
				new SQLiteColumn("BooleanValue", SQLiteColumnType.Boolean)
			};
		}
		protected virtual IEnumerable<SQLiteColumn> GetSettingSelectColumns() {
			return new[] {
				new SQLiteColumn("Id", SQLiteColumnType.Guid),
				new SQLiteColumn("Name", SQLiteColumnType.String),
				new SQLiteColumn("Type", SQLiteColumnType.Integer)
			};
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
		protected virtual IEnumerable<string> GetAttributeValueColumns() {
			return new[] {
				"Id",
				"DoubleValue",
				"Attribute.Id",
				"Region.Id"
			};
		}
		protected virtual IEnumerable<string> GetSettingValueColumns() {
			return new[] {
				"Id",
				"Setting.Id",
				"DoubleValue",
				"IntegerValue",
				"BooleanValue"
			};
		}
		protected virtual IEnumerable<string> GetBaseRecordColumns() {
			return GetBaseLookupColumns().Concat(new[] {
				"Path"
			});
		}
		protected virtual IEnumerable<string> GetBaseLookupColumns() {
			return new[] {
				"Id",
				"Name"
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
		protected virtual IEnumerable<SQLiteColumn> GetBaseLookupSelectColumns() {
			return new[] {
				SQLiteUtilities.CreateGuidColumn("Id"),
				SQLiteUtilities.CreateStringColumn("Name")
			};
		}
		protected virtual IEnumerable<SQLiteColumn> GetBaseRecordSelectColumns() {
			return GetBaseLookupSelectColumns().Concat(new[] {
				SQLiteUtilities.CreateStringColumn("Path")
			});
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
			const string columnName = "Id";
			return id.CreateCondition(columnName);
		}
	}
}