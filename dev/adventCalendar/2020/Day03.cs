namespace dev.adventCalendar._2020
{
  class Day03 : Day
  {
    private int GetTreesEncountered(int movX, int movY)
    {
      var rows = GetFileLines();
      int trees = 0;
      for (int x = 0, y = 0; y < rows.Length; y += movY, x += movX)
        if (rows[y][x % rows[y].Length] == '#')
          ++trees;
      return trees;
    }
    public override string ExecuteFirst()
        => GetTreesEncountered(3, 1).ToString();

    public override string ExecuteSecond()
        => (GetTreesEncountered(1, 1) *
            GetTreesEncountered(3, 1) *
            GetTreesEncountered(5, 1) *
            GetTreesEncountered(7, 1) *
            GetTreesEncountered(1, 2)).ToString();
  }
}
