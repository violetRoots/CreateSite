using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CreateSite
{
    class WritePatternTxt
    {
        string DESCRIPTION;
        string END;
        string FILE_NAME;

        public WritePatternTxt(string WEB_PathForImage, string WEB_PathForPDF, string Title, string ImageName, string PDFName, Description SiteDescription)
        {
            FILE_NAME = Title;

            DESCRIPTION = 
            @"<div class=""wp-block-image""><figure class=""aligncenter""><img src=""https://youmanual.ru/wp-content/uploads/" + WEB_PathForImage + ImageName + @".jpg"" alt=" + Title + @" class=""wp-image-7991""/><figcaption>" + Title + @" </figcaption></figure></div>

            "
            +
            SiteDescription.GetDescription();
            
            END =
            @"
            <div> </div>
            <div> </div>
            <div> </div>
            <div>[adinserter block=""4""]</div>
            <div style=""text-align: center;""><span style=""font-size: 36px; color: #ff0000;""><strong><span style=""font-family: 'comic sans ms', sans-serif;""><a style=""color: #ff0000;"" href=""https://youmanual.ru/wp-content/uploads/" + WEB_PathForPDF + PDFName + @".pdf"" target=""_blank"" rel=""noopener noreferrer""><span style=""font-size: 36px; font-family: 'comic sans ms', sans-serif;"">Смотреть и скачать инструкцию</span></a></span></strong></span></div>
            <div>[adinserter block=""5""]</div>
            <div> </div>
            <div> </div>";
        }

        public void WriteToFile(string PathForWritePattern)
        {
            PathForWritePattern += FILE_NAME;
            PathForWritePattern += ".txt";
            File.AppendAllText(PathForWritePattern, DESCRIPTION, Encoding.GetEncoding(1251));
            File.AppendAllText(PathForWritePattern, END, Encoding.UTF8);
        }
    }
}
