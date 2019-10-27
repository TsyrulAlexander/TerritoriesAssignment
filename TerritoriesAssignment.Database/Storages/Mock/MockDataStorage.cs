using System;
using System.Collections.Generic;
using System.Linq;
using TerritoriesAssignment.Core;
using TerritoriesAssignment.Core.Entities;
using TerritoriesAssignment.Core.Entities.Map;
using Attribute = TerritoriesAssignment.Core.Entities.Attribute;

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
		public Country GetCountry(Guid id) {
			return _countries.Find(country => country.Id == id);
		}
		public Area GetArea(Guid id) {
			return _areas.Find(area => area.Id == id);
		}
		public Region GetRegion(Guid id) {
			return _regions.Find(region => region.Id == id);
		}
		public Attribute GetAttribute(Guid id) {
			throw new NotImplementedException();
		}
		public AttributeValue GetAttributeValue(Guid attributeId, Guid regionId) {
			throw new NotImplementedException();
		}
		public void AddCountry(Country country) {
			_countries.Add(country);
		}
		public void AddRegion(Region region) {
			_regions.Add(region);
		}
		public void AddAttribute(Attribute attribute) {
			throw new NotImplementedException();
		}
		public void AddAttributeValue(AttributeValue attributeValue) {
			throw new NotImplementedException();
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
		public void UpdateAttribute(Attribute attribute) {
			throw new NotImplementedException();
		}
		public void UpdateAttributeValue(AttributeValue attributeValue) {
			throw new NotImplementedException();
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
		public IEnumerable<BaseMapLookup> GetAreasMap(Guid countryId) {
			return _areas;
		}
		public IEnumerable<BaseLookup> GetRegions(Guid areaId, string search = null) {
			return _regions.Where(region => region.Area.Id == areaId);
		}
		public IEnumerable<BaseLookup> GetAttributes(string search = null) {
			throw new NotImplementedException();
		}
		public IEnumerable<AttributeValue> GetAttributeValues(Guid regionId) {
			throw new NotImplementedException();
		}
		public IEnumerable<AttributeValue> GetAttributeValuesFromCountry(Guid countryId) {
			throw new NotImplementedException();
		}
		public IEnumerable<AttributeValue> GetAttributeValuesFromArea(Guid areaId) {
			throw new NotImplementedException();
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
		public void DeleteAttribute(Guid attributeId) {
			throw new NotImplementedException();
		}
		public void DeleteAttributeValue(Guid attributeValueId) {
			throw new NotImplementedException();
		}
	}
}