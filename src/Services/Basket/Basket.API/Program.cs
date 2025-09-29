var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCarter();

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(
    config =>
    {
        config.RegisterServicesFromAssembly(assembly);
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("PostGresDatabase")!);
    options.Schema.For<ShoppingCart>().Identity(shoppingCart => shoppingCart.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
// Alternative zu Decorate:
//builder.Services.AddScoped<IBasketRepository>(provider =>
//{
//    var basketRepository = provider.GetRequiredService<BasketRepository>();
//    return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
//});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");

});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(optins => { });


app.Run();
