using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dev.adventCalendar._2020
{
  class Day14 : Day
  {
    private List<(string, long, string)> GetMemory()
    {
      var lines = GetFileLines();
      var mem = new List<(string, long, string)>();
      foreach (string l in lines)
      {
        var value = l.Substring(l.IndexOf('=') + 2);
        if (l.StartsWith("mask"))
          mem.Add(("mask", -1, value));
        else
        {
          var add = int.Parse(l.Substring(4, l.IndexOf(']') - 4));
          mem.Add(("mem", add, value));
        }
      }
      return mem;
    }

    private string ToBinary(long x, int size)
    {
      string b = Convert.ToString(x, 2);
      return new string('0', size - b.Length) + b;
    }

    #region First Part

    private List<(string, long, string)> RemoveDuplicates(List<(string, long, string)> mem)
    {
      var newMem = new List<(string, long, string)>();

      for (int i = mem.Count - 1; i >= 0; --i)
        if (mem[i].Item1 == "mask" || newMem.Find(x => x.Item1 != "mask" && x.Item2 == mem[i].Item2) == default)
          newMem.Add(mem[i]);

      newMem.Reverse();
      return newMem;
    }

    private string ApplyValueMask(StringBuilder num, string mask)
    {
      if (num.Length != mask.Length)
        throw new Exception("The binary number and mask are not the same length.");

      for (int i = 0; i < num.Length; ++i)
        if (mask[i] != 'X')
          num[i] = mask[i];

      return num.ToString();
    }

    private void DecodeValues(ref List<(string type, long add, string val)> mem)
    {
      string mask = "";
      for (int i = 0; i < mem.Count; ++i)
      {
        if (mem[i].type == "mask")
        {
          mask = mem[i].val;
          continue;
        }

        var binary = ToBinary(long.Parse(mem[i].val), 36);
        var masked = ApplyValueMask(new StringBuilder(binary), mask);
        var converted = Convert.ToInt64(masked, 2).ToString();
        mem[i] = (mem[i].type, mem[i].add, converted);
      }
    }

    private long AddValues(List<(string, long, string)> mem)
        => mem.Sum(x => x.Item1 == "mem" ? long.Parse(x.Item3) : 0);

    #endregion

    #region Second Part

    private string ApplyAddressMask(StringBuilder addr, string mask)
    {
      if (addr.Length != mask.Length)
        throw new Exception("The binary number and mask are not the same length.");

      for (int i = 0; i < addr.Length; ++i)
      {
        if (mask[i] == '1')
          addr[i] = '1';
        else if (mask[i] == 'X')
          addr[i] = 'X';
      }

      return addr.ToString();
    }

    private List<string> GetAddressesFromMask(string addr)
    {
      var addresses = new List<string>();
      addresses.Add(addr);

      for (int pos = addr.Length - 1; pos >= 0; --pos)
      {
        if (addr[pos] != 'X')
          continue;

        int count = addresses.Count;
        for (int i = 0; i < count; ++i)
        {
          addresses.Add(addresses[0].Substring(0, pos) + '0' + addresses[0].Substring(pos + 1));
          addresses.Add(addresses[0].Substring(0, pos) + '1' + addresses[0].Substring(pos + 1));
          addresses.RemoveAt(0);
        }
      }

      return addresses;
    }

    private long DecodeAddresses(List<(string type, long add, string val)> mem)
    {
      var newAddresses = new List<(string, long)>();
      string mask = "";
      long total = 0;
      for (int i = 0; i < mem.Count; ++i)
      {
        if (mem[i].type == "mask")
        {
          mask = mem[i].val;
          continue;
        }

        var binary = ToBinary(mem[i].add, 36);
        var masked = ApplyAddressMask(new StringBuilder(binary), mask);
        var addresses = GetAddressesFromMask(masked);
        var value = long.Parse(mem[i].val);

        foreach (string addr in addresses)
        {
          var exist = newAddresses.Find(x => x.Item1 == addr);

          if (exist != default)
          {
            total -= exist.Item2;
            newAddresses.Remove(exist);
          }

          newAddresses.Add((addr, value));
          total += value;
        }
      }
      return total;
    }

    #endregion

    public override string ExecuteFirst()
    {
      var mem = GetMemory();
      mem = RemoveDuplicates(mem);
      DecodeValues(ref mem);
      return AddValues(mem).ToString();
    }

    public override string ExecuteSecond()
    {
      var memBase = GetMemory();
      var total = DecodeAddresses(memBase);
      return total.ToString();
    }
  }
}
