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
		Console.WriteLine("🕹 Entrez une instruction :");

		var input = Console.ReadLine();
		if (IsStocksCommand(input))
		{
			this.PrintStarshipInventory();
		}
	}

	private static bool IsStocksCommand(string? input)
	{
		return input.Trim().Equals("STOCKS", StringComparison.OrdinalIgnoreCase);
	}

	private void PrintStarshipInventory()
	{
		// ! TODO: NEW INMEMORY
		var starships = new InMemoryStarship();
		starships.PrintInventory();
	}
}
