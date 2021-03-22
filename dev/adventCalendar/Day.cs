using System;
using System.Collections.Generic;
using System.IO;

namespace dev.adventCalendar
{
    abstract class Day
    {
        private static string sep = "\\";
        private static string baseFolder = "dev" + sep;

        public abstract string ExecuteFirst();
        public abstract string ExecuteSecond();

        protected static string GetRessourcePath(int d, int y = 2020, string fileName = null)
        {
            try
            {
                y = y < 2000 ? y + 2000 : y;
                string path = "";

                if (Program.IsLinux())
                {
                    sep = "/";
                    path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + sep;
                }
                else
                    path = System.Reflection.Assembly.GetExecutingAssembly().Location
                        .Substring(0, path.LastIndexOf(baseFolder) + baseFolder.Length);

                return path + "adventCalendar" + sep + y.ToString() + sep + "ressources" + sep + "day_" +
                    (d < 10 ? "0" : "") + d.ToString() + sep + fileName ?? "";
            }
            catch (Exception) { return null; }
        }

        protected static string[] GetFileLines(int d, int y = 2020, string fileName = "input.txt")
        {
            try {
                return File.ReadAllLines(GetRessourcePath(d, y, fileName)); }
            catch (Exception) { return null; }
        }
        protected static string GetFileText(int d, int y = 2020, string fileName = "input.txt")
        {
            try { return File.ReadAllText(GetRessourcePath(d, y, fileName)); }
            catch (Exception) { return null; }
        }

        protected static int[] GetIntegers(int d, int y = 2020, string fileName = "input.txt")
        {
            try { return Array.ConvertAll(GetFileLines(d, y, fileName), sn => int.Parse(sn)); }
            catch (Exception) { return null; }
        }

        protected static double[] GetDoubles(int d, int y = 2020, string fileName = "input.txt")
        {
            try { return Array.ConvertAll(GetFileLines(d, y, fileName), sn => double.Parse(sn)); }
            catch (Exception) { return null; }
        }

        protected static void WriteToFile(string line, int d, int y = 2020, string fileName = "output.txt")
            => WriteToFile(new List<string>() { line }, d, y, fileName);

        protected static void WriteToFile(List<string> lines, int d, int y = 2020, string fileName = "output.txt")
            => File.WriteAllLines(GetRessourcePath(d, y, fileName), lines);
    }
}
