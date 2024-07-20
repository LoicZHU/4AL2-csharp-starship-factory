using core.App.Helpers;
using core.Assemblies;
using core.Services;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class InstructionsHandler : IInputHandler
{
	private const String InvalidCommandMessage = "La commande est invalide.";

	private readonly ComponentService _componentService;
	private readonly StarshipService _starshipService;

	public InstructionsHandler(
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
		InstructionsDisplayHandler.PrintInvalidCommand(message);
	}

	private void HandleStarshipAssemblies(Dictionary<String, Int32> starshipCounts)
	{
		try
		{
			foreach (var (starshipName, quantity) in starshipCounts)
			{
				this.AssembleStarships(starshipName, quantity);
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

	private void AssembleStarships(String starshipName, Int32 quantity)
	{
		for (var i = 1; i <= quantity; i++)
		{
			InstructionsDisplayHandler.PrintStarshipProductionStarting(starshipName, i);

			try
			{
				var starshipComponents = StarshipAssembly.Components[starshipName];

				this.PrintAndGetComponentsOutOfStock(starshipComponents);
				this.PrintAssemblingComponents(starshipComponents);
				InstructionsDisplayHandler.PrintStarshipProductionFinishing(starshipName, i);

				this._starshipService.AddStarship(StarshipFactory.Create(starshipName));
			}
			catch (Exception e)
			{
				Terminal.PrintMessageWithLinebreak(e.Message);
			}
		}

		Terminal.PrintLinebreak();
	}

	private void PrintAssemblingComponents(Dictionary<String, Int32> starshipComponents)
	{
		var components = new List<String>();
		foreach (var (componentName, componentCount) in starshipComponents)
		{
			for (var j = 1; j <= componentCount; j++)
			{
				InstructionsDisplayHandler.PrintAssemblingComponents(components, componentName);
				components.Add(componentName);
			}
		}
	}

	private void PrintAndGetComponentsOutOfStock(
		Dictionary<String, Int32> starshipComponents
	)
	{
		foreach (var (componentName, componentCount) in starshipComponents)
		{
			InstructionsDisplayHandler.PrintGetOutStock(componentCount, componentName);
			this._componentService.GetComponentsOutFromStock(componentName, componentCount);
		}
	}
}
