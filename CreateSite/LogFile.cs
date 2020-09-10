using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSite
{
     static class LogFile
    {
        public static void WriteLog(string Title, string PathToLogFile, string FileName, bool IsAppend)
        {
            if (IsAppend)
            {
                File.AppendAllText($"{PathToLogFile}{FileName}.txt", $"\n{Title}");
            }
            else
            {
                File.WriteAllText($"{PathToLogFile}{FileName}.txt", $"\n{Title}");
            }
        }
    }
}
