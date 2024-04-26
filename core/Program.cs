using core.Inventory;

namespace core;

static class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");

		// TODO: refactor this draft
		var input = Console.ReadLine();
		if (input.Equals("STOCKS", StringComparison.OrdinalIgnoreCase))
		{
			var starships = new InMemoryStarship();
		}
	}
}
