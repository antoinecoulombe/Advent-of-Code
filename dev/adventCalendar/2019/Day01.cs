using System;

namespace dev.adventCalendar._2019
{
    class Day01 : Day
    {
        private int[] GetNumbers()
        {
            string[] stringN = GetFileLines(1, 2019);
            return Array.ConvertAll(stringN, sn => int.Parse(sn));
        }

        public override string ExecuteFirst()
        {
            int[] numbers = GetNumbers();
            double totalMass = 0;

            foreach (int n in numbers)
                totalMass += Math.Floor((double)(n / 3)) - 2;

            return totalMass.ToString();
        }

        public override string ExecuteSecond()
        {
            int[] numbers = GetNumbers();
            double totalMass = 0;

            foreach (int n in numbers)
                for (double i = Math.Floor((double)n / 3) - 2; i > 0; 
                    i = Math.Floor(i / 3) - 2)
                    totalMass += i;

            return totalMass.ToString();
        }
    }
}
