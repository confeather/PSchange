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
    /// GameListWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GameListWindow : Window
    {
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        string gameID = "1";
        int gameIndex = 0;
        int gameCount = 0;
        bool isAdding = false;
        bool isEditing = false;

        public GameListWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG文件(*.jpg)|*.jpg|所有文件(*.*)|*.*";

            UpdateWindow();
        }

        private void updatePicBtn_Click(object sender, RoutedEventArgs e)
        {
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string str1 = dlg.FileNames[0];
                AccessHelper.SaveGameImage(gameID, str1);
                UpdateWindow();
            }
        }

        private void UpdateWindow()
        {
            ZhName.IsEnabled = false;
            EnName.IsEnabled = false;
            ZhName.Text = AccessHelper.GetGameZhName(gameIndex);
            EnName.Text = AccessHelper.GetGameEnName(gameIndex);
            gameID = AccessHelper.GetGameID(gameIndex);
            Pic.Source = AccessHelper.GetGameImage(gameID);

            gameCount = AccessHelper.GetGameCount();
        }

        private void confirmBtn_Click(object sender, RoutedEventArgs e)
        {
            

            if (ZhName.Text == "" || EnName.Text == "")
            {
                MessageBox.Show("信息不完全!", "message", MessageBoxButton.OK);
            }
            else if (isEditing)
            {
                ZhName.IsEnabled = false;
                EnName.IsEnabled = false;
                addGameBtn.IsEnabled = true;
                updateGameBtn.IsEnabled = true;
                deleteGameBtn.IsEnabled = true;
                confirmBtn.IsEnabled = false;
                cancelBtn.IsEnabled = false;

                AccessHelper.UpdateGameInfo(gameID, ZhName.Text, EnName.Text);
                isEditing = false;
                BrowseBtnsControl(true);
            }
            else if (isAdding)
            {
                ZhName.IsEnabled = false;
                EnName.IsEnabled = false;
                addGameBtn.IsEnabled = true;
                updateGameBtn.IsEnabled = true;
                deleteGameBtn.IsEnabled = true;
                confirmBtn.IsEnabled = false;
                cancelBtn.IsEnabled = false;
                updatePicBtn.IsEnabled = true;

                AccessHelper.AddGameInfo(ZhName.Text, EnName.Text);
                gameCount = AccessHelper.GetGameCount();
                gameIndex = gameCount - 1;

                UpdateWindow();
                isAdding = false;
                BrowseBtnsControl(true);
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            ZhName.IsEnabled = false;
            EnName.IsEnabled = false;
            addGameBtn.IsEnabled = true;
            updateGameBtn.IsEnabled = true;
            deleteGameBtn.IsEnabled = true;
            confirmBtn.IsEnabled = false;
            cancelBtn.IsEnabled = false;
            updatePicBtn.IsEnabled = true;

            UpdateWindow();
            isAdding = false;
            isEditing = false;
            BrowseBtnsControl(true);
        }

        private void updateGameBtn_Click(object sender, RoutedEventArgs e)
        {
            ZhName.IsEnabled = true;
            EnName.IsEnabled = true;
            addGameBtn.IsEnabled = false;
            updateGameBtn.IsEnabled = false;
            deleteGameBtn.IsEnabled = false;
            confirmBtn.IsEnabled = true;
            cancelBtn.IsEnabled = true;

            isEditing = true;
            BrowseBtnsControl(false);
        }

        private void addGameBtn_Click(object sender, RoutedEventArgs e)
        {
            ZhName.IsEnabled = true;
            EnName.IsEnabled = true;
            addGameBtn.IsEnabled = false;
            updateGameBtn.IsEnabled = false;
            deleteGameBtn.IsEnabled = false;
            confirmBtn.IsEnabled = true;
            cancelBtn.IsEnabled = true;

            isAdding = true;
            ZhName.Text = "";
            EnName.Text = "";
            Pic.Source = null;
            updatePicBtn.IsEnabled = false;
            BrowseBtnsControl(false);
        }

        private void deleteGameBtn_Click(object sender, RoutedEventArgs e)
        {
            AccessHelper.DeleteGameInfo(gameID);
            gameIndex = 0;
            UpdateWindow();
            MessageBox.Show("已删除!", "message", MessageBoxButton.OK);
        }

        private void firstOneBtn_Click(object sender, RoutedEventArgs e)
        {
            gameIndex = 0;
            UpdateWindow();
        }

        private void lastOneBtn_Click(object sender, RoutedEventArgs e)
        {
            gameIndex = gameCount - 1;
            UpdateWindow();
        }

        private void beforeOneBtn_Click(object sender, RoutedEventArgs e)
        {
            if (gameIndex<=0)
            {
                MessageBox.Show("已是第一项!", "message", MessageBoxButton.OK);
            }
            else
            {
                gameIndex--;
                UpdateWindow();
            }
        }

        private void nextOneBtn_Click(object sender, RoutedEventArgs e)
        {
            if (gameIndex>=gameCount-1)
            {
                MessageBox.Show("已是最后一项!", "message", MessageBoxButton.OK);
            }
            else
            {
                gameIndex++;
                UpdateWindow();
            }
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            //ZhName.Text = AccessHelper.GetSearchGameZhName(searchText.Text); 

            gameIndex = AccessHelper.GetSearchGameIndex(searchText.Text);
            UpdateWindow();

        }

        private void BrowseBtnsControl(bool active)
        {
            firstOneBtn.IsEnabled = active;
            lastOneBtn.IsEnabled = active;
            beforeOneBtn.IsEnabled = active;
            nextOneBtn.IsEnabled = active;
            searchBtn.IsEnabled = active;
        }


    }
}
