using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CreateSite
{
    class Image
    {
        ReadFile ORIGIN;
        string ADDRESS;
        string KEY_IMAGE_BEGIN;
        string KEY_IMAGE_END;

        //Инициализация
        public Image(ReadFile ReadSite)
        {
            ORIGIN = ReadSite;
            KEY_IMAGE_BEGIN = @"class=""c-media-container__image sel-pdp-product-image""";
            KEY_IMAGE_END = @"holder=""#gtmZoomPhoto"" data-pushable=""true"" data-action=""click"">";
        }

        //Получить ссылку на картинку
        public string GetAddress()
        {
            string IntermediateRessult, Result;
            SourceText.Find(ORIGIN.GetGlobalString(), KEY_IMAGE_BEGIN, KEY_IMAGE_END, out IntermediateRessult, false, true);
            SourceText.Find(IntermediateRessult, "//", ".jpg", out Result, true, true);
            return ADDRESS = "http:" + Result;
        }

        //Переименовать картинку
        public string GetNameImage()
        {
            Title TitleImage = new Title(ORIGIN);
            string Title = TitleImage.GetTitle().Replace(' ', '_');
            Title = Title.Replace('\\', '_');
            Title = Title.Replace('/', '_');
            return Title;
        }

        //Скачать картинку
        public void DownloadImage(string Path)
        {
            Path += GetNameImage();
            Path += ".jpg";
            WebClient webClient = new WebClient();
            webClient.DownloadFile(GetAddress(), Path);
        }
    }
}
