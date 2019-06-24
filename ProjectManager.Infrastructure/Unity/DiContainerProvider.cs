using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using Unity.RegistrationByConvention;

namespace ProjectManager.Infrastructure.Unity
{
    public static class DiContainerProvider
    {
        public static IUnityContainer Container { get; set; }

        public static void BuildUnityContainer(object getAssembliesToScan, Action<IUnityContainer> manualRegistrations, Action<IUnityContainer> factoryRegistrations)
        {
            throw new NotImplementedException();
        }

        public static void BuildUnityContainer(object getAssembliesToScan, object manualRegistrations, object factoryRegistrations)
        {
            throw new NotImplementedException();
        }

        static DiContainerProvider()
        {
            Container = new UnityContainer();
        }

        public static void BuildUnityContainer(List<Assembly> assembliesToScan, Action<IUnityContainer> manualRegistrations,
            Action<IUnityContainer> factoryRegistrations, Type lifeTimeManagerType = null)
        {
            manualRegistrations(Container);
            factoryRegistrations(Container);
            ConventionRegistrations(assembliesToScan, lifeTimeManagerType ?? typeof(TransientLifetimeManager));
        }

        private static void ConventionRegistrations(List<Assembly> assembliesToScan, Type lifeTimeManagerType)
        {
            Container.RegisterTypes(GetTypesToLoad(assembliesToScan),
                WithMappings.FromMatchingInterface,
                WithName.Default, t => GetLifeTimeManager(lifeTimeManagerType));
        }

        private static LifetimeManager GetLifeTimeManager(Type type)
        {
            return (LifetimeManager)Activator.CreateInstance(type);
        }

        private static IEnumerable<Type> GetTypesToLoad(List<Assembly> assembliesToScan)
        {
            try
            {
                var mappedTypes = Container.Registrations.Select(r => r.RegisteredType.Name.Remove(0, 1));
                var typesToLoad = new List<Type>();
                assembliesToScan.ForEach(a => typesToLoad.AddRange(a.GetTypes()));
                return typesToLoad.Where(t => !mappedTypes.Any(m => t.Name.Contains(m)));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
