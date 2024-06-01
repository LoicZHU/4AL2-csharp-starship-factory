using System.Text.RegularExpressions;
using core.In_memories;
using core.Starships;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public static class UserInstructionHandler
{
	private const string QuantityWithStarshipPattern = @"(\d+)\s+(\w+)";
	private const String InvalidCommandMessage =
		"❌ La commande doit respecter ce format : [USER_INSTRUCTION] <quantité> <nom_du_vaisseau> [, <quantité> <nom_du_vaisseau>, ...]";

	public static void HandleInput(String input)
	{
		if (!HandlerHelper.IsCommandInputValid(input.Split()))
		{
			PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var splittedBySpaceInput = input.Split(new[] { ' ' }, 2);
		if (!HandlerHelper.IsCommandNameSeparatedByOneSpace(splittedBySpaceInput))
		{
			PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var userInstructionBody = splittedBySpaceInput[1];
		var userInstruction = GetCompleteUserInstructionFrom(userInstructionBody);

		InMemoryUserInstruction.Instance.Add(userInstruction);
	}

	private static void PrintInvalidCommand(String message)
	{
		MainTerminal.PrintMessage(message);
		TerminalHelper.PrintLineBreak();
	}

	private static UserInstruction GetCompleteUserInstructionFrom(String starshipsPart)
	{
		var userInstruction = UserInstruction.Create(new Dictionary<String, int>());

		foreach (var quantityAndStarship in starshipsPart.Split(", "))
		{
			var match = Regex.Match(quantityAndStarship.Trim(), QuantityWithStarshipPattern);
			if (!HandlerHelper.IsMatch(match))
			{
				PrintInvalidCommand(InvalidCommandMessage);
				continue;
			}

			if (!int.TryParse(match.Groups[1].Value, out var quantity))
			{
				PrintInvalidCommand(InvalidCommandMessage);
				continue;
			}

			var starshipNameInput = match.Groups[2].Value;
			var starshipName = HandlerHelper.GetStarshipName(starshipNameInput);
			if (HandlerHelper.IsUnknownStarship(starshipName))
			{
				MainTerminal.PrintMessage($"❌ Vaisseau '{starshipNameInput}' inconnu.");
				continue;
			}

			userInstruction.Add(starshipName, quantity);
		}

		return userInstruction;
	}
}
