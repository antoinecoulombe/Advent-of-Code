using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dev.adventCalendar._2020
{
    class Day14 : Day
    {
        private List<(string, int, string)> GetMemory()
        {
            var lines = GetFileLines(14);
            var mem = new List<(string, int, string)>();
            foreach (string l in lines)
            {
                var value = l.Substring(l.IndexOf('=') + 2);
                if (l.StartsWith("mask"))
                    mem.Add(("mask", 0, value));
                else
                {
                    var add = int.Parse(l.Substring(4, l.IndexOf(']') - 4));
                    mem.Add(("mem", add, value));
                }
            }
            return mem;
        }

        private string ToBinary(int x, int size)
        {
            char[] buff = new char[size];

            for (int i = size - 1; i >= 0; i--)
                buff[(size - 1) - i] = (x & (1 << i)) != 0 ? '1' : '0';

            return new string(buff);
        }

        private string ApplyMask(StringBuilder num, string mask)
        {
            if (num.Length != mask.Length)
                throw new Exception("The binary number and mask are not the same length.");

            for (int i = 0; i < num.Length; ++i)
                if (mask[i] != 'X')
                    num[i] = mask[i];

            return num.ToString();
        }

        private void Decode(ref List<(string type, int add, string val)> mem)
        {
            string mask = "";
            for (int i = 0; i < mem.Count; ++i)
            {
                if (mem[i].type == "mask")
                {
                    mask = mem[i].val;
                    continue;
                }

                var b = ToBinary(int.Parse(mem[i].val), 36);
                b = ApplyMask(new StringBuilder(b), mask);
                b = Convert.ToInt64(b, 2).ToString();
                mem[i] = (mem[i].type, mem[i].add, b);
            }
        }

        public override string ExecuteFirst()
        {
            var mem = GetMemory();
            Decode(ref mem);
            return mem.Sum(m => m.Item1 == "mem" ? long.Parse(m.Item3) : 0).ToString();
        }

        public override string ExecuteSecond()
        {
            return "";
        }
    }
}
