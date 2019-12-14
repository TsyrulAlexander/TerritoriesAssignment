using TerritoriesAssignment.Core;
using TerritoriesAssignment.Core.Entities.Setting;

namespace TerritoriesAssignment.WebApp.Models {
	public class SettingValueView : BaseViewItem, IDataItemView<SettingValue> {
		public SettingView Setting { get; set; }
		public int IntegerValue { get; set; }
		public double DoubleValue { get; set; }
		public bool BoolValue { get; set; }
		public SettingValueView() { }
		public SettingValueView(SettingValue value): base(value) {
			Setting = new SettingView(value.Setting);
			BoolValue = value.BooleanValue;
			DoubleValue = value.DoubleValue;
			IntegerValue = value.IntegerValue;
		}
		public SettingValue Cast() {
			return new SettingValue {
				Id = Id,
				Setting = Setting.Cast(),
				DoubleValue = DoubleValue,
				BooleanValue = BoolValue,
				IntegerValue = IntegerValue
			};
		}
	}
}
