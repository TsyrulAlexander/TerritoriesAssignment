using TerritoriesAssignment.Core.Db;

namespace TerritoriesAssignment.Core.Entities {
	public class Area : BaseLookup {
		public Country Country { get; set; }
	}
}