using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace CreateSite
{
    class Program
    {
        static void Main(string[] args)
        {
            string TegTitle = @"Ноут";
            string TegDescription = @"Крутой моник";

            string PathImageDownload = @"C:\Users\LionsGate\Desktop\";
            string PathPDFDownload = @"C:\Users\LionsGate\Desktop\";
            string PathTXT = @"C:\Users\LionsGate\Desktop\";

            string WEB_PathImage = @"Image/stiralnye-mashiny_/";
            string WEB_PathPDF = @"PDF/stiralnye-mashiny_aeg/";

            ReadFile ReadSite;

            for (int count = 0; count < args.Length; count++)
            {
                try
                {
                    Console.WriteLine("Получение доступа к html-странице");
                    ReadSite = new ReadFile(args[count]);

                    Console.WriteLine("Поиск названия продукта...");
                    Title SiteTitle = new Title(ReadSite);
                    string Title = SiteTitle.GetTitle();
                    Console.WriteLine("Название продукта:\n" + Title);


                    Console.WriteLine("Поиск ссылки на PDF...");
                    PDF SitePDF = new PDF(ReadSite);
                    string PDFAddress = SitePDF.GetAddress();
                    Console.WriteLine("Ссылка найдена");
                    string PDFName = SitePDF.GetNamePDF();
                    Console.WriteLine("Имя PDF:\n" + PDFName);
                    Console.WriteLine("Скачивание PDF...");
                    SitePDF.DownloadPDF(PathPDFDownload);

                    Console.WriteLine("Поиск ссылки на картинку...");
                    Image SiteImage = new Image(ReadSite);
                    string ImageAddress = SiteImage.GetAddress();
                    Console.WriteLine("Ссылка найдена.");
                    string ImageName = SiteImage.GetNameImage();
                    Console.WriteLine("Имя картинки:\n" + ImageName);
                    Console.WriteLine("Скачивание картинки...");
                    SiteImage.DownloadImage(PathImageDownload);

                    Console.WriteLine("Считывание таблицы характеристик");
                    Description SiteDescription = new Description(ReadSite);
                    string Description = SiteDescription.GetDescription();

                    Console.WriteLine("Запись в текстовый файл...");
                    WritePattern WriteSite = new WritePattern(WEB_PathImage, WEB_PathPDF,TegTitle, Title, ImageName, PDFName, TegDescription, SiteDescription);
                    WriteSite.WritePttern(PathTXT);
                }
                catch(Exception exp)
                {
                    Console.WriteLine("-----------------");
                    Console.WriteLine("ЧТО-ТО ПОШЛО НЕ ТАК! ОШИБКА:  \n" + exp.ToString());
                    Console.WriteLine("ФАЙЛ ПО НОМЕРУ " + (count+1) + " ПРОПУЩЕН");
                    Console.WriteLine("-----------------");
                }
            }
            Console.WriteLine("Нажми на любую кнопку, чтобы закрыть консоль");
            Console.ReadKey();
        }
    }
}
