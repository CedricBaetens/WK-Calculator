using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WK_Calculator
{
    [ImplementPropertyChanged]
    public class Group
    {
        public string Name { get; set; }
        public ObservableCollection<Match> Matchen = new ObservableCollection<Match>();

    }
}
