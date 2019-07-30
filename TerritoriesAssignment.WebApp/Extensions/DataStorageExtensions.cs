using Microsoft.Extensions.DependencyInjection;
using TerritoriesAssignment.Core.Db;
using TerritoriesAssignment.Core.Db.Storages.Mock;

namespace TerritoriesAssignment.WebApp.Extensions {
	public static class DataStorageExtensions {
		public static void AddDataStorage(this IServiceCollection services) {
			//Configuration.GetConnectionString("MSSqlEFDataStorage"));
			//services.Add(ServiceDescriptor.Singleton<IDataStorage>(provider => new MSSqlEFDataStorage()));
			services.Add(ServiceDescriptor.Singleton<IDataStorage>(provider => new MockDataStorage()));
		}
	}
}