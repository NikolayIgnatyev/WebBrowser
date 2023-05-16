using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace WebBrowser
{
    public class ConnectDB
    {
        public static string connectionString = "Host = localhost; Port = 5432; Database = postgres; Username = postgres; Password = 123;";
        static string tableName = "users";
        static string queryWrite = "" +
            "insert into " + tableName + 
            "(username, password, processor, videocard, disk, disksize, ram, ramsize, monitor, keyboard, mouse, motherboard)" + 
            " values ()" +
            "(@name, @pass, @proc, @video, @disk, @disksize, @ram, @ramsize, @monitor, @key, @mouse, @mother);"; //запрос на запись
        static string queryRead; //запрос на чтение

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
        static public void WriteInDb(PC pcPreference, string name, string pass)
        {
            if (ReplyFormDb(name))
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand(queryWrite, connection);

                    // Передаваемые параметры
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("pass", pass);
                    command.Parameters.AddWithValue("proc", pcPreference.proc);
                    command.Parameters.AddWithValue("video", pcPreference.video);
                    command.Parameters.AddWithValue("dick", pcPreference.disk);
                    command.Parameters.AddWithValue("dicksize", pcPreference.sizeDiskGb);
                    command.Parameters.AddWithValue("ram", pcPreference.ram);
                    command.Parameters.AddWithValue("ramsize", pcPreference.ramSize);
                    command.Parameters.AddWithValue("monitor", pcPreference.monitorName);
                    command.Parameters.AddWithValue("key", pcPreference.keyboardName);
                    command.Parameters.AddWithValue("mouse", pcPreference.mouseName);
                    command.Parameters.AddWithValue("mother", pcPreference.motherboardName);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Insert complete");
                }
            }

        }
        static public bool ReplyFormDb(string name)
        {
            queryRead = "select username from " + tableName + $" where username='{name}';";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(queryRead, connection);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetValue(0) == null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return false;
            }
        }
        
    }
}
