using System.Collections.Generic;

namespace aoc.puzzles._2020
{
  class Day07 : Day
  {
    private List<Bag> Bags;
    private void InitBags()
    {
      Bags = new List<Bag>();
      var lines = GetFileLines();
      foreach (string l in lines)
      {
        string[] bag = l.Split(" bags contain ");
        Bags.Add(new Bag(bag[0], bag[1].Split(", ")));
      }
    }

    private int CountShiny()
    {
      int count = 0;
      foreach (Bag bag in Bags)
        if (ContainsShiny(bag))
          ++count;
      return count;
    }

    private bool ContainsShiny(Bag bag)
    {
      if (bag == null)
        return false;
      if (bag.ContainsShiny())
        return true;

      foreach ((string, int) bs in bag.Bags)
        if (ContainsShiny(Bags.Find(b => b.Name == bs.Item1)))
          return true;

      return false;
    }

    public override string ExecuteFirst()
    {
      InitBags();
      return CountShiny().ToString();
    }

    private int CountBags(Bag bag)
    {
      int total = 0;
      foreach ((string, int) bs in bag.Bags)
        total += bs.Item2 + (bs.Item2 * CountBagsRec(Bags.Find(b => b.Name == bs.Item1), 0));
      return total;
    }
    private int CountBagsRec(Bag bag, int total)
    {
      if (bag != null)
        foreach ((string, int) bs in bag.Bags)
          total += (bs.Item2 * CountBagsRec(Bags.Find(b => b.Name == bs.Item1), bs.Item2));
      return total;
    }

    public override string ExecuteSecond()
    {
      InitBags();
      return CountBags(Bags.Find(b => b.Name == "shiny gold")).ToString();
    }

    class Bag
    {
      public string Name;
      public List<(string, int)> Bags = new List<(string, int)>();

      public Bag(string name, string[] bagList)
      {
        Name = name;
        if (bagList[0] == "no other bags.")
          return;

        foreach (string b in bagList)
        {
          int start = b.IndexOf(' ') + 1;
          Bags.Add(
              (b.Substring(start, b.LastIndexOf(' ') - start),
                  int.Parse(b.Substring(0, b.IndexOf(' ')))
          ));
        }
      }

      public bool ContainsShiny()
          => Bags.Exists(b => b.Item1 == "shiny gold");
    }
  }
}
