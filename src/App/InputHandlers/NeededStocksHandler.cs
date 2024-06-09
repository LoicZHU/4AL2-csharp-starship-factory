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
			var match = Regex.Match(quantityAndStarship.Trim(), QuantityWithStarshipPattern);
			if (!HandlerHelper.IsMatch(match))
			{
				this.PrintInvalidCommand(InvalidCommandMessage);
				break;
			}

			if (!int.TryParse(match.Groups[1].Value, out var quantity))
			{
				this.PrintInvalidCommand(InvalidCommandMessage);
				break;
			}

			var starshipNameInput = match.Groups[2].Value;
			var starshipName = HandlerHelper.GetStarshipName(starshipNameInput);
			if (HandlerHelper.IsUnknownStarship(starshipName))
			{
				this.PrintUnknownStarship($"❌ Vaisseau '{starshipNameInput}' inconnu.");
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
		Terminal.PrintMessageWithLinebreak(message);
	}
}
