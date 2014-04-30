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
using System.Windows.Shapes;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace WK_Calculator
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class WindowStartup : Window
    {
        bool error = false;

        WindowMainData wMd;
        WindowUserData wUd;
        WindowPoints wP;

        string dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WK Downloader";

        public WindowStartup()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Gebruikers aanmaken
            var files = new DirectoryInfo(dataFolder + @"/Spelers").GetFiles();
            foreach (var file in files)
                Data.Users.Add(new User() { Name = file.Name.Replace(".xls","") });

            // Read Scores
            XLSScores.Init();
            XLSScores.ReadXLS();

            // Read user XLS
            foreach (var user in Data.Users)
            {
                XLSUser.Init(user);
                XLSUser.ReadXLS();
            }
           
        }

        private void btnMainData_Click(object sender, RoutedEventArgs e)
        {
            wMd = new WindowMainData();

            wMd.lbSchema.DataContext = Data.SpeelSchema.Groups;

            this.Hide();
            wMd.ShowDialog();
            this.Show();
        }

        private void btnEnterPlayerData_Click(object sender, RoutedEventArgs e)
        {
            wUd = new WindowUserData();

            wUd.lbUsers.DataContext = Data.Users;

            this.Hide();
            wUd.ShowDialog();
            this.Show();
        }

        private void btnShowPoints_Click(object sender, RoutedEventArgs e)
        {
            wP = new WindowPoints();
            //this.Hide();
            wP.ShowDialog();
            //this.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (error == false)
            {
                XLSScores.WriteXLS();
            }
        }
    }
}
