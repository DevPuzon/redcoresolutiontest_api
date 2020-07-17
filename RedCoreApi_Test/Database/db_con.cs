using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace RedCoreApi_Test.Database
{
    public class db_con
    {
        static string ConnectionString = @"Server=tcp:sofwaresolutionph.database.windows.net,1433;Initial Catalog=softwaresolutionph_project;Persist Security Info=False;User ID=DevPuzon;Password=db09107809998=;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static SqlConnection con = new SqlConnection();
        public static SqlCommand cmd = new SqlCommand();
        public static SqlDataAdapter da = new SqlDataAdapter();
        public static DataSet ds = new DataSet();
        public static DataTable dt = new DataTable();
         


        public DataSet connection(string query)
        {
            try
            {
                con = new SqlConnection(ConnectionString);
                cmd = new SqlCommand(query, con);
                con.Open();

                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds); 
                con.Close();
            }
            catch (Exception e)
            {
                Debug.Write("connection err " + e.Message);
            }
            return ds;
        }
        //public DataSet Connection2(string query)
        //{
        //    try
        //    {
        //        conn = new SqlConnection(ConnectionString1);
        //        cmdd = new SqlCommand(query, conn);
        //        conn.Open();

        //        dss = new DataSet();
        //        daa = new SqlDataAdapter(cmdd);
        //        daa.Fill(dss, "Table1");
        //        conn.Close();

        //    }
        //    catch (Exception e)
        //    {
        //        Console.Write(e.Message);

        //    }
        //    return dss;
        //}
        public async Task<DataSet> ConnectionAsync(string query)
        {
            await Task.Run(() =>
            {
                connection(query);
            });
            return ds;
        }
    }
}