using System;
using System.Collections.Generic;
using System.Linq;
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
			
			var r = 0;
			var step = buses[0].Bus;

			for (int bus = 0; bus < buses.Length - 1; bus++)
			{
				var a = buses[bus].Bus;
				var b = buses[bus + 1].Bus;
				var u = buses[bus + 1].Index;
				var m = 1;

				while (true)
				{
					var t = m * a;

					if (t % b == b - u)
					{
						r += t;
						step *= b;

						break;
					}

					m++;
				}
			}

			return null;
		}
	}
}
