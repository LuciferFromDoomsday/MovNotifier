using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovNotifier.Models;
using MovNotifier.Services;

namespace MovNotifier
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
            services.AddRazorPages();
            services.AddScoped<GenreService, GenreServiceImpl>();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            })
                .AddCookie(options =>
                {

                    options.LoginPath = "/account/google-login";

                })
                .AddGoogle(options =>
                {
                    options.ClientId = "687275022051-v683q9krra9edfdhjiuddsaj5gk3jl39.apps.googleusercontent.com";
                    options.ClientSecret = "4GCPcxBpQslKNn12pnGIlJd-";
                });


            //services.AddIdentity<User, IdentityRole>()
            //    .AddEntityFrameworkStores<UserDbContext>();

           
            

      //      services.AddAuthentication()
      //.AddGoogle(options =>
      //{
      //    IConfigurationSection googleAuthNSection =
      //        Configuration.GetSection("Authentication:Google");

      //    options.ClientId = googleAuthNSection["ClientId"];
      //    options.ClientSecret = googleAuthNSection["ClientSecret"];
      //});
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Registration}/{id?}");
            });
        }
    }
}
