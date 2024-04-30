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
					var starshipModelArg = args[i + 1];
					if (!int.TryParse(args[i], out var quantity))
					{
						_userInterface.PrintInvalidInstructionCommand();
						return;
					}

					if (
						starshipModelArg.Equals(
							StarshipModel.Cargo,
							StringComparison.OrdinalIgnoreCase
						)
					)
					{
						// TODO: Implement
						/**
						 *o Hull_HC1
						  o Engine_EC1
						  o Wings_WC1
						  o Thruster_TC1
						 */
						var hullCount = this._inMemoryHull.CountByName(HullModel.Hull_HC1);
						var engineCount = this._inMemoryHull.CountByName(EngineModel.Engine_EC1);
						var wingsCount = this._inMemoryHull.CountByName(WingModel.Wings_WC1);
						var thrusterCount = this._inMemoryHull.CountByName(
							ThrusterModel.Thruster_TC1
						);
						if (
							!IsMoreInventoryRequired(
								quantity,
								hullCount,
								engineCount,
								wingsCount,
								thrusterCount
							)
						)
						{
							this._userInterface.PrintStarshipProductionStarting(starshipModelArg);

							this.HandleCargoProduction(quantity, starshipModelArg);

							this._userInterface.PrintStarshipProductionFinishing(starshipModelArg);
						}
					}
					else if (
						starshipModelArg.Equals(
							StarshipModel.Explorer,
							StringComparison.OrdinalIgnoreCase
						)
					)
					{
						// TODO: Implement
						/**
						 * o Hull_HE1
						  o Engine_EE1
						  o Wings_WE1
						  o Thruster_TE1
						 */
					}
					else if (
						starshipModelArg.Equals(
							StarshipModel.Speeder,
							StringComparison.OrdinalIgnoreCase
						)
					)
					{
						// TODO: Implement
						/**
						 * o Hull_HS1
						  o Engine_ES1
						  o Wings_WS1
						  o Thruster_TS1
						  o Thruster_TS1
						 */
					}
				}
			}
			else
			{
				_userInterface.PrintUnknownCommand();
			}
		}
	}

	private void HandleCargoProduction(int quantity, string starshipModelArg)
	{
		for (var i = 0; i < quantity; i++)
		{
			this._userInterface.PrintGetOutStockMessage(1, HullModel.Hull_HC1);
			this._inMemoryHull.Remove(HullModel.Hull_HC1);

			this._userInterface.PrintGetOutStockMessage(1, EngineModel.Engine_EC1);
			this._inMemoryEngine.Remove(EngineModel.Engine_EC1);

			this._userInterface.PrintGetOutStockMessage(1, WingModel.Wings_WC1);
			this._inMemoryWing.Remove(WingModel.Wings_WC1);

			this._userInterface.PrintGetOutStockMessage(1, ThrusterModel.Thruster_TC1);
			this._inMemoryThruster.Remove(ThrusterModel.Thruster_TC1);

			var componentAssembly = new ComponentAssembly(String.Empty, new List<string>());
			this._inMemoryComponentAssembly.Add(componentAssembly);

			foreach (var componentModel in StarshipAssembly.ComponentsMap[starshipModelArg])
			{
				this._userInterface.PrintAssemblingComponentsMessage(
					componentAssembly,
					componentModel
				);
				this._inMemoryComponentAssembly.AddComponent(
					componentAssembly.Id,
					componentModel
				);
			}
		}
	}

	private bool IsMoreInventoryRequired(
		int quantity,
		int hullCount,
		int engineCount,
		int wingsCount,
		int thrusterCount
	)
	{
		return hullCount < 1 * quantity
			&& engineCount < 1 * quantity
			&& wingsCount < 1 * quantity
			&& thrusterCount < 1 * quantity;
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
