using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WK_Calculator
{
    [ImplementPropertyChanged]
    public class User
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public Schema SpeelSchema = new Schema();

        public ObservableCollection<string> Last4_VoorWk = new ObservableCollection<string>() { "A", "B", "C", "D" };
        public ObservableCollection<string> Last4_Voor8SteFinale = new ObservableCollection<string>() { "A", "B", "C", "D" };
        public ObservableCollection<string> Last4_Voor4DeFinale = new ObservableCollection<string>() { "A", "B", "C", "D" };
        public ObservableCollection<string> Last4_VoorVoorHalveFinale = new ObservableCollection<string>() { "A", "B", "C", "D" };
    }
}
