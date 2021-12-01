using System;
using System.Diagnostics;

namespace dev.adventCalendar
{
  static class AdventCalendar
  {
    const int CURRENT_YEAR = 2021;
    private static Stopwatch watch = new Stopwatch();

    public static void ExecuteAll()
    {
      for (int i = 2015; i <= CURRENT_YEAR; ++i)
        for (int j = 1; j <= 25; ++j)
          ExecuteDay(j, i);
    }

    public static void ExecuteDay(int d, int y = CURRENT_YEAR)
    {
      string yString = (y < 2000 ? y + 2000 : y).ToString();
      string dString = (d < 10 ? "0" : "") + d.ToString();
      Day day = null;

      try
      {
        day = (Day)Activator.CreateInstance(Type.GetType($"dev.adventCalendar._{yString}.Day{dString}"));
      }
      catch (Exception)
      {
        Console.WriteLine("Le défi pour ce jour n'est pas encore implémenté.");
        return;
      }

      string date = $"{yString}/{dString}";
      Console.WriteLine($"{date} - Execution Started.");
      Execute(day.ExecuteFirst);
      Execute(day.ExecuteSecond);
      Console.WriteLine($"{date} - Execution Ended.");
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
