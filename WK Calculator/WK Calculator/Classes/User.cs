using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Net.Mail;

namespace WK_Calculator
{
    [ImplementPropertyChanged]
    public class User
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public MailAddress Email = new MailAddress("baellonmusic@gmail.com", "Cedric Baetens");
        public string xlsLocation { get; set; }
        public List<Log> PointsLog = new List<Log>();

        public Schema SpeelSchema = new Schema();

        public ObservableCollection<Question> Questions = new ObservableCollection<Question>();

        public User()
        {
             Questions.Add(new Question4Answers() { Name = "Laatste 4 - Voor WK"});
             Questions.Add(new Question4Answers() { Name = "Laatste 4 - Na Groepsfase" });
             Questions.Add(new Question4Answers() { Name = "Laatste 4 - Na Kwart Finale" });
             Questions.Add(new Question4Answers() { Name = "Stand Groep A" });
             Questions.Add(new Question4Answers() { Name = "Stand Groep B" });
             Questions.Add(new Question4Answers() { Name = "Stand Groep C" });
             Questions.Add(new Question4Answers() { Name = "Stand Groep D" });
             Questions.Add(new Question4Answers() { Name = "Stand Groep E" });
             Questions.Add(new Question4Answers() { Name = "Stand Groep F" });
             Questions.Add(new Question4Answers() { Name = "Stand Groep G" });
             Questions.Add(new Question4Answers() { Name = "Stand Groep H" });
             Questions.Add(new QuestionSingleAnswer() { Name = "Topscoorder" });
             Questions.Add(new QuestionSingleAnswer() { Name = "Doelpunten Topscoorder" });
             Questions.Add(new QuestionSingleAnswer() { Name = "Minuten scoren in finale" });
        }
    }
}
