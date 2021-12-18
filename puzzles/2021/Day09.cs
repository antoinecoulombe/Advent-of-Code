using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace aoc.puzzles._2021
{
	class Day09 : Day
	{
		private List<(int n, Point pos)> lowestPoints = new();
		private int[][] numbers;

		private bool IsLowest(ref int[][] lines, int n, int x, int y)
		{
			if ((y > 0 && lines[y - 1][x] <= n) ||
				(y < lines.Length - 1 && lines[y + 1][x] <= n) ||
				(x > 0 && lines[y][x - 1] <= n) ||
				(x < lines.Length - 1 && lines[y][x + 1] <= n))
				return false;

			return true;
		}

		private void GetLowest()
		{
			for (int i = 0; i < numbers.Count(); ++i)
				for (int j = 0; j < numbers.Count(); ++j)
					if (IsLowest(ref numbers, numbers[i][j], j, i))
						lowestPoints.Add((numbers[i][j], new Point(i, j)));
		}

		private void GetNumbers()
		{
			numbers = GetFileLines().Select(x => x.ToCharArray().Select(x => x - '0').ToArray()).ToArray();
		}

		public override string ExecuteFirst()
		{
			GetNumbers();
			GetLowest();
			return lowestPoints.Sum(x => x.n + 1).ToString();
		}

		private bool ValidPoint(int x, int y)
		=> numbers[y][x] != 9;

		private bool NotFrom(int x, int y, Point? from)
			=> from == null || !(x == from?.X && y == from?.Y);

		private List<Point> GetSides(Point p, Point? from)
			=> Utils.GetAdjacent(p, numbers[p.Y].Count(), numbers.Count(),
				(s) => (from == null || !(s.X == from?.X && s.Y == from?.Y)) && numbers[s.Y][s.X] != 9);

		private int FindBassinSize(Point p, List<Point> sides, int bassinSize = 0)
		{
			if (numbers[p.Y][p.X] != 9)
			{
				numbers[p.Y][p.X] = 9;
				++bassinSize;
			}
			foreach (var s in sides)
				bassinSize = FindBassinSize(s, GetSides(s, p), bassinSize);
			return bassinSize;
		}

		public override string ExecuteSecond()
		{
			List<int> bassinSizes = new();
			for (int i = 0; i < numbers.Count(); ++i)
				for (int j = 0; j < numbers[i].Count(); ++j)
					if (numbers[i][j] != 9)
						bassinSizes.Add(FindBassinSize(new Point(j, i), GetSides(new Point(j, i), null)));
			bassinSizes = bassinSizes.OrderByDescending(x => x).ToList();

			int total = 1;
			for (int i = 0; i < 3; ++i)
				total *= bassinSizes[i];
			return total.ToString();
		}
	}
}
