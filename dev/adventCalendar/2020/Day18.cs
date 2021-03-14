using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

                public string final;
                List<(char letter, string eq)> par;

                public Equation(string equation)
                {
                    var (eq, par) = GetParentheses(equation);
                    final = eq;
                    this.par = par;
                }

                public double Resolve(bool ordered)
                {
                    ResolveParentheses(ordered);
                    final = ReplaceAllLetters(final);
                    final = ResolveAllOperands(final, ordered);
                    return double.Parse(ResolveAllOperands(final, ordered));
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

                private void ResolveParentheses(bool ordered)
                {
                    for (int i = 0; i < par.Count; ++i)
                    {
                        var eq = par[i].eq;
                        eq = ReplaceAllLetters(eq);
                        eq = ResolveAllOperands(eq, ordered);
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

                private string ResolveAllOperands(string eq, bool ordered)
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

            private List<Equation> equations = new();

            public Math(List<string> eqs)
                => eqs.ForEach(x => equations.Add(new Equation(x)));

            public double ResolveAll(bool ordered)
                => equations.Sum(x => x.Resolve(ordered));

            public double Resolve(int pos, bool ordered)
                => equations[pos].Resolve(ordered);
        }

        public override string ExecuteFirst()
        {
            var math = new Math(GetFileLines(18).ToList());
            return math.ResolveAll(false).ToString();
        }

        public override string ExecuteSecond()
        {
            var math = new Math(GetFileLines(18).ToList());
            return math.ResolveAll(true).ToString();
        }
    }
}
