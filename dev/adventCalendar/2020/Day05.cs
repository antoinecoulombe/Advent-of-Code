using System;
using System.Collections.Generic;

namespace dev.adventCalendar._2020
{
  class Day05 : Day
  {
    private int GetPosition(string pass, int startIndex, int maxIndex,
        double min, double max, char lowLetter, char highLetter)
    {
      for (int i = startIndex; i <= maxIndex; ++i)
      {
        if (pass[i] == lowLetter)
          max -= Math.Ceiling((max - min) / 2);
        else if (pass[i] == highLetter)
          min += Math.Ceiling((max - min) / 2);
      }
      return (int)max;
    }

    public override string ExecuteFirst()
    {
      int highestId = 0;
      foreach (string l in GetFileLines())
      {
        int row = GetPosition(l, 0, 6, 0, 127, 'F', 'B'),
            column = GetPosition(l, 6, 9, 0, 7, 'L', 'R');

        int id = row * 8 + column;
        if (id > highestId)
          highestId = id;
      }

      return highestId.ToString();
    }

    public override string ExecuteSecond()
    {
      var seats = new List<(int, int)>();

      foreach (string l in GetFileLines())
      {
        int row = GetPosition(l, 0, 6, 0, 127, 'F', 'B'),
            column = GetPosition(l, 6, 9, 0, 7, 'L', 'R');
        seats.Add((row, column));
      }
      seats.Sort(Comparer<(int, int)>.Default);

      for (int i = 1; i < seats.Count; ++i)
      {
        var rowSeats = seats.FindAll(((int r, int c) s) => s.r == i);
        if (rowSeats.Count < 8)
        {
          for (int j = 1; j < rowSeats.Count; ++j)
            if (rowSeats[j].Item2 - rowSeats[j - 1].Item2 != 1)
              return (rowSeats[j].Item1 * 8 + rowSeats[j].Item2 - 1).ToString();
        }
      }

      return "All seats are occupied.";
    }
  }
}
