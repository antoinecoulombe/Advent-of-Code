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

            public Ship()
            {
                north = 0;
                east = 0;
            }

            public int GetManhattanDistance()
                => Math.Abs(north) + Math.Abs(east);

            public virtual void Move(char dir, int num)
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
                }
            }
        }

        private class Ship1 : Ship
        {
            private enum DIR { NORTH = 0, EAST = 90, SOUTH = 180, WEST = 270 }
            private int direction;

            public Ship1()
            {
                direction = 90;
            }

            private char ConvertDirection(int dir)
            {
                if (dir == (int)DIR.NORTH)
                    return 'N';
                if (dir == (int)DIR.EAST)
                    return 'E';
                if (dir == (int)DIR.SOUTH)
                    return 'S';
                if (dir == (int)DIR.WEST)
                    return 'W';
                throw new Exception("Can't convert integer direction to cardinal direction.");
            }

            private int AdjustDirection(int dir)
            {
                if (dir < 0)
                    dir = 360 + dir;
                return dir %= 360;
            }

            public override void Move(char dir, int num)
            {
                switch (dir)
                {
                    case 'N':
                    case 'S':
                    case 'E':
                    case 'W':
                        base.Move(dir, num);
                        break;
                    case 'L':
                        direction -= num;
                        break;
                    case 'R':
                        direction += num;
                        break;
                    case 'F':
                        Move(ConvertDirection(direction), num);
                        break;
                }
                direction = AdjustDirection(direction);
            }
        }

        private class Ship2 : Ship
        {
            private enum DIR { NORTH = 1, EAST = 2, SOUTH = 3, WEST = 4 }
            private ((DIR direction, int distance) dir1, (DIR direction, int distance) dir2) waypoint;

            public Ship2()
            {
                waypoint.dir2.direction = DIR.NORTH;
                waypoint.dir2.distance = 1;
                waypoint.dir1.direction = DIR.EAST;
                waypoint.dir1.distance = 10;
            }

            private char ConvertDirection(DIR dir)
            {
                if (dir == DIR.NORTH)
                    return 'N';
                if (dir == DIR.EAST)
                    return 'E';
                if (dir == DIR.SOUTH)
                    return 'S';
                if (dir == DIR.WEST)
                    return 'W';
                throw new Exception("Can't convert integer direction to cardinal direction.");
            }

            private DIR GetInverseDirection(DIR d)
            {
                int move = (int)d + 2;
                if (move > 4)
                    move %= 4;
                return (DIR)move;
            }

            private void RedirectWaypoint(ref (DIR direction, int distance) way, int move)
            {
                move += (int)way.direction;
                if (move < 1)
                    move = 4 + move;
                else if (move > 4)
                    move %= 4;
                way.direction = (DIR)move;
            }

            private void RotateWaypoint(ref (DIR direction, int distance) way, char dir, int num)
            {
                int move = num == 90 ? 1 : (num == 180 ? 2 : 3);
                move = dir == 'R' ? move : -move;
                RedirectWaypoint(ref way, move);
            }

            private void MoveWaypoint(ref (DIR direction, int distance) way, DIR dir, int num)
            {
                if (way.direction == dir)
                    way.distance += num;
                else if (way.direction == GetInverseDirection(dir))
                    way.distance -= num;
                FixWaypoint(ref way);
            }

            private void FixWaypoint(ref (DIR direction, int distance) way)
            {
                if (way.distance < 0)
                {
                    way.distance = -way.distance;
                    RotateWaypoint(ref way, 'R', 180);
                }
            }

            public override void Move(char dir, int num)
            {
                switch (dir)
                {
                    case 'N':
                        MoveWaypoint(ref waypoint.dir1, DIR.NORTH, num);
                        MoveWaypoint(ref waypoint.dir2, DIR.NORTH, num);
                        break;
                    case 'S':
                        MoveWaypoint(ref waypoint.dir1, DIR.SOUTH, num);
                        MoveWaypoint(ref waypoint.dir2, DIR.SOUTH, num);
                        break;
                    case 'E':
                        MoveWaypoint(ref waypoint.dir1, DIR.EAST, num);
                        MoveWaypoint(ref waypoint.dir2, DIR.EAST, num);
                        break;
                    case 'W':
                        MoveWaypoint(ref waypoint.dir1, DIR.WEST, num);
                        MoveWaypoint(ref waypoint.dir2, DIR.WEST, num);
                        break;
                    case 'L':
                    case 'R':
                        RotateWaypoint(ref waypoint.dir1, dir, num);
                        RotateWaypoint(ref waypoint.dir2, dir, num);
                        break;
                    case 'F':
                        base.Move(ConvertDirection(waypoint.dir1.direction), num * waypoint.dir1.distance);
                        base.Move(ConvertDirection(waypoint.dir2.direction), num * waypoint.dir2.distance);
                        break;
                }
            }
        }

        public override string ExecuteFirst()
        {
            var lines = new List<string>(GetFileLines(12, 2020));
            Ship1 ship = new Ship1();
            foreach (string l in lines)
                ship.Move(l[0], int.Parse(l.Substring(1)));
            return ship.GetManhattanDistance().ToString();
        }

        public override string ExecuteSecond()
        {
            var lines = new List<string>(GetFileLines(12, 2020));
            Ship2 ship = new Ship2();
            foreach (string l in lines)
                ship.Move(l[0], int.Parse(l.Substring(1)));
            return ship.GetManhattanDistance().ToString();
        }
    }
}
