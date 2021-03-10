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
            private class Turn
            {
                public int turnNumber;
                public int NumberSpoken;

                public Turn(int tn, int ns)
                {
                    turnNumber = tn;
                    NumberSpoken = ns;
                }
            }

            private int currentTurn;
            private List<Turn> turns;

            public Turns(string[] firstTurns)
            {
                turns = new List<Turn>();
                for (int i = 0; i < firstTurns.Length; ++i)
                    turns.Add(new Turn(i + 1, int.Parse(firstTurns[i])));
                currentTurn = firstTurns.Length + 1;
            }

            private List<Turn> FindLastTwoTurns()
            {
                var lastTurns = new List<Turn>();
                var lastNumSpoken = turns[turns.Count - 1].NumberSpoken;
                for (int i = turns.Count - 1; i >= 0; --i)
                {
                    if (turns[i].NumberSpoken == lastNumSpoken)
                        lastTurns.Add(turns[i]);

                    if (lastTurns.Count == 2)
                        break;
                }
                return lastTurns;
            }

            private void AddTurn(int spoken)
            {
                turns.Add(new Turn(currentTurn, spoken));
                ++currentTurn;
            }

            private void PlayTurn()
            {
                var lastTurns = FindLastTwoTurns();
                if (lastTurns.Count() < 2)
                    AddTurn(0);
                else
                    AddTurn(lastTurns[0].turnNumber - lastTurns[1].turnNumber);
            }

            public int GetTurn(int turn)
            {
                while (currentTurn != turn + 1)
                    PlayTurn();
                return turns.Find(x => x.turnNumber == turn).NumberSpoken;
            }
        }

        private Turns InitTurns()
            => new Turns(GetFileLines(15)[0].Split(','));

        public override string ExecuteFirst()
        {
            return InitTurns().GetTurn(2020).ToString();
        }

        public override string ExecuteSecond()
        {
            return InitTurns().GetTurn(30000000).ToString();
        }
    }
}
