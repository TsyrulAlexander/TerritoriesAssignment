namespace TerritoriesAssignment.Core.Entities {
	public class AttributeValue : BaseObject {
		public double DoubleValue { get; set; }
		public BaseLookup Attribute { get; set; }
		public BaseLookup Region { get; set; }
	}
}