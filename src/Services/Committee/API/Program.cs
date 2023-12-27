using Committees.Application.Services;

var builder = WebApplication.CreateBuilder(args);

try { 
// Add services to the container.

// Add unit of work & GRepo
builder.Services.AddUnitOfWorkRepository();
builder.Services.AddCorsConfig();
builder.Services.AddDiServices();
	builder.Services.AddGrpc();
	builder.Services.AddGrpcReflection();

	builder.WebHost.ConfigureKestrel(options =>
	{
		options.Listen(IPAddress.Any,7168,o => o.Protocols = HttpProtocols.Http2);

		options.Listen(IPAddress.Any,5222,o => o.Protocols = HttpProtocols.Http1);
	});

	builder.Services.AddVersioning();
    builder.Services.AddDbConfig(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddAuthentication("Bearer")
	  .AddJwtBearer("Bearer",options =>
	  {
		  // the name of your api resources   
		  options.Audience = "UserManagementServer";
		  /// identity server url                    
		  options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
		  options.RequireHttpsMetadata = false;
	  });

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("ResourceOwnerPolicy",policy =>
	{
		policy.AddAuthenticationSchemes("Bearer");
		policy.RequireClaim("scope","4EMicroServices"); // Require scope associated with Resource Owner Password token
	});

	options.AddPolicy("ClientCredentialsPolicy",policy =>
	{
		policy.AddAuthenticationSchemes("Bearer");
		policy.RequireClaim("scope","CPAggregatorAPI"); // Require scope associated with Client Credentials token
	});
});

// Add grpc config to serviceCollection
builder.Services.AddGrpc();
//builder.Services.AddGrpcReflection();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1",new OpenApiInfo
	{
		Version = "v1",
		Title = "Committee APIs Reference",
	});

	c.AddSecurityDefinition(
	"token",
	new OpenApiSecurityScheme
	{
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer",
		In = ParameterLocation.Header,
		Name = HeaderNames.Authorization
	}
		);
	c.AddSecurityRequirement(
	new OpenApiSecurityRequirement
	{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "token"
							},
						},
						Array.Empty<string>()
					}
	}
		);
});
#endregion



builder.Services.AddControllers();
builder.Services.AddFluentValidation();

var app = builder.Build();
app.MapGrpcService<CommitteeApprovalService>();
app.UseMiddleware<GlobalExceptionHandler>();
// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")),
	RequestPath = "/wwwroot"
});

app.ApplyAutoMigration();

// Add GrpcServices
app.AddMapGrpcServices();
app.UseStaticFiles();

//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();

app.Run();


}
catch (Exception)
{

    throw;
}