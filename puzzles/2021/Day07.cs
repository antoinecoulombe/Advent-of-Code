using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc.puzzles._2021
{
  class Day07 : Day
  {
    private int[] GetNumbers()
      => GetFileText().Split(',').Select(x => int.Parse(x)).OrderBy(x => x).ToArray();

    public override string ExecuteFirst()
    {
      var numbers = GetNumbers();
      var med = numbers[numbers.Count() / 2];

      int fuel = 0;
      foreach (var n in numbers)
        fuel += Math.Abs(n - med);

      return fuel.ToString();
    }

    public override string ExecuteSecond()
    {
      var numbers = GetNumbers();
      var avg = numbers.Sum(x => x) / numbers.Count();

      int fuel = 0;
      foreach (var n in numbers)
        for (int i = 0; i < Math.Abs(n - avg); ++i)
          fuel += 1 + i;

      return fuel.ToString();
    }
  }
}
