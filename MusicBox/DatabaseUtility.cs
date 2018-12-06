using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MusicBox
{
    class DatabaseUtility
    {
        private static User currentUser = new User();
        private static string connDatabase = "MusicBox";
        private static string connHost = "localhost";
        private static string constr;

        public static void setUser(string name, string passwrd)
        {
            currentUser.UserName = name;
            currentUser.Password = passwrd;
            constr = string.Format("Database={0};Data Source={1};User Id={2};Password={3};Charset=utf8", connDatabase, connHost,name,passwrd);
        }

        public static MySqlConnection openConn()

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

        public static int removeSong(int songID)

        {

            MySqlConnection conn = openConn();

            if (conn == null)

                return -1;  // -1 means cannot connect to database

            string sqlStr = string.Format("DELETE FROM Songs WHERE song_id = {0};", songID);

            MySqlCommand cmd = new MySqlCommand(sqlStr, conn);

            if (cmd.ExecuteNonQuery() == 0)

            {

                conn.Close();

                return -2; // no such song

            }

            conn.Close();

            return 1;// 1 means everything is right



        }

        public static int updateSongName(string songName, int songId)

        {

            MySqlConnection conn = openConn();



            if (conn == null)

                return -1;

            string sqlStr = string.Format("UPDATE Songs SET song_name = '{0}' WHERE song_id = '{1}' ", songName, songId);

            MySqlCommand cmd = new MySqlCommand(sqlStr, conn);

            try
            {
                cmd.ExecuteNonQuery();
            }catch(Exception e)
            {

                return -2; // error

            }

            return 1;

        }

        public static int getAllSongs(ref ArrayList songs)
        {
            MySqlConnection conn = openConn();
            if(conn == null)
            {
                return -1;
            }
            string sqlStr = string.Format("SELECT * FROM Songs");
            MySqlCommand cmd = new MySqlCommand(sqlStr, conn);

            MySqlDataReader read = cmd.ExecuteReader();

            songs = new ArrayList();
            while (read.Read())
            {
                Song song = new Song();
                song.Song_id = read.GetInt32("song_id");
                song.Song_name = read.GetString("song_name");
                song.Song_path = read.GetString("song_path");
                song.Publish_date = read.GetDateTime("publish_date");
                songs.Add(song);
            }
            read.Close();
            conn.Close();
            return 1;
        }

        public static int getSongById(ref Song song, int ID)
        {
            MySqlConnection conn = openConn();
            if (conn == null)
            {
                return -1;
            }
            string sqlStr = string.Format("SELECT * FROM Songs WHERE song_id = '{0}'", ID);
            MySqlCommand cmd = new MySqlCommand(sqlStr, conn);

            MySqlDataReader read = cmd.ExecuteReader();

            if (!read.Read())
            {

                return -2;    // no such song
            }
            else
            {
                song.Song_id = read.GetInt32("song_id");
                song.Song_name = read.GetString("song_name");
                song.Song_path = read.GetString("song_path");
                song.Publish_date = read.GetDateTime("publish_date");
            }
            read.Close();
            conn.Close();
            return 1;
        }

        public static int getSongsByName(ref ArrayList songs, string songName)
        {
            MySqlConnection conn = openConn();
            if (conn == null)
            {
                return -1;
            }
            string sqlStr = string.Format("SELECT * FROM Songs WHERE song_name like '%{0}%'", songName);
            MySqlCommand cmd = new MySqlCommand(sqlStr, conn);

            MySqlDataReader read = cmd.ExecuteReader();

            songs = new ArrayList();
            while (read.Read())
            {
                Song song = new Song();
                song.Song_id = read.GetInt32("song_id");
                song.Song_name = read.GetString("song_name");
                song.Song_path = read.GetString("song_path");
                song.Publish_date = read.GetDateTime("publish_date");
                songs.Add(song);
            }
            read.Close();
            conn.Close();
            return 1;
        }
    }
}
