using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackSystem.DataAccessLayer.DboModels

{
    public class LoginRequestDbo
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}