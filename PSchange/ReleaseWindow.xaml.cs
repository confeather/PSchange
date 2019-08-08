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
    /// ReleaseWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReleaseWindow : Window
    {

        private bool changeGameIsLoaded = false;
        public string gameID = "";
        public string userID = "";

        public ReleaseWindow()
        {
            InitializeComponent();
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

        private void type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = type.SelectedItem as ComboBoxItem;  
            string content = item.Content.ToString();
            if (content == "出售")
            {
                price.Visibility = Visibility.Visible;
                unit.Visibility = Visibility.Visible;
                if (changeGameIsLoaded)
                {
                    changeGame.Visibility = Visibility.Hidden;
                }

                unit.Content = "元";
            }
            else if (content == "出租")
            {
                price.Visibility = Visibility.Visible;
                unit.Visibility = Visibility.Visible;
                if (changeGameIsLoaded)
                {
                    changeGame.Visibility = Visibility.Hidden;
                }

                unit.Content = "元/日";
            }
            else if (content == "交换")
            {
                price.Visibility = Visibility.Hidden;
                unit.Visibility = Visibility.Hidden;
                if (changeGameIsLoaded)
                {
                    changeGame.Visibility = Visibility.Visible;
                }

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //int gameCount = AccessHelper.GetGameCount();
            //for (int i = 0; i < gameCount; i++)
            //{
            //    changeGame.Items.Add(AccessHelper.GetGameZhName(i));
            //}
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void storageBtn_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem typeItem = type.SelectedItem as ComboBoxItem;
            string typeContent = typeItem.Content.ToString();


            //ComboBoxItem changeItem = changeGame.SelectedItem as ComboBoxItem;
            //string changeContent = changeItem.Content.ToString();
            //Console.WriteLine(changeContent);
            AccessHelper.AddGameStorage(typeContent, gameID, userID, price.Text, changeGame.SelectedItem.ToString(), message.Text);
            MessageBox.Show("发布成功！", "message", MessageBoxButton.OK);
            this.Close();
            EventCenter.Broadcast<string>(EventType.UpdateGameList,gameID);
        }

        private void changeGame_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ComboBoxItem changeItem = changeGame.Items.CurrentItem as ComboBoxItem;

            //if (changeGameIsLoaded)
            //{
            //    //string changeContent = changeItem.Content.ToString();
            //    //Console.WriteLine(changeContent);
            //    Console.WriteLine(changeGame.SelectedIndex);
            //}
        
           
        }
    }
}
