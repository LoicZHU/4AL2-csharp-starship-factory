using core.Inventory;
using core.Inventory.Starships.Components;

namespace core;

public class Menu
{
	private InMemoryStarship _inMemoryStarship;
	private InMemoryEngine _inMemoryEngine;
	private InMemoryHull _inMemoryHull;
	private InMemoryWing _inMemoryWing;
	private InMemoryThruster _inMemoryThruster;

	public Menu(
		InMemoryStarship inMemoryStarship,
		InMemoryEngine inMemoryEngine,
		InMemoryHull inMemoryHull,
		InMemoryWing inMemoryWing,
		InMemoryThruster inMemoryThruster
	)
	{
		this._inMemoryStarship = inMemoryStarship;
		this._inMemoryEngine = inMemoryEngine;
		this._inMemoryHull = inMemoryHull;
		this._inMemoryWing = inMemoryWing;
		this._inMemoryThruster = inMemoryThruster;
	}

	public void Start()
	{
		var ui = new UserInterface();

		ui.PrintWelcomeMessage();

		var userInput = ui.GetUserInput();
		if (IsStocksCommand(userInput))
		{
			this.PrintInventory(_inMemoryStarship);
			this.PrintInventory(_inMemoryEngine);
			this.PrintInventory(_inMemoryHull);
			this.PrintInventory(_inMemoryWing);
			this.PrintInventory(_inMemoryThruster);
		}
		else if (IsHelpCommand(userInput))
		{
			ui.PrintHelp();
		}
	}

	private Boolean IsStocksCommand(String? input)
	{
		return input is not null
			&& input.Trim().Equals("STOCKS", StringComparison.OrdinalIgnoreCase);
	}

	private void PrintInventory(InMemoryStarship inMemory)
	{
		inMemory.PrintInventory();
	}

	private void PrintInventory(InMemoryEngine inMemory)
	{
		inMemory.PrintInventory();
	}

	private void PrintInventory(InMemoryHull inMemory)
	{
		inMemory.PrintInventory();
	}

	private void PrintInventory(InMemoryWing inMemory)
	{
		inMemory.PrintInventory();
	}

	private void PrintInventory(InMemoryThruster inMemory)
	{
		inMemory.PrintInventory();
	}

	private Boolean IsHelpCommand(String? input)
	{
		return input is not null
			&& input.Trim().Equals("HELP", StringComparison.OrdinalIgnoreCase);
	}
}
