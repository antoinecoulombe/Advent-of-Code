using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2020
{
    class Day11 : Day
    {
        private bool Exists(ref List<string> seats, int i, int j) 
            => seats.ElementAtOrDefault(i) != null && seats[i].ElementAtOrDefault(j) != '\0';

        private bool IsOccupied(ref List<string> seats, int i, int j)
            => Exists(ref seats, i, j) && seats[i][j] == '#';

        private int CountSurroundSeats(ref List<string> seats, int i, int j)
        {
            int occupiedSeats = 0;

            (int x, int y)[] pos = new (int, int)[] {
                (i - 1, j - 1), (i - 1, j), (i - 1, j + 1),
                (i, j - 1), (i, j + 1),
                (i + 1, j - 1), (i + 1, j), (i + 1, j + 1) };

            foreach ((int x, int y) in pos)
                if (IsOccupied(ref seats, x, y))
                    ++occupiedSeats;

            return occupiedSeats;
        }

        private List<string> RearrangeSeats(List<string> seats)
        {
            var newSeats = new List<string>();
            for (int i = 0; i < seats.Count; ++i)
            {
                string row = "";
                for (int j = 0; j < seats[i].Length; ++j)
                {
                    if (seats[i][j] == '.')
                        row += '.';
                    else if (seats[i][j] == 'L')
                        row += CountSurroundSeats(ref seats, i, j) == 0 ? '#' : 'L';
                    else if (seats[i][j] == '#')
                        row += CountSurroundSeats(ref seats, i, j) >= 4 ? 'L' : '#';
                }
                newSeats.Add(row);
            }
            return newSeats;
        }

        private int CountOccupied(ref List<string> seats)
        {
            int total = 0;
            foreach (string s in seats)
                total += s.Where(x => x == '#').Count();
            return total;
        }

        public override string ExecuteFirst()
        {
            var currSeats = new List<string>(GetFileLines(11));
            List<string> nextSeats = null;
            while (currSeats != nextSeats)
            {
                if (nextSeats != null)
                    currSeats = nextSeats;
                nextSeats = RearrangeSeats(currSeats);
            }
            return CountOccupied(ref currSeats).ToString();
        }

        public override string ExecuteSecond()
        {
            return "";
        }
    }
}
