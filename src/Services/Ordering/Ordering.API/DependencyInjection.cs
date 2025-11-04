namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Add API services registrations here
        //services.AddCarter();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        //app.MapCarter();

        return app;
    }
}
