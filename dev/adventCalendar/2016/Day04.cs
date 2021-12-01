using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2016
{
  class Day04 : Day
  {
    private (string name, int id)[] GetValidRooms()
    {
      var lines = GetFileLines();
      List<(string name, int id)> rooms = new();

      foreach (var l in lines)
      {
        var name = l.Substring(0, l.LastIndexOf('-'));
        var id = l.Substring(l.LastIndexOf('-') + 1, l.IndexOf('[') - l.LastIndexOf('-') - 1);
        var checksum = l.Substring(l.IndexOf('[') + 1, l.IndexOf(']') - l.IndexOf('[') - 1);

        var grouped =
            name.Replace("-", string.Empty)
            .GroupBy(c => c)
            .Select(x => (x.Key, x.Count()))
            .OrderByDescending(x => x.Item2)
            .ThenBy(x => x.Key).ToList();

        if (String.Join("", grouped.Select(x => x.Key)).StartsWith(checksum))
          rooms.Add((name, int.Parse(id)));
      }
      return rooms.ToArray();
    }

    public override string ExecuteFirst()
        => GetValidRooms().Sum(x => x.id).ToString();

    public override string ExecuteSecond()
    {
      var rooms = GetValidRooms();
      foreach (var r in rooms)
      {
        string s = "";
        foreach (var c in r.name)
          s += c == '-' ? " " : (char)(((int)c - 97 + r.id) % 26 + 97);

        if (s.Contains("northpole"))
          return r.id.ToString();
      }

      return "Can't find encrypted room.";
    }
  }
}
