﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BKind.Data;
using BKind.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using BKind.Services;
using BKind.Hubs;

namespace BKind
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

            //conectarea la BKindContext
            services.AddDbContext<BKindContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("BKindContext")));

            //specificatii privind autentificarea
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedEmail = true; //utilizatorul are nevoie de o adresa confirmata pentru a se loga
            }).AddEntityFrameworkStores<BKindContext>()
             .AddDefaultTokenProviders();


            // adaugarea serviciilor pentru trimitere email
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build(); //policy care se aplica global, asupra tuturor controllers, nimeni nu poate vedea paginile daca nu este logat
                options.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllersWithViews();

            //adaugarea serviciilor de real-time
            services.AddSignalR(configure => configure.EnableDetailedErrors = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            
            //autentificarea precede autorizarea utilizatorului
            app.UseAuthentication();
            app.UseAuthorization();

            //configurarea rutelor
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Requests}/{action=Index}/{id?}");   //utilizatorul logat este redirectionat la pagina cu cereri
                endpoints.MapHub<ChatHub>("/Chat/Index");
            });
        }
    }
}
