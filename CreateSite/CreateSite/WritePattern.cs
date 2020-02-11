using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CreateSite
{
    class WritePattern
    {
        string HEAD;
        string DESCRIPTION;
        string END;
        string NAME_OF_PATTERN;

        public WritePattern(string WEB_PathForImage, string WEB_PathForPDF,string TegTitle, string Title, string ImageName, string PDFName, string TegDescription, Description SiteDescription)
        {
            NAME_OF_PATTERN = Title;

            HEAD =
                @"
            <head>
            <title>"
            +
            TegTitle
            +
            " "
            +
            Title
            +
            @"</title>
            <meta name=""keywords"" content="""
            +
            Title
            +
            @""">
            <meta name=""description"" content="""
            +
            Title
            +
            " "
            +
            TegDescription
            +
            @""">";
            DESCRIPTION = 
            @"</head>

            <div class=""wp-block-image""><figure class=""aligncenter""><img src=""https://youmanual.ru/wp-content/uploads/" + WEB_PathForImage + ImageName + @".jpg"" alt=" + Title + @" class=""wp-image-7991""/><figcaption>" + Title + @" </figcaption></figure></div>

            "
            +
            SiteDescription.GetDescription();
            
            END =
            @"
            <div> </div>
            <div> </div>
            <div> </div>
            <div>[pdfviewer]https://youmanual.ru/wp-content/uploads/" + WEB_PathForPDF + PDFName + @".pdf[/pdfviewer]</div>
            <div>
            <div> </div>
            <div> </div>
            </div>
            <div> </div>
            <div>
            <div> </div>
            <div> </div>
            <div style=""text-align: center;""><span style=""font-size: 36px; color: #ff0000;""><strong><span style=""font-family: 'comic sans ms', sans-serif;""><a style=""color: #ff0000;"" href=""https://youmanual.ru/wp-content/uploads/" + WEB_PathForPDF + PDFName + @".pdf"" target=""_blank"" rel=""noopener noreferrer""><span style=""font-size: 36px; font-family: 'comic sans ms', sans-serif;"">Смотреть и скачать инструкцию</span></a></span></strong></span></div>
            <div> </div>
            <div> </div>
            <div> </div>
            </div>
            <div>
            <p>Для просмотра содержания инструкции на компьютере Вам понадобится программа Adobe Reader или DjVu. Если на Вашем компьютере они не установлены, то Adobe Reader можно скачать с сайта <a href=""https://get.adobe.com/ru/reader/"" target=""_blank"" rel=""noopener noreferrer"">Adobe</a> , а DjVu с сайта <a href=""http://djvu-info.ru/djvu_reader"" target=""_blank"" rel=""noopener noreferrer"">DjVu</a> </p>
            </div>
            ";
        }

        public void WritePttern(string PathForWritePattern)
        {
            PathForWritePattern += NAME_OF_PATTERN;
            PathForWritePattern += ".txt";
            File.WriteAllText(PathForWritePattern, HEAD, Encoding.UTF8);
            File.AppendAllText(PathForWritePattern, DESCRIPTION, Encoding.GetEncoding(1251));
            File.AppendAllText(PathForWritePattern, END, Encoding.UTF8);
        }
    }
}
