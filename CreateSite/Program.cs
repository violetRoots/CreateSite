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
            string TagDescription = @"some description";
            string PageParentId = @"37";

            string PathImageDownload = @"C:\Users\Антон\Desktop\";
            string PathPDFDownload = @"C:\Users\Антон\Desktop\";
            string PathToFile = @"C:\Users\Антон\Desktop\";
            string PathToLogFile = @"C:\Users\Антон\Desktop\";

            string WEB_PathImage = @"Image/stiralnye-mashiny_/";
            string WEB_PathPDF = @"PDF/stiralnye-mashiny_aeg/";

            bool IsWriteToCsv = true;
            bool IsAppendToFile = true;
            bool IsWriteLogFile = true;

            string CsvFileName = "Test";

            ReadFile ReadSite;

            if (!IsAppendToFile)
            {
                Console.WriteLine("Выставленные параметры прдеполагают полную перезапись файла лог-файла. Вы уверены, что хотите перезаписать файлы?\n Если да, то для продолжения нажмите любую кнопку на клавиатуре.");
                Console.ReadKey();
            }

            WritePatternCsv WriteSiteCsv = null;
            try
            {
                if (IsWriteToCsv)
                {
                    WriteSiteCsv = new WritePatternCsv(PathToFile, CsvFileName, IsAppendToFile);
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

                    if (WriteSiteCsv != null)
                    {
                        Console.WriteLine("Запись в текстовый файл (.csv)...");
                        WriteSiteCsv.AppendLine(Title, WEB_PathImage, WEB_PathPDF, ImageName, PDFName, SiteDescription, TagDescription, PageParentId);
                        if (IsWriteLogFile)
                        {
                            LogFile.WriteLog(Title, PathToLogFile, CsvFileName + "-log", IsAppendToFile);
                            IsAppendToFile = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Запись в текстовый файл (.txt)...");
                        WritePatternTxt WriteSite = new WritePatternTxt(WEB_PathImage, WEB_PathPDF, Title, ImageName, PDFName, SiteDescription);
                        WriteSite.WriteToFile(PathToFile);
                    }
                }
                catch(Exception exp)
                {
                    Console.WriteLine("-----------------");
                    Console.WriteLine("ЧТО-ТО ПОШЛО НЕ ТАК! ОШИБКА:  \n" + exp.ToString());
                    Console.WriteLine("ФАЙЛ ПО НОМЕРУ " + (count+1) + " ПРОПУЩЕН");
                    Console.WriteLine("-----------------");
                }
            }

            ChangeFileEncoding(PathToFile + CsvFileName + ".csv", Encoding.UTF8, new UTF8Encoding(true));

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
