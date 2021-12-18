using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc.puzzles._2021
{
	class Day14 : Day
	{
		private (string start, List<(string match, char insert)> rules) GetStartAndRules()
		{
			var lines = GetFileLines();
			var rules = lines.Skip(2).Select(x => x.Split(" -> ").ToArray()).Select(x => (x[0], x[1][0])).ToList();
			return (lines[0], rules);
		}

		private void Increment(ref Dictionary<char, long> count, char c, long inc)
			=> count[c] = count.ContainsKey(c) ? count[c] + inc : inc;

		private (Dictionary<char, long>, Dictionary<string, long>) InitCounts(string s)
		{
			var count = new Dictionary<char, long>();
			var pairs = new Dictionary<string, long>();
			for (int j = 0; j < s.Length; ++j)
			{
				if (j != s.Length - 1)
					pairs.Add(new string(new char[] { s[j], s[j + 1] }), 1);
				Increment(ref count, s[j], 1);
			}
			return (count, pairs);
		}

		private long Execute(int steps)
		{
			(string start, List<(string match, char insert)> rules) = GetStartAndRules();
			(Dictionary<char, long> count, Dictionary<string, long> pairs) = InitCounts(start);

			for (int i = 0; i < steps; ++i)
			{
				var newPairs = new Dictionary<string, long>();
				foreach (var pair in pairs)
				{
					var match = rules.FindIndex(x => x.match == pair.Key);
					var c = rules[match].insert;
					string pLeft = new string(new char[] { pair.Key[0], c }), pRight = new string(new char[] { c, pair.Key[1] });

					newPairs[pLeft] = newPairs.ContainsKey(pLeft) ? newPairs[pLeft] + pair.Value : pair.Value;
					newPairs[pRight] = newPairs.ContainsKey(pRight) ? newPairs[pRight] + pair.Value : pair.Value;
					Increment(ref count, c, pair.Value);
				}
				pairs = newPairs;
			}

			var ordered = count.OrderByDescending(x => x.Value).ToList();
			return ordered[0].Value - ordered[^1].Value;
		}

		public override string ExecuteFirst()
			=> Execute(10).ToString();

		public override string ExecuteSecond()
			=> Execute(40).ToString();
	}
}
