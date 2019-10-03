using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.WebApp.Models {
	public class CountryView : BaseMapViewItem, IDataItemView<Country> {
		public CountryView(Country country) : base(country) { }
		public Country Cast() {
			return new Country {
				Id = Id,
				Name = Name
			};
		}
	}
}