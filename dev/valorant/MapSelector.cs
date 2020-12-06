using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace valorant_stats
{

    class MapSelector
    {
        private Team _team;

        public MapSelector(string[] players)
            => _team = new Team(players);

        public Map GetNextMap()
        {
            List<Map> maps = Maps.GetAll();

            _team.AdjustProbabilities(maps);

            WriteStats(maps);

            Map nextMap = maps.RandomElementByWeight(e => e.Probability);
            Console.WriteLine("CHOSEN MAP: " + nextMap.Name);
            Console.WriteLine("-----------------------------------");
            return nextMap;
        }

        private void WriteStats(List<Map> maps)
            => maps.ForEach(m => m.Write());

        public void PlayRandomAsTeam()          => PlayAsTeam(GetNextMap());
        public void PlayAsTeam(string mapName)  => PlayAsTeam(Maps.Get(mapName));
        public void PlayAsTeam(Map m)           => _team.Play(m);

        public void SwitchPlayer(string toKick, string toAdd)
        {
            _team.Kick(toKick);
            _team.Add(toAdd);
        }

        public void PlayOnePlayer(string mapName, string playerName)
            => PlayOnePlayer(Maps.Get(mapName), playerName);
        public void PlayOnePlayer(Map m, string playerName)
            => _team.Play(m, playerName);
    }

}
