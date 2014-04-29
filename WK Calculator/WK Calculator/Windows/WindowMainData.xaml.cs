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
    /// Interaction logic for MainData.xaml
    /// </summary>
    public partial class WindowMainData : Window
    {
        public WindowMainData()
        {
            InitializeComponent();

            // Visibility
            lbMatches.Visibility = Visibility.Hidden;
            grMatchData.Visibility = Visibility.Hidden;

            //
            grData.DataContext = Data.Last4;
        }

        private void lbSchema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbMatches.DataContext = ((Group)lbSchema.SelectedItem).Matchen;
            lbMatches.Visibility = Visibility.Visible;
            grMatchData.Visibility = Visibility.Hidden;
        }

        private void lbMatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbMatches.Visibility = Visibility.Visible;
            grMatchData.Visibility = Visibility.Visible;
        }

        private void tb_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Clear();
        }        
    }
}
