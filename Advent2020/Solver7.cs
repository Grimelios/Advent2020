using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2020
{
	public class Solver7 : Solver
	{
		public override object SolvePart1(string[] lines)
		{
			var map = new Dictionary<string, Node>();
			var list = new List<Node>();

			foreach (var s in lines)
			{
				var index = s.IndexOf("bags", StringComparison.CurrentCulture) - 1;
				var color = s.Substring(0, index);

				if (!map.TryGetValue(color, out var parent))
				{
					parent = new Node(color);
					map.Add(color, parent);
					list.Add(parent);
				}

				index += 14;

				var tokens = s.Substring(index).Split(',');

				foreach (var bag in tokens)
				{
					var b = bag.TrimStart().Substring(bag.IndexOf(' ') + 1);
					b = b.Substring(0, b.LastIndexOf(' ')).TrimStart();

					if (!map.ContainsKey(b))
					{
						var n = new Node(b);
						map.Add(b, n);
						list.Add(n);
					}

					if (!parent.Edges.Contains(b))
					{
						parent.Edges.Add(b);
					}
				}
			}

			var count = 0;

			int Traverse(Node node)
			{
				node.IsVisited = true;

				var queue = new Queue<Node>();

				foreach (var e in node.Edges)
				{
					queue.Enqueue(map[e]);
				}

				while (queue.Count > 0)
				{
					var n = queue.Dequeue();

					if (n.Color == "shiny gold")
					{
						return 1;
					}

					foreach (var e in n.Edges)
					{
						if (!map[e].IsVisited)
						{
							queue.Enqueue(map[e]);
						}
					}
				}

				return 0;
			}

			foreach (var n in list)
			{
				count += Traverse(n);

				foreach (var n0 in list)
				{
					n0.IsVisited = false;
				}
			}

			return count;
		}

		public override object SolvePart2(string[] lines)
		{
			var map = new Dictionary<string, Node2>();

			foreach (var s in lines)
			{
				var index = s.IndexOf("bags", StringComparison.CurrentCulture) - 1;
				var color = s.Substring(0, index);

				if (!map.TryGetValue(color, out var parent))
				{
					parent = new Node2(color);
					map.Add(color, parent);
				}

				index += 14;

				var tokens = s.Substring(index).Split(',');

				foreach (var bag in tokens)
				{
					if (bag.StartsWith("no other"))
					{
						continue;
					}

					var b = bag.TrimStart();
					var i = b.IndexOf(' ');
					var required = int.Parse(b.Substring(0, i));

					b = b.Substring(i + 1);
					b = b.Substring(0, b.LastIndexOf(' ')).TrimStart();

					if (!map.ContainsKey(b))
					{
						var n = new Node2(b);
						map.Add(b, n);
					}

					if (parent.Edges.All(e => e.Color != b))
					{
						parent.Edges.Add((b, required));
					}
				}
			}

			int Traverse(Node2 n)
			{
				var count = 1;

				foreach (var e in n.Edges)
				{
					count += e.Required * Traverse(map[e.Color]);
				}

				return count;
			}

			var total = 0;

			foreach (var e in map["shiny gold"].Edges)
			{
				total += Traverse(map[e.Color]) * e.Required;
			}

			return total;
		}

		[DebuggerDisplay("{Color}")]
		private class Node
		{
			public Node(string color)
			{
				Color = color;
				Edges = new List<string>();
			}

			public string Color { get; }
			public List<string> Edges { get; }
			public bool IsVisited { get; set; }
		}

		[DebuggerDisplay("{Color}, {Edges.Count} edges")]
		private class Node2
		{
			public Node2(string color)
			{
				Color = color;
				Edges = new List<(string Color, int Required)>();
			}

			public string Color { get; }
			public List<(string Color, int Required)> Edges { get; }
		}
	}
}
