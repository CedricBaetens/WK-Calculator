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

            grMatchData.Visibility = Visibility.Hidden;
            lbMatches.Visibility = Visibility.Hidden;
        }

        private void lbSchema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbMatches.DataContext = ((Group)lbSchema.SelectedItem).Matchen;

            
            if (lbSchema.SelectedIndex > -1)
                lbMatches.Visibility = Visibility.Visible;
            else
                lbMatches.Visibility = Visibility.Hidden;
        }

        private void lbMatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMatches.SelectedIndex > -1)
                grMatchData.Visibility = Visibility.Visible;
            else
                grMatchData.Visibility = Visibility.Hidden;
        }

        
    }
}
