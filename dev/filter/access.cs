using System;
using System.Collections.Generic;

namespace dev.filter
{
    static class WordFilter
    {
        //WordFilter.Clean("");
        //WordFilter.Clean("", new List<Word>() { new Word("", "") });
        public static void Clean(string phrase, List<Word> banned = null)
        {
            Filter f = banned == null ? new Filter(phrase) : 
                new Filter(phrase, banned);

            Console.WriteLine(phrase);
            Console.WriteLine("    => " + f.GetEmphasis());
        }

        public static void Test()
        {
            Clean("When will the extras be released?");
            Clean("When will    the extras    be   released?");
            Clean("When will    the eeexXXtras    be   released?");
            Clean("When will    the ee333exXXtr44as    be   released?");
            Clean("When will    the ee333exXXtr44a5    be   released?");
            Clean("When will    the 3X7r45    be   released?");
            Clean("Wh3n w1ll    th3 3X7r45    b3   r3l3453d?");
        }
    }
}
