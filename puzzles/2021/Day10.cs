using System.Collections.Generic;
using System.Linq;

namespace aoc.puzzles._2021
{
  class Day10 : Day
  {
    private List<char> openers = new() { '(', '[', '{', '<' };
    private List<char> closers = new() { ')', ']', '}', '>' };

    private char GetFirstIllegal(string line)
    {
      Stack<char> opened = new();
      foreach (char c in line)
        if (openers.Contains(c))
          opened.Push(c);
        else if (closers.IndexOf(c) != openers.IndexOf(opened.Pop()))
          return c;
      return '-';
    }

    private long GetIncompleteScore(string line)
    {
      Stack<char> opened = new Stack<char>();

      foreach (char c in line)
        if (openers.Contains(c)) opened.Push(c);
        else opened.Pop();

      long score = 0;
      foreach (char c in opened)
        score = score * 5 + openers.IndexOf(c) + 1;
      return score;
    }

    public override string ExecuteFirst()
    {
      var scores = new int[4] { 3, 57, 1197, 25137 };
      return GetFileLines().Select(x =>
      {
        char c = GetFirstIllegal(x);
        return c == '-' ? 0 : scores[closers.IndexOf(c)];
      }).Sum().ToString();
    }

    public override string ExecuteSecond()
    {
      var lines = GetFileLines().Where(x => GetFirstIllegal(x) == '-').Select(GetIncompleteScore)
        .OrderBy(x => x).ToArray();
      return lines[lines.Length / 2].ToString();
    }
  }
}
