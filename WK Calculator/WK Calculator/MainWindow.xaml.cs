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
    public partial class MainWindow : Window
    {
        ObservableCollection<User> users = new ObservableCollection<User>();

        Schema SpeelSchema = new Schema();

        string dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WK Downloader";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            users.Add(new User() {Name = "Cedric"});
            users.Add(new User() { Name = "Patrik" });
            users.Add(new User() { Name = "Ingrid" });
            lbUsers.DataContext = users;
            lbMatches.DataContext = users[0];


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
                    SpeelSchema.GroepsFase.Add(groep);
                }
            }

            // Speelschema toevoegen aan elke speler
            foreach (var user in users)
            {
                user.SpeelSchema = SpeelSchema;
            }

        }

        private void lbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User temp = (User)lbUsers.SelectedItem;
            lbMatches.DataContext = temp;
        }
    }
}
