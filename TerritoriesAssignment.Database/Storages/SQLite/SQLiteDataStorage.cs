using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLiteFramework;
using SQLiteFramework.Command;
using SQLiteFramework.Condition;
using SQLiteFramework.Condition.Column;
using SQLiteFramework.Condition.Value;
using SQLiteFramework.Table;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.Database.Storages.SQLite
{
	public class SQLiteDataStorage: IDataStorage
	{
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
		public Country GetCountry(Guid id) {
			var selectCommand = new SQLiteSelectCommand(typeof(Country), new [] {
				new SQLiteCondition("Id", SQLiteComparisonType.Equal, new GuidConditionValue(id)) 
			});
			var response = Engine.ExecuteCommand<Country>(selectCommand);
			return response.First();
		}
		public Area GetArea(Guid id) {
			throw new NotImplementedException();
		}
		public Region GetRegion(Guid id) {
			throw new NotImplementedException();
		}
		public void AddCountry(Country country) {
			var insertCommand = new SQLiteInsertCommand(country);
			Engine.ExecuteCommand(insertCommand);
		}
		public void AddArea(Area area) {
			throw new NotImplementedException();
		}
		public void AddRegion(Region region) {
			throw new NotImplementedException();
		}
		public void UpdateCountry(Country country) {
			var updateCommand = new SQLiteUpdateCommand(country, new [] {
				new SQLiteCondition("Id", SQLiteComparisonType.Equal, SQLiteUtilities.Value(country.Id)) 
			});
			Engine.ExecuteCommand(updateCommand);
		}
		public void UpdateArea(Area area) {
			throw new NotImplementedException();
		}
		public void UpdateRegion(Region region) {
			throw new NotImplementedException();
		}
		public IEnumerable<Country> GetCountries() {
			var selectCommand = new SQLiteSelectCommand(typeof(Country));
			var response = Engine.ExecuteCommand<Country>(selectCommand);
			return response;
		}
		public IEnumerable<Area> GetAreas(Guid countryId) {
			throw new NotImplementedException();
		}
		public IEnumerable<Region> GetRegions(Guid areaId) {
			throw new NotImplementedException();
		}
		public void DeleteCountry(Guid countryId) {
			throw new NotImplementedException();
		}
		public void DeleteArea(Guid areaId) {
			throw new NotImplementedException();
		}
		public void DeleteRegion(Guid regionId) {
			throw new NotImplementedException();
		}
	}
}
