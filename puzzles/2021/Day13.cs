using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace aoc.puzzles._2021
{
	class Day13 : Day
	{
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

		private static int MovePoint(int p, int at)
			=> p >= at ? 2 * at - p : p;

		private List<Point> FoldOne((Point[] points, (char axis, int at)[] folds) input)
		{
			return input.folds.Take(1).SelectMany(f => input.points.Select(
				p => new Point(f.axis == 'x' ? MovePoint(p.X, f.at) : p.X, f.axis == 'y' ? MovePoint(p.Y, f.at) : p.Y)))
			.Distinct().ToList();
		}
		private List<Point> FoldAll((Point[] points, (char axis, int at)[] folds) input)
		{
			return input.folds.Aggregate(input.points.ToList(), (d, f) => d.Select(
				p => new Point(f.axis == 'x' ? MovePoint(p.X, f.at) : p.X, f.axis == 'y' ? MovePoint(p.Y, f.at) : p.Y))
			.ToList()).Distinct().OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
		}

		public override string ExecuteFirst()
			=> FoldOne(GetPointsAndFolds()).Count().ToString();

		public override string ExecuteSecond()
		{
			var input = GetPointsAndFolds();
			var points = FoldAll(input);

			string result = "";
			for (int i = 0; i < points.Max(p => p.Y) + 1; ++i)
			{
				for (int j = 0; j < points.Max(p => p.X) + 1; ++j)
					result += points.Contains(new Point(j, i)) ? "@" : " ";
				result += "\n";
			}
			return result;
		}
	}
}
