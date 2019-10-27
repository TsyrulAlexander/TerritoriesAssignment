using TerritoriesAssignment.Core.Entities;
using TerritoriesAssignment.WebApp.Utilities;

namespace TerritoriesAssignment.WebApp.Models {
	public class RegionView : BaseMapViewItem, IDataItemView<Region> {
		public BaseLookupViewItem Area { get; set; }
		public RegionView() { }
		public RegionView(Region region) : base(region) {
			Area = region?.Area?.ToView();
		}
		public new Region Cast() {
			return new Region {
				Id = Id,
				Name = Name,
				Area = Area.Cast()
			};
		}
	}
}
