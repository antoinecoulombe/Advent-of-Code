namespace dev.taxes
{
    static class Taxes
    {
        // Taxes.WriteDeductions(new int[] { 90000, 100000, 110000 });
        public static void WriteDeductions(int[] salaries)
        {
            Brackets b = new Brackets();
            foreach (int s in salaries)
                b.WriteDeductions(s);
        }
    }
}
