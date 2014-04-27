using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WK_Calculator
{
    [ImplementPropertyChanged]
    public class Group
    {
        public string Name { get; set; }
        public List<Match> Matchen = new List<Match>();
    }
}
