using core.Assemblies;

namespace core.UI;

public static class NeededStocksDisplayHandler
{
	public static void PrintInvalidCommand(String message)
	{
		Terminal.PrintInvalidCommand(message);
	}

	public static void PrintNeededStocks(Dictionary<String, Int32> starshipCounts)
	{
		var totalComponents = GetTotalComponents(starshipCounts);

		PrintNeededStarshipsAndComponents(starshipCounts);
		PrintTotalComponents(totalComponents);
		Terminal.PrintLinebreak();
	}

	private static Dictionary<String, Int32> GetTotalComponents(
		Dictionary<String, Int32> starshipCounts
	)
	{
		var components = new Dictionary<String, Int32>();

		foreach (var (starshipName, starshipCount) in starshipCounts)
		{
			foreach (var (component, count) in StarshipAssembly.Components[starshipName])
			{
				Int32 componentQuantity = starshipCount * count;
				components.Add(component, componentQuantity);
			}
		}

		return components;
	}

	private static void PrintNeededStarshipsAndComponents(Dictionary<String, Int32> counts)
	{
		foreach (var (starshipName, starshipCount) in counts)
		{
			NeededStockTerminal.PrintStarshipQuantity(starshipName, starshipCount);

			foreach (var (component, count) in StarshipAssembly.Components[starshipName])
			{
				Int32 componentQuantity = starshipCount * count;
				NeededStockTerminal.PrintItem(component, componentQuantity);
			}
		}
	}

	private static void PrintTotalComponents(Dictionary<string, int> totalComponents)
	{
		Terminal.PrintMessageWithLinebreak("Total :");

		foreach (var (component, count) in totalComponents)
		{
			NeededStockTerminal.PrintItem(component, count);
		}
	}
}
