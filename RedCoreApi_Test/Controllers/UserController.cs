using Newtonsoft.Json;
using RedCoreApi_Test.Database;
using RedCoreApi_Test.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;


namespace RedCoreApi_Test
{
    public class UserController : ApiController
    {

        //private static string baseAddress = "http://localhost:58307/";
        private static string baseAddress = "https://redcoresolutiontestapi.azurewebsites.net/";

        [AcceptVerbs("GET","POST")]
        [Route("api/user/access")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Access(UserModel auth)
        {
            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"grant_type", "password"},
                {"username", auth.email},
                {"password", auth.password},
                };
            var tokenResponse = client.PostAsync(baseAddress + "token", new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<Token>(JsonContent);
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid email or password";
            }
            return Ok(token);
        } 

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // GET api/<controller>
        public IEnumerable Get()
        {
            return new string[] { "", "" };
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // POST api/<controller>
        public async Task<Object> PostAsync(UserModel value)
        {
            Response ret = new Response();
            UserModel model = value;
            db_con dc = new db_con();
            DataSet ds;
            string query;
            try
            {
                query = $@"insert into tbl_user(col_full_name,
col_email, col_password,col_role,col_createdAt,col_updatedAt) values(
'" + model.full_name + "', '" + model.email + "', " +
"'" + model.password + "','" + model.role + "','" + DateTime.Now + "'," +
"'" + DateTime.Now + "')";
                ds = await dc.ConnectionAsync(query);

                ret.status_code = 200;
                ret.message = "Registered";
                ret.data = model;
            }
            catch (Exception ex)
            {
                ret.status_code = 500;
                ret.exception = ex.Message;
                ret.message = "Internal Server Error. Something went Wrong!";

                return Content(HttpStatusCode.InternalServerError, ret);
            }
            return Ok(ret);
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // PUT api/<controller>/5
        public void Put(int id, UserModel value)
        {
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // DELETE api/<controller>/5
        public void Delete(int id)
        {

        }
    }
}