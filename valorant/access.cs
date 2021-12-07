using System;
using valorant_stats;

namespace projects.valorant
{
  static class Valorant
  {
    // Valorant.XP(0, new int[] { 20, 30, 35, 40, 45, 50 }, new DateTime(2020, 10, 2));
    public static void XP(int currentLevel, int[] destinationLevels, DateTime seasonEnd)
    {
      XP xp = new XP(currentLevel, seasonEnd);
      xp.WriteSetup();
      xp.Write(destinationLevels);
    }

    // Valorant.MapSelector(new string[] { "GayLord", "FuckFace", "ShitLoad", "CuntNugget", "TurdOnAStrick" });
    public static void MapSelector(string[] playerNames)
    {
      string[] players = playerNames;
      MapSelector mapSelector = new MapSelector(players);
      mapSelector.PlayRandomAsTeam();
      mapSelector.PlayRandomAsTeam();
      mapSelector.PlayRandomAsTeam();
      mapSelector.PlayRandomAsTeam();

      mapSelector.SwitchPlayer("GayLord", "Mic");
      mapSelector.PlayRandomAsTeam();
    }
  }
}
