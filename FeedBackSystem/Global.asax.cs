
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FeedBackSystem.DataAccessLayer.MapperPro;
using FeedBackSystem.DataAccessLayer.DboModels;
using FeedBackSystem.Model.DtoModels;

namespace FeedBackSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
         
            AutoMapper.Mapper.Initialize(cfg=>cfg.AddProfile<AutoMapperProfile>());
           // AutoMapperConfig.Initialize();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            UnityConfig.RegisterComponents();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
     
        }
    }
   

}
