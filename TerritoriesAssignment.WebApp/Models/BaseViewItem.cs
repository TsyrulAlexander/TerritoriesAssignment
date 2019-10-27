using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TerritoriesAssignment.Core;

namespace TerritoriesAssignment.WebApp.Models
{
	public class BaseViewItem {
		public Guid Id { get; set; }
		public BaseViewItem() { }
		public BaseViewItem(BaseObject baseObject = null) {
			Id = baseObject?.Id ?? Guid.Empty;
		}
	}
}
