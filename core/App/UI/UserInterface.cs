using core.Products.Starship.ComponentAssembly;

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
		Console.WriteLine($"👉 {Command.Stocks} : afficher l'inventaire des vaisseaux");
		Console.WriteLine($"👉 {Command.Help} : afficher les commandes disponibles");
		Console.WriteLine($"👉 {Command.Exit} : quitter l'application");
		this.PrintLineBreak();
	}

	public void PrintUnknownCommand()
	{
		Console.WriteLine("❌ Commande inconnue. Veuillez réessayer.");
		this.PrintLineBreak();
	}

	public void PrintInvalidInstructionCommand()
	{
		Console.WriteLine(
			"❌ La commande doit respecter ce format : INSTRUCTIONS <quantité> <nom_de_l'élément> [<quantité> <nom_de_l'élément> ...]"
		);
		this.PrintLineBreak();
	}

	#region Instructions
	public void PrintStarshipProductionStarting(String starshipModel)
	{
		Console.WriteLine($"{Instruction.Producing} {starshipModel}");
	}

	public void PrintGetOutStockMessage(Int32 quantity, String componentModel)
	{
		Console.WriteLine($"{Instruction.GetOutStock} {quantity} {componentModel}");
	}

	public void PrintAssemblingComponentsMessage(
		ComponentAssembly componentAssembly,
		String componentToAdd
	)
	{
		Console.WriteLine(
			$"{Instruction.Assemble} [{string.Join(", ", componentAssembly.Components)}] {componentToAdd}"
		);
	}

	public void PrintStarshipProductionFinishing(String starshipModel)
	{
		Console.WriteLine($"{Instruction.Finished} {starshipModel}");
		this.PrintLineBreak();
	}
	#endregion

	private void PrintLineBreak()
	{
		Console.WriteLine("");
	}
}
