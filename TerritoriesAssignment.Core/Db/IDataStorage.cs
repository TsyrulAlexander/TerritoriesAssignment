using System;
using System.Collections.Generic;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.Core.Db
{
	public interface IDataStorage {
		Country GetCountry(Guid id);
		Area GetArea(Guid id);
		Region GetRegion(Guid id);
		void AddCountry(Country country);
		void AddArea(Area area);
		void AddRegion(Region region);
		void UpdateCountry(Country country);
		void UpdateArea(Area area);
		void UpdateRegion(Region region);
		IEnumerable<Country> GetCountries();
		IEnumerable<Area> GetAreas(Guid countryId);
		IEnumerable<Region> GetRegions(Guid areaId);
		void DeleteCountry(Guid countryId);
		void DeleteArea(Guid areaId);
		void DeleteRegion(Guid regionId);
	}
}
