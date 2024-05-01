using System.Text.RegularExpressions;
using core.App.Products.Starship;
using core.App.Products.Starship.Components.Engine;
using core.App.Products.Starship.Components.Hull;
using core.App.Products.Starship.Components.Thruster;
using core.App.Products.Starship.Components.Wing;
using core.App.UI;
using core.App.UI.constants;
using core.App.UserInstructionOrder;
using core.In_memory;
using core.In_memory.Inventory;
using core.In_memory.Inventory.Components;

namespace core.App;

public class Menu
{
	private static readonly string QuantityWithStarshipPattern = @"(\d+)\s+(\w+)";

	private readonly UserInterface _userInterface;
	private readonly InMemoryStarship _inMemoryStarship;
	private readonly InMemoryEngine _inMemoryEngine;
	private readonly InMemoryHull _inMemoryHull;
	private readonly InMemoryWing _inMemoryWing;
	private readonly InMemoryThruster _inMemoryThruster;
	private readonly InMemoryComponentAssembly _inMemoryComponentAssembly;
	private readonly InMemoryUserInstruction _inMemoryUserInstruction;

	public Menu(
		UserInterface userInterface,
		InMemoryStarship inMemoryStarship,
		InMemoryEngine inMemoryEngine,
		InMemoryHull inMemoryHull,
		InMemoryWing inMemoryWing,
		InMemoryThruster inMemoryThruster,
		InMemoryComponentAssembly inMemoryComponentAssembly,
		InMemoryUserInstruction inMemoryUserInstruction
	)
	{
		this._userInterface = userInterface;
		this._inMemoryStarship = inMemoryStarship;
		this._inMemoryEngine = inMemoryEngine;
		this._inMemoryHull = inMemoryHull;
		this._inMemoryWing = inMemoryWing;
		this._inMemoryThruster = inMemoryThruster;
		this._inMemoryComponentAssembly = inMemoryComponentAssembly;
		this._inMemoryUserInstruction = inMemoryUserInstruction;
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
				this.HandleInstructionsCommand(userInput!); // `!`: kill the build warning 🤌
			}
			else if (IsUserInstructionCommand(userInput))
			{
				this.HandleUserInstructionCommand(userInput!); // `!`: kill the build warning 🤌
			}
			else if (IsUserInstructionsCommand(userInput))
			{
				this._inMemoryUserInstruction.PrintAll();
				this._userInterface.PrintLineBreak();
			}
			else
			{
				this._userInterface.PrintUnknownCommand();
			}
		}
	}

	private void HandleUserInstructionCommand(String userInput)
	{
		var userArgs = userInput.Split();
		if (!IsUserInstructionCommandValid(userArgs))
		{
			this._userInterface.PrintInvalidUserInstructionCommand();
			return;
		}

		var userInputParts = userInput.Split(new[] { ' ' }, 2);
		if (!IsUserInstructionCommandNameSeparatedByOneSpace(userInputParts))
		{
			this._userInterface.PrintInvalidUserInstructionCommand();
			return;
		}

		var starshipsPart = userInputParts[1];
		var userInstruction = GetCompleteUserInstructionFrom(starshipsPart);

		this._inMemoryUserInstruction.Add(userInstruction);
		this._userInterface.PrintLineBreak();
	}

	private UserInstruction GetCompleteUserInstructionFrom(String starshipsPart)
	{
		var userInstruction = new UserInstruction();

		foreach (var quantityAndStarship in starshipsPart.Split(", "))
		{
			var match = Regex.Match(quantityAndStarship.Trim(), QuantityWithStarshipPattern);
			if (!IsMatching(match))
			{
				this._userInterface.PrintInvalidUserInstructionCommand();
				continue;
			}

			if (!int.TryParse(match.Groups[1].Value, out var quantity))
			{
				this._userInterface.PrintInvalidUserInstructionCommand();
				continue;
			}

			var starshipModelInput = match.Groups[2].Value;
			var starshipModel = this.GetStarshipModel(starshipModelInput);
			if (IsUnknownStarship(starshipModel))
			{
				this._userInterface.PrintUnknownStarshipModel();
				continue;
			}

			userInstruction.Add(starshipModel, quantity);
		}

		return userInstruction;
	}

	private Boolean IsMatching(Match match)
	{
		return match.Success;
	}

	private Boolean IsUserInstructionCommandNameSeparatedByOneSpace(String[] userInputParts)
	{
		return userInputParts.Length == 2;
	}

	private Boolean IsUserInstructionCommandValid(String[] input)
	{
		return input.Length >= 3 && (input.Length - 1) % 2 == 0;
	}

	private void HandleInstructionsCommand(String userInput)
	{
		var userArgs = userInput.Split();
		if (!this.IsInstructionsCommandValid(userArgs))
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

			var starshipModel = this.GetStarshipModel(starshipModelArg);
			var (hullCount, engineCount, wingsCount, thrusterCount) =
				GetStarshipComponentsCountFromInventories();

			if (
				IsMoreInventoryRequired(
					starshipModel,
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

			if (IsCargoStarship(starshipModel))
			{
				this.HandleCargoStarshipAssembly(starshipModel, quantity);
			}
			else if (IsExplorerStarship(starshipModel))
			{
				this.HandleExplorerStarshipAssembly(starshipModel, quantity);
			}
			else if (IsSpeederStarship(starshipModel))
			{
				this.HandleSpeederStarshipAssembly(starshipModel, quantity);
			}
			else
			{
				this._userInterface.PrintUnknownStarshipModel();
			}
		}
	}

	private (Int32, Int32, Int32, Int32) GetStarshipComponentsCountFromInventories()
	{
		return (
			this._inMemoryHull.CountByName(HullModel.Hull_HC1),
			this._inMemoryEngine.CountByName(EngineModel.Engine_EC1),
			this._inMemoryWing.CountByName(WingModel.Wings_WC1),
			this._inMemoryThruster.CountByName(ThrusterModel.Thruster_TC1)
		);
	}

	private String GetStarshipModel(String model)
	{
		if (this.IsCargoStarship(model))
		{
			return StarshipModel.Cargo;
		}
		if (this.IsExplorerStarship(model))
		{
			return StarshipModel.Explorer;
		}
		if (this.IsSpeederStarship(model))
		{
			return StarshipModel.Speeder;
		}

		return StarshipModel.Unknown;
	}

	private Boolean IsCargoStarship(String starshipModelArg)
	{
		return starshipModelArg.Equals(
			StarshipModel.Cargo,
			StringComparison.OrdinalIgnoreCase
		);
	}

	private Boolean IsExplorerStarship(String starshipModelArg)
	{
		return starshipModelArg.Equals(
			StarshipModel.Explorer,
			StringComparison.OrdinalIgnoreCase
		);
	}

	private Boolean IsSpeederStarship(String starshipModelArg)
	{
		return starshipModelArg.Equals(
			StarshipModel.Speeder,
			StringComparison.OrdinalIgnoreCase
		);
	}

	private Boolean IsUnknownStarship(String starshipModelArg)
	{
		return starshipModelArg.Equals(
			StarshipModel.Unknown,
			StringComparison.OrdinalIgnoreCase
		);
	}

	private Boolean IsMoreInventoryRequired(
		String starshipModelArg,
		Int32 quantity,
		Int32 hullCount,
		Int32 engineCount,
		Int32 wingsCount,
		Int32 thrusterCount
	)
	{
		if (IsCargoStarship(starshipModelArg))
		{
			return IsMoreInventoryRequiredForCargoStarship(
				quantity,
				hullCount,
				engineCount,
				wingsCount,
				thrusterCount
			);
		}

		if (IsExplorerStarship(starshipModelArg))
		{
			return IsMoreInventoryRequiredForExplorerStarship(
				quantity,
				hullCount,
				engineCount,
				wingsCount,
				thrusterCount
			);
		}

		if (IsSpeederStarship(starshipModelArg))
		{
			return IsMoreInventoryRequiredForSpeederStarship(
				quantity,
				hullCount,
				engineCount,
				wingsCount,
				thrusterCount
			);
		}

		return false;
	}

	private bool IsMoreInventoryRequiredForCargoStarship(
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

	private bool IsMoreInventoryRequiredForExplorerStarship(
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
			&& thrusterCount < 2 * quantity;
	}

	private bool IsMoreInventoryRequiredForSpeederStarship(
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
			&& thrusterCount < 2 * quantity;
	}

	#region HandleStarshipAssembly
	private void HandleCargoStarshipAssembly(String starshipModelArg, int quantity)
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

	private Boolean IsUserInstructionCommand(String? input)
	{
		return input is not null
			&& input.StartsWith(Command.UserInstruction, StringComparison.OrdinalIgnoreCase);
	}

	private Boolean IsUserInstructionsCommand(String? input)
	{
		return input is not null
			&& input.Equals(Command.UserInstructions, StringComparison.OrdinalIgnoreCase);
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
