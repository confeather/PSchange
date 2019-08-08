using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace PSchange
{
    class MenuCenter
    {
        public static void CreateGameInfo(Grid grid,int row)
        {
            
            Canvas canvas = new Canvas();
            Canvas.SetTop(canvas, row * 200);

            Image img = new Image();
            Canvas.SetTop(img, row * 200);

            string gameID = AccessHelper.GetGameID(row);
            canvas.Name = "canvas" + gameID;

            img.Width = 186;
            img.Height = 186;
            img.Source = AccessHelper.GetGameImage(gameID);
            canvas.Children.Add(img);

            Label zh = new Label();
            Canvas.SetTop(zh, row * 200);
            Canvas.SetLeft(zh, 200);
            zh.Width = 600;
            zh.Height = 50;
            zh.FontSize = 20;
            zh.Content = AccessHelper.GetGameZhName(row);
            canvas.Children.Add(zh);

            Label en = new Label();
            Canvas.SetTop(en, row * 200 + 50);
            Canvas.SetLeft(en, 200);
            en.Width = 600;
            en.Height = 50;
            en.FontSize = 16;
            en.Content = AccessHelper.GetGameEnName(row);
            canvas.Children.Add(en);

            Label storage = new Label();
            Canvas.SetTop(storage, row * 200 + 100);
            Canvas.SetLeft(storage, 200);
            storage.Width = 600;
            storage.Height = 50;
            storage.FontSize = 16;
            storage.Content = AccessHelper.GetGameStorage(gameID) + "条交易信息";

            storage.Name = "storage" + gameID;
            canvas.Children.Add(storage);

            Button detail = new Button();
            Canvas.SetTop(detail, row * 200 + 150);
            Canvas.SetLeft(detail, 200);
            detail.Width = 100;
            detail.Height = 30;
            detail.Content = "查看详情";
            detail.Click += DetailBtn_Click;
            detail.Name = "detail" + gameID;
            detail.Background = Brushes.White;
            canvas.Children.Add(detail);

            Button create = new Button();
            Canvas.SetTop(create, row * 200 + 150);
            Canvas.SetLeft(create, 350);
            create.Width = 100;
            create.Height = 30;
            create.Content = "发布二手";
            create.Name = "create" + gameID;
            create.Click += CreateBtn_Click;
            create.Background = Brushes.White;
            canvas.Children.Add(create);


            grid.Children.Add(canvas);
            grid.Height += 200;
        }

        private static void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string gameId = btn.Name.Replace("create", "");
            EventCenter.Broadcast<string>(EventType.ReleaseInfo, gameId);
            //throw new NotImplementedException();
            //EventCenter.Broadcast(EventType.)
        }

        private static void DetailBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string gameId = btn.Name.Replace("detail", "");
            EventCenter.Broadcast<string>(EventType.ViewDetail, gameId);

            //Console.WriteLine(gameId);
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 删除首页游戏信息
        /// </summary>
        /// <param name="grid"></param>
        public static void DeleteGameInfo(Grid grid)
        {
            int childrenCount = grid.Children.Count;
            for (int i = 0; i < childrenCount; i++)
            {
                grid.Children.RemoveAt(i);
                grid.Height -= 200;
            }
        }
        /// <summary>
        /// 产生对应游戏的所有交易信息
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="index"></param>
        /// <param name="gameid"></param>
        /// <param name="userid"></param>
        public static void CreateStorageInfo(Grid grid,int index,string gameid)
        {
            Canvas canvas = new Canvas();
            Canvas.SetTop(canvas, index * 50);

            Label info = new Label();
            Canvas.SetTop(info, index * 50);
            //Canvas.SetLeft(info, 2+64);
            info.Width = 350;
            info.Height = 50;
            info.FontSize = 16;
            string userid = AccessHelper.GetGameStorageUserID(gameid, index);
            if (AccessHelper.GetGameStorageType(gameid,index) == "交换")
            {
                info.Content = "用户 " + AccessHelper.GetUserNickName(userid) + " 想要用此游戏交换《" + AccessHelper.GetGameStorageChangeGame(gameid, index) + "》";

            }
            else if (AccessHelper.GetGameStorageType(gameid, index) == "出售")
            {
                info.Content = "用户 " + AccessHelper.GetUserNickName(userid) + " 想要以" + AccessHelper.GetGameStoragePrice(gameid, index) + "元 出售此游戏";

            }
            else if (AccessHelper.GetGameStorageType(gameid, index) == "出租")
            {
                info.Content = "用户 " + AccessHelper.GetUserNickName(userid) + " 想要以" + AccessHelper.GetGameStoragePrice(gameid, index) + "元/日 出租此游戏";
            }

            canvas.Children.Add(info);

            Button detail = new Button();
            Canvas.SetTop(detail, index * 50 + 5);
            Canvas.SetLeft(detail, 350);
            detail.Width = 100;
            detail.Height = 30;
            detail.FontSize = 16;
            detail.Content = "查看详情";
            detail.Background = Brushes.White;
            detail.Click += MessageDetail_Click;
            string storageid = AccessHelper.GetGameStorageID(gameid, index);
            detail.Name = "detail" + storageid;
            canvas.Children.Add(detail);

           

            grid.Children.Add(canvas);
            grid.Height += 50;
        }

        private static void MessageDetail_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string storageid = btn.Name.Replace("detail", "");
            EventCenter.Broadcast<string>(EventType.ViewMessage, storageid);
            //throw new NotImplementedException();
        }
        /// <summary>
        /// 创建“已发布”下对应用户交易信息
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="index"></param>
        /// <param name="userid"></param>
        public static void CreateUserReleaseInfo(Grid grid, int index,string userid)
        {

            Canvas canvas = new Canvas();
            Canvas.SetTop(canvas, index * 50);

            Label info = new Label();
            Canvas.SetTop(info, index * 50);
            //Canvas.SetLeft(info, 2+64);
            info.Width = 500;
            info.Height = 50;
            info.FontSize = 16;

            

            string storageID = AccessHelper.GetGameStorageIDFromUserID(userid,index);
            string gameid = AccessHelper.GetGameStorageGameIDFromUserID(userid, index);

            info.Name = "info" + storageID;

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

            canvas.Children.Add(info);

            Button edit = new Button();
            Canvas.SetTop(edit, index * 50 + 5);
            Canvas.SetLeft(edit, 500);
            edit.Width = 100;
            edit.Height = 30;
            edit.FontSize = 16;
            edit.Content = "修改信息";
            edit.Background = Brushes.White;
            edit.Click += EditRelease_Click;
            edit.Name = "edit" + storageID;
            canvas.Children.Add(edit);

            Button delete = new Button();
            Canvas.SetTop(delete, index * 50 + 5);
            Canvas.SetLeft(delete, 600 + 5);
            delete.Width = 100;
            delete.Height = 30;
            delete.FontSize = 16;
            delete.Content = "删除此项";
            //delete.Foreground = Brushes.White;
            delete.Click += DeleteRelease_Click;
            delete.Name = "delete" + storageID;

            canvas.Children.Add(delete);
            canvas.Name = "canvas" + storageID;

            grid.Children.Add(canvas);
            grid.Height += 50;
        }

        private static void DeleteRelease_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string storageid = btn.Name.Replace("delete", "");
            //string userid = AccessHelper.GetGameStorageUserID(storageid);
            string gameid = AccessHelper.GetGameStorageGameID(storageid);
            MessageBoxResult dr = MessageBox.Show("真的要删除此信息吗?", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                AccessHelper.DeleteGameStorageInfo(storageid);

                EventCenter.Broadcast<string>(EventType.DeleteOneUserRelease,storageid);
                EventCenter.Broadcast<string>(EventType.UpdateGameList, gameid);

                
                MessageBox.Show("已删除！", "message", MessageBoxButton.OK);
        
            }

            //throw new NotImplementedException();
        }

        private static void EditRelease_Click(object sender, RoutedEventArgs e)
        {

            Button btn = sender as Button;
            string storageid = btn.Name.Replace("edit", "");
            EventCenter.Broadcast<string>(EventType.EditRelease, storageid);
            //throw new NotImplementedException();
        }
    }
}
