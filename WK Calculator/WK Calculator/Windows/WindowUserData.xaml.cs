using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WK_Calculator
{
    /// <summary>
    /// Interaction logic for WindowUserData.xaml
    /// </summary>
    public partial class WindowUserData : Window
    {
        public List<Match> matchen = new List<Match>();
        public List<Question> vragen = new List<Question>();
        public WindowUserData()
        {
            InitializeComponent();
            lbUsers.SelectedIndex = 0;
        }

        private void lbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            matchen.Clear();
            vragen.Clear();

            User user = (User)lbUsers.SelectedItem;

            // Matchen
            foreach (var group in user.SpeelSchema.Groups)
            {
                foreach (var match in group.Matchen)
                {
                    matchen.Add(match);
                }
            }
            dgMatches.ItemsSource = matchen;
            dgMatches.Items.Refresh();

            // Vragen
            foreach (var question in user.Questions)
            {
                if (question is Question4Answers)
                {
                    question.Antwoord1String = "";
                    Question4Answers question4 = (Question4Answers)question;
                    foreach (var antwoord in question4.Antwoorden)
                    {
                        if (antwoord != "")
                        {
                            question.Antwoord1String += (question4.Antwoorden.IndexOf(antwoord) + 1) + "e: " + antwoord + ", ";
                        }
                    }
                
                }
                else if (question is QuestionSingleAnswer)
                {
                    question.Antwoord1String = ((QuestionSingleAnswer)question).Antwoord;
                }
                vragen.Add(question);
            }
            dgQuestions.ItemsSource = vragen;
            dgQuestions.Items.Refresh();
        }
    }
}
