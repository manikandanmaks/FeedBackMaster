using FeedBackSystem.BussinessLogicLayer.Service;
using FeedBackSystem.Controllers;
using FeedBackSystem.DataAccessLayer;

using FeedBackSystem.Domain.RepositoryInterface;
using FeedBackSystem.Domain.ServiceInterface;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace FeedBackSystem
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAddTitleAndQuestions, AddTitleAndQuestions>();
          // container.RegisterType<IMapperProfile, MapperProfile>();
            container.RegisterType<IFeedBackBussiness, FeedBackBussiness>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}