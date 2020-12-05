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
            bool valid = false; int n = 0;
            switch (field)
            {
                case "byr":
                    valid = int.TryParse(value, out n)
                        && n >= 1920 && n <= 2002;
                    break;
                case "iyr":
                    valid = int.TryParse(value, out n)
                        && n >= 2010 && n <= 2020;
                    break;
                case "eyr":
                    valid = int.TryParse(value, out n)
                        && n >= 2020 && n <= 2030;
                    break;
                case "hgt":
                    string[] m = Regex.Split(value, "^([0-9]*)(cm|in)$")
                        .Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    if (m.Length == 2 && int.TryParse(m[0], out n))
                        valid = m[1] == "in" ? n >= 59 && n <= 76 : n >= 150 && n <= 193;
                    break;
                case "hcl":
                    valid = Regex.IsMatch(value, "^#{1}([a-f]|[0-9]){6}$");
                    break;
                case "ecl":
                    valid = Array.Exists(new string[]
                    { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }, ec => ec == value);
                    break;
                case "pid":
                    valid = Regex.IsMatch(value, "^[0-9]{9}$");
                    break;
                case "cid":
                    valid = true;
                    break;
            }
            return valid;
        }

        private string Execute(bool validate)
        {
            string[] lines = GetFileLines(4);
            var fields = GetFields();

            int valid = 0;
            foreach (string l in lines)
            {
                if (l.Trim().Length == 0)
                {
                    if (!fields.ContainsValue(false))
                        ++valid;
                    fields = GetFields();
                }
                else
                {
                    string[] inputs = l.Split(':', ' ');
                    for (int i = 0; i < inputs.Length - 1; ++i)
                        if (fields.ContainsKey(inputs[i]))
                            fields[inputs[i]] = !validate ? true :
                                ValidateField(inputs[i], inputs[i + 1]);
                }
            }

            if (!fields.ContainsValue(false))
                ++valid;

            return valid.ToString();
        }

        public override string ExecuteFirst()
            => Execute(false);

        public override string ExecuteSecond()
            => Execute(true);
    }
}
