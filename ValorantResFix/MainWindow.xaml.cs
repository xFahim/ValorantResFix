using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ValorantResFix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StartUP();
        }

        private void StartUP()
        {
            List<string> SettingsFileList = new List<string>();

            string dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); //path to appdata
            dir += "\\VALORANT\\Saved\\Config";


            if (Directory.Exists(dir))
            {
                string[] users = Directory.GetDirectories(dir);

                foreach (var u in users)
                {
                    if (File.Exists(u + "\\Windows\\GameUserSettings.ini"))
                    {
                        SettingsFileList.Add(u + "\\Windows\\GameUserSettings.ini");
                    }
                }

                if (SettingsFileList.Count < 1) // if No GameSettings File found
                {
                    MyBaseFrame.Navigate(new ErrorPage());
                }
                else
                {
                    MyBaseFrame.Navigate(new Home(SettingsFileList));
                }
            }
            else // if Valorant folder is not found in AppData Dir
            {
                MyBaseFrame.Navigate(new ErrorPage());
            }
        }


        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
