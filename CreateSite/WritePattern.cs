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

        public WritePattern(string WEB_PathForImage, string WEB_PathForPDF, string Title, string ImageName, string PDFName, string TegDescription, string KeyWords, Description SiteDescription)
        {
            NAME_OF_PATTERN = Title;

            HEAD =
                @"
            <head>
            <meta name=""keywords"" content="""
            +
            Title
            +
            ", "
            +
            KeyWords
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
            @""">
            </head>";

            DESCRIPTION = 
            @"<div class=""wp-block-image""><figure class=""aligncenter""><img src=""https://youmanual.ru/wp-content/uploads/" + WEB_PathForImage + ImageName + @".jpg"" alt=" + Title + @" class=""wp-image-7991""/><figcaption>" + Title + @" </figcaption></figure></div>

            "
            +
            SiteDescription.GetDescription();
            
            END =
            @"
            <div> </div>
            <div> </div>

            <!-- Yandex.RTB R-A-491989-10 -->
            <div id=""yandex_rtb_R-A-491989-10""></div>
            <script type=""text/javascript"">
                (function(w, d, n, s, t) {
                    w[n] = w[n] || [];
                    w[n].push(function() {
                        Ya.Context.AdvManager.render({
                            blockId: ""R-A-491989-10"",
                            renderTo: ""yandex_rtb_R-A-491989-10"",
                            async: true
                        });
                    });
                    t = d.getElementsByTagName(""script"")[0];
                    s = d.createElement(""script"");
                    s.type = ""text/javascript"";
                    s.src = ""//an.yandex.ru/system/context.js"";
                    s.async = true;
                    t.parentNode.insertBefore(s, t);
                })(this, this.document, ""yandexContextAsyncCallbacks"");
            </script>

            <div> </div>
            <div>[pdfviewer]https://youmanual.ru/wp-content/uploads/" + WEB_PathForPDF + PDFName + @".pdf[/pdfviewer]</div>
            <div>
            <div> </div>
            <div> </div>

            <!-- Yandex.RTB R-A-491989-11 -->
            <div id=""yandex_rtb_R-A-491989-11""></div>
            <script type=""text/javascript"">
                (function(w, d, n, s, t) {
                    w[n] = w[n] || [];
                    w[n].push(function() {
                        Ya.Context.AdvManager.render({
                            blockId: ""R-A-491989-11"",
                            renderTo: ""yandex_rtb_R-A-491989-11"",
                            async: true
                        });
                    });
                        t = d.getElementsByTagName(""script"")[0];
                        s = d.createElement(""script"");
                        s.type = ""text/javascript"";
                        s.src = ""//an.yandex.ru/system/context.js"";
                        s.async = true;
                        t.parentNode.insertBefore(s, t);
                    })(this, this.document, ""yandexContextAsyncCallbacks"");
                </script>

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
