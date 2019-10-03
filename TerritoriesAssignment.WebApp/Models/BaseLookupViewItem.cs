using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TerritoriesAssignment.Core;

namespace TerritoriesAssignment.WebApp.Models
{
	public class BaseLookupViewItem : BaseViewItem {
		public string Name { get; set; }
		public BaseLookupViewItem() { }
		public BaseLookupViewItem(BaseLookup baseLookup): base(baseLookup) {
			Name = baseLookup.Name;
		}
	}
}
