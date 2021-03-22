namespace dev.adventCalendar._2015
{
    class Day01 : Day
    {
        public override string ExecuteFirst()
        {
            string[] l = GetFileLines(1, 2015);
            int floor = 0;
            foreach (char c in l[0])
                floor += c == '(' ? 1 : -1;
            return floor.ToString();
        }

        public override string ExecuteSecond()
        {
            string[] l = GetFileLines(1, 2015);
            int floor = 0;
            for (int i = 0; i < l[0].Length; ++i)
            {
                floor += l[0][i] == '(' ? 1 : -1;
                if (floor < 0)
                    return (i + 1).ToString();
            }
            return "Didn't enter basement.";
        }
    }
}
