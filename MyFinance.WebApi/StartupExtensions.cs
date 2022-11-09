using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyFinance.Domain.Models.Identity;
using MyFinance.Persistence;
using MyFinance.WebApi.Models;
using MyFinance.WebApi.Services;

namespace MyFinance.WebApi;

public static class StartupExtensions
{
    private static List<Assembly> _assemblies;

    private static Assembly[] RelatedAssemblies
    {
        get
        {
            if (_assemblies != null)
            {
                return _assemblies.ToArray();
            }

            var current = Assembly.GetEntryAssembly();
            if (current == null)
            {
                throw new ApplicationException("Не найдена сборка");
            }

            _assemblies = new List<Assembly> { current };

            _assemblies.AddRange(current
                .GetReferencedAssemblies()
                .Where(x => x.Name != null && x.Name.StartsWith("MyFinance."))
                .Select(a => Assembly.Load(a.FullName)));
            return _assemblies.ToArray();
        }
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:SecretKey"]));
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.FromSeconds(10)
                };
            });
        return services;
    }
    
    public static void AddAutofacContainer(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        hostBuilder.ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterAssemblyModules(RelatedAssemblies);
        });
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<AppUser, AppRole>()
            .AddDefaultTokenProviders();

        services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));
        services.AddTransient<IUserStore<AppUser>, AppUserStore>();
        services.AddTransient<IRoleStore<AppRole>, AppRoleStore>();

        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(x => x.AddMaps(RelatedAssemblies));
        return services;
    }

    public static void AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.WithOrigins(configuration.GetSection("ClientUrl").Value)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });
    }
}