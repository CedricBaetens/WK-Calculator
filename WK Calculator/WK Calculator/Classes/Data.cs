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
        public static ObservableCollection<User> Users = new ObservableCollection<User>();
        public static Schema SpeelSchema = new Schema();
        public static ObservableCollection<Question> Questions = new ObservableCollection<Question>();

        public static void Init()
        {
            Questions.Add(new Question4Answers() { Name = "Laatste 4" });
            Questions.Add(new Question4Answers() { Name = "Stand Groep A" });
            Questions.Add(new Question4Answers() { Name = "Stand Groep B" });
            Questions.Add(new Question4Answers() { Name = "Stand Groep C" });
            Questions.Add(new Question4Answers() { Name = "Stand Groep D" });
            Questions.Add(new Question4Answers() { Name = "Stand Groep E" });
            Questions.Add(new Question4Answers() { Name = "Stand Groep F" });
            Questions.Add(new Question4Answers() { Name = "Stand Groep G" });
            Questions.Add(new Question4Answers() { Name = "Stand Groep H" });
            Questions.Add(new QuestionSingleAnswer() { Name = "Top Scoorder" });
            Questions.Add(new QuestionSingleAnswer() { Name = "Doelpunten Topscoorder" });
            Questions.Add(new QuestionSingleAnswer() { Name = "Minute scoren in finale" });
        }
    }
}
