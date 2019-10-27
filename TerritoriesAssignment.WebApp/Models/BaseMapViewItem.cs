using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TerritoriesAssignment.Core.Entities.Map;

namespace TerritoriesAssignment.WebApp.Models
{
	public class BaseMapViewItem : BaseLookupViewItem {
		public string Path { get; }
		public BaseMapViewItem(BaseMapLookup baseMapLookup = null) : base(baseMapLookup) {
			Path = baseMapLookup?.Path;
		}
	}
}
