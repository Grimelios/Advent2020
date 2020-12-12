using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020.Utility
{
	public static class Utilities
	{
		public static float ToDegrees(float radians)
		{
			return radians * 180 / 3.14159f;
		}

		public static float ToRadians(float degrees)
		{
			return degrees * 3.14159f / 180;
		}
	}
}
