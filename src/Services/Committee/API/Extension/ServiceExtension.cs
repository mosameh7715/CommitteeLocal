namespace Committees.API.Extension
{
    public static class ServiceExtension
    {
        public static void AddUnitOfWorkRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddScoped(typeof(IGRepository<>), typeof(GRepository<>));
        }
        public static void AddDiServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(ApplicationLayer)));
            services.AddAutoMapper((typeof(ApplicationLayer)));
            services.AddHttpContextAccessor();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IResponseHelper, ResponseHelper>();
            services.AddScoped<GlobalExceptionHandler, GlobalExceptionHandler>();
        }
        public static void AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCors",
                    builder =>
                    {
                        builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials();
                    });
            });
        }

        public static void AddDbConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBContext>(options =>
         {
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

             options.EnableSensitiveDataLogging(true);
             options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
         });
        }

        public static void ApplyAutoMigration(this WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DBContext>();
                var serviceProvider = serviceScope.ServiceProvider;
                if (!serviceScope.ServiceProvider.GetService<DBContext>().AllMigrationsApplied())
                {
                    serviceScope.ServiceProvider.GetService<DBContext>().Migrate();
                }

            }
        }
        public static void AddMapGrpcServices(this WebApplication app)
        {
        }
        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning().AddApiExplorer(options =>
            {
                // Add the versioned API explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
            });
        }
    }
}
