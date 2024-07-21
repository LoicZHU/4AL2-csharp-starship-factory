using core.UI.constants;
using static System.ConsoleColor;

namespace core.UI;

public static class MenuPrinter
{
	public static void PrintWelcome(String message)
	{
		Printer.PrintMessageWithoutLinebreak(message);
	}

	public static void PrintUserInteractionInvitation(String message)
	{
		Printer.PrintMessageWithoutLinebreak(message);
	}

	public static void PrintHelp()
	{
		PrintCommandMessage(Command.Exit, " : quitter l'application.");
		PrintCommandMessage(Command.Help, " : afficher les commandes utilisables.");
		PrintCommandMessage($"{Command.Instructions} ARGS", " : assembler des vaisseaux.");
		PrintCommandMessage(Command.ListOrder, " : afficher les commandes en attente.");
		PrintCommandMessage(
			$"{Command.NeededStocks} ARGS",
			" : afficher les stocks nécessaires pour assembler des vaisseaux."
		);
		PrintCommandMessage($"{Command.Order} ARGS", " : passer une commande.");
		PrintCommandMessage($"{Command.Produce} ARGS", " : produire des vaisseaux.");
		PrintCommandMessage(
			Command.Stocks,
			" : afficher les stocks de vaisseaux et de composants."
		);
		PrintCommandMessage(
			$"{Command.Verify} ARGS",
			" : vérifier la disponibilité de vaisseaux."
		);
	}

	private static void PrintCommandMessage(String command, String message)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(command, Magenta);
		Printer.PrintMessageWithLinebreak(message);
	}

	public static void PrintAvailableCommandsMessage(String message)
	{
		Printer.PrintMessageWithoutLinebreak(message);
	}

	public static void PrintEmptyInstructionMessage(String message)
	{
		Printer.PrintMessageWithoutLinebreak(message);
	}
}
