using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace aoc.puzzles._2021
{
  class Day05 : Day
  {
    private const int gridSize = 1000;

    private (Point from, Point to) SwapPoints(Point from, Point to)
      => from.Y > to.Y ? (to, from) : (from, to);

    private int[,] FillGrid(List<(Point, Point)> points, bool diagonals = false)
    {
      int[,] grid = new int[gridSize, gridSize];
      foreach ((Point from, Point to) p in points)
      {
        (Point from, Point to) = SwapPoints(p.from, p.to);

        if (from.Y == to.Y)
          for (int i = from.X < to.X ? from.X : to.X; i <= (to.X > from.X ? to.X : from.X); ++i)
            ++grid[to.Y, i];
        else if (from.X == to.X)
          for (int i = from.Y; i <= to.Y; ++i)
            ++grid[i, to.X];
        else if (diagonals)
        {
          if (from.X < to.X)
            for (int i = from.X, j = 0; i <= to.X; ++i, ++j)
              ++grid[from.Y + j, i];
          else
            for (int i = to.X, j = 0; i <= from.X; ++i, ++j)
              ++grid[to.Y - j, i];
        }
      }
      return grid;
    }

    private int CountIntersect(int[,] grid)
    {
      int count = 0;
      for (int i = 0; i < gridSize; ++i)
        for (int j = 0; j < gridSize; ++j)
          if (grid[i, j] > 1)
            ++count;
      return count;
    }

    private List<(Point, Point)> GetPoints()
    {
      return GetFileLines().Select(x =>
      {
        var s = x.Split(" -> ").Select(x => x.Split(",").Select(x => int.Parse(x)).ToList()).ToList();
        return (new Point(s[0][0], s[0][1]), new Point(s[1][0], s[1][1]));
      }).ToList();
    }

    public override string ExecuteFirst()
      => CountIntersect(FillGrid(GetPoints())).ToString();

    public override string ExecuteSecond()
      => CountIntersect(FillGrid(GetPoints(), true)).ToString();
  }
}
