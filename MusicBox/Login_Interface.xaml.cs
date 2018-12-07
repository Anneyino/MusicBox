using MySql.Data.MySqlClient;
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
    /// Login_Interface.xaml 的交互逻辑
    /// </summary>
    public partial class Login_Interface : Window
    {
        public static MainWindow mainwindow;
        public Login_Interface()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string  username = UserNameBox.Text;
            string passwrd = passwrdBox.Password;
            if ((username != "") && (passwrd != ""))
            {
                DatabaseUtility.setUser(username, passwrd);
                MySqlConnection testCon = DatabaseUtility.openConn();
                if (testCon == null)
                {   
                    MessageBox.Show("用户名或密码错误，或者本机尚未导入Music数据库");
                }
                else
                {
                    testCon.Close();
                    mainwindow = new MainWindow();
                    mainwindow.Show();
                    this.Hide();
                }

            }
            else
            {
                MessageBox.Show("用户名密码不能为空!");
            }
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Help_Interface help_Interface = new Help_Interface();
            help_Interface.ShowDialog();
        }
    }
}
