using static System.ConsoleColor;

namespace core.UI;

public static class StockTerminal
{
	public static void PrintCurrentStocks(String key, Int32 value)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak($"{value}", Yellow);
		Terminal.PrintMessageWithLinebreak($" {key}");
	}
}
