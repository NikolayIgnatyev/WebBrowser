using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

        //приведение к НФ, а не как было...так при входе пользователя будем делать запись в БД пользователя
        //+ имя ПК с которого зашел, так как имя должно быть уникальным
        //Компы не меняются, а пользователи запросто могут добавляться, но если что можно будет и отредактировать
        static string tableUsersName = "users";
        static string tablePcInfoName = "pcinfo";


        static string queryWriteUserInfo = "insert into " + tableUsersName + " values (@name, @password);";
        static string queryWritePcInfo = "" +
            "insert into " + tablePcInfoName + 
            "(pcname, processor, videocard, disk, disksize, ram, ramsize, monitor, keyboard, mouse, motherboard)" + 
            " values " +
            "(@pcname, @proc, @video, @disk, @disksize, @ram, @ramsize, @monitor, @key, @mouse, @mother);"; //запрос на запись
        static string queryRead; //запрос на чтение

        /// <summary>
        /// проверка подключения к БД
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// запись в БД
        /// </summary>
        /// <param name="pcPreference"></param>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        static public void WriteInDb(PCInfo pcPreference, string name, string pass)
        {
            if (Connect()) // проверка подключения к БД
            {
                if (IsNoThereUser(name))
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        NpgsqlCommand command = new NpgsqlCommand(queryWriteUserInfo, connection);

                        // Передаваемые параметры
                        command.Parameters.AddWithValue("name", name);
                        command.Parameters.AddWithValue("password", pass);

                        command.ExecuteNonQuery();


                        MessageBox.Show("Insert user complete");
                    }

                    if (IsNoTherePC(pcPreference.PcName))
                    {
                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                        {
                            connection.Open();
                            NpgsqlCommand command = new NpgsqlCommand(queryWritePcInfo, connection);

                            // Передаваемые параметры
                            command.Parameters.AddWithValue("pcname", pcPreference.PcName);
                            command.Parameters.AddWithValue("proc", pcPreference.Proc);
                            command.Parameters.AddWithValue("video", pcPreference.Video);
                            command.Parameters.AddWithValue("disk", pcPreference.Disk);
                            command.Parameters.AddWithValue("disksize", pcPreference.SizeDiskGb);
                            command.Parameters.AddWithValue("ram", pcPreference.Ram);
                            command.Parameters.AddWithValue("ramsize", pcPreference.RamSize);
                            command.Parameters.AddWithValue("monitor", pcPreference.MonitorName);
                            command.Parameters.AddWithValue("key", pcPreference.KeyboardName);
                            command.Parameters.AddWithValue("mouse", pcPreference.MouseName);
                            command.Parameters.AddWithValue("mother", pcPreference.MotherboardName);

                            command.ExecuteNonQuery();


                            MessageBox.Show("Insert pcinfo complete");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Такой пользователь уже есть");
                }
            }
            
        }
        /// <summary>
        /// Проверка отсутствия пользователя в БД, если нет = false, иначе = true
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static bool IsNoThereUser(string name)
        {
            queryRead = "select name from " + tableUsersName + $" where name='{name}';";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(queryRead, connection);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    return !reader.HasRows;//нет ли совпадений?
                }
            }
        }
        /// <summary>
        /// Проверка отсутствия пк в базе, при отсутствие возвращает true
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static bool IsNoTherePC(string name)
        {
            queryRead = "select pcname from " + tablePcInfoName + $" where pcname='{name}';";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(queryRead, connection);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    return !reader.HasRows;//нет ли совпадений?
                }
            }
        }

    }
}
