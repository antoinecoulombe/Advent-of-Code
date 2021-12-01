using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace dev.adventCalendar._2020
{
  class Day04 : Day
  {
    private Dictionary<string, bool> GetFields()
    {
      return new Dictionary<string, bool>()
            {
                { "byr", false }, { "iyr", false }, { "eyr", false },
                { "hgt", false }, { "hcl", false }, { "ecl", false },
                { "pid", false }, { "cid", true }
            };
    }

    private bool ValidateField(string field, string value)
    {
      int n = 0;
      switch (field)
      {
        case "byr":
          return int.TryParse(value, out n)
              && n >= 1920 && n <= 2002;
        case "iyr":
          return int.TryParse(value, out n)
              && n >= 2010 && n <= 2020;
        case "eyr":
          return int.TryParse(value, out n)
              && n >= 2020 && n <= 2030;
        case "hgt":
          var m = Regex.Split(value, "^([0-9]*)(cm|in)$")
              .Where(x => !string.IsNullOrEmpty(x)).ToArray();
          if (m.Length == 2 && int.TryParse(m[0], out n))
            return m[1] == "in" ? n >= 59 && n <= 76 : n >= 150 && n <= 193;
          else
            return false;
        case "hcl":
          return Regex.IsMatch(value, "^#{1}([a-f]|[0-9]){6}$");
        case "ecl":
          return Array.Exists(new string[]
          { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }, ec => ec == value);
        case "pid":
          return Regex.IsMatch(value, "^[0-9]{9}$");
        case "cid":
          return true;
        default:
          return false;
      }
    }

    private string Execute(bool validate)
    {
      var fields = GetFields();
      int valid = 0;
      foreach (string l in GetFileLines())
      {
        if (l.Trim().Length == 0)
        {
          if (!fields.ContainsValue(false))
            ++valid;
          fields = GetFields();
        }
        else
        {
          var inputs = l.Split(':', ' ');
          for (int i = 0; i < inputs.Length - 1; ++i)
            if (fields.ContainsKey(inputs[i]))
              fields[inputs[i]] = !validate || ValidateField(inputs[i], inputs[i + 1]);
        }
      }

      return (valid + (!fields.ContainsValue(false) ? 1 : 0)).ToString();
    }

    public override string ExecuteFirst()
        => Execute(false);

    public override string ExecuteSecond()
        => Execute(true);
  }
}
