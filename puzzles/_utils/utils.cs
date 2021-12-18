using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace aoc.puzzles
{
	static class Extensions
	{
		// T[y][x] => T[x,y]
		public static T[,] To2dArray<T>(this T[][] target)
		{
			if (target.Length == 0) return new T[0, 0];
			T[,] array = new T[target.Length, target[0].Length];
			for (int i = 0; i < target.Length; ++i)
				for (int j = 0; j < target[i].Length; ++j)
					array[j, i] = target[i][j];
			return array;
		}
	}

	static class Utils
	{
		public static List<Point> GetAdjacent(this Point p, int width, int height, Func<Point, bool> condition)
			=> GetAdjacent(p, width, height).Where(condition).ToList();

		public static List<Point> GetAdjacent(this Point p, int width, int height)
			=> GetAdjacent(p, (s) => Enumerable.Range(0, width).Contains(s.X) && Enumerable.Range(0, height).Contains(s.Y));

		public static List<Point> GetAdjacent(this Point p, Func<Point, bool> condition)
		{
			return new List<Point>
				{
					new Point(p.X - 1, p.Y),
					new Point(p.X + 1, p.Y),
					new Point(p.X, p.Y - 1),
					new Point(p.X, p.Y + 1),
				}.Where(condition).ToList();
		}

		public static int GetManhattanDistance(Point p)
			=> -(2 + p.X + p.Y);
	}
}
