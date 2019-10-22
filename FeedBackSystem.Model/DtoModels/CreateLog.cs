using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using FeedBackSystem;

namespace FeedBackSystem.Model.DtoModels

{
    public class CreateLog
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void GlobalPropertiesOfContAndMethodName(Exception ex,string cont,string met)
        {
           
            log4net.GlobalContext.Properties["ControllerName"] =cont;
            log4net.GlobalContext.Properties["MethodName"] =met;
            XmlConfigurator.Configure();
            log.Error(ex.ToString());
            
        }

    }
}