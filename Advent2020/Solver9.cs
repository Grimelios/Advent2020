using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Solver9 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			var array = lines.Select(s => long.Parse(s)).ToArray();

			for (int i = 25; i < array.Length; i++)
			{
				var n = array[i];
				var isValid = false;

				for (int j = i - 25; j <= i - 1; j++)
				{
					for (int k = j + 1; k <= i; k++)
					{
						if (array[j] + array[k] == n)
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
					return n;
				}
			}

			return null;
		}

		public override object SolvePart2(string[] lines)
		{
			var array = lines.Select(s => long.Parse(s)).ToArray();
			var n = FindInvalidEntry(array);

			for (int i = 0; i < array.Length; i++)
			{
				var sum = array[i];

				for (int j = i + 1; j < array.Length; j++)
				{
					sum += array[j];

					if (sum == n)
					{
						var list = new List<long>();

						for (int k = i; k <= j; k++)
						{
							list.Add(array[k]);
						}

						var min = list.Min();
						var max = list.Max();

						return min + max;
					}
					
					if (sum > n)
					{
						break;
					}
				}
			}

			return null;
		}

		private long FindInvalidEntry(long[] array)
		{
			for (int i = 25; i < array.Length; i++)
			{
				var n = array[i];
				var isValid = false;

				for (int j = i - 25; j <= i - 1; j++)
				{
					for (int k = j + 1; k <= i; k++)
					{
						if (array[j] + array[k] == n)
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
					return n;
				}
			}

			return 0;
		}
	}
}
