using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.WebApp.Models {
	public class CountryView : BaseMapViewItem, IDataItemView<Country> {
		public CountryView() : base(null) {

		}
		public CountryView(Country country = null) : base(country) {

		}
		public new Country Cast() {
			return new Country {
				Id = Id,
				Name = Name
			};
		}
	}
}