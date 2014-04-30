using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace WK_Calculator
{
    abstract class XLS
    {
        protected static HSSFWorkbook Workbook;
        protected static ISheet Sheet;
        protected static FileStream File;
        protected static string dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WK Downloader";
    }
}
