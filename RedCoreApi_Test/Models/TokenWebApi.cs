using RedCoreApi_Test.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RedCoreApi_Test.Models
{
    public class TokenWebApi : IDisposable
    {
        // SECURITY_DBEntities it is your context class
        //SECURITY_DBEntities context = new SECURITY_DBEntities();
        //This method is used to check and validate the user credentials
        public UserModel ValidateUser(string email, string password)
        {
            db_con db = new db_con();
            DataSet ds;
            DataRow drow;
            bool ret = false;
            string query = "select * from tbl_user where col_email ='" + email + "' and col_password = '" + password + "'";
            ds = db.connection(query);
            int count = ds.Tables[0].Rows.Count;
            UserModel model = null;
            if (count > 0) 
            {
                drow = ds.Tables[0].Rows[0];
                model = new UserModel
                {
                    user_id = int.Parse(drow.ItemArray.GetValue(0).ToString()),
                    full_name = drow.ItemArray.GetValue(1).ToString(),
                    email = drow.ItemArray.GetValue(2).ToString(),
                    password = drow.ItemArray.GetValue(3).ToString(),
                    updatedAt = drow.ItemArray.GetValue(4).ToString(),
                    createdAt = drow.ItemArray.GetValue(5).ToString(),
                    role = drow.ItemArray.GetValue(6).ToString(),
                };
            }
            return model;
        }
        public void Dispose()
        {
            //context.Dispose();
        }
    }
}