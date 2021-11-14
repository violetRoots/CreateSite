﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSite
{
    //Класс инкасулирующий методы связаные с получением доступа к названию продукта
    class Title
    {
        ReadFile ORIGIN;
        string TITLE;
        string KEY_TITLE_BEGIN;
        string KEY_TITLE_END;

        //Конструктор определяющий поток, и строки-ключи для поиска названия
        //Возможно потом ключи лучше вынести в начало программы для удобства их изменения 
        public Title(ReadFile Origin)
        {
            ORIGIN = Origin;
            KEY_TITLE_BEGIN = @"<h2 _ngcontent-serverapp-c179="""" class=""bar__product-title"">";
            KEY_TITLE_END = @"</h2>";
        }

        //Метод, определяющий, является ли символ буквой английского алфавита
        //Необходим для исключения русских слов в начале названия
        bool IsEnglishSymbol(char Symbol)
        {
            return (Symbol >= 'a' && Symbol <= 'z') || (Symbol >= 'A' && Symbol <= 'Z');
        }

        bool IsNormalSymbol(char Symbol)
        {
            return (Symbol >= 'a' && Symbol <= 'z') || (Symbol >= 'A' && Symbol <= 'Z') || Symbol == ' ' || Char.IsDigit(Symbol);
        }

        //Метод, возвращающий название продукта
        public string GetTitle()
        {
            bool StartWrite = false;
            string IntermediateResult;
            SourceText.Find(ORIGIN.GetGlobalString(), KEY_TITLE_BEGIN, KEY_TITLE_END, out IntermediateResult);
            for(int i = 0; i < IntermediateResult.Length; i++)
            {
                if (IsNormalSymbol(IntermediateResult[i]))
                {
                    if (IsEnglishSymbol(IntermediateResult[i]) && !StartWrite)
                    {
                        StartWrite = true;

                    }
                    if (StartWrite && i != (IntermediateResult.Length - 1) && i != IntermediateResult.Length - 2)
                    {
                        TITLE += IntermediateResult[i];
                    }
                }
            }
            return TITLE;
        }
    }
}
