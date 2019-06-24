using AutoMapper;
using ProjectManager.DataAccess;
using ProjectManager.DataAccess.Repository;
using ProjectManager.Infrastructure.Unity;
using ProjectManager.Logic;
using ProjectManager.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.Interception.Utilities;
using Unity.Lifetime;
using Unity.Registration;
using Unity.RegistrationByConvention;
using Unity.WebApi;
using System.Reflection;
using ProjectManager.BusinessObjects;
using ProjectManager.Infrastructure.Logging;

namespace ProjectManager.Api
{
    public static class UnityConfig
    {
        public static IUnityContainer Container
        {
            get { return DiContainerProvider.Container; }
        }
        public static void RegisterComponents()
        {
            //var container = new UnityContainer();

            //         // register all your components with the container here
            //         // it is NOT necessary to register your controllers

            //         // e.g. container.RegisterType<ITestService, TestService>();
            //         //RegisterAutoMapperProfiles(container);
            //         // RegisterMapper(container);
            //         //AutomapperConfiguration.Configure();
            //         container.RegisterType<IUnitOfWork, UnitOfWork>();
            //         container.RegisterType(typeof(IRepository<DataAccess.Entity.User>), typeof(UserRepository));
            //         container.RegisterType<IUserManager, UserManager>();
            //         GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            DiContainerProvider.BuildUnityContainer(GetAssembliesToScan(), ManualRegistrations, FactoryRegistrations);
            AutoMapperHelper.RegisterAutoMapperProfiles(Container, new List<Assembly>
            {
                typeof(UserManager).Assembly
            });
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(Container);

        }

        private static void FactoryRegistrations(IUnityContainer obj)
        {
            
        }

        private static void ManualRegistrations(IUnityContainer container)
        {
            container.RegisterType(typeof(IRepository<DataAccess.Entity.User>), typeof(UserRepository));
        }

        private static List<Assembly> GetAssembliesToScan()
        {
            return new List<Assembly>
            {
                typeof(UnitOfWork).Assembly,
                typeof(Project).Assembly,
                typeof(UserManager).Assembly,
                typeof(LogLevel).Assembly,
                Assembly.GetExecutingAssembly()
            };
        }

        private static void RegisterAutoMapperProfiles(IUnityContainer container)
        {
            IEnumerable<Type> autoMapperProfileTypes = AllClasses.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                           .Where(type => type != typeof(Profile) && typeof(Profile).IsAssignableFrom(type));

            autoMapperProfileTypes.ForEach(autoMapperProfileType =>
                container.RegisterType(typeof(Profile),
                autoMapperProfileType,
                autoMapperProfileType.FullName,
                new ContainerControlledLifetimeManager(),
                new InjectionMember[0]));
        }
        public static IUnityContainer RegisterMapper(this IUnityContainer container)
        {
            return container
            .RegisterType<MapperConfiguration>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(c =>
                    new MapperConfiguration(configuration =>
                    {
                        configuration.ConstructServicesUsing(t => container.Resolve(t));
                        foreach (var profile in c.ResolveAll<Profile>())
                            configuration.AddProfile(profile);
                    })))
            .RegisterType<IConfigurationProvider>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(c => c.Resolve<MapperConfiguration>()))
            .RegisterType<IMapper>(
                new InjectionFactory(c => c.Resolve<MapperConfiguration>().CreateMapper()));
        }
    }

    public static class AutomapperConfiguration
    {
        public static MapperConfiguration MyMapperConfiguration;
        public static void Configure()
        {
            MyMapperConfiguration = new MapperConfiguration(cfg =>
            {
                var types = new List<Type> { typeof(Logic.UserManager) };// AllClasses.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
                var profiles = types.Where(x => x.IsSubclassOf(typeof(Profile)))
                                    .Select(Activator.CreateInstance)
                                    .OfType<Profile>()
                                    .ToList();
                profiles.ForEach(p => cfg.AddProfile(p));
            });

            MyMapperConfiguration.AssertConfigurationIsValid();
        }
    }
}