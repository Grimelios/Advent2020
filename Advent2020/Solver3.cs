using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Solver3 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			var w = lines[0].Length;
			var h = lines.Length;
			var grid = new char[w, h];

			for (int i = 0; i < h; i++)
			{
				for (int j = 0; j < w; j++)
				{
					grid[j, i] = lines[i][j];
				}
			}

			var x = 3;
			var y = 1;
			var trees = 0;

			for (int i = 0; i < h - 1; i++)
			{
				if (grid[x, y] == '#')
				{
					trees++;
				}

				x = (x + 3) % w;
				y++;
			}

			return trees;
		}

		public override object SolvePart2(string[] lines)
		{
			var w = lines[0].Length;
			var h = lines.Length;
			var grid = new char[w, h];

			for (int i = 0; i < h; i++)
			{
				for (int j = 0; j < w; j++)
				{
					grid[j, i] = lines[i][j];
				}
			}

			var trees = new long[5];
			trees[0] = Traverse(grid, w, h, 1, 1);
			trees[1] = Traverse(grid, w, h, 3, 1);
			trees[2] = Traverse(grid, w, h, 5, 1);
			trees[3] = Traverse(grid, w, h, 7, 1);
			trees[4] = Traverse(grid, w, h, 1, 2);

			return trees.Aggregate(1L, (current, t) => current * t);
		}

		private long Traverse(char[,] grid, int w, int h, int stepX, int stepY)
		{
			var x = stepX;
			var y = stepY;
			var trees = 0;

			while (y < h)
			{
				if (grid[x, y] == '#')
				{
					trees++;
				}

				x = (x + stepX) % w;
				y += stepY;
			}

			return trees;
		}
	}
}
