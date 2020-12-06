namespace dev.adventCalendar._2020
{
    class Day02 : Day
    {
        public override string ExecuteFirst()
        {
            int correct = 0;
            foreach (string l in GetFileLines(2))
            {
                var s = l.Split('-', ' ', ':');
                int count = 0;
                foreach (char c in s[4])
                    if (c == char.Parse(s[2])) count++;
                if (count >= int.Parse(s[0]) && count <= int.Parse(s[1]))
                    ++correct;
            }

            return correct.ToString();
        }

        public override string ExecuteSecond()
        {
            int correct = 0;
            foreach (string l in GetFileLines(2))
            {
                var s = l.Split('-', ' ', ':');
                int[] i = new int[] { int.Parse(s[0]) - 1, int.Parse(s[1]) - 1 };
                int count = 0;
                for (int j = 0; j < i.Length; ++j)
                    if (s[4].Length > i[j] && s[4][i[j]] == char.Parse(s[2]))
                        ++count;
                if (count == 1)
                    ++correct;
            }

            return correct.ToString();
        }
    }
}
