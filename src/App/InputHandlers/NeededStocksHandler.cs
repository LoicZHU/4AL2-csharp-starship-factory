using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class NeededStocksHandler : IInputHandler
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

		var inputBody = splitBySpaceInput[1];

		var starshipCounts = this.GetStarshipSumsFromInput(inputBody);
		if (!UtilsFunction.IsNull(starshipCounts))
		{
			NeededStocksDisplayHandler.PrintNeededStocks(starshipCounts);
		}
	}

	private void PrintInvalidCommand(String message)
	{
		NeededStocksDisplayHandler.PrintInvalidCommand(message);
	}

	private Dictionary<String, Int32>? GetStarshipSumsFromInput(String input)
	{
		var starshipCounts = new Dictionary<String, Int32>();

		foreach (var quantityAndStarship in input.Split(", "))
		{
			var (isValid, starshipName, quantity, errorMessage) =
				HandlerHelper.ParseQuantityAndStarship(quantityAndStarship);
			if (!isValid)
			{
				this.PrintInvalidCommand(InvalidCommandMessage);
				return null;
			}
			if (HandlerHelper.IsUnknownStarship(starshipName))
			{
				this.PrintUnknownStarship(errorMessage);
				return null;
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

	private void PrintUnknownStarship(String message)
	{
		NeededStocksDisplayHandler.PrintUnknownStarship(message);
	}
}
