using Microsoft.Extensions.DependencyInjection;
using TerritoriesAssignment.Database;
using TerritoriesAssignment.Database.Storages.Mock;

namespace TerritoriesAssignment.WebApp.Extensions {
	public static class DataStorageExtensions {
		public static void AddDataStorage(this IServiceCollection services) {
			//Configuration.GetConnectionString("MSSqlEFDataStorage"));
			//services.Add(ServiceDescriptor.Singleton<IDataStorage>(provider => new MSSqlEFDataStorage()));
			services.Add(ServiceDescriptor.Singleton<IDataStorage>(provider => new MockDataStorage()));
		}
	}
}