namespace core.UI;

public static class StockDisplayHandler
{
	public static void PrintStarshipStock(Dictionary<String, Int32> starshipCounts)
	{
		foreach (var (starshipKey, value) in starshipCounts)
		{
			StockTerminal.PrintCurrentStocks(starshipKey, value);
		}

		Terminal.PrintLinebreak();
	}

	public static void PrintComponentStock(List<Dictionary<String, Int32>> counts)
	{
		foreach (var componentCounts in counts)
		{
			foreach (var (componentKey, value) in componentCounts)
			{
				StockTerminal.PrintCurrentStocks(componentKey, value);
			}
		}

		Terminal.PrintLinebreak();
	}
}
