using core.Services;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class VerifyHandler : IInputHandler
{
	private const String InvalidCommandMessage = "La commande est invalide.";

	private readonly ComponentService _componentService;
	private readonly InventoryService _inventoryService;
	private readonly StarshipService _starshipService;

	public VerifyHandler(
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
			var starshipCounts = _starshipService.GetStarshipSumsFromInput(
				inputContent,
				this.PrintInvalidCommand
			);
			if (UtilsFunction.IsDictionaryEmpty(starshipCounts))
			{
				return;
			}

			VerifyStockAvailability(starshipCounts);
		}
		catch (Exception e)
		{
			Terminal.PrintMessageWithLinebreak(e.Message);
		}
	}

	private void PrintInvalidCommand(String message)
	{
		VerifyDisplayHandler.PrintInvalidCommand(message);
	}

	private void VerifyStockAvailability(Dictionary<String, Int32> starshipCounts)
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
		}

		this.PrintSufficientStock();
	}

	private void PrintSufficientStock()
	{
		VerifyDisplayHandler.PrintSufficientStock();
	}

	private void PrintInsufficientStock()
	{
		VerifyDisplayHandler.PrintInsufficientStock();
	}
}
