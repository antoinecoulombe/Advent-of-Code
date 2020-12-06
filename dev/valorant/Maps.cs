using System;
using System.Collections.Generic;
using System.Linq;

namespace valorant_stats
{
    static class Maps
    {
        public readonly static string[] Names = { "BIND", "HAVEN", "SPLIT", "ASCENT" };

        private static List<Map> ToList(string[] maps)
            => maps.ToList().Select(name => new Map(name, decimal.Divide(1, maps.Length))).ToList();

        public static List<Map> GetAll() => ToList(Names);
        public static Map GetRandom() => GetAll().RandomElementByWeight(e => e.Probability);
        public static Map Get(string name) => GetAll().Find(m => m.Name == name);
        public static Map Get(int index) => GetAll()[index];
        public static Map Clean(Map map) => GetAll().Find(m => m.Name == map.Name);
    }

    class Map
    {
        public string Name { get; private set; }
        public decimal Probability { get; private set; }

        public Map(string name, decimal probability)
        {
            Name = name;
            Probability = probability;
        }

        public void AdjustProbability(decimal adjustment)
            => Probability += adjustment;

        public void Write()
            => Console.WriteLine(Name.ToUpper() + " (" + Math.Round(Probability * 100, 2) + "%).");
    }
}
