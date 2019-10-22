using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackSystem.Model.DtoModels
{
 public   class UserNameAndFormList
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<FormList> List { get; set; }
    }

  
}
