using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.WebApp.Models {
	public class RegionView : BaseMapViewItem, IDataItemView<Region> {
		public RegionView() { }
		public RegionView(Region region) : base(region) { }
		public new Region Cast() {
			return new Region {
				Id = Id,
				Name = Name,
				Area = new Area {
					Id = Id
				}
			};
		}
	}
}
