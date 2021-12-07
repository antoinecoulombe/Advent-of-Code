using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace aoc.puzzles._2015
{
  class Day08 : Day
  {
    public override string ExecuteFirst()
    {
      var l = GetFileLines();
      return l.Sum(s => s.Length - Regex.Replace(
          s[1..^1].Replace("\\\"", "A").Replace("\\\\", "B"),
          "\\\\x[a-f0-9]{2}", "C").Length).ToString();
    }

    public override string ExecuteSecond()
    {
      var l = GetFileLines();
      return l.Sum(s => s.Replace("\\", "AA")
          .Replace("\"", "BB").Length + 2 - s.Length).ToString();
    }
  }
}
