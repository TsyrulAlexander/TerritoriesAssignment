using TerritoriesAssignment.Core.Db;

namespace TerritoriesAssignment.Core.Entities
{
	public class City : BaseLookup {
		public Country Country { get; set; }
	}
}
