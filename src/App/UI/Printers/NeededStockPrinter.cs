using static System.ConsoleColor;

namespace core.UI;

public static class NeededStockPrinter
{
	public static void PrintStarshipQuantity(String starshipName, Int32 quantity)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak($"{quantity}", Yellow);
		Printer.PrintMessageWithLinebreak($" {starshipName} :");
	}

	public static void PrintItem(String component, Int32 quantity)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak($"{quantity}", Yellow);
		Printer.PrintMessageWithLinebreak($" {component}");
	}
}
