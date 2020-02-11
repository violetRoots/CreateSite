using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSite
{
    class Description
    {
        ReadFile ORIGIN;
        string KEY_STRING_BEGIN;
        string KEY_STRING_END;
        string DEL_STRING_1, DEL_STRING_2, DEL_STRING_3;

        public Description(ReadFile ReadSite)
        {
            ORIGIN = ReadSite;
            KEY_STRING_BEGIN = @"<div class=""product-details-tables-holder sel-characteristics-table"">";
            KEY_STRING_END = @"<div class=""product-details-text sel-characteristics-text"">";
            DEL_STRING_1 = @" class=""table table-striped product-details-table""";
            DEL_STRING_2 = @"<span class=""product-details-overview-specification"">";
            DEL_STRING_3 = @"</span>";
        }

        public string GetDescription()
        {
            string  Result;
            SourceText.Find(ORIGIN.GetGlobalString(), KEY_STRING_BEGIN, KEY_STRING_END, out Result);

            int count = 0;
            bool IsAllStringDeleted = false;
            string DeletedString;
            while (!IsAllStringDeleted)
            {
                SourceText.Find(ORIGIN.GetGlobalString(), @"<span class=""wrapper-specification-ico-help"">", @"span>", out DeletedString, out count, count, true);
                Result = Result.Replace(DeletedString, "");
                if (DeletedString == "Непредвиденная ошибка" || count == ORIGIN.GetGlobalString().Length - 1)
                    IsAllStringDeleted = true;
            }

            Result = Result.Replace(DEL_STRING_1, "");
            Result = Result.Replace(DEL_STRING_2, "");
            Result = Result.Replace(DEL_STRING_3, "");
            return Result;
        }
    }
}
