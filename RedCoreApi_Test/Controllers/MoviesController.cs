using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedCoreApi_Test.Database;
using RedCoreApi_Test.Models;
using RedCoreApi_Test.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RedCoreApi_Test.Controllers
{
    public class MoviesController : ApiController
    {
        [Authorize(Roles = "SuperAdmin, User, Admin")]
        [EnableCors(origins: "*", headers: "*", methods: "*")] 
        public IHttpActionResult Get()
        {
            Response ret = new Response();
            List<MoviesModel> list = new List<MoviesModel>();
            db_con dc = new db_con();
            DataSet ds;
            DataRow drow;
            string query;
            try
            {
                query = $@"select  col_movie_id,col_title,col_thumbnail,
col_desc ,col_isrented,col_createdAt,col_updatedAt from tbl_movies";
                ds = dc.connection(query);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    drow = ds.Tables[0].Rows[i];
                    list.Add(new MoviesModel
                    {
                        movie_id = int.Parse(drow.ItemArray.GetValue(0).ToString()),
                         title  = drow.ItemArray.GetValue(1).ToString(),
                        thumbnail = drow.ItemArray.GetValue(2).ToString(),
                        description = drow.ItemArray.GetValue(3).ToString(),
                        isrented = drow.ItemArray.GetValue(4).ToString(),
                        createdAt = drow.ItemArray.GetValue(5).ToString(),
                        updatedAt = drow.ItemArray.GetValue(6).ToString(),
                        
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

                return Content(HttpStatusCode.InternalServerError, ret);
            } 
            return Ok(ret);
        }


        [EnableCors(origins: "*", headers: "*", methods: "*")] 
        public IHttpActionResult Get(string id)
        {
            Response ret = new Response(); 
            db_con dc = new db_con();
            MoviesModel model =null;
            DataSet ds;
            DataRow drow;
            string query;
            try
            {
                query = $@"select  col_movie_id, col_title,col_thumbnail,
col_desc ,col_isrented,col_createdAt,col_updatedAt from tbl_movies where col_movie_id = '" +id+"'";
                ds = dc.connection(query);
                drow = ds.Tables[0].Rows[0];
                model = new MoviesModel
                {
                    movie_id = int.Parse(drow.ItemArray.GetValue(0).ToString()),

                    title = drow.ItemArray.GetValue(1).ToString(),
                    thumbnail = drow.ItemArray.GetValue(2).ToString(),
                    description = drow.ItemArray.GetValue(3).ToString(),
                    isrented = drow.ItemArray.GetValue(4).ToString(),
                    createdAt = drow.ItemArray.GetValue(5).ToString(),
                    updatedAt = drow.ItemArray.GetValue(6).ToString(),
                };

                ret.status_code = 200;
                ret.message = "success";
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

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IHttpActionResult> PostAsync(MoviesModel value)
        { 
            Response ret = new Response();
            MoviesModel model = value; 
            db_con dc = new db_con();
            DataSet ds;
            string query;
            try
            {
                query = $@"insert into tbl_movies(col_thumbnail,
col_title,col_desc,col_createdAt,col_updatedAt) values 
('" + model.thumbnail + "','" + model.title + "'," +
"'" + model.description + "','" + DateTime.Now + "'," +
"'" + DateTime.Now + "')";
                ds = await dc.ConnectionAsync(query);

                ret.status_code = 200;
                ret.message = "Success to save ";
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
        [Authorize(Roles = "SuperAdmin, Admin")]
        public IHttpActionResult PutAsync(string id, MoviesModel value)
        {
            MoviesModel model = value; 
            string filename = Guid.NewGuid().ToString(); 

            Response ret = new Response();

            db_con dc = new db_con();
            DataSet ds;
            string query;
            try
            {
                query = $@"update tbl_movies set
col_thumbnail = '"+model.thumbnail+"'," + 
"col_title = '"+model.title+"'," +
"col_desc = '"+model.description+"'," +
"col_isrented = '" + model.isrented + "'," +
"col_updatedAt = '" + DateTime.Now + "'" +
"where col_movie_id = '"+ id + "'";
                ds = dc.connection(query); 
                ret.status_code = 200;
                ret.message = "Success to update";
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
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IHttpActionResult> DeleteAsync(string id)
        {
            Response ret = new Response(); 

            db_con dc = new db_con();
            DataSet ds;
            string query;
            try
            {
                query = $@"delete from tbl_movies where col_movie_id =" + id  ;
                ds = await dc.ConnectionAsync(query);

                ret.status_code = 200;
                ret.message = "Success to delete"; 
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


        //[Authorize(Roles = "SuperAdmin, User, Admin")]
        [AcceptVerbs("PUT")]
        [Route("api/movies/rented/{id}")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> PutIsRented(string id, MoviesModel value)
        {
            MoviesModel model = value;  
            Response ret = new Response(); 
            db_con dc = new db_con();
            DataSet ds;
            string query;
            try
            {
                query = $@"update tbl_movies set col_isrented = '" + model.isrented + "' where col_movie_id = " + id;
                ds = dc.connection(query);
                ret.status_code = 200;
                ret.message = "Success to update" ;
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
    }
}