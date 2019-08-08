using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PSchange
{
    /// <summary>
    /// RegistWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RegistWindow : Window
    {
        public RegistWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void registBtn_Click(object sender, RoutedEventArgs e)
        {
            if (userName.Text == ""||userPasswd.Text==""||userNickName.Text=="")
            {
                MessageBox.Show("用户名、密码、昵称不可为空！", "message", MessageBoxButton.OK);
            }
            else
            {
                AccessHelper.RegistUser(userName.Text, userPasswd.Text, userNickName.Text, userPhone.Text, userQQ.Text, userEmail.Text, userAddress.Text);
                this.Close();
            }
        }
    }
}
