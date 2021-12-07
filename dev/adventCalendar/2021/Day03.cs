using System;
using System.Collections.Generic;
using System.Linq;

namespace dev.adventCalendar._2021
{
  class Day03 : Day
  {
    public override string ExecuteFirst()
    {
      var lines = GetFileLines();
      string gamma = "";
      string epsilon = "";
      for (int i = 0; i < lines[0].Length; ++i)
      {
        var ones = lines.Count(x => x[i] == '1');
        gamma += ones > lines.Length / 2 ? "1" : "0";
        epsilon += ones > lines.Length / 2 ? "0" : "1";
      }
      return (Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2)).ToString();
    }

    private string FindRating(List<string> lines, bool findMax = true)
    {
      if (!findMax)
        Console.Write("");

      for (int i = 0; i < lines[0].Length; ++i)
      {
        decimal length = lines.Count();
        decimal ones = lines.Count(x => x[i] == '1');
        char max = ones >= length / 2 ? '1' : '0';
        char min = max == '1' ? '0' : '1';
        lines = lines.FindAll(x => x[i] == (findMax ? max : min)).ToList();
        if (lines.Count() == 1)
          return lines.First();
      }
      return lines.First();
    }

    public override string ExecuteSecond()
    {
      var lines = GetFileLines();
      return (Convert.ToInt32(FindRating(((string[])lines.Clone()).ToList()), 2) *
        Convert.ToInt32(FindRating(((string[])lines.Clone()).ToList(), false), 2)).ToString();
    }
  }
}
