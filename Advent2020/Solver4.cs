using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Solver4 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			var valid = 0;
			var fields = new HashSet<string>();
			var required = new []
			{
				"byr",
				"iyr",
				"eyr",
				"hgt",
				"hcl",
				"ecl",
				"pid"
			};

			bool IsValid()
			{
				return required.All(r => fields.Contains(r));
			}

			foreach (var s in lines)
			{
				if (s == "")
				{
					if (IsValid())
					{
						valid++;
					}

					fields.Clear();

					continue;
				}

				var tokens = s.Split(' ');

				foreach (var t in tokens)
				{
					fields.Add(t.Substring(0, 3));
				}
			}

			if (IsValid())
			{
				valid++;
			}

			return valid;
		}

		public override object SolvePart2(string[] lines)
		{
			var valid = 0;
			var fields = new Dictionary<string, string>();
			var required = new []
			{
				"byr",
				"iyr",
				"eyr",
				"hgt",
				"hcl",
				"ecl",
				"pid"
			};

			var eyes = new HashSet<string>
			{
				"amb", "blu", "brn", "gry", "grn", "hzl", "oth"
			};

			var validators = new Func<string, bool>[]
			{
				s => s.Length == 4 && int.TryParse(s, out var year) && year >= 1920 && year <= 2002,
				s => s.Length == 4 && int.TryParse(s, out var year) && year >= 2010 && year <= 2020,
				s => s.Length == 4 && int.TryParse(s, out var year) && year >= 2020 && year <= 2030,
				s =>
				{
					var isCm = s.EndsWith("cm");
					var isIn = s.EndsWith("in");

					if (!isCm && !isIn)
					{
						return false;
					}

					var i = int.Parse(s.Substring(0, s.Length - 2));

					return isCm
						? i >= 150 && i <= 193
						: i >= 59 && i <= 76;
				},

				s => s.Length == 7 && s[0] == '#' && s.Substring(1).All(c => c - '0' <= 9 || c - 'a' <= 6),
				s => eyes.Contains(s),
				s => s.Length == 9 && int.TryParse(s, out _),
			};

			bool IsValid()
			{
				for (int i = 0; i < required.Length; i++)
				{
					var r = required[i];

					if (!fields.TryGetValue(r, out var value) || !validators[i](value))
					{
						return false;
					}
				}

				return true;
			}

			foreach (var s in lines)
			{
				if (s == "")
				{
					if (IsValid())
					{
						valid++;
					}

					fields.Clear();

					continue;
				}

				var tokens = s.Split(' ');

				foreach (var t in tokens)
				{
					var sub = t.Split(':');

					fields.Add(sub[0], sub[1]);
				}
			}

			if (IsValid())
			{
				valid++;
			}

			return valid;
		}
	}
}
