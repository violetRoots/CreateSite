using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace CreateSite
{
    class ReadFile
    {
        //Создать поток для чтения
        StreamReader STREAM_FOR_READ;
        string GLOBAL_STRING;
        //string SITE_PATH_TO_OPEN;

        //Считать весь файл
        public ReadFile(string path)
        {
            STREAM_FOR_READ = new StreamReader(path, Encoding.GetEncoding(1251));
            GLOBAL_STRING = STREAM_FOR_READ.ReadToEnd();
            STREAM_FOR_READ.Close();
        }

        //Вернуть значение строки, содержащей значение всего файла
        public string GetGlobalString()
        {
            return GLOBAL_STRING;
        }

        /*
        public string GetSitePathToOPen()
        {
            return SITE_PATH_TO_OPEN;
        }


        //Вернуть массив со всеми адресами
        public string[] GetAllAddress()
        {
            bool IsAllAddressFound = true;
            int EndIndex, CountArr = 0, FirstIndex = 0;
            string OutString;
            List<string> ListString = new List<string>();
            while (IsAllAddressFound)
            {
                SourceText.Find(GLOBAL_STRING, @"HREF=""", @"""", out OutString, out EndIndex, FirstIndex);
                ListString.Add(OutString);
                FirstIndex = EndIndex;
                if(EndIndex == GLOBAL_STRING.Length - 1 || OutString[0] != 'h')
                {
                    IsAllAddressFound = false;
                    ListString.RemoveAt(ListString.Count - 1);
                }
                CountArr++;
            }
            return ConvertLists.StringListToArray(ListString);
        }*/

        //Вернуть массив имён
        public string[] GetAllNames(string[] AddressArray)
        {
            string IntermediateResult;
            string[] NameArray = new string[AddressArray.Length];
            for(int i = 0; i < AddressArray.Length; i++)
            {
                SourceText.Find(AddressArray[i], "https://www.mvideo.ru/products/", "/specificatio", out IntermediateResult);
                IntermediateResult.Replace('/', '-');
                NameArray[i] = IntermediateResult;
            }
            return NameArray;
        }

        public string[] GetAllPathes(string Path, string[] Names)
        {
            string[] PathesArray = new string[Names.Length];
            string IntermediateResult;
            for(int i = 0; i < Names.Length; i++)
            {
                IntermediateResult = "";
                IntermediateResult += Path;
                IntermediateResult += Names[i];
                IntermediateResult += ".txt";
                PathesArray[i] = IntermediateResult;
            }
            return PathesArray;
        }

        //Скачать сайт
        public void DownLoadSite(string URL, string Path)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile(URL, Path);
        }
    }
}