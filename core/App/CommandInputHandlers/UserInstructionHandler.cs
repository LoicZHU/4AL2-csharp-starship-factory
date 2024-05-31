using System.Text.RegularExpressions;
using core.In_memories;
using core.Starships;
using core.UI;

namespace core.InputHandlers;

public static class UserInstructionHandler
{
	private const string QuantityWithStarshipPattern = @"(\d+)\s+(\w+)";
	private const String InvalidCommandMessage =
		"❌ La commande doit respecter ce format : [USER_INSTRUCTION] <quantité> <nom_du_vaisseau> [, <quantité> <nom_du_vaisseau>, ...]";

	public static void HandleInput(String input)
	{
		if (!IsInputValid(input.Split()))
		{
			PrintInvalidCommandMessage(InvalidCommandMessage);
			return;
		}

		var splittedBySpaceInput = input.Split(new[] { ' ' }, 2);
		if (!IsCommandNameSeparatedByOneSpace(splittedBySpaceInput))
		{
			PrintInvalidCommandMessage(InvalidCommandMessage);
			return;
		}

		var userInstructionBody = splittedBySpaceInput[1];
		var userInstruction = GetCompleteUserInstructionFrom(userInstructionBody);

		InMemoryUserInstruction.Instance.Add(userInstruction);
	}

	private static Boolean IsInputValid(String[] input)
	{
		return input.Length >= 3 && (input.Length - 1) % 2 == 0;
	}

	private static void PrintInvalidCommandMessage(String message)
	{
		MainTerminal.PrintMessage(message);
	}

	private static Boolean IsCommandNameSeparatedByOneSpace(String[] parts)
	{
		return parts.Length == 2;
	}

	private static UserInstruction GetCompleteUserInstructionFrom(String starshipsPart)
	{
		var userInstruction = UserInstruction.Create(new Dictionary<String, int>());

		foreach (var quantityAndStarship in starshipsPart.Split(", "))
		{
			var match = Regex.Match(quantityAndStarship.Trim(), QuantityWithStarshipPattern);
			if (!IsMatch(match))
			{
				PrintInvalidCommandMessage(InvalidCommandMessage);
				continue;
			}

			if (!int.TryParse(match.Groups[1].Value, out var quantity))
			{
				PrintInvalidCommandMessage(InvalidCommandMessage);
				continue;
			}

			var starshipModelInput = match.Groups[2].Value;
			var starshipName = GetStarshipName(starshipModelInput);
			if (IsUnknownStarship(starshipName))
			{
				MainTerminal.PrintMessage($"❌ Vaisseau '{starshipModelInput}' inconnu.");
				continue;
			}

			userInstruction.Add(starshipName, quantity);
		}

		return userInstruction;
	}

	private static Boolean IsMatch(Match match)
	{
		return match.Success;
	}

	private static String GetStarshipName(String name)
	{
		if (IsCargoStarship(name))
		{
			return StarshipName.Cargo;
		}
		if (IsExplorerStarship(name))
		{
			return StarshipName.Explorer;
		}
		if (IsSpeederStarship(name))
		{
			return StarshipName.Speeder;
		}

		return StarshipName.Unknown;
	}

	private static Boolean IsCargoStarship(String name)
	{
		return name.Equals(StarshipName.Cargo, StringComparison.OrdinalIgnoreCase);
	}

	private static Boolean IsExplorerStarship(String name)
	{
		return name.Equals(StarshipName.Explorer, StringComparison.OrdinalIgnoreCase);
	}

	private static Boolean IsSpeederStarship(String name)
	{
		return name.Equals(StarshipName.Speeder, StringComparison.OrdinalIgnoreCase);
	}

	private static Boolean IsUnknownStarship(String name)
	{
		return name.Equals(StarshipName.Unknown, StringComparison.OrdinalIgnoreCase);
	}
}
