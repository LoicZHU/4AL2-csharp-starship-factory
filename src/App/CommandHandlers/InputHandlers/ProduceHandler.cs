using core.Assemblies;
using core.Components;
using core.Repositories.ComponentAssemblyRepository;
using core.Repositories.ComponentRepository;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class ProduceHandler : IInputHandler
{
	private const String InvalidCommandMessage = "La commande est invalide.";

	private readonly IComponentAssemblyRepository _componentAssemblyRepository;
	private readonly IComponentRepository _componentRepository;

	public ProduceHandler(
		IComponentAssemblyRepository componentAssemblyRepository,
		IComponentRepository componentRepository
	)
	{
		_componentAssemblyRepository = componentAssemblyRepository;
		this._componentRepository = componentRepository;
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

		var inputContent = splitBySpaceInput[1];
		var starshipCounts = this.GetStarshipSumsFromInput(inputContent);
		if (!UtilsFunction.IsDictionaryEmpty(starshipCounts))
		{
			this.AssembleStarships(starshipCounts);
		}
	}

	private void PrintInvalidCommand(String message)
	{
		ProduceDisplayHandler.PrintInvalidCommand(message);
	}

	private Dictionary<String, Int32> GetStarshipSumsFromInput(String input)
	{
		var starshipCounts = new Dictionary<String, Int32>();

		foreach (var quantityAndStarship in input.Split(", "))
		{
			var (isValid, starshipName, quantity, errorMessage) =
				HandlerHelper.ParseQuantityAndStarship(quantityAndStarship);
			if (!isValid)
			{
				this.PrintInvalidCommand(errorMessage);
				return new Dictionary<String, Int32>();
			}

			if (!starshipCounts.ContainsKey(starshipName))
			{
				starshipCounts.Add(starshipName, quantity);
			}
			else
			{
				starshipCounts[starshipName] += quantity;
			}
		}

		return starshipCounts;
	}

	private void AssembleStarships(Dictionary<String, Int32> starshipCounts)
	{
		try
		{
			foreach (var (starshipName, quantity) in starshipCounts)
			{
				var (hullCount, engineCount, wingCount, thrusterCount) =
					this.GetStarshipComponentsCountFromInventories();

				if (
					this.IsMoreInventoryRequired(
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

	private (Int32, Int32, Int32, Int32) GetStarshipComponentsCountFromInventories()
	{
		try
		{
			return (
				this._componentRepository.GetCount(EngineComponent.EngineEc1),
				this._componentRepository.GetCount(HullComponent.HullHc1),
				this._componentRepository.GetCount(ThrusterComponent.ThrusterTc1),
				this._componentRepository.GetCount(WingComponent.WingsWc1)
			);
		}
		catch (Exception e)
		{
			Terminal.PrintMessageWithLinebreak(e.Message);
			return (0, 0, 0, 0);
		}
	}

	private Boolean IsMoreInventoryRequired(
		String starshipName,
		Int32 quantity,
		Int32 hullCount,
		Int32 engineCount,
		Int32 wingCount,
		Int32 thrusterCount
	)
	{
		if (HandlerHelper.IsCargoStarship(starshipName))
		{
			return this.IsMoreInventoryRequiredForCargoStarship(
				quantity,
				hullCount,
				engineCount,
				wingCount,
				thrusterCount
			);
		}

		if (HandlerHelper.IsExplorerOrSpeederStarship(starshipName))
		{
			return this.IsMoreInventoryRequiredForExplorerOrSpeederStarship(
				quantity,
				hullCount,
				engineCount,
				wingCount,
				thrusterCount
			);
		}

		return false;
	}

	private Boolean IsMoreInventoryRequiredForCargoStarship(
		Int32 quantity,
		Int32 hullCount,
		Int32 engineCount,
		Int32 wingCount,
		Int32 thrusterCount
	)
	{
		return hullCount < 1 * quantity
			&& engineCount < 1 * quantity
			&& wingCount < 1 * quantity
			&& thrusterCount < 1 * quantity;
	}

	private Boolean IsMoreInventoryRequiredForExplorerOrSpeederStarship(
		Int32 quantity,
		Int32 hullCount,
		Int32 engineCount,
		Int32 wingCount,
		Int32 thrusterCount
	)
	{
		return hullCount < 1 * quantity
			&& engineCount < 1 * quantity
			&& wingCount < 1 * quantity
			&& thrusterCount < 2 * quantity;
	}

	private void PrintInsufficientStock()
	{
		VerifyTerminal.PrintUnavailableMessage();
		Terminal.PrintLinebreak();
	}

	private void HandleStarshipAssembly(String starshipName, Int32 quantity)
	{
		var getComponentsOutFromStock = this.GetComponentsOutFromStockDelegate(starshipName);
		if (UtilsFunction.IsNull(getComponentsOutFromStock))
		{
			throw new InvalidOperationException(
				"Callback to get out components from inventories is null."
			);
		}

		for (var i = 1; i <= quantity; i++)
		{
			getComponentsOutFromStock();

			var componentAssembly = ComponentAssembly.Create(String.Empty, new List<String>());
			this._componentAssemblyRepository.Add(componentAssembly);

			foreach (var (componentName, count) in StarshipAssembly.Components[starshipName])
			{
				for (var j = 1; j <= count; j++)
				{
					this.AddComponentAssemblyToItsInventory(componentAssembly, componentName);
				}
			}
		}
	}

	private Action? GetComponentsOutFromStockDelegate(String starshipName)
	{
		if (HandlerHelper.IsCargoStarship(starshipName))
		{
			return this.GetCargoComponentsOutFromStock;
		}

		if (HandlerHelper.IsExplorerStarship(starshipName))
		{
			return this.GetExplorerComponentsOutFromStock;
		}

		if (HandlerHelper.IsSpeederStarship(starshipName))
		{
			return this.GetSpeederComponentsOutFromStock;
		}

		return null;
	}

	private void AddComponentAssemblyToItsInventory(
		ComponentAssembly componentAssembly,
		String componentName
	)
	{
		this._componentAssemblyRepository.AddComponent(componentAssembly.Id, componentName);
	}

	private void GetCargoComponentsOutFromStock()
	{
		GetComponentsOutFromStock(HullComponent.HullHc1, 1);
		GetComponentsOutFromStock(EngineComponent.EngineEc1, 1);
		GetComponentsOutFromStock(WingComponent.WingsWc1, 1);
		GetComponentsOutFromStock(ThrusterComponent.ThrusterTc1, 1);
	}

	private void GetExplorerComponentsOutFromStock()
	{
		GetComponentsOutFromStock(HullComponent.HullHe1, 1);
		GetComponentsOutFromStock(EngineComponent.EngineEe1, 1);
		GetComponentsOutFromStock(WingComponent.WingsWe1, 1);
		GetComponentsOutFromStock(ThrusterComponent.ThrusterTe1, 1);
	}

	private void GetSpeederComponentsOutFromStock()
	{
		GetComponentsOutFromStock(HullComponent.HullHs1, 1);
		GetComponentsOutFromStock(EngineComponent.EngineEs1, 1);
		GetComponentsOutFromStock(WingComponent.WingsWs1, 1);
		GetComponentsOutFromStock(ThrusterComponent.ThrusterTs1, 2);
	}

	private void GetComponentsOutFromStock(String componentName, Int32 quantity)
	{
		for (var i = 1; i <= quantity; i++)
		{
			this._componentRepository.Remove(componentName);
		}
	}

	private void PrintStockUpdatedMessage()
	{
		ProduceDisplayHandler.PrintStockUpdated();
	}
}