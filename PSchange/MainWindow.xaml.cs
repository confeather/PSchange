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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PSchange
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public string userID = "";
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="str"></param>
        /// <param name="id"></param>
        private void UserLogin(string str,string id)
        {
            nickName.Content = str;
            userID = id;
            registBtn.Visibility = Visibility.Hidden;
            loginBtn.Visibility = Visibility.Hidden;
            userCenterBtn.Visibility = Visibility.Visible;
            viewReleaseBtn.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// 用户登出
        /// </summary>
        private void UserLogout()
        {
            nickName.Content = "未登陆";
            userID = "";
            registBtn.Visibility = Visibility.Visible;
            loginBtn.Visibility = Visibility.Visible;
            userCenterBtn.Visibility = Visibility.Hidden;
            viewReleaseBtn.Visibility = Visibility.Hidden;
        }

        private void UpdateUserNickname(string str)
        {
            nickName.Content = str;
        }

        private void ViewGameStorageInfo(string gameid)
        {
            if (userID == "")
            {
                MessageBox.Show("未登录！", "message", MessageBoxButton.OK);
            }
            else
            {
                if (AccessHelper.GetGameStorage(gameid)=="0")
                {
                    MessageBox.Show("暂无库存！", "message", MessageBoxButton.OK);
                }
                else
                {
                    StorageWindow storageWindow = new StorageWindow();
                    storageWindow.Show();
                    storageWindow.ZhName.Content = AccessHelper.GetGameZhName(gameid);
                    storageWindow.EnName.Content = AccessHelper.GetGameEnName(gameid);
                    storageWindow.Pic.Source = AccessHelper.GetGameImage(gameid);

                    //storageWindow.gameId = gameid;
                    for (int i = 0; i < Convert.ToInt32(AccessHelper.GetGameStorage(gameid)); i++)
                    {
                        MenuCenter.CreateStorageInfo(storageWindow.list, i, gameid);
                    }
                }
            }
        }

        private void ReleaseGameStorageInfo(string gameid)
        {
            if (userID == "")
            {
                MessageBox.Show("未登录！", "message", MessageBoxButton.OK);
            }
            else
            {
                ReleaseWindow releaseWindow = new ReleaseWindow();
                releaseWindow.Show();
                releaseWindow.gameID = gameid;
                releaseWindow.userID = userID;
                releaseWindow.Pic.Source = AccessHelper.GetGameImage(gameid);
                releaseWindow.ZhName.Content = AccessHelper.GetGameZhName(gameid);
                releaseWindow.EnName.Content = AccessHelper.GetGameEnName(gameid);
            }

        }

        private void ViewStorageMessage(string storageID)
        {
            MessageWindow messageWindow = new MessageWindow();
            messageWindow.Show();
            string gameid = AccessHelper.GetGameStorageGameID(storageID);
            string userid = AccessHelper.GetGameStorageUserID(storageID);

            messageWindow.nickname.Content = AccessHelper.GetUserNickName(userid);
            messageWindow.phone.Content = AccessHelper.GetUserPhone(userid);
            messageWindow.qq.Content = AccessHelper.GetUserQQ(userid);
            messageWindow.email.Content = AccessHelper.GetUserEmail(userid);
            messageWindow.address.Text = AccessHelper.GetUserAddress(userid);
            if (AccessHelper.GetGameStorageType(storageID) == "交换")
            {
                messageWindow.storageInfo.Text = "用户 " + AccessHelper.GetUserNickName(userid) + " 想要用《" + AccessHelper.GetGameZhName(gameid) + "》交换《" + AccessHelper.GetGameStorageChangeGame(storageID) + "》";

            }
            else if (AccessHelper.GetGameStorageType(storageID) == "出售")
            {
                messageWindow.storageInfo.Text = "用户 " + AccessHelper.GetUserNickName(userid) + " 想要以" + AccessHelper.GetGameStoragePrice(storageID) + "元 出售《" + AccessHelper.GetGameZhName(gameid) + "》";

            }
            else if (AccessHelper.GetGameStorageType(storageID) == "出租")
            {
                messageWindow.storageInfo.Text = "用户 " + AccessHelper.GetUserNickName(userid) + " 想要以" + AccessHelper.GetGameStoragePrice(storageID) + "元/日 出租《" + AccessHelper.GetGameZhName(gameid) + "》";
            }

            messageWindow.Pic.Source = AccessHelper.GetGameImage(gameid);
            messageWindow.message.Text = AccessHelper.GetGameStorageMessage(storageID);
        }

        private void EditUserRelease(string storageid)
        {
            ReleaseEditWindow releaseEditWindow = new ReleaseEditWindow();
            releaseEditWindow.Show();

            releaseEditWindow.userID = userID;
            releaseEditWindow.storageID = storageid;

            string gameid = AccessHelper.GetGameStorageGameID(storageid);

            releaseEditWindow.gameID = gameid;

            releaseEditWindow.Pic.Source = AccessHelper.GetGameImage(gameid);
            releaseEditWindow.ZhName.Content = AccessHelper.GetGameZhName(gameid);
            releaseEditWindow.EnName.Content = AccessHelper.GetGameEnName(gameid);

            string storageType = AccessHelper.GetGameStorageType(storageid);

            if (storageType == "出售")
            {
                releaseEditWindow.type.SelectedIndex = 0;

                releaseEditWindow.changeGame.Visibility = Visibility.Hidden;
                releaseEditWindow.price.Visibility = Visibility.Visible;
                releaseEditWindow.unit.Visibility = Visibility.Visible;
                releaseEditWindow.price.Text = AccessHelper.GetGameStoragePrice(storageid);
                releaseEditWindow.unit.Content = "元";
            }
            else if (storageType == "出租")
            {
                releaseEditWindow.type.SelectedIndex = 1;

                releaseEditWindow.changeGame.Visibility = Visibility.Hidden;
                releaseEditWindow.price.Visibility = Visibility.Visible;
                releaseEditWindow.unit.Visibility = Visibility.Visible;
                releaseEditWindow.price.Text = AccessHelper.GetGameStoragePrice(storageid);
                releaseEditWindow.unit.Content = "元/日";
            }
            else if (storageType == "交换")
            {
                releaseEditWindow.type.SelectedIndex = 2;

                releaseEditWindow.changeGame.Visibility = Visibility.Visible;
                releaseEditWindow.price.Visibility = Visibility.Hidden;
                releaseEditWindow.unit.Visibility = Visibility.Hidden;

                releaseEditWindow.changeGame.SelectedItem = AccessHelper.GetGameStorageChangeGame(storageid);

            }

            releaseEditWindow.message.Text = AccessHelper.GetGameStorageMessage(storageid);

        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateGameInfo(string gameid)
        {
            Label label = UIFindHelper.GetChildObject<Label>(list, "storage" + gameid);
            label.Content = AccessHelper.GetGameStorage(gameid) + "条交易信息"; 
          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EventCenter.AddListener<string,string>(EventType.Login, UserLogin);
            EventCenter.AddListener(EventType.Logout, UserLogout);
            EventCenter.AddListener<string>(EventType.ViewDetail, ViewGameStorageInfo);
            EventCenter.AddListener<string>(EventType.ReleaseInfo, ReleaseGameStorageInfo);
            EventCenter.AddListener<string>(EventType.UpdateGameList, UpdateGameInfo);
            EventCenter.AddListener<string>(EventType.UpdateNickname, UpdateUserNickname);
            EventCenter.AddListener<string>(EventType.ViewMessage, ViewStorageMessage);
            EventCenter.AddListener<string>(EventType.EditRelease, EditUserRelease);

            userCenterBtn.Visibility = Visibility.Hidden;
            viewReleaseBtn.Visibility = Visibility.Hidden;

            int gameCount = AccessHelper.GetGameCount();
            for (int i = 0; i < gameCount; i++)
            {
                MenuCenter.CreateGameInfo(list, i);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            EventCenter.RemoveListener<string, string>(EventType.Login, UserLogin);
            EventCenter.RemoveListener(EventType.Logout, UserLogout);
            EventCenter.RemoveListener<string>(EventType.ViewDetail, ViewGameStorageInfo);
            EventCenter.RemoveListener<string>(EventType.ReleaseInfo, ReleaseGameStorageInfo);
            EventCenter.RemoveListener<string>(EventType.UpdateGameList, UpdateGameInfo);
            EventCenter.RemoveListener<string>(EventType.UpdateNickname, UpdateUserNickname);
            EventCenter.RemoveListener<string>(EventType.ViewMessage, ViewStorageMessage);
            EventCenter.RemoveListener<string>(EventType.EditRelease, EditUserRelease);
        }
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

     
        private void registBtn_Click(object sender, RoutedEventArgs e)
        {
            //nickName.Content = "a";
            RegistWindow registWindow = new RegistWindow();
            registWindow.Show();
        }

        private void userCenterBtn_Click(object sender, RoutedEventArgs e)
        {
            UserCenterWindow userCenterWindow = new UserCenterWindow();
            userCenterWindow.Show();
            userCenterWindow.userID = userID;

            userCenterWindow.userPasswd.Text = AccessHelper.GetUserPasswd(userID);
            userCenterWindow.userNickName.Text = AccessHelper.GetUserNickName(userID);
            userCenterWindow.userPhone.Text = AccessHelper.GetUserPhone(userID);
            userCenterWindow.userQQ.Text = AccessHelper.GetUserQQ(userID);
            userCenterWindow.userEmail.Text = AccessHelper.GetUserEmail(userID);
            userCenterWindow.userAddress.Text = AccessHelper.GetUserAddress(userID);
        }

        private void viewReleaseBtn_Click(object sender, RoutedEventArgs e)
        {
            UserReleaseWindow userReleaseWindow = new UserReleaseWindow();
            //userReleaseWindow.list = new Grid();
            userReleaseWindow.Show();

            userReleaseWindow.nickName.Content = nickName.Content;

            int count = AccessHelper.GetGameStorageCountFromUserID(userID);
            for (int i = 0; i < count; i++)
            {
                MenuCenter.CreateUserReleaseInfo(userReleaseWindow.list, i, userID);
            }
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            // 获取要定位之前 ScrollViewer 目前的滚动位置
            //var currentScrollPosition = ScrollViewer.VerticalOffset;
            //var point = new Point(0, currentScrollPosition);

            //// 计算出目标位置并滚动
            //var targetPosition = TargetControl.TransformToVisual(ScrollViewer).Transform(point);
            //ScrollViewer.ScrollToVerticalOffset(targetPosition.Y);

            //Canvas canvas = UIFindHelper.GetChildObject<Canvas>(list, "canvas9");
            int index = AccessHelper.GetSearchGameIndex(searchText.Text);
            scrollviewer1.ScrollToVerticalOffset(200 * index);
        }
    }
}
