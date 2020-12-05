namespace dev.filter
{
    class Word
    {
        public string Singular { get; private set; }
        public string Plural { get; private set; }

        public Word(string singular, string plural)
        {
            Singular = singular;
            Plural = plural;
        }

        public bool IsSame(string word)
            => Singular == word || Plural == word;
    }
}
