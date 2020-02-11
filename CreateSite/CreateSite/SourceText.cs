using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSite
{
    class SourceText
    {
        //Вернуть значение в обратном порядке
        public static string ReverseString(string StringForReverse)
        {
            char[] ArrayForReverse = StringForReverse.ToCharArray();
            Array.Reverse(ArrayForReverse);
            return new string(ArrayForReverse);
        }

        //Найти текст между строками-ключами
        public static void Find(string SourceString, string KeyStringBegin, string KeyStringEnd, out string RESULT, bool WithKeys = false, bool IsReverse = false)
        {
            RESULT = "Непредвиденная ошибка";
            //Список для внесения промежуточных результатов
            List<char> ResultCharList = new List<char>();

            /*
            Для того, чтобы производить поиск, начиная с конца файла определено это условие
            Глобальная строка инвертируется, начальная и конечная строки меняются местами и тоже инвертируются
            Далее поиск идёт таким же образом, как и при обычном поиске.
            */
            if (IsReverse)
            {
                SourceString = ReverseString(SourceString);

                string MemoryString = KeyStringBegin;
                KeyStringBegin = ReverseString(KeyStringEnd);
                KeyStringEnd = ReverseString(MemoryString);
            }

            CountForKeyString CountForStringBegin = new CountForKeyString(KeyStringBegin);
            CountForKeyString CountForStringEnd = new CountForKeyString(KeyStringEnd);

            bool IsFindStringKeyBegin = true;

            /*
            Работа происходит по следующему принципу. Перебираются все символы файла. 
            Поиск по строке начинается с элемента с индексом зависящим от параметра FirstIndex.
            Параметр EndIndex возвращает индекс последнего обработанного элемента.
            С какого конца лначать поиск зависит от параметра IsReverse.
            Если символ совпадает с первым символом строки-ключа, то счётчик строки повышается и, 
            при следующей итерации символ файла сравнивается со вторым символом строки и т. д.
            сначала ищется строка-начало, затем - строка-конец. При поиске строки-конца символ сохраниется
            в список. Затем список преобразовывается в возвращаемую строку.
            Если необходимо включить в результат строки-ключи, следует передать "true" в параметр WithKeys
            */

            for (int count = 0; count < (SourceString.Length - 1); count++)
            {
                if (IsFindStringKeyBegin)
                {
                    if (SourceString[count] == KeyStringBegin[CountForStringBegin.Count])
                    {
                        if (!CountForStringBegin.IsMax())
                        {
                            CountForStringBegin.Count++;
                        }
                        else
                        {
                            IsFindStringKeyBegin = false;
                        }
                    }

                    else
                    {
                        CountForStringBegin.Count = 0;
                    }

                    if ((!CountForStringBegin.IsMax()) && (count == (SourceString.Length - 1)))
                    {
                        RESULT = "Строка-ключ-начало не была найдена";
                    }
                }
                else
                {
                    ResultCharList.Add(SourceString[count]);

                    if (SourceString[count] == KeyStringEnd[CountForStringEnd.Count])
                    {
                        if (!CountForStringEnd.IsMax())
                        {
                            CountForStringEnd.Count++;
                        }
                        else
                        {
                            RESULT = ConvertLists.CharListToString(ResultCharList, KeyStringBegin, KeyStringEnd, WithKeys, IsReverse);
                            return;
                        }
                    }
                    else
                    {
                        CountForStringEnd.Count = 0;
                    }

                    if (((!CountForStringEnd.IsMax()) && (count == SourceString.Length)))
                    {
                        RESULT = "Строка-ключ-конец не была найдена";
                    }
                }
            }
        }


        //___________________________________________//
        //ПЕРЕГРУЗКА//
        //___________________________________________//


        //Перегрузка для того чтобы начинать поиск с определённого элемента строки
        public static void Find(string SourceString, string KeyStringBegin, string KeyStringEnd, out string RESULT, out int EndIndex, int FirstIndex = 0, bool WithKeys = false, bool IsReverse = false)
        {
            EndIndex = 0;
            RESULT = "Непредвиденная ошибка";
            //Список для внесения промежуточных результатов
            List<char> ResultCharList = new List<char>();

            /*
            Для того, чтобы производить поиск, начиная с конца файла определено это условие
            Глобальная строка инвертируется, начальная и конечная строки меняются местами и тоже инвертируются
            Далее поиск идёт таким же образом, как и при обычном поиске.
            */
            if (IsReverse)
            {
                SourceString = ReverseString(SourceString);

                string MemoryString = KeyStringBegin;
                KeyStringBegin = ReverseString(KeyStringEnd);
                KeyStringEnd = ReverseString(MemoryString);
            }

            CountForKeyString CountForStringBegin = new CountForKeyString(KeyStringBegin);
            CountForKeyString CountForStringEnd = new CountForKeyString(KeyStringEnd);

            bool IsFindStringKeyBegin = true;

            /*
            Работа происходит по следующему принципу. Перебираются все символы файла. 
            Поиск по строке начинается с элемента с индексом зависящим от параметра FirstIndex.
            Параметр EndIndex возвращает индекс последнего обработанного элемента.
            С какого конца лначать поиск зависит от параметра IsReverse.
            Если символ совпадает с первым символом строки-ключа, то счётчик строки повышается и, 
            при следующей итерации символ файла сравнивается со вторым символом строки и т. д.
            сначала ищется строка-начало, затем - строка-конец. При поиске строки-конца символ сохраниется
            в список. Затем список преобразовывается в возвращаемую строку.
            Если необходимо включить в результат строки-ключи, следует передать "true" в параметр WithKeys
            */

            for (int count = FirstIndex; count < (SourceString.Length - 1); count++)
            {
                if (IsFindStringKeyBegin)
                {
                    if (SourceString[count] == KeyStringBegin[CountForStringBegin.Count])
                    {
                        if (!CountForStringBegin.IsMax())
                        {
                            CountForStringBegin.Count++;
                        }
                        else
                        {
                            IsFindStringKeyBegin = false;
                        }
                    }

                    else
                    {
                        CountForStringBegin.Count = 0;
                    }

                    if ((!CountForStringBegin.IsMax()) && (count == (SourceString.Length - 1)))
                    {
                        RESULT = "Строка-ключ-начало не была найдена";
                    }
                }
                else
                {
                    ResultCharList.Add(SourceString[count]);

                    if (SourceString[count] == KeyStringEnd[CountForStringEnd.Count])
                    {
                        if (!CountForStringEnd.IsMax())
                        {
                            CountForStringEnd.Count++;
                        }
                        else
                        {
                            EndIndex = count;
                            RESULT = ConvertLists.CharListToString(ResultCharList, KeyStringBegin, KeyStringEnd, WithKeys, IsReverse);
                            return;
                        }
                    }
                    else
                    {
                        CountForStringEnd.Count = 0;
                    }

                    if (((!CountForStringEnd.IsMax()) && (count == SourceString.Length)))
                    {
                        RESULT = "Строка-ключ-конец не была найдена";
                    }
                }
            }
        }
    }

    //Класс для управления и ограничения счётчиков для поиска текста
    class CountForKeyString
    {
        string KeyString;
        int count;
        public CountForKeyString(string GetString)
        {
            KeyString = GetString;
            count = 0;
        }

        public int Count
        {
            get { return count; }
            set
            {
                if (value >= 0 && value <= (KeyString.Length - 1))
                    count = value;
            }
        }

        public bool IsMax()
        {
            if (count == (KeyString.Length - 1)) { return true; }
            else { return false; }
        }
    }

    //Преобразовать список в строку
    static class ConvertLists
    {
        //Из списка символом в строку с учётом границ и булевых перемнных
        public static string CharListToString(List<char> ResultCharList, string KeyStringBegin, string KeyStringEnd, bool WithKeys, bool IsReverse)
        {
            int EndOfStringResult = ResultCharList.Count - KeyStringEnd.Length;
            int CountForDel = ResultCharList.Count - EndOfStringResult;
            ResultCharList.RemoveRange(EndOfStringResult, CountForDel);
            char[] ResultCharArray = new char[ResultCharList.Count];
            ResultCharArray = ResultCharList.ToArray();
            string ResultString;
            if (WithKeys)
            {
                string ResultFromList = new string(ResultCharArray);
                ResultString = KeyStringBegin + ResultFromList + KeyStringEnd;
            }
            else
            {
                ResultString = new string(ResultCharArray);
            }
            if (!IsReverse)
            {
                return ResultString;
            }
            else
            {
                return SourceText.ReverseString(ResultString);
            }
        }

        //Перегрузка для преобразования списка символов в строку без учета границ и булевых переменных
        public static string CharListToString(List<char> ResultCharList)
        {
            char[] ResultCharArray = ResultCharList.ToArray();
            string ResultString = new string(ResultCharArray);
            return ResultString;
        }

        //Из списка строк в массив строк
        public static string[] StringListToArray(List<string> StringList)
        {
            string[] ResultArray = new string[StringList.Count];
            for(int i = 0; i < StringList.Count; i++)
            {
                ResultArray[i] = StringList[i];
            }
            return ResultArray;
        }
    }

    static class Sort
    {
        public static string[] StringsByAlfabet(string[] Strings)
        {
            string[] result = new string[Strings.Length];
            int count = 0;
            var OrderedStrings = from str in Strings
                                 orderby str
                                 select str;
            foreach(string str in OrderedStrings)
            {
                result[count] = str;
                count++;
            }
            return result;
        }
    }
}

