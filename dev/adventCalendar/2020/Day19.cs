using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2020
{
    class Day19 : Day
    {
        private class Rule
        {
            public readonly byte number;
            public readonly char letter;
            public readonly List<List<byte>> subrules;
            
            public Rule(string rule)
            {
                subrules = new();

                var split = rule.Split(':');
                number = byte.Parse(split[0]);

                if (split[1].Contains('"'))
                {
                    letter = split[1].Substring(split[1].IndexOf('"') + 1, 1)[0];
                    return;
                }

                letter = '-';

                subrules.Add(new List<byte>());
                var sub = split[1].Split(' ').ToList().FindAll(x => x.Length > 0);
                foreach (var s in sub)
                {
                    if (s == "|")
                        subrules.Add(new List<byte>());
                    else
                        subrules.Last().Add(byte.Parse(s));
                }
            }
        }

        private class Rules
        {
            private enum LETTERS { a = 200, b = 201 };
            private List<Rule> rules;
            private List<string> matches;

            public Rules(List<string> ruleLines)
            {
                GetRules(ruleLines);
                ReplaceNumbersWithLetters();
                FindMatches();
            }

            private void GetRules(List<string> lines)
            {
                rules = new List<Rule>();
                lines.ForEach(x => rules.Add(new Rule(x)));
                rules = rules.OrderBy(x => x.number).ToList();
            }

            private void WriteRules()
            {
                List<string> lines = new();
                foreach (Rule r in rules)
                {
                    string s = r.number.ToString() + ": ";

                    if (r.letter != '-')
                    {
                        s += "\"" + r.letter + "\"";
                        lines.Add(s);
                        continue;
                    }

                    foreach (var sub in r.subrules)
                    {
                        foreach (var num in sub)
                            s += num.ToString() + " ";
                        s += "| ";
                    }

                    lines.Add(s.Substring(0, s.Length - 2));
                }
                WriteToFile(lines, 19);
            }

            private (byte a, byte b) GetLetterNumbers()
            {
                byte a = byte.MaxValue, b = byte.MaxValue;
                foreach (Rule r in rules)
                {
                    if (r.letter == 'a')
                        a = r.number;
                    if (r.letter == 'b')
                        b = r.number;
                }

                rules.RemoveAll(x => x.letter == 'a' || x.letter == 'b');
                return (a, b);
            }

            private void ReplaceNumbersWithLetters()
            {
                var (a, b) = GetLetterNumbers();
                foreach (Rule r in rules)
                {
                    foreach (var s in r.subrules)
                    {
                        for (int i = 0; i < s.Count; ++i)
                        {
                            if (s[i] == a)
                                s[i] = (byte)LETTERS.a;
                            else if (s[i] == b)
                                s[i] = (byte)LETTERS.b;
                        }
                    }
                }
            }

            private void FindMatches()
            {
                matches = new();
                List<List<byte>> poss = new();
                poss.AddRange(rules.Find(x => x.number == 0).subrules);

                while (poss.Count > 0)
                {
                    string str = "";
                    var first = poss.First();
                    while (first.Count > 0)
                    {
                        while (IsLetter(first.First()))
                        {
                            str += first.First() == (byte)LETTERS.a ? "a" : "b";
                            first.RemoveAt(0);

                            if (first.Count == 0)
                                break;
                        }

                        if (first.Count == 0)
                            break;

                        byte n = first.First();
                        var newRule = rules.Find(x => x.number == n);

                        first.RemoveAt(0);

                        if (newRule.subrules.Count > 1)
                        {
                            poss.Add(new(first));
                            poss.Last().InsertRange(0, newRule.subrules[1]);
                        }

                        first.InsertRange(0, newRule.subrules[0]);
                    }

                    poss.RemoveAt(0);
                    if (str.Length >= 24 && !matches.Contains(str))
                    {
                        matches.Add(str);
                        Console.WriteLine(str);
                    }
                }
            }

            private bool IsLetter(byte b)
                => b == (byte)LETTERS.a || b == (byte)LETTERS.b;

            public List<string> GetMatches(List<string> msgs)
                => msgs.FindAll(x => matches.Contains(x));
        }

        private (List<string>, List<string>) GetLines()
        {
            var l = GetFileLines(19);
            var rules = l.TakeWhile(x => !(x.StartsWith('a') || x.StartsWith('b')) && x.Length > 0).ToList();
            var messages = l.Skip(rules.Count + 1).ToList();
            return (rules, messages);
        }

        public override string ExecuteFirst()
        {
            var (ruleLines, messages) = GetLines();
            var rules = new Rules(ruleLines);
            var matches = rules.GetMatches(messages);
            return matches.Count.ToString();
        }

        public override string ExecuteSecond()
        {
            return "";
        }
    }
}
