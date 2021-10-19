using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyFishingApp.Data;
using MyFishingApp.Data.Common;
using MyFishingApp.Data.Common.Repositories;
using MyFishingApp.Data.Models;
using MyFishingApp.Data.Repositories;
using MyFishingApp.Services.Data.Dam;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Reflection;

using MyFishingApp.Data.Seeding;


using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MyFishingApp.Services.Data;
using MyFishingApp.Services.Data.Cities;
using AirportSystem.Services.Data.CitiesAndCountries;
using MyFishingApp.Services.Data.Comments;
using MyFishingApp.Services.Data.Countries;
using MyFishingApp.Services.Data.FishServ;
using MyFishingApp.Services.Data.Knots;
using MyFishingApp.Services.Data.Posts;
using MyFishingApp.Services.Data.Votes;
using MyFishingApp.Services.Data.Weather;
using MyFishingApp.Services.Data.AppUsers;
using CloudinaryDotNet;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MyFishingApp.Services.Data.Jwt;

namespace MyFishingApp.Web
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();


            //services.Configure<CookiePolicyOptions>(
            //    options =>
            //    {
            //        options.CheckConsentNeeded = context => true;
            //        options.MinimumSameSitePolicy = SameSiteMode.None;
            //    });


            //services.AddAntiforgery(options =>
            //{
            //    options.HeaderName = "X-CSRF-TOKEN";
            //});

            services.AddSingleton(this.configuration);



            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            services.AddTransient<IJwtService, JwtService>();

            services.AddTransient<IReservoirService, ReservoirService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IFishService, FishService>();
            services.AddTransient<IKnotService, KnotService>();
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<IVotesService, VotesService>();
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<IAppUser, AppUser>();
            services.AddTransient<IVotesService, VotesService>();
            services.AddTransient<IPostsService, PostsService>();

            // configure strongly typed settings objects
            //var appSettingsSection = configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingsSection);

            //// configure jwt authentication
            //var appSettings = appSettingsSection.Get<AppSettings>();
            //var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x =>
            //{
            //    x.Events = new JwtBearerEvents
            //    {
            //        OnTokenValidated = context =>
            //        {
            //            var userService = context.HttpContext.RequestServices.GetRequiredService<IAppUser>();
            //            var userId = context.Principal.Identity.Name;
            //            var user = userService.GetById(userId);
            //            if (user == null)
            //            {
            //                // return unauthorized if user no longer exists
            //                context.Fail("Unauthorized");
            //            }
            //            return Task.CompletedTask;
            //        }
            //    };
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});

            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            //});

            services.AddCors();


            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                = new DefaultContractResolver());

            services.AddControllers();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyFishingApp.Web", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CloudinaryConfiguration.CloudName = this.configuration.GetSection("Cloudinary")["CloundName"];
            CloudinaryConfiguration.ApiKey = this.configuration.GetSection("Cloudinary")["CloudApiKey"];
            CloudinaryConfiguration.ApiSecret = this.configuration.GetSection("Cloudinary")["CloudApiSecret"];

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyFishingApp.Web v1"));
            }

            app.UseCors(options => options.WithOrigins(new[] { 
            "http://localhost:3000","http://localhost:8080","http://localhost:4200"}).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

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
