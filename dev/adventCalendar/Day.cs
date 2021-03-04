using System;
using System.IO;

namespace dev.adventCalendar
{
    abstract class Day
    {
        private static string sep = "\\";
        private static string baseFolder = "dev" + sep;

        public abstract string ExecuteFirst();
        public abstract string ExecuteSecond();


        public string GetRessourcePath(int d, int y = 2020, string fileName = null)
        {
            try
            {
                string path = "";

                if (Program.IsLinux())
                {
                    sep = "/";
                    path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + sep;
                }
                else
                    path = System.Reflection.Assembly.GetExecutingAssembly().Location
                        .Substring(0, path.LastIndexOf(baseFolder) + baseFolder.Length);

                path += "adventCalendar" + sep + y.ToString() + sep + "ressources" + sep + "day_" +
                    (d < 10 ? "0" : "") + d.ToString() + sep + fileName ?? "";
                return path;
            }
            catch (Exception) { return null; }
        }

        public string[] GetFileLines(int d, int y = 2020, string fileName = "input.txt")
        {
            try {
                return File.ReadAllLines(GetRessourcePath(d, y, fileName)); }
            catch (Exception) { return null; }
        }
        public string GetFileText(int d, int y = 2020, string fileName = "input.txt")
        {
            try { return File.ReadAllText(GetRessourcePath(d, y, fileName)); }
            catch (Exception) { return null; }
        }

        public int[] GetIntegers(int d, int y = 2020, string fileName = "input.txt")
        {
            try { return Array.ConvertAll(GetFileLines(d, y, fileName), sn => int.Parse(sn)); }
            catch (Exception) { return null; }
        }

        public double[] GetDoubles(int d, int y = 2020, string fileName = "input.txt")
        {
            try { return Array.ConvertAll(GetFileLines(d, y, fileName), sn => double.Parse(sn)); }
            catch (Exception) { return null; }
        }
    }
}
