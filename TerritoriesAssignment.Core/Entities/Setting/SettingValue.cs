namespace TerritoriesAssignment.Core.Entities.Setting {
	public class SettingValue : BaseObject {
		public Setting Setting { get; set; }
		public int IntegerValue { get; set; }
		public double DoubleValue { get; set; }
		public bool BooleanValue { get; set; }
	}
}
