using System;
using System.Collections.Generic;
using System.Text;

namespace dev.adventCalendar._2017
{
  class Day03 : Day
  {
    private (int, int) GetLines(int n)
    {
      int l = 1, i = 1;
      for (; i <= n; i += 8)
        ++l;
      return (l, i);
    }

    private enum DIR { RIGHT, UP, LEFT, DOWN }

    private int BuildSpiral(int n)
    {
      var spiral = new List<List<int>>();
      spiral.Add(new List<int>() { 1 });

      DIR dir = DIR.RIGHT;
      int move = 0, dist = 1;
      for (int i = 1; i <= n; ++i, ++move)
      {
        if (move == 2)
        {
          move = 0;
          ++dist;
          dir = (DIR)(((int)dir + 1) % 4);
        }

        // DETERMINE ROW #
        switch (dir)
        {
          case DIR.RIGHT:
            spiral[0].Add(i); // insert after
            break;
          case DIR.UP:
            // insert above
            break;
          case DIR.LEFT:
            spiral[0].Insert(0, i); // insert before
            break;
          case DIR.DOWN:
            // insert below
            break;
        }
      }

      return 0;
    }

    public override string ExecuteFirst()
    {
      var n = int.Parse(GetFileLines()[0]);
      return BuildSpiral(n).ToString();
    }

    public override string ExecuteSecond()
    {
      return "";
    }
  }
}
