using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;

namespace WK_Calculator
{
    abstract class XLSDataSpelers : XLS
    {
        public static void Init()
        {
            File = new FileStream(dataFolder + @"\DataSpelers.xls", FileMode.Open, FileAccess.Read);           
            Workbook = new HSSFWorkbook(File);
            Sheet = Workbook.GetSheet("Users");
        }

        public static void ReadXLS()
        {
            #region Scores

            Init();
            // Elke gebruiker
            foreach (var user in Data.Users)
            {
                // Gebruikers aflopen in excel
                for (int col = 1; col < Data.Users.Count + 1; col++)
                {
                    // Zelfde grbuiker in de lijst als in excel
                    string val = Sheet.GetRow(0).GetCell(col).StringCellValue;
                    if (Sheet.GetRow(0).GetCell(col).StringCellValue == user.Name)
                    {
                        #region Matchen
                        
                        int groupIndex = 0;
                        int matchIndex = 0;
                        for (int row = 1; row < 78; row++)
                        {
                            if (Sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (Sheet.GetRow(row).GetCell(col).StringCellValue != "" && Sheet.GetRow(row).GetCell(col).StringCellValue != "/")
                                {
                                    string value = Sheet.GetRow(row).GetCell(col).StringCellValue;
                                    var valueSplit = value.Split('-');

                                    user.SpeelSchema.Groups[groupIndex].Matchen[matchIndex].TeamAScore = Convert.ToInt32(valueSplit[0]);
                                    user.SpeelSchema.Groups[groupIndex].Matchen[matchIndex].TeamBScore = Convert.ToInt32(valueSplit[1]);
                                    matchIndex++;
                                }
                                else
                                {
                                    groupIndex++;
                                    matchIndex = 0;
                                }
                            }
                        }

                        #endregion
                        #region Vragen Laatste 4

                        int antwoordIndex = 0;
                        int vraagIndex = 0;
                        for (int row = 79; row < 93; row++)
                        {
                            if (Sheet.GetRow(row) != null)
                            {
                                if (Sheet.GetRow(row).GetCell(col) != null)
                                {
                                    string value = Sheet.GetRow(row).GetCell(col).StringCellValue;
                                    if (value != "/")
                                    {
                                        user.Questions[vraagIndex].Antwoorden[antwoordIndex] = value;
                                    }                            
                                    antwoordIndex++;
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
                }
            }

            #endregion
        }
        public static void WriteXLS()
        {
            Init();
            // Elke gebruiker
            foreach (var user in Data.Users)
            {
                // Gebruikers aflopen in excel
                for (int col = 1; col < Data.Users.Count + 1; col++)
                {
                    // Zelfde grbuiker in de lijst als in excel
                    string val = Sheet.GetRow(0).GetCell(col).StringCellValue;
                    if (val == user.Name)
                    {
                        #region Matchen

                        int groupIndex = 0;
                        int matchIndex = 0;
                        for (int row = 1; row < 78; row++)
                        {
                            if (Sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (Sheet.GetRow(row).GetCell(col).StringCellValue != "")
                                {
                                    int scoreA = user.SpeelSchema.Groups[groupIndex].Matchen[matchIndex].TeamAScore;
                                    int scoreB = user.SpeelSchema.Groups[groupIndex].Matchen[matchIndex].TeamBScore;
                                    if (scoreA != -1 && scoreB != -1)
                                    {
                                        Sheet.GetRow(row).GetCell(col).SetCellValue(scoreA + "-" + scoreB);
                                        string value = Sheet.GetRow(row).GetCell(col).StringCellValue;
                                        matchIndex++;
                                    }
                                }
                                else
                                {
                                    groupIndex++;
                                    matchIndex = 0;
                                }
                            }
                        }

                        #endregion
                        #region Vragen Laatste 4

                        int antwoordIndex = 0;
                        int vraagIndex = 0;

                        for (int row = 79; row < 93; row++)
                        {
                            if (Sheet.GetRow(row) != null)
                            {
                                string valExcel = Sheet.GetRow(row).GetCell(col).StringCellValue;
                                string valProg = user.Questions[vraagIndex].Antwoorden[antwoordIndex];
                                if (valExcel != "")
                                {
                                    if (valExcel == "/" && valProg != "")
                                    {
                                        Sheet.GetRow(row).GetCell(col).SetCellValue(user.Questions[vraagIndex].Antwoorden[antwoordIndex]);
                                    }
                                    antwoordIndex++;
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
                }
            }
            File = new FileStream(dataFolder + @"\DataSpelers.xls", FileMode.Create);
            Workbook.Write(File);
            File.Close();
        }
    }
}