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
            private List<Rule> rules;
            private List<bool[]> boolMatches;
            private List<List<byte>> matches;

            public Rules(List<string> ruleLines)
            {
                GetRules(ruleLines);
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

            private void FindMatches()
            {
                boolMatches = new List<bool[]>();

                do
                {

                } while (matches.Exists(x => x.Exists(y => y != 200 && y != 201))); // 200 = a, 201 = b
            }

            private bool[] ConvertMessage(string msg)
            {
                bool[] boolMsg = new bool[msg.Length];
                for (int i = 0; i < msg.Length; ++i)
                    boolMsg[i] = msg[i] == 'b';
                return boolMsg;
            }

            private bool IsExactMatch(bool[] msg)
                => boolMatches.Contains(msg);

            public List<bool[]> GetMatches(List<string> msgs)
            {
                List<bool[]> boolMsgs = new();
                msgs.ForEach(x => boolMsgs.Add(ConvertMessage(x)));
                return boolMsgs.FindAll(x => IsExactMatch(x));
            }
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
            return new Rules(ruleLines).GetMatches(messages).Count.ToString();
        }

        public override string ExecuteSecond()
        {
            return "";
        }
    }
}
