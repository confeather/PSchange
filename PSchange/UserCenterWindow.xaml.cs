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
    /// UserCenterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserCenterWindow : Window
    {
        public string userID = "";
        public UserCenterWindow()
        {
            InitializeComponent();
        }

        private void confirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (userNickName.Text == "" || userPasswd.Text == "")
            {
                MessageBox.Show("密码、昵称不可为空！", "message", MessageBoxButton.OK);
            }
            else
            {
                updateBtn.IsEnabled = true;
                confirmBtn.IsEnabled = false;
                cancelBtn.IsEnabled = false;

                userPasswd.IsEnabled = false;
                userNickName.IsEnabled = false;
                userPhone.IsEnabled = false;
                userQQ.IsEnabled = false;
                userEmail.IsEnabled = false;
                userAddress.IsEnabled = false;

                AccessHelper.UpdateUser(userID, userPasswd.Text, userNickName.Text, userPhone.Text, userQQ.Text, userEmail.Text, userAddress.Text);
                EventCenter.Broadcast<string>(EventType.UpdateNickname, userNickName.Text);
                MessageBox.Show("修改完成！", "message", MessageBoxButton.OK);
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            updateBtn.IsEnabled = false;
            confirmBtn.IsEnabled = true;
            cancelBtn.IsEnabled = true;

            userPasswd.IsEnabled = true;
            userNickName.IsEnabled = true;
            userPhone.IsEnabled = true;
            userQQ.IsEnabled = true;
            userEmail.IsEnabled = true;
            userAddress.IsEnabled = true;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            updateBtn.IsEnabled = true;
            confirmBtn.IsEnabled = false;
            cancelBtn.IsEnabled = false;

            userPasswd.IsEnabled = false;
            userNickName.IsEnabled = false;
            userPhone.IsEnabled = false;
            userQQ.IsEnabled = false;
            userEmail.IsEnabled = false;
            userAddress.IsEnabled = false;

            userPasswd.Text = AccessHelper.GetUserPasswd(userID);
            userNickName.Text = AccessHelper.GetUserNickName(userID);
            userPhone.Text = AccessHelper.GetUserPhone(userID);
            userQQ.Text = AccessHelper.GetUserQQ(userID);
            userEmail.Text = AccessHelper.GetUserEmail(userID);
            userAddress.Text = AccessHelper.GetUserAddress(userID);
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            EventCenter.Broadcast(EventType.Logout);
        }
    }
}
