using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Helpers
{
   public class Utilities
    {
        public static string GenerateNumber(int length)
        {
            Random random = new Random();
            string r = "";
            for (int i = 1; i < length + 1; i++)
                r += random.Next(0, 9).ToString();

            return r;
        }
    }
}
