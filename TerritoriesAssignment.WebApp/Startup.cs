using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.DependencyInjection;
using TerritoriesAssignment.WebApp.Extensions;

namespace TerritoriesAssignment.WebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			services.AddMvc();
			services.AddDataStorage();
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
			app.UseMvc(routes => {
				routes
				.MapRoute(name: "api", template: "api/{controller}/{action}/{id?}");
			});
		}
	}
}