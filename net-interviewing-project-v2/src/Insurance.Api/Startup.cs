using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Insurance.Api.Repositories;
using Insurance.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Hellang.Middleware.ProblemDetails;
using Insurance.Api.Exceptions;
using Insurance.Api.Services;

namespace Insurance.Api
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
            //services.AddHttpContextAccessor();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IInsuranceService, InsuranceService>();

            //register services
            //services.AddScoped<ICartService, CartService>();

            /*services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });*/

            //services.AddProblemDetails();

            services.AddProblemDetails(setup =>
                {
                    //setup.IncludeExceptionDetails = _ => !Environment.IsDevelopment();
                    setup.Map<NotFoundException>(exception => new ProblemDetails
                    {
                        Title = exception.Message,
                        Detail = exception.Message,
                        //Balance = exception.Balance,
                        Status = StatusCodes.Status404NotFound,
                        Type = "Not Found"
                    });
                });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Type = $"https://httpstatuses.com/400",
                        Detail = "ModelStateValidation failed"
                    };
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes =
                            {
                                "application/problem+json",
                                "application/problem+xml"
                            }   
                    };
                };
            });

            services.AddControllers();

            //register swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Insurance.API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Insurance.API v1"));
            }

            app.UseProblemDetails();

            app.UseRouting();

            app.UseAuthentication();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
