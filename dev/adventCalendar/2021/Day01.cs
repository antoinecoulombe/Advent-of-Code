using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dev.adventCalendar._2021
{
  class Day01 : Day
  {
    public override string ExecuteFirst()
    {
      var lines = GetFileLines();
      int count = 0;
      for (int i = 0; i < lines.Length - 1; ++i)
        if (int.Parse(lines[i + 1]) > int.Parse(lines[i]))
          ++count;
      return count.ToString();
    }

    private int SumLines(string[] lines, int[] n)
      => n.ToList().Sum(x => int.Parse(lines[x]));

    public override string ExecuteSecond()
    {
      var lines = GetFileLines();
      int count = 0;
      for (int i = 0; i < lines.Length - 3; ++i)
        if (SumLines(lines, new int[] { i + 1, i + 2, i + 3 }) > SumLines(lines, new int[] { i, i + 1, i + 2 }))
          ++count;

      return count.ToString();
    }
  }
}
