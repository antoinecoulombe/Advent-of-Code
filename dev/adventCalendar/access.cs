using System;
using System.Diagnostics;

namespace dev.adventCalendar
{
    static class AdventCalendar
    {
        private static Stopwatch watch = new Stopwatch();

        public static void ExecuteDay(int d, int y = 2020)
        {
            Type t = Type.GetType("dev.adventCalendar._" + y.ToString() + 
                ".Day" + (d < 10 ? "0" : "") + d.ToString());
            Day day = null;

            try
            {
                day = (Day)Activator.CreateInstance(t);
            }
            catch (Exception)
            {
                Console.WriteLine("Le défi pour ce jour n'est pas encore implémenté.");
                return;
            }

            Console.WriteLine("Execution Started.");
            Execute(day.ExecuteFirst);
            Execute(day.ExecuteSecond);
            Console.WriteLine("Execution Ended.");
        }

        private static void Execute(Func<string> method)
        {
            watch.Restart();
            string answer = method();
            watch.Stop();
            WriteTimedExecute(answer);
        }

        private static void WriteTimedExecute(string answer)
            => Console.WriteLine(answer + " - " + GetReadableTime());

        private static string GetReadableTime()
            => watch.ElapsedMilliseconds < 1000 ?
                watch.ElapsedMilliseconds.ToString() + "ms" :
                decimal.Divide(watch.ElapsedMilliseconds, 1000).ToString() + "s";
    }
}
