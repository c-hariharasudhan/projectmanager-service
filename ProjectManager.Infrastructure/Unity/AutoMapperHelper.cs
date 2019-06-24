using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace ProjectManager.Infrastructure.Unity
{
    public static class AutoMapperHelper
    {
        public static void RegisterAutoMapperProfiles(IUnityContainer container, List<Assembly> assembliesToScan)
        {
            if (assembliesToScan == null) return;
            var profileType = typeof(Profile);
            var profilesWithIMapperInjection = assembliesToScan.SelectMany(a => a.GetTypes())
                   .Where(t => profileType.IsAssignableFrom(t)
                    && t.GetConstructor(new[] { typeof(IMapper) }) != null).ToList();
            if(profilesWithIMapperInjection.Any())
            {
                throw new Exception("IMapper can't be injected into mapper profiles with Automapper. try using Func instead. eg: Func<IMapper> mapper");
            }
            var profileTypesNoConstructor = assembliesToScan.SelectMany(a => a.GetTypes())
                .Where(t => profileType.IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) != null).ToList();
            var profileTypes = assembliesToScan.SelectMany(a => a.GetTypes())
                .Where(t => profileType.IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) == null).ToList();

            profileTypesNoConstructor.ForEach(p =>
            {
                container.RegisterType(profileType, p, p.Name, new ContainerControlledLifetimeManager());
            });
            profileTypes.ForEach(p =>
            {
                container.RegisterType(profileType, p, p.Name, new ContainerControlledLifetimeManager());
            });
            profileTypes.AddRange(profileTypesNoConstructor);

            container.RegisterType<MapperConfiguration>(new ContainerControlledLifetimeManager(),
                new InjectionFactory(c => new MapperConfiguration(cfg =>
                {
                    foreach (var profile in profileTypes)
                    {
                        cfg.AddProfile((Profile)container.Resolve(profile, profile.Name));
                    }
                }))).RegisterType<IMapper>(new ContainerControlledLifetimeManager(),
                new InjectionFactory(c => c.Resolve<MapperConfiguration>().CreateMapper()));
        }
    }
}
