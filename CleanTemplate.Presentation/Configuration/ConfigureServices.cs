﻿using Asp.Versioning;
using AutoMapper;
using CleanTemplate.Core.Dtos.Profiles;
using CleanTemplate.Core.Interfaces.Infrastructure;
using CleanTemplate.Core.Interfaces.Services;
using CleanTemplate.Core.Services;
using CleanTemplate.Infrastructure.Context;
using CleanTemplate.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanTemplate.Presentation
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Database")));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBooksRepository, BooksRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBooksService, BooksService>();

            return services;
        }

        public static IServiceCollection AddDI(this IServiceCollection services)
        {
            AddRepositories(services);
            AddServices(services);

            return services;
        }

        public static void AddCustomProblemDetails(this IServiceCollection services)
        {
            services.AddProblemDetails(options => 
                options.CustomizeProblemDetails = ctx =>
                {
                    ctx.ProblemDetails.Extensions.Add("trace-id", ctx.HttpContext.TraceIdentifier);
                    ctx.ProblemDetails.Extensions.Add("instance", $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
                });
            services.AddExceptionHandler<ProblemDetailsExceptionHandler>();
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BookDtoProfile>();
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new QueryStringApiVersionReader("v"));
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V.v";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}