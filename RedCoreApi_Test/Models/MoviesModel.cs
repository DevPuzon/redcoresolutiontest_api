using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace RedCoreApi_Test.Models
{
    public class MoviesModel
    {
        public int movie_id { get; set; }
        public string thumbnail { get; set; }
        public string title { get; set; }
        public string isrented { get; set; }
        public string description { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
         
    }
}