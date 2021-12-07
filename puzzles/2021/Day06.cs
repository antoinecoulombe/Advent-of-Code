using System.Linq;

namespace aoc.puzzles._2021
{
  class Day06 : Day
  {
    private long[] GetFish()
    {
      var numbers = GetFileText().Split(',').Select(x => byte.Parse(x))
           .GroupBy(x => x).Select(x => x.Count()).ToList();

      long[] fish = new long[9];
      for (int i = 0; i < numbers.Count(); ++i)
        fish[i + 1] = numbers[i];

      return fish;
    }

    private long Execute(int days)
    {
      long[] fish = GetFish();

      for (int i = 1; i <= days; ++i)
      {
        var zeros = fish[0];
        for (byte j = 1; j < fish.Count(); ++j)
          fish[j - 1] = fish[j];

        fish[6] = fish[6] + zeros;
        fish[8] = zeros;
      }

      return fish.Sum(x => x);
    }

    public override string ExecuteFirst()
        => Execute(80).ToString();

    public override string ExecuteSecond()
        => Execute(256).ToString();
  }
}
