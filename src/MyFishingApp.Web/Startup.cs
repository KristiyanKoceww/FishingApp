using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

using MyFishingApp.Data.Seeding;
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
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using MyFishingApp.Services.Data.NEWJWTSERVICE;
using System;

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

            //services.AddAntiforgery(options =>
            //{
            //    options.HeaderName = "X-CSRF-TOKEN";
            //});

            services.AddSingleton(this.configuration);

            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

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

            services.AddScoped<JWTAuthService>();
            services.AddScoped<SignInManager>();

            var jwtTokenConfig = configuration.GetSection("jwt").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret))
                };
            });

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
