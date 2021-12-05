using dev.adventCalendar;
using projects.valorant;

using System;

namespace dev
{
  class Program
  {
    #region Utilities

    public static bool IsLinux()
    {
      int p = (int)Environment.OSVersion.Platform;
      return (p == 4) || (p == 6) || (p == 128);
    }

    #endregion

    static void Main(string[] args)
    {
      Console.Clear();
      AdventCalendar.ExecuteDay(5, 2021);
      // Console.ReadLine();
    }
  }
}
