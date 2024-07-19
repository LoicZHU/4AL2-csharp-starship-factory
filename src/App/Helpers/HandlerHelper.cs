using System.Text.RegularExpressions;
using core.Starships;

namespace core.Utils;

public static class HandlerHelper
{
	private const String InvalidCommandMessage = "La commande est invalide.";
	private const String QuantityWithStarshipPattern = @"(\d+)\s+(\w+)";

	public static Boolean IsCommandInputValid(String[] input)
	{
		Boolean hasAtLeastThreeArguments = input.Length >= 3;
		Boolean areArgumentsPairedWithQuantityAndStarshipName = input.Length % 2 == 1;

		return hasAtLeastThreeArguments && areArgumentsPairedWithQuantityAndStarshipName;
	}

	public static Boolean IsCommandNameSeparatedByOneSpace(String[] parts)
	{
		return parts.Length == 2;
	}

	public static Boolean IsMatch(Match match)
	{
		return match.Success;
	}

	public static String GetStarshipName(String name)
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

	public static Boolean IsCargoStarship(String name)
	{
		return name.Equals(StarshipName.Cargo, StringComparison.OrdinalIgnoreCase);
	}

	public static Boolean IsExplorerStarship(String name)
	{
		return name.Equals(StarshipName.Explorer, StringComparison.OrdinalIgnoreCase);
	}

	public static Boolean IsSpeederStarship(String name)
	{
		return name.Equals(StarshipName.Speeder, StringComparison.OrdinalIgnoreCase);
	}

	public static Boolean IsCargoOrExplorerStarship(String name)
	{
		return IsCargoStarship(name) || IsExplorerStarship(name);
	}

	public static Boolean IsUnknownStarship(String name)
	{
		return name.Equals(StarshipName.Unknown, StringComparison.OrdinalIgnoreCase);
	}

	public static (
		Boolean isValid,
		String name,
		Int32 quantity,
		String errorMessage
	) ParseQuantityAndStarship(String input)
	{
		var match = Regex.Match(input.Trim(), QuantityWithStarshipPattern);
		if (!IsMatch(match))
		{
			return (false, String.Empty, 0, InvalidCommandMessage);
		}

		var parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
		// seems to be useless:
		if (!Int32.TryParse(parts[0], out var quantity))
		{
			return (false, String.Empty, quantity, InvalidCommandMessage);
		}
		if (IsLessThanOrEqualToZero(quantity))
		{
			return (false, String.Empty, quantity, "La quantité doit être >= 1.");
		}

		var starshipNameInput = parts[1];
		var officialStarshipName = GetStarshipName(starshipNameInput);
		var starshipNameInputInTitleCase =
			$"{starshipNameInput.First().ToString().ToUpper()}{starshipNameInput.Substring(1).ToLower()}";

		return !IsUnknownStarship(officialStarshipName)
			? (true, starshipName: officialStarshipName, quantity, String.Empty)
			: (
				false,
				StarshipName.Unknown,
				quantity,
				$"Vaisseau inconnu : {starshipNameInputInTitleCase}"
			);
	}

	private static Boolean IsLessThanOrEqualToZero(Int32 quantity)
	{
		return quantity <= 0;
	}
}
