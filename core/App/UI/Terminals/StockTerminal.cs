namespace core.UI;

public static class StockTerminal
{
	public static void PrintCurrentStocks(String key, Int32 value)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak($"{value}", ConsoleColor.Yellow);
		Console.WriteLine($" {key}");
	}
}
