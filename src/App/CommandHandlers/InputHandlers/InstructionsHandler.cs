using core.Assemblies;
using core.Services;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class InstructionsHandler : IInputHandler
{
	private const String InvalidCommandMessage = "La commande est invalide.";

	private readonly ComponentAssemblyService _componentAssemblyService;
	private readonly ComponentService _componentService;
	private readonly InventoryService _inventoryService;
	private readonly StarshipService _starshipService;

	public InstructionsHandler(
		ComponentAssemblyService componentAssemblyService,
		ComponentService componentService,
		InventoryService inventoryService,
		StarshipService starshipService
	)
	{
		this._componentAssemblyService = componentAssemblyService;
		this._componentService = componentService;
		this._inventoryService = inventoryService;
		this._starshipService = starshipService;
	}

	public void Handle(String input)
	{
		if (!HandlerHelper.IsCommandInputValid(input.Split()))
		{
			this.PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var splitBySpaceInput = input.Split(new[] { ' ' }, 2);
		if (!HandlerHelper.IsCommandNameSeparatedByOneSpace(splitBySpaceInput))
		{
			this.PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		try
		{
			var inputContent = splitBySpaceInput[1];
			var starshipCounts = this._starshipService.GetStarshipSumsFromInput(
				inputContent,
				this.PrintInvalidCommand
			);
			if (!UtilsFunction.IsDictionaryEmpty(starshipCounts))
			{
				this.AssembleStarships(starshipCounts);
			}
		}
		catch (Exception e)
		{
			Terminal.PrintMessageWithLinebreak(e.Message);
		}
	}

	private void PrintInvalidCommand(String message)
	{
		InstructionsDisplayHandler.PrintInvalidCommand(message);
	}

	private void AssembleStarships(Dictionary<String, Int32> starshipCounts)
	{
		try
		{
			foreach (var (starshipName, quantity) in starshipCounts)
			{
				var (engineCount, hullCount, wingCount, thrusterCount) =
					this._starshipService.GetStarshipComponentsCountFromInventories(starshipName);

				if (
					this._inventoryService.IsMoreInventoryRequired(
						starshipName,
						quantity,
						hullCount,
						engineCount,
						wingCount,
						thrusterCount
					)
				)
				{
					this.PrintInsufficientStock();
					return;
				}

				this.HandleStarshipAssembly(starshipName, quantity);
			}
		}
		catch (Exception e)
		{
			Terminal.PrintMessageWithLinebreak(e.Message);
		}
	}

	private void PrintInsufficientStock()
	{
		Terminal.PrintMessageWithLinebreak("Stock insuffisant.");
	}

	private void HandleStarshipAssembly(String starshipName, Int32 quantity)
	{
		for (var i = 1; i <= quantity; i++)
		{
			InstructionsDisplayHandler.PrintStarshipProductionStarting(starshipName, i);

			try
			{
				var starshipComponents = StarshipAssembly.Components[starshipName];

				foreach (var (componentName, componentCount) in starshipComponents)
				{
					InstructionsDisplayHandler.PrintGetOutStock(componentCount, componentName);
					this._componentService.GetComponentsOutFromStock(componentName, componentCount);
				}

				var componentAssembly = ComponentAssembly.Create(String.Empty, new());
				this._componentAssemblyService.Add(componentAssembly);

				foreach (var (componentName, componentCount) in starshipComponents)
				{
					for (var j = 1; j <= componentCount; j++)
					{
						InstructionsDisplayHandler.PrintAssemblingComponents(
							componentAssembly,
							componentName
						);
						this._componentAssemblyService.AddComponentAssemblyToItsInventory(
							componentAssembly,
							componentName
						);
					}
				}

				InstructionsDisplayHandler.PrintStarshipProductionFinishing(starshipName, i);
			}
			catch (Exception e)
			{
				Terminal.PrintMessageWithLinebreak(e.Message);
			}
		}

		Terminal.PrintLinebreak();
	}
}
