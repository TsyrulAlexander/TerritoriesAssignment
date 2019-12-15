using Microsoft.Extensions.DependencyInjection;
using TerritoriesAssignment.Core;
using TerritoriesAssignment.Database.Storages.SQLite;

namespace TerritoriesAssignment.WebApp.Extensions {
	public static class DataStorageExtensions {
		public static void AddDataStorage(this IServiceCollection services) {
			//Configuration.GetConnectionString("MSSqlEFDataStorage"));
			//services.Add(ServiceDescriptor.Singleton<IDataStorage>(provider => new MSSqlEFDataStorage()));
			//services.Add(ServiceDescriptor.Singleton<IDataStorage>(provider => new MockDataStorage()));
			services.Add(ServiceDescriptor.Singleton<IDataStorage>(provider => new SQLiteDataStorage(@"C:\Temp\azaza.db")));
		}
	}
}