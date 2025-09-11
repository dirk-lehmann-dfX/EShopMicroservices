var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("PostGresDatabase")!);
    //options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
    //options.DatabaseSchemaName = "catalog"; 
    //options.RegisterDocumentType<Domain.Entities.CatalogItem>();
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
