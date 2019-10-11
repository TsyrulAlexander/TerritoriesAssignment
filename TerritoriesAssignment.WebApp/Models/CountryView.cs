using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.WebApp.Models {
	public class CountryView : BaseMapViewItem, IDataItemView<Country> {
		public CountryView() {}
		public CountryView(Country country) : base(country) { }
		public new Country Cast() {
			return new Country {
				Id = Id,
				Name = Name
			};
		}
	}
}