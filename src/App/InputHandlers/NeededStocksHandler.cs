using System.Text.RegularExpressions;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class NeededStocksHandler : IInputHandler
{
	private const String QuantityWithStarshipPattern = @"(\d+)\s+(\w+)";
	private const String InvalidCommandMessage = "❌ La commande est invalide.";

	public void HandleInput(String input)
	{
		if (!HandlerHelper.IsCommandInputValid(input.Split()))
		{
			this.PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var splittedBySpaceInput = input.Split(new[] { ' ' }, 2);
		if (!HandlerHelper.IsCommandNameSeparatedByOneSpace(splittedBySpaceInput))
		{
			this.PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var inputBody = splittedBySpaceInput[1];

		var starshipCounts = this.GetStarshipSumsFromInput(inputBody);
		NeededStocksDisplayHandler.PrintNeededStocks(starshipCounts);
	}

	private void PrintInvalidCommand(String message)
	{
		NeededStocksDisplayHandler.PrintInvalidCommand(message);
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
				this.PrintInvalidCommand(InvalidCommandMessage);
				break;
			}
			if (HandlerHelper.IsUnknownStarship(starshipName))
			{
				this.PrintUnknownStarship(errorMessage);
				break;
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
