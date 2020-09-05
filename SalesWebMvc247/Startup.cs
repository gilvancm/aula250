using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc247.Data;
using SalesWebMvc247.Services;

namespace SalesWebMvc247
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
            services.AddControllersWithViews();
                                                        //recebe um delegate
            services.AddDbContext<SalesWebMvc247Context>(options =>
                          //servico do sqlserver
                      //  options.UseSqlServer(Configuration.GetConnectionString("SalesWebMvc247Context")));
                          //ele acresentou os servicos do mysql              classe de context      delegate                 nome do assemble no do projeto  
                         options.UseMySql(Configuration.GetConnectionString("SalesWebMvc247Context"), builder => builder.MigrationsAssembly("SalesWebMvc247")));
            //lembre do provide instalar o pacore MySql
          
            //registrar nosso serviço no  injeção de dependencia da aplicação para copiar os registros /objetos
            services.AddScoped<SeedingService>();
            services.AddScoped<SellerService>();
            services.AddScoped<DepartmentService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //acresentei SeedingService seedingService
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService)
        {
            //definir o local da aplicação como se fosse nos USA  -- App locale abaixo colocamos 3 atributos
            var enUS = new CultureInfo("en-Us");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS }
            };

            app.UseRequestLocalization(localizationOptions);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //chamar o metodo abaixo
                seedingService.Seed();
                
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
