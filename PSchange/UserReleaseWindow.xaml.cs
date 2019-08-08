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
    /// UserReleaseWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserReleaseWindow : Window
    {
        public UserReleaseWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EventCenter.AddListener<string>(EventType.UpdateUserRelease, UpdateReleaseList);
            EventCenter.AddListener<string>(EventType.DeleteOneUserRelease, UpdateDeleteReleaseList);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            EventCenter.RemoveListener<string>(EventType.UpdateUserRelease, UpdateReleaseList);
            EventCenter.RemoveListener<string>(EventType.DeleteOneUserRelease, UpdateDeleteReleaseList);

        }
        private void UpdateDeleteReleaseList(string storageID)
        {
            list.Children.Remove(UIFindHelper.GetChildObject<Canvas>(list, "canvas" + storageID));
            //list.Height -= 50;
            //for (int i = 0; i < list.Children.Count; i++)
            //{
            //    list.Children.RemoveAt(i);
            //    list.Height -= 50;
            //}

            //int count = AccessHelper.GetGameStorageCountFromUserID(userID);
            //for (int i = 0; i < count; i++)
            //{
            //    MenuCenter.CreateUserReleaseInfo(list, i, userID);
            //}
        }

        private void UpdateReleaseList(string storageID)
        {
            //Console.WriteLine(list.Children.Count);
            //Canvas canvas = UIFindHelper.GetChildObject<Canvas>(list, "canvas" + storageID);
            Label info = UIFindHelper.GetChildObject<Label>(list, "info" + storageID);
            //label.Content = AccessHelper.GetGameStorage(gameid) + "条交易信息";

            string userid = AccessHelper.GetGameStorageUserID(storageID);
            string gameid = AccessHelper.GetGameStorageGameID(storageID);

            if (AccessHelper.GetGameStorageType(storageID) == "交换")
            {
                info.Content = "用户 " + AccessHelper.GetUserNickName(userid) + " 想要用《" + AccessHelper.GetGameZhName(gameid) + "》交换《" + AccessHelper.GetGameStorageChangeGame(storageID) + "》";

            }
            else if (AccessHelper.GetGameStorageType(storageID) == "出售")
            {
                info.Content = "用户 " + AccessHelper.GetUserNickName(userid) + " 想要以" + AccessHelper.GetGameStoragePrice(storageID) + "元 出售《" + AccessHelper.GetGameZhName(gameid) + "》";

            }
            else if (AccessHelper.GetGameStorageType(storageID) == "出租")
            {
                info.Content = "用户 " + AccessHelper.GetUserNickName(userid) + " 想要以" + AccessHelper.GetGameStoragePrice(storageID) + "元/日 出租《" + AccessHelper.GetGameZhName(gameid) + "》";
            }

            
        }

       
    }
}
