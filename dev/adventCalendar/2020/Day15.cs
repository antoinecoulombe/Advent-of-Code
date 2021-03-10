using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2020
{
    class Day15 : Day
    {
        private class Turns
        {
            private int[] turns;
            private int lastTurn = 0;
            private int toPlay;

            public Turns(string[] firstTurns, int toPlay)
            {
                this.toPlay = toPlay;
                turns = new int[toPlay];
                for (; lastTurn < firstTurns.Length; ++lastTurn)
                    turns[lastTurn] = int.Parse(firstTurns[lastTurn]);
            }

            private int GetTurnDifference()
            {
                for (int i = lastTurn - 1; i >= 0; --i)
                    if (turns[i] == turns[lastTurn])
                        return lastTurn - i;
                return 0;
            }

            public int GetLastTurn()
            {
                int x = 0;
                while (lastTurn + 1 < toPlay)
                {
                    turns[lastTurn + 1] = GetTurnDifference();
                    ++lastTurn;
                }
                return turns[toPlay - 1];
            }
        }

        private Turns InitTurns(int turns)
            => new Turns(GetFileLines(15)[0].Split(','), turns);

        public override string ExecuteFirst()
        {
            return InitTurns(2020).GetLastTurn().ToString();
        }

        public override string ExecuteSecond()
        {
            return InitTurns(30000000).GetLastTurn().ToString();
        }
    }
}
