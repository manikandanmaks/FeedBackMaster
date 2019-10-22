using FeedBackSystem.Domain.RepositoryInterface;
using FeedBackSystem.Model.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FeedBackSystem.Domain.ServiceInterface
{
  public  interface IFeedBackBussiness
    {
         bool SaveCompanyFromBase(string txt1, IAddTitleAndQuestions repository);
         List<AssignedTitles> GetAllUsers1FromBase(IAddTitleAndQuestions repository);
         List<Roles> GetAllRolesFromBase(IAddTitleAndQuestions repository);
        bool SaveSelectedUsers1FromBase(List<AssignedTitles> list1, IAddTitleAndQuestions repository);
         Task<List<AssignedTitles>> GetAllUsers2FromBase( IAddTitleAndQuestions repository);
        List<AssignedTitles>  GetAllAdminFromBase(IAddTitleAndQuestions repository);
        List<AssignedTitles> GetAllCompanyFromBase(IAddTitleAndQuestions repository);
        bool AssignCompanyFromBase(List<AssignedTitles> List, IAddTitleAndQuestions repository);
        List<RegisterMutlipleUsers> ImportFromHome(HttpPostedFileBase excelFile, IAddTitleAndQuestions repository);
        Task<List<AssignedTitles>> GetTitlesFromHome(string userID, IAddTitleAndQuestions repository);
        Task<List<AssignedTitles>> GetFormFromHome(int TitleId, IAddTitleAndQuestions repository);
        Task<bool> SubmitFormFromHome(List<AssignedTitles> List, string userid, IAddTitleAndQuestions repository);
        Task<List<AssignedTitles>> GetAllUsersFromHome(string userID, IAddTitleAndQuestions repository);
        Task<bool> SaveSelectedUsersFromHome(List<AssignedTitles> List, IAddTitleAndQuestions repository);
        Task<bool> RemoveTitleFromUserFromHome(string UserId, int id, IAddTitleAndQuestions repository);
        Task<List<TitleAndQuestions>> GetFormsListFromHome(string userID, IAddTitleAndQuestions repository);
        Task<bool> SaveDataInDatabaseFromHome(TitleAndQuestions model, IAddTitleAndQuestions repository);
        Task<TitleAndQuestions> GetFormByIdFromHome(int QuestionId, IAddTitleAndQuestions repository);
        Task<bool> DeleteFromHome(int QuestionId, string userID, IAddTitleAndQuestions repository);
        Task<bool> SaveDataInDatabase1FromHome(AssignedTitles model, IAddTitleAndQuestions repository);
        Task<AssignedTitles> GetUserByIdFromHome(int UserId, IAddTitleAndQuestions repository);
        Task<bool> DeleteUserFromHome(int UserId, string userID, IAddTitleAndQuestions repository);
        Task<bool> SaveTitleFromHome(string title, string id, IAddTitleAndQuestions repository);
        Task<bool> SaveQuestionsFromHome(List<TitleAndQuestions> questions, string title, string userId, IAddTitleAndQuestions repository);
        string getCompanyIdFromHome(string id, IAddTitleAndQuestions repository);
        List<AssignedFormsDetails> GetAssignedFormsFromHome(string userid, IAddTitleAndQuestions repository);
        List<AssignedFormsDetails> GetSubmittedFormsFromHome(string userID, IAddTitleAndQuestions repository);
        List<UserNameAndFormList> GetUserNameAndFormListsFromHome(string uId,IAddTitleAndQuestions repository);

    }
}
