using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.Core.Db.Storages.MSSql.EF {
	public class MsSqlEfDataStorage : IDataStorage {
		private MSSqlEFContext Context { get; set; }

		public MsSqlEfDataStorage(string connectionString) {
			Context = new MSSqlEFContext(connectionString);
		}
		public Country GetCountry(Guid id) {
			return Context.Countries.Find(id);
		}
		public Area GetArea(Guid id) {
			return Context.Areas.Find(id);
		}
		public Region GetRegion(Guid id) {
			return Context.Regions.Find(id);
		}
		public void AddCountry(Country country) {
			Context.Countries.Add(country);
			Context.SaveChanges();
		}
		public void AddRegion(Region region) {
			Context.Regions.Add(region);
			Context.SaveChanges();
		}
		public void UpdateCountry(Country country) {
			Context.Update(country);
			Context.SaveChanges();
		}
		public void UpdateArea(Area area) {
			Context.Update(area);
			Context.SaveChanges();
		}
		public void UpdateRegion(Region region) {
			Context.Update(region);
			Context.SaveChanges();
		}
		public void AddArea(Area area) {
			Context.Areas.Add(area);
			Context.SaveChanges();
		}
		public IEnumerable<Country> GetCountries() {
			return Context.Countries.AsNoTracking();
		}
		public IEnumerable<Area> GetAreas(Guid countryId) {
			return Context.Areas.Where(area => area.Country.Id == countryId).AsNoTracking();
		}
		public IEnumerable<Region> GetRegions(Guid areaId) {
			return Context.Regions.Where(region => region.Area.Id == areaId).AsNoTracking();
		}
		public void DeleteCountry(Guid countryId) {
			Context.Remove(GetCountry(countryId));
			Context.SaveChanges();
		}
		public void DeleteArea(Guid areaId) {
			Context.Remove(GetArea(areaId));
			Context.SaveChanges();
		}
		public void DeleteRegion(Guid regionId) {
			Context.Remove(GetRegion(regionId));
			Context.SaveChanges();
		}
	}
}
