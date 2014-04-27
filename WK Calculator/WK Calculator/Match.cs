using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WK_Calculator
{
    class Match
    {
        public string Datum { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public int TeamAScore { get; set; }
        public int TeamBScore { get; set; }

        public Match(string teamA, string teamB,string datum)
        {
            TeamA = teamA;
            TeamB = teamB;
            Datum = datum;
        }
    }
}
