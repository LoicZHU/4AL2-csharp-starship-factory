using core.UI.constants;
using static System.ConsoleColor;

namespace core.UI;

public static class MenuTerminal
{
	public static void PrintWelcome(String message)
	{
		Terminal.PrintLinebreak();
		Terminal.PrintMessageWithLinebreak(message);
		Terminal.PrintLinebreak();
	}

	public static void PrintUserInteractionInvitation(String message)
	{
		Terminal.PrintMessageWithLinebreak(message);
	}

	public static void PrintHelp()
	{
		PrintCommandMessage(Command.Exit, " : quitter l'application.");
		PrintCommandMessage(Command.Help, " : afficher les commandes utilisables.");
		PrintCommandMessage($"{Command.Instructions} ARGS", " : assembler des vaisseaux.");
		PrintCommandMessage(
			$"{Command.NeededStocks} ARGS",
			" : afficher les stocks nécessaires pour assembler des vaisseaux."
		);
		PrintCommandMessage($"{Command.Produce} ARGS", " : produire des vaisseaux.");
		PrintCommandMessage(
			$"{Command.Stocks} ARGS",
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
		Terminal.PrintMessageWithLinebreak(message);
	}
}
