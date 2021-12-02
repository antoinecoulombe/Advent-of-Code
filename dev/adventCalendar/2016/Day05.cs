using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace dev.adventCalendar._2016
{
  class Day05 : Day
  {
    private static string input = "ffykfhsq";
    private static int pwdSize = 8;
    private static string startsWith = "00000";

    private static string CreateMD5(string input)
    {
      byte[] inputBytes = Encoding.ASCII.GetBytes(input);
      byte[] hashBytes = MD5.Create().ComputeHash(inputBytes);

      StringBuilder sb = new();
      for (int i = 0; i < hashBytes.Length; i++)
        sb.Append(hashBytes[i].ToString("X2"));
      return sb.ToString();
    }

    private static int HexToInt(char hexChar)
    {
      hexChar = char.ToUpper(hexChar);
      return (int)hexChar < (int)'A' ?
          ((int)hexChar - (int)'0') :
          10 + ((int)hexChar - (int)'A');
    }

    private static bool validateHash(string hash, int[] positions, int pos)
    {
      return pos < positions.Length && positions[pos] == 0;
    }

    private static int[] GetNumbersStartingWith(string start, int pwdLength, bool validate = false)
    {
      var digits = new List<int>();
      int[] positions = new int[pwdLength];

      int i = 0;
      while (digits.Count < pwdLength)
      {
        var hash = CreateMD5(input + i.ToString());
        var pos = HexToInt(hash[5]);
        if (hash.StartsWith(start) && (!validate || validateHash(hash, positions, pos)))
        {
          if (validate)
            positions[pos] = 1;
          digits.Add(i);
        }
        ++i;
      }
      return digits.ToArray();
    }

    public override string ExecuteFirst()
    {
      var numbers = GetNumbersStartingWith(startsWith, pwdSize);
      return String.Join("", numbers.Select(x => CreateMD5(input + x)).Select(x => x[5]));
    }

    public override string ExecuteSecond()
    {
      var hashes = GetNumbersStartingWith(startsWith, pwdSize, true).Select(x => CreateMD5(input + x)).OrderBy(x => x[5]);
      return String.Join("", hashes.Select(x => x[6]));
    }
  }
}
