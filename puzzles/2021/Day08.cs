using System.Collections.Generic;
using System.Linq;

namespace aoc.puzzles._2021
{
  class Day08 : Day
  {
    public override string ExecuteFirst()
        => GetFileLines().Sum(l => l.Split("| ")[1].Split(' ')
            .Count(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7)).ToString();

    private (string, int)[][][] GetSignals()
    {
      return GetFileLines().Select(x => x.Split(" | ")
      .Select(y => y.Split(' ')
      .Select(z => (signal: z, number: z.Length == 2 ? 1 : z.Length == 3 ? 7 : z.Length == 4 ? 4 : z.Length == 7 ? 8 : -1))
      .ToArray()).ToArray()).ToArray();
    }

    private void DecodeNumber(ref List<(string signal, int number)> input, ref List<(string, int)> nList, string contained, int nResult, bool reversed = false)
    {
      var n = contained == null ? nList.First() :
        nList.First(x => reversed ? x.Item1.All(contained.Contains) : contained.All(x.Item1.Contains));
      input[input.FindIndex(x => x.signal == n.Item1)] = (n.Item1, nResult);
      nList.Remove(n);
    }

    private (string, int)[][][] DecodeInput((string signal, int number)[][][] signals)
    {
      for (int i = 0; i < signals.Length; ++i)
      {
        var input = signals[i][0].ToList();
        var s235 = input.Where(x => x.signal.Length == 5).ToList();
        var s069 = input.Where(x => x.signal.Length == 6).ToList();

        DecodeNumber(ref input, ref s235, input.First(x => x.number == 1).signal, 3);
        DecodeNumber(ref input, ref s069, input.First(x => x.number == 3).signal, 9);
        DecodeNumber(ref input, ref s069, input.First(x => x.number == 1).signal, 0);
        DecodeNumber(ref input, ref s069, null, 6);
        DecodeNumber(ref input, ref s235, input.First(x => x.number == 6).signal, 5, true);
        DecodeNumber(ref input, ref s235, null, 2);

        signals[i][0] = input.ToArray();
      }

      return signals;
    }

    private int DecodeAndCountOutput((string signal, int number)[][][] signals)
    {
      int total = 0;

      foreach (var signal in signals)
      {
        var input = signal[0];

        string output = "";
        foreach (var n in signal[1])
          output += input.First(x => n.signal.Length == x.signal.Length && n.signal.All(x.signal.Contains)).number.ToString();

        total += int.Parse(output);
      }

      return total;
    }

    public override string ExecuteSecond()
    {
      // signals[line][in/out][(signal, number)]
      return DecodeAndCountOutput(DecodeInput(GetSignals())).ToString();
    }
  }
}
