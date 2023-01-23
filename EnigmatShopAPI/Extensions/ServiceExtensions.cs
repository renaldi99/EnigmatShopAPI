﻿using EnigmatShopAPI.Repositories;
using EnigmatShopAPI.Services;
using EnigmatShopAPI.Services.Impl;

namespace EnigmatShopAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IPersistence, Persistence>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}