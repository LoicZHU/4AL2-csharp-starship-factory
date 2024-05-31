using core.UI.constants;
using static System.ConsoleColor;

namespace core.UI;

public static class MainTerminal
{
	public static void PrintWelcomeMessage(String message)
	{
		TerminalHelper.PrintLineBreak();
		Console.WriteLine(message);
		TerminalHelper.PrintLineBreak();
	}

	public static void PrintHelp()
	{
		Console.WriteLine("\nCommandes disponibles :");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.Exit, Magenta);
		Console.WriteLine(" : quitter l'application.");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.Help, Magenta);
		Console.WriteLine(" : afficher les commandes utilisables.");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.Instructions, Magenta);
		Console.WriteLine(" : assembler des vaisseaux.");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.Produce, Magenta);
		Console.WriteLine(" : produire des vaisseaux.");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.Stocks, Magenta);
		Console.WriteLine(" : afficher l'inventaire.");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.UserInstruction, Magenta);
		Console.WriteLine(" : enregistrer une commande de vaisseaux.");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.UserInstructions, Magenta);
		Console.WriteLine(" : lister les commandes de vaisseaux en attente.");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.Verify, Magenta);
		Console.WriteLine(" : vérifier la disponibilité de vaisseaux.");

		TerminalHelper.PrintLineBreak();
	}

	public static void PrintMessage(String message)
	{
		Console.WriteLine(message);
	}
}
