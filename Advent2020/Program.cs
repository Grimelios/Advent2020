using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var shouldExit = false;

			while (!shouldExit)
			{
				Console.Write("Choose solver: ");

				var s = Console.ReadLine();

				if (s == "exit" || s == "quit")
				{
					shouldExit = true;
				}
				else if (int.TryParse(s, out var i))
				{
					if (i < 1 || i > 25)
					{
						Console.WriteLine("Invalid");
					}
					else
					{

						var type = Type.GetType($"Advent2020.Solver{i}");
					
						if (type == null)
						{
							Console.WriteLine($"Missing solver class Solver{i}");
						}
						else
						{
							var inputFile = $"Input/Input{i}.txt";
							var lines = (string[])null;

							if (!File.Exists(inputFile))
							{
								Console.WriteLine($"Missing input file \"{inputFile}\"");
							}
							else if ((lines = File.ReadAllLines(inputFile)).Length == 0)
							{
								Console.WriteLine($"Input file \"{inputFile}\" is empty");
							}
							else
							{
								var solver = (Solver)Activator.CreateInstance(type);
								var solution = (object)null;
						
								Console.Write("Part 1: ");

								try
								{
									solution = solver.SolvePart1(lines);

									Console.WriteLine($"{solution ?? "Null"}");
								}
								catch (NotImplementedException e)
								{
									Console.WriteLine("Not implemented");
								}
								
								Console.Write("Part 2: ");

								try
								{
									solution = solver.SolvePart2(lines);
									
									Console.WriteLine($"{solution ?? "Null"}");
								}
								catch (NotImplementedException e)
								{
									Console.WriteLine("Not implemented");
								}
							}
						}
					}
				}
				else
				{
					Console.WriteLine("Invalid");
				}
			}
		}
	}
}
