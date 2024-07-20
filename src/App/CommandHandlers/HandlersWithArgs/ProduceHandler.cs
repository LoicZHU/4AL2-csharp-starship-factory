using core.App.Helpers;
using core.Assemblies;
using core.Services;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class ProduceHandlerWithArgs : IHandlerWithArgs
{
	private const String InvalidCommandMessage = "La commande est invalide.";

	private readonly ComponentService _componentService;
	private readonly StarshipService _starshipService;

	public ProduceHandlerWithArgs(
		ComponentService componentService,
		StarshipService starshipService
	)
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
			var starshipCounts = this._starshipService.GetStarshipSumsFromInput(
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

			this.HandleStarshipAssemblies(starshipCounts);
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

	private void HandleStarshipAssemblies(Dictionary<String, Int32> starshipCounts)
	{
		try
		{
			foreach (var (starshipName, quantity) in starshipCounts)
			{
				this.AssembleStarships(starshipName, quantity);
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

	private void AssembleStarships(String starshipName, Int32 quantity)
	{
		var starshipComponents = StarshipAssembly.Components[starshipName];

		for (var i = 1; i <= quantity; i++)
		{
			this.GetComponentsOutOfStock(starshipComponents);

			this._starshipService.AddStarship(StarshipFactory.Create(starshipName));
		}
	}

	private void GetComponentsOutOfStock(Dictionary<String, Int32> starshipComponents)
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
