using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace WK_Calculator
{
    [ImplementPropertyChanged]
    class QuestionSingleAnswer : Question
    {
        public string Antwoord { get; set; }

        public QuestionSingleAnswer()
        {
            Antwoord = "";
        }
    }
}
