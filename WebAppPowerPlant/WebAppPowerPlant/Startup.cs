using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PowerPlantApplication.Models;
using PowerPlantApplication.Services;

namespace WebAppPowerPlant
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAppPowerPlant v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAppPowerPlant", Version = "v1" });
            });
            services.AddTransient<ICalculatingService, CalculatingService>();
            services.AddTransient<IPowerPlantGasfiredCalculation, PowerPlantGasfiredCalculation>();
            services.AddTransient<IPowerPlantTurbojetCalculation, PowerPlantTurbojetCalculation>();
            services.AddTransient<IPowerPlantWindturbineCalculation, PowerPlantWindturbineCalculation>();
        }
    }
}