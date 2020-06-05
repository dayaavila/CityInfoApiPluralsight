using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CityInfoAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //este metodo estaba vacio 
            //para ejecutar el MVC
            //services.AddMvc();

            //Si queremos cmbiar o eliminar el formato json que es el que viene por defecto
            services.AddMvc()
                .AddMvcOptions(o =>
                {
                    o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                });


            //En caso se necesite serializar el formato Json, para que los parametros comincen con letra mayuscula, Ejemplo id con este codigo ahora sera: Id Id
            //services.AddMvc()
            //    .AddJsonOptions(o =>
            //    {
            //        if (o.SerializerSettings.ContractResolver != null)
            //        {
            //            var castedResolved = o.SerializerSettings.ContractResolver
            //                                 as DefaultContractResolver;
            //            castedResolved.NamingStrategy = null;
            //        }
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ////if (env.IsDevelopment())
            ////{
            ////    app.UseDeveloperExceptionPage();
            ////}

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else //este trozo fue añadido y el las propiedades del proyecto CityInfoAPI, cambiamos el environment de Development a Production, guardamos y corremos la app, veremos que salta el herror pero una sola vez y no toda la inf que solo los desarrolladores debemos ver jejeje.
            {
                app.UseExceptionHandler();
            }

            //Me permite ver el mensaje del status en postman 
            app.UseStatusCodePages();

            app.UseMvc(); //Desde este momento, el MVC middleware will handle HTTP requests.
                          //Eso significa que ahora podemos deshacerse del código del módulo anterior. 

            ////Este trozo comentado fue eliminado
            ////app.Run(async (context) =>
            //app.Run(async (context) =>
            // {
            //    //await context.Response.WriteAsync("Hello World!");
            //    throw new Exception("Example exception");
            //});

            app.UseStatusCodePages(); // el not found retorna un simple text-based message
        }
    }
}
