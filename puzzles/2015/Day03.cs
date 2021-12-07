using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc.puzzles._2015
{
  class Day03 : Day
  {
    private class Shipper
    {
      public List<(int, int)> visited;
      private (int x, int y) pos;

      public Shipper()
      {
        visited = new() { (0, 0) };
        pos = (0, 0);
      }

      public void Move(char c)
      {
        switch (c)
        {
          case '^':
            ++pos.y;
            break;
          case 'v':
            --pos.y;
            break;
          case '>':
            ++pos.x;
            break;
          case '<':
            --pos.x;
            break;
        }
        if (!visited.Contains((pos.x, pos.y)))
          visited.Add((pos.x, pos.y));
      }
    }

    public override string ExecuteFirst()
    {
      var s = GetFileText();
      var shipper = new Shipper();
      foreach (var c in s)
        shipper.Move(c);
      return shipper.visited.Count.ToString();
    }

    public override string ExecuteSecond()
    {
      var s = GetFileText();
      var santa = new Shipper();
      var robot = new Shipper();
      for (int i = 0; i < s.Length; ++i)
        (i % 2 == 0 ? santa : robot).Move(s[i]);
      return santa.visited.Union(robot.visited).Count().ToString();
    }
  }
}
