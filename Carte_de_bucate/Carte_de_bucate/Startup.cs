using AutoMapper;
using Carte_de_bucate.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Newtonsoft.Json.Serialization;
using Carte_de_bucate.Data;

namespace Carte_de_bucate
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
            services.AddDbContext<ReteteContext>(option =>
            option.UseSqlServer(Configuration.GetConnectionString
            ("Retete_db_munte_local")));

            //   services.AddDbContext<ReteteContext>(option =>
            //option.UseSqlServer(Configuration.GetConnectionString
            //("Retete_testing")));



            //  .AddNewtonsoftJson(....) este pentru Patch
            services.AddControllers().AddNewtonsoftJson(s =>
            s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddScoped<IReteteServices, ReteteServiceSQL>();
            services.AddScoped<IIngredienteServices, IngredienteServicesSQL>();
            services.AddScoped<ITariServices, TariSerivecSQL>();

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
            });
        }
    }
}
