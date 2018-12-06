using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MusicBox
{
    /// <summary>
    /// Search_Interface.xaml 的交互逻辑
    /// </summary>
    public partial class Search_Interface : Window
    {
        public string contents;
        public Search_Interface(string searchContext)
        {   
            InitializeComponent();
            contents = searchContext;
            reloadDatabase();
        }

        private void reloadDatabase()
        {
            ArrayList songList = new ArrayList();
            int state = DatabaseUtility.getSongsByName(ref songList, contents);
            DataTable dt = new DataTable();
            dt.Columns.Add("song_id");
            dt.Columns.Add("song_name");
            dt.Columns.Add("song_path");
            dt.Columns.Add("publish_date");
            foreach (Song s in songList)
            {
                string datess = s.Publish_date.ToShortDateString().ToString();
                dt.Rows.Add(s.Song_id, s.Song_name,s.Song_path,datess);

            }
            SongListView.DataContext = dt;
        }

       

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongListView.SelectedItem != null)
            {
                string ss = "";
                DataRowView drv = SongListView.SelectedItem as DataRowView;
                ss = drv["song_id"].ToString();
                int ID = int.Parse(ss);
                Song selectedSong = new Song();
                int state = DatabaseUtility.getSongById(ref selectedSong, ID);
                if (state == -1)
                {
                    MessageBox.Show("无法连接到数据库");
                }else if(state == -2)
                {
                    MessageBox.Show("无法找到相关歌曲");
                }
                else
                {
                    Login_Interface.mainwindow.updateSong(selectedSong.Song_path);
                    Login_Interface.mainwindow.SetCurrentSong(selectedSong);
                    this.Close();
                }
            }
            
        }

        private void AddToDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            Add_Interface addInterface = new Add_Interface();
            addInterface.ShowDialog();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongListView.SelectedItem != null)
            {
                string ss = "";
                DataRowView drv = SongListView.SelectedItem as DataRowView;
                ss = drv["song_id"].ToString();
                int ID = int.Parse(ss);
                int state = DatabaseUtility.removeSong(ID);
                if (state == -1)
                {
                    MessageBox.Show("无法连接到数据库!");
                }
                else if(state == -2)
                {
                    MessageBox.Show("无法删除改项!");
                }
                reloadDatabase();
            }
            else
            {
                MessageBox.Show("请选中一个数据库项来删除！");
            }
        }

        private void ModifyNameButton_Click(object sender, RoutedEventArgs e)
        {
            string ss = "";
            DataRowView drv = SongListView.SelectedItem as DataRowView;
            ss = drv["song_id"].ToString();
            int ID = int.Parse(ss);
            Modify_Interface modify_Interface = new Modify_Interface(ID);
            modify_Interface.ShowDialog();
        }
    }
}
