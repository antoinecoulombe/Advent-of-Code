using System;
using System.Collections.Generic;
using System.Text;

namespace dev.adventCalendar._2020
{
    class Day08 : Day
    {
        private List<(string, char, int, bool)> GetOperations()
        {
            List<(string, char, int, bool)> ops = new List<(string, char, int, bool)>();
            string[] lines = GetFileLines(8);
            foreach (string l in lines)
            {
                string[] split = l.Split(' ');
                ops.Add((split[0], split[1][0], int.Parse(split[1].Substring(1)), false));
            }
            return ops;
        }

        private int ExecuteOperations(List<(string, char, int, bool)> ops, bool returnOnBreak = true)
        {
            int acc = 0;
            for (int i = 0; i < ops.Count;)
            {
                if (ops[i].Item4)
                    return returnOnBreak ? acc : -1;
                ops[i] = (ops[i].Item1, ops[i].Item2, ops[i].Item3, true);
                i += ExecuteOperation(ops[i], ref acc);
            }
            return acc;
        }

        private int ExecuteOperation((string, char, int, bool) op, ref int acc)
        {
            switch (op.Item1)
            {
                case "nop":
                    return 1;
                case "acc":
                    acc += op.Item2 == '+' ? op.Item3 : -op.Item3;
                    return 1;
                case "jmp":
                    return op.Item2 == '+' ? op.Item3 : -op.Item3;
            }
            return 0;
        }

        private int ExecuteUntilFail()
        {
            var ops = GetOperations();
            int acc = 0;
            for (int i = 0; i < ops.Count; ++i)
            {
                if (ops[i].Item1 == "nop" || ops[i].Item1 == "jmp")
                    ops[i] = (ops[i].Item1 == "nop" ? "jmp" : "nop", ops[i].Item2, ops[i].Item3, false);

                acc = ExecuteOperations(ops, false);
                if (acc != -1)
                    return acc;

                ops = GetOperations();
            }
            return -1;
        }

        public override string ExecuteFirst()
        {
            return ExecuteOperations(GetOperations()).ToString();
        }

        public override string ExecuteSecond()
        {
            return ExecuteUntilFail().ToString();
        }
    }
}
