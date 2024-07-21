using core.UI.constants;
using static System.ConsoleColor;

namespace core.UI;

public static class ProducePrinter
{
	public static void PrintError(String message)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak($"{Production.Error}", Red);
		Printer.PrintMessageWithLinebreak($" {message}");
		Printer.PrintLinebreak();
	}

	public static void PrintStockUpdated()
	{
		TerminalHelper.ColorizeMessageWithLinebreak(Production.StockUpdated, Yellow);
		Printer.PrintLinebreak();
	}
}
