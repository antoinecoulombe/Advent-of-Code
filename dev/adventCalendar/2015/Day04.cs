using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace dev.adventCalendar._2015
{
    class Day04 : Day
    {
        private static string CreateMD5(string input)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = MD5.Create().ComputeHash(inputBytes);

            StringBuilder sb = new();
            for (int i = 0; i < hashBytes.Length; i++)
                sb.Append(hashBytes[i].ToString("X2"));
            return sb.ToString();
        }

        private static int GetNumberStartingWith(string start)
        {
            var s = "yzbqklnj";
            int i = 0;
            while (!CreateMD5(s + i.ToString()).StartsWith(start))
                ++i;
            return i;
        }

        public override string ExecuteFirst()
            => GetNumberStartingWith("00000").ToString();

        public override string ExecuteSecond()
            => GetNumberStartingWith("000000").ToString();
    }
}
