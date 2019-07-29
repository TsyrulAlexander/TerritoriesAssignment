using System.Collections.Generic;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.Core.Db
{
	public interface IDataStorage {
		void AddCountry(Country country);
		void AddCity(City country);
		IEnumerable<Country> GetCountries();
		IEnumerable<City> GetCities();
	}
}
