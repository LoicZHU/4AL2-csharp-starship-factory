namespace core.UI;

public static class StockPrintingHandler
{
	public static void PrintStarshipStock(Dictionary<String, Int32> starshipCounts)
	{
		foreach (var (starshipKey, value) in starshipCounts)
		{
			StockPrinter.PrintCurrentStocks(starshipKey, value);
		}

		Printer.PrintLinebreak();
	}

	public static void PrintComponentStock(List<Dictionary<String, Int32>> counts)
	{
		foreach (var componentCounts in counts)
		{
			foreach (var (componentKey, value) in componentCounts)
			{
				StockPrinter.PrintCurrentStocks(componentKey, value);
			}
		}

		Printer.PrintLinebreak();
	}
}
