using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RealEstateAPI.Configurations;
using RealEstateAPI.Data;
using RealEstateAPI.IRepository;
using RealEstateAPI.Repository;
using RealEstateAPI.Services;

namespace RealEstateAPI
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

            //cors, when someone thats not in your network tries accesing api,
            //by default your api will reject that request. so we want to configure this policy
            //for AddCors we need to add policy so it knows how to behave
            //so bellow with adding policys we can determine who is allowed to access our recources
            //if everybody on internet will use API you cant be to strict. In our case we allow everybody
            //adding databaseContext
            services.AddDbContext<DatabaseContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));



            services.AddAuthentication();
            //calling method from ServiceExtensions to configure Identity
            //Configuration for JWT from ServiceExtensions. It requers to pass Configuration
            services.ConfigureJWT(Configuration);

            //adding AddMemoryCache to keep track who requested, what requested and ..
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            //adding MapperInitilizer as mapper to automapper
            services.AddAutoMapper(typeof(MapperInitilizer));
            //adding new serivice. IAuthManager mapped to AuthManager. AuthManager has methods implementation.
            services.AddScoped<IAuthManager, AuthManager>();

            //addTranscient means when someone hits my controller it'll always provide fresh copy of IUnitOfWork
            //adds transcient service of type specified in IUnitOfWork with implementation type specified in UnitOfWork
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RealEstateAPI", Version = "v1" });
            });
            services.AddControllers().AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RealEstateAPI v1"));

            app.UseHttpsRedirection();

            //letting app know that it should use cors policy. We put policy name
            //that we created for it to use
            app.UseCors("AllowAll");

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
