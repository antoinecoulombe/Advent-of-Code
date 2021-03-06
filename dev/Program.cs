using dev.adventCalendar;

using System;

namespace dev
{
    class Program
    {
        public static bool IsLinux()
        {
            int p = (int)Environment.OSVersion.Platform;
            return (p == 4) || (p == 6) || (p == 128);
        }

        static void Main(string[] args)
        {
            AdventCalendar.ExecuteDay(13);
            Console.ReadKey();
        }
    }
}
