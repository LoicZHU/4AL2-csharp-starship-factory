using core.App.UI;
using core.Inventory.Starships;
using core.Inventory.Starships.ComponentAssembly;
using core.Inventory.Starships.Components;
using core.Products.Starship;
using core.Products.Starship.ComponentAssembly;
using core.Products.Starship.Components.Engine;
using core.Products.Starship.Components.Hull;
using core.Products.Starship.Components.Thruster;
using core.Products.Starship.Components.Wing;

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

		while (true)
		{
			var userInput = Console.ReadLine()?.Trim();
			if (IsHelpCommand(userInput))
			{
				this._userInterface.PrintHelp();
			}
			else if (IsStocksCommand(userInput))
			{
				this.PrintAllInventories();
			}
			else if (IsInstructionsCommand(userInput))
			{
				this.HandleInstructionsCommand(userInput);
			}
			else
			{
				this._userInterface.PrintUnknownCommand();
			}
		}
	}

	private void HandleInstructionsCommand(String? userInput)
	{
		var userArgs = userInput.Split();
		if (!IsInstructionsCommandValid(userArgs))
		{
			_userInterface.PrintInvalidInstructionCommand();
			return;
		}

		for (var i = 1; i < userArgs.Length; i += 2)
		{
			var starshipModelArg = userArgs[i + 1];
			if (!int.TryParse(userArgs[i], out var quantity))
			{
				_userInterface.PrintInvalidInstructionCommand();
				return;
			}

			var hullCount = this._inMemoryHull.CountByName(HullModel.Hull_HC1);
			var engineCount = this._inMemoryHull.CountByName(EngineModel.Engine_EC1);
			var wingsCount = this._inMemoryHull.CountByName(WingModel.Wings_WC1);
			var thrusterCount = this._inMemoryHull.CountByName(ThrusterModel.Thruster_TC1);

			if (IsCargoStarship(starshipModelArg))
			{
				if (
					IsMoreInventoryRequired(
						quantity,
						hullCount,
						engineCount,
						wingsCount,
						thrusterCount
					)
				)
				{
					return;
				}

				this.HandleCargoStarshipAssembly(StarshipModel.Cargo, quantity);
			}
			else if (IsExplorerStarship(starshipModelArg))
			{
				if (
					IsMoreInventoryRequired(
						quantity,
						hullCount,
						engineCount,
						wingsCount,
						thrusterCount
					)
				)
				{
					return;
				}

				this.HandleExplorerStarshipAssembly(StarshipModel.Explorer, quantity);
			}
			else if (IsSpeederStarship(starshipModelArg))
			{
				if (
					IsMoreInventoryRequired(
						quantity,
						hullCount,
						engineCount,
						wingsCount,
						thrusterCount
					)
				)
				{
					return;
				}

				this.HandleSpeederStarshipAssembly(StarshipModel.Speeder, quantity);
			}
			else
			{
				this._userInterface.PrintUnknownStarshipModel();
			}
		}
	}

	private Boolean IsSpeederStarship(string starshipModelArg)
	{
		return starshipModelArg.Equals(
			StarshipModel.Speeder,
			StringComparison.OrdinalIgnoreCase
		);
	}

	private Boolean IsExplorerStarship(string starshipModelArg)
	{
		return starshipModelArg.Equals(
			StarshipModel.Explorer,
			StringComparison.OrdinalIgnoreCase
		);
	}

	private Boolean IsCargoStarship(string starshipModelArg)
	{
		return starshipModelArg.Equals(
			StarshipModel.Cargo,
			StringComparison.OrdinalIgnoreCase
		);
	}

	private bool IsMoreInventoryRequired(
		Int32 quantity,
		Int32 hullCount,
		Int32 engineCount,
		Int32 wingsCount,
		Int32 thrusterCount
	)
	{
		return hullCount < 1 * quantity
			&& engineCount < 1 * quantity
			&& wingsCount < 1 * quantity
			&& thrusterCount < 1 * quantity;
	}

	#region HandleStarshipAssembly
	private void HandleCargoStarshipAssembly(string starshipModelArg, int quantity)
	{
		this._userInterface.PrintStarshipProductionStarting(starshipModelArg);

		for (var i = 0; i < quantity; i++)
		{
			GetOutCargoComponentsFromInventories();

			var componentAssembly = new ComponentAssembly(String.Empty, new List<string>());
			this._inMemoryComponentAssembly.Add(componentAssembly);

			foreach (var componentModel in StarshipAssembly.ComponentsMap[starshipModelArg])
			{
				AddComponentAssemblyToItsInventory(componentAssembly, componentModel);
			}
		}

		this._userInterface.PrintStarshipProductionFinishing(starshipModelArg);
	}

	private void HandleExplorerStarshipAssembly(String starshipModelArg, Int32 quantity)
	{
		this._userInterface.PrintStarshipProductionStarting(starshipModelArg);

		for (var i = 0; i < quantity; i++)
		{
			GetOutExplorerComponentsFromInventories();

			var componentAssembly = new ComponentAssembly(String.Empty, new List<string>());
			this._inMemoryComponentAssembly.Add(componentAssembly);

			foreach (var componentModel in StarshipAssembly.ComponentsMap[starshipModelArg])
			{
				AddComponentAssemblyToItsInventory(componentAssembly, componentModel);
			}
		}

		this._userInterface.PrintStarshipProductionFinishing(starshipModelArg);
	}

	private void HandleSpeederStarshipAssembly(String starshipModelArg, Int32 quantity)
	{
		this._userInterface.PrintStarshipProductionStarting(starshipModelArg);

		for (var i = 0; i < quantity; i++)
		{
			GetOutSpeederComponentsFromInventories();

			var componentAssembly = new ComponentAssembly(String.Empty, new List<string>());
			this._inMemoryComponentAssembly.Add(componentAssembly);

			foreach (var componentModel in StarshipAssembly.ComponentsMap[starshipModelArg])
			{
				AddComponentAssemblyToItsInventory(componentAssembly, componentModel);
			}
		}

		this._userInterface.PrintStarshipProductionFinishing(starshipModelArg);
	}

	private void AddComponentAssemblyToItsInventory(
		ComponentAssembly componentAssembly,
		string componentModel
	)
	{
		this._userInterface.PrintAssemblingComponentsMessage(
			componentAssembly,
			componentModel
		);

		this._inMemoryComponentAssembly.AddComponent(componentAssembly.Id, componentModel);
	}

	private void GetOutCargoComponentsFromInventories()
	{
		this.GetOutHullFromItsInventory(HullModel.Hull_HC1, 1);
		this.GetOutEngineFromItsInventory(EngineModel.Engine_EC1, 1);
		this.GetOutWingFromItsInventory(WingModel.Wings_WC1, 1);
		this.GetOutThrusterFromItsInventory(ThrusterModel.Thruster_TC1, 1);
	}

	private void GetOutExplorerComponentsFromInventories()
	{
		this.GetOutHullFromItsInventory(HullModel.Hull_HE1, 1);
		this.GetOutEngineFromItsInventory(EngineModel.Engine_EE1, 1);
		this.GetOutWingFromItsInventory(WingModel.Wings_WE1, 1);
		this.GetOutThrusterFromItsInventory(ThrusterModel.Thruster_TE1, 1);
	}

	private void GetOutSpeederComponentsFromInventories()
	{
		this.GetOutHullFromItsInventory(HullModel.Hull_HS1, 1);
		this.GetOutEngineFromItsInventory(EngineModel.Engine_ES1, 1);
		this.GetOutWingFromItsInventory(WingModel.Wings_WS1, 1);
		this.GetOutThrusterFromItsInventory(ThrusterModel.Thruster_TS1, 2);
	}

	private void GetOutHullFromItsInventory(String componentModel, Int32 quantity)
	{
		this._userInterface.PrintGetOutStockMessage(quantity, componentModel);

		for (var i = 1; i <= quantity; i++)
		{
			this._inMemoryHull.Remove(componentModel);
		}
	}

	private void GetOutEngineFromItsInventory(String componentModel, Int32 quantity)
	{
		this._userInterface.PrintGetOutStockMessage(quantity, componentModel);

		for (var i = 1; i <= quantity; i++)
		{
			this._inMemoryEngine.Remove(componentModel);
		}
	}

	private void GetOutWingFromItsInventory(String componentModel, Int32 quantity)
	{
		this._userInterface.PrintGetOutStockMessage(quantity, componentModel);

		for (var i = 1; i <= quantity; i++)
		{
			this._inMemoryWing.Remove(componentModel);
		}
	}

	private void GetOutThrusterFromItsInventory(String componentModel, Int32 quantity)
	{
		this._userInterface.PrintGetOutStockMessage(quantity, componentModel);

		for (var i = 1; i <= quantity; i++)
		{
			this._inMemoryThruster.Remove(componentModel);
		}
	}
	#endregion

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

	private Boolean IsInstructionsCommandValid(String[] args)
	{
		return args.Length >= 3 && (args.Length - 1) % 2 == 0;
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
		this.PrintInventory(this._inMemoryStarship);
		this.PrintInventory(this._inMemoryEngine);
		this.PrintInventory(this._inMemoryHull);
		this.PrintInventory(this._inMemoryWing);
		this.PrintInventory(this._inMemoryThruster);
		this.PrintInventory(this._inMemoryComponentAssembly);
		this._userInterface.PrintLineBreak();
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
