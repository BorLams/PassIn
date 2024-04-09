using Microsoft.Extensions.DependencyInjection;
using PassIn.Application.Services;
using PassIn.Application.Services.Interfaces;
using PassIn.Infrastructure.Contexts;
using PassIn.Infrastructure.Repositories;
using PassIn.Infrastructure.Repositories.Interfaces;

namespace PassIn.Application.Configurations;

public static class DIConfiguration
{
    public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
    {
        #region Contexts
        _ = services.AddScoped<PassInDbContext>();
        #endregion

        #region Services
        _ = services.AddScoped<IEventsService, EventsService>();
        _ = services.AddScoped<IAttendeesService, AttendeesService>();
        #endregion

        #region Repositories
        _ = services.AddScoped<IEventsRepository, EventsRepository>();
        _ = services.AddScoped<IAttendeeRepository, AttendeeRepository>();
        #endregion

        return services;
    }
}
