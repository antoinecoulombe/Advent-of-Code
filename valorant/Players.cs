using System;
using System.Collections.Generic;
using System.Linq;

namespace valorant_stats
{
    class Team
    {
        private List<Player> _players = new List<Player>();

        public Team(string[] players)
            => players.ToList().ForEach(name => _players.Add(new Player(name)));

        public void Play(Map m, string playerName = null)
        {
            if (playerName == null)
                _players.ForEach(p => p.Play(m));
            else
                _players.Find(p => p.Name == playerName).Play(m);
        }

        public void Swap(Player toKick, Player toAdd)
        {
            Kick(toKick);
            Add(toAdd);
        }

        public void Add(Player p)
            => _players.Add(p);

        public void Kick(Player p)
            => _players.Remove(p);

        public void Add(string name)
            => Add(new Player(name));

        public void Kick(string name)
            => Kick(_players.Find(p => p.Name == name));

        public void AdjustProbabilities(List<Map> maps)
            => _players.ForEach(p => p.AdjustAllProbabilities(maps, _players.Count));
    }

    class Player
    {
        private List<Map> PreviousMaps = new List<Map>();
        public string Name { get; private set; }

        public Player(string name)
            => Name = name;

        public void Play(Map map)
            => PreviousMaps.Add(Maps.Clean(map));

        public Map GetLastMap()
            => GetPreviousMap();

        public Map GetPreviousMap(Map map = null)
        {
            if (map == null && PreviousMaps.Count > 0)
                return PreviousMaps[PreviousMaps.Count - 1];

            int index = PreviousMaps.FindLastIndex(e => e.Name == map.Name) - 1;
            return index >= 0 ? PreviousMaps[index] : null;
        }

        public void AdjustAllProbabilities(List<Map> maps, int playerCount)
        {
            Map lastMap = GetLastMap();
            Map prevMap = GetPreviousMap(lastMap);
            AdjustProbabilities(maps, lastMap, playerCount);
            AdjustProbabilities(maps, GetPreviousMap(lastMap), playerCount, 2);
        }

        private void AdjustProbabilities(List<Map> maps, Map playedMap, int playerCount, int divider = 1)
        {
            if (playedMap == null) return;

            decimal toDeduct = decimal.Divide(decimal.Divide(playedMap.Probability, playerCount), divider),
                    toAdd = decimal.Divide(toDeduct, maps.Count - 1);

            maps.ForEach(m => m.AdjustProbability(m.Name != playedMap.Name ? toAdd : -toDeduct));
        }
    }
}
