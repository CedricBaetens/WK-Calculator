using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WK_Calculator
{
    [ImplementPropertyChanged]
    public class Schema
    {
        public List<Group> GroepsFase = new List<Group>();

        public override string ToString()
        {
            return "test";
        }
    }
}
