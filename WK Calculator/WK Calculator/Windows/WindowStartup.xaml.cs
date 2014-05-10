using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace WK_Calculator
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class WindowStartup : Window
    {
        FileInfo[] files;

        bool error = false;

        WindowMainData wMd;
        WindowUserData wUd;
        WindowPoints wP;

        string dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WK Calculator";

        public WindowStartup()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Data initialiseren
            Data.Init();

            // Gebruikers aanmaken
            files = new DirectoryInfo(dataFolder + @"/Spelers").GetFiles();
            foreach (var file in files)
                Data.Users.Add(new User() { Name = file.Name.Replace(".xls",""),xlsLocation = file.FullName });

            // Read Scores
            XLSDataSpelers.Init();
            XLSDataSpelers.ReadXLS();

            int b = 0;
            // Read user XLS
            foreach (var user in Data.Users)
            {
                XLSUser.Init(user);
                XLSUser.ReadXLS();
            }
            int a = 0;

            // Read main XLS
            XLSMainData.Init();
            XLSMainData.ReadXLS();

            int c = 0;
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
            if (error == false)
            {
                XLSDataSpelers.WriteXLS();
            }
        }

        private void SentMail(MailAddress DestinationAddress, string filePath)
        {
            var fromAddress = new MailAddress("baellonmusic@gmail.com", "Cedric Baetens");
            var toAddress = DestinationAddress;

            const string fromPassword = "123cb456";
            const string subject = "WK Excel File";
            const string body = "Hier uw WK Excel File!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            var message = new MailMessage(fromAddress.Address, toAddress.Address, subject, body);

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(filePath);
            message.Attachments.Add(attachment);

           // smtp.Send(message);
            smtp.SendAsync(message,null);
        }

        private void btnMailXLS_Click(object sender, RoutedEventArgs e)
        {
            foreach (var user in Data.Users)
            {
                SentMail(user.Email, user.xlsLocation);
            }
            MessageBox.Show("Excel Files Sent!");
        }
    }
}
