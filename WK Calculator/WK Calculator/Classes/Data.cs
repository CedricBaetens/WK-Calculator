using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WK_Calculator
{
    public abstract class Data
    {
       public static ObservableCollection<User> users = new ObservableCollection<User>();
       public static Schema SpeelSchema = new Schema();
    }
}
