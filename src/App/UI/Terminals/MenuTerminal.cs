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
		PrintCommandMessage(Command.Instructions, " : assembler des vaisseaux.");
		PrintCommandMessage(
			Command.NeededStocks,
			" : afficher les stocks nécessaires pour assembler des vaisseaux."
		);
		PrintCommandMessage(Command.Produce, " : produire des vaisseaux.");
		PrintCommandMessage(
			Command.Stocks,
			" : afficher les stocks de vaisseaux et de composants."
		);
		PrintCommandMessage(
			Command.UserInstruction,
			" : enregistrer une commande de vaisseaux."
		);
		PrintCommandMessage(
			Command.UserInstructions,
			" : lister les commandes de vaisseaux en attente."
		);
		PrintCommandMessage(Command.Verify, " : vérifier la disponibilité de vaisseaux.");
	}

	private static void PrintCommandMessage(String command, String message)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(command, Magenta);
		Terminal.PrintMessageWithLinebreak(message);
	}
}
