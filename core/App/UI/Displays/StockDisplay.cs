using core.In_memories.Items;
using core.Utils;

namespace core.UI;

public static class StockDisplay
{
	public static void PrintStarshipStock()
	{
		var inMemoryStarship = InMemoryStarship.Instance;
		var starshipCounts = inMemoryStarship.GetStock();
		if (Utils.UtilsFunction.IsNull(starshipCounts))
		{
			return;
		}

		foreach (var (starshipKey, value) in starshipCounts)
		{
			StockTerminal.PrintCurrentStocks(starshipKey, value);
		}

		TerminalHelper.PrintLineBreak();
	}

	public static void PrintComponentStock()
	{
		var inMemoryComponent = InMemoryComponent.Instance;
		var counts = inMemoryComponent.GetStockOfEachComponent();
		if (Utils.UtilsFunction.IsNull(counts))
		{
			return;
		}

		foreach (var componentCounts in counts)
		{
			foreach (var (componentKey, value) in componentCounts)
			{
				StockTerminal.PrintCurrentStocks(componentKey, value);
			}
		}

		TerminalHelper.PrintLineBreak();
	}
}
