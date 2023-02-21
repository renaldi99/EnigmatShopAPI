using EnigmatShopAPI.Repositories;
using EnigmatShopAPI.Services;
using EnigmatShopAPI.Services.Impl;

namespace EnigmatShopAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IPersistence, Persistence>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<ICustomerRepository, CustomerRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IPurchaseDetailService, PurchaseDetailService>();

        }
    }
}
