using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace aoc.puzzles._2016
{
  class Day01 : Day
  {
    private enum DIR { EAST = 90, SOUTH = 180, WEST = 270, NORTH = 0 }

    private DIR move(DIR curr, char next)
    {
      int move = next == 'R' ? 90 : -90;
      return (DIR)(((int)curr + move) < 0 ? 270 : ((int)curr + move) % 360);
    }

    private List<(int x, int y)> Execute()
    {
      string[] lines = GetFileText().Split(", ");
      (int x, int y) pos = (0, 0);
      DIR dir = DIR.NORTH;
      List<(int x, int y)> history = new();

      foreach (var l in lines)
      {
        char nextDir = l[0];
        int dist = int.Parse(l.Substring(1));
        dir = move(dir, nextDir);

        for (int i = 1; i <= dist; ++i)
        {
          if (dir == DIR.EAST) pos.x += 1;
          else if (dir == DIR.WEST) pos.x -= 1;
          else if (dir == DIR.NORTH) pos.y += 1;
          else if (dir == DIR.SOUTH) pos.y -= 1;

          history.Add((pos.x, pos.y));
        }
      }

      return history;
    }

    public override string ExecuteFirst()
    {
      var history = Execute();
      return (Math.Abs(history[^1].x) + Math.Abs(history[^1].y)).ToString();
    }

    public override string ExecuteSecond()
    {
      var history = Execute();
      int minY = int.MaxValue;

      for (int i = 0; i < history.Count; ++i)
        for (int y = i + 1; y < history.Count; ++y)
          if (history[i] == history[y] && y < minY)
            minY = y;

      return (Math.Abs(history[minY].x) + Math.Abs(history[minY].y)).ToString();
    }
  }
}
