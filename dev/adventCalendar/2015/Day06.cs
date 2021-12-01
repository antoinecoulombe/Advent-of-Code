using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2015
{
  class Day06 : Day
  {
    private short[][] lights;

    private class Instruction
    {
      public (int x, int y) from;
      public (int x, int y) to;
      public OP operation;

      public enum OP { toggle, turn_on, turn_off }

      public Instruction(string s)
      {
        var parts = s.Split(' ');
        operation = parts[0] == "toggle" ? OP.toggle :
            (parts[1] == "on" ? OP.turn_on : OP.turn_off);

        var f = parts[operation == OP.toggle ? 1 : 2].Split(',').Select(int.Parse).ToList();
        var t = parts[^1].Split(',').Select(int.Parse).ToList();
        from = (f[0], f[1]);
        to = (t[0], t[1]);
      }
    }

    private List<Instruction> GetInstructions()
    {
      List<Instruction> inst = new();
      var l = GetFileLines();
      foreach (string s in l)
        inst.Add(new Instruction(s));
      return inst;
    }

    private void InitLights()
    {
      lights = new short[1000][];
      for (int i = 0; i < lights.Length; ++i)
        lights[i] = new short[1000];
    }

    private void ExecuteInstructions(List<Instruction> inst, bool add = false)
    {
      InitLights();
      foreach (var i in inst)
      {
        for (int y = i.from.y; y <= i.to.y; ++y)
        {
          for (int x = i.from.x; x <= i.to.x; ++x)
          {
            if (!add)
              lights[y][x] = (short)(i.operation == Instruction.OP.toggle ?
                  (lights[y][x] == 0 ? 1 : 0) : (i.operation == Instruction.OP.turn_off ? 0 : 1));
            else
              lights[y][x] += (short)(i.operation == Instruction.OP.toggle ? 2 :
                  (i.operation == Instruction.OP.turn_off ? (lights[y][x] > 0 ? -1 : 0) : 1));
          }
        }
      }
    }

    private string Execute(bool add)
    {
      ExecuteInstructions(GetInstructions(), add);
      return lights.Sum(x => x.Sum(y => y)).ToString();
    }

    public override string ExecuteFirst()
        => Execute(false);

    public override string ExecuteSecond()
        => Execute(true);
  }
}
