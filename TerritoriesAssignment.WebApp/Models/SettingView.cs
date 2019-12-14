using TerritoriesAssignment.Core.Entities.Setting;

namespace TerritoriesAssignment.WebApp.Models {
	public class SettingView : BaseLookupViewItem, IDataItemView<Setting> {
		public int Type { get; set; }
		public SettingView() { }
		public SettingView(Setting setting) : base(setting) {
			Type = (int) setting.Type;
		}
		public new Setting Cast() {
			return new Setting {
				Id = Id,
				Type = (SettingType) Type,
				Name = Name
			};
		}
	}
}