using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.WebApp.Models {
	public class AttributeView : BaseLookupViewItem, IDataItemView<Attribute> {
		public AttributeView(Attribute attribute) {
			this.Id = attribute.Id;
			this.Name = attribute.Name;
		}
		public new Attribute Cast() {
			return new Attribute {
				Id = Id,
				Name = Name
			};
		}
	}
}