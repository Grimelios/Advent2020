using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020.Structures
{
	[DebuggerDisplay("{X}, {Y}")]
	public struct Vec2
	{
		public static float Length(float x, float y)
		{
			return (float)Math.Sqrt(x * x + y * y);
		}

		public static float LengthSquared(float x, float y)
		{
			return x * x + y * y;
		}

		public static Vec2 Direction(float angle, AngleUnits units)
		{
			var radians = units == AngleUnits.Radians ? angle : angle * 3.14159f / 180;
			var x = (float)Math.Cos(radians);
			var y = (float)Math.Sin(radians);

			return new Vec2(x, y);
		}

		public static Vec2 operator +(Vec2 a, Vec2 b) => new Vec2(a.X + b.X, a.Y + b.Y);
		public static Vec2 operator -(Vec2 a, Vec2 b) => new Vec2(a.X - b.X, a.Y - b.Y);
		public static Vec2 operator *(Vec2 a, Vec2 b) => new Vec2(a.X * b.X, a.Y * b.Y);
		public static Vec2 operator *(Vec2 a, float f) => new Vec2(a.X * f, a.Y * f);
		public static Vec2 operator /(Vec2 a, Vec2 b) => new Vec2(a.X / b.X, a.Y / b.Y);
		public static Vec2 operator /(Vec2 a, float f) => new Vec2(a.X / f, a.Y / f);

		public Vec2(float x, float y)
		{
			X = x;
			Y = y;
		}

		public float X { get; set; }
		public float Y { get; set; }

		public float Length()
		{
			return (float)Math.Sqrt(X * X + Y * Y);
		}

		public float LengthSquared()
		{
			return X * X + Y * Y;
		}

		public float Angle()
		{
			return (float)Math.Atan2(Y, X);
		}

		public float Distance(Vec2 v)
		{
			return Length(v.X - X, v.Y - Y);
		}

		public float DistanceSquared(Vec2 v)
		{
			return LengthSquared(v.X - X, v.Y - Y);
		}

		public float ManhattanDistance()
		{
			return Math.Abs(X) + Math.Abs(Y);
		}

		public Vec2 Normalize()
		{
			return new Vec2(X, Y) / Length();
		}
	}
}
