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
	}

	public void PrintUnknownCommand()
	{
		Console.WriteLine("❌ Commande inconnue. Veuillez réessayer.");
	}

	public void PrintInvalidInstructionCommand()
	{
		Console.WriteLine(
			"❌ La commande doit respecter ce format : INSTRUCTIONS <quantité> <nom_de_l'élément> [<quantité> <nom_de_l'élément> ...]"
		);
	}
}
