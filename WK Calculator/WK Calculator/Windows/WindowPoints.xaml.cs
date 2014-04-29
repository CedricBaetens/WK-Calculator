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
    /// Interaction logic for WindowPoints.xaml
    /// </summary>
    public partial class WindowPoints : Window
    {
        public WindowPoints()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            // Punten Wisse
            foreach (var user in Data.Users)
            {
                user.Points = 0;
            }


            // Punten Berekenen
            // Matchen
            // Elke user
            for (int i = 0; i < Data.Users.Count; i++)
            {
                // Elke group
                for (int j = 0; j < Data.Users[i].SpeelSchema.Groups.Count; j++)
                {
                    // Elke match
                    for (int k = 0; k < Data.Users[i].SpeelSchema.Groups[j].Matchen.Count; k++)
                    {
                        Match userMatch = Data.Users[i].SpeelSchema.Groups[j].Matchen[k];
                        Match mainMatch = Data.SpeelSchema.Groups[j].Matchen[k];

                        //Wedstrijd is gespeeld
                        if (mainMatch.TeamAScore != -1 && mainMatch.TeamBScore != -1)
                        {
                            // Zelfde Uitslag
                            if (userMatch.TeamAScore == mainMatch.TeamAScore &&
                                userMatch.TeamBScore == mainMatch.TeamBScore)
                            {
                                Data.Users[i].Points += 5;
                            }

                            else
                            {
                                if (userMatch.Winnaar == mainMatch.Winnaar)
                                {
                                    Data.Users[i].Points += 2;
                                }
                            }
                        } 
                    }
                }
            }
            lvData.DataContext = Data.Users;
        }
    }
}
