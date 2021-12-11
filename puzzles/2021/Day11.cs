using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc.puzzles._2021
{

  class Day11 : Day
  {
    const int SIZE = 10;
    (int energy, bool flashed)[][] octo = new (int, bool)[SIZE][];

    private List<Point> GetSides(Point p)
    {
      (int X, int Y)[] adjPos = new (int, int)[8] { (-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1) };
      var limits = Enumerable.Range(0, 10);
      List<Point> sides = new();
      foreach (var adj in adjPos)
        if (limits.Contains(p.X + adj.X) && limits.Contains(p.Y + adj.Y))
          sides.Add(new Point(p.X + adj.X, p.Y + adj.Y));
      return sides;
    }

    private int GetFlash(Point p, int flashes = 0)
    {
      if (octo[p.Y][p.X].flashed) return flashes;

      ++octo[p.Y][p.X].energy;
      if (octo[p.Y][p.X].energy <= 9) return flashes;
      octo[p.Y][p.X] = (0, true);
      ++flashes;

      var sides = GetSides(p);
      foreach (var s in sides)
        flashes = GetFlash(s, flashes);

      return flashes;
    }

    private int GetFlashes()
    {
      for (int i = 0; i < SIZE; ++i)
        for (int j = 0; j < SIZE; ++j)
          octo[i][j] = (octo[i][j].energy, false);

      int flashes = 0;
      for (int i = 0; i < SIZE; ++i)
        for (int j = 0; j < SIZE; ++j)
          flashes += GetFlash(new Point(j, i));

      return flashes;
    }

    private void GetOctopuses()
    {
      octo = GetFileLines().Select(x => x.ToCharArray().Select(y => (y - '0', false)).ToArray()).ToArray();
    }

    public override string ExecuteFirst()
    {
      GetOctopuses();
      int flashes = 0;
      for (int i = 0; i < 100; ++i)
        flashes += GetFlashes();
      return flashes.ToString();
    }

    public override string ExecuteSecond()
    {
      GetOctopuses();
      for (int i = 1; i < 1000; ++i)
        if (GetFlashes() == 100)
          return i.ToString();
      return "Could not find sync flash.";
    }
  }
}
