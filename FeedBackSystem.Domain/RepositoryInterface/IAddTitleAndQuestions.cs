using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedBackSystem;
using System.Web;
using FeedBackSystem.Model.DtoModels;

namespace FeedBackSystem.Domain.RepositoryInterface
{
   public interface IAddTitleAndQuestions
    {
        Task<bool> SaveTitle(string title,string id);
        Task<bool> SaveQuestions(List<TitleAndQuestions> questions,string title, string userId);
        Task<List<AssignedTitles>> GetTitles(string userID);
        Task<List<AssignedTitles>> GetForm(int TitleId);
        Task<bool> SubmitForm(List<AssignedTitles> List,string userid);
        Task<bool> SaveSelectedUsers(List<AssignedTitles> List);
        Task<bool> RemoveTitleFromUser(string UserId,int id);
        Task<List<AssignedTitles>> GetAllUsers(string userID);
        Task<List<AssignedTitles>> AllTitles(string id);
        Task<bool> Delete(int QuestionId,string userID);
        Task<TitleAndQuestions> GetFormById(int QuestionId);
        Task<bool> SaveDataInDatabase(TitleAndQuestions model);
        Task<List<TitleAndQuestions>> GetFormsList(string userID);
        Task<bool> DeleteUser(int UserId, string userID);
        Task<AssignedTitles> GetUserById(int UserId);
        Task<bool> SaveDataInDatabase1(AssignedTitles model);
        Task<List<TitleAndQuestions>> GenerateExcel(int titleId, int type);
        Task<List<TitleAndQuestions>> AvgRating(int titleId);
        bool SaveCompanyName(string companyname);
        List<AssignedTitles> GetAllUsers1();
        Task<List<AssignedTitles>> GetAllUsers2();
        List<Roles> GetAllRoles();
        bool SaveSelectedUsers1(List<AssignedTitles> List);
        List<AssignedTitles> GetAllAdmin();
        List<AssignedTitles> GetAllCompany();
        bool AssignCompany(List<AssignedTitles> List);
        string getCompanyId(string id);
        List<AssignedFormsDetails> GetAssignedForms(string userid);
        List<AssignedFormsDetails> GetSubmittedForms(string userID);
        List<RegisterMutlipleUsers> Import(HttpPostedFileBase excelFile);
        List<UserNameAndFormList> GetUserNameAndFormLists(string uId);
    }
}
