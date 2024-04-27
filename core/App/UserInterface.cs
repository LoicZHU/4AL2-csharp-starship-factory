using core.Inventory;

namespace core;

public class UserInterface
{
	public void PrintWelcomeMessage()
	{
		Console.WriteLine("Bienvenue chez Capsule Corp ! 🚀");
	}

	public void PrintUserCommandInvitation()
	{
		// TODO: FAIRE UN MENU AVEC LES COMMANDES
		Console.WriteLine("🕹 Entrez une instruction (HELP pour de l'aide) :");

		var input = Console.ReadLine();
		if (IsStocksCommand(input))
		{
			this.PrintStarshipInventory();
		}
		else if (IsHelpCommand(input))
		{
			this.PrintHelp();
		}
	}

	private static bool IsHelpCommand(String? input)
	{
		return input is not null
			&& input.Trim().Equals("HELP", StringComparison.OrdinalIgnoreCase);
	}

	private void PrintHelp()
	{
		Console.WriteLine("\nCommandes disponibles :");
		Console.WriteLine("👉 STOCKS : afficher l'inventaire des vaisseaux");
		Console.WriteLine("👉 HELP : afficher les commandes disponibles");
		Console.WriteLine("👉 EXIT : quitter l'application");
	}

	private static Boolean IsStocksCommand(String? input)
	{
		return input is not null
			&& input.Trim().Equals("STOCKS", StringComparison.OrdinalIgnoreCase);
	}

	private void PrintStarshipInventory()
	{
		// ! TODO: NEW INMEMORY
		var starships = new InMemoryStarship();
		starships.PrintInventory();
	}
}
