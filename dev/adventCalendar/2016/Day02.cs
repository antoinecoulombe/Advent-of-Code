using System;
using System.Collections.Generic;
using System.Text;

namespace dev.adventCalendar._2016
{
  class Day02 : Day
  {
    public override string ExecuteFirst()
    {
      var lines = GetFileLines();
      string combination = "";
      foreach (var l in lines)
      {
        int num = 5;
        foreach (var dir in l)
        {
          if (dir == 'U' && num > 3) num -= 3;
          else if (dir == 'D' && num < 7) num += 3;
          else if (dir == 'R' && num % 3 != 0) num += 1;
          else if (dir == 'L' && num % 3 != 1) num -= 1;
        }
        combination += num.ToString();
      }
      return combination;
    }

    public override string ExecuteSecond()
    {
      var keypad = new int[][] {
        new int[] {-1,-1, 1,-1,-1},
        new int[] {-1, 2, 3, 4,-1},
        new int[] {5 , 6, 7, 8, 9},
        new int[] {-1,10,11,12,-1},
        new int[] {-1,-1,13,-1,-1}
        };

      var lines = GetFileLines();
      string combination = "";
      foreach (var l in lines)
      {
        (int x, int y) pos = (0, 2);
        foreach (var dir in l)
        {
          try
          {
            if (dir == 'D' && keypad[pos.y + 1][pos.x] != -1) pos.y += 1;
            else if (dir == 'U' && keypad[pos.y - 1][pos.x] != -1) pos.y -= 1;
            else if (dir == 'R' && keypad[pos.y][pos.x + 1] != -1) pos.x += 1;
            else if (dir == 'L' && keypad[pos.y][pos.x - 1] != -1) pos.x -= 1;
          }
          catch (Exception) { }
        }
        combination += keypad[pos.y][pos.x].ToString("X");
      }
      return combination;
    }
  }
}
