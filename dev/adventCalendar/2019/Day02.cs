using System;

namespace dev.adventCalendar._2019
{
  class Day02 : Day
  {
    private int[] GetNumbers()
        => Array.ConvertAll(GetFileText().Split(','), sn => int.Parse(sn));

    private string Execute(int i1 = -1, int i2 = -1)
    {
      int[] n = GetNumbers();
      if (i1 >= 0 && i1 <= 99) n[1] = i1;
      if (i2 >= 0 && i2 <= 99) n[2] = i2;

      for (int i = 0; i < n.Length; i += 4)
      {
        if (n[i] == 99)
          return n[0].ToString();

        int cmd = n[i], dest = n[i + 3],
            n1 = n[n[i + 1]], n2 = n[n[i + 2]];

        n[dest] = cmd == 1 ? n1 + n2 : n1 * n2;
      }

      return "Exit code '99' not found.";
    }

    public override string ExecuteFirst()
        => Execute();

    public override string ExecuteSecond()
    {
      int toFind = 19690720, min = 0, max = 99;
      for (int noun = min; noun <= max; ++noun)
        for (int verb = min; verb <= max; ++verb)
          if (int.TryParse(Execute(noun, verb), out int n) && n == toFind)
            return (100 * noun + verb).ToString();

      return "No combinations of noun and verb between '" + min.ToString() +
          "' and '" + max.ToString() + "' ouputs '" + toFind.ToString() + "'.";
    }
  }
}
