using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TerritoriesAssignment.Core;
using TerritoriesAssignment.Core.Entities;
using TerritoriesAssignment.WebApp.Models;

namespace TerritoriesAssignment.WebApp.Utilities
{
	public static class DataItemViewUtilities
	{
		public static BaseLookupViewItem ToView(this BaseLookup baseLookup) {
			return new BaseLookupViewItem(baseLookup);
		}
		public static CountryView ToView(this Country country) {
			return new CountryView(country);
		}
		public static AreaView ToView(this Area area) {
			return new AreaView(area);
		}
		public static RegionView ToView(this Region region) {
			return new RegionView(region);
		}
		public static IEnumerable<AreaView> ToView(this IEnumerable<Area> areas) {
			return areas.Select(area => area.ToView()).ToArray();
		}
		public static IEnumerable<RegionView> ToView(this IEnumerable<Region> regions) {
			return regions.Select(region => region.ToView()).ToArray();
		}
		public static IEnumerable<CountryView> ToView(this IEnumerable<Country> countries) {
			return countries.Select(country => country.ToView()).ToArray();
		}
		public static IEnumerable<BaseLookupViewItem> ToView(this IEnumerable<BaseLookup> values) {
			return values.Select(value => value.ToView()).ToArray();
		}
	}
}
