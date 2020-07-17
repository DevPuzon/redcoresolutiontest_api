using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedCoreApi_Test.Database;
using RedCoreApi_Test.Models;
using RedCoreApi_Test.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace RedCoreApi_Test.Controllers
{
    public class MoviesController : ApiController
    {
        // GET api/values
        public List<MoviesModel> Get()
        {
            Response ret = new Response();
            List<MoviesModel> list = new List<MoviesModel>();
            db_con dc = new db_con();
            DataSet ds;
            DataRow drow;
            string query;
            try
            {
                query = $@"select  * from tbl_movies";
                ds = dc.connection(query);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    drow = ds.Tables[0].Rows[i];
                    list.Add(new MoviesModel
                    {
                        docid = drow.ItemArray.GetValue(0).ToString(),
                        thumbnail = drow.ItemArray.GetValue(1).ToString(),
                        title = drow.ItemArray.GetValue(2).ToString(),
                        description = drow.ItemArray.GetValue(3).ToString(),
                        createdAt = drow.ItemArray.GetValue(4).ToString(),
                        updatedAt = drow.ItemArray.GetValue(5).ToString(),
                    });
                }
                ret.status_code= 200;
                ret.message= "success";
                ret.data = list;
            }
            catch (Exception ex)
            {

                ret.status_code  = 500;
                ret.exception = ex.Message;
                ret.message = "Internal Server Error. Somthing went Wrong!";

                //return Content(HttpStatusCode.InternalServerError, ret);
            } 
            return list;
        }


        // GET api/<controller>/5
        public string Get(string id)
        {
            return "value";
        }

        // POST api/<controller>
        public async Task<object> PostAsync(MoviesModel value)
        {
            Response ret = new Response();
            MoviesModel model = value;
            db_con dc = new db_con();
            DataSet ds;
            string query;
            try
            {
                query = $@"insert into tbl_movies(col_docid,col_thumbnail,col_title,
col_desc,col_createdAt,col_updatedAt) values ('" + Guid.NewGuid().ToString() + "','" + model.thumbnail + "','" + model.title + "','" + model.description + "','" + DateTime.Now + "','" + DateTime.Now + "')";
                ds = await dc.ConnectionAsync(query);

                ret.status_code = 200;
                ret.message = "Success to saved";
                ret.data = model;
            }
            catch (Exception ex)
            { 
                ret.status_code = 500;
                ret.exception = ex.Message;
                ret.message = "Internal Server Error. Somthing went Wrong!";

                return Content(HttpStatusCode.InternalServerError, ret);
            }
            return Ok(ret);
        }

        // PUT api/<controller>/5
        public async Task<object> PutAsync(string id, [FromBody] MoviesModel value)
        {
            MoviesModel model = value;
            Response ret = new Response();

            db_con dc = new db_con();
            DataSet ds;
            string query;
            try
            {
                query = $@"update tbl_movies set
col_thumbnail = '"+model.thumbnail+"', " +
"col_title = '"+model.title+"'," +
"col_desc = '"+model.description+"'," +
"col_updatedAt = '"+ DateTime.Now + "'" +
"where col_docid = '"+ id + "'";
                ds = await dc.ConnectionAsync(query);

                ret.status_code = 200;
                ret.message = "Success to update";
                ret.data = model;
            }
            catch (Exception ex)
            {
                ret.status_code = 500;
                ret.exception = ex.Message;
                ret.message = "Internal Server Error. Somthing went Wrong!";

                return Content(HttpStatusCode.InternalServerError, ret);
            }
            return Ok(ret);
        }

        // DELETE api/<controller>/5
        public async Task<object> DeleteAsync(string id)
        {
            Response ret = new Response(); 

            db_con dc = new db_con();
            DataSet ds;
            string query;
            try
            {
                query = $@"delete from tbl_movies where col_docid ='"+id+"'";
                ds = await dc.ConnectionAsync(query);

                ret.status_code = 200;
                ret.message = "Success to deleted"; 
            }
            catch (Exception ex)
            { 
                ret.status_code = 500;
                ret.exception = ex.Message;
                ret.message = "Internal Server Error. Somthing went Wrong!";

                return Content(HttpStatusCode.InternalServerError, ret);
            }
            return Ok(ret);
        }
    }
}