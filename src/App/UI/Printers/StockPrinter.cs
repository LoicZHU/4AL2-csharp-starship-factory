using static System.ConsoleColor;

namespace core.UI;

public static class StockPrinter
{
	public static void PrintCurrentStocks(String key, Int32 value)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak($"{value}", Yellow);
		Printer.PrintMessageWithLinebreak($" {key}");
	}
}
