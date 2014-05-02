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
    /// Interaction logic for WindowUserData.xaml
    /// </summary>
    public partial class WindowUserData : Window
    {
        public WindowUserData()
        {
            InitializeComponent();

            lbSchema.Visibility = Visibility.Hidden;
            lbMatches.Visibility = Visibility.Hidden;
            grMatchData.Visibility = Visibility.Hidden;
            grQuestionAnswers.Visibility = Visibility.Hidden;
            lbQuestion.Visibility = Visibility.Hidden;
        }

        // Grid1
        private void lbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Grid 1
            lbSchema.DataContext = ((User)lbUsers.SelectedItem).SpeelSchema.Groups;
            lbSchema.Visibility = Visibility.Visible;
            lbMatches.Visibility = Visibility.Hidden;
            grMatchData.Visibility = Visibility.Hidden;

            // Grid 2
            lbQuestion.DataContext = ((User)lbUsers.SelectedItem).Questions;
            lbQuestion.Visibility = Visibility.Visible;
            grQuestionAnswers.Visibility = Visibility.Hidden;
        }

        private void lbSchema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Group)lbSchema.SelectedItem) != null)
                lbMatches.DataContext = ((Group)lbSchema.SelectedItem).Matchen;
            lbSchema.Visibility = Visibility.Visible;
            lbMatches.Visibility = Visibility.Visible;
            grMatchData.Visibility = Visibility.Hidden;
        }

        private void lbMatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbSchema.Visibility = Visibility.Visible;
            lbMatches.Visibility = Visibility.Visible;
            grMatchData.Visibility = Visibility.Visible;
        }
               
        // Grid 2
        private void lbQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grQuestionAnswers.DataContext = ((Question)lbQuestion.SelectedItem).Antwoorden;
            grQuestionAnswers.Visibility = Visibility.Visible;
        }

        private void tb_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Clear();
        }
    }
}
