using System;
using System.Collections.Generic;
using System.Text;

namespace dev.adventCalendar._2020
{
    class Day12 : Day
    {
        private class Ship
        {
            private int north;
            private int east;
            private int direction;

            private enum DIR { NORTH = 0, EAST = 90, SOUTH = 180, WEST = 270 }

            public Ship()
            {
                north = 0;
                east = 0;
                direction = 90;
            }

            public int GetManhattanDistance()
                => Math.Abs(north) + Math.Abs(east);

            private char ConvertDirection()
            {
                if (direction == (int)DIR.NORTH)
                    return 'N';
                if (direction == (int)DIR.EAST)
                    return 'E';
                if (direction == (int)DIR.SOUTH)
                    return 'S';
                if (direction == (int)DIR.WEST)
                    return 'W';
                throw new Exception("Can't convert integer direction to cardinal direction.");
            }

            private void AdjustDirection()
            {
                if (direction < 0)
                    direction = 360 + direction;
                direction %= 360;
            }

            public void Move(char dir, int num)
            {
                switch (dir)
                {
                    case 'N':
                        north += num;
                        break;
                    case 'S':
                        north -= num;
                        break;
                    case 'E':
                        east += num;
                        break;
                    case 'W':
                        east -= num;
                        break;
                    case 'L':
                        direction -= num;
                        break;
                    case 'R':
                        direction += num;
                        break;
                    case 'F':
                        Move(ConvertDirection(), num);
                        break;
                }
                AdjustDirection();
            }
        }
        
        public override string ExecuteFirst()
        {
            var lines = new List<string>(GetFileLines(12));
            Ship ship = new Ship();
            foreach (string l in lines)
                ship.Move(l[0], int.Parse(l.Substring(1)));
            return ship.GetManhattanDistance().ToString();
        }

        public override string ExecuteSecond()
        {
            return "";
        }
    }
}
