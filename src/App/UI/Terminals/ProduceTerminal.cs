using core.UI.constants;
using static System.ConsoleColor;

namespace core.UI;

public static class ProduceTerminal
{
	public static void PrintError(String message)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak($"{Production.Error}", Red);
		Terminal.PrintMessageWithLinebreak($" {message}");
		Terminal.PrintLinebreak();
	}

	public static void PrintStockUpdated()
	{
		TerminalHelper.ColorizeMessageWithLinebreak(Production.StockUpdated, Yellow);
		Terminal.PrintLinebreak();
	}
}
