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
            Group groep;
            string dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WK Downloader";

            string[] lines = File.ReadAllLines(dataFolder + @"\Matchen.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Groep"))
                {
                    groep = CreateMatches(lines, i,6);
                }
                else if (lines[i].Contains("1/8e"))
                {
                    groep = CreateMatches(lines, i, 8);
                }
                else if (lines[i].Contains("Kwart"))
                {
                    groep = CreateMatches(lines, i, 4);
                }
                else if (lines[i].Contains("Halve"))
                {
                    groep = CreateMatches(lines, i, 2);
                }
                else if (lines[i].Contains("Kleine finale"))
                {
                    groep = CreateMatches(lines, i, 1);
                }
                else if (lines[i].Contains("Finale"))
                {
                    groep = CreateMatches(lines, i, 1);
                }
            }
        }

        private Group CreateMatches(string[] lines, int i, int count)
        {
            count += 1;
            Group groep;
            groep = new Group();
            groep.Name = lines[i].Replace(":", "");
            for (int j = 1; j < count; j++)
            {
                var line = lines[i + j];
                groep.Matchen.Add(FormatData(line));
            }
            Groups.Add(groep);
            return groep;
        }
        private Match FormatData(string line)
        {
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

            return new Match(teamA, teamB, datumFull);
        }
    }
}
