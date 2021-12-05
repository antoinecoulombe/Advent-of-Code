using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2015
{
  class Day09 : Day
  {
    private static string[] lines = GetFileLines();
    private class Route
    {
      public string from;
      public string to;
      public int distance;

      public Route(string s)
      {
        var split = s.Split(" to ");
        from = split[0];
        split = split[1].Split(" = ");
        to = split[0];
        distance = int.Parse(split[1]);
      }
    }

    private List<(string node, int dist, string prevNode)> GetInitTable()
    {
      return lines.Select(s => new Route(s))
        .GroupBy(x => x.from)
        .Select(x => (x.Key, int.MaxValue, "-")).ToList();
    }

    //IEnumerable<IGrouping<string, Route>> GetRoutes()
    private List<Route> GetNodes()
    {
      return lines.Select(s => new Route(s)).ToList();
    }

    private int Execute(bool max = false)
    {
      // DIJKSTRA algorithm (IFT585 - TP2)
      return 0;
    }

    public override string ExecuteFirst()
      => Execute().ToString();

    public override string ExecuteSecond()
      => Execute(true).ToString();
  }
}
