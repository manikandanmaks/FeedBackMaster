using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FeedBackSystem.Domain.RepositoryInterface;
using FeedBackSystem.Domain.ServiceInterface;
using FeedBackSystem.Model.DtoModels;


namespace FeedBackSystem.BussinessLogicLayer.Service
{
    public class FeedBackBussiness : IFeedBackBussiness
    {

        
       
        public bool SaveCompanyFromBase(string txt1, IAddTitleAndQuestions repository)
        {
            return repository.SaveCompanyName(txt1);
        }
        public List<AssignedTitles> GetAllUsers1FromBase(IAddTitleAndQuestions repository)
        {
            return repository.GetAllUsers1();
        }
        public List<Roles> GetAllRolesFromBase(IAddTitleAndQuestions repository)
        {
            return repository.GetAllRoles();
        }
        public bool SaveSelectedUsers1FromBase(List<AssignedTitles> list1, IAddTitleAndQuestions repository)
        {
            return repository.SaveSelectedUsers1(list1);
        }
        public async Task<List<AssignedTitles>> GetAllUsers2FromBase(IAddTitleAndQuestions repository)
        {
            return await repository.GetAllUsers2();
        }
        public List<AssignedTitles> GetAllAdminFromBase(IAddTitleAndQuestions repository)
        {
            return repository.GetAllAdmin();
        }
        public List<AssignedTitles> GetAllCompanyFromBase(IAddTitleAndQuestions repository)
        {
            return repository.GetAllCompany();
        }
        public bool AssignCompanyFromBase(List<AssignedTitles> List, IAddTitleAndQuestions repository)
        {
            return repository.AssignCompany(List);
        }
        public List<RegisterMutlipleUsers> ImportFromHome(HttpPostedFileBase excelFile, IAddTitleAndQuestions repository)
        {
            return repository.Import(excelFile);
        }
        public async Task<List<AssignedTitles>> GetTitlesFromHome(string userID, IAddTitleAndQuestions repository)
        {
            return await repository.GetTitles(userID);
        }
        public async Task<List<AssignedTitles>> GetFormFromHome(int TitleId, IAddTitleAndQuestions repository)
        {
            return await repository.GetForm(TitleId);
        }
        public async Task<bool> SubmitFormFromHome(List<AssignedTitles> List, string userid, IAddTitleAndQuestions repository)
        {
            return await repository.SubmitForm(List, userid);
        }
        public async Task<List<AssignedTitles>> GetAllUsersFromHome(string userID, IAddTitleAndQuestions repository)
        {
            return await repository.GetAllUsers(userID);
        }
        public async Task<bool> SaveSelectedUsersFromHome(List<AssignedTitles> List, IAddTitleAndQuestions repository)
        {
            return await repository.SaveSelectedUsers(List);
        }

        public async Task<bool> RemoveTitleFromUserFromHome(string UserId, int id, IAddTitleAndQuestions repository)
        {
            return await repository.RemoveTitleFromUser(UserId, id);
        }

        public async Task<List<TitleAndQuestions>> GetFormsListFromHome(string userID, IAddTitleAndQuestions repository)
        {
            return await repository.GetFormsList(userID);
        }

        public async Task<bool> SaveDataInDatabaseFromHome(TitleAndQuestions model, IAddTitleAndQuestions repository)
        {
            return await repository.SaveDataInDatabase(model);
        }

        public async Task<TitleAndQuestions> GetFormByIdFromHome(int QuestionId, IAddTitleAndQuestions repository)
        {
            return await repository.GetFormById(QuestionId);
        }
       public async Task<bool> DeleteFromHome(int QuestionId, string userID, IAddTitleAndQuestions repository)
        {
            return await repository.Delete(QuestionId, userID);
        }
        public async Task<bool> SaveDataInDatabase1FromHome(AssignedTitles model, IAddTitleAndQuestions repository)
        {
            return await repository.SaveDataInDatabase1(model);
        }
        public async Task<AssignedTitles> GetUserByIdFromHome(int UserId, IAddTitleAndQuestions repository)
        {
            return await repository.GetUserById(UserId);
        }
        public async Task<bool> DeleteUserFromHome(int UserId, string userID, IAddTitleAndQuestions repository)
        {
            return await repository.DeleteUser(UserId, userID);
        }

        public async Task<bool> SaveTitleFromHome(string title, string id, IAddTitleAndQuestions repository)
        {
            return await repository.SaveTitle(title, id);
        }
        public async Task<bool> SaveQuestionsFromHome(List<TitleAndQuestions> questions, string title, string userId, IAddTitleAndQuestions repository)
        {
            return await repository.SaveQuestions(questions, title, userId);
        }
        public string getCompanyIdFromHome(string id, IAddTitleAndQuestions repository)
        {
            return repository.getCompanyId(id);
        }
        public List<AssignedFormsDetails> GetAssignedFormsFromHome(string userid, IAddTitleAndQuestions repository)
        {
            return repository.GetAssignedForms(userid);
        }
        public List<AssignedFormsDetails> GetSubmittedFormsFromHome(string userID, IAddTitleAndQuestions repository)
        {
            return repository.GetSubmittedForms(userID);
        }
        public List<UserNameAndFormList> GetUserNameAndFormListsFromHome(string uId,IAddTitleAndQuestions repository)
        {
            return repository.GetUserNameAndFormLists(uId);
        }
    }
}

