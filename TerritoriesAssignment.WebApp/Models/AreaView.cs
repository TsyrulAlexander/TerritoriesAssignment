using TerritoriesAssignment.Core.Entities;
using TerritoriesAssignment.WebApp.Utilities;

namespace TerritoriesAssignment.WebApp.Models {
	public class AreaView : BaseMapViewItem, IDataItemView<Area> {
		public BaseLookupViewItem Country { get; set; }
		public AreaView() { }
		public AreaView(Area area) : base(area) {
			Country = area.Country.ToView();
		}
		public new Area Cast() {
			return new Area {
				Id = Id,
				Name = Name,
				Country = Country.Cast()
			};
		}
	}
}