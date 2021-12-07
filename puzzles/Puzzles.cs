using System;
using System.Diagnostics;
using System.IO;

namespace aoc.puzzles
{
  static class Puzzles
  {
    const int CURRENT_YEAR = 2021;

    private static Stopwatch watch = new Stopwatch();

    public static void ExecuteAll()
    {
      for (int i = 2015; i <= CURRENT_YEAR; ++i)
        for (int j = 1; j <= 25; ++j)
          Execute(j, i);
    }

    private static void CreateFiles(string year, string day)
    {
      Directory.CreateDirectory(Path.Combine(Day.GetBasePath(), "puzzles", year, "ressources", $"day_{day}"));
      Console.WriteLine($"Created ressource folder.");
      string template = File.ReadAllText(Path.Combine(Day.GetBasePath(), "puzzles", "template.txt"))
        .Replace("{PUZZLE_YEAR}", year).Replace("{PUZZLE_DAY}", day);
      File.WriteAllText(Path.Combine(Day.GetBasePath(), "puzzles", year, $"Day{day}.cs"), template);
      Console.WriteLine("Created c# file from template.");
    }

    public static void Execute(int d, int y = CURRENT_YEAR)
    {
      string yString = (y < 2000 ? y + 2000 : y).ToString();
      string dString = (d < 10 ? "0" : "") + d.ToString();
      Day day = null;

      try
      {
        day = (Day)Activator.CreateInstance(Type.GetType($"{Day.projectFolderName}.{Day.codeFolderName}._{yString}.Day{dString}"));
      }
      catch (Exception)
      {
        if (d < 1 || d > 25)
          Console.WriteLine("Error - Day must be between 1 and 25.");
        else if (int.Parse(yString) < 2015 || int.Parse(yString) > CURRENT_YEAR + 1)
          Console.WriteLine($"Error - Year must be between 2015 and {CURRENT_YEAR + 1}.");
        else
        {
          Console.WriteLine($"{yString}/{dString} does not exist.");
          CreateFiles(yString, dString);
        }
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
