using core.UI.constants;
using static System.ConsoleColor;

namespace core.UI;

public static class Terminal
{
	public static void PrintWelcomeMessage(String message)
	{
		TerminalHelper.PrintLineBreak();
		Console.WriteLine(message);
		TerminalHelper.PrintLineBreak();
	}

	public static void PrintGoodbyeMessage(String message)
	{
		Console.WriteLine(message);
	}

	public static void PrintInvitationToUserInteraction(String message)
	{
		Console.WriteLine(message);
	}

	public static void PrintHelp()
	{
		Console.WriteLine("\nCommandes disponibles :");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.Exit, Magenta);
		Console.WriteLine(" : quitter l'application.");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.Help, Magenta);
		Console.WriteLine(" : afficher les commandes disponibles.");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.Instructions, Magenta);
		Console.WriteLine(" : assembler des vaisseaux.");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.Produce, Magenta);
		Console.WriteLine(" : produire des vaisseaux.");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.Stocks, Magenta);
		Console.WriteLine(" : afficher l'inventaire des vaisseaux.");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.UserInstruction, Magenta);
		Console.WriteLine(" : enregistrer une commande.");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.UserInstructions, Magenta);
		Console.WriteLine(" : lister les commandes en attente.");

		TerminalHelper.ColorizeMessageWithoutLinebreak(Command.Verify, Magenta);
		Console.WriteLine(" : vérifier la disponibilité de vaisseaux.");

		TerminalHelper.PrintLineBreak();
	}

	public static void PrintUnknownInstruction(String instruction)
	{
		Console.WriteLine(instruction);
	}
}
