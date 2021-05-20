using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2015
{
    class Day09 : Day
    {
        private class Route
        {
            public string from;
            public string to;
            public int distance;

            public Route(string s)
            {
                var split = s.Split(" to ");
                from = split[0];
                split = split[1].Split(" = ");
                to = split[0];
                distance = int.Parse(split[1]);
            }
        }

        private List<Route> GetRoutes()
            => GetFileLines(9, 15).Select(s => new Route(s)).ToList();

        private int countCities(List<Route> routes)
        {
            List<string> cities = new();
            foreach (var r in routes)
            {
                if (!cities.Contains(r.from))
                    cities.Add(r.from);
                if (!cities.Contains(r.to))
                    cities.Add(r.to);
            }
            return cities.Count;
        }

        private List<List<string>> GetPossibilities(List<Route> routes)
        {
            List<List<string>> poss = new();
            poss.Add(new List<string> { routes[0].from });

            while (routes.Count > 0)
            {
                foreach (var p in poss)
                {

                }
            }
            return poss;
        }

        public override string ExecuteFirst()
        {
            var routes = GetRoutes();
            var cityCount = countCities(routes);
            var poss = GetPossibilities(routes);
            return "";
        }

        public override string ExecuteSecond()
        {
            return "";
        }
    }
}
