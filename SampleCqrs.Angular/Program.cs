using Mapster;
using SampleCqrs.Angular.Middlewares;
using static SampleCqrs.Application.ServiceConfigurator;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Func<string, string> toLower = s => s.ToLower();
TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(
    new NameMatchingStrategy
    {
        SourceMemberNameConverter = toLower,
        DestinationMemberNameConverter = toLower,
    });


//builder.Services.ConfigurePersistanceServices(builder.Configuration);

builder.Services.ConfigureApplicationServices(builder.Configuration);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
string CORS_NAME = "CORS";

builder.Services.AddCors(options =>
{
    var origins = new List<string>();
    builder.Configuration.GetSection("Origins").Bind(origins);
    options.AddPolicy(
        CORS_NAME,
        builder =>
        {
            builder.WithOrigins(origins.ToArray()).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});

var presentationAssembly = typeof(SampleCqrs.Presentation.AssemblyReference).Assembly;
builder.Services.AddControllers()
    .AddApplicationPart(presentationAssembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(CORS_NAME);

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
