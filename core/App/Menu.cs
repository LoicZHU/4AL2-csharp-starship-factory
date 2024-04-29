using core.App.UI;
using core.Inventory.Starships;
using core.Inventory.Starships.ComponentAssembly;
using core.Inventory.Starships.Components;

namespace core.App;

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
		_userInterface.PrintInvitationToUserInteraction();

		var userInput = Console.ReadLine()?.Trim();
		if (IsHelpCommand(userInput))
		{
			_userInterface.PrintHelp();
		}
		else if (IsStocksCommand(userInput))
		{
			this.PrintAllInventories();
		}
		else if (IsInstructionsCommand(userInput))
		{
			var args = userInput.Split();
			if (!IsInstructionsCommandValid(args))
			{
				_userInterface.PrintInvalidInstructionCommand();
				return;
			}

			for (var i = 1; i < args.Length; i += 2)
			{
				try
				{
					var quantity = int.Parse(args[i]);
					var itemName = args[i + 1];

					Console.WriteLine($"Quantity: {quantity}, Item Name: {itemName}");
				}
				catch (FormatException)
				{
					Console.WriteLine(
						$"Error: '{args[i]}' is not a valid integer. Skipping this item."
					);
				}
			}
		}
		else
		{
			_userInterface.PrintUnknownCommand();
		}
	}

	private Boolean IsInstructionsCommandValid(String[] args)
	{
		return args.Length >= 3 && (args.Length - 1) % 2 == 0;
	}

	#region commands
	private Boolean IsHelpCommand(String? input)
	{
		return input is not null
			&& input.Equals(Command.Help, StringComparison.OrdinalIgnoreCase);
	}

	private Boolean IsInstructionsCommand(String? input)
	{
		return input is not null
			&& input.StartsWith(Command.Instructions, StringComparison.OrdinalIgnoreCase);
	}

	private Boolean IsStocksCommand(String? input)
	{
		return input is not null
			&& input.Equals(Command.Stocks, StringComparison.OrdinalIgnoreCase);
	}
	#endregion

	#region PrintInventory

	private void PrintAllInventories()
	{
		this.PrintInventory(_inMemoryStarship);
		this.PrintInventory(_inMemoryEngine);
		this.PrintInventory(_inMemoryHull);
		this.PrintInventory(_inMemoryWing);
		this.PrintInventory(_inMemoryThruster);
		this.PrintInventory(_inMemoryComponentAssembly);
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

	private void PrintInventory(InMemoryComponentAssembly inMemory)
	{
		inMemory.PrintInventory();
	}
	#endregion
}
