using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc.puzzles._2020
{
  class Day09 : Day
  {
    private int FindInvalidPos(ref double[] n)
    {
      for (int i = 25; i < n.Length; ++i)
        if (IsSum(ref n, i) == -1)
          return i;
      return -1;
    }

    private int IsSum(ref double[] n, int i)
    {
      int min = i - 25;
      for (int j = min; j < i; ++j)
        for (int k = min + 1; k < i; ++k)
          if (n[k] + n[j] == n[i])
            return i;
      return -1;
    }

    private double FindSum(ref double[] n, int resultPos)
    {
      List<double> consec = new List<double>();
      double total = 0;
      for (int i = 0; i < resultPos; ++i)
      {
        for (int j = i; j < resultPos; ++j)
        {
          total += n[j];
          consec.Add(n[j]);

          if (total == n[resultPos])
            return GetResult(consec);

          if (total > n[resultPos])
          {
            total = 0;
            consec = new List<double>();
            break;
          }
        }
      }
      return -1;
    }

    private double GetResult(List<double> consec)
        => consec.Max() + consec.Min();

    public override string ExecuteFirst()
    {
      var numbers = GetDoubles();
      return numbers[FindInvalidPos(ref numbers)].ToString();
    }

    public override string ExecuteSecond()
    {
      var numbers = GetDoubles();
      return FindSum(ref numbers, FindInvalidPos(ref numbers)).ToString();
    }
  }
}
