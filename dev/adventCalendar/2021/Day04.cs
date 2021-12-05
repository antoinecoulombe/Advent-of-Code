using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dev.adventCalendar._2021
{
  class Day04 : Day
  {
    private bool IsCompleted(List<List<(int number, bool visited)>> board)
    {
      // check lines
      foreach (var l in board)
        if (l.FindAll(x => x.visited).Count() == l.Count())
          return true;

      // check columns
      for (int i = 0; i < board.Count(); ++i)
      {
        bool columnFilled = true;
        for (int j = 0; j < board.Count(); ++j)
          if (!board[j][i].visited)
            columnFilled = false;

        if (columnFilled)
          return true;
      }

      return false;
    }

    private List<List<List<(int number, bool visited)>>> GetBoards(string[] lines)
    {
      List<List<List<(int number, bool visited)>>> boards = new();
      for (int i = 1; i < lines.Length; ++i)
        boards.Add(lines[i].Split('\n', StringSplitOptions.None).Where(x => x != "")
            .Select(x => x.Split(' ').Where(x => x != "")
            .Select(x => (int.Parse(x), false)).ToList())
            .ToList());
      return boards;
    }

    private (List<List<(int number, bool visited)>> board, int n) FillNumbers(int[] numbers, ref List<List<List<(int number, bool visited)>>> boards, bool first = true)
    {
      (List<List<(int number, bool visited)>> board, int n) last = (null, -1);
      foreach (var n in numbers)
      {
        for (int i = 0; i < boards.Count(); ++i)
        {
          bool nFound = false;
          for (int j = 0; j < boards[i].Count(); ++j)
          {
            var ln = boards[i][j].FindIndex(x => x.number == n);
            if (ln >= 0)
            {
              boards[i][j][ln] = (boards[i][j][ln].number, true);
              nFound = true;
              break;
            }
          }

          if (nFound && IsCompleted(boards[i]))
          {
            if (first)
              return (boards[i], n);
            else
            {
              last = (boards[i], n);
              boards.RemoveAt(i);
              --i;
            }
          }
        }
      }

      return last;
    }

    private string Execute(bool first = true)
    {
      var text = GetFileText();
      var lines = text.Split("\n\n");
      int[] numbers = lines[0].Split(',', StringSplitOptions.None).Select(x => int.Parse(x)).ToArray();

      var boards = GetBoards(lines);
      var board = FillNumbers(numbers, ref boards, first);

      if (board.board == null)
        return "No boards completed.";
      else
      {
        int sum = 0;
        foreach (var l in board.board)
          l.FindAll(x => !x.visited).ForEach(x => sum += x.number);
        return (sum * board.n).ToString();
      }
    }

    public override string ExecuteFirst()
        => Execute();

    public override string ExecuteSecond()
        => Execute(false);
  }
}
