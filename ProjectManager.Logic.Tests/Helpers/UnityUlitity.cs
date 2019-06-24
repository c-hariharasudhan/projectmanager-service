using AutoMapper;
using ProjectManager.DataAccess.Entity;
using ProjectManager.Infrastructure.Unity;
using ProjectManager.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Unity;

namespace ProjectManager.Logic.Tests.Helpers
{
    internal static class UnityUlitity
    {

        public static IMapper SetupAutoMapper()
        {
            DiContainerProvider.BuildUnityContainer(GetAssembliesToScan(), ManualRegistrations, FactoryRegistrations);
            AutoMapperHelper.RegisterAutoMapperProfiles(DiContainerProvider.Container, GetAssembliesToScan());
            return DiContainerProvider.Container.Resolve<IMapper>();
        }

        private static List<Assembly> GetAssembliesToScan()
        {
            return new List<Assembly>
            {
                typeof(Project).Assembly,
                typeof(UserManager).Assembly
            };
        }

        private static void FactoryRegistrations(IUnityContainer obj)
        {
        }

        private static void ManualRegistrations(IUnityContainer obj)
        {
         
        }
    }
}
