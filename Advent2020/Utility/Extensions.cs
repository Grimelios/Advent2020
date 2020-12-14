using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020.Utility
{
	public static class Extensions
	{
		public static ulong SetBit(this ulong u, int position, int bit)
		{
			var mask = 1UL << position;

			return (u & ~mask) | (((ulong)bit << position) & mask);
		}
	}
}
