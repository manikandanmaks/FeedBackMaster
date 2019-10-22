using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FeedBackSystem.Domain.RepositoryInterface;
using FeedBackSystem.DataAccessLayer;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;

using Excel1 = Microsoft.Office.Interop.Excel;
using FeedBackSystem.DataAccessLayer.DboModels;
using FeedBackSystem.Model.DtoModels;
using AutoMapper;
using FeedBackSystem.DataAccessLayer.MapperPro;
using Dapper;
using System.Web.Mvc;



namespace FeedBackSystem.DataAccessLayer
{
    public class AddTitleAndQuestions : IAddTitleAndQuestions
    {
       
       
        public string getCompanyIdforAcc(string id)
        {
            string cId;
            string query1 = "select [companyId] from [AspNetUsers] where Id ='" + id + "' ";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query1, MyConnection))
                {
                    SqlDataReader sdr = MyCommand.ExecuteReader();
                    sdr.Read();
                    cId = Convert.ToString(sdr["companyId"]);
                }
                MyConnection.Close();
            }
            return cId;
        }

        public List<AssignedFormsDetails> GetAssignedForms(string userid)
        {
            int cId = int.Parse(new AddTitleAndQuestions().getCompanyIdforAcc(userid));
            IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
             var parameter = new DynamicParameters();
            parameter.Add("@cid", cId);
             var result = db.Query<AssignedFormsDetailsDbo>("sp_GetAssignedForms", parameter, commandType: CommandType.StoredProcedure).ToList();      
           
            var res = Mapper.Map<List<AssignedFormsDetails>>(result);
            return res;
        }

        public List<AssignedFormsDetails> GetSubmittedForms(string userID)
        {

            IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var parameter = new DynamicParameters();
            parameter.Add("@uid", userID);
            var result = db.Query<AssignedFormsDetails>("sp_Getsubmittedforms", parameter, commandType: CommandType.StoredProcedure).ToList();
            return result;
        }
        public bool AssignCompanyForUsers(string uId, int cId)
        {
            string query = "UPDATE [AspNetUsers] SET [companyId]=" + cId + " ,[isAssigned]=1,  [isCompanyAssigned]=1  WHERE [Id]='" + uId + "'";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();
                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    MyCommand.ExecuteNonQuery();
                }
                MyConnection.Close();
            }
            return true;
        }
        public bool IsAssignRole(string uId)
        {
            string query = "UPDATE [AspNetUsers] SET [isAssigned]=1  WHERE [Id]='" + uId + "'";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();
                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    MyCommand.ExecuteNonQuery();
                }
                MyConnection.Close();
            }
            return true;

        }

        public string getCompanyId(string id)
        {
            string cId;
            string query1 = "select [companyId] from [AspNetUsers] where Id ='" + id + "' ";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query1, MyConnection))
                {
                    SqlDataReader sdr = MyCommand.ExecuteReader();
                    sdr.Read();
                    cId = Convert.ToString(sdr["companyId"]);
                }
                MyConnection.Close();
            }
            return cId;
        }


        public async Task<bool> SaveTitle(string title, string id)
        {
            bool status;
            int cId = Convert.ToInt32(getCompanyId(id));
            string query = "insert into Tbl_FeedBack_Titles (title,companyId) values('" + title + "'," + cId + ")";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();
                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    await MyCommand.ExecuteNonQueryAsync();
                    status = true;
                }
                MyConnection.Close();
            }
            return status;
        }

        public async Task<bool> SaveQuestions(List<TitleAndQuestions> questions, string title, string userId)
        {
            bool status;
            string query = "select titleId from Tbl_FeedBack_Titles where title='" + title + "'";
            int titleId;
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {

                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                    sdr.Read();
                    titleId = Convert.ToInt32(sdr["titleId"]);
                }
                MyConnection.Close();

                foreach (var ques in questions)
                {
                    string query1 = "insert into Tbl_FeedBack_QuestionsAndComments (titleId,question,isDeleted,updatedBy,updatedTime) values(" + titleId + ",'" + ques.Question + "'," + 0 + ",'" + userId + "',GETDATE())";
                    MyConnection.Open();

                    using (SqlCommand MyCommand = new SqlCommand(query1, MyConnection))
                    {
                        await MyCommand.ExecuteNonQueryAsync();

                    }
                    MyConnection.Close();

                }
                status = true;

            }
            return status;
        }
        public async Task<List<AssignedTitles>> GetTitles(string userID)
        {

            int cId;
            string query2 = "select [companyId] from [AspNetUsers] where Id ='" + userID + "' ";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query2, MyConnection))
                {
                    SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                    sdr.Read();
                    cId = int.Parse(sdr["companyId"].ToString());
                }
                MyConnection.Close();
            }

            List<AssignedTitlesDbo> list = new List<AssignedTitlesDbo>();


            string query = "select titleId from Tbl_Assigned_Titles_To_The_User where userId='" + userID + "' and isDeleted!=1";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                    while (sdr.Read())
                    {
                        string query1 = "select * from Tbl_FeedBack_Titles where titleId=" + Convert.ToInt32(sdr["titleId"]) + " and companyId='" + cId + "'";

                        using (SqlConnection MyConnection1 = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
                        {
                            MyConnection1.Open();
                            using (SqlCommand MyCommand1 = new SqlCommand(query1, MyConnection1))
                            {

                                SqlDataReader sdr1 = await MyCommand1.ExecuteReaderAsync();
                                while (sdr1.Read())
                                {
                                    AssignedTitlesDbo t = new AssignedTitlesDbo
                                    {
                                        TitleId = Convert.ToInt32(sdr1["titleId"]),
                                        Title = sdr1["title"].ToString()
                                    };
                                    list.Add(t);

                                }
                            }
                            MyConnection1.Close();

                        }

                    }

                }
                MyConnection.Close();
            }
        
            var res = Mapper.Map<List<AssignedTitles>>(list);
            return res;
        }

        public async Task<List<AssignedTitles>> GetForm(int TitleId)
        {
            List<AssignedTitlesDbo> list = new List<AssignedTitlesDbo>();
            string query = "select * from Tbl_FeedBack_QuestionsAndComments where titleId =" + TitleId + "  AND  isDeleted != 1";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                    while (sdr.Read())
                    {
                        AssignedTitlesDbo user = new AssignedTitlesDbo
                        {
                            QuestionId = Convert.ToInt32(sdr["questionId"]),
                            Question = sdr["question"].ToString()
                        };
                        list.Add(user);

                    }

                }
                MyConnection.Close();
            }
       
            var res = Mapper.Map<List<AssignedTitles>>(list);
            return res;
        }

        public async Task<bool> SubmitForm(List<AssignedTitles> List, string userid)
        {
            bool status;
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {

                foreach (var Ques in List)
                {
                    string query = "insert into Tbl_CommentsAndRating (questionId,comment,rating,updatedby,updatedtime) values(" + Ques.QuestionId + ",'" + Ques.Comment + "'," + Ques.Rating + ",'" + userid + "',GETDATE())";
                    MyConnection.Open();

                    using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                    {
                        await MyCommand.ExecuteNonQueryAsync();

                    }
                    MyConnection.Close();


                }
                status = true;
            }

            return status;
        }

        public async Task<List<AssignedTitles>> GetAllUsers(string userID)
        {
            int cId;
            string query2 = "select [companyId] from [AspNetUsers] where Id ='" + userID + "' ";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query2, MyConnection))
                {
                    SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                    sdr.Read();
                    cId = int.Parse(sdr["companyId"].ToString());
                }
                MyConnection.Close();
            }



            string query1 = "select UserId from AspNetUserRoles where RoleId='2'";
            var list1 = new List<AssignedTitlesDbo>();
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query1, MyConnection))
                {
                    SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                    while (sdr.Read())
                    {
                        AssignedTitlesDbo user = new AssignedTitlesDbo
                        {
                            UserId = sdr["UserId"].ToString(),
                        };
                        list1.Add(user);

                    }

                }
                MyConnection.Close();
            }
            var list = new List<AssignedTitlesDbo>();
            foreach (var n in list1)
            {
                string query = "select Id,UserName,RefId from dbo.AspNetUsers where Id='" + n.UserId + "' And isDeleted != 1 And companyId=" + cId + "  ";
                using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
                {
                    MyConnection.Open();

                    using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                    {
                        SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                        while (sdr.Read())
                        {
                            AssignedTitlesDbo user = new AssignedTitlesDbo
                            {
                                UserId = sdr["Id"].ToString(),
                                RefId = Convert.ToInt32(sdr["RefId"]),
                                User = sdr["UserName"].ToString()
                            };
                            list.Add(user);

                        }

                    }
                    MyConnection.Close();
                }
            }
        
            var res = Mapper.Map<List<AssignedTitles>>(list);
            return res;
        }

        public async Task<List<AssignedTitles>> GetAllUsers2()
        {
            string query1 = "select UserId from AspNetUserRoles where RoleId='2'";
            var list1 = new List<AssignedTitlesDbo>();
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query1, MyConnection))
                {
                    SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                    while (sdr.Read())
                    {
                        AssignedTitlesDbo user = new AssignedTitlesDbo
                        {
                            UserId = sdr["UserId"].ToString(),
                        };
                        list1.Add(user);

                    }

                }
                MyConnection.Close();
            }
            var list = new List<AssignedTitlesDbo>();
            foreach (var n in list1)
            {
                string query = "select Id,UserName,RefId from dbo.AspNetUsers where Id='" + n.UserId + "' And isDeleted != 1 And isCompanyAssigned!=1";
                using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
                {
                    MyConnection.Open();

                    using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                    {
                        SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                        while (sdr.Read())
                        {
                            AssignedTitlesDbo user = new AssignedTitlesDbo
                            {
                                UserId = sdr["Id"].ToString(),
                                RefId = Convert.ToInt32(sdr["RefId"]),
                                User = sdr["UserName"].ToString()
                            };
                            list.Add(user);

                        }

                    }
                    MyConnection.Close();
                }
            }
        
            var res = Mapper.Map<List<AssignedTitles>>(list);
            return res;
        }

        public List<AssignedTitles> GetAllAdmin()
        {
            string query1 = "select UserId from AspNetUserRoles where RoleId='1'";
            var list1 = new List<AssignedTitlesDbo>();
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query1, MyConnection))
                {
                    SqlDataReader sdr = MyCommand.ExecuteReader();
                    while (sdr.Read())
                    {
                        AssignedTitlesDbo user = new AssignedTitlesDbo
                        {
                            UserId = sdr["UserId"].ToString(),
                        };
                        list1.Add(user);

                    }

                }
                MyConnection.Close();
            }
            var list = new List<AssignedTitlesDbo>();
            foreach (var n in list1)
            {
                string query = "select Id,UserName,RefId from dbo.AspNetUsers where Id='" + n.UserId + "' And isDeleted != 1  And [isCompanyAssigned] !=1";
                using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
                {
                    MyConnection.Open();

                    using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                    {
                        SqlDataReader sdr = MyCommand.ExecuteReader();
                        while (sdr.Read())
                        {
                            AssignedTitlesDbo user = new AssignedTitlesDbo
                            {
                                UserId = sdr["Id"].ToString(),
                                RefId = Convert.ToInt32(sdr["RefId"]),
                                User = sdr["UserName"].ToString()
                            };
                            list.Add(user);

                        }

                    }
                    MyConnection.Close();
                }
            }
       
            var res = Mapper.Map<List<AssignedTitles>>(list);
            return res;
        }

        public List<AssignedTitles> GetAllCompany()
        {
            var list = new List<AssignedTitlesDbo>();
            string query = "select * from Tbl_CompanyDetails";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    SqlDataReader sdr = MyCommand.ExecuteReader();
                    while (sdr.Read())
                    {
                        AssignedTitlesDbo company = new AssignedTitlesDbo
                        {
                            CompanyId = Convert.ToInt32(sdr["companyId"]),
                            Company = sdr["companyName"].ToString()
                        };
                        list.Add(company);

                    }

                }
                MyConnection.Close();
            }
           
            var res = Mapper.Map<List<AssignedTitles>>(list);
            return res;
        }


        public async Task<List<AssignedTitles>> AllTitles(string id)
        {
            int cId;
            string query2 = "select [companyId] from [AspNetUsers] where Id ='" + id + "' ";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query2, MyConnection))
                {
                    SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                    sdr.Read();
                    cId = int.Parse(sdr["companyId"].ToString());
                }
                MyConnection.Close();
            }


            var list = new List<AssignedTitlesDbo>();
            string query = "select * from Tbl_FeedBack_Titles where companyId='" + cId + "'";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                    while (sdr.Read())
                    {
                        AssignedTitlesDbo user = new AssignedTitlesDbo
                        {
                            TitleId = Convert.ToInt32(sdr["titleId"]),
                            Title = sdr["title"].ToString()
                        };
                        list.Add(user);

                    }

                }
                MyConnection.Close();
            }
          
            var res = Mapper.Map<List<AssignedTitles>>(list);
            return res;
        }

        public async Task<bool> SaveSelectedUsers(List<AssignedTitles> List)
        {

            foreach (var li in List)
            {
                string query = "insert into Tbl_Assigned_Titles_To_The_User(userId,titleId) values('" + li.UserId + "'," + li.TitleId + ")";
                using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
                {
                    MyConnection.Open();
                    using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                    {
                        await MyCommand.ExecuteNonQueryAsync();
                    }
                    MyConnection.Close();
                }
            }
            return true;
        }

        public async Task<bool> RemoveTitleFromUser(string UserId, int id)
        {

            string query = "UPDATE Tbl_Assigned_Titles_To_The_User SET  isDeleted=1 ,submittedtime=GETDATE()  WHERE titleId=" + id + " and userId='" + UserId + "' and submittedtime is null";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();
                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    await MyCommand.ExecuteNonQueryAsync();
                }
                MyConnection.Close();
            }
            return true;
        }


        public async Task<bool> Delete(int QuestionId, string userID)
        {

            bool result = false;
            string query = "UPDATE Tbl_FeedBack_QuestionsAndComments SET deletedTime=GETDATE(),deletedBy='" + userID + "' ,isDeleted=1 WHERE questionId=" + QuestionId + "";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {

                MyConnection.Open();
                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    await MyCommand.ExecuteNonQueryAsync();
                }
                MyConnection.Close();
            }
            return result;
        }


        public async Task<TitleAndQuestions> GetFormById(int QuestionId)
        {
            var ListStudents = new TitleAndQuestionsDbo();

            string query = "select * from Tbl_FeedBack_QuestionsAndComments where questionId=" + QuestionId + "";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {

                using (SqlCommand MyCommand = new SqlCommand(query))
                {
                    MyCommand.Connection = MyConnection;
                    MyConnection.Open();
                    SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                    sdr.Read();


                    ListStudents.QuestionId = Convert.ToInt32(sdr["questionId"]);
                    ListStudents.Question = sdr["question"].ToString();

                    MyConnection.Close();

                }
            }
           
            var res = Mapper.Map<TitleAndQuestions>(ListStudents);
            return res;
        }

        public async Task<bool> SaveDataInDatabase(TitleAndQuestions model)
        {

            bool status = false;
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {

                MyConnection.Open();
                //if (model.LoginHistoryId > 0)
                //{
                using (SqlCommand MyCommand = new SqlCommand("update Tbl_FeedBack_QuestionsAndComments set   question=' " + model.Question + "' where questionId =' " + model.QuestionId + "' ", MyConnection))
                {
                    await MyCommand.ExecuteNonQueryAsync();
                    status = true;
                }



                //}
                //else
                //{
                //    string query = "insert into LogIn_History_Table values('" + model.UserName + "','" + model.UserPassword + "')";
                //    SqlCommand MyCommand = new SqlCommand(query, MyConnection);
                //    MyCommand.ExecuteNonQuery();
                //    status = true;
                //}
                MyConnection.Close();
            }
            return status;
        }

        public async Task<List<TitleAndQuestions>> GetFormsList(string userID)
        {
            int cId;
            string query1 = "select [companyId] from [AspNetUsers] where Id ='" + userID + "' ";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query1, MyConnection))
                {
                    SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                    sdr.Read();
                    cId = int.Parse(sdr["companyId"].ToString());
                }
                MyConnection.Close();
            }
           var list = new List<TitleAndQuestionsDbo>();
            string query = "select * from Tbl_FeedBack_Titles where companyId='" + cId + "'";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                    while (sdr.Read())
                    {
                        TitleAndQuestionsDbo user = new TitleAndQuestionsDbo
                        {
                            TitleId = Convert.ToInt32(sdr["titleId"]),
                            Title = sdr["title"].ToString()
                        };
                        list.Add(user);

                    }

                }
                MyConnection.Close();
            }
           var ListForms = new List<TitleAndQuestionsDbo>();


            foreach (var itr in list)
            {
                string query2 = "select * from Tbl_FeedBack_QuestionsAndComments where isDeleted != 1 and  titleId=" + itr.TitleId + " ";
                using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
                {

                    using (SqlCommand MyCommand = new SqlCommand(query2))
                    {
                        MyCommand.Connection = MyConnection;
                        MyConnection.Open();
                        SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                        while (sdr.Read())
                        {
                            TitleAndQuestionsDbo form = new TitleAndQuestionsDbo
                            {
                                QuestionId = Convert.ToInt32(sdr["questionId"]),
                                TitleId = Convert.ToInt32(sdr["titleId"]),
                                Title = itr.Title,
                                Question = sdr["question"].ToString()

                            };
                            ListForms.Add(form);
                        }
                        MyConnection.Close();

                    }
                }

            }
          
            var res = Mapper.Map<List<TitleAndQuestions>>(ListForms);
            return res;
        }

        public async Task<bool> DeleteUser(int UserId, string userID)
        {

            bool result = false;
            string query = "UPDATE AspNetUsers SET deletedTime=GETDATE(),deletedby='" + userID + "' ,isDeleted=1 WHERE RefId='" + UserId + "'";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {

                MyConnection.Open();
                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    await MyCommand.ExecuteNonQueryAsync();
                    result = true;
                }
                MyConnection.Close();
            }
            return result;
        }


        public async Task<AssignedTitles> GetUserById(int UserId)
        {
            var ListStudents = new AssignedTitlesDbo();

            string query = "select * from AspNetUsers where RefId='" + UserId + "'";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {

                using (SqlCommand MyCommand = new SqlCommand(query))
                {
                    MyCommand.Connection = MyConnection;
                    MyConnection.Open();
                    SqlDataReader sdr = await MyCommand.ExecuteReaderAsync();
                    sdr.Read();


                    ListStudents.UserId = sdr["Id"].ToString();
                    ListStudents.User = sdr["UserName"].ToString();

                    MyConnection.Close();

                }
            }
           
           
            var res = Mapper.Map<AssignedTitles>(ListStudents);
            return res;
        }

        public async Task<bool> SaveDataInDatabase1(AssignedTitles model)
        {
            bool status = false;
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();
                using (SqlCommand MyCommand = new SqlCommand("update AspNetUsers set  UserName=' " + model.User + "' where RefId =' " + model.RefId + "' ", MyConnection))
                {
                    await MyCommand.ExecuteNonQueryAsync();
                    status = true;
                }
                MyConnection.Close();
            }
            return status;
        }

        public async Task<List<TitleAndQuestions>> GenerateExcel(int titleId, int type)
        {
            if (type == 1)
            {
                var list = new List<TitleAndQuestionsDbo>();
                using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
                {
                    MyConnection.Open();
                    using (SqlCommand MyCommand = new SqlCommand("getAllFormDetails", MyConnection))
                    {
                        MyCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        MyCommand.Parameters.AddWithValue("@type", 1);
                        MyCommand.Parameters.AddWithValue("@titleId", titleId);
                        SqlDataReader reader = await MyCommand.ExecuteReaderAsync();
                        TitleAndQuestionsDbo test = null;

                        while (reader.Read())
                        {
                            test = new TitleAndQuestionsDbo();
                            test.UserName = reader["UserName"].ToString();
                            test.Question = reader["question"].ToString();
                            test.Comment = reader["comment"].ToString();
                            test.Rating = int.Parse(reader["rating"].ToString());
                            list.Add(test);
                        }

                    }
                    MyConnection.Close();

                }

             
                var res = Mapper.Map<List<TitleAndQuestions>>(list);
                return res;
            }
            else if (type == 2)
            {
                var list = new List<TitleAndQuestionsDbo>();
                using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
                {
                    MyConnection.Open();
                    using (SqlCommand MyCommand = new SqlCommand("getAllFormDetails", MyConnection))
                    {
                        MyCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        MyCommand.Parameters.AddWithValue("@type", 2);
                        MyCommand.Parameters.AddWithValue("@titleId", titleId);
                        SqlDataReader reader = await MyCommand.ExecuteReaderAsync();
                        TitleAndQuestionsDbo test = null;
                        while (reader.Read())
                        {
                            test = new TitleAndQuestionsDbo();
                            test.UserName = reader["UserName"].ToString();
                            list.Add(test);
                        }
                    }
                    MyConnection.Close();
                }
            
                var res = Mapper.Map<List<TitleAndQuestions>>(list);
                return res;
            }

            return null;
        }

        public async Task<List<TitleAndQuestions>> AvgRating(int titleId)
        {

            var list = new List<TitleAndQuestionsDbo>();
            var list1 = new List<TitleAndQuestionsDbo>();
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();
                using (SqlCommand MyCommand = new SqlCommand("getAllFormDetails", MyConnection))
                {
                    MyCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    MyCommand.Parameters.AddWithValue("@type", 3);
                    MyCommand.Parameters.AddWithValue("@titleId", titleId);
                    SqlDataReader reader = await MyCommand.ExecuteReaderAsync();
                    TitleAndQuestionsDbo test = null;
                    while (reader.Read())
                    {
                        test = new TitleAndQuestionsDbo();
                        test.QuestionId = int.Parse(reader["questionId"].ToString());
                        test.Question = reader["question"].ToString();
                        test.Rating = int.Parse(reader["rating"].ToString());
                        list.Add(test);
                    }
                }
                MyConnection.Close();
            }

            int count = 0, sum = 0, repeat = 0;
            var q = "";
            foreach (var i in list)
            {

                foreach (var j in list)
                {
                    if (i.QuestionId == j.QuestionId && repeat != i.QuestionId)
                    {
                        count += 1;
                        sum += j.Rating;
                        q = j.Question;
                    }
                }
                if (count != 0)
                {
                    var temp = new TitleAndQuestionsDbo();
                    temp.Rating = sum;
                    temp.QuestionId = count;
                    temp.Question = q;
                    sum = count = 0;
                    repeat = i.QuestionId;
                    list1.Add(temp);
                    temp = null;
                    q = null;
                }
            }
            foreach (var i in list1)
            {
                decimal avg = (Convert.ToDecimal(i.Rating) / Convert.ToDecimal(i.QuestionId * 5)) * 100;
                i.AvgRating = avg;
            }

         
            var res = Mapper.Map<List<TitleAndQuestions>>(list1);
            return res;

        }

        public bool SaveCompanyName(string companyname)
        {

            bool result = false;
            string query = "insert into Tbl_CompanyDetails (companyName) values('" + companyname + "')";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {

                MyConnection.Open();
                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    MyCommand.ExecuteNonQuery();
                    result = true;
                }
                MyConnection.Close();
            }
            return result;
        }

        public List<AssignedTitles> GetAllUsers1()
        {

            var list = new List<AssignedTitlesDbo>();

            string query = "select Id,UserName from dbo.AspNetUsers where isAssigned!=1 And isDeleted != 1 ";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    SqlDataReader sdr = MyCommand.ExecuteReader();
                    while (sdr.Read())
                    {
                    var user = new AssignedTitlesDbo
                        {
                            UserId = sdr["Id"].ToString(),

                            User = sdr["UserName"].ToString()
                        };
                        list.Add(user);

                    }

                }
                MyConnection.Close();
            }
        
            var res = Mapper.Map<List<AssignedTitles>>(list);
            return res;
        }

        public List<Roles> GetAllRoles()
        {
            var list = new List<RolesDbo>();

            string query = "select Id,Name from dbo.AspNetRoles";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    SqlDataReader sdr = MyCommand.ExecuteReader();
                    while (sdr.Read())
                    {
                        var role = new RolesDbo
                        {
                            RoleId = int.Parse(sdr["Id"].ToString()),

                            Role = sdr["Name"].ToString()
                        };
                        list.Add(role);

                    }

                }
                MyConnection.Close();
            }

     
            var res = Mapper.Map<List<Roles>>(list);
            return res;

        }
        public bool SaveSelectedUsers1(List<AssignedTitles> List)
        {
            foreach (var li in List)
            {
                string query = "UPDATE AspNetUserRoles SET RoleId=" + li.RoleId + " WHERE UserId = '" + li.UserId + "' ";
                string query1 = "UPDATE AspNetUsers SET isAssigned=1 WHERE Id = '" + li.UserId + "' ";
                using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
                {
                    MyConnection.Open();
                    using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                    {
                        MyCommand.ExecuteNonQuery();
                    }
                    MyConnection.Close();
                }
                using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
                {
                    MyConnection.Open();
                    using (SqlCommand MyCommand = new SqlCommand(query1, MyConnection))
                    {
                        MyCommand.ExecuteNonQuery();
                    }
                    MyConnection.Close();
                }
            }
            return true;
        }

        public bool AssignCompany(List<AssignedTitles> List)
        {
            foreach (var li in List)
            {
                string query = "UPDATE AspNetUsers SET companyId=" + li.CompanyId + ", isCompanyAssigned=1 WHERE Id = '" + li.UserId + "' ";

                using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
                {
                    MyConnection.Open();
                    using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                    {
                        MyCommand.ExecuteNonQuery();
                    }
                    MyConnection.Close();
                }

            }
            return true;
        }

        public List<RegisterMutlipleUsers> Import(HttpPostedFileBase excelFile)
        {
            var Users = new List<RegisterMutlipleUsersDbo>();
            if (excelFile.ContentLength == 0 || excelFile == null)
            {
                //ViewPage.Error = "Please Select a excel file<br/>";
                // return Json("Index");
            }
            else
            {
                if (excelFile.FileName.EndsWith("xls") || excelFile.FileName.EndsWith("xlsx"))
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/" + excelFile.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);

                    }

                    excelFile.SaveAs(path);



                    Excel1.Application application = new Excel1.Application();
                    Excel1.Workbook workbook = application.Workbooks.Open(path);
                    Excel1.Worksheet worksheet = workbook.ActiveSheet;
                    Excel1.Range range = worksheet.UsedRange;

                    for (int row = 1; row <= range.Rows.Count; row++)
                    {
                       var user = new RegisterMutlipleUsersDbo();
                        user.Email = ((Excel1.Range)range.Cells[row, 1]).Text;
                        user.Password = ((Excel1.Range)range.Cells[row, 2]).Text;
                        // user.ConfirmPassword = ((Excel1.Range)range.Cells[row, 3]).Text;
                        Users.Add(user);
                    }
                    workbook.Close(false, Type.Missing, Type.Missing);
                    application.Quit();

                }
                else
                {
                    // ViewBag.Error = "File  type is incorrect <br/> ";
                    // return Json("Index");
                }
            }
        
            var res = Mapper.Map<List<RegisterMutlipleUsers>>(Users);
            return res;
        }

        public List<UserNameAndFormList> GetUserNameAndFormLists(string uId)
        {
            var list1 = new List<UserNameAndFormListDbo>();
            int cId =int.Parse( getCompanyIdforAcc(uId));
            string query = "  select b.Id, b.UserName from [AspNetUserRoles] as a join [AspNetUsers] as b on a.UserId=b.Id where b.companyId="+ cId + " and a.RoleId=2  ";
            using (SqlConnection MyConnection = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
                MyConnection.Open();

                using (SqlCommand MyCommand = new SqlCommand(query, MyConnection))
                {
                    SqlDataReader sdr = MyCommand.ExecuteReader();
                    while (sdr.Read())
                    {
                        var user = new UserNameAndFormListDbo
                        {
                           userId  = sdr["Id"].ToString(),

                            UserName = sdr["UserName"].ToString()
                        };
                        list1.Add(user);

                    }

                }
                MyConnection.Close();
            }
           
            
            
            using (SqlConnection MyConnection1 = new SqlConnection("data source=AMX-MANIKANDAN;database=FEEDBACK_SYSTEM;User Id=sa;Password=sa5"))
            {
               
                foreach (var l in list1)
                {
                    MyConnection1.Open();
                    string query1 = "  select a.userId,a.titleId,b.title from  [Tbl_Assigned_Titles_To_The_User] as a join [Tbl_FeedBack_Titles] as b on a.titleId = b.titleId where  a.userId='"+l.userId+"' ";
                    var list2 = new List<FormListDbo>();
                    using (SqlCommand MyCommand1 = new SqlCommand(query1, MyConnection1))
                    {
                        SqlDataReader sdr1 = MyCommand1.ExecuteReader();
                        
                        while (sdr1.Read())
                        {
                            var form = new FormListDbo
                            {
                                userId = sdr1["userId"].ToString(),
                                titleId = int.Parse( sdr1["titleId"].ToString()),
                                title = sdr1["title"].ToString()
                            };
                            list2.Add(form);

                        }

                    }
                    l.List = list2;
                    MyConnection1.Close();
                }
                
            }
            var res = Mapper.Map<List<UserNameAndFormList>>(list1);
            return res ;
        }


    }
}