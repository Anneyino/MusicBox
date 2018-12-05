using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Add_Interface.xaml 的交互逻辑
    /// </summary>
    public partial class Add_Interface : Window
    {
        private string songpath="";
        private Song tempsong = new Song();
        private Regex reg = new Regex(@"^(?:(?!0000)[0-9]{4}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)$");

        public Add_Interface()
        {
            InitializeComponent();
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "C:\\";
            fileDialog.Filter = "MP3音乐文件|*.mp3|WAV音乐文件|*.wav";
            fileDialog.Title = "选择音乐文件";
            if (fileDialog.ShowDialog() == true)
            {
                songpath = System.IO.Path.GetFullPath(fileDialog.FileName);
            }
            if (File.Exists(songpath))
            {
                PathTextBox.Text = songpath;
                string songTitle = System.IO.Path.GetFileNameWithoutExtension(songpath);
                tempsong.Song_name = songTitle;
                tempsong.Song_path = songpath;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string Datestring = PublishDateBox.Text;
            if (songpath == "")
            {
                MessageBox.Show("请打开一个音乐文件");
            }
            else if (!reg.IsMatch(Datestring))
            {
                MessageBox.Show("请输入合法的日期格式");
            }
            else
            {
                DateTime dt = Convert.ToDateTime(Datestring);
                tempsong.Publish_date = dt;
                int state = DatabaseUtility.AddNewSong(ref tempsong);
                if (state == -1)
                {
                    MessageBox.Show("无法连接到数据库");
                }else if(state == -2)
                {
                    MessageBox.Show("试图添加重复的歌曲名!");
                }
            }
        }
    }
}
