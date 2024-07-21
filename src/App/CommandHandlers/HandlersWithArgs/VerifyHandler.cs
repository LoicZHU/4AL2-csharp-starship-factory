using core.Services;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class VerifyHandler : IHandlerWithArgs
{
	private const String InvalidCommandMessage = "La commande est invalide.";

	private readonly ComponentService _componentService;
	private readonly StarshipService _starshipService;

	public VerifyHandler(ComponentService componentService, StarshipService starshipService)
	{
		this._componentService = componentService;
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

			if (this._componentService.IsComponentStockInsufficient(starshipCounts))
			{
				this.PrintInsufficientStock();
				return;
			}

			this.PrintSufficientStock();
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

	private void PrintSufficientStock()
	{
		VerifyDisplayHandler.PrintSufficientStock();
	}

	private void PrintInsufficientStock()
	{
		VerifyDisplayHandler.PrintInsufficientStock();
	}
}
