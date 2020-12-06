using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Solver5 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			return lines.Max(s => ComputeSeat(s));
		}

		public override object SolvePart2(string[] lines)
		{
			var seats = lines.Select(s => ComputeSeat(s)).ToList();
			seats.Sort();

			var missing = seats[0] + 1;

			for (int i = 1; i < seats.Count - 1; i++)
			{
				if (seats[i] != missing)
				{
					break;
				}

				missing++;
			}

			return missing;
		}
		
		private int ComputeSeat(string s)
		{
			var min = 0;
			var max = 127;

			for (int i = 0; i < 6; i++)
			{
				switch (s[i])
				{
					case 'F': max -= (int)Math.Ceiling((max - min) / 2f); break;
					case 'B': min += (int)Math.Ceiling((max - min) / 2f); break;
				}
			}

			var row = s[6] == 'F' ? min : max;

			min = 0;
			max = 7;

			for (int i = 0; i < 2; i++)
			{
				switch (s[i + 7])
				{
					case 'L': max -= (int)Math.Ceiling((max - min) / 2f); break;
					case 'R': min += (int)Math.Ceiling((max - min) / 2f); break;
				}
			}

			var seat = s[9] == 'L' ? min : max;

			return row * 8 + seat;
		}
	}
}
