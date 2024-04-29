using core.Inventory;
using core.Inventory.Starships;
using core.Inventory.Starships.ComponentAssembly;
using core.Inventory.Starships.Components;

namespace core;

public class Menu
{
	private readonly UserInterface _userInterface;
	private readonly InMemoryStarship _inMemoryStarship;
	private readonly InMemoryEngine _inMemoryEngine;
	private readonly InMemoryHull _inMemoryHull;
	private readonly InMemoryWing _inMemoryWing;
	private readonly InMemoryThruster _inMemoryThruster;
	private readonly InMemoryComponentAssembly _inMemoryComponentAssembly;

	public Menu(
		UserInterface userInterface,
		InMemoryStarship inMemoryStarship,
		InMemoryEngine inMemoryEngine,
		InMemoryHull inMemoryHull,
		InMemoryWing inMemoryWing,
		InMemoryThruster inMemoryThruster,
		InMemoryComponentAssembly inMemoryComponentAssembly
	)
	{
		this._userInterface = userInterface;
		this._inMemoryStarship = inMemoryStarship;
		this._inMemoryEngine = inMemoryEngine;
		this._inMemoryHull = inMemoryHull;
		this._inMemoryWing = inMemoryWing;
		this._inMemoryThruster = inMemoryThruster;
		this._inMemoryComponentAssembly = inMemoryComponentAssembly;
	}

	public void Start()
	{
		_userInterface.PrintWelcomeMessage();

		this.InviteUserToInteractWithTheUserInterface();
	}

	private void InviteUserToInteractWithTheUserInterface()
	{
		var userInput = _userInterface.GetUserInput();
		if (IsHelpCommand(userInput))
		{
			_userInterface.PrintHelp();
		}
		else if (IsStocksCommand(userInput))
		{
			this.PrintInventory(_inMemoryStarship);
			this.PrintInventory(_inMemoryEngine);
			this.PrintInventory(_inMemoryHull);
			this.PrintInventory(_inMemoryWing);
			this.PrintInventory(_inMemoryThruster);
			this.PrintInventory(_inMemoryComponentAssembly);
		}
	}

	#region commands
	private Boolean IsHelpCommand(String? input)
	{
		return input is not null
			&& input.Trim().Equals("HELP", StringComparison.OrdinalIgnoreCase);
	}

	private Boolean IsStocksCommand(String? input)
	{
		return input is not null
			&& input.Trim().Equals("STOCKS", StringComparison.OrdinalIgnoreCase);
	}
	#endregion

	#region PrintInventory
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

	private void PrintInventory(InMemoryComponentAssembly inMemory)
	{
		inMemory.PrintInventory();
	}
	#endregion
}
