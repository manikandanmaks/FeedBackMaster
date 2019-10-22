using AutoMapper;
using FeedBackSystem.DataAccessLayer.DboModels;
using FeedBackSystem.Model.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackSystem.DataAccessLayer.MapperPro
{
   public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AssignedFormsDetailsDbo, AssignedFormsDetails>();
            CreateMap<AssignedTitlesDbo, AssignedTitles>();
            CreateMap<TitleAndQuestionsDbo, TitleAndQuestions>();
            CreateMap<RolesDbo, Roles>();
            CreateMap<RegisterMutlipleUsersDbo, RegisterMutlipleUsers>();
            CreateMap<UserNameAndFormListDbo, UserNameAndFormList>();
        }
       
    }
}
