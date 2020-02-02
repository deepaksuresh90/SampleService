using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using profile_app.Data;

namespace profile_app
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
            services.AddControllers();
            services.AddDbContext<ProfileDbContext>(options=>options.UseSqlServer(@"Data Source=DESKTOP-OA1F9BH;Initial Catalog=ProfileDb; User Id=sa;Password=1234"));
            
            /*Media Type formatters*/
            /*This line added for content negotiation to xml*/
            /*Nuget package : Microsoft.AspnetCore.MVC.Formatters.xml*/
            services.AddMvc().AddXmlSerializerFormatters();


            /*Response Caching used to store the response in proxy server for sometime-(timeout)
             * Once timeout it will clear the cache and send the request to actual server
             * Nuget package -Aspnet.core.ResponseCache
             * */
            services.AddResponseCaching();

            /*Adding authentication and autherisation using auth0 portal*/
            // 1. Add Authentication Services
            /*Nuget for auth : Microsoft.AspnetCore.Authentication.JwtBearer*/
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://profileapi.au.auth0.com/";
                options.Audience = "https://localhost:5001/";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProfileDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            /*Code First Migration*/
            /*The below line will ensure Database and tables are created from Model class*/
            /*Later if modifying this by adding a new property in the model class will cause error*/
            /*This will be applicable if the model/Table is fixed,Later there is no alteration to that*/
            db.Database.EnsureCreated();

            /*This will be applicable if the model/Table is not fixed,Later there is modification allowed to that*/
            /*Also In packaage manager console tools->pkg manager console*/
            /*Add-Migration UserFieldAdded*/
            /*update-database*/
            //db.Database.Migrate();


            /*response caching in proxy server*/
            app.UseResponseCaching();

            // 2. Enable authentication middleware
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
