using core.Components;
using core.In_memories.Items;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class VerifyHandler : IInputHandler
{
	private const String InvalidCommandMessage = "La commande est invalide.";

	public void HandleInput(String input)
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
		if (!HandlerHelper.IsDictionaryEmpty(starshipCounts))
		{
			VerifyStockAvailability(starshipCounts);
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
		var inMemoryComponent = InMemoryComponent.Instance;

		try
		{
			return (
				inMemoryComponent.CountByName(EngineComponent.EngineEc1),
				inMemoryComponent.CountByName(HullComponent.HullHc1),
				inMemoryComponent.CountByName(ThrusterComponent.ThrusterTc1),
				inMemoryComponent.CountByName(WingComponent.WingsWc1)
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

	private void PrintSufficientStock()
	{
		VerifyDisplayHandler.PrintSufficientStock();
	}

	private void PrintInsufficientStock()
	{
		VerifyDisplayHandler.PrintInsufficientStock();
	}
}
