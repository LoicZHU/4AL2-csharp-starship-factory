using core.UI.constants;
using static System.ConsoleColor;

namespace core.UI;

public static class ProduceTerminal
{
	public static void PrintStockUpdated()
	{
		TerminalHelper.ColorizeMessageWithLinebreak(Production.StockUpdated, Yellow);
		Terminal.PrintLinebreak();
	}
}
