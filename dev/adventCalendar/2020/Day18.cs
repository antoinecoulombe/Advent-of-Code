using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace dev.adventCalendar._2020
{
  class Day18 : Day
  {
    private class Math
    {
      private class Equation
      {
        private readonly char[] operators = new char[] { '+', '-', '*', '/' };
        private readonly char[] opSecond = new char[] { '*', '/' };
        private readonly char[] opFirst = new char[] { '+', '-' };

        private string final;
        private bool ordered;
        private List<(char letter, string eq)> par;

        public Thread thread;

        public Equation(string equation, bool ordered)
        {
          var (eq, par) = GetParentheses(equation);
          final = eq;
          this.par = par;
          this.ordered = ordered;
          thread = new Thread(() => DoWork());
        }

        public void DoWork()
        {
          ResolveParentheses();
          final = ReplaceAllLetters(final);
          final = ResolveAllOperands(final);
        }

        public double GetResult()
            => double.Parse(final);

        public void Resolve()
        {
          thread.Start();
        }

        private (string, List<(char, string)>) GetParentheses(string eq)
        {
          eq = eq.Replace(" ", "");
          List<(char, string)> orderedParentheses = new List<(char, string)>();

          int ascii = 'a';
          while (eq.Contains('('))
          {
            int start = eq.LastIndexOf('(');
            int end = eq.IndexOf(')', start);
            string parentheses = eq.Substring(start + 1, end - start - 1);
            orderedParentheses.Add(((char)ascii, parentheses));
            eq = eq.Substring(0, start) + (char)ascii + eq.Substring(end + 1);
            ++ascii;
          }

          return (eq, orderedParentheses);
        }

        private void ResolveParentheses()
        {
          for (int i = 0; i < par.Count; ++i)
          {
            var eq = par[i].eq;
            eq = ReplaceAllLetters(eq);
            eq = ResolveAllOperands(eq);
            par[i] = (par[i].letter, eq);
          }
        }

        private string ReplaceAllLetters(string eq)
        {
          char l = IndexOfLastChar(eq);
          while (l != '-')
          {
            var replacement = par.Find(x => x.letter == l).eq;
            eq = eq.Replace(l.ToString(), replacement);
            l = IndexOfLastChar(eq);
          }
          return eq;
        }

        private string ResolveAllOperands(string eq)
        {
          while (eq.IndexOfAny(operators) != -1)
            eq = ordered ? ResolveOrderedOperands(eq) : ResolveFirstOperands(eq);
          return eq;
        }

        private string ResolveFirstOperands(string eq)
        {
          int op = eq.IndexOfAny(operators),
              nextOp = eq.IndexOfAny(operators, op + 1);
          var left = eq.Substring(0, op);
          var right = nextOp == -1 ? eq.Substring(op + 1) : eq.Substring(op + 1, nextOp - op - 1);

          if (IsNumber(left) && IsNumber(right))
            return ComputeOperation((double.Parse(left), double.Parse(right)), eq[op]).ToString()
                + (nextOp == -1 ? "" : eq.Substring(nextOp));

          throw new Exception("Can't compute operands.");
        }

        private string ResolveOrderedOperands(string eq)
        {
          int first = eq.IndexOfAny(opFirst),
              second = eq.IndexOfAny(opSecond),
              op = first == -1 ? second : first,
              previous = eq.LastIndexOfAny(operators, op - 1),
              next = eq.IndexOfAny(operators, op + 1);

          var left = previous == -1 ? eq.Substring(0, op) : eq.Substring(previous + 1, op - previous - 1);
          var right = next == -1 ? eq.Substring(op + 1) : eq.Substring(op + 1, next - op - 1);

          string prevEq = previous == -1 ? "" : eq.Substring(0, previous + 1),
              nextEq = next == -1 ? "" : eq.Substring(next);

          char opChar = eq[op];

          if (IsNumber(left) && IsNumber(right))
            return prevEq + ComputeOperation((double.Parse(left), double.Parse(right)), opChar).ToString() + nextEq;

          throw new Exception("Can't compute operands.");
        }

        private char IndexOfLastChar(string eq)
        {
          try
          {
            return eq.Last(x => char.IsLower(x));
          }
          catch (Exception)
          {
            return '-';
          }
        }

        private bool IsNumber(string operand)
            => operand.All(char.IsDigit);

        private double ComputeOperation((double left, double right) var, char op)
        {
          return op switch
          {
            '+' => var.left + var.right,
            '/' => var.left / var.right,
            '*' => var.left * var.right,
            '-' => var.left - var.right,
            _ => throw new Exception("Invalid Operation."),
          };
        }
      }

      private void WaitAll()
      {
        foreach (Equation eq in equations)
          eq.thread.Join();
      }

      private List<Equation> equations = new();

      public Math(List<string> eqs, bool ordered)
          => eqs.ForEach(x => equations.Add(new Equation(x, ordered)));

      public double ResolveAll()
      {
        foreach (Equation eq in equations)
          eq.Resolve();

        WaitAll();

        return equations.Sum(x => x.GetResult());
      }
    }

    public override string ExecuteFirst()
    {
      var math = new Math(GetFileLines().ToList(), false);
      return math.ResolveAll().ToString();
    }

    public override string ExecuteSecond()
    {
      var math = new Math(GetFileLines().ToList(), true);
      return math.ResolveAll().ToString();
    }
  }
}
