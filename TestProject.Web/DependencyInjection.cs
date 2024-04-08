using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MongoDB.Driver;
using TestProject.Application.Handlers;
using TestProject.Domain.Entities;
using TestProject.Domain.Handlers;
using TestProject.Domain.Repositories;
using TestProject.Infrastructure.Context;
using TestProject.Infrastructure.Repositories;

namespace TestProject.Web
{
    public static class DependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddScoped<IUserHandler, UserHandler>();
            services.AddScoped<IMotorcycleHandler, MotorcycleHandler>();
            services.AddScoped<IBookingHandler, BookingHandler>();
            services.AddScoped<IOrderHandler, OrderHandler>();
            

        }
    }
}
