using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WK_Calculator
{
    [ImplementPropertyChanged]
    public class Schema
    {
        public ObservableCollection<Group> Groups = new ObservableCollection<Group>();

        public Schema()
        {
            string dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WK Downloader";

            // Read GroupsFase
            string[] lines = File.ReadAllLines(dataFolder + @"\Matchen.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Groep"))
                {
                    Group groep = new Group();
                    groep.Name = lines[i].Replace(":","");
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
                        var datum = tempDatum[0].Replace(' ', '-');
                        var uurSplit = tempDatum[1].Split('(');
                        string uur = uurSplit[1].Substring(0, 2);

                        string datumFull = datum + "-" + uur;

                        groep.Matchen.Add(new Match(teamA, teamB, datumFull));
                    }
                    Groups.Add(groep);
                }
            }
        }
    }
}
