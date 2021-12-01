using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace dev.adventCalendar
{
  abstract class Day
  {
    private static string sep = "\\";
    private static string baseFolder = "dev" + sep;

    public abstract string ExecuteFirst();
    public abstract string ExecuteSecond();

    protected static string GetRessourcePath(string fileName = null)
    {
      try
      {
        // Get longest caller path
        var caller = new StackTrace().GetFrames().Select(x =>
        {
          string s = x.GetMethod().DeclaringType.AssemblyQualifiedName;
          return s.Substring(0, s.IndexOf(',')).Split('.');
        }).ToArray().Aggregate((x, y) => x.Length > y.Length ? x : y);
        string path = "";

        if (Program.IsLinux())
        {
          sep = "/";
          path = Environment.CurrentDirectory + sep;
        }
        else
          path = System.Reflection.Assembly.GetExecutingAssembly().Location
              .Substring(0, path.LastIndexOf(baseFolder) + baseFolder.Length);
        return $"{path}adventCalendar{sep}{caller[2].Substring(1)}{sep}ressources{sep}day_{caller[3].Substring(3)}{sep}{fileName}";
      }
      catch (Exception) { return null; }
    }

    protected static string[] GetFileLines(string fileName = "input.txt")
    {
      try
      {
        return File.ReadAllLines(GetRessourcePath(fileName));
      }
      catch (Exception e) { Console.WriteLine(e.Message); return null; }
    }
    protected static string GetFileText(string fileName = "input.txt")
    {
      try { return File.ReadAllText(GetRessourcePath(fileName)); }
      catch (Exception e) { Console.WriteLine(e.Message); return null; }
    }

    protected static int[] GetIntegers(string fileName = "input.txt")
    {
      try { return Array.ConvertAll(GetFileLines(fileName), sn => int.Parse(sn)); }
      catch (Exception e) { Console.WriteLine(e.Message); return null; }
    }

    protected static double[] GetDoubles(string fileName = "input.txt")
    {
      try { return Array.ConvertAll(GetFileLines(fileName), sn => double.Parse(sn)); }
      catch (Exception e) { Console.WriteLine(e.Message); return null; }
    }

    protected static void WriteToFile(string line, string fileName = "output.txt")
        => WriteToFile(new List<string>() { line }, fileName);

    protected static void WriteToFile(List<string> lines, string fileName = "output.txt")
        => File.WriteAllLines(GetRessourcePath(fileName), lines);
  }
}
