using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Solver11 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			return Solve(lines, ModifySeatAdjacent);
		}

		public override object SolvePart2(string[] lines)
		{
			return Solve(lines, ModifySeatRaycast);
		}

		private int Solve(string[] lines, Func<char, char[,], int, int, char> modify)
		{
			var w = lines[0].Length;
			var h = lines.Length;
			var grid = new char[w + 2, h + 2];

			for (int i = 0; i < h + 2; i++)
			{
				for (int j = 0; j < w + 2; j++)
				{
					grid[j, i] = '.';
				}
			}

			for (int i = 1; i <= h; i++)
			{
				var s = lines[i - 1];

				for (int j = 1; j <= w; j++)
				{
					grid[j, i] = s[j - 1];
				}
			}

			while (true)
			{
				var @new = Iterate(grid, w, h, modify);
				var same = true;

				for (int i = 1; i <= h; i++)
				{
					for (int j = 1; j <= w; j++)
					{
						if (grid[j, i] != @new[j, i])
						{
							same = false;

							break;
						}
					}

					if (!same)
					{
						break;
					}
				}
				
				grid = @new;

				if (same)
				{
					break;
				}
			}

			var occupied = 0;

			for (int i = 1; i <= h; i++)
			{
				for (int j = 1; j <= w; j++)
				{
					if (grid[j, i] == '#')
					{
						occupied++;
					}
				}
			}

			return occupied;
		}

		private char[,] Iterate(char[,] grid, int w, int h, Func<char, char[,], int, int, char> modify)
		{
			var @new = new char[w + 2, h + 2];

			for (int i = 0; i < h + 2; i++)
			{
				for (int j = 0; j < w + 2; j++)
				{
					@new[j, i] = '.';
				}
			}

			for (int i = 1; i <= h; i++)
			{
				for (int j = 1; j <= w; j++)
				{
					if (grid[j, i] == '.')
					{
						continue;
					}

					@new[j, i] = modify(grid[j, i], grid, i, j);
				}
			}

			return @new;
		}

		private char ModifySeatAdjacent(char seat, char[,] grid, int i, int j)
		{
			var surrounding = new []
			{
				grid[j - 1, i - 1],
				grid[j, i - 1],
				grid[j + 1, i - 1],
				grid[j - 1, i],
				grid[j + 1, i],
				grid[j - 1, i + 1],
				grid[j, i + 1],
				grid[j + 1, i + 1]
			};

			switch (seat)
			{
				case 'L':
				{
					if (surrounding.All(s => s != '#'))
					{
						return '#';
					}

					break;
				}

				case '#':
				{
					if (surrounding.Count(s => s == '#') >= 4)
					{
						return 'L';
					}

					break;
				}
			}

			return seat;
		}

		private char ModifySeatRaycast(char seat, char[,] grid, int i, int j)
		{
			var directions = new []
			{
				(-1, -1),
				(0, -1),
				(1, -1),
				(-1, 0),
				(1, 0),
				(-1, 1),
				(0, 1),
				(1, 1)
			};

			var count = 0;
			var target = grid[j, i] == '#' ? 5 : 1;

			for (int d = 0; d < 8; d++)
			{
				var (dX, dY) = directions[d];

				count += Raycast(grid, i, j, dX, dY);

				if (count == target)
				{
					break;
				}
			}

			if (grid[j, i] == 'L')
			{
				return count == 0 ? '#' : 'L';
			}

			return count == 5 ? 'L' : seat;
		}

		private int Raycast(char[,] grid, int i, int j, int dX, int dY)
		{
			var w = grid.GetLength(0);
			var h = grid.GetLength(1);
			var x = j;
			var y = i;

			do
			{
				x += dX;
				y += dY;

				if (grid[x, y] != '.')
				{
					return grid[x, y] == '#' ? 1 : 0;
				}
			}
			while (y > 0 && y < h - 1 && x > 0 && x < w - 1);

			return 0;
		}
	}
}
