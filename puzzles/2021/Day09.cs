using System.Collections.Generic;
using System.Linq;

namespace aoc.puzzles._2021
{
  class Day09 : Day
  {
    private bool IsLowest(ref int[][] lines, int n, int x, int y)
    {
      if ((y > 0 && lines[y - 1][x] <= n) ||
        (y < lines.Length - 1 && lines[y + 1][x] <= n) ||
        (x > 0 && lines[y][x - 1] <= n) ||
        (x < lines.Length - 1 && lines[y][x + 1] <= n))
        return false;

      return true;
    }

    private List<(int n, int x, int y)> GetLowest(int[][] lines)
    {
      List<(int n, int x, int y)> lowNumbers = new();
      for (int i = 0; i < lines.Count(); ++i)
        for (int j = 0; j < lines.Count(); ++j)
          if (IsLowest(ref lines, lines[i][j], j, i))
            lowNumbers.Add((lines[i][j], i, j));
      return lowNumbers;
    }

    private int[][] GetNumbers()
      => GetFileLines().Select(x => x.ToCharArray().Select(x => x - '0').ToArray()).ToArray();

    public override string ExecuteFirst()
      => GetLowest(GetNumbers()).Sum(x => x.n + 1).ToString();

    public override string ExecuteSecond()
    {
      var numbers = GetNumbers();
      for (int i = 0; i < numbers.Count(); ++i)
      {
        for (int j = 0; j < numbers.Count(); ++j)
        {
          int n = numbers[i][j];

        }
      }
      return "";
    }
  }
}
