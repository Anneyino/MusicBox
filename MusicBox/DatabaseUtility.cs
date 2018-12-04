using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MusicBox
{
    class DatabaseUtility
    {
        private static User currentUser;
        private static string connDatabase = "MusicBox";
        private static string connHost = "localhost";
        private static string constr;

        public void setUser(string name, string passwrd)
        {
            currentUser.UserName = name;
            currentUser.Password = passwrd;
            constr = string.Format("Database={0};Data Source={1};User Id={2};Password={3};Charset=utf8", connDatabase, connHost,name,passwrd);
        }

        private static MySqlConnection openConn()

        {

            MySqlConnection conn = new MySqlConnection(constr);

            try

            {

                conn.Open();

            }

            catch (MySqlException exp)

            {
                //MessageBox.Show("无法访问数据库，请检查是否成功执行sql文件，或是输入了错误的用户密码");
                // cannot access the database, which means a possible failure in network connection
               
                Console.WriteLine(exp.Message);

                return null;

            }

            return conn;

        }

        public static int AddNewSong(ref Song song)
        {
            MySqlConnection conn = openConn();
            if (conn == null)

                return -1;  // -1 means cannot connect to database

            string sqlStr = string.Format("INSERT INTO Songs(song_name,song_path,publish_date) VALUES('{0}','{1}','{2}');", song.Song_name,song.Song_path,song.Publish_date);

            MySqlCommand cmd = new MySqlCommand(sqlStr, conn);

            try

            {

                cmd.ExecuteNonQuery();

            }
            catch (Exception e)

            {

                conn.Close();

                return -2; // duplicate song name or wrong format for publish_date

            }

            conn.Close();

            return 1;// 1 means everything is right
        }
    }
}
