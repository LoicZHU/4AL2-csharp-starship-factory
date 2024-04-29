namespace core;

public class UserInterface
{
	public void PrintWelcomeMessage()
	{
		Console.WriteLine("Bienvenue chez Capsule Corp ! 🚀\n");
	}

	public string? GetUserInput()
	{
		Console.WriteLine("🕹 Entrez une instruction (HELP pour de l'aide) :");

		return Console.ReadLine();
	}

	public void PrintHelp()
	{
		Console.WriteLine("\nCommandes disponibles :");
		Console.WriteLine("👉 STOCKS : afficher l'inventaire des vaisseaux");
		Console.WriteLine("👉 HELP : afficher les commandes disponibles");
		Console.WriteLine("👉 EXIT : quitter l'application");
	}
}
