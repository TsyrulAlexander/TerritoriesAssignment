using System;
using System.Collections.Generic;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.Core.Db
{
	public interface IDataStorage {
		void AddCountry(Country country);
		void AddRegion(Region region);
		void AddArea(Area area);
		IEnumerable<Country> GetCountries();
		IEnumerable<Area> GetAreas(Guid countryId);
		IEnumerable<Region> GetRegions(Guid areaId);
	}
}
