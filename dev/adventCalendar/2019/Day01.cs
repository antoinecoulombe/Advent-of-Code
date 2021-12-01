using System;

namespace dev.adventCalendar._2019
{
  class Day01 : Day
  {
    private int[] GetNumbers()
        => Array.ConvertAll(GetFileLines(), sn => int.Parse(sn));

    public override string ExecuteFirst()
    {
      double totalMass = 0;
      foreach (int n in GetNumbers())
        totalMass += Math.Floor((double)(n / 3)) - 2;
      return totalMass.ToString();
    }

    public override string ExecuteSecond()
    {
      double totalMass = 0;
      foreach (int n in GetNumbers())
        for (double i = Math.Floor((double)n / 3) - 2; i > 0;
            i = Math.Floor(i / 3) - 2)
          totalMass += i;
      return totalMass.ToString();
    }
  }
}
