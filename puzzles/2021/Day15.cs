using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace aoc.puzzles._2021
{
	class Day15 : Day
	{
		public override string ExecuteFirst()
			=> Dijskra.Execute(GetFileLines()).ToString();

		public override string ExecuteSecond()
		{
			var lines = GetFileLines().Select(x => x.Select(y => y - '0').ToArray()).ToArray();
			return Dijskra.Execute(ExpendGraph(lines), lines[0].Length * 5, lines.Length * 5).ToString();
		}

		public static int[,] ExpendGraph(int[][] lines)
		{
			int size = lines.Length, newSize = size * 5;
			int[,] graph = new int[newSize, newSize];

			for (int y = 0; y < newSize; ++y)
			{
				for (int x = 0; x < newSize; ++x)
				{
					var value = lines[x % size][y % size] + (y / size) + (x / size);
					while (value > 9) value -= 9;
					graph[x, y] = value;
				}
			}
			return graph;
		}
	}
}
