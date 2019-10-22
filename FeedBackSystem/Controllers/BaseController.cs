using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FeedBackSystem.Domain.RepositoryInterface;
using FeedBackSystem.Model.DtoModels;
using FeedBackSystem.BussinessLogicLayer.Service;
using FeedBackSystem.Domain.ServiceInterface;

namespace FeedBackSystem.Controllers
{
    [Authorize(Roles = "superadmin")]
    public class BaseController : Controller
    { 

      
        readonly IFeedBackBussiness  service;
        readonly IAddTitleAndQuestions  repository;

        //inject dependency
        public BaseController(IFeedBackBussiness service, IAddTitleAndQuestions repository)
        {
            this.repository = repository;
            this.service = service;
      
        }
        public void Index()
        {
            try
            {
                int x, y, z;
                x = 5; y = 0;
                z = x / y;
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var log = new CreateLog();
                log.GlobalPropertiesOfContAndMethodName(ex, actionName, controllerName);
            }

        }
        
        public ActionResult MasterPage()
        {
            return View();
        }

        public JsonResult SaveCompany(string txt1)
        {
            return Json(service.SaveCompanyFromBase(txt1,  repository),  JsonRequestBehavior.AllowGet);
        }

        public ActionResult AssignRolesToUsers()
        {
            return View();
        }

        public JsonResult GetAllUsers1()
        {
            return Json(service.GetAllUsers1FromBase(repository), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllRoles()
        {
            return Json(service.GetAllRolesFromBase(repository), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveSelectedUsers1(List<AssignedTitles> list1)
        {
            return Json(service.SaveSelectedUsers1FromBase(list1, repository), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AssignUsersToCompany()
        {
            return View();
        }

        public async Task<JsonResult> GetAllUsers2()
        {
            return Json(await service.GetAllUsers2FromBase(repository), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllAdmin()
        {
            return Json(service.GetAllAdminFromBase(repository), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllCompany()
        {
            return Json(service.GetAllCompanyFromBase(repository), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AssignCompany(List<AssignedTitles> list)
        {
            return Json(service.AssignCompanyFromBase(list, repository), JsonRequestBehavior.AllowGet);
        }

    }
}