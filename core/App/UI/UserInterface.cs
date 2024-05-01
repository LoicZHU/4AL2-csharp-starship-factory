using core.App.Products.Starship;
using core.App.UI.constants;
using static System.ConsoleColor;

namespace core.App.UI;

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

	public void PrintUnknownCommand()
	{
		Console.WriteLine("❌ Commande inconnue. Veuillez réessayer.\n");
	}

	public void PrintInvalidInstructionCommand()
	{
		Console.WriteLine(
			"❌ La commande doit respecter ce format : INSTRUCTIONS <quantité> <nom_de_l'élément> [<quantité> <nom_de_l'élément> ...]\n"
		);
	}

	public void PrintInvalidUserInstructionCommand()
	{
		Console.WriteLine(
			"❌ La commande doit respecter ce format : [USER_INSTRUCTION] <quantité> <nom_du_vaisseau> [, <quantité> <nom_du_vaisseau>, ...]\n"
		);
	}

	#region Instructions
	public void PrintStarshipProductionStarting(String starshipModel)
	{
		this.ColorizeMessageWithoutLinebreak(Instruction.Producing, Green);
		Console.WriteLine($" {starshipModel}");
	}

	public void PrintGetOutStockMessage(Int32 quantity, String componentModel)
	{
		this.ColorizeMessageWithoutLinebreak(Instruction.GetOutStock, Green);
		this.ColorizeMessageWithoutLinebreak($" {quantity}", Yellow);
		Console.WriteLine($" {componentModel}");
	}

	public void PrintAssemblingComponentsMessage(
		ComponentAssembly componentAssembly,
		String componentToAdd
	)
	{
		this.ColorizeMessageWithoutLinebreak(Instruction.Assemble, Green);
		Console.WriteLine(
			$" [{string.Join(", ", componentAssembly.Components)}] {componentToAdd}"
		);
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

	public void PrintStarshipProductionFinishing(String starshipModel)
	{
		this.ColorizeMessageWithoutLinebreak(Instruction.Finished, Green);
		Console.WriteLine($" {starshipModel}\n");
	}
	#endregion

	public void PrintUnknownStarshipModel()
	{
		Console.WriteLine("❌ Modèle de vaisseau inconnu...\n");
	}

	public void PrintLineBreak()
	{
		Console.Write("\n");
	}

	public void PrintInvalidStarshipInputArgument(String argument)
	{
		this.ColorizeMessageWithoutLinebreak(Verification.Error, Red);
		Console.WriteLine($" `{argument}` is not a recognized spaceship\n");
	}

	public void PrintAvailableMessage()
	{
		this.ColorizeMessageWithLinebreak(Verification.Available, Yellow);
	}

	public void PrintUnavailableMessage()
	{
		this.ColorizeMessageWithLinebreak(Verification.Unavailable, Yellow);
	}

	public void PrintInvalidCommandArguments()
	{
		Console.WriteLine(
			"❌ Les arguments de la commande sont invalides. Veuillez réessayer.\n"
		);
	}

	public void PrintInvalidCommand()
	{
		Console.WriteLine("❌ Commande invalide. Veuillez réessayer.\n");
	}

	public void PrintStockUpdatedMessage()
	{
		this.ColorizeMessageWithLinebreak(Production.StockUpdated, Yellow);
	}
}
