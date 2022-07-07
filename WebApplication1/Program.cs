using WebApplication1;
using Serilog;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseSerilog();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("myappsettings.json", optional: true,
                       reloadOnChange: true);

builder.Host.UseSerilog((ctx, lc) => 
lc.WriteTo
.Console()
.ReadFrom
.Configuration(ctx.Configuration));

//
//builder.Services.Configure<PositionOptions>(builder.Configuration.GetSection(PositionOptions.Position));
//builder.Services.AddConfig(builder.Configuration);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("myappsettings.json",
                       optional: true,
                       reloadOnChange: true);
});
//builder.Services.Configure<PositionOptions>(builder.Configuration.GetSection(PositionOptions.Position));

builder.Services.AddScoped<IMyDependency, MyDependency2>();

builder.Services.AddScoped<Service1>();
builder.Services.AddScoped<Service2>();

var myKey = builder.Configuration["MyKey"];
//builder.Services.AddScoped<IService3>(sp => new Service3(myKey));
//builder.Services.AddScoped<IService3, Service3>();
builder.Services.AddW3CLogging(logging =>
{
    // Log all W3C fields
    logging.LoggingFields = W3CLoggingFields.All;

    logging.FileSizeLimit = 5 * 1024 * 1024;
    logging.RetainedFileCountLimit = 2;
    logging.FileName = "MyLogFile";
    logging.LogDirectory = @"C:\logs";
    logging.FlushInterval = TimeSpan.FromSeconds(2);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}



app.UseAuthorization();

app.UseSerilogRequestLogging();
app.MapControllers();

app.Run();
