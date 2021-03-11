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
            private List<Constraint> constraints;
            private List<int> myTicketNumbers;
            private List<List<int>> nearbyTicketNumbers;
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

            private void FindConstraintsValue()
            {

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

            public int MultiplyConstraintsValue(int count)
            {
                int total = 1;
                for (int i = 0; i < count; ++i)
                    total *= constraints[i].value;
                return total;
            }
        }

        private class Constraint
        {
            public readonly string name;
            public readonly List<(int lowerbound, int upperbound)> ranges;
            public int value;

            public Constraint(string line)
            {
                ranges = new List<(int, int)>();

                var split = line.Split(": ");
                name = split[0];

                split = split[1].Split(" or ");
                ranges.Add(SplitNumbers(split[0]));
                ranges.Add(SplitNumbers(split[1]));
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
            var tickets = new Tickets(GetFileLines(16).ToList());
            return tickets.SumErrorRate().ToString();
        }

        public override string ExecuteSecond()
        {
            var tickets = new Tickets(GetFileLines(16).ToList());
            return tickets.MultiplyConstraintsValue(6).ToString();
        }
    }
}
