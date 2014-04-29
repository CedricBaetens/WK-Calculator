﻿using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WK_Calculator
{
    [ImplementPropertyChanged]
    public class Match
    {
        public DateTime Datum { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public int TeamAScore { get; set; }
        public int TeamBScore { get; set; }
        public Uitslag Winnaar
        {
            get 
            {
                if (TeamAScore > TeamBScore)
                    return Uitslag.TeamA;

                else if (TeamBScore > TeamAScore)
                    return Uitslag.TeamB;

                else
                    return Uitslag.Gelijk;
            }
        }
        


        public Match(string teamA, string teamB,string datum)
        {
            TeamA = teamA;
            TeamB = teamB;

            TeamAScore = -1;
            TeamBScore = -1;

            // Datum
            var date = datum.Split('-');
            if (date[1]=="juni")
                date[1] = "06";
            if (date[1] == "juli")
                date[1] = "07";

            Datum = new DateTime(2014,Convert.ToInt32(date[1]),Convert.ToInt32(date[0]),Convert.ToInt32(date[2]),0,0);        
        }

        public override string ToString()
        {
            return TeamA + " - " + TeamB + "(" + Datum.ToString(@"dd/MM/yyyy") + ")";
        }
    }

    public enum Uitslag
    {
        TeamA,
        TeamB,
        Gelijk
    }
}
