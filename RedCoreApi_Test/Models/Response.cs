using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedCoreApi_Test.Models
{
    public class Response
    {
        public string message { get; set; }
        public int status_code { get; set; }
        public string exception { get; set; }
        public object data { get; set; }
    }
}