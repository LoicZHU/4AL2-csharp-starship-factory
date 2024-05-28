using core.UI.constants;
using static System.ConsoleColor;

namespace core.UI;

public class UserInterface
{
	public void PrintWelcomeMessage()
	{
		Console.WriteLine("Bienvenue chez Capsule Corp ! 🚀\n");
	}

	public void PrintInvitationToUserInteraction()
	{
		Console.WriteLine($"🕹 Entrez une instruction ({Command.Help} pour de l'aide) :");
	}

	public void PrintHelp()
	{
		Console.WriteLine("\nCommandes disponibles :");

		this.ColorizeMessageWithoutLinebreak(Command.Exit, Magenta);
		Console.WriteLine(" : quitter l'application.");

		this.ColorizeMessageWithoutLinebreak(Command.Help, Magenta);
		Console.WriteLine(" : afficher les commandes disponibles.");

		this.ColorizeMessageWithoutLinebreak(Command.Instructions, Magenta);
		Console.WriteLine(" : assembler des vaisseaux.");

		this.ColorizeMessageWithoutLinebreak(Command.Produce, Magenta);
		Console.WriteLine(" : produire des vaisseaux.");

		this.ColorizeMessageWithoutLinebreak(Command.Stocks, Magenta);
		Console.WriteLine(" : afficher l'inventaire des vaisseaux.");

		this.ColorizeMessageWithoutLinebreak(Command.UserInstruction, Magenta);
		Console.WriteLine(" : enregistrer une commande.");

		this.ColorizeMessageWithoutLinebreak(Command.UserInstructions, Magenta);
		Console.WriteLine(" : lister les commandes en attente.");

		this.ColorizeMessageWithoutLinebreak(Command.Verify, Magenta);
		Console.WriteLine(" : vérifier la disponibilité de vaisseaux.");

		this.PrintLineBreak();
	}

	private void ColorizeMessageWithLinebreak(String message, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Console.WriteLine(message);
		Console.ResetColor();
	}

	private void ColorizeMessageWithoutLinebreak(String message, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Console.Write(message);
		Console.ResetColor();
	}

	public void PrintLineBreak()
	{
		Console.Write("\n");
	}
}
