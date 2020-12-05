using System;
using System.Collections.Generic;

namespace dev.taxes
{

    class Brackets
    {
        private class Bracket
        {
            public readonly int MaxIncome;
            public readonly double Rate;

            public Bracket(int maxIncome, double rate)
            {
                MaxIncome = maxIncome;
                Rate = rate;
            }
        }

        private List<Bracket> federal = new List<Bracket>();
        private List<Bracket> provincial = new List<Bracket>();

        public Brackets()
        {
            int[] fed_incomes = new int[] { 47630, 95259, 147667, 210371, int.MaxValue },
                prov_incomes = new int[] { 44545, 89080, 108390, int.MaxValue };
            double[] fed_rates = new double[] { 0.15, 0.205, 0.26, 0.29, 0.33 },
                prov_rates = new double[] { 0.15, 0.20, 0.24, 0.2575 };

            for (int i = 0; i < fed_incomes.Length; ++i)
                federal.Add(new Bracket(fed_incomes[i], fed_rates[i]));
            for (int i = 0; i < prov_incomes.Length; ++i)
                provincial.Add(new Bracket(prov_incomes[i], prov_rates[i]));
        }

        public double[] GetDeductions(int salary)
            => new double[] { CalculateBrackets(ref federal, salary), CalculateBrackets(ref provincial, salary) };

        public void WriteDeductions(int salary)
        {
            double[] deductions = GetDeductions(salary);
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Salary:                      " + salary + "$");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Federal Deductions:          " + deductions[0] + "$");
            Console.WriteLine("Provincial Deductions:       " + deductions[1] + "$");
            Console.WriteLine("Net Salary:                  " + (salary - (deductions[0] + deductions[1])) + "$");
            Console.WriteLine("\n");
        }

        private double CalculateBrackets(ref List<Bracket> brackets, int salary, int i = 0, double deductions = 0)
        {
            if (salary <= 0)
                return deductions;

            Bracket cb = brackets[i];
            return CalculateBrackets(ref brackets, 
                salary - (i == 0 ? cb.MaxIncome : cb.MaxIncome - brackets[i - 1].MaxIncome), 
                i < brackets.Count - 1 ? i + 1 : i, 
                deductions + (salary > cb.MaxIncome ? cb.MaxIncome * cb.Rate : salary * cb.Rate));
        } 
    }
}