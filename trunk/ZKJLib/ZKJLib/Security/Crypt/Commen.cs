using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZKJLib.Security.Crypt
{
    public class Commen
    {
        public static  string StdLen(string strIn, int Len)
        {
            if (strIn.Length < Len)
            {
                int N = strIn.Length;
                for (int i = 0; i < Len - N; i++)
                {
                    strIn += " ";
                }
                return strIn;
            }

            if (strIn.Length > Len)
            {
                return strIn.Substring(0, Len);
            }
            return strIn;
        }
    }
}
