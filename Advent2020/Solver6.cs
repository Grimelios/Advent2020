using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Solver6 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			var counts = new List<int>();
			var answers = new bool[26];

			foreach (var s in lines)
			{
				if (s == "")
				{
					counts.Add(answers.Count(a => a));

					for (int i = 0; i < 26; i++)
					{
						answers[i] = false;
					}

					continue;
				}

				foreach (var c in s)
				{
					answers[c - 'a'] = true;
				}
			}
			
			counts.Add(answers.Count(a => a));

			return counts.Sum();
		}

		public override object SolvePart2(string[] lines)
		{
			var counts = new List<int>();
			var answers = new int[26];
			var group = 0;

			foreach (var s in lines)
			{
				if (s == "")
				{
					counts.Add(answers.Count(a => a == group));

					for (int i = 0; i < 26; i++)
					{
						answers[i] = 0;
					}

					group = 0;

					continue;
				}

				group++;

				foreach (var c in s)
				{
					answers[c - 'a']++;
				}
			}
			
			counts.Add(answers.Count(a => a == group));

			return counts.Sum();
		}
	}
}
