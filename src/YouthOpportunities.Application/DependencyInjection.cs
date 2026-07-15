using Microsoft.Extensions.DependencyInjection;
using YouthOpportunities.Application.Categories.Services;

namespace YouthOpportunities.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}
