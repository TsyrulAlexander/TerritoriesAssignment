using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Core;
using TerritoriesAssignment.Core.Entities.Setting;
using TerritoriesAssignment.Database;
using TerritoriesAssignment.WebApp.Models;

namespace TerritoriesAssignment.WebApp.Controllers {
	[ApiController]
	[Route("api/setting")]
	public class SettingController : Controller {
		public IDataStorage Storage { get; }
		public SettingController(IDataStorage storage) {
			Storage = storage;
		}
		[HttpPost("getSettings")]
		public SettingValueView[] GetSettings() {
			return Storage.GetSettingsValue().Select(value => new SettingValueView(value)).ToArray();
		}
		[HttpPost("setSettings")]
		public void SetSettings([FromBody] SettingValueView[] settingValues) {
			foreach (var settingValueView in settingValues) {
				var settingValue = settingValueView.Cast();
				Storage.UpdateSettingValue(settingValue);
			}
		}
	}
}