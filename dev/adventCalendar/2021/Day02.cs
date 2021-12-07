namespace dev.adventCalendar._2021
{
  class Day02 : Day
  {
    private (int x, int y) Execute(bool level = false)
    {
      var lines = GetFileLines();
      (int x, int y) pos = (0, 0);
      int aim = 0;

      foreach (var l in lines)
      {
        var s = l.Split(' ');
        if (s[0] == "forward")
        {
          pos.x += int.Parse(s[1]);
          if (level)
            pos.y += int.Parse(s[1]) * aim;
        }
        if (s[0] == "down") aim += int.Parse(s[1]);
        if (s[0] == "up") aim -= int.Parse(s[1]);
      }

      return (pos.x, !level ? aim : pos.y);
    }

    public override string ExecuteFirst()
    {
      var pos = Execute();
      return (pos.x * pos.y).ToString();
    }

    public override string ExecuteSecond()
    {
      var pos = Execute(true);
      return (pos.x * pos.y).ToString();
    }
  }
}
