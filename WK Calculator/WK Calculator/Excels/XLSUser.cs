using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;

namespace WK_Calculator
{
    class XLSUser : XLS
    {
        static User user;
        public static void Init(User user_)
        {
            user = user_;
            File = new FileStream(dataFolder + @"\Spelers\" + user.Name + ".xls" , FileMode.Open, FileAccess.Read);
            Workbook = new HSSFWorkbook(File);
            Sheet = Workbook.GetSheet("Sheet1");
        }

        public static void ReadXLS()
        {
            int groupIndex = 0;
            int matchIndex = 0;

            for (int row = 1; row < Sheet.LastRowNum + 1; row++)
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

                        if (oldScoreA == -1 && oldScoreB == -1)
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
    }
}
