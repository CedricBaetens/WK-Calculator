using System;
using System.Collections.Generic;
using System.IO;
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
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace WK_Calculator
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class WindowStartup : Window
    {
        WindowMainData wMd;
        WindowUserData wUd;
        WindowPoints wP;

        string dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WK Downloader";
        HSSFWorkbook xlsDocument;
        ISheet sheetUsers;
        FileStream file;

        public WindowStartup()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Data.Users.Add(new User() { Name = "Cedric"});
            Data.Users.Add(new User() { Name = "Patrik" });
            Data.Users.Add(new User() { Name = "Ingrid" });

            // Excel inlezen
            file = new FileStream(dataFolder + @"\Scores.xls", FileMode.Open, FileAccess.Read);
            xlsDocument = new HSSFWorkbook(file);
            sheetUsers = xlsDocument.GetSheet("Users");

            #region Gebruikers Data Inlezen

            ReadXLS(sheetUsers);

            #endregion

            #region Main Data Inlezen



            #endregion

        }

        private void ReadXLS(ISheet sheet)
        {
            // Elke gebruiker
            foreach (var user in Data.Users)
            {
                // Gebruikers aflopen in excel
                for (int col = 1; col < Data.Users.Count + 1; col++)
                {
                    // Zelfde grbuiker in de lijst als in excel
                    if (sheet.GetRow(0).GetCell(col).StringCellValue == user.Name)
                    {
                        int groupIndex = 0;
                        int matchIndex = 0;
                        for (int row = 1; row < sheet.LastRowNum + 1; row++)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).StringCellValue != "" && sheet.GetRow(row).GetCell(col).StringCellValue != "/")
                                {
                                    string value = sheet.GetRow(row).GetCell(col).StringCellValue;
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
                    }
                }
            }
        }

        private void WriteXLS(ISheet sheet)
        {
            // Elke gebruiker
            foreach (var user in Data.Users)
            {
                // Gebruikers aflopen in excel
                for (int col = 1; col < Data.Users.Count + 1; col++)
                {
                    // Zelfde grbuiker in de lijst als in excel
                    if (sheetUsers.GetRow(0).GetCell(col).StringCellValue == user.Name)
                    {
                        int groupIndex = 0;
                        int matchIndex = 0;
                        for (int row = 1; row < sheetUsers.LastRowNum + 1; row++)
                        {
                            if (sheetUsers.GetRow(row).GetCell(col) != null)
                            {
                                if (sheetUsers.GetRow(row).GetCell(col).StringCellValue != "")
                                {
                                    int scoreA = user.SpeelSchema.Groups[groupIndex].Matchen[matchIndex].TeamAScore;
                                    int scoreB = user.SpeelSchema.Groups[groupIndex].Matchen[matchIndex].TeamBScore;
                                    if (scoreA != -1 && scoreB != -1)
                                    {
                                        sheetUsers.GetRow(row).GetCell(col).SetCellValue(scoreA + "-" + scoreB);
                                        string value = sheet.GetRow(row).GetCell(col).StringCellValue;
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
                    }
                }
            }
            file = new FileStream(dataFolder + @"\Scores.xls", FileMode.Create);
            xlsDocument.Write(file);
            file.Close();
        }

        private void btnMainData_Click(object sender, RoutedEventArgs e)
        {
            wMd = new WindowMainData();

            wMd.lbSchema.DataContext = Data.SpeelSchema.Groups;

            this.Hide();
            wMd.ShowDialog();
            this.Show();
        }

        private void btnEnterPlayerData_Click(object sender, RoutedEventArgs e)
        {
            wUd = new WindowUserData();

            wUd.lbUsers.DataContext = Data.Users;

            this.Hide();
            wUd.ShowDialog();
            this.Show();
        }

        private void btnShowPoints_Click(object sender, RoutedEventArgs e)
        {
            wP = new WindowPoints();
            //this.Hide();
            wP.ShowDialog();
            //this.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WriteXLS(sheetUsers);
        }
    }
}
