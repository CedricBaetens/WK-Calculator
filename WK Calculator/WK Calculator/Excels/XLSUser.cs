using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NPOI.HSSF.UserModel;

namespace WK_Calculator
{
    class XLSUser : XLS
    {
        static User user;
        public static void Init(User user_)
        {
            try
            {
                user = user_;
                File = new FileStream(dataFolder + @"\Spelers\" + user.Name + ".xls", FileMode.Open, FileAccess.Read);
                Workbook = new HSSFWorkbook(File);
                Sheet = Workbook.GetSheet("Sheet1");
            }
            catch (IOException)
            {
                MessageBox.Show(user.Name + ".xls is open, gelieve deze te sluiten en het programma opnieuw op te starten.", "Excel lees fout");
                Environment.Exit(0);
            }
        }

        public static void ReadXLS()
        {
            int row = 1;
            try
            {
                #region Matchen
                int groupIndex = 0;
                int matchIndex = 0;
                for (row = 1; row < 77; row++)
                {
                    if (Sheet.GetRow(row) != null)
                    {
                        if (Sheet.GetRow(row).GetCell(1) != null)
                        {
                            if (Sheet.GetRow(row).GetCell(1).StringCellValue != "" && Sheet.GetRow(row).GetCell(1).StringCellValue != "/")
                            {
                                string val = Sheet.GetRow(row).GetCell(1).StringCellValue;
                                var valueSplit = val.Split('-');

                                int oldScoreA = user.SpeelSchema.Groups[groupIndex].Matchen[matchIndex].TeamAScore;
                                int oldScoreB = user.SpeelSchema.Groups[groupIndex].Matchen[matchIndex].TeamBScore;

                                int newScoreA = Convert.ToInt32(valueSplit[0]);
                                int newScoreB = Convert.ToInt32(valueSplit[1]);

                                DateTime matchTime = user.SpeelSchema.Groups[groupIndex].Matchen[matchIndex].Datum;
                                DateTime currentTime = DateTime.Now;

                                if (oldScoreA == -1 && oldScoreB == -1 && currentTime < matchTime)
                                {
                                    user.SpeelSchema.Groups[groupIndex].Matchen[matchIndex].TeamAScore = Convert.ToInt32(valueSplit[0]);
                                    user.SpeelSchema.Groups[groupIndex].Matchen[matchIndex].TeamBScore = Convert.ToInt32(valueSplit[1]);
                                }

                                matchIndex++;
                            }
                            else
                            {
                                groupIndex++;
                                matchIndex = 0;
                            }
                        }
                    }
                }
                #endregion
                #region Vragen

                int antwoordIndex = 0;
                int vraagIndex = 0;
                for (row = 78; row < 138; row++)
                {
                    if (Sheet.GetRow(row) != null)
                    {
                        if (Sheet.GetRow(row).GetCell(1) != null)
                        {
                            string value = Sheet.GetRow(row).GetCell(1).StringCellValue;
                            if (Sheet.GetRow(row).GetCell(1).StringCellValue != "")
                            {
                                // Laatste 4 + groepsstand
                                if (row < 133)
                                {
                                    string val = Sheet.GetRow(row).GetCell(1).StringCellValue;
                                    string questionVal = ((Question4Answers)user.Questions[vraagIndex]).Antwoorden[antwoordIndex];

                                    if (questionVal == "" && val != "/")
                                    {
                                        ((Question4Answers)user.Questions[vraagIndex]).Antwoorden[antwoordIndex] = val;
                                    }
                                }
                                else
                                {
                                    string val = Sheet.GetRow(row).GetCell(1).StringCellValue;
                                    string questionVal = ((QuestionSingleAnswer)user.Questions[vraagIndex]).Antwoord;

                                    if (questionVal == "" && val != "/")
                                    {
                                        ((QuestionSingleAnswer)user.Questions[vraagIndex]).Antwoord = val;

                                    }
                                    vraagIndex++;
                                }
                                antwoordIndex++;
                            }
                        }
                    }
                    else
                    {
                        antwoordIndex = 0;
                        vraagIndex++;
                    }
                }

                #endregion
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format("Fout bij het inlezen van {0}.xls. op rij {1}. Gelieve dit probleem op te lossen en het programma opnieuw op te starten.", user.Name,row+1));
                Environment.Exit(0);
            }
            
        }    
    }
}
