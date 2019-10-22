using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackSystem.Models
{
    public class LogViewModel
    {
        public int LoginHistoryId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}