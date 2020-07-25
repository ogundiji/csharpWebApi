using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using System.Reflection;
using System.Web.Http;
using WebApiFundamental.Core.Repositories;
using WebApiFundamental.Persistence;

namespace WebApiFundamental.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var bldr = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            bldr.RegisterApiControllers(Assembly.GetExecutingAssembly());
            RegisterServices(bldr);
            bldr.RegisterWebApiFilterProvider(config);
            bldr.RegisterWebApiModelBinderProvider();
            var container = bldr.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterServices(ContainerBuilder bldr)
        {
            var config = new MapperConfiguration(cfg =>
              {
                  cfg.AddProfile(new CampMappingProfile());
              });
            //register instances of automapper
            bldr.RegisterInstance(config.CreateMapper())
                .As<IMapper>()
                .SingleInstance();


            bldr.RegisterType<CampContext>()
           .InstancePerRequest();

            bldr.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
           .InstancePerRequest();
        }

    }
}