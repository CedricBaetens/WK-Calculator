using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WK_Calculator
{
    [ImplementPropertyChanged]
    public partial class WindowUserData : Window
    {
        ObservableCollection<User> users = new ObservableCollection<User>();

        Schema SpeelSchema = new Schema();

        string dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WK Downloader";

        public WindowUserData()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            // UI
            HideWindows(3);

            users.Add(new User() {Name = "Cedric"});
            users.Add(new User() { Name = "Patrik" });
            users.Add(new User() { Name = "Ingrid" });
            lbUsers.DataContext = users;


            // Read Group Matches + add to Speelschema
            string[] lines = File.ReadAllLines(dataFolder + @"\Matchen.txt" );
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Groep"))
                {
                    Group groep = new Group();
                    groep.Name = lines[i];
                    for (int j = 1; j < 7; j++)
                    {
                        var line = lines[i + j];

                        // Match
                        var tempMatch = line.Split(')');
                        var match = tempMatch[1].Replace(" ", "");
                        var matchSplit = match.Split('-');
                        string teamA = matchSplit[0];
                        string teamB = matchSplit[1];

                        // Datum
                        var tempDatum = line.Split(',');
                        var datum = tempDatum[0].Replace(' ','-');
                        var uurSplit = tempDatum[1].Split('(');
                        string uur = uurSplit[1].Substring(0,2);

                        string datumFull = datum +  "-" + uur;

                        groep.Matchen.Add(new Match(teamA,teamB,datumFull));
                    }
                    SpeelSchema.Groups.Add(groep);
                }
            }

            // Speelschema toevoegen aan elke speler
            foreach (var user in users)
            {
                user.SpeelSchema = SpeelSchema;
            }

        }

        private void HideWindows(int Count)
        {
            if (Count == 3)
            {
                lbGroepen.Visibility = Visibility.Hidden;
                lbMatches.Visibility = Visibility.Hidden;
                grMatchData.Visibility = Visibility.Hidden;
                
            }
            else if (Count == 2)
            {
                lbMatches.Visibility = Visibility.Hidden;
                grMatchData.Visibility = Visibility.Hidden;
            }
            
        }

        private void lbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideWindows(3);
            lbGroepen.DataContext = ((User)lbUsers.SelectedItem).SpeelSchema.Groups;
            lbGroepen.Visibility = Visibility.Visible;
        }

        private void lbGroepen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideWindows(2);
            lbMatches.Visibility = Visibility.Visible;
            lbMatches.DataContext = ((Group)lbGroepen.SelectedItem).Matchen;
        }

        private void lbMatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMatches.SelectedIndex > -1)
                grMatchData.Visibility = Visibility.Visible;
        }

        private void btnCalculatePoints_Click(object sender, RoutedEventArgs e)
        {
            int a = 0;
        }
    }
}
