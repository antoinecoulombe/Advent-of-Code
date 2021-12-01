using System;

namespace dev.adventCalendar._2020
{
  class Day01 : Day
  {
    public override string ExecuteFirst()
    {
      int[] n = GetIntegers();
      for (int i = 0; i < n.Length; ++i)
        for (int j = i + 1; j < n.Length; ++j)
          if (n[i] + n[j] == 2020)
            return (n[i] * n[j]).ToString();

      return "No two numbers sum to 2020.";
    }

    public override string ExecuteSecond()
    {
      int[] n = GetIntegers();
      for (int i = 0; i < n.Length; ++i)
        for (int j = i + 1; j < n.Length; ++j)
          for (int k = j + 1; k < n.Length; ++k)
            if (n[i] + n[j] + n[k] == 2020)
              return (n[i] * n[j] * n[k]).ToString();

      return "No three numbers sum to 2020.";
    }
  }
}
