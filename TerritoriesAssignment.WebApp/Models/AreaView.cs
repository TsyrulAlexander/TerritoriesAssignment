using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.WebApp.Models {
	public class AreaView : BaseMapViewItem, IDataItemView<Area> {
		public AreaView() { }
		public AreaView(Area area): base(area) {}
		public new Area Cast() {
			return new Area {
				Id = Id,
				Name = Name
			};
		}
	}
}