using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectCars.BL.Interface;
using ProjectCars.BL.Services;
using ProjectCars.DL.InMemoryDB;
using ProjectCars.DL.Interface;
using ProjectCars.DL.Services;

namespace ProjectCars
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InMemoryDb.Init();

            services.AddControllers();

            services.AddSingleton<IVehicleRepository, VehicleRepository>();
            services.AddSingleton<IRVRepository, RVRepository>();
            services.AddSingleton<ISUVRepository, SUVRepository>();

            services.AddSingleton<IVehicleService, VehicleService>();
            services.AddSingleton<IRVService, RVService>();
            services.AddSingleton<ISUVService, SUVService>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "LastTask",
                        Description = "Cars",
                        Version = "v1"
                    });
            });

            services.AddHealthChecks();

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "CARS");
            });
        }
    }
}
