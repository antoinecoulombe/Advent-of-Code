namespace dev.adventCalendar._2020
{
  class Day15 : Day
  {
    private class Turns
    {
      private (int prev, int last)[] turns;
      private int lastTurn = 0;
      private int lastValue = 0;
      private int toPlay;

      private void InitTurns()
      {
        turns = new (int, int)[toPlay];
        for (int i = 0; i < toPlay; ++i)
          turns[i] = (-1, -1);
      }

      public Turns(string[] firstTurns, int toPlay)
      {
        this.toPlay = toPlay;
        InitTurns();
        for (; lastTurn < firstTurns.Length; ++lastTurn)
        {
          lastValue = int.Parse(firstTurns[lastTurn]);
          turns[lastValue].last = lastTurn + 1;
        }
      }

      private void Play()
      {
        var last = turns[lastValue];
        lastValue = last.prev == -1 ? 0 : last.last - last.prev;
        ++lastTurn;
        turns[lastValue].prev = turns[lastValue].last;
        turns[lastValue].last = lastTurn;
      }

      public int GetLastTurn()
      {
        while (lastTurn < toPlay)
          Play();
        return lastValue;
      }
    }

    private Turns InitTurns(int turns)
        => new Turns(GetFileLines()[0].Split(','), turns);

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
