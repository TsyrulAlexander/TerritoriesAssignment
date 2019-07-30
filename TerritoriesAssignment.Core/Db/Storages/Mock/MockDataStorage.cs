using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.Core.Db.Storages.Mock {
	public class MockDataStorage : IDataStorage {
		readonly List<Country> _countries = new List<Country>();
		readonly List<Area> _areas = new List<Area>();
		readonly List<Region> _regions = new List<Region>();
		public MockDataStorage() {
			_countries.Add(new Country {
				Id = Guid.NewGuid(),
				Name = "Ukraina"
			});
			_countries.Add(new Country {
				Id = Guid.NewGuid(),
				Name = "China"
			});
			_areas.Add(new Area {
				Id = Guid.NewGuid(),
				Name = "Kyiv",
				Country = _countries.First(country => country.Name == "Ukraina")
			});
			_areas.Add(new Area {
				Id = Guid.NewGuid(),
				Name = "Kyiv",
				Country = _countries.First(country => country.Name == "Ukraina")
			});
			_areas.Add(new Area {
				Id = Guid.NewGuid(),
				Name = "Vinnytsia",
				Country = _countries.First(country => country.Name == "Ukraina")
			});
			_regions.Add(new Region {
				Id = Guid.NewGuid(),
				Name = "Barsky",
				Area = _areas.First(area => area.Name == "Vinnytsia")
			});
			_regions.Add(new Region {
				Id = Guid.NewGuid(),
				Name = "Bershadsky",
				Area = _areas.First(area => area.Name == "Vinnytsia")
			});
			_regions.Add(new Region {
				Id = Guid.NewGuid(),
				Name = "Gaysinsky",
				Area = _areas.First(area => area.Name == "Vinnytsia")
			});
		}
		public void AddCountry(Country country) {
			_countries.Add(country);
		}
		public void AddRegion(Region region) {
			_regions.Add(region);
		}
		public void AddArea(Area area) {
			_areas.Add(area);
		}
		public IEnumerable<Country> GetCountries() {
			return _countries;
		}
		public IEnumerable<Area> GetAreas(Guid countryId) {
			return _areas.Where(area => area.Country.Id == countryId);
		}
		public IEnumerable<Region> GetRegions(Guid areaId) {
			return _regions.Where(region => region.Area.Id == areaId);
		}
	}
}