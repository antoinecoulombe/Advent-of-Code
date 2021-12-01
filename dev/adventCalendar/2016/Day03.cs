using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2016
{
  class Day03 : Day
  {
    private int CountValidTriangles(string[] lines)
    {
      int count = 0;
      foreach (var l in lines)
      {
        var numbers = l.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x)).ToList();
        numbers.Sort();
        if (numbers[0] + numbers[1] > numbers[2])
          ++count;
      }
      return count;
    }

    public override string ExecuteFirst()
      => CountValidTriangles(GetFileLines()).ToString();

    public override string ExecuteSecond()
    {
      var lines = GetFileLines();
      var newTriangles = new List<string>();

      for (int i = 0; i < lines.Length - 1; i += 3)
        for (int j = 0; j < 3; ++j)
          newTriangles.Add(String.Join(' ', new string[] {
            lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries)[j],
            lines[i + 1].Split(' ', StringSplitOptions.RemoveEmptyEntries)[j],
            lines[i + 2].Split(' ', StringSplitOptions.RemoveEmptyEntries)[j]
            }));

      return CountValidTriangles(newTriangles.ToArray()).ToString();
    }
  }
}
