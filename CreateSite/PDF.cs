using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CreateSite
{
    class PDF
    {
        ReadFile ORIGIN;
        string ADDRESS;
        string KEY_PDF_BEGIN;
        string KEY_PDF_END;

        public PDF(ReadFile ReadSite)
        {
            ORIGIN = ReadSite;
            KEY_PDF_BEGIN = @"<a _ngcontent-serverapp-c37="""" class=""attachment""";
            KEY_PDF_END = @"<div _ngcontent-serverapp-c37="""" class=""file-type"">";
        }

        //Получить ссылку на файл PDF
        public string GetAddress()
        {
            string IntermediateResult;
            SourceText.Find(ORIGIN.GetGlobalString(), KEY_PDF_BEGIN, KEY_PDF_END, out IntermediateResult, false, false);
            SourceText.Find(IntermediateResult, @"href=""", "\"", out ADDRESS);
            return ADDRESS;
        }

        //Переименовать PDF
        public string GetNamePDF()
        {
            Title TitlePDF = new Title(ORIGIN);
            string Title = TitlePDF.GetTitle().Replace(' ', '-');
            Title = Title.Replace('\\', '-');
            Title = Title.Replace('/', '-');
            return Title;
        }

        //Скачать PDF
        public void DownloadPDF(string Path)
        {
            Path += GetNamePDF();
            Path += ".pdf";
            WebClient webClient = new WebClient();
            webClient.DownloadFile(GetAddress(), Path);
        }
    }
}
