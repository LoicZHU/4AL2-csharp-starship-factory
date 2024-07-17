using core.Assemblies;
using core.Components;
using core.Repositories.ComponentRepository;
using core.Starships;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class VerifyHandler : IInputHandler
{
	private const String InvalidCommandMessage = "La commande est invalide.";
	private readonly IComponentRepository _componentRepository;

	public VerifyHandler(IComponentRepository componentRepository)
	{
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
		if (UtilsFunction.IsDictionaryEmpty(starshipCounts))
		{
			return;
		}

		try
		{
			VerifyStockAvailability(starshipCounts);
		}
		catch (Exception e)
		{
			Terminal.PrintMessageWithLinebreak(e.Message);
		}
	}

	private void VerifyStockAvailability(Dictionary<String, Int32> starshipCounts)
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
		}

		this.PrintSufficientStock();
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

	private void PrintInvalidCommand(String message)
	{
		VerifyDisplayHandler.PrintInvalidCommand(message);
	}

	private (Int32, Int32, Int32, Int32) GetStarshipComponentsCountFromInventories()
	{
		try
		{
			return (
				this._componentRepository.GetCount(EngineComponent.EngineEc1),
				this._componentRepository.GetCount(HullComponent.HullHc1),
				this._componentRepository.GetCount(WingComponent.WingWc1),
				this._componentRepository.GetCount(ThrusterComponent.ThrusterTc1)
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
		Console.WriteLine($" {hullCount} {engineCount} {wingCount} {thrusterCount}");
		Console.WriteLine($"{quantity}");
		Console.WriteLine(
			$" {components[HullComponent.HullHs1]} {components[EngineComponent.EngineEs1]} {components[WingComponent.WingWs1]} {components[ThrusterComponent.ThrusterTs1]}"
		);
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
		Console.WriteLine($" {hullCount} {engineCount} {wingCount} {thrusterCount}");
		Console.WriteLine($"{quantity}");
		Console.WriteLine(
			$" {components[HullComponent.HullHc1]} {components[EngineComponent.EngineEc1]} {components[WingComponent.WingWc1]} {components[ThrusterComponent.ThrusterTc1]}"
		);
		return hullCount < components[HullComponent.HullHc1] * quantity
			|| engineCount < components[EngineComponent.EngineEc1] * quantity
			|| wingCount < components[WingComponent.WingWc1] * quantity
			|| thrusterCount < components[ThrusterComponent.ThrusterTc1] * quantity;
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
