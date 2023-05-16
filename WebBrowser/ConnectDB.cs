using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace WebBrowser
{
    public class ConnectDB
    {
        public static string connectionString = "Host = localhost; Port = 5432; Database = postgres; Username = postgres; Password = 123;";
        static string tableName = "";
        static string queryWrite = "insert into " + tableName + " values ();"; //запрос на запись
        static string queryRead = "select * from " + tableName + ";"; //запрос на чтение

        static public bool Connect()
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
        static public void WriteInDb()
        {
            using(NpgsqlConnection connection = new NpgsqlConnection(connectionString)) 
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(queryWrite, connection);
                command.ExecuteNonQuery();
            }
        }
        static public InfoFromDb ReadFormDb()
        {
            InfoFromDb infoFromDb = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(queryRead, connection);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        infoFromDb = new InfoFromDb();

                    }
                }
            }

            return infoFromDb;
        }
        
    }
}
