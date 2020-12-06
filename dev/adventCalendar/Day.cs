using System;
using System.IO;

namespace dev.adventCalendar
{
    abstract class Day
    {
        private static string baseFolder = "dev\\";

        public abstract string ExecuteFirst();
        public abstract string ExecuteSecond();

        public string GetRessourcePath(int d, int y = 2020, string fileName = null)
        {
            try
            {
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                path = path.Substring(0, path.LastIndexOf(baseFolder) + baseFolder.Length);
                path += "adventCalendar\\" + y.ToString() + "\\ressources\\day_" + 
                    (d < 10 ? "0" : "") + d.ToString() + "\\" + fileName ?? "";
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
    }
}
