using System;
using System.Reflection;

namespace valorant_stats
{
    class XP
    {
        private readonly int CurrentLevel;

        public XP(int currentLevel, DateTime seasonEndDate)
        {
            INFO.SEASON_END_DATE = seasonEndDate;
            CurrentLevel = currentLevel;
        }

        public void WriteSetup()
        {
            INFO.Write(CurrentLevel);
        }

        public void Write(int[] destinationLevels, bool includeChallenges = true)
        {
            int total = 0, matches = 0, matchesChall = 0;
            decimal matchesPerDay = 0, matchesPerDayChall = 0, hours = 0,
                    hoursChall = 0, hoursPerDay = 0, hoursPerDayChall = 0;

            foreach (int level in destinationLevels)
            {
                if (CurrentLevel < level)
                {
                    total = Calculate(level);
                    Calculate(level, ref matches, ref hours, ref matchesPerDay, ref hoursPerDay);

                    if (includeChallenges)
                        Calculate(level, ref matchesChall, ref hoursChall, ref matchesPerDayChall, ref hoursPerDayChall, true);

                    WriteLevel(level);
                    WriteResults(matches, hours, matchesPerDay, hoursPerDay);

                    if (includeChallenges && matchesPerDayChall > 0)
                    {
                        Console.WriteLine("\nWITH CHALLENGES");
                        WriteResults(matchesChall, hoursChall, matchesPerDayChall, hoursPerDayChall);
                    }

                    INFO.WriteSeparator();
                }
            }
        }

        private void Calculate(int level, ref int matches, ref decimal hours, ref decimal mPd, ref decimal hPd, bool chall = false)
        {
            int total = Calculate(level),
                dChallXP = chall ? INFO.DAYS_REMAINING * INFO.DAY_CHALL_NUM * INFO.DAY_CHALL_XP : 0,
                wChallXP = chall ? (int)Math.Ceiling(INFO.WEEKS_REMAINING) * INFO.WEEK_CHALL_XP * INFO.WEEK_CHALL_NUM : 0;

            matches = (int)Math.Ceiling((decimal)(total - (dChallXP + wChallXP)) / INFO.POINTS_PER_GAME);
            hours = matches * INFO.MINUTES_PER_GAME / 60;
            mPd = Math.Round(decimal.Divide(matches, INFO.DAYS_REMAINING), 2);
            hPd = Math.Round(mPd * INFO.MINUTES_PER_GAME / 60, 2);
        }

        private void WriteLevel(int level) => Console.WriteLine("LEVEL " + level + "\n");
        private void WriteResults(int matches, decimal hours, decimal matchesPerDay, decimal hoursPerDay)
        {
            Console.WriteLine("Matches = " + matches);
            Console.WriteLine("Hours = " + hours);
            Console.WriteLine("Matches per day = " + matchesPerDay);
            Console.WriteLine("Hours per day = " + hoursPerDay);
        }

        private int Calculate(int level, int total = 0)
        {
            if (level == CurrentLevel) return total;
            return Calculate(--level, total + 2000 + level * 1000);
        }


        private static class INFO
        {
            public const int
                POINTS_PER_GAME = 4600,
                MINUTES_PER_GAME = 30,
                DAY_CHALL_NUM = 2,
                DAY_CHALL_XP = 2000,
                WEEK_CHALL_NUM = 3,
                WEEK_CHALL_XP = 10800;

            private static DateTime _SEASON_END_DATE;
            public static DateTime SEASON_END_DATE
            {
                get => _SEASON_END_DATE;
                set
                {
                    _SEASON_END_DATE = value;
                    DAYS_REMAINING = (value - DateTime.Now).Days;
                    WEEKS_REMAINING = Math.Round(decimal.Divide(DAYS_REMAINING, 7), 2);
                }
            }

            public static int DAYS_REMAINING { get; private set; }
            public static decimal WEEKS_REMAINING { get; private set; }

            public static void Write(int currentLevel = -1)
            {
                var fields = typeof(INFO).GetFields(BindingFlags.Public | BindingFlags.Static);

                foreach (var field in fields)
                    Console.WriteLine(field.Name + " = " + field.GetValue(null));

                Console.WriteLine();
                Console.WriteLine("SEASON_END_DATE = " + SEASON_END_DATE.ToShortDateString());
                Console.WriteLine("DAYS_REMAINING = " + DAYS_REMAINING);
                Console.WriteLine("WEEKS_REMAINING = " + WEEKS_REMAINING);

                if (currentLevel != -1)
                    Console.WriteLine("\nCURRENT_LEVEL = " + currentLevel);

                WriteSeparator();
            }

            public static void WriteSeparator()
                => Console.WriteLine("---------------------------------------");
        }
    }
}
