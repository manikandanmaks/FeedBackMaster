using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FeedBackSystem.DataAccessLayer.DboModels
{
    public class UserNameAndFormListDbo
    {
        public  string userId { get; set; }
        public string UserName { get; set; }
        public List<FormListDbo> List { get; set; }
    }

   
}
