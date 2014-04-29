using System;
using System.Collections.Generic;
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

namespace WK_Calculator
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class WindowStartup : Window
    {
        WindowMainData wMd;
        WindowUserData wUd;

        public WindowStartup()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Data.users.Add(new User() { Name = "Cedric"});
            Data.users.Add(new User() { Name = "Patrik" });
            Data.users.Add(new User() { Name = "Ingrid" });
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
            wUd.ShowDialog();
        }
    }
}
