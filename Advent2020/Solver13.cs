using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Solver13 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			var earliest = int.Parse(lines[0]);
			var buses = lines[1].Split(',').Where(s => s[0] != 'x').Select(s => int.Parse(s)).ToArray();
			var nearest = int.MaxValue;
			var bus = 0;

			foreach (var b in buses)
			{
				var t0 = earliest / b;
				var t1 = b * (t0 + 1);
				var m = t1 % earliest;

				if (m < nearest)
				{
					nearest = m;
					bus = b;
				}
			}

			return bus * nearest;
		}

		public override object SolvePart2(string[] lines)
		{
			var tokens = lines[1].Split(',');
			var buses = new (int Bus, int Index)[tokens.Count(s => s[0] != 'x')];
			var index = 0;

			for (int i = 0; i < tokens.Length; i++)
			{
				if (int.TryParse(tokens[i], out var bus))
				{
					buses[index++] = (bus, i);
				}
			}
			
			var r = new BigInteger();
			var step = new BigInteger(buses[0].Bus);
			var t = new BigInteger();

			for (int bus = 1; bus < buses.Length; bus++)
			{
				var b = buses[bus].Bus;
				var u = buses[bus].Index - buses[bus - 1].Index;
				var m = 1;

				while (true)
				{
					t = m * step + r;

					if ((t + buses[bus - 1].Index) % b == b - u)
					{
						r = t;
						step *= b;

						break;
					}

					m++;
				}
			}

			return t;
		}
	}
}
