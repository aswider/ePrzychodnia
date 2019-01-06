using ePrzychodnia.Core.Enums;
using ePrzychodnia.Data;
using ePrzychodnia.Domain.Implementations;
using ePrzychodnia.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ePrzychodnia.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ClinicDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            }
                ).AddEntityFrameworkStores<ClinicDbContext>().AddDefaultTokenProviders();

            services.AddAuthorization(options => options.AddPolicy("Admin", policy => policy.RequireRole("Admin")));
            services.AddAuthorization(options => options.AddPolicy("Standard", policy => policy.RequireRole("Standard")));

         
            services.AddScoped<IDoctorService,DoctorService>()
                .AddScoped<IPatientService,PatientService>()
                .AddScoped<IVisitService,VisitService>()
                .AddScoped<IUserService,UserService>();

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
          

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}