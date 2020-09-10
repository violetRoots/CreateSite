using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSite
{
    class WritePatternCsv
    {
        string DESCRIPTION;
        string END;
        string HEAD;
        string FILE_NAME;
        string FILE_PATH;
        
        public WritePatternCsv(string PathForWritePattern, string FileName, bool IsAppendToFile)
        {
            FILE_NAME = FileName;
            FILE_PATH = PathForWritePattern;
            FILE_PATH += FILE_NAME;
            FILE_PATH += ".csv";

            if (IsAppendToFile) return; 

            HEAD = "Title;Content;Description;id";

            File.WriteAllText(FILE_PATH, HEAD);

        }

        public void AppendLine(string Title, string WEB_PathForImage, string WEB_PathForPDF, string ImageName, string PDFName, Description SiteDescription, string TagDescription, string ParentPageId)
        {
            DESCRIPTION = SiteDescription.GetDescription();

            END = @"<div> </div><div> </div><div> </div><div style=""text-align: center;""><span style=""font-size: 36px; color: #ff0000;""><strong><span style=""font-family: 'comic sans ms', sans-serif;""><a style=""color: #ff0000;"" href=""https://youmanual.ru/wp-content/uploads/" + WEB_PathForPDF + PDFName + @".pdf"" target=""_blank"" rel=""noopener noreferrer""><span style=""font-size: 36px; font-family: 'comic sans ms', sans-serif;"">Смотреть и скачать инструкцию</span></a></span></strong></span></div><div>[adinserter block=""5""]</div><div> </div><div> </div>""";

            DESCRIPTION = DESCRIPTION.Replace("\n", "");
            DESCRIPTION = DESCRIPTION.Replace(";", ",");
            END = END.Replace("\n", "");
            END = END.Replace(";", ",");

            string AppendLine = $"\n {Title};";
            File.AppendAllText(FILE_PATH, AppendLine);

            AppendLine = $"{DESCRIPTION}";
            File.AppendAllText(FILE_PATH, AppendLine,Encoding.GetEncoding(1251));

            AppendLine = $"{END}; {TagDescription}; {ParentPageId}";
            File.AppendAllText(FILE_PATH, AppendLine);
        }
    }
}
