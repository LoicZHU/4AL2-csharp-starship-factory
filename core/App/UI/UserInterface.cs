using core.App.Products.Starship;
using core.App.UI.constants;

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
		Console.WriteLine($"👉 {Command.Help} : afficher les commandes disponibles");
		Console.WriteLine($"👉 {Command.Instructions} : assembler des vaisseaux");
		Console.WriteLine($"👉 {Command.Stocks} : afficher l'inventaire des vaisseaux");
		Console.WriteLine($"👉 {Command.UserInstruction} : assembler des ");
		Console.WriteLine($"👉 {Command.Exit} : quitter l'application");
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
		Console.WriteLine($"{Instruction.Finished} {starshipModel}\n");
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
}
