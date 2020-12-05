namespace dev.adventCalendar._2020
{
    class Day03 : Day
    {
        private int GetTreesEncountered(int movX, int movY)
        {
            string[] rows = GetFileLines(3);
            int trees = 0,
                x = 0,
                y = 0;
            for (; y < rows.Length; y += movY, x += movX)
            {
                if (rows[y][x % rows[y].Length] == '#')
                    ++trees;
            }
            return trees;
        }
        public override string ExecuteFirst()
        {
            return GetTreesEncountered(3, 1).ToString();
        }

        public override string ExecuteSecond()
        {
            return (GetTreesEncountered(1,1) *
                GetTreesEncountered(3, 1) *
                GetTreesEncountered(5, 1) *
                GetTreesEncountered(7, 1) *
                GetTreesEncountered(1, 2)).ToString();
        }
    }
}
