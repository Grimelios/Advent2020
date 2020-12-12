using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Structures;
using Advent2020.Utility;

namespace Advent2020
{
	public class Solver12 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			var angle = 0;
			var ship = new Vec2(0, 0);

			foreach (var s in lines)
			{
				var action = s[0];
				var value = int.Parse(s.Substring(1));

				switch (action)
				{
					case 'N': ship.Y += value; break;
					case 'S': ship.Y -= value; break;
					case 'E': ship.X += value; break;
					case 'W': ship.X -= value; break;
					case 'L': angle += value; break;
					case 'R': angle -= value; break;
					case 'F':
					{
						ship += Vec2.Direction(angle, AngleUnits.Degrees) * value;

						break;
					}
				}
			}

			return (int)Math.Round(ship.ManhattanDistance());
		}

		public override object SolvePart2(string[] lines)
		{
			var waypoint = new Vec2(10, 1);
			var ship = new Vec2(0, 0);

			foreach (var s in lines)
			{
				var action = s[0];
				var value = int.Parse(s.Substring(1));

				switch (action)
				{
					case 'N': waypoint.Y += value; break;
					case 'S': waypoint.Y -= value; break;
					case 'E': waypoint.X += value; break;
					case 'W': waypoint.X -= value; break;
					case 'L':
					{
						var angle = waypoint.Angle() + Utilities.ToRadians(value);
						var length = waypoint.Length();

						waypoint = Vec2.Direction(angle, AngleUnits.Radians) * length;
						
						break;
					}

					case 'R':
					{
						var angle = waypoint.Angle() - Utilities.ToRadians(value);
						var length = waypoint.Length();

						waypoint = Vec2.Direction(angle, AngleUnits.Radians) * length;
						
						break;
					}

					case 'F':
					{
						ship += waypoint * value;

						break;
					}
				}
			}

			return (int)Math.Round(ship.ManhattanDistance());
		}

		private void ComputeDirection(float angle, out float x, out float y)
		{
			var radians = angle * 3.14159f / 180;

			x = (float)Math.Cos(radians);
			y = (float)Math.Sin(radians);
		}
	}
}
