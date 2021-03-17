using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace dev.adventCalendar._2020
{
    class Day19 : Day
    {
        private enum LETTERS { a = 250, b = 251 }
        private (Dictionary<int, List<List<Rule>>>, List<string>) GetParts()
        {
            var input = GetFileLines(19);
            var rules = ParseRules(input.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).ToList());
            var messages = input.SkipWhile(x => !string.IsNullOrWhiteSpace(x)).Skip(1);
            return (rules, messages.ToList());
        }

        public override string ExecuteFirst()
        {
            var (rules, messages) = GetParts();
            return messages.Count(m => GetMatches(rules, m).FirstOrDefault() == m.Length).ToString();
        }

        public override string ExecuteSecond()
        {
            var (rules, messages) = GetParts();
            rules[8] = new List<List<Rule>>() {
                new List<Rule>() { new PointerRule(42) },
                new List<Rule>() { new PointerRule(42), new PointerRule(8) }
            };
            rules[11] = new List<List<Rule>>() {
                new List<Rule>() { new PointerRule(42), new PointerRule(31) },
                new List<Rule>() { new PointerRule(42), new PointerRule(11), new PointerRule(31) }
            };
            return messages.Count(m => GetMatches(rules, m).FirstOrDefault() == m.Length).ToString();
        }

        private static Dictionary<int, List<List<Rule>>> ParseRules(List<string> rules)
        {
            return rules.Select(line =>
                {
                    var parts = line.Split(": ");
                    var entry = new KeyValuePair<int, List<List<Rule>>>(int.Parse(parts[0]),
                        parts[1].Split(" | ")
                        .Select(combination => combination.Split(" ")
                        .Select<string, Rule>(part =>
                                part.StartsWith(@"""") ? new CharRule(part[1]) : new PointerRule(int.Parse(part))
                            ).ToList()
                        ).ToList());
                    return entry;
                }).ToDictionary();
        }

        private char TryGetCharAt(string msg, int i)
        {
            try
            {
                return msg[i];
            }
            catch (Exception) { return char.MaxValue; }
        }

        private IEnumerable<int> GetMatches(Dictionary<int, List<List<Rule>>> rules, string message,
            int ruleId = 0, int index = 0)
        {
            return rules[ruleId].SelectMany(rulesList =>
            {
                var positions = MoreEnumerable.Return(index);
                rulesList.ForEach(rule =>
                {
                    positions = positions.SelectMany(i =>
                    {
                        return i switch
                        {
                            _ when rule is CharRule charRule && TryGetCharAt(message, i) == charRule.Character => MoreEnumerable
                                .Return(i + 1),
                            _ when rule is PointerRule pointerRule => GetMatches(rules, message, pointerRule.Id, i),
                            _ => Enumerable.Empty<int>()
                        };
                    });
                });
                return positions;
            });
        }
    }

    public abstract record Rule { }
    public sealed record CharRule(char Character) : Rule { }
    public sealed record PointerRule(int Id) : Rule { }
}