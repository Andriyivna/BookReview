using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using BookReview.Core.Repositories;
using BookReview.Infrastructure.Repositories;
using BookReview.Infrastructure.Services;
using BookReview.DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReview.Infrastructure.Mappers;

namespace BookReview
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IBooksServices, BooksServices>();
            services.AddSingleton(AutoMapperConfig.Initialize());

            services.AddDbContext<MyBase>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("MyBase")));
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITokenService, TokenService>();

            services.AddIdentityServices(Configuration);

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            //});
        }
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

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
