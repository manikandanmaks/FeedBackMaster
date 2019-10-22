using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using FeedBackSystem.Excel;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using FeedBackSystem.Model.DtoModels;
using FeedBackSystem.Domain.RepositoryInterface;
using FeedBackSystem.Domain.ServiceInterface;
using System.Web.Script.Serialization;

namespace FeedBackSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // IAddTitleAndQuestions obj = new Repository.Service.AddTitleAndQuestions();



        private readonly IFeedBackBussiness service;
        readonly IAddTitleAndQuestions repository;

        //inject dependency
        public HomeController( IFeedBackBussiness service, IAddTitleAndQuestions repository)
        {
          
            this.repository = repository;
            this.service = service;
        }

        [Authorize(Roles = "admin")]
        public ActionResult RegisterMultipleUsers()
        {
            return View(new List<RegisterMutlipleUsers>());
        }

        public JsonResult Import(HttpPostedFileBase excelFile)
        {          
          
            return Json(service.ImportFromHome(excelFile, repository), JsonRequestBehavior.AllowGet);
        }

        

        [Authorize(Roles = "admin")]
        public  ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "user")]
        public ActionResult FeedBackForm()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddTitles()
        {
            List<TitleAndQuestions> list = new List<TitleAndQuestions>();
            return View(list);
        }

        public async Task<JsonResult> GetTitles()
        {
            var userID = User.Identity.GetUserId();         
            return Json(await service.GetTitlesFromHome(userID, repository), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetForm(int TitleId)
        {
            return Json(await service.GetFormFromHome(TitleId, repository),JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> SaveForm(List<AssignedTitles> List)
        {
            var userID = User.Identity.GetUserId();
            return Json(await service.SubmitFormFromHome(List,userID, repository), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AssignTitles()
        {
            return View();
        }

        public async Task<JsonResult> GetAllUsers()
        {
            var userID = User.Identity.GetUserId();
            return Json(await service.GetAllUsersFromHome(userID, repository),JsonRequestBehavior.AllowGet);
        }

        public JsonResult AllTitles()
        {
            var userID = User.Identity.GetUserId();
            var Titles = new List<AssignedTitles>();
            using (var client = new HttpClient())
            {               
                client.BaseAddress = new Uri("http://feedbacksystem.api.com/api/");
                //HTTP GET
                var responseTask = client.GetAsync("MicroService/GetAllTitlesForReports?userid=" + userID);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<AssignedTitles>>();
                    readTask.Wait();
                    Titles = readTask.Result;
                }

            }
            return Json(Titles, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> SaveSelectedUsers(List<AssignedTitles> list)
        {
            return Json(await service.SaveSelectedUsersFromHome(list, repository), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> RemoveTitleFromUser(int id)
        {
            var userID = User.Identity.GetUserId();
            return Json(await service.RemoveTitleFromUserFromHome(userID,id, repository), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Forms()
        {
            return View();
        }
       
        public async Task<JsonResult> GetFormsList()
        {
            var userID = User.Identity.GetUserId();
            return Json(await service.GetFormsListFromHome(userID, repository), JsonRequestBehavior.AllowGet);
        }
       
        public async Task<JsonResult> SaveDataInDatabase(TitleAndQuestions model)
        {
            return Json(await service.SaveDataInDatabaseFromHome(model, repository), JsonRequestBehavior.AllowGet);
        }
     
        public async Task<JsonResult> GetFormById(int QuestionId)
        {
            return Json(await service.GetFormByIdFromHome(QuestionId, repository) , JsonRequestBehavior.AllowGet);
        }
  
        public async Task<JsonResult> DeleteQuestion(int QuestionId)
        {
            var userID = User.Identity.GetUserId();
            return Json(await service.DeleteFromHome(QuestionId,userID, repository), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Users()
        {
            return View();
        }

        public async Task<JsonResult> GetUsersList()
        {
            var userID = User.Identity.GetUserId();
            return Json(await service.GetAllUsersFromHome(userID, repository), JsonRequestBehavior.AllowGet);
        }

       public async Task<JsonResult> SaveDataInDatabase1(AssignedTitles model)
        {
            return Json(await service.SaveDataInDatabase1FromHome(model, repository), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetUserById(int RefId)
        {
            return Json(await service.GetUserByIdFromHome(RefId, repository), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeleteUser(int RefId)
        {
            var userID = User.Identity.GetUserId();
            return Json(await service.DeleteUserFromHome(RefId, userID, repository), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> SaveTitle(string txt1)
        {
            Session["txt"] = txt1;
            var userID = User.Identity.GetUserId();
            return Json(await service.SaveTitleFromHome(txt1, userID, repository), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> SaveResult(List<TitleAndQuestions> questions)
        {
            var userID = User.Identity.GetUserId();
           
            return Json(await service.SaveQuestionsFromHome(questions, Session["txt"].ToString(), userID, repository), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Reports()
        {
            return View();
        }

        public JsonResult GenerateExcel(int titleId)
        {
            var obj = new LoginRequest();
            obj.Username = Request.Cookies["mybigcookie"]["name"];
            obj.Password = Request.Cookies["mybigcookie"]["password"];
            GetToken(obj);

            var List = GetList(titleId, 1);
            Excel(List,1);
            var List1 = GetList(titleId, 2);
            Excel(List1, 2);
            var List2 = GetList(titleId, 3);
            Excel(List2, 3);
            return  Json(true,JsonRequestBehavior.AllowGet);
        }

        public void GetToken(LoginRequest obj)
        {
          
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://feedbacksystem.api.com/api/");
            //HTTP GET
            var responseTask = client.PostAsJsonAsync("LogIn/Authenticate", obj);
            responseTask.Wait();
            var result1 = responseTask.Result;
            if (result1.IsSuccessStatusCode)
            {
                var readTask = result1.Content.ReadAsAsync<string>();
                readTask.Wait();
                string val = (readTask.Result).ToString();
                Session["token"] = val;
            }
        }

        public  List<TitleAndQuestions> GetList(int titleid, int type)
        {
            string baseUrl = "http://feedbacksystem.api.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var contentType = new MediaTypeWithQualityHeaderValue
        ("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer",
        (Session["token"]).ToString());

            HttpResponseMessage response = client.GetAsync
        ("api/Values/GetAllReports?titleid=" + titleid + "&type=" + type + "").Result;
            string stringData = response.Content.
        ReadAsStringAsync().Result;
            List<TitleAndQuestions> data = JsonConvert.DeserializeObject
        <List<TitleAndQuestions>>(stringData);
            return data;
        }
        
        public void Excel(List<TitleAndQuestions> list,int type)
        {
            ReportExcel excel = new ReportExcel();
            Response.ClearContent();
            Response.Buffer = true;
            if (type==1)
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                Response.AddHeader("content-disposition", "attachment;  filename=" + "ExcelDemo.xls");
            }
           
            excel.GenerateExcel(list,type);
            Response.End();
            Response.Flush();

        }

        public JsonResult GetCompanyId()
        {
            var userID = User.Identity.GetUserId();
            return Json(service.getCompanyIdFromHome(userID, repository), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetAssignedForms()
        {
            var userID = User.Identity.GetUserId();
            return Json(service.GetAssignedFormsFromHome(userID, repository), JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetSubmittedForms()
        {
            var userID = User.Identity.GetUserId();
            return Json(service.GetSubmittedFormsFromHome(userID, repository), JsonRequestBehavior.AllowGet);
        }

        
        [Authorize(Roles = "admin")]
        public ActionResult GetFormsPerUser()
        {
         var  nodes = new List<UserAndForm>();
            var userID = User.Identity.GetUserId();
            var List = service.GetUserNameAndFormListsFromHome(userID,repository);

            foreach (var type in List)
            {
                nodes.Add(new UserAndForm { id = type.UserId.ToString(), parent = "#", text = type.UserName });
            }
            foreach (var subType in List)
            {
                foreach (var l in subType.List)
                {
                nodes.Add(new UserAndForm { id = l.UserId.ToString() + "-" + l.titleId.ToString(), parent = l.UserId.ToString(), text = l.title });
                }
            }

            ViewBag.Json = (new JavaScriptSerializer()).Serialize(nodes);
            
            return View();
        }
    }
}