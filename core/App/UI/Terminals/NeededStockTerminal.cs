using static System.ConsoleColor;

namespace core.UI;

public static class NeededStockTerminal
{
	public static void PrintStarshipQuantity(String starshipName, Int32 quantity)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak($"{quantity}", Yellow);
		MainTerminal.PrintMessage($" {starshipName} :");
	}

	public static void PrintItem(String component, Int32 quantity)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak($"{quantity}", Yellow);
		MainTerminal.PrintMessage($" {component}");
	}
}
