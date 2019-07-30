using Microsoft.EntityFrameworkCore;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.Core.Db.Storages.MSSql.EF {
	internal class MSSqlEFContext : DbContext {
		public string ConnectionString { get; }

		public DbSet<Country> Countries { get; set; }
		public DbSet<Area> Areas { get; set; }
		public DbSet<Region> Regions { get; set; }
		public MSSqlEFContext(string connectionString) {
			ConnectionString = connectionString;
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder.UseSqlServer(ConnectionString);
		}
	}
}