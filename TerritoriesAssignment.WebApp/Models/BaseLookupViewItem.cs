using TerritoriesAssignment.Core;

namespace TerritoriesAssignment.WebApp.Models {
	public class BaseLookupViewItem : BaseViewItem, IDataItemView<BaseLookup> {
		public string Name { get; set; }
		public BaseLookupViewItem() { }
		public BaseLookupViewItem(BaseLookup baseLookup = null) : base(baseLookup) {
			Name = baseLookup?.Name;
		}
		public BaseLookup Cast() {
			return new BaseLookup {
				Id = Id,
				Name = Name
			};
		}
	}
}
