using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Solver15 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			return Solve(lines, 2020);
		}

		public override object SolvePart2(string[] lines)
		{
			return Solve(lines, 30000000);
		}

		private int Solve(string[] lines, int limit)
		{
			var ints = lines[0].Split(',').Select(s => int.Parse(s)).ToArray();
			var memory = new Dictionary<int, (int SpokenAt, int SpokeAtBefore, bool IsFirst)>();

			for (int i = 0; i < ints.Length; i++)
			{
				memory.Add(ints[i], (i, i, true));
			}

			var last = ints.Last();

			for (int i = ints.Length; i < limit; i++)
			{
				var (spokenAt, spokeAtBefore, isFirst) = memory[last];

				if (isFirst)
				{
					var b = memory.TryGetValue(0, out var entry);

					memory[0] = (i, entry.SpokenAt, !b);
					last = 0;
				}
				else
				{
					var age = spokenAt - spokeAtBefore;
					var b = memory.TryGetValue(age, out var entry);

					memory[age] = (i, entry.SpokenAt, !b);
					last = age;
				}
			}

			return last;
		}
	}
}
