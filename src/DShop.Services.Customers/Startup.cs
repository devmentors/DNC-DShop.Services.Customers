using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DShop.Common.Mongo;
using DShop.Common.Mvc;
using DShop.Common.RabbitMq;
using DShop.Common.RestEase;
using DShop.Messages.Commands.Customers;
using DShop.Messages.Events.Identity;
using DShop.Messages.Events.Orders;
using DShop.Messages.Events.Products;
using DShop.Services.Customers.Domain;
using DShop.Services.Customers.ServiceForwarders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DShop.Services.Customers
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddDefaultJsonOptions();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                    .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddRabbitMq();
            builder.AddMongoDB();
            builder.AddMongoDBRepository<Customer>("Customers");
            builder.RegisterServiceForwarder<IProductsApi>("products-service");
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
            app.UseErrorHandler();
            app.UseRabbitMq()
                .SubscribeCommand<CreateCustomer>()
                .SubscribeCommand<AddProductToCart>()
                .SubscribeCommand<DeleteProductFromCart>()
                .SubscribeEvent<OrderCompleted>()
                .SubscribeEvent<ProductCreated>()
                .SubscribeEvent<ProductUpdated>()
                .SubscribeEvent<ProductDeleted>()
                .SubscribeEvent<SignedUp>();
            applicationLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
