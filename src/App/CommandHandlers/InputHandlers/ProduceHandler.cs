using core.App.Helpers;
using core.Assemblies;
using core.Services;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class ProduceHandler : IInputHandler
{
	private const String InvalidCommandMessage = "La commande est invalide.";

	private readonly ComponentService _componentService;
	private readonly InventoryService _inventoryService;
	private readonly StarshipService _starshipService;

	public ProduceHandler(
		ComponentService componentService,
		InventoryService inventoryService,
		StarshipService starshipService
	)
	{
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
		ProduceDisplayHandler.PrintInvalidCommand(message);
	}

	private void AssembleStarships(Dictionary<String, Int32> starshipCounts)
	{
		try
		{
			foreach (var (starshipName, quantity) in starshipCounts)
			{
				var (engineCount, hullCount, wingCount, thrusterCount) =
					this._componentService.GetComponentsCountFromInventories(starshipName);

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

			this.PrintStockUpdatedMessage();
		}
		catch (Exception e)
		{
			Terminal.PrintMessageWithLinebreak(e.Message);
		}
	}

	private void PrintInsufficientStock()
	{
		VerifyTerminal.PrintUnavailableMessage();
		Terminal.PrintLinebreak();
	}

	private void HandleStarshipAssembly(String starshipName, Int32 quantity)
	{
		var starshipComponents = StarshipAssembly.Components[starshipName];

		for (var i = 1; i <= quantity; i++)
		{
			this.GetComponentsOutOfStock(starshipComponents);

			this._starshipService.AddStarship(StarshipFactory.Create(starshipName));
		}
	}

	private void GetComponentsOutOfStock(Dictionary<string, int> starshipComponents)
	{
		foreach (var (componentName, componentCount) in starshipComponents)
		{
			this._componentService.GetComponentsOutFromStock(componentName, componentCount);
		}
	}

	private void PrintStockUpdatedMessage()
	{
		ProduceDisplayHandler.PrintStockUpdated();
	}
}
