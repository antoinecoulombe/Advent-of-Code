using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2021
{
  class Day06 : Day
  {
    private (byte, long)[] GetFish()
    {
      var fish = GetFileText().Split(',').Select(x => byte.Parse(x))
           .GroupBy(x => x).Select(x => (daysLeft: x.Key, count: (long)x.Count())).ToList();

      for (byte i = 0; i <= 8; ++i)
        if (!fish.Exists(x => x.daysLeft == i))
          fish.Add((i, 0));

      return fish.OrderBy(x => x.daysLeft).ToArray();
    }

    private long Execute(int days)
    {
      (byte daysLeft, long count)[] fish = GetFish();

      for (int i = 1; i <= days; ++i)
      {
        var zeros = (((byte, long)[])(fish.Clone()))[0].Item2;
        for (byte j = 1; j < fish.Count(); ++j)
          fish[j - 1] = ((byte)(j - 1), fish[j].count);

        fish[6] = (6, fish[6].count + zeros);
        fish[8] = (8, zeros);
        fish[0] = (0, fish[0].count);
      }

      return fish.Sum(x => x.count);
    }

    public override string ExecuteFirst()
        => Execute(80).ToString();

    public override string ExecuteSecond()
        => Execute(256).ToString();
  }
}
