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
            container.RegisterType(typeof(IRepository<DataAccess.Entity.Project>), typeof(ProjectRepository));
            container.RegisterType(typeof(IRepository<DataAccess.Entity.Task>), typeof(TaskRepository));
            container.RegisterType(typeof(IRepository<DataAccess.Entity.ParentTask>), typeof(ParentTaskRepository));
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

        
    }
        
}