using System;
using System.Collections.Generic;
using System.Linq;
using TerritoriesAssignment.Core;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.Database.Storages.Mock {
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
				Name = "Lviv",
				Country = _countries.First(country => country.Name == "Ukraina"),
				MapPoint = "66.03:93.19,67.78:93.08,71.33:97.42,73.7:97.85,74.17:96.64,75.93:96.1,76.81:98.61,78.21:99.59,80.89:99,81.76:102.12,83.24:101.95,83.18:106.44,85.14:106.61,87.43:108.35,87.15:110.06,92.43:109.28,94.75:110.05,94.94:107.76,98.66:110.23,98.86:112.32,100.72:113.08,100.72:113.08,103.89:113.48,102.59:116.74,104.21:118.74,103.56:121.39,108.7:127.97,108.7:127.97,110.43:129,111.25:131.3,109.96:131.73,109.48:135.21,107:135.86,106.64:137.24,99.69:137.28,99.45:140.22,100.84:141.65,99.17:143.36,97.6:142.02,96.48:146.98,94.8:148.58,87.29:150.6,87.5:154.48,87.5:154.48,84.65:154.33,82.43:152.49,80.6:153.04,80.73:152.12,79.14:151.26,77.81:152.43,78.15:154,76.66:154.63,76.91:157.16,75.55:157.93,76.16:159.84,77.74:160.58,76.31:161.86,75.78:164.08,73.22:163.83,73.18:164.57,73.27:165.52,75.33:165.72,74.81:168.13,78.27:168.27,77.68:171.19,75.13:170.6,70.86:172.74,68.88:172.77,68.63:173.62,65.07:172.57,63.68:174.17,62.05:173,59.75:174.97,54.89:173.79,52.6:175.25,51.89:178.4,50.38:178.86,48.4:181.53,49.1:182.61,48.23:184.8,49.81:186.51,48.67:187.04,47.98:189.21,48.8:191.28,47.79:194.14,47.79:194.14,43.53:194.07,41.47:191.98,40.23:192.79,38.87:191.69,36.04:192.42,34.32:188.73,32.53:187.15,28.83:188.65,28.06:186.43,25.63:184.88,24.93:183.2,26.45:181.72,25.9:180.07,27.11:179.23,25.19:178.15,25.19:178.15,24.65:176.72,25.48:175.18,23.45:174.39,22.09:172.02,20.39:171.97,20.95:170.74,19.71:171.67,19.32:171.13,20.65:169.05,19.57:168.26,20.41:167.38,20.63:161.48,18.98:154.64,17.05:152.75,38.64:122.79,52.36:110.77,53.73:107.81,62.96:106.51,65.89:99.59"
			});
			_areas.Add(new Area {
				Id = Guid.NewGuid(),
				Name = "Vinnytsia",
				Country = _countries.First(country => country.Name == "Ukraina"),
				MapPoint = "192.8:141.47,196.95:139.79,200.85:140.23,206.05:139.39,206.78:137.99,211.22:139.72,212.73:137.49,214.81:138.26,216.27:137.69,217.72:139.99,219.48:137.97,223.23:138.79,223.92:136.39,226.99:135.48,227.95:134.02,230.64:136.52,231.17:138.47,230.27:139.58,232.35:141.96,230.46:144.99,232.62:147.33,231.95:149.27,236.33:149.95,240.47:148.96,241.55:149.63,241.7:147.63,243.81:148.35,244.3:147.59,245.14:148.02,246.04:146.08,249.34:146.54,249.34:146.54,250.3:147.22,249.22:149.41,250.62:153.24,252.09:153.99,250.91:155.7,252.37:156.87,249.21:160.85,250.52:163.59,253.25:164.46,253:166.64,255.56:167.96,256.37:166.76,257.13:168.44,257.13:168.44,256.1:168.71,257.12:173.98,255.36:174.5,255.56:175.79,254.34:175.39,253.56:177.56,253.13:176.87,252.83:177.5,254:179.66,255.43:179.06,255.8:180.21,254.33:183.46,257.03:185,256.41:185.67,257.94:188.14,258.27:191.28,261.62:192.37,262.23:193.9,261.43:195.57,265.89:198.95,264.33:200.97,266.27:202.07,266.79:204.83,265.85:204.8,266.2:205.98,265:206.28,265.02:207.19,265.02:207.19,258.96:212.38,259.78:213.46,257.69:216.46,258.73:220.53,258.73:220.53,254.52:221.3,254.79:225.64,252.67:224.44,251.58:226.16,249.26:225.95,248.14:224.88,245.62:225.33,244.88:226.54,241.81:226.61,241.03:225.46,239.68:225.89,239.15:225.11,241.08:224.53,240.77:223.65,237.07:224.03,237.56:223.57,234.62:221.33,233.61:224.9,233.1:225.23,232.68:223.59,230.45:223.23,229.84:226.35,227.81:227.6,227.12:227.01,227.12:227.01,226.89:224.79,221.8:224.74,221.48:223.73,217.61:222.25,215.5:223.48,215.36:227.79,214.17:227.41,212.71:224.94,212.94:222.44,211.37:222.33,210.34:224.25,208.83:224.09,210.89:220.92,210.38:218.66,205.43:220.8,203.99:217.97,201.51:219.33,201.09:215.21,196.76:214.42,194.85:212.85,194.05:210.62,191.12:208.48,186.26:208.69,184.55:207.71,184.73:206.18,182.6:207.3,182.6:207.3,180.77:205.39,179.87:200.71,177.18:199.15,177.18:199.15,178.61:195.73,177.73:192.18,179.04:190.67,177.84:185.01,178.69:181.68,177.48:179.73,179.43:176.61,181.5:176.52,181.29:173.86,182.75:173.88,182.66:172.93,184.16:173.92,184.73:172.1,186.27:171.54,189.62:172.94,190.93:170.09,191.42:171.06,192.18:170.45,193.51:171.33,194.98:170.54,193.57:167.97,193.67:163.3,192.15:161.79,193.34:158.02,189.96:157.49,189.96:157.49,190.16:156.28,190.16:156.28,191.1:155.04,189.53:154.6,189.54:152.86,191.35:152.79,191.45:151.49,190.22:150.33,192.04:147.86,190.41:144.08,192:143.86"
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
		public Country GetCountry(Guid id) {
			return _countries.Find(country => country.Id == id);
		}
		public Area GetArea(Guid id) {
			return _areas.Find(area => area.Id == id);
		}
		public Region GetRegion(Guid id) {
			return _regions.Find(region => region.Id == id);
		}
		public void AddCountry(Country country) {
			_countries.Add(country);
		}
		public void AddRegion(Region region) {
			_regions.Add(region);
		}
		public void UpdateCountry(Country country) {
			var item = GetCountry(country.Id);
			item.Name = country.Name;
		}
		public void UpdateArea(Area area) {
			var item = GetArea(area.Id);
			item.Name = area.Name;
		}
		public void UpdateRegion(Region region) {
			var item = GetRegion(region.Id);
			item.Name = region.Name;
		}
		public void AddArea(Area area) {
			_areas.Add(area);
		}
		public IEnumerable<BaseLookup> GetCountries(string search = null) {
			return _countries;
		}
		public IEnumerable<BaseLookup> GetAreas(Guid countryId, string search = null) {
			return _areas.Where(area => area.Country.Id == countryId);
		}
		public IEnumerable<BaseLookup> GetRegions(Guid areaId, string search = null) {
			return _regions.Where(region => region.Area.Id == areaId);
		}
		public void DeleteCountry(Guid countryId) {
			var item = GetCountry(countryId);
			_countries.Remove(item);
		}
		public void DeleteArea(Guid areaId) {
			var item = GetArea(areaId);
			_areas.Remove(item);
		}
		public void DeleteRegion(Guid regionId) {
			var item = GetRegion(regionId);
			_regions.Remove(item);
		}
	}
}