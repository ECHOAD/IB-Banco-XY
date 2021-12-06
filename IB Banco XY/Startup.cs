using Capo_Datos;
using Contratos.Repository_Contracts;
using Contratos.BL_Contracts;
using Entidades;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Negocio;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contratos.Helpers;
using IB_Banco_XY.Helpers;

namespace IB_Banco_XY
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

            services.AddDbContext<InternetBanking>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<InternetBanking>();
            services.AddControllersWithViews();


            services.AddScoped<IRepositoryWrapper, RepositoryWapper>();


            ConfigureHelpers(services);
            ConfigureBusineesLayer(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapRazorPages();
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }

        public void ConfigureBusineesLayer(IServiceCollection services)
        {
            services.AddScoped<ICuentaAhorroBL, CuentaAhorroBL>();
            services.AddScoped<ITransferenciaBL, TransferenciaBL>();
            services.AddScoped<IEstadoCuentaBL, EstadoCuentaBL>();
            services.AddScoped<ITarjetaCreditoBL, TarjetaCreditoBL>();
            services.AddScoped<IEstadoCreditoBL, EstadoCreditoBL>();
            services.AddScoped<IEstadoPrestamoBL, EstadoPrestamoBL>();
            services.AddScoped<IPrestamoBL, PrestamoBL>();



        }

        public void ConfigureHelpers(IServiceCollection service)
        {
            service.AddScoped<INumberGenerator<CuentasAhorro>, AccountNumberGenerator>();
            service.AddScoped<INumberGenerator<TarjetaCredito>, CreditCardNumberGenerator>();
            service.AddScoped<INumberGenerator<Prestamo>, PrestamoNumberGenerator>();


        }

    }
}
