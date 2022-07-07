using MES.ConfigService.Filters;
using MES.ConfigService.Models;
using Microsoft.OpenApi.Models;
using SqlSugar;
using SqlSugar.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(option => {
    option.Filters.Add(typeof(SampleActionFilter));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

var key = builder.Configuration["ConnectionStrings"];

builder.Services.AddSqlSugar(new IocConfig()
{
    ConnectionString = builder.Configuration["ConnectionStrings"],
    DbType = IocDbType.MySql,
    IsAutoCloseConnection = true
});
Console.WriteLine("program init sql sugar");

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAreaRepository, AreaRepository>();

//builder.Services.AddScoped<SampleActionFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
