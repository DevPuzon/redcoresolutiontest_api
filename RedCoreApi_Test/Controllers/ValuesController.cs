using RedCoreApi_Test.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RedCoreApi_Test.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public List<values_pojo> Get()
        {
            List<values_pojo> list = new List<values_pojo>();
            db_con dc = new db_con();
            DataSet ds;
            DataRow drow;
            string query;
            try
            { 
                query = $@"select * from tbl_movies";
                ds = dc.Connection2(query); 
                for (int i = 0;i < ds.Tables[0].Rows.Count; i++)
                {
                    drow = ds.Tables[0].Rows[i];
                    list.Add(new values_pojo {
                        name = drow.ItemArray.GetValue(0).ToString(),
                        description = drow.ItemArray.GetValue(1).ToString(),
                    });
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        // GET api/values/5
        public string Get(int id)
        {
            Console.Write("try");
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public class values_pojo
    {
        public string  name{get;set; }
        public string description { get; set; }
    }
}
