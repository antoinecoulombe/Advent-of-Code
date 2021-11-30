using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2020
{
    class Day13 : Day
    {
        #region First Part

        private List<int> GetBusIds(string s)
        {
            var buses = s.Split(',').ToList();
            buses.RemoveAll(x => x == "x");
            return buses.Select(int.Parse).ToList();
        }

        private (int, int) GetFastestBus(int arrivalTime, List<int> busIds)
        {
            int fastestTime = int.MaxValue;
            int fastestId = -1;
            foreach (int id in busIds)
            {
                var before = arrivalTime - (id * Math.Floor(decimal.Divide(arrivalTime, id)));
                var after = id - before;
                if (after < fastestTime)
                {
                    fastestId = id;
                    fastestTime = (int)after;
                }
            }
            return (fastestTime, fastestId);
        }

        #endregion

        #region Second Part

        private List<(int id, int pos)> GetBusesInfo(List<string> input)
        {
            var buses = new List<(int, int)>();
            for (int i = 0; i < input.Count; ++i)
                if (int.TryParse(input[i], out int n))
                    buses.Add((n, i));
            return buses;
        }

        private long GetInverse(long Ni, int id)
        {
            long x = Ni % id;
            for (int i = 1; i < id; ++i)
                if ((x * i) % id == 1)
                    return i;
            return 1;
        }

        private long GetModulo(int offset, int id)
            => ((offset % id) + id) % id;

        private long GetCRTValue(List<(int id, int pos)> buses)
        {
            long N = 1, sum = 0;
            buses.ForEach(x => N *= x.id);
            foreach (var (id, pos) in buses)
            {
                long mod = GetModulo(id - pos, id),
                    inverse = GetInverse(N / id, id);
                sum += mod * (N / id) * inverse;
            }
            return sum % N;
        }

        #endregion

        public override string ExecuteFirst()
        {
            var l = GetFileLines(13, 2020);
            (int time, int id) fastest = GetFastestBus(int.Parse(l[0]), GetBusIds(l[1]));
            return (fastest.time * fastest.id).ToString();
        }

        public override string ExecuteSecond()
        {
            var buses = GetBusesInfo(GetFileLines(13, 2020)[1].Split(',').ToList());
            return GetCRTValue(buses).ToString();
        }
    }
}
