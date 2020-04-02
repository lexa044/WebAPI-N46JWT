using System.Web.Http;
using System.Configuration;

using Unity;
using Unity.Injection;
using Unity.Lifetime;

using DNFKit.Core;
using DNFKit.Data;
using DNFKit.Core.Repositories;
using DNFKit.Data.Repositories;
using DNFKit.Core.Services;
using DNFKit.Services;

namespace DNKit.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            //container.RegisterType<IDalSession>(
            //    new InjectionFactory(u =>
            //        new DalSession(ConfigurationManager.ConnectionStrings["ReadConnection"].ConnectionString,
            //        ConfigurationManager.ConnectionStrings["WriteConnection"].ConnectionString)
            //    )
            //);
            container.RegisterType<IDalSession>(
                new HierarchicalLifetimeManager(),
                new InjectionFactory(c =>
                            new DalSession(ConfigurationManager.ConnectionStrings["ReadConnection"].ConnectionString,
                            ConfigurationManager.ConnectionStrings["WriteConnection"].ConnectionString)
                    ));
            //container.RegisterType<IDalSession>(()=> new DalSession(), new ContainerControlledLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ICustomerService, CustomerService>();

            //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}