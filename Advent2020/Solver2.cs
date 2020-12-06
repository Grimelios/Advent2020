using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Solver2 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			var valid = 0;
			var chars = new int[26];

			foreach (var s in lines)
			{
				var tokens0 = s.Split(':');
				var tokens1 = tokens0[0].Split(' ');
				var tokens2 = tokens1[0].Split('-');
				var min = int.Parse(tokens2[0] + "");
				var max = int.Parse(tokens2[1] + "");
				var target = tokens1[1][0] - 'a';

				foreach (var c in tokens0[1].Substring(1))
				{
					chars[c - 'a']++;
				}

				if (chars[target] >= min && chars[target] <= max)
				{
					valid++;
				}

				for (int i = 0; i < 26; i++)
				{
					chars[i] = 0;
				}
			}

			return valid;
		}

		public override object SolvePart2(string[] lines)
		{
			var valid = 0;

			foreach (var s in lines)
			{
				var tokens0 = s.Split(':');
				var tokens1 = tokens0[0].Split(' ');
				var tokens2 = tokens1[0].Split('-');
				var p0 = int.Parse(tokens2[0] + "") - 1;
				var p1 = int.Parse(tokens2[1] + "") - 1;
				var target = tokens1[1][0];
				var s0 = tokens0[1].Substring(1);

				if ((s0[p0] == target) ^ (s0[p1] == target))
				{
					valid++;
				}
			}

			return valid;
		}
	}
}
