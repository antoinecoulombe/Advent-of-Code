using dev.adventCalendar;

using System;

namespace dev
{
    class Program
    {
        #region Utilities

        public static bool IsLinux()
        {
            int p = (int)Environment.OSVersion.Platform;
            return (p == 4) || (p == 6) || (p == 128);
        }

        #endregion

        static void Main(string[] args)
        {
            AdventCalendar.ExecuteDay(18);
            Console.ReadKey();
        }
    }
}
