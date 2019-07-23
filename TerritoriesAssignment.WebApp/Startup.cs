using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.DependencyInjection;

namespace TerritoriesAssignment.WebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {

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
		}
	}
}