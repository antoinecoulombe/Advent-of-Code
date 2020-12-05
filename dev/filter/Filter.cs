using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace dev.filter
{
    class Filter
    {
        private readonly List<Word> bannedWords = new List<Word>()
        {
            new Word("extra", "extras")
        };

        private readonly string toFilter;
        private Dictionary<int, string> authorized = new Dictionary<int, string>();
        private Dictionary<int, string> banned = new Dictionary<int, string>();

        public Filter(string s, List<Word> banned) : this(s)
            => bannedWords = banned;

        public Filter(string s)
        {
            toFilter = s;
            FilterString();
        }

        public void FilterString()
        {
            SeparateWords(RemoveConsecutiveSpaces(toFilter.ToLower()).Split(" ").ToList());
        }

        public string GetAuthorized()
            => string.Join(" ", authorized);

        public string GetBanned()
            => string.Join(" ", banned);

        public string GetEmphasis()
        {
            string result = "";

            for (int i = 0; i < (authorized.Count + banned.Count); ++i)
                result += (authorized.ContainsKey(i) ? authorized[i] : "[" + banned[i] + "]") + " ";

            return result.Length <= 1 ? result : result.Substring(0,result.Length - 1);
        }

        private void SeparateWords(List<string> words)
        {
            for (int i = 0; i  < words.Count; ++i)
            {
                if (ContainsAlphas(words[i]))
                {
                    string cleanWord = RemoveConsecutive(ConvertNumbers(words[i]));
                    if (bannedWords.Exists(x => x.IsSame(cleanWord)))
                        banned.Add(i, words[i]);
                    else
                        authorized.Add(i, words[i]);
                }
            }
        }

        private string ConvertNumbers(string word)
        {
            Dictionary<char, char> dict = new Dictionary<char, char>
            {
                { '0', 'o' },
                { '1', 'i' },
                { '3', 'e' },
                { '4', 'a' },
                { '5', 's' },
                { '6', 'b' },
                { '7', 't' }
            };

            string cleaned = "";
            foreach (char c in word)
                cleaned += dict.ContainsKey(c) ? dict[c] : c;
            return cleaned;
        }

        private bool ContainsAlphas(string word)
        {
            foreach (char c in word)
                if (c < '0' || c > '9')
                    return true;
            return false;
        }

        private string RemoveConsecutiveSpaces(string s)
            => new Regex("[ ]{2,}", RegexOptions.None).Replace(s, " ");

        private string RemoveConsecutive(string s)
        {
            if (s.Length < 2)
                return s;

            string cleaned = s[0].ToString();
            for (int i = 1; i < s.Length; ++i)
                if (s[i - 1] != s[i])
                    cleaned += s[i];
            return cleaned;
        }
    }
}
