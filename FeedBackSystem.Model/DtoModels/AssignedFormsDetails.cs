using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackSystem.Model.DtoModels
{
    public class AssignedFormsDetails
    {
        public string userId { get; set; }
        public int titleId { get; set; }
        public string UserName { get; set; }
        public string title { get; set; }
        public int countoftitle { get; set; }
        public string submittedtime { get; set; }
        
    }
}