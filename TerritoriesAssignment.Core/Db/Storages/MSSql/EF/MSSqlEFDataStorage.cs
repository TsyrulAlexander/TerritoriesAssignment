using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.Core.Db.Storages.MSSql.EF {
	public class MSSqlEFDataStorage : IDataStorage {
		private MSSqlEFContext Context { get; set; }

		public MSSqlEFDataStorage(string connectionString) {
			Context = new MSSqlEFContext(connectionString);
		}
		public void AddCountry(Country country) {
			Context.Countries.Add(country);
			Context.SaveChanges();
		}
		public void AddRegion(Region region) {
			Context.Regions.Add(region);
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
			return Context.Areas.Where(area => area.Country.Id == countryId);
		}
		public IEnumerable<Region> GetRegions(Guid areaId) {
			return Context.Regions.Where(region => region.Area.Id == areaId);
		}
	}
}
