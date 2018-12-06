using System;
using System.Collections.Generic;
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
    /// Modify_Interface.xaml 的交互逻辑
    /// </summary>
    public partial class Modify_Interface : Window
    {
        int songid = 0;
        public Modify_Interface(int id)
        {
            InitializeComponent();
            songid = id;
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewNameBox.Text != "")
            {
                string newname = NewNameBox.Text;
                int state = DatabaseUtility.updateSongName(newname, songid);
                if (state == -1)
                {
                    MessageBox.Show("无法连接到数据库");
                }
                else if (state == -2)
                {
                    MessageBox.Show("该歌曲名已存在");
                }
                else
                {
                    MessageBox.Show("修改成功!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("请输入新的歌曲名");
            }
        }
    }
}
