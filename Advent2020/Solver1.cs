using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Solver1 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			var ints = lines.Select(s => int.Parse(s)).ToArray();

			for (int i = 0; i < ints.Length; i++)
			{
				for (int j = i + 1; j < ints.Length; j++)
				{
					if (ints[i] + ints[j] == 2020)
					{
						return ints[i] * ints[j];
					}
				}
			}

			return null;
		}

		public override object SolvePart2(string[] lines)
		{
			var ints = lines.Select(s => int.Parse(s)).ToArray();

			for (int i = 0; i < ints.Length; i++)
			{
				for (int j = i + 1; j < ints.Length; j++)
				{
					for (int k = j + 1; k < ints.Length; k++)
					{
						if (ints[i] + ints[j] + ints[k] == 2020)
						{
							return ints[i] * ints[j] * ints[k];
						}
					}
				}
			}

			return null;
		}
	}
}
