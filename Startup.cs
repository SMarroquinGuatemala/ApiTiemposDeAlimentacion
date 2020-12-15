using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ApiTiemposDeAlimentacion.Data;
using ApiTiemposDeAlimentacion.Models;
using ApiTiemposDeAlimentacion.Repository;
using ApiTiemposDeAlimentacion.Repository.IRepository;
using ApiTiemposDeAlimentacion.TiemposDeAlimentacionMapper;

using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ApiTiemposDeAlimentacion
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
         services.AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

         services.AddScoped<IModuloHabitacionalRepository, ModuloHabitacionalRepository>();
         services.AddScoped<ITiemposDeAlimentacionRepository, TiemposDeAlimentacionRepository>();
         services.AddScoped<IPersonalTiemposDeAlimentacionRepository, PersonalTiemposDeAlimentacionRepository>();
         services.AddScoped<IUsuarioRepository, UsuarioRepository>();

         /*Dependencias del token*/
         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                  ValidateIssuer = false,
                  ValidateAudience = false
               };
            });

         services.AddAutoMapper(typeof(TiemposDeAlimentacionMappers));
         //Configuración de la documentación de API
         services.AddSwaggerGen(options =>
         {
            options.SwaggerDoc("ApiTiemposDeAlimentacion", new Microsoft.OpenApi.Models.OpenApiInfo()
            {
               Title = "API Servicios de Comida",
               Version = "1",
               Description ="Backend Servicios de Comida",
               Contact = new Microsoft.OpenApi.Models.OpenApiContact()
               { 
                  Email ="soporte@sandiego.com.gt",
                  Name ="Tiempos de alimentación",
                  Url  = new Uri("https://www.sandiego.com.gt")
               },
               License = new Microsoft.OpenApi.Models.OpenApiLicense()
               { 
               Name = "MIT License",
               Url  = new Uri("https://opensource.org/licenses/MIT")
               }                  
            });

            options.SwaggerDoc("ApiModulosHabitacionales", new Microsoft.OpenApi.Models.OpenApiInfo()
            {
               Title = "API Modulos Habitacionales",
               Version = "1",
               Description = "Backend Modulos  Habitacionales",
               Contact = new Microsoft.OpenApi.Models.OpenApiContact()
               {
                  Email = "soporte@sandiego.com.gt",
                  Name = "Tiempos de alimentación",
                  Url = new Uri("https://www.sandiego.com.gt")
               },
               License = new Microsoft.OpenApi.Models.OpenApiLicense()
               {
                  Name = "MIT License",
                  Url = new Uri("https://opensource.org/licenses/MIT")
               }
            });

            options.SwaggerDoc("ApiPersonalTiemposDeAlimentacion", new Microsoft.OpenApi.Models.OpenApiInfo()
            {
               Title = "Api Personal Tiempos de Alimentacion",
               Version = "1",
               Description = "Backend Tiempos de Alimentación",
               Contact = new Microsoft.OpenApi.Models.OpenApiContact()
               {
                  Email = "soporte@sandiego.com.gt",
                  Name = "Tiempos de alimentación",
                  Url = new Uri("https://www.sandiego.com.gt")
               },
               License = new Microsoft.OpenApi.Models.OpenApiLicense()
               {
                  Name = "MIT License",
                  Url = new Uri("https://opensource.org/licenses/MIT")
               }
            });

            options.SwaggerDoc("ApiUsuarios", new Microsoft.OpenApi.Models.OpenApiInfo()
            {
               Title = "API Usuarios",
               Version = "1",
               Description = "Backend Usuarios",
               Contact = new Microsoft.OpenApi.Models.OpenApiContact()
               {
                  Email = "soporte@sandiego.com.gt",
                  Name = "Tiempos de alimentación",
                  Url = new Uri("https://www.sandiego.com.gt")
               },
               License = new Microsoft.OpenApi.Models.OpenApiLicense()
               {
                  Name = "MIT License",
                  Url = new Uri("https://opensource.org/licenses/MIT")
               }
            });


            var ArchivoXMLComentarios = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var rutaApiComentarios = Path.Combine(AppContext.BaseDirectory, ArchivoXMLComentarios);
            options.IncludeXmlComments(rutaApiComentarios);

            //Primero definir el esquema de seguridad
            options.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                   Description = "Autenticación JWT (Bearer)",
                   Type = SecuritySchemeType.Http,
                   Scheme = "bearer"
                });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }
                  });

         });
         //Configuración de la documentación de API



         services.AddControllers();         
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseHttpsRedirection();
         //Configuración de la documentación de API
         app.UseSwagger();

         app.UseSwaggerUI(options =>
         {
            //options.SwaggerEndpoint("/swagger/ApiTiemposDeAlimentacion/swagger.jason", "Api Tiempos De Alimentacion");
            options.SwaggerEndpoint("/swagger/ApiTiemposDeAlimentacion/swagger.json", "API Tiempos de Alimentacion");
            options.SwaggerEndpoint("/swagger/ApiModulosHabitacionales/swagger.json", "API Modulos Habitacionales");
            options.SwaggerEndpoint("/swagger/ApiPersonalTiemposDeAlimentacion/swagger.json", "API Pesonal Tiempos de Alimentacion");
            options.SwaggerEndpoint("/swagger/ApiUsuarios/swagger.json", "API Usuarios");
            

            options.RoutePrefix = "";
         });

         app.UseRouting();
         //Autenticación y Autorización
         app.UseAuthentication();
         app.UseAuthorization();
         //Autenticación y Autorización


         //app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });

         /*Damos soporte para CORS*/
         app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
      }
   }
}
