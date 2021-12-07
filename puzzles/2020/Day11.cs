using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc.puzzles._2020
{
  class Day11 : Day
  {
    private bool Exists(ref List<string> seats, int i, int j)
        => seats.ElementAtOrDefault(i) != null && seats[i].ElementAtOrDefault(j) != '\0';

    private bool IsSeat(ref List<string> seats, int i, int j)
        => Exists(ref seats, i, j) && seats[i][j] == '.';

    private bool IsOccupied(ref List<string> seats, int i, int j)
        => Exists(ref seats, i, j) && seats[i][j] == '#';

    private bool HasChanged(ref List<string> currSeats, ref List<string> nextSeats)
    {
      for (int i = 0; i < currSeats.Count; ++i)
        for (int j = 0; j < currSeats[i].Length; ++j)
          if (currSeats[i][j] != nextSeats[i][j])
            return true;
      return false;
    }

    private int CountSurroundOccupied(ref List<string> seats, int i, int j)
    {
      int occupiedSeats = 0;

      (int x, int y)[] pos = new (int, int)[] {
                (i - 1, j - 1), (i - 1, j), (i - 1, j + 1),
                (i, j - 1), (i, j + 1),
                (i + 1, j - 1), (i + 1, j), (i + 1, j + 1) };

      foreach ((int x, int y) in pos)
        if (IsOccupied(ref seats, x, y))
          ++occupiedSeats;

      return occupiedSeats;
    }

    private bool IsFirstOccupied(ref List<string> seats, int i, int j, string dir)
    {
      int movI = 0, movJ = 0;
      do
      {
        if (dir.StartsWith('-'))
        {
          if (dir.EndsWith("xy"))
          {
            movI--;
            movJ--;
          }
          else if (dir.EndsWith("yx"))
          {
            movI++;
            movJ--;
          }
          else if (dir.EndsWith('x'))
            movI--;
          else if (dir.EndsWith('y'))
            movJ--;
        }
        else
        {
          if (dir == "xy")
          {
            movI++;
            movJ++;
          }
          else if (dir == "yx")
          {
            movI--;
            movJ++;
          }
          else if (dir == "x")
            movI++;
          else if (dir == "y")
            movJ++;
        }
      } while (IsSeat(ref seats, i + movI, j + movJ));

      return IsOccupied(ref seats, i + movI, j + movJ);
    }

    private int CountFirstOccupied(ref List<string> seats, int i, int j)
    {
      int occupiedSeats = 0;
      var directions = new string[] { "x", "y", "xy", "yx", "-x", "-y", "-xy", "-yx" };

      foreach (string dir in directions)
        occupiedSeats += IsFirstOccupied(ref seats, i, j, dir) ? 1 : 0;

      return occupiedSeats;
    }

    private List<string> RearrangeSeats(ref List<string> seats, int exec = 1)
    {
      var newSeats = new List<string>();
      for (int i = 0; i < seats.Count; ++i)
      {
        string row = "";
        for (int j = 0; j < seats[i].Length; ++j)
        {
          if (exec == 1)
          {
            if (seats[i][j] == '.')
              row += '.';
            else if (seats[i][j] == 'L')
              row += CountSurroundOccupied(ref seats, i, j) == 0 ? '#' : 'L';
            else if (seats[i][j] == '#')
              row += CountSurroundOccupied(ref seats, i, j) >= 4 ? 'L' : '#';
          }
          else if (exec == 2)
          {
            if (seats[i][j] == '.')
              row += '.';
            else if (seats[i][j] == 'L')
              row += CountFirstOccupied(ref seats, i, j) == 0 ? '#' : 'L';
            else if (seats[i][j] == '#')
              row += CountFirstOccupied(ref seats, i, j) >= 5 ? 'L' : '#';
          }
        }
        newSeats.Add(row);
      }
      return newSeats;
    }

    private int CountOccupied(ref List<string> seats)
    {
      int total = 0;
      foreach (string s in seats)
        total += s.Where(x => x == '#').Count();
      return total;
    }

    public override string ExecuteFirst()
    {
      var currSeats = new List<string>(GetFileLines());
      List<string> nextSeats = null;
      do
      {
        if (nextSeats != null)
          currSeats = nextSeats;
        nextSeats = RearrangeSeats(ref currSeats);
      } while (HasChanged(ref currSeats, ref nextSeats));
      return CountOccupied(ref currSeats).ToString();
    }

    public override string ExecuteSecond()
    {
      var currSeats = new List<string>(GetFileLines());
      List<string> nextSeats = null;
      do
      {
        if (nextSeats != null)
          currSeats = nextSeats;
        nextSeats = RearrangeSeats(ref currSeats, 2);
      } while (HasChanged(ref currSeats, ref nextSeats));
      return CountOccupied(ref currSeats).ToString();
    }
  }
}
