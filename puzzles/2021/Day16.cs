using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc.puzzles._2021
{
	class Day16 : Day
	{
		private string ToBinary(char hex)
		{
			string b = Convert.ToString(Convert.ToInt32(hex.ToString(), 16), 2);
			return new string('0', 4 - b.Length) + b;
		}

		public override string ExecuteFirst()
		{
			var binary = String.Join("", GetFileText().Select(x => ToBinary(x)));
			int versionTotal = 0;
			for (int i = 0; i < binary.Length;)
			{
				int version = Convert.ToInt32(binary.Substring(i, 3), 2);
				i += 3;

				versionTotal += version;

				int id = Convert.ToInt32(binary.Substring(i, 3), 2);
				i += 3;

				if (id == 4)
				{
					string number = "";
					while (binary[i] != '0')
					{
						number += binary.Substring(i += 1, 4);
						i += 4;
					}
					number += binary.Substring(i += 1, 4);
					i += 4;

					var remainder = number.Length % 4;
					if (remainder != 0) i += 4 - remainder;

					int result = Convert.ToInt32(number, 2);
				}
				else
				{
					if (binary[i] == '1')
					{
						int pcktNumber = Convert.ToInt32(binary.Substring(i += 1, 11), 2);
						i += 11;
						for (int j = 0; j < pcktNumber; ++j)
						{
							versionTotal += Convert.ToInt32(binary.Substring(i + 6, 5), 2);
							i += 11;
						}
					}
					else
					{
						string pckt = binary.Substring(i += 1, 15);
						int pcktLength = Convert.ToInt32(pckt, 2);
						i += 15;
						i += pcktLength;
					}
				}
			}
			return versionTotal.ToString();
		}

		public override string ExecuteSecond()
		{
			return "";
		}
	}
}
