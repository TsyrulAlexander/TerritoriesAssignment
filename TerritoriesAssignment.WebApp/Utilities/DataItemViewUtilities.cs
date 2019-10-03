using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TerritoriesAssignment.Core.Entities;
using TerritoriesAssignment.WebApp.Models;

namespace TerritoriesAssignment.WebApp.Utilities
{
	public static class DataItemViewUtilities
	{
		public static CountryView ToView(this Country country) {
			return new CountryView(country);
		}
		public static IEnumerable<CountryView> ToView(this IEnumerable<Country> countries) {
			return countries.Select(country => new CountryView(country));
		}
	}
}
