using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Solver16 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			var rules = ParseRules(lines, out var index);
			var sum = 0;

			for (int i = index + 5; i < lines.Length; i++)
			{
				var values = lines[i].Split(',').Select(s => int.Parse(s));

				foreach (var v in values)
				{
					var isValid = false;

					foreach (var r in rules)
					{
						foreach (var range in r.Ranges)
						{
							if (v >= range.Min && v <= range.Max)
							{
								isValid = true;

								break;
							}
						}

						if (isValid)
						{
							break;
						}
					}

					if (!isValid)
					{
						sum += v;
					}
				}
			}

			return sum;
		}

		public override object SolvePart2(string[] lines)
		{
			var rules = ParseRules(lines, out var index);
			var tickets = FilterTickets(lines, rules, index);
			var order = DetermineFieldOrder(rules, tickets);
			var t = lines[index + 2].Split(',').Select(s => int.Parse(s)).ToArray();
			var m = 1L;

			for (int i = 0; i < order.Length; i++)
			{
				if (order[i].StartsWith("departure"))
				{
					m *= t[i];
				}
			}

			return m;
		}

		private (string Rule, (int Min, int Max)[] Ranges)[] ParseRules(string[] lines, out int index)
		{
			var rules = new List<(string Rule, (int Min, int Max)[] Ranges)>();
			
			index = 0;

			do
			{
				var s = lines[index++];
				var colon = s.IndexOf(':');
				var rule = s.Substring(0, colon);
				var tokens = s.Substring(colon + 2).Split(' ');
				var r = new List<(int Min, int Max)>();

				for (int i = 0; i < tokens.Length; i += 2)
				{
					var s0 = tokens[i];
					var dash = s0.IndexOf('-');
					var min = int.Parse(s0.Substring(0, dash));
					var max = int.Parse(s0.Substring(dash + 1));

					r.Add((min, max));
				}

				rules.Add((rule, r.ToArray()));
			}
			while (lines[index].Length > 0);

			return rules.ToArray();
		}

		private int[][] FilterTickets(string[] lines, (string Rule, (int Min, int Max)[] Ranges)[] rules, 
			int index)
		{
			var filtered = new List<int[]>();

			for (int i = index + 5; i < lines.Length; i++)
			{
				var values = lines[i].Split(',').Select(s => int.Parse(s)).ToArray();
				var isTicketValid = true;

				foreach (var v in values)
				{
					var isFieldValid = false;

					foreach (var r in rules)
					{
						foreach (var range in r.Ranges)
						{
							if (v >= range.Min && v <= range.Max)
							{
								isFieldValid = true;

								break;
							}
						}

						if (isFieldValid)
						{
							break;
						}
					}

					if (!isFieldValid)
					{
						isTicketValid = false;

						break;
					}
				}

				if (isTicketValid)
				{
					filtered.Add(values);
				}
			}

			return filtered.ToArray();
		}

		private string[] DetermineFieldOrder((string Rule, (int Min, int Max)[] Ranges)[] rules, int[][] tickets)
		{
			var possible = new List<(int Index, HashSet<int> Set)>(rules.Length);
			
			for (int field = 0; field < rules.Length; field++)
			{
				var set = new HashSet<int>();

				for (int i = 0; i < rules.Length; i++)
				{
					set.Add(i);
				}

				foreach (var ticket in tickets)
				{
					var v = ticket[field];
					var toRemove = new List<int>();

					foreach (var rule in set)
					{
						var isValid = false;

						foreach (var range in rules[rule].Ranges)
						{
							if (v >= range.Min && v <= range.Max)
							{
								isValid = true;

								break;
							}
						}

						if (!isValid)
						{
							toRemove.Add(rule);
						}
					}

					toRemove.ForEach(r => set.Remove(r));
				}

				possible.Add((field, set));
			}

			possible.Sort((s0, s1) => s0.Set.Count.CompareTo(s1.Set.Count));

			var order = new List<(string Rule, int Index)>(rules.Length);

			for (int i = 0; i < possible.Count; i++)
			{
				var entry = possible[i];
				var r = entry.Set.First();
				order.Add((rules[r].Rule, entry.Index));

				for (int j = i + 1; j < possible.Count; j++)
				{
					possible[j].Set.Remove(r);
				}
			}

			order.Sort((r0, r1) => r0.Index.CompareTo(r1.Index));

			return order.Select(r => r.Rule).ToArray();
		}
	}
}
