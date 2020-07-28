using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedCoreApi_Test.Models
{
    public class UserModel
    {
        public int user_id { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}