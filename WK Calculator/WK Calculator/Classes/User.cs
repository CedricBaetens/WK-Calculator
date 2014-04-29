using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WK_Calculator
{
    [ImplementPropertyChanged]
    public class User
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public Schema SpeelSchema = new Schema();

        public User()
        {
            SpeelSchema = Data.SpeelSchema;
        }
    }
}
