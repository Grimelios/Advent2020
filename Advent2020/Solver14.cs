using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Utility;

namespace Advent2020
{
	public class Solver14 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			var memory = new Dictionary<ulong, ulong>();
			var zeroMask = ulong.MaxValue;
			var oneMask = 0UL;

			foreach (var s in lines)
			{
				var value = s.Split('=')[1].Substring(1);

				if (s[1] == 'a')
				{
					zeroMask = ulong.MaxValue;
					oneMask = 0UL;

					for (int i = 0; i < value.Length; i++)
					{
						switch (value[i])
						{
							case '0': zeroMask = zeroMask.SetBit(35 - i, 0); break;
							case '1': oneMask = oneMask.SetBit(35 - i, 1); break;
						}
					}
				}
				else
				{
					var address = ulong.Parse(s.Substring(4, s.IndexOf(']') - 4));
					var v = ulong.Parse(value);
					v &= zeroMask;
					v |= oneMask;

					memory[address] = v;
				}
			}

			var sum = 0UL;

			foreach (var v in memory.Values)
			{
				sum += v;
			}

			return sum;
		}

		public override object SolvePart2(string[] lines)
		{
			var memory = new Dictionary<ulong, ulong>();
			var oneMask = 0UL;
			var floatingBits = new List<int>();

			foreach (var s in lines)
			{
				var value = s.Split('=')[1].Substring(1);

				if (s[1] == 'a')
				{
					oneMask = 0UL;
					floatingBits.Clear();

					for (int i = 0; i < value.Length; i++)
					{
						switch (value[i])
						{
							case '1': oneMask = oneMask.SetBit(35 - i, 1); break;
							case 'X': floatingBits.Add(35 - i); break;
						}
					}
				}
				else
				{
					var v = ulong.Parse(value);
					var address = ulong.Parse(s.Substring(4, s.IndexOf(']') - 4));
					address |= oneMask;

					void Recurse(int index, ulong a)
					{
						if (index == floatingBits.Count)
						{
							memory[a] = v;
						}
						else
						{
							Recurse(index + 1, a.SetBit(floatingBits[index], 0));
							Recurse(index + 1, a.SetBit(floatingBits[index], 1));
						}
					}

					Recurse(0, address);
				}
			}

			var sum = 0UL;

			foreach (var v in memory.Values)
			{
				sum += v;
			}

			return sum;
		}
	}
}
