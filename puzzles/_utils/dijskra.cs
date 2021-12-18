using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace aoc.puzzles
{
	static class Dijskra
	{
		public static int Execute(string[] lines)
			=> Execute(lines.Select(x => x.Select(y => y - '0').ToArray()).ToArray());

		public static int Execute(int[][] lines)
			=> Execute(lines.To2dArray(), lines[0].Length, lines.Length);

		public static int Execute(int[,] graph, int width, int height)
		{
			var nodes = new SortedSet<(int n, int manhattan, int x, int y)>() { (0, Utils.GetManhattanDistance(new Point(0, 0)), 0, 0) };
			var visited = new HashSet<Point> { new Point(0, 0) };

			while (nodes.Count > 0)
			{
				var curNode = nodes.First();
				nodes.Remove(curNode);
				foreach (var n in Utils.GetAdjacent(new Point(curNode.x, curNode.y), width, height))
				{
					if (n.X == width - 1 && n.Y == height - 1) return curNode.n + graph[n.X, n.Y];
					if (!visited.Contains(n))
					{
						visited.Add(n);
						nodes.Add((graph[n.X, n.Y] + curNode.n, Utils.GetManhattanDistance(n), n.X, n.Y));
					}
				}
			}

			return -1;
		}
	}
}
