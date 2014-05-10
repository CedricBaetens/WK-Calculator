using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;

namespace WK_Calculator
{
    class XLSMainData : XLS
    {
        public static void Init()
        {
            File = new FileStream(dataFolder + @"\DataMain.xls", FileMode.Open, FileAccess.Read);
            Workbook = new HSSFWorkbook(File);
            Sheet = Workbook.GetSheet("Main");
        }

        public static void ReadXLS()
        {
            #region Matchen

            int groupIndex = 0;
            int matchIndex = 0;
            for (int row = 2; row < 78; row++)
            {
                if (Sheet.GetRow(row).GetCell(1) != null)
                {
                    if (Sheet.GetRow(row).GetCell(1).StringCellValue != "" && Sheet.GetRow(row).GetCell(1).StringCellValue != "/")
                    {
                        string value = Sheet.GetRow(row).GetCell(1).StringCellValue;
                        var valueSplit = value.Split('-');
                        Data.SpeelSchema.Groups[groupIndex].Matchen[matchIndex].TeamAScore = Convert.ToInt32(valueSplit[0]);
                        Data.SpeelSchema.Groups[groupIndex].Matchen[matchIndex].TeamBScore = Convert.ToInt32(valueSplit[1]);
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
            for (int row = 79; row < 130; row++)
            {
                if (Sheet.GetRow(row) != null)
                {
                    if (Sheet.GetRow(row).GetCell(1) != null)
                    {
                        if (row < 123)
                        {
                            string value = Sheet.GetRow(row).GetCell(1).StringCellValue;
                            if (value != "/")
                            {
                                ((Question4Answers)Data.Questions[vraagIndex]).Antwoorden[antwoordIndex] = value;
                            }
                            antwoordIndex++;
                        }

                        else
                        {
                            string value = Sheet.GetRow(row).GetCell(1).StringCellValue;
                            ((QuestionSingleAnswer)Data.Questions[vraagIndex]).Antwoord = value;
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
    }
}
