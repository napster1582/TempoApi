using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Napster.BuildToken.Jwt;
using tempo_api.Interfaces.Repositories;
using tempo_api.Interfaces.Services;
using tempo_api.Models;
using tempo_api.Models.Account;
using tempo_api.Models.Generic;
using tempo_api.Repositories;
using tempo_api.Services;

namespace tempo_api
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


           services.AddCors();
           services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

           services.AddDbContext<TEMPOContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("TempoDb")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                      .AddEntityFrameworkStores<TEMPOContext>()
                      // Traducir los mensajes  de error a español para mostrarlos en el frontend
                      .AddErrorDescriber<IdentityErrorDescription>();



            // Injección de dependencias
            services.AddSingleton<ITokenFactory, TokenFactory>();

            services.AddTransient<IEmpleadosService, EmpleadosService>();
            services.AddScoped<IEmpleadosRepository, EmpleadosRepository>();

            services.AddTransient<IActividadService, ActividadService>();
            services.AddScoped<IActividadRepository, ActividadRepository>();

            services.AddTransient<IDetalleActividadService, DetalleActividadService>();
            services.AddScoped<IDetalleActividadRepository, DetalleActividadRepository>();




            services.AddMvc().AddJsonOptions(configureJson);

        }


        private void configureJson(MvcJsonOptions obj)
        {
            obj.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            obj.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            obj.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
