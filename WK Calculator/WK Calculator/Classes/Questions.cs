using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WK_Calculator
{
    public class Question
    {
        public string Name { get; set; }
        public ObservableCollection<string> Antwoorden = new ObservableCollection<string>() { "", "", "", "" };
    }
}
