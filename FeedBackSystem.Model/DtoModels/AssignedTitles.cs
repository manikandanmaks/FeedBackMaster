using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackSystem.Model.DtoModels
{
    public class AssignedTitles
    {
        public string UserId { get; set; }
        public int TitleId { get; set; } 
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        public int QuestionId { get; set; } 
        public string Question { get; set; } 
        public int Rating { get; set; } 
        public string Comment { get; set; } 
        public int RefId { get; set; }
        
    }
}