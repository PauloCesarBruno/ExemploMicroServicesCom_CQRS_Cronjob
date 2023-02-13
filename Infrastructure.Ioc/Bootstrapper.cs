using Application.App;
using Application.Commands.CommitDeltaCommand;
using Application.Events;
using Application.Interfaces;
using Application.Profiles;
using Application.Queries.InventoryQuery;
using Application.Queries.PartNumberQuery;
using Azul.Framework.Context;
using Azul.Framework.Events.Extensions;
using Azul.Framework.IoC;
using Domain.Repositories;
using Infrastructure.Data.Profiles;
using Infrastructure.Data.Repositories;
using Infrastructure.Publishers.Interfaces;
using Infrastructure.Publishers.Publishers;
using Infrastructure.Services.AzureTables.Config;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services.ServicesHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Reflection;

namespace Infrastructure.Ioc
{
    public sealed class Bootstrapper : BaseDependencyInjectionBootstrap
    {

        public Bootstrapper(IServiceCollection services, Container container)
        {
            container.Options.DefaultLifestyle = Lifestyle.Singleton;
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            services.AddLogging(configure => configure.AddConsole());
            services.AddSimpleInjector(container);

            Inject(container);

            container.Register<IHttpContextAccessor, HttpContextAccessor>();
            container.RegisterSingleton<IContextFactory>(() => new ContextFactory(container));
            container.Register<IContext, ScopeContext>();

            var mapperAssemblies = new Assembly[]
            {       
                 typeof(PartNumberProfile).Assembly,
                 typeof(PartNumberQuantityProfile).Assembly,
                 typeof(PartNumberQuantityEntityProfile).Assembly,
                 typeof(PartNumberEntityProfile).Assembly
            };

            var mediatorAssemblies = new Assembly[]
            {
                 typeof(PartNumberQuery).Assembly,
                 typeof(PartNumberQuantity).Assembly,
                 typeof(CommitDeltaCommand).Assembly
            };

            InjectAutoMapper(container, mapperAssemblies);
            InjectMediator(container, mediatorAssemblies);

            InjectPublishers(container);
            InjectApplication(container);
            InjectRepositories(container);
            InjectServices(container);
            InjectClients(services);

            services.BuildServiceProvider(true).UseSimpleInjector(container);

            var loggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(typeof(Bootstrapper));

            container.StartPublishersAndSubscribers();
        }

        private void InjectApplication(Container container)
        {
            container.Register<IPartNumberApp, PartNumberApp>();
            container.Register<IPartNumberQuantityApp, PartNumberQuantityApp>();
        }
        private static void InjectPublishers(Container container)
        {
            container.RegisterPublishers(typeof(PartNumberPublisher).Assembly);
            container.Register<IPartNumberPublisher, PartNumberPublisher>();
            container.Register<IPartNumberQuantityPublisher, PartNumberQuantityPublisher>();

        }
        private void InjectRepositories(Container container)
        {
            container.Register<IPartNumberRepository, FakePartNumberRepository>();
            container.Register<IPartNumberQuantityRepository, FakePartNumberQuantityRepository>();
            //container.Register<IPartNumberRepository, PartNumberRepository>();
            //container.Register<IPartNumberQuantityRepository, PartNumberQuantityRepository>();
        }

        private void InjectServices(Container container)
        {
            container.Register<IAzureTablesService, AzureTablesService>();
        }

        private void InjectClients(IServiceCollection services)
        {
            services.RegisterTableClient(nameof(AzureTablesService));
        }
    }
}
