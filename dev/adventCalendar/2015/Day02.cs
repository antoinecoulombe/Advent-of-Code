using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2015
{
  class Day02 : Day
  {
    private List<List<int>> GetDimensions()
        => GetFileLines().Select(
            x => x.Split('x').Select(int.Parse).ToList()).ToList();

    public override string ExecuteFirst()
    {
      var l = GetDimensions();
      long surface = 0;
      foreach (var d in l)
      {
        long s1 = 2 * d[0] * d[1],
            s2 = 2 * d[1] * d[2],
            s3 = 2 * d[0] * d[2];
        surface += s1 + s2 + s3 + Math.Min(s1, Math.Min(s2, s3)) / 2;
      }
      return surface.ToString();
    }

    public override string ExecuteSecond()
    {
      var l = GetDimensions();
      long length = 0;
      foreach (var d in l)
      {
        d.Sort();
        long bow = d[0] * d[1] * d[2];
        long ribbon = 2 * d[0] + 2 * d[1];
        length += ribbon + bow;
      }
      return length.ToString();
    }
  }
}
