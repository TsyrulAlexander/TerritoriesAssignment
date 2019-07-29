using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.Core.Db.Storages.MSSql.EF
{
    public class MSSqlEFDataStorage:IDataStorage
    {
	    private MSSqlEFContext Context { get; set; }

	    public MSSqlEFDataStorage(string connectionString) {
			Context = new MSSqlEFContext(connectionString);

		}
		public void AddCountry(Country country) {
		    throw new NotImplementedException();
	    }

	    public void AddCity(City country) {
		    throw new NotImplementedException();
	    }

	    public IEnumerable<Country> GetCountries() {
		    return Context.Countries.AsNoTracking();
	    }

	    public IEnumerable<City> GetCities() {
		    throw new NotImplementedException();
	    }
    }
}
