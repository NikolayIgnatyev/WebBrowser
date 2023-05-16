using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace WebBrowser
{
    public class ConnectDB
    {
        public static string connectionString = "Host = localhost; Port = 5432; Database = postgres; Username = postgres; Password = 123;";
        static public bool Connect(string connectionString)
        {
            try
            {
                using(NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }
        
    }
}
