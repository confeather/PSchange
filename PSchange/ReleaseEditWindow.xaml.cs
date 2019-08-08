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
    /// ReleaseEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReleaseEditWindow : Window
    {
        public string storageID = "";
        public string gameID = "";
        public string userID = "";
        public bool changeGameIsLoaded = false;
        public ReleaseEditWindow()
        {
            InitializeComponent();
        }

        private void confirmBtn_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem typeItem = type.SelectedItem as ComboBoxItem;
            string typeContent = typeItem.Content.ToString();
            AccessHelper.UpdateGameStorageInfo(storageID, typeContent, gameID, userID, price.Text, changeGame.SelectedItem.ToString(), message.Text);
            MessageBox.Show("修改完成！", "message", MessageBoxButton.OK);

            

            EventCenter.Broadcast<string>(EventType.UpdateUserRelease, storageID);
            this.Close();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            type.IsEnabled = false;
            changeGame.IsEnabled = false;
            message.IsEnabled = false;
            price.IsEnabled = false;

            editBtn.IsEnabled = true;
            confirmBtn.IsEnabled = false;
            cancelBtn.IsEnabled = false;

            string storageType = AccessHelper.GetGameStorageType(storageID);
            if (storageType == "出售")
            {
                type.SelectedIndex = 0;
            }
            else if (storageType == "出租")
            {
                type.SelectedIndex = 1;
            }
            else if (storageType == "交换")
            {
                type.SelectedIndex = 2;
            }

            price.Text = AccessHelper.GetGameStoragePrice(storageID);
            changeGame.SelectedItem = AccessHelper.GetGameStorageChangeGame(storageID);
            message.Text = AccessHelper.GetGameStorageMessage(storageID);
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            type.IsEnabled = true;
            changeGame.IsEnabled = true;
            message.IsEnabled = true;
            price.IsEnabled = true;

            editBtn.IsEnabled = false;
            confirmBtn.IsEnabled = true;
            cancelBtn.IsEnabled = true;
        }

        private void type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = type.SelectedItem as ComboBoxItem;
            string content = item.Content.ToString();
            if (content == "出售")
            {
                
                if (changeGameIsLoaded)
                {
                    price.Visibility = Visibility.Visible;
                    unit.Visibility = Visibility.Visible;
                    changeGame.Visibility = Visibility.Hidden;
                    unit.Content = "元";
                }

                
            }
            else if (content == "出租")
            {
                
                if (changeGameIsLoaded)
                {
                    price.Visibility = Visibility.Visible;
                    unit.Visibility = Visibility.Visible;
                    changeGame.Visibility = Visibility.Hidden;
                    unit.Content = "元/日";
                }

              
            }
            else if (content == "交换")
            {
                
                if (changeGameIsLoaded)
                {
                    
                    price.Visibility = Visibility.Hidden;
                    unit.Visibility = Visibility.Hidden;
                    changeGame.Visibility = Visibility.Visible;
                }

            }
        }

        private void changeGame_Loaded(object sender, RoutedEventArgs e)
        {
            int gameCount = AccessHelper.GetGameCount();
            for (int i = 0; i < gameCount; i++)
            {
                changeGame.Items.Add(AccessHelper.GetGameZhName(i));
            }
            changeGame.Visibility = Visibility.Hidden;
            changeGameIsLoaded = true;
        }
    }
}
