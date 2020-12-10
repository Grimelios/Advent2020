using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Solver10 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			var ints = lines.Select(s => int.Parse(s)).ToList();
			ints.Sort();

			var counts = new int[3];

			for (int i = 1; i < ints.Count; i++)
			{
				counts[ints[i] - ints[i - 1] - 1]++;
			}

			counts[ints[0] - 1]++;
			counts[2]++;

			return counts[0] * counts[2];
		}

		public override object SolvePart2(string[] lines)
		{
			var ints = lines.Select(s => int.Parse(s)).ToList();
			ints.Sort();
			ints.Insert(0, 0);
			ints.Add(ints.Max() + 3);

			var arrangements = new Dictionary<int, int[]>();
			arrangements.Add(ints.Last(), new [] { -1, -1, -1 });

			for (int i = 0; i < ints.Count - 1; i++)
			{
				var a0 = ints[i];
				var a1 = ints[i + 1];
				var a2 = i < ints.Count - 2 ? ints[i + 2] : -1;
				var a3 = i < ints.Count - 3 ? ints[i + 3] : -1;
				var a4 = i < ints.Count - 4 ? ints[i + 4] : -1;
				var array = new int[4];
				array[0] = a1;
				array[3] = a4;

				if (a2 - a0 <= 3)
				{
					array[1] = a2;
				}

				if (a3 - a0 <= 3)
				{
					array[2] = a3;
				}

				arrangements.Add(a0, array);
			}

			var map = new Dictionary<int, long>();

			long Recurse(int joltage)
			{
				if (joltage == -1)
				{
					return 1;
				}

				if (map.TryGetValue(joltage, out var v))
				{
					return v;
				}

				var array = arrangements[joltage];
				var available = 1;

				for (int i = 1; i < 3; i++)
				{
					if (array[i] > 0)
					{
						available++;
					}
				}

				var options = 1L;

				switch (available)
				{
					case 1:
					{
						options = Recurse(array[0]);

						break;
					}

					case 2:
					{
						var b0 = array[3] - array[0] <= 3;

						if (b0)
						{
							options = 3 * Recurse(array[1]);
						}
						else
						{
							options = 2 * Recurse(array[1]);
						}

						break;
					}

					case 3:
					{
						var b0 = array[3] - array[0] <= 3;
						var b1 = array[3] - array[1] <= 3;

						if (b1)
						{
							if (b0)
							{
								options = 7 * Recurse(array[2]);
							}
							else
							{
								options = 6 * Recurse(array[2]);
							}
						}
						else
						{
							options = 4 * Recurse(array[2]);
						}

						break;
					}
				}

				map.Add(joltage, options);

				return options;
			}

			return Recurse(ints[0]);
		}
	}
}
