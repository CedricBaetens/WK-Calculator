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
    /// Interaction logic for WindowPoints.xaml
    /// </summary>
    public partial class WindowPoints : Window
    {
        public WindowPoints()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            // Punten Wissen
            foreach (var user in Data.Users)
            {
                user.Points = 0;
            }


            // Punten Berekenen
            // Elke user
            for (int i = 0; i < Data.Users.Count; i++)
            {
                #region Matchen

                // Elke group
                for (int j = 0; j < Data.Users[i].SpeelSchema.Groups.Count; j++)
                {
                    // Elke match
                    for (int k = 0; k < Data.Users[i].SpeelSchema.Groups[j].Matchen.Count; k++)
                    {
                        Match userMatch = Data.Users[i].SpeelSchema.Groups[j].Matchen[k];
                        Match mainMatch = Data.SpeelSchema.Groups[j].Matchen[k];

                        //Wedstrijd is gespeeld
                        if (mainMatch.TeamAScore != -1 && mainMatch.TeamBScore != -1)
                        {
                            // Zelfde Uitslag
                            if (userMatch.TeamAScore == mainMatch.TeamAScore &&
                                userMatch.TeamBScore == mainMatch.TeamBScore)
                            {
                                Data.Users[i].Points += 5;
                                Data.Users[i].PointsLog.Add(
                                    new Log() {
                                        Type = "Match",
                                        Name = mainMatch.GetName(),
                                        Date = mainMatch.Datum.ToShortDateString(),
                                        PointsGained = "5",
                                        PointsTotal = Data.Users[i].Points.ToString()
                                    });
                            }

                            else
                            {
                                if (userMatch.Winnaar == mainMatch.Winnaar)
                                {
                                    Data.Users[i].Points += 2;
                                    Data.Users[i].PointsLog.Add(
                                    new Log()
                                    {
                                        Type = "Match",
                                        Name = mainMatch.GetName(),
                                        Date = mainMatch.Datum.ToShortDateString(),
                                        PointsGained = "2",
                                        PointsTotal = Data.Users[i].Points.ToString()
                                    });
                                }
                            }
                        } 
                    }
                }
                #endregion
                #region Vragen

                for (int j = 0; j < Data.Users[i].Questions.Count; j++)
                {
                    #region Laatste 4

                    if (j == 0)
                    {
                        for (int k = 0; k < ((Question4Answers)Data.Users[i].Questions[j]).Antwoorden.Count; k++)
                        {
                            AddPoints_Laatste4(i, j, k, 20, 10);
                        }
                    }
                    else if(j==1)
                    {
                        for (int k = 0; k < ((Question4Answers)Data.Users[i].Questions[j]).Antwoorden.Count; k++)
                        {
                            AddPoints_Laatste4(i, j, k, 10, 5);
                        }
                    }
                    else if(j==2)
                    {
                        for (int k = 0; k < ((Question4Answers)Data.Users[i].Questions[j]).Antwoorden.Count; k++)
                        {
                            AddPoints_Laatste4(i, j, k, 5, 0);
                        }
                    }
                    #endregion 
                    #region Groepsstand
                    else if (j >= 3 && j <= 10)
                    {
                        for (int k = 0; k < ((Question4Answers)Data.Users[i].Questions[j]).Antwoorden.Count; k++)
                        {
                            if (((Question4Answers)Data.Questions[j-2]).Antwoorden[k] == ((Question4Answers)Data.Users[i].Questions[j]).Antwoorden[k])
                            {
                                Data.Users[i].Points += 2;
                                Data.Users[i].PointsLog.Add(
                                    new Log()
                                    {
                                        Type = "Vraag",
                                        Name = Data.Users[i].Questions[j].Name + " (" + (k+1) + "e)",
                                        Date = DateTime.Now.ToShortDateString(),
                                        PointsGained = "2",
                                        PointsTotal = Data.Users[i].Points.ToString()
                                    });
                            }
                        }
 
                    }

                    #endregion

                    #region Topscoorder
                    
                    else if(j == 11)
                    {
                        if (((QuestionSingleAnswer)Data.Questions[j - 2]).Antwoord == ((QuestionSingleAnswer)Data.Users[i].Questions[j]).Antwoord)
                        {
                            Data.Users[i].Points += 5;
                            Data.Users[i].PointsLog.Add(
                                new Log()
                                {
                                    Type = "Vraag",
                                    Name = Data.Users[i].Questions[j].Name,
                                    Date = DateTime.Now.ToShortDateString(),
                                    PointsGained = "5",
                                    PointsTotal = Data.Users[i].Points.ToString()
                                });
                        }
                    }
                    #endregion
                }

                #endregion

                // Sorteren op datum
                Data.Users[i].PointsLog = Data.Users[i].PointsLog.OrderByDescending(order => order.Date).ToList();
            }
            lvData.DataContext = Data.Users;           
        }

        private static void AddPoints_Laatste4(int userIndex, int questionIndex, int antwoordIndex, int point1, int point2)
        {
            if (((Question4Answers)Data.Questions[0]).Antwoorden.Contains(((Question4Answers)Data.Users[userIndex].Questions[questionIndex]).Antwoorden[antwoordIndex]))
            {
                if (((Question4Answers)Data.Questions[0]).Antwoorden[antwoordIndex] == ((Question4Answers)Data.Users[userIndex].Questions[questionIndex]).Antwoorden[antwoordIndex])
                {
                    Data.Users[userIndex].Points += point1;
                    Data.Users[userIndex].PointsLog.Add(
                        new Log()
                        {
                            Type = "Vraag",
                            Name = Data.Users[userIndex].Questions[questionIndex].Name + " (Juiste Plaats)",
                            Date = DateTime.Now.ToShortDateString(),
                            PointsGained = point1.ToString(),
                            PointsTotal = Data.Users[userIndex].Points.ToString()
                        });
                }
                else
                {
                    if (point2 != 0)
                    {
                        Data.Users[userIndex].Points += point2;
                        Data.Users[userIndex].PointsLog.Add(
                            new Log()
                            {
                                Type = "Vraag",
                                Name = Data.Users[userIndex].Questions[questionIndex].Name + " (Foute Plaats)",
                                Date = DateTime.Now.ToShortDateString(),
                                PointsGained = point2.ToString(),
                                PointsTotal = Data.Users[userIndex].Points.ToString()
                            });
                    } 
                }
            }
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvLog.DataContext = ((User)lvData.SelectedItem).PointsLog;
        }
    }
}
