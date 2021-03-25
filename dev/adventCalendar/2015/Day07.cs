using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2015
{
    class Day07 : Day
    {
        private static class Operators
        {
            public static int AND(int left, int right)
                => left & right;
            public static int OR(int left, int right)
                => left | right;
            public static int LSHIFT(int left, int right)
                => left << right;
            public static int RSHIFT(int left, int right)
                => left >> right;
            public static int NOT(int source, int discard)
                => ~source;
            public static int IN(int from, int discard)
                => from;
        }

        private class Instruction
        {
            public Func<int, int, int> op;
            public (string left, string right) from;
            public string to;

            public Instruction(string s)
            {
                var parts = s.Split(" -> ");
                to = parts[1];

                parts = parts[0].Split(" ");
                if (parts[0] == "NOT")
                {
                    op = Operators.NOT;
                    from = (parts[1], null);
                }
                else if (parts.Length == 1)
                {
                    op = Operators.IN;
                    from = (parts[0], null);
                }
                else
                {
                    op = GetOperator(parts[1]);
                    from = (parts[0], parts[2]);
                }
            }

            private Func<int, int, int> GetOperator(string s)
            {
                return s switch
                {
                    "LSHIFT" => Operators.LSHIFT,
                    "RSHIFT" => Operators.RSHIFT,
                    "OR" => Operators.OR,
                    "AND" => Operators.AND,
                    _ => throw new Exception("Invalid operator."),
                };
            }
        }

        private List<Instruction> instructions;
        private Dictionary<string, int> wires;

        private void GetInstructions()
        {
            instructions = new();
            var l = GetFileLines(7, 15);
            foreach (var s in l)
                instructions.Add(new Instruction(s));
        }

        private void GetBaseWireValues()
        {
            wires = new();
            var ins = instructions.FindAll(x => x.op == Operators.IN).ToList();
            for (int i = 0; i < ins.Count; ++i)
            {
                if (int.TryParse(ins[i].from.left, out int n))
                {
                    wires.Add(ins[i].to, n);
                    instructions.Remove(ins[i]);
                }
            }
        }

        private void GetAllWireValues()
        {
            while (instructions.Count > 0)
            {
                for (int i = 0; i < instructions.Count; ++i)
                {
                    var from = instructions[i].from;
                    int left = 0, right = 0;
                    bool wireLeft = wires.ContainsKey(from.left),
                        wireRight = from.right != null && wires.ContainsKey(from.right),
                        parsedLeft = !wireLeft && int.TryParse(from.left, out left),
                        parsedRight = !wireRight && int.TryParse(from.right, out right),
                        oneParam = instructions[i].op == Operators.NOT || instructions[i].op == Operators.IN;

                    if ((wireLeft && wireRight) || (wireLeft && parsedRight) ||
                        (wireRight && parsedLeft) || (parsedLeft && parsedRight) ||
                        (wireLeft && oneParam))
                    {
                        left = parsedLeft ? left : wires[from.left];
                        right = oneParam ? -1 : parsedRight ? right : wires[from.right];
                        wires.Add(instructions[i].to, instructions[i].op(left, right));
                        instructions.RemoveAt(i);
                        --i;
                    }
                }
            }
        }

        public override string ExecuteFirst()
        {
            GetInstructions();
            GetBaseWireValues();
            GetAllWireValues();
            return wires["a"].ToString();
        }

        public override string ExecuteSecond()
        {
            GetInstructions();
            GetBaseWireValues();
            wires["b"] = 46065;
            GetAllWireValues();
            return wires["a"].ToString();
        }
    }
}
