using System;

namespace dev.adventCalendar
{
    static class AdventCalendar
    {
        //AdventCalendar.ExecuteDay(1);
        //AdventCalendar.ExecuteDay(1, 2019);
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

            Console.WriteLine(day.ExecuteFirst());
            Console.WriteLine(day.ExecuteSecond());
        }
    }
}
