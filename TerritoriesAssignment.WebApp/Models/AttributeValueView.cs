using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.WebApp.Models {
	public class AttributeValueView : BaseViewItem, IDataItemView<AttributeValue> {
		public double DoubleValue { get; set; }
		public BaseLookupViewItem Region { get; set; }
		public BaseLookupViewItem Attribute { get; set; }
		public AttributeValueView() {
			
		}
		public AttributeValueView(AttributeValue attributeValue) {
			Id = attributeValue.Id;
			DoubleValue = attributeValue.DoubleValue;
			Region = new BaseLookupViewItem(attributeValue.Region);
			Attribute = new BaseLookupViewItem(attributeValue.Attribute);
		}
		public AttributeValue Cast() {
			return new AttributeValue {
				Id = Id,
				DoubleValue = DoubleValue,
				Region = Region.Cast(),
				Attribute = Attribute.Cast()
			};
		}
	}
}