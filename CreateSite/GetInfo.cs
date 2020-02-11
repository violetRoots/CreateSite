using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CreateSite
{
    public class GetInfo : ReadFile
    {
        public GetInfo()
        {
            ReadFile j = new ReadFile(@"C:\Users\Adam\Desktop\t.html");
            Console.WriteLine(GetGlobalString());
        }
    }
}
