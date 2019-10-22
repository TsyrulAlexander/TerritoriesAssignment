using System;
using System.Collections.Generic;
using TerritoriesAssignment.Core;
using TerritoriesAssignment.Core.Entities;
using TerritoriesAssignment.Core.Entities.Map;

namespace TerritoriesAssignment.Database
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
		IEnumerable<BaseLookup> GetCountries(string search = null);
		IEnumerable<BaseLookup> GetAreas(Guid countryId, string search = null);
		IEnumerable<BaseMapLookup> GetAreasMap(Guid countryId);
		IEnumerable<BaseLookup> GetRegions(Guid areaId, string search = null);
		void DeleteCountry(Guid countryId);
		void DeleteArea(Guid areaId);
		void DeleteRegion(Guid regionId);
	}
}
