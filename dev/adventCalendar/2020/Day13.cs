using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2020
{
    class Day13 : Day
    {
        private List<int> GetBusIds(string s)
        {
            var buses = s.Split(',').ToList();
            buses.RemoveAll(x => x == "x");
            return buses.Select(int.Parse).ToList();
        }

        private List<(ulong,ulong)> GetBusOffsets(string input)
        {
            var buses = new List<(ulong, ulong)>();
            var split = input.Split(',').ToList();
            ulong offset = 1, n = 0;
            for (int i = 0; i < split.Count; ++i)
                if (ulong.TryParse(split[i], out n))
                    buses.Add((n, (ulong)i + 1));
            //foreach (string s in split)
            //{
            //    if (ulong.TryParse(s, out n))
            //    {
            //        buses.Add((n, offset));
            //        offset = 1;
            //    }
            //    else
            //        ++offset;
            //}
            buses[0] = (buses[0].Item1, 0);
            return buses;
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

        private string GetResult1((int time,int id)fastest)
        {
            return (fastest.time * fastest.id).ToString();
        }

        public override string ExecuteFirst()
        {
            var l = GetFileLines(13);
            return GetResult1(GetFastestBus(int.Parse(l[0]), GetBusIds(l[1])));
        }

        private bool AreOrdered(List<(ulong id, ulong offset)> buses, ulong currentTime)
        {
            foreach ((ulong id, ulong offset) bus in buses)
                if ((currentTime + bus.offset) % bus.id != 0)
                    return false;
            return true;
        }

        private ulong GetEarliestOrderedBuses(List<(ulong id, ulong offset)> buses)
        {
            ulong currentTime = buses[0].id * 100000000000000;
            while (!AreOrdered(buses, currentTime))
                currentTime += buses[0].id;
            return currentTime;
        }

        public override string ExecuteSecond()
        {
            return GetEarliestOrderedBuses(GetBusOffsets(GetFileLines(13)[1])).ToString();
        }
    }
}
