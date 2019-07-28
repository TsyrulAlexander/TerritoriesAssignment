using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.DependencyInjection;
using TerritoriesAssignment.Core.Db;
using TerritoriesAssignment.Core.Db.Storages.MSSql.EF;

namespace TerritoriesAssignment.WebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			services.AddMvc();
			services.Add(ServiceDescriptor.Singleton<IDataStorage>(provider => new MSSqlEFDataStorage(@"Server=.\SQLEXPRESS;Database=testDb;Trusted_Connection=True;")));
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
					HotModuleReplacement = true
				});
			}
			
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseMvc();
		}
	}
}