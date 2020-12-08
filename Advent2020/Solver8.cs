using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Solver8 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			var accumulator = 0;
			var instruction = 0;
			var executed = new bool[lines.Length];

			while (true)
			{
				if (executed[instruction])
				{
					break;
				}
				
				executed[instruction] = true;

				var tokens = lines[instruction].Split(' ');
				var value = int.Parse(tokens[1]);

				switch (tokens[0])
				{
					case "acc":
						accumulator += value;
						instruction++;

						break;

					case "jmp":
						instruction += value;

						break;

					case "nop":
						instruction++;

						break;
				}
			}

			return accumulator;
		}

		public override object SolvePart2(string[] lines)
		{
			var instructions = new (string Arg, int Value)[lines.Length];

			for (int i = 0; i < lines.Length; i++)
			{
				var tokens = lines[i].Split(' ');

				instructions[i] = (tokens[0], int.Parse(tokens[1]));
			}

			var result = 0;

			for (int i = 0; i < instructions.Length; i++)
			{
				var entry = instructions[i];

				switch (entry.Arg)
				{
					case "nop":
					{
						if (entry.Value == 0)
						{
							continue;
						}

						instructions[i] = ("jmp", entry.Value);
						
						break;
					}

					case "jmp": instructions[i] = ("nop", entry.Value); break;
					case "acc": continue;
				}

				result = Run(instructions);

				if (result >= 0)
				{
					break;
				}

				switch (entry.Arg)
				{
					case "jmp": instructions[i] = ("jmp", entry.Value); break;
					case "nop": instructions[i] = ("nop", entry.Value); break;
				}
			}

			return result;
		}

		private int Run((string Arg, int Value)[] instructions)
		{
			var accumulator = 0;
			var instruction = 0;
			var executed = new bool[instructions.Length];

			while (instruction < instructions.Length)
			{
				if (executed[instruction])
				{
					return -1;
				}
				
				executed[instruction] = true;

				var entry = instructions[instruction];

				switch (entry.Arg)
				{
					case "acc":
						accumulator += entry.Value;
						instruction++;

						break;

					case "jmp":
						instruction += entry.Value;

						break;

					case "nop":
						instruction++;

						break;
				}
			}

			return accumulator;
		}
	}
}