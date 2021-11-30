using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2020
{
    class Day16 : Day
    {
        private class Tickets
        {
            public List<Constraint> constraints;
            public List<int> myTicketNumbers;
            public List<List<int>> nearbyTicketNumbers;
            private bool[] valid;

            public Tickets(List<string> lines)
            {
                int myTicketLine = lines.FindIndex(x => x.Contains("your ticket")) + 1;
                constraints = GetConstraints(lines.Take(myTicketLine - 2).ToList());
                myTicketNumbers = GetTicketNumbers(lines[myTicketLine]);
                nearbyTicketNumbers = GetNearbyNumbers(lines.Skip(myTicketLine + 3).ToList());
                valid = GetValidNumbers();
            }

            private List<int> GetTicketNumbers(string line)
                => line.Split(',').Select(int.Parse).ToList();

            private List<List<int>> GetNearbyNumbers(List<string> lines)
            {
                var numbers = new List<List<int>>();
                foreach (string l in lines)
                    numbers.Add(GetTicketNumbers(l));
                return numbers;
            }

            private List<Constraint> GetConstraints(List<string> lines)
            {
                var constraints = new List<Constraint>();

                foreach (string l in lines)
                    if (l.Contains(':'))
                        constraints.Add(new Constraint(l));

                return constraints;
            }

            private bool[] GetValidNumbers()
            {
                valid = new bool[1000];
                foreach (var constraint in constraints)
                    foreach (var (lowerbound, upperbound) in constraint.ranges)
                        for (int i = lowerbound; i <= upperbound; ++i)
                            valid[i] = true;
                return valid;
            }

            private void FilterNearbyTickets()
                => nearbyTicketNumbers.RemoveAll(x => x.Exists(y => !valid[y]));

            private void FindValidIndexes()
            {
                var found = new List<int>();
                foreach (Constraint c in constraints)
                {
                    var validIndexes = Enumerable.Range(0, nearbyTicketNumbers[0].Count).ToList();
                    validIndexes.RemoveAll(x => found.Contains(x));

                    foreach (var nearby in nearbyTicketNumbers)
                        for (int i = 0; i < nearby.Count; ++i)
                            if (!c.InRanges(nearby[i]))
                                validIndexes.Remove(i);

                    c.validIndexes = validIndexes;
                }
            }

            public void FindConstraintsValue()
            {
                FilterNearbyTickets();
                FindValidIndexes();

                while (constraints.Exists(x => x.value == -1))
                {
                    constraints = constraints.OrderBy(x => x.validIndexes.Count).ToList();

                    var first = constraints.Find(x => x.validIndexes.Count > 0);
                    var index = first.validIndexes.First();
                    first.value = myTicketNumbers[index];

                    foreach (Constraint c in constraints)
                        if (c.validIndexes.Contains(index))
                            c.validIndexes.Remove(index);
                }
            }

            public int SumErrorRate()
            {
                int total = 0;
                foreach (var ticket in nearbyTicketNumbers)
                    foreach (var n in ticket)
                        if (!valid[n])
                            total += n;
                return total;
            }

            public long MultiplyConstraintsValue()
            {
                long total = 1;
                foreach (Constraint c in constraints)
                    if (c.name.StartsWith("departure"))
                        total *= c.value;
                return total;
            }
        }

        private class Constraint
        {
            public readonly string name;
            public readonly List<(int lowerbound, int upperbound)> ranges;
            public List<int> validIndexes;
            public int value;

            public Constraint(string line)
            {
                ranges = new List<(int, int)>();

                var split = line.Split(": ");
                name = split[0];

                split = split[1].Split(" or ");
                ranges.Add(SplitNumbers(split[0]));
                ranges.Add(SplitNumbers(split[1]));

                validIndexes = new List<int>();
                value = -1;
            }

            private (int, int) SplitNumbers(string s)
            {
                var n = s.Split('-');
                return (int.Parse(n[0]), int.Parse(n[1]));
            }

            public bool InRanges(int n)
            {
                foreach (var (lowerbound, upperbound) in ranges)
                    if (n >= lowerbound && n <= upperbound)
                        return true;
                return false;
            }
        }

        public override string ExecuteFirst()
        {
            var tickets = new Tickets(GetFileLines(16, 2020).ToList());
            return tickets.SumErrorRate().ToString();
        }

        public override string ExecuteSecond()
        {
            var tickets = new Tickets(GetFileLines(16, 2020).ToList());
            tickets.FindConstraintsValue();
            return tickets.MultiplyConstraintsValue().ToString();
        }
    }
}
