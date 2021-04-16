using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OData.Swagger.Services;
using System;
using System.Linq;
using System.Text;
using TodoItem2.Model;
using TodoItem2.Model.Repositories;
using TodoItem2.Services;
using TodoItem2.Services.Items;
using ToDoItem2.BL.Dtos;
using ToDoItem2.BL.Validators;
using ToDoItem2.Models;

namespace ToDoItem2
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
            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddOData();
            services.AddDbContext<ItemContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IUserService, UserService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMvc().AddFluentValidation();
            services.AddTransient<IValidator<ItemDto>, ItemValidator>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });
            services.AddOdataSwaggerSupport();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidAudience = Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                            ClockSkew = TimeSpan.Zero
                        };
                    });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(options => options.AllowAnyOrigin());
            app.UseCors(options => options.WithOrigins("https://localhost:44331"));

            var builder = new ODataConventionModelBuilder(app.ApplicationServices);
            builder.EntitySet<ItemDto>("Items");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.EnableDependencyInjection();
                endpoints.Expand().Select().Filter().OrderBy().MaxTop(100).Count();
                endpoints.MapODataRoute("odata", "api", builder.GetEdmModel());
            });
        }
    }
}
