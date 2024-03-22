using Infrastructure.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sharedFolder = Path.Combine(builder.Environment.ContentRootPath, "..", "SolutionItems");

builder.Configuration.AddJsonFile(Path.Combine(sharedFolder, "sharedsettings.json"), optional: true)
        .AddJsonFile("sharedsettings.json", optional: true);

DependencyInjection.RegisterServices(builder.Services, builder.Configuration);



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
