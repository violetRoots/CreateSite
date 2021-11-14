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
            string TagDescription = @" инструкция на пылесос. Правила пользования и режимы работы. Скачать руководство по эксплуатации PDF. Настройка и очистка.";
            string PageParentId = @"0";

            string PathImageDownload = @"C:\Users\Geralt\Desktop\TES\";
            string PathPDFDownload = @"C:\Users\Geralt\Desktop\TES\";
            string Derictory = @"C:\Users\Geralt\Desktop\TES\";
            string CsvSiteName = "Content";
            string CsvAdditionalName = "Additional";

            string WEB_PathImage = @"Image/all/";
            string WEB_PathPDF = @"PDF/all/";

            bool IsWriteToCsv = true;
            bool IsAppendToFile = false;
            bool IsWriteLogFile = true;

            ReadFile ReadSite;

            if (!IsAppendToFile)
            {
                Console.WriteLine("Выставленные параметры прдеполагают полную перезапись файла лог-файла. Вы уверены, что хотите перезаписать файлы?\n Если да, то для продолжения нажмите любую кнопку на клавиатуре.");
                Console.ReadKey();
            }

            WritePatternCsv WriteSiteCsv = null;
            WritePatternCsv WriteAdditionalCsv = null;
            try
            {
                if (IsWriteToCsv)
                {
                    WriteSiteCsv = new WritePatternCsv(Derictory, CsvSiteName, IsAppendToFile);
                    WriteAdditionalCsv = new WritePatternCsv(Derictory, CsvAdditionalName, IsAppendToFile, false);
                }
            }
            catch(Exception exp)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("ПРИ СОЗДАНИИ .csv ФАЙЛА ПРОИЗОШЛА ОШИБКА:  \n" + exp.ToString());
                Console.WriteLine("-----------------");
            }

            for (int count = 0; count < args.Length; count++)
            {
                try
                {
                    Console.WriteLine("Получение доступа к html-странице");
                ReadSite = new ReadFile(args[count]);
                //ReadSite = new ReadFile(@"C:\Users\Geralt\Desktop\test.html");

                Console.WriteLine("Поиск названия продукта...");
                    Title SiteTitle = new Title(ReadSite);
                    string Title = SiteTitle.GetTitle();
                    Console.WriteLine($"Название продукта: {Title}");


                    Console.WriteLine("Поиск ссылки на PDF...");
                    PDF SitePDF = new PDF(ReadSite);
                    string PDFAddress = SitePDF.GetAddress();
                    Console.WriteLine($"Ссылка найдена: {PDFAddress}");
                    string PDFName = SitePDF.GetNamePDF();
                    Console.WriteLine($"Имя PDF: {PDFName}.pdf");
                    Console.WriteLine("Скачивание PDF...");
                    SitePDF.DownloadPDF(PathPDFDownload);
                    Console.WriteLine($"PDF загружен по пути {PathPDFDownload}{PDFName}.pdf");

                    Console.WriteLine("Поиск ссылки на картинку...");
                    Image SiteImage = new Image(ReadSite);
                    string ImageAddress = SiteImage.GetAddress();
                    Console.WriteLine($"Ссылка найдена: {ImageAddress}");
                    string ImageName = SiteImage.GetNameImage();
                    Console.WriteLine($"Имя картинки: {ImageName}");
                    Console.WriteLine("Скачивание картинки...");
                    SiteImage.DownloadImage(PathImageDownload);
                    Console.WriteLine($"Картинка загружена по пути {PathImageDownload}{ImageName}.pdf");

                    Console.WriteLine("Считывание таблицы характеристик");
                    Description SiteDescription = new Description(ReadSite);
                    string Description = SiteDescription.GetDescription();

                    if (WriteSiteCsv != null && WriteAdditionalCsv != null)
                    {
                        Console.WriteLine("Запись в текстовый файл (.csv)...");
                        WriteSiteCsv.AppendLineToSiteFile(Title, WEB_PathImage, WEB_PathPDF, ImageName, PDFName, SiteDescription, TagDescription, PageParentId);
                        WriteAdditionalCsv.AppendLineToAdditionalFile(Title, PDFName);
                        if (IsWriteLogFile)
                        {
                            LogFile.WriteLog(Title, Derictory, CsvSiteName + "-log", IsAppendToFile);
                            IsAppendToFile = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Запись в текстовый файл (.txt)...");
                        WritePatternTxt WriteSite = new WritePatternTxt(WEB_PathImage, WEB_PathPDF, Title, ImageName, PDFName, SiteDescription);
                        WriteSite.WriteToFile(Derictory);
                    }
                }
                catch(Exception exp)
                {
                    Console.WriteLine("-----------------");
                    Console.WriteLine("ЧТО-ТО ПОШЛО НЕ ТАК! ОШИБКА:  \n" + exp.ToString());
                    Console.WriteLine("ФАЙЛ ПО НОМЕРУ " + (count + 1) + " ПРОПУЩЕН");
                    Console.WriteLine("-----------------");
                }
            }

            ChangeFileEncoding(Derictory + CsvSiteName + ".csv", Encoding.UTF8, new UTF8Encoding(true));

            Console.WriteLine("Нажми на любую кнопку, чтобы закрыть консоль");
            Console.ReadKey();
        }

        private static bool ChangeFileEncoding(string _fileFullName, Encoding _oldEncoding, Encoding _newEncoding)
        {
            try
            {
                File.WriteAllText(_fileFullName, File.ReadAllText(_fileFullName, _oldEncoding), _newEncoding);
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("ПРИ ПЕРЕКОДИРОВКИ .csv ФАЙЛА ПРОИЗОШЛА ОШИБКА:  \n" + exp.ToString());
                Console.WriteLine("-----------------");
            }

            return false;
        }
    }
}
