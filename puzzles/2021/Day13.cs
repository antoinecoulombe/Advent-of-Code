using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace aoc.puzzles._2021
{
  class Day13 : Day
  {
    (int x, int y) size = (0, 0);

    private (Point[] points, (char axis, int at)[] folds) GetPointsAndFolds()
    {
      var lines = GetFileLines();
      var points = lines.TakeWhile(x => x != "").Select(x =>
      {
        var pos = x.Split(',').Select(x => int.Parse(x)).ToList();
        return new Point(pos[0], pos[1]);
      }).ToArray();
      var folds = lines.Skip(points.Length + 1).Select(x =>
      {
        var split = x.Split(' ')[2].Split('=');
        return (split[0][0], int.Parse(split[1]));
      }).ToArray();
      return (points, folds);
    }

    private (int x, int y) GetMax(Point[] points)
    {
      int xMax = 0;
      int yMax = 0;
      foreach (var p in points)
      {
        if (p.X > xMax)
          xMax = p.X;
        if (p.Y > yMax)
          yMax = p.Y;
      }
      return (xMax, yMax);
    }

    private void WritePoints(ref bool[,] paper, Point[] points)
    {
      foreach (var p in points)
        paper[p.X, p.Y] = true;
    }

    private void Fold(ref bool[,] paper, (char axis, int at)[] folds, bool allFolds = false)
    {
      for (int i = 0; i < (allFolds ? folds.Length : 1); ++i)
      {
        // TODO: Handle folds 
        if (folds[i].axis == 'x') // go up from 656 (x+1), go down from 655
          return; // y stays same
        else if (folds[i].axis == 'y')
          return; // x stays same
      }
    }

    private int CountPoints(ref bool[,] paper)
    {
      int count = 0;
      for (int i = 0; i < size.x; ++i)
        for (int j = 0; j < size.y; ++j)
          if (paper[i, j])
            ++count;
      return count;
    }

    public override string ExecuteFirst()
    {
      var data = GetPointsAndFolds();
      size = GetMax(data.points);
      bool[,] paper = new bool[size.x, size.y];
      WritePoints(ref paper, data.points);
      Fold(ref paper, data.folds);
      return CountPoints(ref paper).ToString();
    }

    public override string ExecuteSecond()
    {
      return "";
    }
  }
}
