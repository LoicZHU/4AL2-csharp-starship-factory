using core.Assemblies;
using core.Components;
using core.Repositories.ComponentAssemblyRepository;
using core.Repositories.ComponentRepository;
using core.Starships;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class InstructionsHandler : IInputHandler
{
	private const String InvalidCommandMessage = "La commande est invalide.";

	private readonly IComponentAssemblyRepository _componentAssemblyRepository;
	private readonly IComponentRepository _componentRepository;

	public InstructionsHandler(
		IComponentAssemblyRepository componentAssemblyRepository,
		IComponentRepository componentRepository
	)
	{
		this._componentAssemblyRepository = componentAssemblyRepository;
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

		try
		{
			var inputContent = splitBySpaceInput[1];
			var starshipCounts = this.GetStarshipSumsFromInput(inputContent);
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
				var (engineCount, hullCount, wingCount, thrusterCount) =
					this.GetStarshipComponentsCountFromInventories(starshipName);

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
		}
		catch (Exception e)
		{
			Terminal.PrintMessageWithLinebreak(e.Message);
		}
	}

	private (Int32, Int32, Int32, Int32) GetStarshipComponentsCountFromInventories(
		String starshipName
	)
	{
		try
		{
			if (HandlerHelper.IsCargoStarship(starshipName))
			{
				return (
					this._componentRepository.GetCount(EngineComponent.EngineEc1),
					this._componentRepository.GetCount(HullComponent.HullHc1),
					this._componentRepository.GetCount(WingComponent.WingWc1),
					this._componentRepository.GetCount(ThrusterComponent.ThrusterTc1)
				);
			}

			if (HandlerHelper.IsExplorerStarship(starshipName))
			{
				return (
					this._componentRepository.GetCount(EngineComponent.EngineEe1),
					this._componentRepository.GetCount(HullComponent.HullHe1),
					this._componentRepository.GetCount(WingComponent.WingWe1),
					this._componentRepository.GetCount(ThrusterComponent.ThrusterTe1)
				);
			}

			if (HandlerHelper.IsSpeederStarship(starshipName))
			{
				return (
					this._componentRepository.GetCount(EngineComponent.EngineEs1),
					this._componentRepository.GetCount(HullComponent.HullHs1),
					this._componentRepository.GetCount(WingComponent.WingWs1),
					this._componentRepository.GetCount(ThrusterComponent.ThrusterTs1)
				);
			}

			return (0, 0, 0, 0);
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
		if (HandlerHelper.IsSpeederStarship(starshipName))
		{
			return this.IsMoreInventoryRequiredForSpeederStarship(
				quantity,
				hullCount,
				engineCount,
				wingCount,
				thrusterCount
			);
		}

		if (HandlerHelper.IsCargoOrExplorerStarship(starshipName))
		{
			return this.IsMoreInventoryRequiredForCargoOrExplorerStarship(
				quantity,
				hullCount,
				engineCount,
				wingCount,
				thrusterCount
			);
		}

		return false;
	}

	private Boolean IsMoreInventoryRequiredForSpeederStarship(
		Int32 quantity,
		Int32 hullCount,
		Int32 engineCount,
		Int32 wingCount,
		Int32 thrusterCount
	)
	{
		var components = StarshipAssembly.Components[StarshipName.Speeder];

		return hullCount < components[HullComponent.HullHs1] * quantity
			|| engineCount < components[EngineComponent.EngineEs1] * quantity
			|| wingCount < components[WingComponent.WingWs1] * quantity
			|| thrusterCount < components[ThrusterComponent.ThrusterTs1] * quantity;
	}

	private Boolean IsMoreInventoryRequiredForCargoOrExplorerStarship(
		Int32 quantity,
		Int32 hullCount,
		Int32 engineCount,
		Int32 wingCount,
		Int32 thrusterCount
	)
	{
		var components = StarshipAssembly.Components[StarshipName.Cargo];

		return hullCount < components[HullComponent.HullHc1] * quantity
			|| engineCount < components[EngineComponent.EngineEc1] * quantity
			|| wingCount < components[WingComponent.WingWc1] * quantity
			|| thrusterCount < components[ThrusterComponent.ThrusterTc1] * quantity;
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
				this.GetComponentsOutFromStock(starshipName);

				var componentAssembly = ComponentAssembly.Create(String.Empty, new());
				this._componentAssemblyRepository.Add(componentAssembly);

				foreach (var (componentName, count) in StarshipAssembly.Components[starshipName])
				{
					for (var j = 1; j <= count; j++)
					{
						this.AddComponentAssemblyToItsInventory(componentAssembly, componentName);
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

	private void AddComponentAssemblyToItsInventory(
		ComponentAssembly componentAssembly,
		String componentName
	)
	{
		InstructionsDisplayHandler.PrintAssemblingComponents(
			componentAssembly,
			componentName
		);
		this._componentAssemblyRepository.AddComponent(componentAssembly.Id, componentName);
	}

	private void GetComponentsOutFromStock(String starshipName)
	{
		foreach (var (componentName, quantity) in StarshipAssembly.Components[starshipName])
		{
			InstructionsDisplayHandler.PrintGetOutStock(quantity, componentName);

			for (var i = 1; i <= quantity; i++)
			{
				this._componentRepository.Remove(componentName);
			}
		}
	}
}
